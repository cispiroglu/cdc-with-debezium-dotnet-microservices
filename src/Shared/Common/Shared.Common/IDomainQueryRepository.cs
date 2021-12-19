using Shared.Common.AggregateRoot;

namespace Shared.Common;

public interface  IDomainQueryRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
{
    IEnumerable<TAggregateRoot> GetAll();
        
    TAggregateRoot Get(Guid key);
}