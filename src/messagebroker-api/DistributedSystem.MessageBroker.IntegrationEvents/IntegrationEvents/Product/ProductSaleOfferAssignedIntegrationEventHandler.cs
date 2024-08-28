using DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Events.Integration;
using MassTransit;

namespace DistributedSystem.MessageBroker.Core.IntegrationEvents.Product;
public class ProductSaleOfferAssignedIntegrationEventHandler : IConsumer<ProductSaleOfferAssignedIntegrationEvent>
{
    public Task Consume(ConsumeContext<ProductSaleOfferAssignedIntegrationEvent> context)
    {
        throw new NotImplementedException();
    }
}

