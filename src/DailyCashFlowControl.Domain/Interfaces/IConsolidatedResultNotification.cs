namespace DailyCashFlowControl.Domain.Interfaces
{
    public interface IConsolidatedResultNotification
    {
        Task SendNotification(string clientId, string message);
    }
}
