using Autofac;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Extensions.Autofac;

public static class DbInitializer
{
    public static void Migrate<T>(ILifetimeScope lifeTimeScope) where T : DbContext
    {
        var context = lifeTimeScope.Resolve<T>();
        context.Database.Migrate();
    } 
}