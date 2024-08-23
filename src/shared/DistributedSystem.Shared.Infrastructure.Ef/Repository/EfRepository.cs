using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Core.Entities;
using DistributedSystem.Shared.Core.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Infrastructure.Ef.Repository;
public abstract class EfRepository : IRepository, IDbContextAccessor
{
    // Based on appsetting.json both "Context" must be nullable as Database might be "None"
    private readonly IEntityIdFactory _entityIdFactory;
    private readonly SqlDbContext _sqlDbContext;

    protected EfRepository(IEntityIdFactory entityIdFactory, SqlDbContext sqlDbContext)
    {
        _entityIdFactory = entityIdFactory;
        _sqlDbContext = sqlDbContext;
    }

    public async Task<T?> GetByIdAsync<T>(string id, CancellationToken cancellationToken) where T : BaseEntity
    {
        return await _sqlDbContext.Set<T>().FindAsync(new[] { id }, cancellationToken);
    }

    public async Task<T?> GetByIdAsync<T>(Type entityType, string id, CancellationToken cancellationToken)
        where T : BaseEntity
    {
        // See bottom comment on use case of this method
        return await _sqlDbContext.GetDbSetForType(entityType).FindAsync(new[] { id }, cancellationToken);
    }

    public async Task<T?> GetBySpecAsync<T>(BaseSpecification<T> specification, CancellationToken cancellationToken)
    {
        return await specification.Execute(this).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<T>> ListAsync<T>(CancellationToken cancellationToken) where T : BaseEntity
    {
        return await _sqlDbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<List<T>> ListAsync<T>(Type entityType, CancellationToken cancellationToken) where T : BaseEntity
    {
        // See bottom comment on use case of this method
        dynamic? dynamicSet = await EntityFrameworkQueryableExtensions
            .ToListAsync(_sqlDbContext.GetDbSetForType(entityType), cancellationToken);
        return ((IEnumerable)dynamicSet).Cast<T>().ToList();
    }

    public async Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken) where T : BaseEntity, IAggregateRoot
    {
        if (string.IsNullOrEmpty(entity.Id))
        {
            entity.Id = _entityIdFactory.NewId();
        }
        else
        {
            throw new InvalidOperationException(
                $"Invalid attempt made to create an entity of type {entity.GetType().Name} which already has an id {entity.Id}");
        }

        // See bottom comment on why differing GetDbContext is used
        await _sqlDbContext.GetDbSetForEntity(entity).AddAsync((dynamic)entity, cancellationToken);
        await _sqlDbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task UpdateAsync<T>(T entity, CancellationToken cancellationToken) where T : BaseEntity, IAggregateRoot
    {
        // See bottom comment on why differing GetDbContext is used
        _sqlDbContext.Entry(entity).State = EntityState.Modified;
        await _sqlDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync<T>(T entity, CancellationToken cancellationToken) where T : BaseEntity, IAggregateRoot
    {
        // See bottom comment on why differing GetDbContext is used
        _sqlDbContext.GetDbSetForEntity(entity).Remove((dynamic)entity);
        await _sqlDbContext.SaveChangesAsync(cancellationToken);
    }

    
    public Task<int> CountAsync<T>(CancellationToken cancellationToken) where T : BaseEntity
    {
        return _sqlDbContext.Set<T>().CountAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<int> CountAsync<T>(Type entityType, CancellationToken cancellationToken) where T : BaseEntity
    {
        // See bottom comment on use case of this method
        return EntityFrameworkQueryableExtensions.CountAsync(_sqlDbContext.GetDbSetForType(entityType),
            cancellationToken);
    }

    public void Detach<T>(T entity) where T : BaseEntity, IAggregateRoot
    {
        _sqlDbContext.Entry(entity).State = EntityState.Detached;
    }

    public DbContext GetDbContext<T>() where T : BaseEntity
    {
        return GetDbContext(typeof(T));
    }

    private DbContext GetDbContext<T>(T entityType)
        where T : BaseEntity, IAggregateRoot
    {
        return GetDbContext(entityType.GetType());
    }

    private DbContext GetDbContext(Type entityType)
    {
        if (_sqlDbContext != null)
        {
            return _sqlDbContext;
        }

        throw new ArgumentNullException(nameof(entityType), "Entity type is required");
    }



    //public async Task<List<T>> ListAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken)
    //    where T : BaseEntity
    //{
    //    IQueryable<T> specificationResult = ApplySpecification(specification);
    //    return await specificationResult.ToListAsync(cancellationToken);
    //}

    //public async Task<ITransaction> BeginTransactionAsync<T>(CancellationToken cancellationToken)
    //{
    //    return new DatabaseTransaction(await _sqlDbContext.Database.BeginTransactionAsync(cancellationToken));
    //}

    /// <inheritdoc />
    //public Task<int> CountAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken)
    //    where T : BaseEntity
    //{
    //    return ApplySpecification(specification, true).CountAsync(cancellationToken);
    //}

    /// <inheritdoc />

    private IQueryable<T> ApplySpecification<T>(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
        where T : BaseEntity
    {
        return SpecificationEvaluator.Default.GetQuery(_sqlDbContext.Set<T>().AsQueryable(), specification,
            evaluateCriteriaOnly);
    }

    private IQueryable<TResult> ApplySpecification<T, TResult>(ISpecification<T, TResult> specification)
        where T : BaseEntity
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification), "Specification is required");
        }

        if (specification.Selector is null)
        {
            throw new SelectorNotFoundException();
        }

        return SpecificationEvaluator.Default.GetQuery(_sqlDbContext.Set<T>().AsQueryable(), specification);
    }

    
}
