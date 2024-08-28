using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;
using DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Specifications;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Handlers;
using MediatR;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Queries;
public class GetProductDetails
{
    public class Query : IQuery<Product>
    {
        public string Id { get; set; }
        public Query(string id)
        {
            this.Id = id;
        }

        public Query() : this(string.Empty)
        {
        }
    }

    public class Handler : BaseQueryHandler, IRequestHandler<Query, Product>
    {
        public Handler(IReadRepository repository) : base(repository)
        {

        }

        public async Task<Product> Handle(Query request, CancellationToken cancellationToken)
        {
            var product = await LoadEntityBySpec(new ProductWithSaleOfferSpec(request.Id), cancellationToken);

            return product!;
        }
    }
}
