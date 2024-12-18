using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using Backend.Protocol;

namespace Backend;

public class UdpCommunicator : ICommunicator
{
    private const int INITIAL_PORT = 12000;
    public const int MAX_AGENTS_PER_HOST = 20;

    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly IPEndPoint _listeningEndpoint;
    private readonly List<UdpClient> _senders = new();
    private readonly UdpClient _receiver;


    private readonly string _nodeId;

    ILogger _logger;

    private readonly NetworkHelper _networkHelper = new NetworkHelper();

    public UdpCommunicator(ILoggerFactory loggerFactory, string nodeId, bool broadcast = false, int port = INITIAL_PORT)
    {
        _logger = loggerFactory.CreateLogger($"Powercher udpcommunicator {nodeId}");
        this._nodeId = nodeId;
        
        while (_networkHelper.IsPortInUse(IPAddress.Any.ToString(), port))
        {
            _logger.LogDebug($"Port {port} already used, trying {++port}");
            if (port - INITIAL_PORT > MAX_AGENTS_PER_HOST)
            {
                throw new Exception($"Too much agents on same host, aborting");
            }
        }

        // Server
        _listeningEndpoint = new IPEndPoint(broadcast?IPAddress.Any:IPAddress.Loopback, port);

        //Clients
        if (broadcast)
        {
            NetworkInterface.GetAllNetworkInterfaces()
                .Where(netInterface => netInterface.OperationalStatus == OperationalStatus.Up)
                .SelectMany(adapter => adapter.GetIPProperties()
                    .UnicastAddresses.Where(address => address.Address.AddressFamily == AddressFamily.InterNetwork /*IPV4*/))
                .ToList().ForEach(address =>  CreateUdpClient(new IPEndPoint(address.Address, 0)));
        }
        else
        {
            CreateUdpClient(new IPEndPoint(IPAddress.Loopback, 0));
        }

        _receiver = new UdpClient(_listeningEndpoint);

        _receiver.EnableBroadcast = broadcast;
        // To stop server thread
        _cancellationTokenSource = new CancellationTokenSource();

        _logger.LogInformation("UDP Communicator is in {mode} mode", broadcast ? "BROADCAST" : "LOCAL");
    }

    private void CreateUdpClient(IPEndPoint ipAndPort)
    {
        // Create a UdpClient for the broadcast
        UdpClient udpClient = new UdpClient();

        udpClient.EnableBroadcast = true;
        udpClient.Client.Bind(ipAndPort);//attach to adapter
        _senders.Add(udpClient);
        
        _logger.LogInformation("Binded to {ip}",ipAndPort.Address);
    }

    public void Start()
    {
        if (OnMessageReceived == null)
        {
            _logger.LogWarning("No callback registered for incoming messages");
        }

        // Run the listening task in a separate thread
        Task.Run(() => Listen(_cancellationTokenSource.Token), _cancellationTokenSource.Token);

        //Hello
        //SendBroadcastMessage(MessageType.HELLO.Create(nodeId));

        //Initial time sync
        //SendBroadcastMessage(MessageType.TIME_SYNC.Create(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));
    }

    // Listen on the UDP port asynchronously with a cancellation token
    private async Task Listen(CancellationToken token)
    {
        _logger.LogInformation($"Listening UDP traffic on {_listeningEndpoint.Address}:{_listeningEndpoint.Port}");

        try
        {
            while (!token.IsCancellationRequested)
            {
                // Receive data
                var result = await _receiver.ReceiveAsync(token);
                var receivedMessage = Encoding.UTF8.GetString(result.Buffer);

                OnMessageReceived(Envelope.FromJson(receivedMessage));
            }
        }
        catch (SocketException e) when (e.Message.Contains("cancel"))
        {
            //Valid close of socket
        }
        //Other issue
        catch (Exception ex)
        {
            _logger.LogError("Error while listening: {ex}", ex);
        }
    }

    public void Send(string message)
    {
        try
        {
            var data = Encoding.UTF8.GetBytes(message);

            // Send the data to the broadcast address on the specified port
            var endPort = INITIAL_PORT + MAX_AGENTS_PER_HOST;
            for (int targetPort = INITIAL_PORT; targetPort <= endPort; targetPort++)
            {
                _senders.ForEach(sender =>
                    sender.SendAsync(data, data.Length, new IPEndPoint(IPAddress.Loopback, targetPort)));
            }

            _logger.LogInformation("Sent: {message} on ports {start}-{end} through following adapter(s) : {adapters}", message, INITIAL_PORT, endPort, _senders.Select(sender=>sender.Client.LocalEndPoint).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError("Error sending message {message}", ex);
        }
    }


    // Stop the agent and close the UDP client
    public void Stop()
    {
        // Cancel the listening task and close the UDP client
        _cancellationTokenSource.Cancel();
        _senders.ForEach(sender=>sender.Close());
        _receiver.Close();
        _logger.LogInformation("Network agent stopped.");
    }

    public void Send(Envelope envelope,string? topic = null)
    {
        Send(envelope.ToJson());
    }

    public Action<Envelope>? OnMessageReceived { private get; set; }

    public override string ToString()
    {
        return $"UDPCommunicator {_nodeId}";
    }
}