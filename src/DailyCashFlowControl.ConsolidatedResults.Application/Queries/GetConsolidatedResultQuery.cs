using DailyCashFlowControl.Domain.Dtos;
using MediatR;

namespace DailyCashFlowControl.ConsolidatedResults.Application.Queries
{
    public class GetConsolidatedResultQuery : IRequest<ConsolidatedResultDto>
    {
        public DateTime? Date { get; set; }
    }
}
