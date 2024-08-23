using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Core.ProductAggregate.Queries;
public class ListProducts
{ 
    public class Query : IQuery<List<Product>>
    {
        public Query()
        {
            
        }
    }

    public class Handler : BaseQueryHandler, IRequestHandler<Query, List<Product>>
    {
        public Handler(IRepository repository) : base(repository)
        {
            
        }

        public async Task<List<Product>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await LoadAllEntities<Product>(cancellationToken);

            return products;
        }
    }

}

