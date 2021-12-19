namespace Shared.Common.Outbox;

public interface IOutboxEventRepository
{
    Task Add(OutboxEvent @event);
}