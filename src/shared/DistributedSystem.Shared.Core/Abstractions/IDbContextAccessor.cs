using DistributedSystem.Shared.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DistributedSystem.Shared.Core.Abstractions;
public interface IDbContextAccessor
{
    DbContext GetDbContext<T>() where T : BaseEntity;
}
