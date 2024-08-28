using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;

namespace DistributedSystem.Client.Core.Interfaces;
public interface IProductService
{
    const string ConfigName = "ProductService";

    Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken);

}
