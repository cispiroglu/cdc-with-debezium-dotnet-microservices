using Autofac;
using Leave.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Common.DbParams;
using Shared.Common.Extensions;

namespace Leave.Infrastructure;

public class LeaveDbContextModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(componentContext =>
                {
                    var dbParams = componentContext.Resolve<IDbParams>();
                    var optionsBuilder = new DbContextOptionsBuilder<LeaveDbContext>();
                    var dbType = dbParams.DbType.ToEnum<DbType>();

                    switch (dbType)
                    {
                        case DbType.MSSQL:
                            optionsBuilder.UseSqlServer(dbParams.ConnectionString);
                            break;
                        case DbType.Oracle:
                        case DbType.PostgreSQL:
                        case DbType.MySQL:
                        case DbType.SQLite:
                        case DbType.NotSelected:
                            throw new NotImplementedException();
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                
                    optionsBuilder.UseUpperCaseNamingConvention();
#if DEBUG
                    // For logging sql query parameters value
                    optionsBuilder.EnableSensitiveDataLogging();
                    optionsBuilder.LogTo(Console.WriteLine);
#endif
                    
                    return new LeaveDbContext(optionsBuilder.Options);
                })
                .As<LeaveDbContext>()
                .InstancePerLifetimeScope();
    }
}