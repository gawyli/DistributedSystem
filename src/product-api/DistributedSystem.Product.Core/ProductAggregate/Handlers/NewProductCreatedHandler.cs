using DistributedSystem.Product.Core.ProductAggregate.Events;
using DistributedSystem.Product.Core.ProductAggregate.Events.Outbox;
using DistributedSystem.Shared.Core.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Infrastructure.Outbox.Handlers;
public class NewProductCreatedHandler : INotificationHandler<NewProductCreatedEvent>
{
    private readonly IOutbox _outbox;

    public NewProductCreatedHandler(IOutbox outbox)
    {
        _outbox = outbox;
    }

    public Task Handle(NewProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        return _outbox.PublishDelayAsync(TimeSpan.FromSeconds(10), ProductCreatedOutboxEvent.EventName, new ProductCreatedOutboxEvent(notification.Product), cancellationToken);
    }
}
