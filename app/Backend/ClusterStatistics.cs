using System.Text;
using Microsoft.Extensions.Primitives;

namespace Backend;

public class ClusterStatistics
{
    private readonly Dictionary<string, object> nodes;
    public int MessagesSent { get; set; }
    public int MessagesReceived { get; set; }
    
    public int NodesCount { get => nodes.Count; }

    public ClusterStatistics(Dictionary<string, object> nodes)
    {
        this.nodes = nodes;
    }

    public override string ToString()
    {
        return new StringBuilder()
                   .Append($"Messages sent: {MessagesSent}\n")
                   .Append($"Messages received: {MessagesReceived}\n")
                   .Append($"Nodes : {NodesCount}")
            .ToString();
    }
}