namespace DailyCashFlowControl.Domain.Interfaces
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
