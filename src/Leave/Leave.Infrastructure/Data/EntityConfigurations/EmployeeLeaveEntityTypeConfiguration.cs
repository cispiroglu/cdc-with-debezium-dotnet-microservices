using Leave.Domain.Aggregates.EmployeeLeaveAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leave.Infrastructure.Data.EntityConfigurations;

public class EmployeeLeaveEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeLeave>
{
    public void Configure(EntityTypeBuilder<EmployeeLeave> configuration)
    {
        configuration.ToTable("EMPLOYEE_LEAVE");
        configuration.HasKey(x => x.Id);
            
        configuration.Property(x => x.Id).HasColumnName("ID").IsRequired();
        configuration.Property(x => x.EmployeeId).HasColumnName("EMPLOYEE_ID").IsRequired();
        configuration.Property(x => x.StartDate).HasColumnName("START_DATE").IsRequired();
        configuration.Property(x => x.EndDate).HasColumnName("END_DATE").IsRequired();
        configuration.Property(x => x.Duration).HasColumnName("DURATION").IsRequired();
        configuration.Property(x => x.Description).HasColumnName("DESCRIPTION");

        configuration.Ignore(x => x.DomainEvents);
    }
}