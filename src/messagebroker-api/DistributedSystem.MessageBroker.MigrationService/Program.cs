using DistributedSystem.MessageBroker.Infrastructure.Repository;
using DistributedSystem.MessageBroker.MigrationService;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Infrastructure.Clock;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddDbContextPool<MessageBrokerSqlDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("messagebrokerdb"), sqlOptions =>
    {
        sqlOptions.ExecutionStrategy(c => new NpgsqlRetryingExecutionStrategy(c));
        sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
    });
});

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(Program)));
builder.Services.AddSingleton<IClock, UtcClock>();

builder.EnrichNpgsqlDbContext<MessageBrokerSqlDbContext>(settings =>
{
    settings.DisableRetry = true;
});

var host = builder.Build();
host.Run();
