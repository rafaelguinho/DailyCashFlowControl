using DailyCashFlowControl.Transactions.Application.Commands;
using DailyCashFlowControl.Transactions.Application.Notifications;
using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;
using MediatR;

namespace DailyCashFlowControl.Transactions.Application.Handlers
{

    // MediatR Request Handler
    public class AddTransactionHandler : IRequestHandler<TransactionCommand, Transaction>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Transaction> _repository;

        public AddTransactionHandler(IMediator mediator, IRepository<Transaction> repository)
        {
            _mediator = mediator;
            _repository = repository;   
        }

        public async Task<Transaction> Handle(TransactionCommand command, CancellationToken cancellationToken)
        {
            Transaction transaction = await _repository.Add(new Transaction(command.Type, command.Value.Value, command.Description));

            await _mediator.Publish(new AddedTransactionNotification(transaction.Id, transaction.Type, transaction.Description, transaction.Value, transaction.Date));
            return transaction;
        }
    }
}
