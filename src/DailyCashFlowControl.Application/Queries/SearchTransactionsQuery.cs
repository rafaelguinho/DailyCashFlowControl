using DailyCashFlowControl.Domain.Models;
using MediatR;

namespace DailyCashFlowControl.Application.Queries
{
    public class SearchTransactionsQuery: IRequest<IEnumerable<Transaction>>
    {
        public string? Id { get; set; }
        public string? Type { get; set; }

        public string? Description { get; set; }

        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}
