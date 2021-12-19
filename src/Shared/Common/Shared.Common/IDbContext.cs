namespace Shared.Common;

public interface IDbContext
{
    Task SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
}