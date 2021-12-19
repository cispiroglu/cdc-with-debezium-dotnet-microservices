using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common.Outbox;

namespace Infrastructure.EntityFramework.Outbox;

public class OutboxEventsEntityTypeConfiguration: IEntityTypeConfiguration<OutboxEvent>
{
    public void Configure(EntityTypeBuilder<OutboxEvent> configuration)
    {
        configuration.ToTable("OUTBOX_EVENTS");
        configuration.Property(x => x.Id).HasColumnName("ID").IsRequired();
        configuration.Property(x => x.AggregateId).HasColumnName("AGGREGATE_ID").IsRequired();
        configuration.Property(x => x.AggregateType).HasColumnName("AGGREGATE_TYPE").IsRequired();
        configuration.Property(x => x.EventType).HasColumnName("EVENT_TYPE").IsRequired();
        configuration.Property(x => x.Payload).HasColumnName("PAYLOAD").IsRequired();
    }
}