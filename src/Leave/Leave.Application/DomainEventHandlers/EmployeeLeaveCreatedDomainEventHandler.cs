using Infrastructure.MediatR;
using Leave.Domain.Events.EmployeeLeave;
using MediatR;
using Shared.Common.Extensions;

namespace Leave.Application.DomainEventHandlers;

public class LeaveCreatedDomainEventHandler : INotificationHandler<DomainEventNotification<EmployeeLeaveCreatedDomainEvent>>
{
    public Task Handle(DomainEventNotification<EmployeeLeaveCreatedDomainEvent> notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"LeaveCreateDomainEvent: {notification.ToJSON()}");
        return Task.CompletedTask;
    }
}