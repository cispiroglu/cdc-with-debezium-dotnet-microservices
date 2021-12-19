namespace Shared.Common.DomainEntity;

public class DomainEntity : IDomainEntity<Guid>
{
    public virtual Guid Id { get; set; }
}