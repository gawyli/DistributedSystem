using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Core.ProductAggregate.Events.Outbox;
public class ProductCreatedOutboxEvent
{
    public const string EventName = "product.index";

    public Product Product { get; }

    public ProductCreatedOutboxEvent(Product product)
    {
        Product = product;
    }

    
}
