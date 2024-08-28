using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DistributedSystem.Shared.Api;
public static class SharedApiRegistration
{
    public static IServiceCollection AddSharedApi(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();




        return services;
    }

}
