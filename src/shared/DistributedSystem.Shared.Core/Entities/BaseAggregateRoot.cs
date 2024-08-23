using DistributedSystem.Shared.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Core.Entities;

[Serializable]
public class BaseAggregateRoot : BaseEntity, IAggregateRoot
{
    public DateTimeOffset? Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
}
