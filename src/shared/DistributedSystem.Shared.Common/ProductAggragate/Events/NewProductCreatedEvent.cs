using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;
using DistributedSystem.Shared.Core.Entities;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Events;

public class NewProductCreatedEvent : BaseDomainEvent
{
    public Product Product { get; private set; }

    public NewProductCreatedEvent(Product product)
    {
        this.Product = product;
    }
}
