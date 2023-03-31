using DailyCashFlowControl.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DailyCashFlowControl.RabbitMQ.Consumers
{

    public class ConsolidatedResultsHandler : IMessageConsumerHandler<Transaction>
    {
        private readonly IServiceProvider _serviceProvider;

        public ConsolidatedResultsHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(Transaction body)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            ISender mediator = scope.ServiceProvider.GetService<ISender>();

            try
            {
                var response = await mediator.Send(body);

            }
            catch(Exception ex) { 
            
            }
        }
    }

    public class ConsolidatedResultsConsumer : BaseConsumer<IMessageConsumerHandler<Transaction>, Transaction>
    {
        public ConsolidatedResultsConsumer(IMessageConsumerHandler<Transaction> messageConsumerHandler, ConnectionProvider connectionProvider, RabbitMQRouting options) : base(messageConsumerHandler, connectionProvider, options)
        {
        }
    }
}
