namespace Shared.Common.DomainEvent;

public record DomainEventBase : IDomainEvent
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; }
    
    public DomainEventBase()
    {
        CreatedAt = DateTime.UtcNow;
    }
}