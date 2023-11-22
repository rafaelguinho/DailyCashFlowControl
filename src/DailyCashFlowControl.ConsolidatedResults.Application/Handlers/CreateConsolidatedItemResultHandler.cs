using DailyCashFlowControl.ConsolidatedResults.Application.Commands;
using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;
using MediatR;

namespace DailyCashFlowControl.ConsolidatedResults.Application.Handlers
{
    public class CreateConsolidatedItemResultHandler : IRequestHandler<ConsolidatedItemResultCommand, ConsolidatedItemResult>
    {
        private readonly IRepository<ConsolidatedItemResult> _repository;

        public CreateConsolidatedItemResultHandler(IRepository<ConsolidatedItemResult> repository)
        {
            _repository = repository;
        }

        public async Task<ConsolidatedItemResult> Handle(ConsolidatedItemResultCommand command, CancellationToken cancellationToken)
        {
            var dailyItems = await _repository.GetFiltered(c => c.DateKey == command.Date.Date.ToShortDateString());

            decimal currentSubTotal = dailyItems.Sum(d => d.Value);
            int newIndex = dailyItems.Any(d => d.Order > 0) ? (dailyItems.Max(d => d.Order)) + 1 : 1;

            decimal value = command.Type == "debit" ? command.Value * -1 : command.Value;
            decimal subTotal = currentSubTotal + value;

            return await _repository.Add(new ConsolidatedItemResult(command.Date, command.Date.Date.ToShortDateString(), command.TransactionId, value, subTotal, newIndex));

        }
    }
}
