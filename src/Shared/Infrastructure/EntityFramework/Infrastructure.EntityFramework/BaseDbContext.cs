using Infrastructure.EntityFramework.Outbox;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Common.AggregateRoot;
using Shared.Common.DomainEventDispatcher;
using Shared.Common.Outbox;

namespace Infrastructure.EntityFramework;

public class BaseDbContext<TDbContext> : DbContext, IDbContext where TDbContext : DbContext
{
    protected DbSet<OutboxEvent> Outboxes { get; set; }
    
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    
    protected BaseDbContext(DbContextOptions<TDbContext> options,
        IDomainEventDispatcher domainEventDispatcher)
        : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }
    
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
    
    public async Task SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        // For Dispatching Domain Events
        await DispatchDomainEvents();

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        await base.SaveChangesAsync(cancellationToken);
    }
    
    private async Task DispatchDomainEvents()
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        // await _mediator.DispatchDomainEventsAsync(_lifetimeScope, this);
        var domainEventEntities = ChangeTracker.Entries<IAggregateRoot>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        var domainEventTasks = domainEventEntities.Select(async entity =>
        {
            entity.DomainEvents.TryTake(out var domainEvent);
            await _domainEventDispatcher.Dispatch(domainEvent);
        });

        await Task.WhenAll(domainEventTasks);
    }
}