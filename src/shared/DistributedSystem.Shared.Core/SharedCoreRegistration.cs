using Microsoft.Extensions.DependencyInjection;

namespace DistributedSystem.Shared.Core;
public static class SharedCoreRegistration
{
    public static IServiceCollection AddSharedCore(this IServiceCollection services)
    {
        // TODO: PipelineBehaviours


        return services;
    }
}
