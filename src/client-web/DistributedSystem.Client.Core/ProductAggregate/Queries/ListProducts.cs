using DistributedSystem.Client.Core.Interfaces;
using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Client.Core.ProductAggregate.Queries;
public class ListProducts
{
    public class Query : IQuery<List<Product>>
    {
    }

    public class Handler : IRequestHandler<Query, List<Product>>
    {
        private readonly IProductService _productService;

        public Handler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<Product>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsAsync(cancellationToken);

            return products;
        }
    }


}
