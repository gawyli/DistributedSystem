using MediatR;

namespace DistributedSystem.Shared.Core.Entities;

[Serializable]
public abstract class BaseDomainEvent : INotification
{
    public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}
