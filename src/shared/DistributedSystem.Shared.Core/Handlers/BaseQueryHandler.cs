using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities;

namespace DistributedSystem.Shared.Core.Handlers;

public abstract class BaseQueryHandler
{
    private readonly IReadRepository _repository;

    protected BaseQueryHandler(IReadRepository repository)
    {
        _repository = repository;
    }

    protected async Task<TEntity> LoadEntity<TEntity>(string entityId, CancellationToken cancellationToken) where TEntity : BaseEntity, IAggregateRoot
    {
        var entity = await _repository.GetByIdAsync<TEntity>(entityId, cancellationToken) ?? throw new ArgumentNullException(entityId);
        return entity;
    }

    protected async Task<List<TEntity>> LoadAllEntities<TEntity>(CancellationToken cancellationToken) where TEntity : BaseEntity, IAggregateRoot
    {
        return await _repository.ListAsync<TEntity>(cancellationToken);
    }

    protected async Task<T?> LoadEntityBySpec<T>(BaseSpecification<T> specification, CancellationToken cancellationToken)
    {
        return await _repository.GetBySpecAsync(specification, cancellationToken);
    }
}
