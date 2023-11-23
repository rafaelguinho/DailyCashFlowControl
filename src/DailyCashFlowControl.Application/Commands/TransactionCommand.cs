using DailyCashFlowControl.Domain.Models;
using DailyCashFlowControl.Domain.Models.Requests;
using MediatR;

namespace DailyCashFlowControl.Transactions.Application.Commands
{
    public record TransactionCommand : TransactionRequest, IRequest<Transaction>
    {
        public TransactionCommand(string? Type, decimal? Value, string? Description, string? HubClientId) : base(Type, Value, Description, HubClientId)
        {
        }
    }
}
