using AutoMapper;
using Leave.Application.Queries.EmployeeLeaveQueries;
using Leave.Domain.Aggregates.EmployeeLeaveAggregate;
using Leave.Domain.Events.EmployeeLeave;

namespace Leave.Application.Mappings;

public class ToDtosProfile : Profile
{
    public ToDtosProfile()
    {
        CreateMap<EmployeeLeave, EmployeeLeaveDto>();
        CreateMap<EmployeeLeave, EmployeeLeaveCreatedEvent>();
    }
}
