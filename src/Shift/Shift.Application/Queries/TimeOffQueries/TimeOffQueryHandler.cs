using MediatR;
using Shared.Common;
using Shift.Domain.Aggregates;

namespace Shift.Application.Queries.TimeOffQueries;

public record GetAllQuery : IRequest<IEnumerable<TimeOffDto>>;
    
public record GetQuery(Guid Id) : IRequest<TimeOffDto>;

public record TimeOffDto 
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime TimeOffDate { get; set; }
}
    
public class TimeOffQueryHandler :
    IRequestHandler<GetAllQuery, IEnumerable<TimeOffDto>>,
    IRequestHandler<GetQuery, TimeOffDto>
{
    private readonly IApplicationMapper _applicationMapper;
    private readonly ITimeOffQueryRepository _timeOffQueryRepository;

    public TimeOffQueryHandler(IApplicationMapper applicationMapper, ITimeOffQueryRepository timeOffQueryRepository)
    {
        _applicationMapper = applicationMapper;
        _timeOffQueryRepository = timeOffQueryRepository;
    }

    public Task<IEnumerable<TimeOffDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var dal = _timeOffQueryRepository.GetAll();
        var result = _applicationMapper.Map<IEnumerable<TimeOffDto>>(dal);

        return Task.FromResult(result);
    }

    public Task<TimeOffDto> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        var dal = _timeOffQueryRepository.Get(request.Id);
        var result = _applicationMapper.Map<TimeOffDto>(dal);

        return Task.FromResult(result);
    }
}