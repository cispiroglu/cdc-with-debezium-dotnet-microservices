using Infrastructure.EntityFramework;
using Leave.Domain.Aggregates.EmployeeLeaveAggregate;
using Leave.Infrastructure.Data;

namespace Leave.Infrastructure.Repositories.EmployeeLeaveAggregate;

public class EmployeeLeaveQueryRepository : BaseDomainQueryRepository<LeaveDbContext, EmployeeLeave>, IEmployeeLeaveQueryRepository
{
    public EmployeeLeaveQueryRepository(LeaveDbContext leaveDbContext)
        :base(leaveDbContext)
    {
        // ...
    }
}