using Autofac;
using Infrastructure.EntityFramework.Outbox;
using Leave.Domain.Aggregates.EmployeeLeaveAggregate;
using Leave.Infrastructure.Data;
using Leave.Infrastructure.Repositories.EmployeeLeaveAggregate;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Common.DbParams;
using Shared.Common.DomainEventDispatcher;
using Shared.Common.Extensions;
using Shared.Common.Outbox;

namespace Leave.Infrastructure;

public class LeaveDbContextModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EmployeeLeaveRepository>().As<IEmployeeLeaveRepository>();
        builder.RegisterType<EmployeeLeaveQueryRepository>().As<IEmployeeLeaveQueryRepository>();
        builder.RegisterType<OutboxEventRepository<LeaveDbContext>>().As<IOutboxEventRepository>();
        
        builder.Register(componentContext =>
                {
                    var dbParams = componentContext.Resolve<IDbParams>();
                    var domainEventDispatcher = componentContext.Resolve<IDomainEventDispatcher>();
                    var optionsBuilder = new DbContextOptionsBuilder<LeaveDbContext>();
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
#if DEBUG
                    // For logging sql query parameters value
                    optionsBuilder.EnableSensitiveDataLogging();
                    optionsBuilder.LogTo(Console.WriteLine);
#endif
                    
                    return new LeaveDbContext(optionsBuilder.Options, domainEventDispatcher);
                })
                .As<LeaveDbContext>()
                .InstancePerLifetimeScope();
    }
}