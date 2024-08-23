using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Core.ProductAggregate.Commands;
public class UpdateProduct
{
    public class Command : ICommand<Product>
    {
        public string Id { get; set; }
        public decimal Price { get; set; }

        public Command(string id, decimal price)
        {
            this.Id = id;
            this.Price = price;
        }

        public Command() : this(string.Empty, 0)
        {
        }
    }

    public class Handler : BaseCommandHandler, IRequestHandler<Command, Product>
    {
        public Handler(IRepository repository) : base(repository)
        {

        }

        public async Task<Product> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await LoadEntity<Product>(request.Id, cancellationToken);

            product.SetPrice(request.Price);

            await UpdateEntity(product, cancellationToken);

            return product;
        }
    }
}
