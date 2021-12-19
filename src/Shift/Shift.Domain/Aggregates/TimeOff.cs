using Shared.Common.AggregateRoot;

namespace Shift.Domain.Aggregates;

public class TimeOffAggregate : AggregateRoot<TimeOffAggregate>
{
    public virtual Guid Id { get; }
    public virtual Guid EmployeeId { get; }
    public virtual DateTime StartDate { get; }
    public virtual DateTime EndDate { get; }

    private TimeOffAggregate(Guid id, Guid employeeId, DateTime startDate, DateTime endDate)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        EmployeeId = employeeId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static TimeOffAggregate ForCreate(Guid employeeId, DateTime startDate, DateTime endDate)
    {
        
        return new TimeOffAggregate(Guid.Empty, employeeId, startDate, endDate);
    }
    
    public static TimeOffAggregate ForUpdate(Guid id, Guid employeeId, DateTime startDate, DateTime endDate)
    {
        return new TimeOffAggregate(id, employeeId, startDate, endDate);
    }
}