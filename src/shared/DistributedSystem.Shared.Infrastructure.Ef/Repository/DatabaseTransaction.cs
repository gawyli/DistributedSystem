using DistributedSystem.Shared.Core.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Infrastructure.Ef.Repository;
public class DatabaseTransaction : ITransaction
{
    public DatabaseTransaction(IDbContextTransaction dbTransaction)
    {
        _dbTransaction = dbTransaction;
    }

    private readonly IDbContextTransaction _dbTransaction;

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _dbTransaction.CommitAsync(cancellationToken);
    }

    public Task RollbackAsync(CancellationToken cancellationToken)
    {
        return _dbTransaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbTransaction.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _dbTransaction.DisposeAsync();
    }
}