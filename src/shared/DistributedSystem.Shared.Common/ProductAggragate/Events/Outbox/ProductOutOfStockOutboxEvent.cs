namespace DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Events.Outbox;
public class ProductOutOfStockOutboxEvent
{
    public const string EventName = "product.outofstock";

    public Product Product { get; }

    public ProductOutOfStockOutboxEvent(Product product)
    {
        Product = product;
    }
}


