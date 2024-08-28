using DistributedSystem.Shared.Core.Entities;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Events;
public class ProductOutOfStockEvent : BaseDomainEvent
{
    public Product Product { get; private set; }

    public ProductOutOfStockEvent(Product product)
    {
        Product = product;
    }
}
