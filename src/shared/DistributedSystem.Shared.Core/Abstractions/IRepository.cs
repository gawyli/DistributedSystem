using DistributedSystem.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Core.Abstractions;
public interface IRepository : IReadRepository
{
    Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken) where T : BaseEntity, IAggregateRoot;
    Task UpdateAsync<T>(T entity, CancellationToken cancellationToken) where T : BaseEntity, IAggregateRoot;
    Task DeleteAsync<T>(T entity, CancellationToken cancellationToken) where T : BaseEntity, IAggregateRoot;
    Task<ITransaction> BeginTransactionAsync<T>(CancellationToken cancellationToken);
}
