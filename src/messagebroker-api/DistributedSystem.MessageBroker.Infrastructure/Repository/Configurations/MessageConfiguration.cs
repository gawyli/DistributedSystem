
using DistributedSystem.MessageBroker.Core.MessageAggregate;
using DistributedSystem.Shared.Infrastructure.Ef.Entities;

namespace DistributedSystem.MessageBroker.Infrastructure.Repository.Configurations;
public class MessageConfiguration : BaseEntityConfiguration<Message>
{
    public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Message> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.EventId);
        builder.Property(x => x.EventName);
    }
}
