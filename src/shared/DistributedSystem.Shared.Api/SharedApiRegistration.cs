using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Api;
public static class SharedApiRegistration
{
    public static IServiceCollection AddSharedApi(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();




        return services;
    }

}
