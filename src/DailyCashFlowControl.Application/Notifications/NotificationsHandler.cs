using DailyCashFlowControl.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyCashFlowControl.Application.Notifications
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
