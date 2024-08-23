using Ardalis.Specification;
using DistributedSystem.Shared.Core.Handlers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Core.ProductAggregate.Specifications;
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
