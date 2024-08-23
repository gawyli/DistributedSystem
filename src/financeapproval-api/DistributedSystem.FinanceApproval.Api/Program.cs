using DistributedSystem.FinanceApproval.Api;
using DistributedSystem.FinanceApproval.Core;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddApi(builder.Configuration)
    .AddCore(builder.Configuration);


var app = builder.Build();

app.UseApi();

app.Run();
