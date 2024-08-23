using Ardalis.Specification;
using DistributedSystem.Shared.Core.Entities;
using DistributedSystem.Shared.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Core.Abstractions;
public interface IReadRepository
{
    Task<T?> GetByIdAsync<T>(string id, CancellationToken cancellationToken) where T : BaseEntity;
    Task<T?> GetByIdAsync<T>(Type entityType, string id, CancellationToken cancellationToken) where T : BaseEntity;

    Task<List<T>> ListAsync<T>(CancellationToken cancellationToken) where T : BaseEntity;
    Task<List<T>> ListAsync<T>(Type entityType, CancellationToken cancellationToken) where T : BaseEntity;

    Task<int> CountAsync<T>(CancellationToken cancellationToken) where T : BaseEntity;
    Task<int> CountAsync<T>(Type entityType, CancellationToken cancellationToken) where T : BaseEntity;

    Task<T?> GetBySpecAsync<T>(BaseSpecification<T> specification, CancellationToken cancellationToken);

    //Task<List<T>> ListAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken) where T : BaseEntity;
    //Task<int> CountAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken) where T : BaseEntity;
}
