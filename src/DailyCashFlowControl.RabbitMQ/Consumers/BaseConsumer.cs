using DailyCashFlowControl.RabbitMQ.Consumers.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DailyCashFlowControl.RabbitMQ.Consumers
{
    public interface IMessageConsumerHandler<T>
    {
        Task Handle(T body);
    }

    public class RabbitMQConfiguration
    {
        public List<RabbitMQRouting> RabbitMQRouting { get; set; }
    }

    public class RabbitMQRouting
    {
        public string Queue { get; set; }
    }

    public abstract class BaseConsumer<THandle, THandleBody>: IMessageConsumer where THandle : IMessageConsumerHandler<THandleBody>
    {
        private readonly THandle _messageConsumerHandler;
        private readonly ConnectionProvider _connectionProvider;
        private readonly RabbitMQRouting _options;

        public BaseConsumer(THandle messageConsumerHandler, ConnectionProvider connectionProvider, RabbitMQRouting options)
        {
            _messageConsumerHandler = messageConsumerHandler;
            _connectionProvider = connectionProvider;
            _options = options;
        }

        public void Consume()
        {
            var connection = _connectionProvider.GetConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(_options.Queue, durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;

            channel.BasicConsume(
                queue: _options.Queue,
                autoAck: false,
                consumer: consumer
            );
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var strBody = Encoding.UTF8.GetString(body);

            var headers = e.BasicProperties.Headers;

            THandleBody handleBody = JsonSerializer.Deserialize<THandleBody>(strBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            await _messageConsumerHandler.Handle(handleBody);
        }
    }
}
