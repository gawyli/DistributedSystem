using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DistributedSystem.Shared.Web;
public static class SharedWebRegistration
{
    public static IServiceCollection AddSharedWeb(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
