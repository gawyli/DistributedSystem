using DistributedSystem.Shared.Core.Abstractions;

namespace DistributedSystem.Shared.Core.Entities.IdFactories;
public class GuidEntityIdFactory : IEntityIdFactory
{
    public string NewId()
    {
        return Guid.NewGuid().ToString();
    }
}