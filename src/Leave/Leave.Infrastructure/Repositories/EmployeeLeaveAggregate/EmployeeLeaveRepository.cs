using Infrastructure.EntityFramework;
using Leave.Domain.Aggregates.EmployeeLeaveAggregate;
using Leave.Infrastructure.Data;

namespace Leave.Infrastructure.Repositories.EmployeeLeaveAggregate;

public class EmployeeLeaveRepository : BaseCommandRepository<LeaveDbContext, EmployeeLeave>, IEmployeeLeaveRepository
{
    public EmployeeLeaveRepository(LeaveDbContext leaveDbContext)
        : base(leaveDbContext)
    {
        // ...
    }
}