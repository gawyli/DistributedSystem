using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Infrastructure.Ef.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Infrastructure.Repository;
public class ProductRepository : EfRepository
{
    public ProductRepository(IEntityIdFactory entityIdFactory, ProductSqlDbContext sqlDbContext) : base(entityIdFactory, sqlDbContext)
    {
        
    }
}
