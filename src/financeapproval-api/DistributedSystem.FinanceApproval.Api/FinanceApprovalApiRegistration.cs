using Microsoft.OpenApi.Models;
using DistributedSystem.Shared.Api;
namespace DistributedSystem.FinanceApproval.Api;

public static class FinanceApprovalApiRegistration
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddSharedApi(configuration);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DistributedSystem.FinanceApproval.Api", Version = "v1" });
        });

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DistributedSystem.FinanceApproval.Api v1"));
        }

        app.UseHttpsRedirection();

        //app.UseAuthentication();
        //app.UseAuthorization();

        app.MapDefaultEndpoints();
        app.MapDefaultControllerRoute();

        return app;
    }
}
