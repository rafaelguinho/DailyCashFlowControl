using DailyCashFlowControl.Domain.Models.Requests;
using MediatR;

namespace DailyCashFlowControl.Application.Commands
{
    public record TransactionCommand : TransactionRequest, IRequest<string>
    {
        public TransactionCommand(string? Type, decimal? Value, string? Description) : base(Type, Value, Description)
        {
        }
    }
}
