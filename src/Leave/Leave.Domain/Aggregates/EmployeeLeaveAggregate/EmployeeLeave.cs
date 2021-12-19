using Shared.Common.AggregateRoot;

namespace Leave.Domain.Aggregates;

public class EmployeeLeave : AggregateRoot<EmployeeLeave>
{
    public virtual Guid Id { get; }
    public virtual DateTime StartDate { get; }
    public virtual DateTime EndDate { get; }
    public virtual double Duration { get; private set; }
    public virtual string Description { get; }

    public EmployeeLeave(Guid id, DateTime startDate, DateTime endDate, string description)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
    }

    public static EmployeeLeave ForCreate(DateTime startDate, DateTime endDate, string description)
    {
        return new EmployeeLeave(Guid.NewGuid(), startDate, endDate, description);
    }
    
    public static EmployeeLeave ForUpdate(Guid id, DateTime startDate, DateTime endDate, string description)
    {
        return new EmployeeLeave(id, startDate, endDate, description);
    }

    public void CalculateDuration(IEnumerable<DateTime> publicHolidays)
    {
        var range = Enumerable.Range(0, 1 + EndDate.Subtract(StartDate).Days)
            .Select(offset => StartDate.AddDays(offset)).ToList();
        Duration = (EndDate - StartDate).TotalDays - publicHolidays.Intersect(range).Count();
    }
}