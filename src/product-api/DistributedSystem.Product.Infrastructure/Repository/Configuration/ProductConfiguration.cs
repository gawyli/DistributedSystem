using DistributedSystem.Shared.Infrastructure.Ef.Entities;
using DistributedSystem.Product.Core.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DistributedSystem.Product.Infrastructure.Repository.Configuration;
public class ProductConfiguration : BaseEntityConfiguration<DistributedSystem.Product.Core.ProductAggregate.Product>
{
    public override void Configure(EntityTypeBuilder<Core.ProductAggregate.Product> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(6,2)
            .IsRequired();

        builder.Property(x => x.Quantity)    
            .IsRequired();


    }
}
