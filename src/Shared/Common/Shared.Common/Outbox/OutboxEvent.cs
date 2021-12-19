namespace Shared.Common.Outbox;

public class OutboxEvent
{
    public OutboxEvent(Guid aggregateId, string aggregateType, string eventType, string payload)
    {
        Id = new Guid();
        AggregateId = aggregateId;
        AggregateType = aggregateType;
        EventType = eventType;
        Payload = payload;
    }

    public Guid Id { get; }

    public Guid AggregateId { get; }
        
    public string AggregateType { get; }
        
    public string EventType { get; }
        
    public string Payload { get; }
}