using DistributedSystem.MessageBroker.Infrastructure.Repository;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities.IdFactories;
using DistributedSystem.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.MessageBroker.Infrastructure;
public static class MessageBrokerInfraServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedInfrastructure(configuration);



        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MessageBrokerSqlDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("messagebrokerdb"), sqlOptions =>
            {
                // Can't run this _and_ have my own transactions
                 sqlOptions.ExecutionStrategy(c => new NpgsqlRetryingExecutionStrategy(c));
            });
        });

        services.AddScoped<IEntityIdFactory, GuidEntityIdFactory>();
        services.AddScoped<MessageRepository>();
        services.AddScoped<IRepository>(sp => sp.GetRequiredService<MessageRepository>());
        services.AddScoped<IReadRepository>(sp => sp.GetRequiredService<MessageRepository>());


        return services;
    }
}
