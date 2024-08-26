

using DistributedSystem.InventoryContol.WorkerService.Core;
using DistributedSystem.InventoryContol.WorkerService.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
