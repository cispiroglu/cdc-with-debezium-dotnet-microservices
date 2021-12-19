namespace Shared.Common;

public interface IDomainEventDispatcher
{
    Task Dispatch(IDomainEvent @event);
}