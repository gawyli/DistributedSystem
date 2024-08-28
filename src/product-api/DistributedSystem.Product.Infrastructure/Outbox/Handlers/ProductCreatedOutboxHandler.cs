using DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Events.Integration;
using DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Events.Outbox;
using DotNetCore.CAP;
using MassTransit;

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
