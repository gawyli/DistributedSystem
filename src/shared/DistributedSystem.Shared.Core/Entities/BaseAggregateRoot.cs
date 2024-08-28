using DistributedSystem.Shared.Core.Abstractions;

namespace DistributedSystem.Shared.Core.Entities;

[Serializable]
public class BaseAggregateRoot : BaseEntity, IAggregateRoot
{
    public DateTimeOffset? Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
}
