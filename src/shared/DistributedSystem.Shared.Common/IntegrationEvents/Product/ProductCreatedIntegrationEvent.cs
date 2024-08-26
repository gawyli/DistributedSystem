using DistributedSystem.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Common.IntegrationEvents.Product;
public class ProductCreatedIntegrationEvent : BaseIntegrationEvent
{
    public string ProductId { get; set; }

    public ProductCreatedIntegrationEvent(string productId)
    {
        ProductId = productId;
    }
}
