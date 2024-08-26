using DistributedSystem.InventoryControl.Core.ProductAggregate.Events;
using DistributedSystem.InventoryControl.Core.ProductAggregate.Events.Outbox;
using DistributedSystem.Shared.Core.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.InventoryControl.Core.ProductAggregate.Handlers;
public class ProductOutOfStockHandler : INotificationHandler<ProductOutOfStockEvent>
{
    private readonly IOutbox _outboxService;

    public ProductOutOfStockHandler(IOutbox outboxService)
    {
        _outboxService = outboxService;
    }

    public Task Handle(ProductOutOfStockEvent notification, CancellationToken cancellationToken)
    {
        var outboxEvent = new ProductOutOfStockOutboxEvent(notification.Product);
        return _outboxService.PublishDelayAsync(TimeSpan.FromSeconds(10), ProductOutOfStockOutboxEvent.EventName, outboxEvent, cancellationToken);
    }
}

