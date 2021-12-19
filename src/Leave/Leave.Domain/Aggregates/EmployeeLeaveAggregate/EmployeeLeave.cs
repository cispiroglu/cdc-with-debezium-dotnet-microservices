using Leave.Domain.Events.EmployeeLeave;
using Shared.Common.AggregateRoot;

namespace Leave.Domain.Aggregates.EmployeeLeaveAggregate;

public class EmployeeLeave : AggregateRoot<EmployeeLeave>
{
    public virtual Guid Id { get; }
    public virtual Guid EmployeeId { get; }
    public virtual DateTime StartDate { get; }
    public virtual DateTime EndDate { get; }
    public virtual double Duration { get; private set; }
    public virtual string Description { get; }

    private EmployeeLeave(Guid id, Guid employeeId, DateTime startDate, DateTime endDate, string description)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        EmployeeId = employeeId;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
        
        if (id == Guid.Empty)
            AddDomainEvent(new EmployeeLeaveCreatedDomainEvent(Id, EmployeeId, StartDate, EndDate, Duration, Description));
    }

    public static EmployeeLeave ForCreate(Guid employeeId, DateTime startDate, DateTime endDate, string description)
    {
        
        return new EmployeeLeave(Guid.Empty, employeeId, startDate, endDate, description);
    }
    
    public static EmployeeLeave ForUpdate(Guid id, Guid employeeId, DateTime startDate, DateTime endDate, string description)
    {
        return new EmployeeLeave(id, employeeId, startDate, endDate, description);
    }

    public void CalculateDuration(IEnumerable<DateTime> publicHolidays)
    {
        var range = Enumerable.Range(0, 1 + EndDate.Subtract(StartDate).Days)
            .Select(offset => StartDate.AddDays(offset).Date).ToList();
        Duration = (EndDate - StartDate).TotalDays - publicHolidays.Intersect(range).Count();
    }
}