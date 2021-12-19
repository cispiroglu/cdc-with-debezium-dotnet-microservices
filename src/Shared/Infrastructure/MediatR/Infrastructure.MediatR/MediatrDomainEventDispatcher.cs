using MediatR;
using Shared.Common.DomainEvent;
using Shared.Common.DomainEventDispatcher;

namespace Infrastructure.MediatR;

public class MediatrDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;
    
    public MediatrDomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Dispatch(IDomainEvent @event)
    {
        var domainEventNotification = createDomainEventNotification(@event);
        await _mediator.Publish(domainEventNotification);
    }
       
    private INotification createDomainEventNotification(IDomainEvent domainEvent)
    {
        var genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
        return (INotification)Activator.CreateInstance(genericDispatcherType, domainEvent);

    }
}