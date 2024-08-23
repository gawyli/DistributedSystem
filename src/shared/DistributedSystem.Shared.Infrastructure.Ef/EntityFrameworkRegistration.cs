using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Infrastructure.Ef;
public static class EntityFrameworkRegistration
{
    public static IServiceCollection AddEntityFrameworkRepository(this IServiceCollection services)
    {

        return services;
    }

}
