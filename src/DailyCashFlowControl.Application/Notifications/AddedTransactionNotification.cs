using DailyCashFlowControl.Domain.Models;
using DailyCashFlowControl.Domain.Models.Requests;
using MediatR;

namespace DailyCashFlowControl.Transactions.Application.Notifications
{
    public class AddedTransactionNotification : Transaction, INotification
    {
        public AddedTransactionNotification(string id,  string Type, string Description, decimal Value, DateTime dateTime) : base(id, Type, Value, Description, dateTime)
        {
        }
    }
}
