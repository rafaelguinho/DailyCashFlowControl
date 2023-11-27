using DailyCashFlowControl.ConsolidatedResults.Infra;
using DailyCashFlowControl.Domain.Dtos;
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
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

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
            services.AddTransient<IMessageConsumerHandler<AddedTransactionNotificationDto>, ConsolidatedResultsMessageConsumerHandler>();
            
            services.AddHostedService<RabbitMQConsumer<AddedTransactionNotificationDto>>((s) => {
                ConnectionProvider conn = s.GetRequiredService<ConnectionProvider>();
                IMessageConsumerHandler<AddedTransactionNotificationDto> handler = s.GetRequiredService<IMessageConsumerHandler<AddedTransactionNotificationDto>>();
                return new RabbitMQConsumer<AddedTransactionNotificationDto>(conn, new RabbitMQRouting
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

            BsonClassMap.RegisterClassMap<ConsolidatedItemResult>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(c => c.Date).SetSerializer(new MyDateTimeSerializer());
            });

            services.AddScoped<IRepository<ConsolidatedItemResult>, ConsolidatedItemResultRepository>();
            services.AddSingleton<IConsolidatedResultNotification, ConsolidatedResultNotificationHub>();

            return services;
        }
    }
}
