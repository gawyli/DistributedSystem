using DistributedSystem.Shared.Infrastructure.Ef.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DistributedSystem.Product.Infrastructure.Repository.Configuration;
public class ProductConfiguration : BaseEntityConfiguration<DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Product>
{
    public override void Configure(EntityTypeBuilder<DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Product> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(6, 2)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();


    }
}
