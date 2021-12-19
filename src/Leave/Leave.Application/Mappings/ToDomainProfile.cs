using AutoMapper;
using Leave.Application.Commands.EmployeeLeaveCommands;
using Leave.Domain.Aggregates.EmployeeLeaveAggregate;

namespace Leave.Application.Mappings;

public class ToDomainProfile : Profile
{
    public ToDomainProfile()
    {
        CreateMap<CreateEmployeeLeaveCommand, EmployeeLeave>()
            .ConstructUsing(src => EmployeeLeave.ForCreate(src.EmployeeId, src.StartDate, src.EndDate, src.Description));
    }
}