using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Infrastructure.Ef.Repository;

namespace DistributedSystem.Product.Infrastructure.Repository;
public class ProductRepository : EfRepository
{
    public ProductRepository(IEntityIdFactory entityIdFactory, ProductSqlDbContext sqlDbContext) : base(entityIdFactory, sqlDbContext)
    {

    }
}
