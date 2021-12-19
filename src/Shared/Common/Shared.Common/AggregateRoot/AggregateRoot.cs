using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Common.DomainEvent;
using Shared.Common.Outbox;

namespace Shared.Common.AggregateRoot;

public class AggregateRoot<T> : DomainEntity.DomainEntity, IAggregateRoot
{
    [NotMapped] 
    private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new();

    [NotMapped]
    public IProducerConsumerCollection<IDomainEvent> DomainEvents => _domainEvents;
        
    protected void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Enqueue(eventItem);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
        
    public OutboxEvent ToOutbox(string eventType, string serializedPayload)
    {
        return new OutboxEvent(Id, typeof(T).Name, eventType, serializedPayload);
    }
}