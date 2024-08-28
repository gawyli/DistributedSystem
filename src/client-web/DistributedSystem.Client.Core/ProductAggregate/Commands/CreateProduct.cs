using DistributedSystem.Client.Core.Interfaces;
using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;
using DistributedSystem.Shared.Core.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Client.Core.ProductAggregate.Commands;
public class CreateProduct
{
    public class Command : IQuery<string>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Command(string name, string description, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public Command() : this(string.Empty,string.Empty, 0, 0)
        {
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly IProductService _productService;

            public Handler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _productService.CreateProductAsync(new Product(request.Name, request.Price, request.Quantity), cancellationToken);

                return product.Id;
            }
        }
    }
}
