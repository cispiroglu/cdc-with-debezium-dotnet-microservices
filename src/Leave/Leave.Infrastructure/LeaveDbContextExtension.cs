using System.Reflection;
using Autofac;

namespace Leave.Infrastructure;

public static class LeaveDbContextExtension
{
    public static void AddLeaveDbContext(this ContainerBuilder builder)
        => builder.RegisterModule(new LeaveDbContextModule());
}