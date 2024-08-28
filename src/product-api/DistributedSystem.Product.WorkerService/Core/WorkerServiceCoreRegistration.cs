using DistributedSystem.Shared.Core;

namespace DistributedSystem.Product.WorkerService.Core;
public static class WorkerServiceCoreRegistration
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSharedCore();

        return services;
    }
}
