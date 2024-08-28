using Microsoft.Extensions.Configuration;

namespace DistributedSystem.Client.Infrastructure.Product;
public class ProductServiceConfig
{
    public const string SectionName = "Services";

    public string Name { get; set; } = null!;
    public string BaseUrl { get; set; } = null!;

    public ProductServiceConfig()
    {
    }

    public static ProductServiceConfig New(IConfiguration configuration)
    {
        var productServiceConfig = new ProductServiceConfig();
        configuration.GetSection(SectionName).Bind(productServiceConfig);

        return productServiceConfig;
    }
}
