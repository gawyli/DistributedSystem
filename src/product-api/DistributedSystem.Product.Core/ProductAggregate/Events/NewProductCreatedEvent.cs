using DistributedSystem.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Core.ProductAggregate.Events;

public class NewProductCreatedEvent : BaseDomainEvent
{
    public Product Product { get; set; }

    public NewProductCreatedEvent(Product product)
    {
        this.Product = product;
    }
}
