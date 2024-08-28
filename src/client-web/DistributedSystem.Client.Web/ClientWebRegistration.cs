using DistributedSystem.Client.Core;
using DistributedSystem.Client.Infrastructure;
using DistributedSystem.Shared.Web;

namespace DistributedSystem.Client.Web;

public static class ClientWebRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddSharedWeb(configuration);
        services.AddCore();
        services.AddInfrastructure(configuration);

        return services;
    }

    public static WebApplication UseWeb(this WebApplication app, IConfiguration configuration)
    {
        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        //app.MapControllerRoute(
        //    name: "default",
        //    pattern: "{controller=Home}/{action=Index}/{id?}");

        return app;
    }
}
