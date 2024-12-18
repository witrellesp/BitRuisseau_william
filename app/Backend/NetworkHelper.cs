using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Backend;

public class NetworkHelper
{
    public bool IsPortInUse(string adrress,int port)
    {
        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        var tcpConnections = ipGlobalProperties.GetActiveTcpListeners();
        var udpConnections = ipGlobalProperties.GetActiveUdpListeners();

        return tcpConnections.Concat(udpConnections).Any(endpoint => endpoint.Port == port);
    }
}