using DistributedSystem.Shared.Core.Entities;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Events.Integration;
public class ProductCreatedIntegrationEvent : BaseIntegrationEvent
{
    public string ProductId { get; set; }

    public ProductCreatedIntegrationEvent(string productId)
    {
        ProductId = productId;
    }
}
