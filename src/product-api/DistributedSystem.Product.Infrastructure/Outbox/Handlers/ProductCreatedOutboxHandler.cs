using DistributedSystem.Product.Core.ProductAggregate.Events.Outbox;
using DistributedSystem.Shared.Common.IntegrationEvents.Product;
using DotNetCore.CAP;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Core.ProductAggregate.Handlers.Outbox;
public class ProductCreatedOutboxHandler : ICapSubscribe
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductCreatedOutboxHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [CapSubscribe(ProductCreatedOutboxEvent.EventName)]
    public async Task Receive(ProductCreatedOutboxEvent request, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(new ProductCreatedIntegrationEvent(request.Product.Id), cancellationToken);
    }
}
