using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Core.ProductAggregate.Commands;
public class CreateSaleOffer
{
    public class Command : ICommand<Product>
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Command(string productId, string name, int discount, DateTime startDate, DateTime endDate)
        {
            this.ProductId = productId;
            this.Name = name;
            this.Discount = discount;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public Command() : this(string.Empty, string.Empty, 0, DateTime.MinValue, DateTime.MinValue)
        {
        }
    }

    public class Handler : BaseCommandHandler, IRequestHandler<Command, Product>
    {
        private readonly IEntityIdFactory _idFactory;

        public Handler(IEntityIdFactory idFactory, IRepository repository) : base(repository)
        {
            _idFactory = idFactory;
        }

        public async Task<Product> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await LoadEntity<Product>(request.ProductId, cancellationToken);

            product.SetSaleOffer(new SaleOffer(_idFactory.NewId(), request.Name, request.Discount, request.StartDate, request.EndDate));

            await UpdateEntity(product, cancellationToken);

            return product;
        }
    }
}
