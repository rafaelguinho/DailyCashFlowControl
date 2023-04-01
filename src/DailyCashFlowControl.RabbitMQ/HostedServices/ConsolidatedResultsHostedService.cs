using DailyCashFlowControl.RabbitMQ.Consumers.Interfaces;
using Microsoft.Extensions.Hosting;

namespace DailyCashFlowControl.RabbitMQ.HostedServices
{
    public class ConsolidatedResultsHostedService : IHostedService
    {
        private readonly IConsolidatedResultsConsumer _service;

        public ConsolidatedResultsHostedService(IConsolidatedResultsConsumer service)
        {
            _service = service;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //_service.Consume();

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    Console.WriteLine(
            //        $"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            //    await Task.Delay(500, stoppingToken);
            //}

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine(
                    $"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await Task.Delay(500, cancellationToken);
            }

            //return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    return Task.CompletedTask;
        //}
    }
}
