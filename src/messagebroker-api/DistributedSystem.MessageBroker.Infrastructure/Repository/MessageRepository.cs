using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Infrastructure.Ef.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.MessageBroker.Infrastructure.Repository;
public class MessageRepository : EfRepository
{
    public MessageRepository(IEntityIdFactory entityIdFactory, MessageBrokerSqlDbContext sqlDbContext) : base(entityIdFactory, sqlDbContext)
    {
    }
}
