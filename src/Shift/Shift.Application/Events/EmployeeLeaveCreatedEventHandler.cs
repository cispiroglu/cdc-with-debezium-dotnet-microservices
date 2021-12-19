using Shared.Common.Extensions;
using Shared.Common.Kafka;
using Shift.Domain.Aggregates;

namespace Shift.Application.Events;

public class EmployeeLeaveCreatedEvent
{
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class EmployeeLeaveCreatedEventHandler : IKafkaHandler<string, EmployeeLeaveCreatedEvent>
{
    private readonly ITimeOffRepository _timeOffRepository;

    public EmployeeLeaveCreatedEventHandler(ITimeOffRepository timeOffRepository)
    {
        _timeOffRepository = timeOffRepository;
    }

    public Task HandleAsync(string key, EmployeeLeaveCreatedEvent @event)
    {
        var timeOffRange = Enumerable.Range(0, 1 + @event.EndDate.Subtract(@event.StartDate).Days)
            .Select(offset => @event.StartDate.AddDays(offset).Date).ToList();
        
        Console.WriteLine($"Key: {key}, Event: {nameof(EmployeeLeaveCreatedEvent)} : {@event.ToJSON()}");
        
        foreach (var timeOff in timeOffRange)
            _timeOffRepository.Insert(TimeOff.ForCreate(@event.EmployeeId, timeOff));
        
        return Task.CompletedTask;
    }
}