using DistributedSystem.Shared.Core.Abstractions;

namespace DistributedSystem.Shared.Infrastructure.Clock;
public class UtcClock : IClock
{
    // TODO: [jmcd] behaviour changed to use UTC - discuss what is wanted
    private DateTimeOffset Now => DateTimeOffset.UtcNow;

    public DateTimeOffset CurrentDateTime => Now;
    public DateOnly CurrentDate => DateOnly.FromDateTime(Now.UtcDateTime);
    public TimeOnly CurrentTime => TimeOnly.FromDateTime(Now.UtcDateTime);
}
