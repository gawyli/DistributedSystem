using DistributedSystem.Shared.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DistributedSystem.Product.Core;
public static class ProductApiCoreRegistration
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSharedCore();

        // MediatR
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ProductApiCoreRegistration)));

        // Config

        return services;
    }
}
