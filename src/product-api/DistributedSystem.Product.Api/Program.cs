using DistributedSystem.Product.Api;
using DistributedSystem.Product.Core;
using DistributedSystem.Product.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddCore()
                .AddInfrastructure(builder.Configuration)
                .AddProductApi(builder.Configuration);

var app = builder.Build();

app.UseApi(builder.Configuration);


app.Run();
