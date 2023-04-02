using DailyCashFlowControl.RabbitMQ.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DailyCashFlowControl.Domain.Interfaces
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly ConnectionProvider _connectionProvider;

        public RabbitMQProducer(ConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void SendMessage<T>(T message)
        {
            var connection = _connectionProvider.GetConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("orders", durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
        }
    }
}
