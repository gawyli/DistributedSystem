using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributedSystem.Shared.Core;

namespace DistributedSystem.Product.WorkerService.Core;
public static class WorkerServiceCoreRegistration
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSharedCore();

        return services;
    }
}
