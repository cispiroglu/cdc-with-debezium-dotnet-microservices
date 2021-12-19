using Infrastructure.EntityFramework;
using Shift.Domain.Aggregates;
using Shift.Infrastructure.Data;

namespace Shift.Infrastructure.Repositories.TimeOffAggregate;

public class TimeOffRepository : BaseCommandRepository<ShiftDbContext, TimeOff>, ITimeOffRepository
{
    public TimeOffRepository(ShiftDbContext shiftDbContext)
        : base(shiftDbContext)
    {
        // ...
    }
}