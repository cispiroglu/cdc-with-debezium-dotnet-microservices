using Shared.Common.DomainEvent;

namespace Leave.Domain.Events.EmployeeLeave;

public record LeaveCreatedDomainEvent : DomainEventBase
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public double Duration { get; }
    public string Description { get; }
}