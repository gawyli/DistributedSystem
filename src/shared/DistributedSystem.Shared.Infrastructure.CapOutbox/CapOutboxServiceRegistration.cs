using DistributedSystem.Shared.Core.Abstractions;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Savorboard.CAP.InMemoryMessageQueue;

namespace DistributedSystem.Shared.Infrastructure.CapOutbox;
public static class CapOutboxRegistration
{
    public static IServiceCollection AddInMemoryCapOutbox(this IServiceCollection services)
    {
        return services.AddCapOutbox(options =>
        {
            // Use in memory storage and transport
            options.UseInMemoryStorage();
            options.UseInMemoryMessageQueue();
        });
    }

    public static IServiceCollection AddCapOutbox(this IServiceCollection services, Action<CapOptions> configureOptions)
    {
        services.AddCap(configureOptions);
        services.AddSingleton<IOutbox, CAPOutbox>();

        return services;
    }
}
