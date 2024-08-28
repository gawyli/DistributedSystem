using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Events.Outbox;
public class ProductCreatedOutboxEvent
{
    public const string EventName = "product.index";

    public Product Product { get; }

    public ProductCreatedOutboxEvent(Product product)
    {
        Product = product;
    }


}
