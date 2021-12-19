using Microsoft.Extensions.Hosting;
using Shared.Common;

namespace Shared.API;

public class ApplicationLifeCycleService : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IEnumerable<IApplicationLifecycleHook> _applicationLifecycleHooks;

    public ApplicationLifeCycleService(
        IHostApplicationLifetime hostApplicationLifetime,
        IEnumerable<IApplicationLifecycleHook> applicationLifecycleHooks)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _applicationLifecycleHooks = applicationLifecycleHooks;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
        _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
        _hostApplicationLifetime.ApplicationStopped.Register(OnStopped);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        Console.WriteLine("Application Started");
        foreach (var hook in _applicationLifecycleHooks)
            hook.OnStarted();
    }

    private void OnStopping()
    {
        Console.WriteLine("Application Stopping");
        foreach (var hook in _applicationLifecycleHooks)
            hook.OnStopRequested();
    }

    private void OnStopped()
    {
        Console.WriteLine("Application Stopped");
        foreach (var hook in _applicationLifecycleHooks)
            hook.OnStopped();
    }
}