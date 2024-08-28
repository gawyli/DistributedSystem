using DistributedSystem.Shared.Common;
using DistributedSystem.Shared.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DistributedSystem.Client.Core;
public static class ClientCoreRegistration
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSharedCore();

        // MediatR
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ClientCoreRegistration)));

        // Config

        return services;
    }
}
