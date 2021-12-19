using Autofac;
using Shared.Common;
using Shared.Common.DbParams;
using Shared.Infrastructure.Autofac.Modules;

namespace Shared.Infrastructure.Autofac;

public static class ApplicationExtension
{
    public static void AddAutofac(this ContainerBuilder builder, IDbParams dbParams)
    {
        builder.RegisterModule(new DbParamsModule(dbParams));
        
        builder.RegisterType<LifetimeScopeProvider>()
            .As<ILifetimeScopeProvider>()
            .InstancePerLifetimeScope();
    }
}