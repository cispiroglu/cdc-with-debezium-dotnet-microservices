using Leave.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leave.Infrastructure.Data.EntityConfigurations;

public class LeaveEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeLeave>
{
    public void Configure(EntityTypeBuilder<EmployeeLeave> configuration)
    {
        configuration.ToTable("LEA_EMPLOYEE_LEAVE");
        configuration.HasKey(x => x.Id);
            
        configuration.Property(x => x.Id).HasColumnName("ID").IsRequired();
        configuration.Property(x => x.StartDate).HasColumnName("IS_DELETED").IsRequired();
        configuration.Property(x => x.EndDate).HasColumnName("TENANT_ID");
        configuration.Property(x => x.Duration).HasColumnName("INSERTED_BY");
        configuration.Property(x => x.Description).HasColumnName("INSERTED_DATE");

        configuration.Ignore(x => x.DomainEvents);
    }
}