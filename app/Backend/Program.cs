namespace Backend;

internal class Program
{
    public static ILoggerFactory loggerFactory;
    public static async Task Main(string[] args)
    {
        //Init logger factory
        loggerFactory=LoggerFactory.Create(
            builder => builder
                .AddConsole()
                .SetMinimumLevel(args.Contains("--debug")?LogLevel.Debug:LogLevel.Information));
        
        // Create a new network agent that listens on all local interfaces (0.0.0.0) and auto inc PORT if already used
        var agent = new Agent(loggerFactory,"localhost"); // TODO vérifier
        agent.Start();
        Thread.Sleep(250);//waits for server start
        
        //Start mgmt api
        var managementApi = new ManagementApi(agent);
        managementApi.Start();//blocking call until ctrl-c
        
        agent.Stop();

    }
    

}