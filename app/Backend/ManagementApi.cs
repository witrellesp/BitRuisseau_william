using Backend.Protocol;

namespace Backend;

public class ManagementApi
{
    private readonly Agent agent;
    private WebApplication webapp;
    private const string HOST = "localhost";
    private const int DEFAULT_PORT = 8080;
    private readonly int port;
    
    ILogger logger = Program.loggerFactory.CreateLogger<ManagementApi>();
    

    public ManagementApi(Agent agent, int port = DEFAULT_PORT)
    {
        var networkHelper = new NetworkHelper();
        if (networkHelper.IsPortInUse(HOST,port))
        {
            port++;
        }
        this.agent = agent;
        this.port = port;
        
        var builder = WebApplication.CreateBuilder();
        builder.WebHost
            .UseKestrel() //cross-platform http server
            .UseUrls($"http://{HOST}:{port}")
            .ConfigureLogging(
                loggingBuilder=>
                {
                    //remove all default info
                    loggingBuilder.SetMinimumLevel(LogLevel.Warning);
                    //removes standard info on any requests
                    //loggingBuilder.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
                })
            ; 
        
        webapp = builder.Build();
        
        ConfigureRoutes();
    }

    private void ConfigureRoutes()
    {
        //INDEX
        webapp.MapGet("/", ()=>Task.FromResult(new Status(){Stats = agent.Statistics}));
        
        //Technical actions on agent
        webapp.MapGet("/start", this.agent.Start);
        webapp.MapGet("/stop", this.agent.Stop);
        
        //Playground
        webapp.MapGet("/stats", () => Task.FromResult(this.agent.Statistics));
        webapp.MapGet("/send/hello/{message}", (string message) =>
        {
            agent.Send(new Envelope(
                message : message, 
                type : MessageType.HELLO,
                senderId : agent.NodeId));
            return $"Message '{message}' sent.\n";
        });
    }

    public void Start()
    {
        logger.LogInformation("Starting mgmt api on http://localhost:{port} (Press ctrl-c to stop)",port);
        webapp.Run();
    }
}

class Status
{
    public DateTime Date { get; set; } = DateTime.Now;
    public ClusterStatistics? Stats { get; set; }
}