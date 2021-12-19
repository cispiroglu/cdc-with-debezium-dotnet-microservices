using Infrastructure.EntityFramework;
using Shift.Domain.Aggregates;
using Shift.Infrastructure.Data;

namespace Shift.Infrastructure.Repositories.TimeOffAggregate;

public class TimeOffQueryRepository : BaseDomainQueryRepository<ShiftDbContext, TimeOff>, ITimeOffQueryRepository
{
    public TimeOffQueryRepository(ShiftDbContext shiftDbContext)
        :base(shiftDbContext)
    {
        // ...
    }
}