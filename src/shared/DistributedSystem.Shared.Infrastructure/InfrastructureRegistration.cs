﻿using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Infrastructure.Clock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DistributedSystem.Shared.Infrastructure;
public static class InfrastructureRegistration
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IClock, UtcClock>();

        return services;
    }
}
