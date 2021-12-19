namespace Shared.Common;

public interface IApplicationMapper
{
    TDestination Map<TDestination>(object source);

    object Map(object source, Type sourceType, Type destinationType);
}