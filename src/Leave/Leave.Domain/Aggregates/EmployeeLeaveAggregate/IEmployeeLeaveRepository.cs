using Shared.Common;

namespace Leave.Domain.Aggregates.EmployeeLeaveAggregate;

public interface IEmployeeLeaveRepository : IDomainCommandRepository<EmployeeLeave>
{
    
}