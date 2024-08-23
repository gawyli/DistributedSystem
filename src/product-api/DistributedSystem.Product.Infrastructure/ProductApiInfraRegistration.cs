using DistributedSystem.Product.Infrastructure.Repository;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities.IdFactories;
using DistributedSystem.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Infrastructure;
public static class ProductApiInfraRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedInfrastructure(configuration);

        services.AddDatabase(configuration);


        return services;
    }


    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<ProductSqlDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("productdb"), sqlOptions =>
            {
                sqlOptions.ExecutionStrategy(c => new SqlServerRetryingExecutionStrategy(c));
            });
        });


        services.AddScoped<IEntityIdFactory, GuidEntityIdFactory>();
        services.AddScoped<ProductRepository>();
        services.AddScoped<IRepository>(sp => sp.GetRequiredService<ProductRepository>());
        services.AddScoped<IReadRepository>(sp => sp.GetRequiredService<ProductRepository>());

        return services;
    }

}
