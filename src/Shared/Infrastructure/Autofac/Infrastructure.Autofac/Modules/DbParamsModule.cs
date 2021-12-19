using Autofac;
using Shared.Common.DbParams;

namespace Shared.Infrastructure.Autofac.Modules;

public class DbParamsModule : Module
{
    private readonly IDbParams _dbParams;

    public DbParamsModule(IDbParams dbParams)
    {
        _dbParams = dbParams;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterInstance(_dbParams).As<IDbParams>().SingleInstance();
    }
}