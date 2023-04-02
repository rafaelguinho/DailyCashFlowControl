namespace DailyCashFlowControl.RabbitMQ.Consumers.Interfaces
{
    public interface IMessageConsumerHandler<T>
    {
        Task Handle(T body);
    }
}