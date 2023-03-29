using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DailyCashFlowControl.RabbitMQ
{
    public class RabbitMQWorker : BackgroundService
    {
        private readonly ConnectionProvider _connectionProvider;

        public RabbitMQWorker(ConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var connection = _connectionProvider.GetConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("orders", durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var json = JsonSerializer.Serialize("oossss");
            var body = Encoding.UTF8.GetBytes(json);

            //channel.BasicPublish(exchange: "", routingKey: "orders", body: body);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;

            channel.BasicConsume(
                queue: "orders",
                autoAck: false,
                consumer: consumer
            );

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine(
                    $"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await Task.Delay(500, stoppingToken);
            }


        }

        private async Task Consumer_Received(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Recieved from Rabbit: {0}", message);
        }
       
    }
}

