using DistributedSystem.Shared.Api;
using DistributedSystem.Shared.Api.Swagger;
using Microsoft.OpenApi.Models;

namespace DistributedSystem.Product.Api;

public static class ProductApiRegistration
{
    public static IServiceCollection AddProductApi(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddSharedApi(configuration);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });
            c.CustomSchemaIds(type => type.FormatSwaggerSchemaId());
            c.EnableAnnotations();
        });

        

        return services;
    }

    public static WebApplication UseApi(this WebApplication app, IConfigurationRoot configuration)
    {

        app.UseHttpsRedirection();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
            });
        }

        app.MapDefaultControllerRoute();
        app.MapDefaultEndpoints();

        return app;
    }

}
