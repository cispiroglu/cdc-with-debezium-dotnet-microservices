using Infrastructure.EntityFramework;
using Leave.Domain.Aggregates.EmployeeLeaveAggregate;
using Leave.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shared.Common;
using Shared.Common.DomainEventDispatcher;
using Shared.Common.Extensions;
using Shared.Common.Extensions.Configuration;

namespace Leave.Infrastructure.Data;

/// <summary>
/// For EFCore CLI Migration Tools
/// </summary>
public class LeaveDbContextDbFactory : IDesignTimeDbContextFactory<LeaveDbContext>
{
    LeaveDbContext IDesignTimeDbContextFactory<LeaveDbContext>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LeaveDbContext>();
        var dbParams = ConfigurationHelper.DbParams;
        var dbType = dbParams.DbType.ToEnum<DbType>();

        switch (dbType)
        {
            case DbType.MSSQL:
                optionsBuilder.UseSqlServer(dbParams.ConnectionString);
                break;
            case DbType.PostgreSQL:
                optionsBuilder.UseNpgsql(dbParams.ConnectionString);
                break;
            case DbType.Oracle:
            case DbType.MySQL:
            case DbType.SQLite:
            case DbType.NotSelected:
                throw new NotImplementedException();
            default:
                throw new ArgumentOutOfRangeException();
        }

        optionsBuilder.UseUpperCaseNamingConvention();
        return new LeaveDbContext(optionsBuilder.Options);
    }
}

public class LeaveDbContext : BaseDbContext<LeaveDbContext>
{
    public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="domainEventDispatcher"></param>
    public LeaveDbContext(DbContextOptions<LeaveDbContext> options, IDomainEventDispatcher domainEventDispatcher)
        : base(options, domainEventDispatcher)
    {
        // ....
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public LeaveDbContext(DbContextOptions<LeaveDbContext> options)
        : base(options)
    {
        // ....
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeLeaveEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}