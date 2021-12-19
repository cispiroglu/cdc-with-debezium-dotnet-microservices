using AutoMapper;
using Shared.Common;

namespace Infrastructure.AutoMapper;

public class ApplicationMapper : IApplicationMapper
{
    private readonly IMapper _mapper;

    public ApplicationMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public object Map(object source, Type sourceType, Type destinationType)
    {
        return _mapper.Map(source, sourceType, destinationType);
    }
}