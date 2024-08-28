using DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Events.Integration;
using DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Events.Outbox;
using DotNetCore.CAP;
using MassTransit;

namespace DistributedSystem.InventoryControl.Infrastructure.Outbox.Handlers;
public class ProductOutOfStockOutboxHandler : ICapSubscribe
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductOutOfStockOutboxHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [CapSubscribe(ProductOutOfStockOutboxEvent.EventName)]
    public async Task Receive(ProductOutOfStockOutboxEvent request, CancellationToken cancellationToken)
    {
        if (request.Product.StockLevel == Shared.Core.Entities.Enums.StockLevel.OutOfStock)
        {
            await _publishEndpoint.Publish(new ProductOutOfStockIntegrationEvent(request.Product.Id, request.Product.StockLevel), cancellationToken);
        }

    }
}
