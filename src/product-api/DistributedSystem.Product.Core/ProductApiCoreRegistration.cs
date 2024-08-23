using Microsoft.Extensions.DependencyInjection;
using DistributedSystem.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Core;
public static class ProductApiCoreRegistration
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSharedCore();

        // MediatR
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ProductApiCoreRegistration)));
    

        // Config

        return services;
    }
}
