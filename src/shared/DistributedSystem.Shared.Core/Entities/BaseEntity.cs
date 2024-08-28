namespace DistributedSystem.Shared.Core.Entities;

[Serializable]
public abstract class BaseEntity
{
    public string Id { get; set; } = null!;
    public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
}


