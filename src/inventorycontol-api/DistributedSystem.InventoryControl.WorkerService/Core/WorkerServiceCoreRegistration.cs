using DistributedSystem.Shared.Core;

namespace DistributedSystem.InventoryContol.WorkerService.Core;
public static class WorkerServiceCoreRegistration
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSharedCore();

        return services;
    }
}
