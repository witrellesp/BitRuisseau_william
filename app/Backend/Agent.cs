//Auteur : JMY
//Date   : 28.10.2024 
//Lieu   : ETML
//Descr. : Agent de base pour powercher

using System.Collections.Immutable;
using System.Text.Json;
using Backend.Protocol;

namespace Backend;

public class Agent
{
    private const string CommunicatorUdp = "udp";
    private const string CommunicatorMqtt = "mqtt";


    private readonly ClusterStatistics _statistics;
    public ClusterStatistics Statistics { get => _statistics; }

    public readonly string NodeId;

    private readonly Dictionary<string, object> _nodes = new();
    
    private readonly Dictionary<string,ICommunicator> _communicators = new();
    
    
    public int NodesCount => _nodes.Count;
    public ImmutableDictionary<string,object> Nodes => _nodes.ToImmutableDictionary();

    private readonly ILogger _logger;

    public Agent(ILoggerFactory loggerFactory, string broker, Action<Envelope>? onMessageReceived=null,bool isMotherNature=false,string idPrefix="")
    {
        _logger = loggerFactory.CreateLogger($"Powercher Agent {NodeId}");

        _statistics = new(_nodes);

        NodeId = (isMotherNature?"MN-":"")+idPrefix + Guid.NewGuid();
        
        //broadcast on wifi not working a lot...
        //_communicators.Add(CommunicatorUdp,new UdpCommunicator(loggerFactory,NodeId,broadcast));
        _communicators.Add(CommunicatorMqtt,new MqttCommunicator(loggerFactory,broker,NodeId));
        
        //Messages handling
        _communicators.Values.ToList().ForEach(communicator=>communicator.OnMessageReceived=onMessageReceived??DefaultOnMessageReceived);
        
        //Register itself
        _nodes.Add(NodeId,"nada");
    }

    private void DefaultOnMessageReceived(Envelope envelope)
    {
        switch (envelope.Type)
        {
            case MessageType.HELLO:
                _logger.LogDebug("Received Hello  from {sender}",envelope.SenderId);
                if (_nodes.TryAdd(envelope.SenderId, "nada"))
                {
                    _logger.LogDebug("Added new sender {sender}",envelope.SenderId);
                }
                break;
            case MessageType.GOOD_BYE:
                _logger.LogDebug("Received GoodBye  from {sender}",envelope.SenderId);
                if (_nodes.Remove(envelope.SenderId))
                {
                    _logger.LogDebug("Removed sender {sender}",envelope.SenderId);
                }
                break;
            default:
                _logger.LogInformation("Received message {message}",envelope);
                break;
        }
        
        _statistics.MessagesReceived++;
    }

    // Start listening on a dedicated thread
    public void Start()
    {
        _communicators.Values.ToList().ForEach(communicator =>communicator.Start());
        
        //Hello
        MessageType.HELLO.Send(this,null);
        
    }

    public void Stop()
    {
        MessageType.GOOD_BYE.Send(this,null);
        _communicators.Values.ToList().ForEach(communicator => communicator.Stop());
    }

    public void Send(Envelope envelope)
    {
        string communicatorId;
        switch (envelope.Type)
        {
            default: communicatorId = CommunicatorMqtt;
                break;
        }
        _communicators[communicatorId].Send(envelope);
        _statistics.MessagesSent++;
        _logger.LogInformation("{} sent",envelope);
    }


    public override string ToString()
    {
        return $"Powercher Agent {this.NodeId}";
    }




}