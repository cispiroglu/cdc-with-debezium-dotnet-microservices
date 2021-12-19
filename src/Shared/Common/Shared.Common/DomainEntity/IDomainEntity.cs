namespace Shared.Common.DomainEntity;

public class IDomainEntity<TPrimaryKey>
{
    TPrimaryKey Id { get; set; }
}