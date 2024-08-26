using DistributedSystem.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.InventoryControl.Core.ProductAggregate.Events;
public class ProductOutOfStockEvent : BaseDomainEvent
{
    public Product Product { get; set; }

    public ProductOutOfStockEvent(Product product)
    {
        this.Product = product;
    }
}
