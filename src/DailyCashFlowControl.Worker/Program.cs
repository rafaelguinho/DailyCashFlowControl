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
                    //services.AddSingleton<ParametrosExecucao>(
                    //    new ParametrosExecucao()
                    //    {
                    //        ConnectionString = args[0],
                    //        Queue = args[1]
                    //    });
                    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateConsolidatedItemResultHandler).GetTypeInfo().Assembly));
                    services.AddConsolidatedResultInfraestructure();
                    services.AddRabbitMQ();
                    services.AddMessageConsumer();
                    
                });
    }
}