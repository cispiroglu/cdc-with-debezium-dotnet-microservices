using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shared.Common;
using Shared.Common.DomainEventDispatcher;
using Shared.Common.Extensions;
using Shared.Common.Extensions.Configuration;
using Shift.Domain.Aggregates;
using Shift.Infrastructure.Data.EntityConfigurations;

namespace Shift.Infrastructure.Data;

/// <summary>
/// For EFCore CLI Migration Tools
/// </summary>
public class ShiftDbContextDbFactory : IDesignTimeDbContextFactory<ShiftDbContext>
{
    ShiftDbContext IDesignTimeDbContextFactory<ShiftDbContext>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShiftDbContext>();
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
        return new ShiftDbContext(optionsBuilder.Options);
    }
}

public class ShiftDbContext : BaseDbContext<ShiftDbContext>
{
    public DbSet<TimeOff> TimeOffs { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="domainEventDispatcher"></param>
    public ShiftDbContext(DbContextOptions<ShiftDbContext> options, IDomainEventDispatcher domainEventDispatcher)
        : base(options, domainEventDispatcher)
    {
        // ....
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public ShiftDbContext(DbContextOptions<ShiftDbContext> options)
        : base(options)
    {
        // ....
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TimeOffEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}