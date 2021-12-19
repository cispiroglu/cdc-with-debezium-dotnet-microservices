namespace Shared.Common;

public interface ILifetimeScopeProvider
{
    T Resolve<T>();
        
    object Resolve(Type serviceType);
}