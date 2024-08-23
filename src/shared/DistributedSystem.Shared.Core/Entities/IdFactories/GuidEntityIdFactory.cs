using DistributedSystem.Shared.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Core.Entities.IdFactories;
public class GuidEntityIdFactory : IEntityIdFactory
{
    public string NewId()
    {
        return Guid.NewGuid().ToString();
    }
}