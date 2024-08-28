using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Handlers;
using MediatR;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Commands;
public class UpdateProduct
{
    public class Command : ICommand<Product>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Command(string id, string name, decimal price, int quantity)
        {
            this.Id = id;
            this.Price = price;
            this.Name = name;
            this.Quantity = quantity;
        }

        public Command() : this(string.Empty, string.Empty, 0, 0)
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
            product.AddQuantity(request.Quantity);

            await UpdateEntity(product, cancellationToken);

            return product;
        }
    }
}
