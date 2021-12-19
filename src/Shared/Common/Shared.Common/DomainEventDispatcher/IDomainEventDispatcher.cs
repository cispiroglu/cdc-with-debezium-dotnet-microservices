using Shared.Common.DomainEvent;

namespace Shared.Common.DomainEventDispatcher;

public interface IDomainEventDispatcher
{
    Task Dispatch(IDomainEvent @event);
}