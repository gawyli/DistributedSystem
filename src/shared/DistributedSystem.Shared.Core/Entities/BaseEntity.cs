using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Core.Entities;

[Serializable]
public abstract class BaseEntity
{
    public string Id { get; set; } = null!;
    public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
}


