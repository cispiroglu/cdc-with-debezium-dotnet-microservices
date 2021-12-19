using MediatR;
using Shared.Common.DomainEvent;

namespace Infrastructure.MediatR;

public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
{
    public TDomainEvent DomainEvent { get; }

    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }
}