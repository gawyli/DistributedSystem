using DistributedSystem.Product.WorkerService.Core;
using DistributedSystem.Product.WorkerService.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
