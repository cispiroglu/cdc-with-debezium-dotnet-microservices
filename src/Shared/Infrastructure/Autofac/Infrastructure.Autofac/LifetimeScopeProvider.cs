using Autofac;
using Shared.Common;

namespace Shared.Infrastructure.Autofac;

public class LifetimeScopeProvider: ILifetimeScopeProvider
{
    private readonly ILifetimeScope _lifetimeScope;

    public LifetimeScopeProvider(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    public T Resolve<T>() => _lifetimeScope.Resolve<T>();

    public object Resolve(Type serviceType) => _lifetimeScope.Resolve(serviceType);
}