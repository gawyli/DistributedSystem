using DistributedSystem.Shared.Common.IntegrationEvents.Product;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.MessageBroker.Core.IntegrationEvents.Product;
public class ProductSaleOfferAssignedIntegrationEventHandler : IConsumer<ProductSaleOfferAssignedIntegrationEvent>
{
    public Task Consume(ConsumeContext<ProductSaleOfferAssignedIntegrationEvent> context)
    {
        throw new NotImplementedException();
    }
}

