using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities;

namespace DistributedSystem.Shared.Core.Handlers;

public class BaseCommandHandler
{
    private readonly IRepository _repository;

    public BaseCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    protected async Task<TEntity> LoadEntity<TEntity>(string entityId, CancellationToken cancellationToken) where TEntity : BaseEntity, IAggregateRoot
    {
        return await _repository.GetByIdAsync<TEntity>(entityId, cancellationToken) ?? throw new ArgumentNullException("Not found entity " + entityId);
    }

    protected async Task<TEntity> CreateEntity<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseEntity, IAggregateRoot
    {
        return await _repository.AddAsync(entity, cancellationToken);
    }

    protected async Task UpdateEntity<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseEntity, IAggregateRoot
    {
        await _repository.UpdateAsync(entity, cancellationToken);
    }



}
