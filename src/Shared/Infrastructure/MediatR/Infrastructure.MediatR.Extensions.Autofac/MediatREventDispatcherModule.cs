using Autofac;
using Shared.Common.DomainEventDispatcher;

namespace Infrastructure.MediatR.Extensions.Autofac;

public class MediatREventDispatcherModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MediatrDomainEventDispatcher>()
            .As<IDomainEventDispatcher>()
            .InstancePerLifetimeScope();
    }
}