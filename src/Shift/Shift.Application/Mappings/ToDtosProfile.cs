using AutoMapper;
using Shift.Application.Queries.TimeOffQueries;
using Shift.Domain.Aggregates;

namespace Shift.Application.Mappings;

public class ToDtosProfile : Profile
{
    public ToDtosProfile()
    {
        CreateMap<TimeOff, TimeOffDto>();
    }
}