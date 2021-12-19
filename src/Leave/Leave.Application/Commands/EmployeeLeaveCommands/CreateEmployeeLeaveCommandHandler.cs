using Leave.Domain.Aggregates.EmployeeLeaveAggregate;
using Leave.Domain.Events.EmployeeLeave;
using MediatR;
using Shared.Common;
using Shared.Common.Extensions;
using Shared.Common.Outbox;

namespace Leave.Application.Commands.EmployeeLeaveCommands;

public record CreateEmployeeLeaveCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
}

public class CreateEmployeeLeaveCommandHandler : IRequestHandler<CreateEmployeeLeaveCommand, Guid>
{
    private readonly IApplicationMapper _applicationMapper;
    private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
    private readonly IOutboxEventRepository _outboxEventRepository;

    public CreateEmployeeLeaveCommandHandler(IApplicationMapper applicationMapper, IEmployeeLeaveRepository employeeLeaveRepository, IOutboxEventRepository outboxEventRepository)
    {
        _applicationMapper = applicationMapper;
        _employeeLeaveRepository = employeeLeaveRepository;
        _outboxEventRepository = outboxEventRepository;
    }

    public Task<Guid> Handle(CreateEmployeeLeaveCommand request, CancellationToken cancellationToken)
    {
        var employeeLeave = _applicationMapper.Map<EmployeeLeave>(request);
        var publicHolidays = new List<DateTime> { new DateTime(2021, 12, 16), new DateTime(2021, 12, 17) };
        employeeLeave.CalculateDuration(publicHolidays);

        _employeeLeaveRepository.Insert(employeeLeave);
        _outboxEventRepository.Add(new OutboxEvent(Guid.NewGuid(), nameof(EmployeeLeave), nameof(EmployeeLeaveCreatedEvent), 
            _applicationMapper.Map<EmployeeLeaveCreatedEvent>(employeeLeave).ToJSON()));
        
        return Task.FromResult(employeeLeave.Id);
    }
}