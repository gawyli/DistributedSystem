using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Core.Entities;

[Serializable]
public abstract class BaseIntegrationEvent
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTimeOffset CreatedUtc { get; } = DateTimeOffset.UtcNow;
}
