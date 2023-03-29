using DailyCashFlowControl.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using DailyCashFlowControl.Main;

namespace DailyCashFlowControl.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(
                "*** Testando o consumo de mensagens com RabbitMQ + Filas ***");

            CreateHostBuilder(args).Build().Run();

            //var factory = new ConnectionFactory
            //{
            //    HostName = "rabbitmq",
            //    Port = 5671,
            //    UserName = "guest",
            //    Password = "guest"
            //};

            //var connection = factory.CreateConnection();
            //using var channel = connection.CreateModel();

            //channel.QueueDeclare("orders", durable: false,
            //                    exclusive: false,
            //                    autoDelete: false,
            //                    arguments: null);

            //var json = JsonSerializer.Serialize("oossss");
            //var body = Encoding.UTF8.GetBytes(json);

            //channel.BasicPublish(exchange: "", routingKey: "orders", body: body);

            //var consumer = new EventingBasicConsumer(channel);

            //consumer.Received += (model, ea) =>
            //{
            //    var body = ea.Body.ToArray();
            //    var message = Encoding.UTF8.GetString(body);
            //    Console.WriteLine(" [x] Recieved from Rabbit: {0}", message);
            //};

            //channel.BasicConsume(
            //    queue: "orders",
            //    autoAck: true,
            //    consumer: consumer
            //);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddSingleton<ParametrosExecucao>(
                    //    new ParametrosExecucao()
                    //    {
                    //        ConnectionString = args[0],
                    //        Queue = args[1]
                    //    });
                    services.AddRabbitMQ();
                    services.AddHostedService<RabbitMQWorker>();
                    
                });
    }
}