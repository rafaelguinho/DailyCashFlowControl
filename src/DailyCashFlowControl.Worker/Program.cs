using DailyCashFlowControl.ConsolidatedResults.Application.Handlers;
using DailyCashFlowControl.Main;
using System.Reflection;

namespace DailyCashFlowControl.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateConsolidatedItemResultHandler).GetTypeInfo().Assembly));
                    services.AddConsolidatedResultInfraestructure();
                    services.AddRabbitMQ();
                    services.AddMessageConsumer();
                    
                });
    }
}