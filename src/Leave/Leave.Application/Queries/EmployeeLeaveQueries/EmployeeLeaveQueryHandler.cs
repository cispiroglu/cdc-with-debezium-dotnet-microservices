using Leave.Domain.Aggregates.EmployeeLeaveAggregate;
using MediatR;
using Shared.Common;

namespace Leave.Application.Queries.EmployeeLeaveQueries;

public record GetAllQuery : IRequest<IEnumerable<EmployeeLeaveDto>>;
    
public record GetQuery(Guid Id) : IRequest<EmployeeLeaveDto>;

public record EmployeeLeaveDto 
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Duration { get; set; }
    public string Description { get; set; } = string.Empty;
}
    
public class EmployeeLeaveQueryHandler :
    IRequestHandler<GetAllQuery, IEnumerable<EmployeeLeaveDto>>,
    IRequestHandler<GetQuery, EmployeeLeaveDto>
{
    private readonly IApplicationMapper _applicationMapper;
    private readonly IEmployeeLeaveQueryRepository _employeeLeaveQueryRepository;

    public EmployeeLeaveQueryHandler(IApplicationMapper applicationMapper, IEmployeeLeaveQueryRepository employeeLeaveQueryRepository)
    {
        _applicationMapper = applicationMapper;
        _employeeLeaveQueryRepository = employeeLeaveQueryRepository;
    }

    public Task<IEnumerable<EmployeeLeaveDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var dal = _employeeLeaveQueryRepository.GetAll();
        var result = _applicationMapper.Map<IEnumerable<EmployeeLeaveDto>>(dal);

        return Task.FromResult(result);
    }

    public Task<EmployeeLeaveDto> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        var dal = _employeeLeaveQueryRepository.Get(request.Id);
        var result = _applicationMapper.Map<EmployeeLeaveDto>(dal);

        return Task.FromResult(result);
    }
}