using DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Events;
using DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Events.Outbox;
using DistributedSystem.Shared.Core.Abstractions;
using MediatR;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Handlers;
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
