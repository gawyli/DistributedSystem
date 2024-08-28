using DistributedSystem.Client.Web;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseWeb(builder.Configuration);

app.Run();
