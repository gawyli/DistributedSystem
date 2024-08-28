using DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Events.Integration;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DistributedSystem.MessageBroker.Core.IntegrationEvents.Product;

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
