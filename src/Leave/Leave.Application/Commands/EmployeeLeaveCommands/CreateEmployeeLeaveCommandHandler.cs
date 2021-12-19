using Leave.Domain.Aggregates;
using MediatR;
using Shared.Common;

namespace Leave.Application.Commands.LeaveCommands;

public record CreateEmployeeLeaveCommand : IRequest<Unit>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
}

public class CreateEmployeeLeaveCommandHandler : IRequestHandler<CreateEmployeeLeaveCommand, Unit>
{
    private readonly IApplicationMapper _applicationMapper;

    public CreateEmployeeLeaveCommandHandler(IApplicationMapper applicationMapper)
    {
        _applicationMapper = applicationMapper;
    }

    public Task<Unit> Handle(CreateEmployeeLeaveCommand request, CancellationToken cancellationToken)
    {
        var employeeLeave = _applicationMapper.Map<EmployeeLeave>(request);

        return Task.FromResult(Unit.Value);
    }
}