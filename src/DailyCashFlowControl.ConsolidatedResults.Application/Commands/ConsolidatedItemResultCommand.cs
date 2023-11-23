using DailyCashFlowControl.Domain.Models;
using MediatR;

namespace DailyCashFlowControl.ConsolidatedResults.Application.Commands
{
    public class ConsolidatedItemResultCommand : IRequest<ConsolidatedItemResult>
    {
        public DateTime Date { get; set; }

        public string Type { get; set; }

        public string TransactionId { get; set; }

        public decimal Value { get; set; }

        public string HubClientId { get; set; }
    }
}
