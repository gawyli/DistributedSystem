using DistributedSystem.Shared.Core.Entities;
using DistributedSystem.Shared.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Common.IntegrationEvents.Product;
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
