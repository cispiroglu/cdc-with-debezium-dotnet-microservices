using Microsoft.EntityFrameworkCore;
using Shared.Common.Outbox;

namespace Infrastructure.EntityFramework.Outbox;

public class OutboxEventRepository<TDbContext> : IOutboxEventRepository where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
        
    public OutboxEventRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(OutboxEvent @event)
    {
        await _dbContext.AddAsync(@event);
    }
}