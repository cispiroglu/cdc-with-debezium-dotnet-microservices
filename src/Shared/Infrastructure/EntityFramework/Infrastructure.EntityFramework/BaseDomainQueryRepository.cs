using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Common.AggregateRoot;
using Shared.Common.DomainEntity;

namespace Infrastructure.EntityFramework;

public abstract class BaseDomainQueryRepository<TDbContext, TAggregateRoot> : IDomainQueryRepository<TAggregateRoot> 
    where TDbContext : DbContext
    where TAggregateRoot : DomainEntity, IAggregateRoot
{
    protected readonly TDbContext _dbContext;
        
    protected BaseDomainQueryRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    public IEnumerable<TAggregateRoot> GetAll()
    {
        return _dbContext.Set<TAggregateRoot>().AsNoTracking().AsEnumerable();
    }
        
    public TAggregateRoot Get(Guid key)
    {
        if (key == Guid.Empty)
            throw new ArgumentNullException(nameof(key));

        var entity = _dbContext.Set<TAggregateRoot>().AsNoTracking().FirstOrDefault(q => q.Id.ToString() == key.ToString());

        if (entity == null)
            throw new Exception($"(Id: {key}, Method: Get, EntityType: {typeof(TAggregateRoot).FullName})");

        return entity;
    }
}