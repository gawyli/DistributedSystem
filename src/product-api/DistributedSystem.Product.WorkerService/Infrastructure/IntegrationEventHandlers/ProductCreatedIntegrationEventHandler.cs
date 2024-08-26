using DistributedSystem.Shared.Common.IntegrationEvents.Product;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.WorkerService.Infrastructure.IntegrationEventHandlers;

public class ProductCreatedIntegrationEventHandler : IConsumer<ProductCreatedIntegrationEvent>
{
    private readonly ILogger<ProductCreatedIntegrationEventHandler> _logger;

    public ProductCreatedIntegrationEventHandler(ILogger<ProductCreatedIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ProductCreatedIntegrationEvent> context)
    {
        // Add some actions
        _logger.LogInformation($"Handling message type {context.Message.GetType().Name} with id {context.Message.Id}");

        return Task.CompletedTask;
    }
}
