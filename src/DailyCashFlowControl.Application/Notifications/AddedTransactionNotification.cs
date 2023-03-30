using DailyCashFlowControl.Domain.Models.Requests;
using MediatR;

namespace DailyCashFlowControl.Application.Notifications
{
    public record AddedTransactionNotification : TransactionRequest, INotification
    {
        public AddedTransactionNotification(string id,  string? Type, decimal? Value, DateTime dateTime) : base(Type, Value)
        {
            Id = id;
            DateTime = dateTime;
        }

        public string Id { get; set; }

        public DateTime DateTime { get; set; }
    }
}
