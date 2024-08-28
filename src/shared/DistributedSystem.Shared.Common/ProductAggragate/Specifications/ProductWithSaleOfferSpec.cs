using Ardalis.Specification;
using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;
using DistributedSystem.Shared.Core.Handlers;
using Microsoft.EntityFrameworkCore;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Specifications;
public class ProductWithSaleOfferSpec : BaseEntitySpecification<Product>
{
    public string ProductId { get; }

    public ProductWithSaleOfferSpec(string productId)
    {
        this.ProductId = productId;
    }

    protected override IQueryable<Product> Query(IQueryable<Product> products)
    {
        return products.Where(product => product.Id == this.ProductId)
                        .Include(product => product.SaleOffer);
    }
}
