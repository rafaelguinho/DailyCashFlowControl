using DailyCashFlowControl.Application.Queries;
using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;
using MediatR;
using System.Linq;

namespace DailyCashFlowControl.Application.Handlers
{
    public class SearchTransactionsQueryHandler : IRequestHandler<SearchTransactionsQuery, IEnumerable<Transaction>>
    {
        private readonly IRepository<Transaction> _repository;

        public SearchTransactionsQueryHandler(IRepository<Transaction> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Transaction>> Handle(SearchTransactionsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                return await _repository.GetFiltered(c => c.Description.ToLower().Contains(request.Description));
            }
            return await _repository.GetAll();
        }
    }
}
