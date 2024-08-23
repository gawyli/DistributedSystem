using DistributedSystem.Product.Infrastructure.Repository;
using DistributedSystem.Product.MigrationService;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Infrastructure.Clock;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddDbContextPool<ProductSqlDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("productdb"), sqlOptions =>
    {
        sqlOptions.ExecutionStrategy(c => new SqlServerRetryingExecutionStrategy(c));
        sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
    });
});

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(Program)));
builder.Services.AddSingleton<IClock, UtcClock>();

builder.EnrichSqlServerDbContext<ProductSqlDbContext>(settings =>
{
    settings.DisableRetry = true;
});

var host = builder.Build();
host.Run();
