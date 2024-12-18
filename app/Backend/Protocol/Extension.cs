using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.Protocol;

public static class Extension
{
    public static void Send(this MessageType target, Agent agent, object? message)
    {
        var envelope = new Envelope(agent.NodeId, target, message==null?"":JsonSerializer.Serialize(message));
        agent.Send(envelope);
    }
}