using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.InventoryControl.Core.ProductAggregate.Events.Outbox;
public class ProductOutOfStockOutboxEvent
{
    public const string EventName = "product.outofstock";

    public Product Product { get; }

    public ProductOutOfStockOutboxEvent(Product product)
    {
        this.Product = product;
    }
}
