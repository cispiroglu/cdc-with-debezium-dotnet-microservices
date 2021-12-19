using System.Collections.Concurrent;
using Shared.Common.DomainEvent;
using Shared.Common.Outbox;

namespace Shared.Common.AggregateRoot;

public interface IAggregateRoot
{
    IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
        
    OutboxEvent ToOutbox(string eventType, string serializedPayload);
}