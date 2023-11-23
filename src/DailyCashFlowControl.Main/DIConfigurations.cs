using DailyCashFlowControl.ConsolidatedResults.Infra;
using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;
using DailyCashFlowControl.Hubs;
using DailyCashFlowControl.RabbitMQ.Consumers;
using DailyCashFlowControl.RabbitMQ.Consumers.Handlers;
using DailyCashFlowControl.RabbitMQ.Consumers.Interfaces;
using DailyCashFlowControl.RabbitMQ.Models;
using DailyCashFlowControl.Transactions.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyCashFlowControl.Main
{
    public static class DIConfigurations
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<ConnectionProvider>(s =>
            {
                IConfiguration configuration = s.GetRequiredService<IConfiguration>();
                var uri = configuration["RabbitMq:uri"];
                return new ConnectionProvider(uri);
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
            services.AddScoped<IConsolidatedItemResultContext>((s =>
            {
                IConfiguration configuration = s.GetRequiredService<IConfiguration>();
                return new ConsolidatedItemResultContext(configuration);
            }));

            services.AddScoped<IRepository<ConsolidatedItemResult>, ConsolidatedItemResultRepository>();
            services.AddSingleton<IConsolidatedResultNotification, ConsolidatedResultNotificationHub>();

            return services;
        }
    }
}
