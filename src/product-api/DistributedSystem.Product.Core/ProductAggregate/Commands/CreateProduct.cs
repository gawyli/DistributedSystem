using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Core.ProductAggregate.Commands;
public static class CreateProduct
{
    public class Command : ICommand<Product>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }  

        public Command(string name, decimal price, int quantity)
        {
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
        }

        public Command() : this(string.Empty, 0M, 0)
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
            var entity = await CreateEntity(new Product(request.Name, request.Price, request.Quantity), cancellationToken);

            return entity;
        }
    }
}
