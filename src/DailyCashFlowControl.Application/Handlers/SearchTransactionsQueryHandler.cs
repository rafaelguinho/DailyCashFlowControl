using DailyCashFlowControl.Transactions.Application.Queries;
using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;
using MediatR;

namespace DailyCashFlowControl.Transactions.Application.Handlers
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
                IEnumerable<Transaction> result = await _repository.GetFiltered(c => c.Description.ToLower().Contains(request.Description));

                return Enumerable.DefaultIfEmpty(result);
            }
            return await _repository.GetAll();
        }
    }
}
