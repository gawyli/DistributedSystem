using DistributedSystem.Product.Core.ProductAggregate.Handlers.Outbox;
using DistributedSystem.Product.Infrastructure.Repository;
using DistributedSystem.Shared.Common;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities.IdFactories;
using DistributedSystem.Shared.Infrastructure;
using DistributedSystem.Shared.Infrastructure.CapOutbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Savorboard.CAP.InMemoryMessageQueue;

namespace DistributedSystem.Product.Infrastructure;
public static class ProductApiInfraRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedInfrastructure(configuration);

        services.AddMassTransit(options =>
        {
            options.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri(configuration.GetConnectionString("messagebus")!));
            });
        });

        services.AddDatabase(configuration);

        services.AddSharedCommon();

        return services;
    }


    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<ProductSqlDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("productdb"), sqlOptions =>
            {
                //sqlOptions.ExecutionStrategy(c => new SqlServerRetryingExecutionStrategy(c));
            });
        });


        services.AddScoped<IEntityIdFactory, GuidEntityIdFactory>();
        services.AddScoped<ProductRepository>();
        services.AddScoped<IRepository>(sp => sp.GetRequiredService<ProductRepository>());
        services.AddScoped<IReadRepository>(sp => sp.GetRequiredService<ProductRepository>());

        services.AddCapOutbox(options =>
        {
            options.UseInMemoryStorage();
            options.UseInMemoryMessageQueue();
        });
        services.AddScoped<ProductCreatedOutboxHandler>();



        return services;
    }


}
