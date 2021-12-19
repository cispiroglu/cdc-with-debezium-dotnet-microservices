using Autofac;

namespace Shift.Infrastructure;

public static class ShiftDbContextExtension
{
    public static void AddShiftDbContext(this ContainerBuilder builder)
        => builder.RegisterModule(new ShiftDbContextModule());
}