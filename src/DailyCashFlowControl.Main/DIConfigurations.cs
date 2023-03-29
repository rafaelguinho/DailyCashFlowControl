using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace DailyCashFlowControl.Main
{
    public static class DIConfigurations
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<ConnectionProvider>(c => {
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
            services.AddHostedService<RabbitMQWorker>();

            return services;
        }
    }
}
