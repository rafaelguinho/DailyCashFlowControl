using DailyCashFlowControl.ConsolidatedResults.Application.Commands;
using DailyCashFlowControl.Domain.Models;
using DailyCashFlowControl.RabbitMQ.Consumers.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DailyCashFlowControl.RabbitMQ.Consumers.Handlers
{
    public class ConsolidatedResultsMessageConsumerHandler : IMessageConsumerHandler<Transaction>
    {
        private readonly IServiceProvider _serviceProvider;

        public ConsolidatedResultsMessageConsumerHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(Transaction body)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            IMediator mediator = scope.ServiceProvider.GetService<IMediator>();

            try
            {
                var response = await mediator.Send(new ConsolidatedItemResultCommand
                {
                    Date = body.Date,
                    TransactionId = body.Id,
                    Type = body.Type,
                    Value = body.Value,
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
