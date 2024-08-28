using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities;
using MediatR;

namespace DistributedSystem.Shared.Core.Transaction;
public class TransactionBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : BaseEntity, IAggregateRoot
{
    private readonly IRepository _repository;

    public TransactionBehavior(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<TResponse> Handle(TCommand command, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // send integration event form here
        await using var transaction = await _repository.BeginTransactionAsync<TResponse>(cancellationToken);
        try
        {
            TResponse result = await next();
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}