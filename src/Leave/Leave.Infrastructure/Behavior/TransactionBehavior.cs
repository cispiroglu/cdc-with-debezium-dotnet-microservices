using Leave.Infrastructure.Data;
using MediatR;

namespace Leave.Infrastructure.Behavior;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly LeaveDbContext _leaveDbContext;

    public TransactionBehavior(LeaveDbContext parametersDbContext)
    {
        _leaveDbContext = parametersDbContext ?? throw new ArgumentException(nameof(LeaveDbContext));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var response = await next();

        await _leaveDbContext.SaveEntitiesAsync(cancellationToken);

        return response;
    }
}