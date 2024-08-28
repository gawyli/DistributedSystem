namespace DistributedSystem.Shared.Core.Abstractions;
public interface IClock
{
    DateTimeOffset CurrentDateTime { get; }
    DateOnly CurrentDate { get; }
    TimeOnly CurrentTime { get; }
}
