﻿using DailyCashFlowControl.ConsolidatedResults.Application.Commands;
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
            var dailyItems = await _repository.GetFiltered(c => c.Date.Date == command.Date.Date);

            decimal subTotal = dailyItems.Sum(d => d.Value);
            int newIndex = (dailyItems.Max(d => d.Order)) + 1;

            decimal value = command.Type == "debit" ? command.Value * -1 : command.Value;

            return await _repository.Add(new ConsolidatedItemResult(command.Date, command.TransactionId, value, subTotal, newIndex));

        }
    }
}