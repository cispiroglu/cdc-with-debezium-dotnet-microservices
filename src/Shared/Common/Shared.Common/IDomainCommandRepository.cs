using Shared.Common.AggregateRoot;

namespace Shared.Common;

public interface IDomainCommandRepository
{
    // ...
}

public interface IDomainCommandRepository<TAggregateRoot> : IDomainCommandRepository where TAggregateRoot : IAggregateRoot
{
    TAggregateRoot Get(Guid key);

    Guid Insert(TAggregateRoot entity);

    void Update(TAggregateRoot entity);

    void Delete(Guid id);
}