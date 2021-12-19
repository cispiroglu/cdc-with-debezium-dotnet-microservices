using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shift.Domain.Aggregates;

namespace Shift.Infrastructure.Data.EntityConfigurations;

public class TimeOffEntityTypeConfiguration : IEntityTypeConfiguration<TimeOff>
{
    public void Configure(EntityTypeBuilder<TimeOff> configuration)
    {
        configuration.ToTable("TIME_OFF");
        configuration.HasKey(x => x.Id);
            
        configuration.Property(x => x.Id).HasColumnName("ID").IsRequired();
        configuration.Property(x => x.EmployeeId).HasColumnName("EMPLOYEE_ID").IsRequired();
        configuration.Property(x => x.TimeOffDate).HasColumnName("TIME_OFF_DATE").IsRequired();

        configuration.Ignore(x => x.DomainEvents);
    }
}