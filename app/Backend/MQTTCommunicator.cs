﻿using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Backend.Protocol;
using MQTTnet;
using MQTTnet.Protocol;


namespace Backend;

public class MqttCommunicator : ICommunicator
{
    private const string DefaultTopic = "media/player";
    private readonly string _brokerIp;
    private IMqttClient _mqttClient;
    private readonly ILogger _logger;
    private readonly string _username;
    private readonly string _password;
    private readonly string _nodeId;
    private readonly string _topic;

    private readonly MqttClientFactory _factory = new ();
    
    private bool _retain = false;
    private MqttQualityOfServiceLevel _qos = MqttQualityOfServiceLevel.AtLeastOnce;

    public MqttCommunicator(ILoggerFactory loggerFactory,
        string brokerHost,string nodeId,string topic=MqttCommunicator.DefaultTopic,
        string username="ict", string password="321")
    {
        _nodeId = nodeId;
        _topic = topic;
        _brokerIp = GetPreferredIpAddress(brokerHost).ToString();
        _logger = loggerFactory.CreateLogger<MqttCommunicator>();
        _username = username;
        _password = password;
        _mqttClient = _factory.CreateMqttClient();
    }

    IPAddress GetPreferredIpAddress(string host)
    {
        //priority on the dgep ipv4 address
        return Dns.GetHostAddresses(host)
            .Where(/*V4*/address=>address.AddressFamily == AddressFamily.InterNetwork)
            .Where(address=>address.ToString().StartsWith("10"))
            .FirstOrDefault(Dns.GetHostAddresses(host)[0]);
    }

    public void Send(Envelope envelope,string? topic = null)
    {
        //For Senders only
        if (!_mqttClient.IsConnected)
        {
            Connect();
        }

        var finalTopic = topic ?? _topic;
     

        var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(topic??_topic)
            .WithRetainFlag(_retain)
            .WithQualityOfServiceLevel(_qos)
            .WithPayload(envelope.ToJson())
            .Build();

        //Async => sync
        var publishResult = _mqttClient.PublishAsync(applicationMessage).Result;
        if (!publishResult.IsSuccess)
        {
            _logger.LogError(publishResult.ReasonString);
        }
    }

    public Action<Envelope>? OnMessageReceived { private get; set; }
    public void Start()
    {

        // Setup message handling before connecting so that queued messages
        // are also handled properly. When there is no event handler attached all
        // received messages get lost.
        _mqttClient.ApplicationMessageReceivedAsync += message =>
        {
            _logger.LogDebug("Received message {}",message.ApplicationMessage.Topic);

            var payload = Encoding.UTF8.GetString(message.ApplicationMessage.Payload);
            try
            {
                var envelope = Envelope.FromJson(payload);
                OnMessageReceived(envelope);

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }

        };

        //Async => sync
        Connect();

        //Async => sync
        var mqttSubscribeOptions = _factory
            .CreateSubscribeOptionsBuilder()
            .WithTopicFilter(_topic,
                _qos,
                //noLocal:..,
                retainAsPublished:_retain,
                retainHandling:MqttRetainHandling.SendAtSubscribe)
            .Build();

        var subscriptionResult = _mqttClient.SubscribeAsync(mqttSubscribeOptions).Result;
        if (subscriptionResult.Items.Count() < 0)
        {
            throw new InvalidOperationException($"Failed to connect to the MQTT broker. Reason: {subscriptionResult.ReasonString}");
        }

    }

    private void Connect()
    {
        var options = new MqttClientOptionsBuilder()
            .WithClientId(_nodeId)
            .WithTcpServer(_brokerIp, 1883) //
            .WithCredentials(_username,_password)
            .Build();

        var connectResult = _mqttClient.ConnectAsync(options).Result;
        if (connectResult.ResultCode != MqttClientConnectResultCode.Success)
        {
            throw new InvalidOperationException($"Failed to connect to the MQTT broker. Reason: {connectResult.ReasonString}");
        }

    }

    public void Stop()
    {
        _mqttClient.DisconnectAsync();
    }
   
}