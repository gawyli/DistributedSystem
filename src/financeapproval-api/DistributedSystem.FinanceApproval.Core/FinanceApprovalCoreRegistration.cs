using DistributedSystem.Shared.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.FinanceApproval.Core;
public static class FinanceApprovalCoreRegistration
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddSharedCore(configuration);

        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(FinanceApprovalCoreRegistration)));

        return services;
    }
}
