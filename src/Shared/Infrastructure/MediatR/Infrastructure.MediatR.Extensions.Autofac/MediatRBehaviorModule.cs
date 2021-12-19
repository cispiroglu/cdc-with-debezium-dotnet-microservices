using Autofac;
using Infrastructure.MediatR.Behavior;
using MediatR;

namespace Infrastructure.MediatR.Extensions.Autofac;

public class MediatRBehaviorModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(UnhandledExceptionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
    }
}