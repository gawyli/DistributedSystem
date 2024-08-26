using DistributedSystem.InventoryControl.Core.ProductAggregate.Events.Outbox;
using DistributedSystem.Shared.Common.IntegrationEvents.Product;
using DotNetCore.CAP;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
