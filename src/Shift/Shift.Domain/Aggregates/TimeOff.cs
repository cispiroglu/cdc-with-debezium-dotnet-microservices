using Shared.Common.AggregateRoot;

namespace Shift.Domain.Aggregates;

public class TimeOff : AggregateRoot<TimeOff>
{
    public virtual Guid Id { get; }
    public virtual Guid EmployeeId { get; }
    public virtual DateTime TimeOffDate { get; }

    private TimeOff(Guid id, Guid employeeId, DateTime timeOffDate)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        EmployeeId = employeeId;
        TimeOffDate = timeOffDate;
    }

    public static TimeOff ForCreate(Guid employeeId, DateTime timeOffDate)
    {
        
        return new TimeOff(Guid.Empty, employeeId, timeOffDate);
    }
    
    public static TimeOff ForUpdate(Guid id, Guid employeeId, DateTime timeOffDate)
    {
        return new TimeOff(id, employeeId, timeOffDate);
    }
}