using DailyCashFlowControl.ConsolidatedResults.Infra;
using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;
using DailyCashFlowControl.RabbitMQ.Consumers;
using DailyCashFlowControl.RabbitMQ.Consumers.Handlers;
using DailyCashFlowControl.RabbitMQ.Consumers.Interfaces;
using DailyCashFlowControl.RabbitMQ.Models;
using DailyCashFlowControl.Transactions.Infra;
using Microsoft.Extensions.DependencyInjection;

namespace DailyCashFlowControl.Main
{
    public static class DIConfigurations
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<ConnectionProvider>(c =>
            {
                return new ConnectionProvider("amqp://guest:guest@rabbitmq:5672");
            });

            return services;
        }

        public static IServiceCollection AddMessageProducer(this IServiceCollection services)
        {
            services.AddScoped<IMessageProducer, RabbitMQProducer>();

            return services;
        }

        public static IServiceCollection AddMessageConsumer(this IServiceCollection services)
        {
            services.AddTransient<IMessageConsumerHandler<Transaction>, ConsolidatedResultsMessageConsumerHandler>();


            services.AddHostedService<RabbitMQConsumer<Transaction>>((s) => {
                ConnectionProvider conn = s.GetRequiredService<ConnectionProvider>();
                IMessageConsumerHandler<Transaction> handler = s.GetRequiredService<IMessageConsumerHandler<Transaction>>();
                return new RabbitMQConsumer<Transaction>(conn, new RabbitMQRouting
                {
                    Queue = "orders"
                }, handler);
            });

            return services;
        }

        public static IServiceCollection AddTransactionInfraestructure(this IServiceCollection services)
        {
            services.AddSingleton<IRepository<Transaction>, TransactionRepository>();

            return services;
        }

        public static IServiceCollection AddConsolidatedResultInfraestructure(this IServiceCollection services)
        {
            services.AddSingleton<IRepository<ConsolidatedItemResult>, ConsolidatedItemResultRepository>();

            return services;
        }
    }
}
