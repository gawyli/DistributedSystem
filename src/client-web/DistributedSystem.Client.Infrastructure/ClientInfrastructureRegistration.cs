using DistributedSystem.Client.Core.Interfaces;
using DistributedSystem.Client.Infrastructure.Product;
using DistributedSystem.Shared.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DistributedSystem.Client.Infrastructure;
public static class ClientInfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedInfrastructure(configuration);

        services.AddScoped<IProductService, ProductService>();
        services.AddHttpClient<IProductService, ProductService>(cfg =>
        {
            cfg.BaseAddress = new Uri("https://localhost:7029"); // This should be discovered by aspire
            cfg.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        return services;
    }

}
