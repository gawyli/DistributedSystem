using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributedSystem.Product.WorkerService.Infrastructure.IntegrationEventHandlers;
using DistributedSystem.Shared.Infrastructure;
using MassTransit;

namespace DistributedSystem.Product.WorkerService.Infrastructure;
public static class WorkerServiceInfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedInfrastructure(configuration);

        services.AddMassTransit(options =>
        {
            options.AddConsumer<ProductCreatedIntegrationEventHandler>();

            options.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri(configuration.GetConnectionString("messagebus")!));

                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}
