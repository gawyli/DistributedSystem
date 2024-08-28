using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Infrastructure.Ef.Repository;

namespace DistributedSystem.MessageBroker.Infrastructure.Repository;
public class MessageRepository : EfRepository
{
    public MessageRepository(IEntityIdFactory entityIdFactory, MessageBrokerSqlDbContext sqlDbContext) : base(entityIdFactory, sqlDbContext)
    {
    }
}
