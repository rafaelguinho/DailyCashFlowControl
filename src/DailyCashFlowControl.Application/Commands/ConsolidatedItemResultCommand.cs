using MediatR;

namespace DailyCashFlowControl.Application.Commands
{
    public class ConsolidatedItemResultCommand : IRequest<string>
    {
        public DateTime Date { get; set; }

        public string Type { get; set; }

        public string TransactionId { get; set; }

        public decimal Value { get; set; }
    }
}
