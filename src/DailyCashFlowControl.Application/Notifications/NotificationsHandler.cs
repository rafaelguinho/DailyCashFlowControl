using DailyCashFlowControl.Domain.Interfaces;
using MediatR;

namespace DailyCashFlowControl.Transactions.Application.Notifications
{
    public class NotificationsHandler :
                            INotificationHandler<AddedTransactionNotification>
    {
        IMessageProducer _messagePublisher;

        public NotificationsHandler(IMessageProducer messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public Task Handle(AddedTransactionNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _messagePublisher.SendMessage(notification);
            });
        }

    }
}
