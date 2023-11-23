using DailyCashFlowControl.Domain.Models;
using MediatR;

namespace DailyCashFlowControl.Transactions.Application.Notifications
{
    public class AddedTransactionNotification : Transaction, INotification
    {
        public AddedTransactionNotification(string id,  string Type, string Description, decimal Value, DateTime dateTime, string? hubClientId) : base(id, Type, Value, Description, dateTime)
        {
            HubClientId = hubClientId;
        }

        public string? HubClientId { get; set; }
    }
}
