using System.Reflection;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace Infrastructure.MediatR.Extensions.Autofac;

public static class MediatRExtension
{
    public static void AddMediatR(this ContainerBuilder builder, Assembly assembly)
    {
        builder.RegisterMediatR(assembly);
        builder.RegisterModule(new MediatRBehaviorModule());
        builder.RegisterModule(new MediatREventDispatcherModule());
    }
}