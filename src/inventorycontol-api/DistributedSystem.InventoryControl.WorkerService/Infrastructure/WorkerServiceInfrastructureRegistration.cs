using DistributedSystem.InventoryControl.WorkerService.Infrastructure.IntegrationEventHandlers;
using DistributedSystem.Shared.Infrastructure;
using MassTransit;

namespace DistributedSystem.InventoryContol.WorkerService.Infrastructure;
public static class WorkerServiceInfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedInfrastructure(configuration);

        services.AddMassTransit(options =>
        {
            options.AddConsumer<ProductOutOfStockIntegrationEventHandler>();

            options.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri(configuration.GetConnectionString("messagebus")!));

                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}
