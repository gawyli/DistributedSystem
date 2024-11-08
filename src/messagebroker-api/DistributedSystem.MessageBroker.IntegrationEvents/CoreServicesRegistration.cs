﻿using DistributedSystem.MessageBroker.Core.IntegrationEvents.Product;
using DistributedSystem.Shared.Core;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DistributedSystem.MessageBroker.Core;
public static class CoreServicesRegistration
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedCore();

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
