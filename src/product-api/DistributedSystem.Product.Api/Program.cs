using DistributedSystem.Product.Api;
using DistributedSystem.Product.Core;
using DistributedSystem.Product.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProductApi(builder.Configuration)
                .AddCore()
                .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseApi(builder.Configuration);


app.Run();
