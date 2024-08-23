using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Handlers;
using FluentValidation;
using MediatR;

namespace DistributedSystem.FinanceApproval.Api.FinanceApplicationAggregate.Commands;

public class CreateFinanceApplication
{
    public class Command : ICommand<string>
    {
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public decimal Amount { get; set; }
        public Command(string customerId, string productId, decimal amount)
        {
            CustomerId = customerId;
            ProductId = productId;
            Amount = amount;
        }

        public Command() : this(string.Empty, string.Empty, decimal.Zero)
        {
        }
    }

    public class InputValidator : AbstractValidator<Command>
    {
       public InputValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }

    //public class Authorizer : BaseCommandAuthorizer<Command>
    //{
    //    protected override bool UserHasCorrectRole(IAppUser user)
    //        => user.HasRole("ChatUser");
    //}


    public class Handler : BaseCommandHandler, IRequestHandler<Command, string>
    {
        public Handler()
        {
            
        }
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await CreateEntity(new FinanceApplication(request.CustomerId, request.ProductId, FinanceApplicationStatus.Pending, request.Amount), cancellationToken);
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error message:", ex.Message);
            }
        }
    }
}
