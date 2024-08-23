using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Core;
public static class SharedCoreRegistration
{
    public static IServiceCollection AddSharedCore(this IServiceCollection services)
    {
        // TODO: PipelineBehaviours
        

        return services;
    }
}
