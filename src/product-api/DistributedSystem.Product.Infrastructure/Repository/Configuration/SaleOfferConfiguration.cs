using DistributedSystem.Shared.Common.Aggregates.ProductAggragate;
using DistributedSystem.Shared.Infrastructure.Ef.Entities;

namespace DistributedSystem.Product.Infrastructure.Repository.Configuration;
public class SaleOfferConfiguration : BaseEntityConfiguration<SaleOffer>
{
    public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SaleOffer> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasMaxLength(SaleOffer.Invariants.NameMaxLength)
            .IsRequired();

        builder.Property(x => x.Discount)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();
    }
}
