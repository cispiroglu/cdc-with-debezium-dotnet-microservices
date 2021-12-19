using Infrastructure.EntityFramework.Outbox;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Outbox;

namespace Infrastructure.EntityFramework;

public class BaseDbContext<TDbContext> : DbContext where TDbContext : DbContext
{
    protected DbSet<OutboxEvent> Outboxes { get; set; }
    
    protected BaseDbContext(DbContextOptions<TDbContext> options)
        : base(options)
    {
        // ...
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OutboxEventsEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}