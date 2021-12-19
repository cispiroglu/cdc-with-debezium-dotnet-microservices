using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Common.AggregateRoot;
using Shared.Common.DomainEntity;

namespace Infrastructure.EntityFramework;

public abstract class BaseCommandRepository<TDbContext, TAggregateRoot> : IDomainCommandRepository<TAggregateRoot>
    where TDbContext : DbContext
    where TAggregateRoot : DomainEntity, IAggregateRoot
{
    protected readonly TDbContext _dbContext;

    protected BaseCommandRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual TAggregateRoot Get(Guid key)
    {
        if (key == Guid.Empty)
            throw new ArgumentNullException(nameof(key));

        var entity = _dbContext.Set<TAggregateRoot>().FirstOrDefault(q => q.Id == key);

        if (entity == null)
            throw new Exception($"(Id: {key}, Method: Get, EntityType: {typeof(TAggregateRoot).FullName})");

        return entity;
    }

    public Guid Insert(TAggregateRoot entity)
    {
        var entityEntry = _dbContext.Set<TAggregateRoot>().Add(entity);
        return entityEntry.Entity.Id;
    }

    public void Update(TAggregateRoot entity)
    {
        _dbContext.Set<TAggregateRoot>().Update(entity);
    }

    public void Delete(Guid id)
    {
        var entity = Get(id);
        _dbContext.Set<TAggregateRoot>().Remove(entity);
    }
}