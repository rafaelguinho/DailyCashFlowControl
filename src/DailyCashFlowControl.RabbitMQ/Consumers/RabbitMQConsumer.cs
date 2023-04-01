using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace DailyCashFlowControl.RabbitMQ.Consumers
{
    public class RabbitMQConsumer<T> : BackgroundService where T : class
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;
        IMessageConsumerHandler<T> _consumerHandler;

        public RabbitMQConsumer(ConnectionProvider connectionProvider, RabbitMQRouting options, IMessageConsumerHandler<T> consumerHandler)
        {
            _connection = connectionProvider.GetConnection();
            _queueName = options.Queue;
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(_queueName, durable: true,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

            _consumerHandler = consumerHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                T handleBody = JsonSerializer.Deserialize<T>(message, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (handleBody == null) throw new Exception("Message Body is null");

                Console.WriteLine($"Received message: {handleBody}");

                await _consumerHandler.Handle(handleBody);

                
                await Task.Yield();
            };

            _channel.BasicConsume(queue: _queueName,
                                  autoAck: true,
                                  consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }

}
