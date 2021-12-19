using Shared.Common.DomainEvent;

namespace Leave.Domain.Events.EmployeeLeave;

public record EmployeeLeaveCreatedDomainEvent : DomainEventBase
{
    public Guid EmployeeId { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public double Duration { get; }
    public string Description { get; }

    public EmployeeLeaveCreatedDomainEvent(Guid id, Guid employeeId, DateTime startDate, DateTime endDate,
        double duration, string description)
    {
        Id = id;
        EmployeeId = employeeId;
        StartDate = startDate;
        EndDate = endDate;
        Duration = duration;
        Description = description;

    }
}