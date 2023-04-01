using DailyCashFlowControl.ConsolidatedResults.Application.Queries;
using DailyCashFlowControl.Domain.Dtos;
using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;
using MediatR;

namespace DailyCashFlowControl.ConsolidatedResults.Application.Handlers
{
    public class GetConsolidatedResultQueryHandler : IRequestHandler<GetConsolidatedResultQuery, ConsolidatedResultDto>
    {
        private readonly IRepository<ConsolidatedItemResult> _repository;

        public GetConsolidatedResultQueryHandler(IRepository<ConsolidatedItemResult> repository)
        {
            _repository = repository;
        }
        public async Task<ConsolidatedResultDto?> Handle(GetConsolidatedResultQuery request, CancellationToken cancellationToken)
        {
            if (!request.Date.HasValue) return null;

            var items = await _repository.GetFiltered(c => c.Date.Date == request.Date.Value);

            if (!items.Any()) return null;

            var lasItemOfTheDay = items.OrderBy(i => i.Order).Last();

            return new ConsolidatedResultDto(lasItemOfTheDay.TotalByDate);


        }
    }
}
