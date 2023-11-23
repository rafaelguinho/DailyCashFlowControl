namespace DailyCashFlowControl.Domain.Dtos
{
    public record AddedTransactionNotificationDto(string Id, string Type, string Description, decimal Value, DateTime Date, string HubClientId);

}
