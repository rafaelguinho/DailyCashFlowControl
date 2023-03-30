using DailyCashFlowControl.Domain.Models.Requests;
using MediatR;

namespace DailyCashFlowControl.Application.Commands
{
    public record TransactionCommand : TransactionRequest, IRequest<string>
    {
        public TransactionCommand(string? Type, decimal? Value) : base(Type, Value)
        {
        }
    }
}
