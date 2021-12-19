using Autofac;
using Infrastructure.EntityFramework.Outbox;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Common.DbParams;
using Shared.Common.DomainEventDispatcher;
using Shared.Common.Extensions;
using Shared.Common.Outbox;
using Shift.Domain.Aggregates;
using Shift.Infrastructure.Data;
using Shift.Infrastructure.Repositories.TimeOffAggregate;

namespace Shift.Infrastructure;

public class ShiftDbContextModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TimeOffRepository>().As<ITimeOffRepository>();
        builder.RegisterType<TimeOffQueryRepository>().As<ITimeOffQueryRepository>();
        builder.RegisterType<OutboxEventRepository<ShiftDbContext>>().As<IOutboxEventRepository>();
        
        builder.Register(componentContext =>
            {
                var dbParams = componentContext.Resolve<IDbParams>();
                var domainEventDispatcher = componentContext.Resolve<IDomainEventDispatcher>();
                var optionsBuilder = new DbContextOptionsBuilder<ShiftDbContext>();
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
                    
                return new ShiftDbContext(optionsBuilder.Options, domainEventDispatcher);
            })
            .As<ShiftDbContext>()
            .InstancePerLifetimeScope();
    }
}