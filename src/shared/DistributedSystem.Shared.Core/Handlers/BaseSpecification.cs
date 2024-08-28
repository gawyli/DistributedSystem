using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities;

namespace DistributedSystem.Shared.Core.Handlers;
public abstract class BaseSpecification<TResult>
{
    private IDbContextAccessor? _dBContextAccessor;
    public IQueryable<TResult> Execute(IDbContextAccessor dbContextAccessor)
    {
        _dBContextAccessor = dbContextAccessor;
        return Query();
    }

    protected abstract IQueryable<TResult> Query();

    protected IQueryable<T> GetQueryable<T>() where T : BaseEntity
    {
        return _dBContextAccessor!.GetDbContext<T>().Set<T>();
    }
}

public abstract class BaseEntitySpecification<TEntity> : BaseSpecification<TEntity>
    where TEntity : BaseEntity
{
    protected abstract IQueryable<TEntity> Query(IQueryable<TEntity> queryable);

    protected override IQueryable<TEntity> Query()
    {
        return Query(GetQueryable<TEntity>());
    }
}

