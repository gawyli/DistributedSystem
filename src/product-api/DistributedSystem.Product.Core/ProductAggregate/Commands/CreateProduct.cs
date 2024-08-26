using DistributedSystem.Product.Core.ProductAggregate.Events.Outbox;
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
        private readonly IRepository _repository;
        private readonly IOutbox _outbox;

        public Handler(IRepository repository, IOutbox outbox) : base(repository)
        {
            _repository = repository;
            _outbox = outbox;
        }

        public async Task<Product> Handle(Command request, CancellationToken cancellationToken)
        {
            var transaction = await _repository.BeginTransactionAsync<Product>(cancellationToken);
            try
            {
                var entity = await CreateEntity(new Product(request.Name, request.Price, request.Quantity), cancellationToken);

                await _outbox.PublishDelayAsync(TimeSpan.FromSeconds(10), ProductCreatedOutboxEvent.EventName, new ProductCreatedOutboxEvent(entity), cancellationToken: cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return entity;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
