using DailyCashFlowControl.Domain.Models;
using DailyCashFlowControl.RabbitMQ.Consumers.Interfaces;

namespace DailyCashFlowControl.RabbitMQ.Consumers
{
    public class ConsolidatedResultsConsumer : BaseConsumer<IMessageConsumerHandler<Transaction>, Transaction>, IConsolidatedResultsConsumer
    {
        public ConsolidatedResultsConsumer(IMessageConsumerHandler<Transaction> messageConsumerHandler, ConnectionProvider connectionProvider, RabbitMQRouting options) : base(messageConsumerHandler, connectionProvider, options)
        {
        }
    }
}
