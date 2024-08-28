namespace DistributedSystem.Shared.Core.Abstractions;
public interface IHasUpdated
{
    DateTimeOffset? Updated { get; set; }
}
