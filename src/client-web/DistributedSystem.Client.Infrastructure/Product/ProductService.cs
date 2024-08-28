
using DistributedSystem.Client.Core.Interfaces;
using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;
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
            throw new Exception("Failed to deserialize response");
        }

        return products!;
    }

    public async Task<Shared.Common.Aggregates.ProductAggragate.Product> CreateProductAsync(Shared.Common.Aggregates.ProductAggragate.Product product, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "products/create")
        {
            Content = JsonContent.Create(product)
        };

        var response = await _client.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to create product");
        }

        var productCreated = await response.Content.ReadFromJsonAsync<Shared.Common.Aggregates.ProductAggragate.Product>(cancellationToken);
        if (productCreated == null)
        {
            throw new Exception("Failed to deserialize response");
        }

        return productCreated;
    }

    public async Task UpdateProduct(Shared.Common.Aggregates.ProductAggragate.Product product, CancellationToken cancellationToken)
    {
        var response = await _client.PutAsJsonAsync($"products/{product.Id}", product);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to update product");
        }
    }
}
