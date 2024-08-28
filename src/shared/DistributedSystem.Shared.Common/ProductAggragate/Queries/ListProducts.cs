using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Handlers;
using MediatR;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Queries;
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

