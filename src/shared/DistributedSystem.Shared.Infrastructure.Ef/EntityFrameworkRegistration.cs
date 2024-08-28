using Microsoft.Extensions.DependencyInjection;

namespace DistributedSystem.Shared.Infrastructure.Ef;
public static class EntityFrameworkRegistration
{
    public static IServiceCollection AddEntityFrameworkRepository(this IServiceCollection services)
    {
        //services.AddScoped<IEntityIdFactory, GuidEntityIdFactory>();
        //services.AddScoped<EfRepository>();
        //services.AddScoped<IRepository>(sp => sp.GetRequiredService<EfRepository>());
        //services.AddScoped<IReadRepository>(sp => sp.GetRequiredService<EfRepository>());

        return services;
    }

}
