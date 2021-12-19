using MediatR;

namespace Infrastructure.MediatR.Behavior;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            Console.WriteLine($"RequestName: {requestName}, ExceptionMessage: {ex.Message}");

            throw;
        }
    }
}