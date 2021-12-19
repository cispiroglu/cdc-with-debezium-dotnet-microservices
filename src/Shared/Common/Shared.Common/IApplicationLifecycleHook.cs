namespace Shared.Common;

public interface IApplicationLifecycleHook
{
    void OnStarted();

    void OnStopRequested();

    void OnStopped();
}