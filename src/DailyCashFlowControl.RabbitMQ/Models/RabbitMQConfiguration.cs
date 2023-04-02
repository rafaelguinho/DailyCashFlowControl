namespace DailyCashFlowControl.RabbitMQ.Models
{
    public class RabbitMQRouting
    {
        public string Queue { get; set; }
    }

    public class RabbitMQConfiguration
    {
        public List<RabbitMQRouting> RabbitMQRouting { get; set; }
    }
}
