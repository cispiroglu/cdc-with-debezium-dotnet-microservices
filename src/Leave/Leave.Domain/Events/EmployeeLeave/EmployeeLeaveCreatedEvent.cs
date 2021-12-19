namespace Leave.Domain.Events.EmployeeLeave;

public class EmployeeLeaveCreatedEvent
{
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}