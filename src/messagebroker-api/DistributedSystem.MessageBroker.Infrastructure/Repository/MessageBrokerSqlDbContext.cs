using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Infrastructure.Ef.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DistributedSystem.MessageBroker.Infrastructure.Repository;
public class MessageBrokerSqlDbContext : SqlDbContext
{
    public MessageBrokerSqlDbContext(DbContextOptions options, IMediator? mediator, IClock clock) : base(options, mediator, clock)
    {

    }
    protected override Assembly GetEntityConfigurationAssembly()
    {
        return this.GetType().Assembly;
    }
}
