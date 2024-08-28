using DistributedSystem.Shared.Core.Entities;
using DistributedSystem.Shared.Core.Entities.Enums;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Events.Integration;
public class ProductOutOfStockIntegrationEvent : BaseIntegrationEvent
{
    public string ProductId { get; set; }
    public StockLevel StockLevel { get; }

    public ProductOutOfStockIntegrationEvent(string productId, StockLevel stockLevel)
    {
        ProductId = productId;
        StockLevel = stockLevel;
    }
}
