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

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _service.Consume();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
