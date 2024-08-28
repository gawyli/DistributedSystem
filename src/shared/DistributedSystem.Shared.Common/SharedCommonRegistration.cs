
using Microsoft.Extensions.DependencyInjection;

namespace DistributedSystem.Shared.Common;
public static class SharedCommonRegistration
{
    public static IServiceCollection AddSharedCommon(this IServiceCollection services)
    {
        // MediatR
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(SharedCommonRegistration)));

        return services;
    }

}
