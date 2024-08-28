
using DistributedSystem.Client.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace DistributedSystem.Client.Infrastructure.Product;
public class ProductService : IProductService
{
    public const string ServiceName = "ProductApi";

    private readonly ILogger<ProductService> _logger;
    private readonly HttpClient _client;

    public ProductService(ILogger<ProductService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _client = httpClient;
    }

    public async Task<List<Shared.Common.Aggregates.ProductAggragate.Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync("products", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to get products");

        }

        var products = await response.Content.ReadFromJsonAsync<List<Shared.Common.Aggregates.ProductAggragate.Product>>();
        if (products == null)
        {
            throw new Exception("No products found");
        }

        return products!;
    }

    public async Task UpdateProduct(Shared.Common.Aggregates.ProductAggragate.Product product)
    {
        var response = await _client.PutAsJsonAsync($"products/{product.Id}", product);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to update product");
        }
    }
}
