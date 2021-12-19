using MediatR;
using Shift.Infrastructure.Data;

namespace Shift.Infrastructure.Behavior;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ShiftDbContext _shiftDbContext;

    public TransactionBehavior(ShiftDbContext shiftDbContext)
    {
        _shiftDbContext = shiftDbContext ?? throw new ArgumentException(nameof(ShiftDbContext));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var response = await next();

        await _shiftDbContext.SaveEntitiesAsync(cancellationToken);

        return response;
    }
}