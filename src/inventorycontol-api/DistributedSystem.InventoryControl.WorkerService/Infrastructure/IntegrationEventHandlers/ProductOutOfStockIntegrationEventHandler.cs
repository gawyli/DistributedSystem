using DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Events.Integration;
using MassTransit;

namespace DistributedSystem.InventoryControl.WorkerService.Infrastructure.IntegrationEventHandlers;

public class ProductOutOfStockEventHandler : IConsumer<ProductOutOfStockIntegrationEvent>
{
    private readonly ILogger<ProductOutOfStockEventHandler> _logger;

    public ProductOutOfStockEventHandler(ILogger<ProductOutOfStockEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ProductOutOfStockIntegrationEvent> context)
    {
        // Add some actions
        _logger.LogInformation($"Handling message type {context.Message.GetType().Name} with id {context.Message.Id}");


        // send email to store manager


        return Task.CompletedTask;
    }
}
