using Backend.Protocol;

namespace Backend;

public interface ICommunicator
{
    public void Send(Envelope envelope,string? topic = null);
    
    public Action<Envelope>? OnMessageReceived { set; }
    public void Start();
    public void Stop();
}