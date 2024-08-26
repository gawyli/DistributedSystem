using DistributedSystem.Product.Infrastructure.Repository;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities.IdFactories;
using DistributedSystem.Shared.Infrastructure;
using DistributedSystem.Shared.Infrastructure.CapOutbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DotNetCore.CAP;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Savorboard.CAP.InMemoryMessageQueue;
using DistributedSystem.Product.Core.ProductAggregate.Handlers.Outbox;
using DistributedSystem.Product.Infrastructure.MessageBroker;

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

        //var messageBrokerConfig = MessageBrokerConfig.New(configuration);

        services.AddCapOutbox(options =>
        {
            options.UsePostgreSql(connectionString: configuration.GetConnectionString("messagebrokerdb")!);
            options.UseInMemoryMessageQueue();
        });
        services.AddScoped<ProductCreatedOutboxHandler>();


        var cfg = configuration.GetConnectionString("messagebrokerdb")!;
        return services;
    }

    
}
