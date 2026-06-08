namespace DevTools.RuntimeDeploy.Infrastructure;

public sealed class RuntimeDeployStatusService
{
    public event EventHandler<RuntimeDeployStatusEventArgs>? StatusReported;

    public void ReportInfo(string message)
        => StatusReported?.Invoke(this, new RuntimeDeployStatusEventArgs(message, Color.DarkGreen, null));

    public void ReportException(Exception exception)
        => StatusReported?.Invoke(this, new RuntimeDeployStatusEventArgs(exception.Message, Color.DarkRed, exception));
}

public sealed class RuntimeDeployStatusEventArgs(string message, Color foregroundColor, Exception? exception) : EventArgs
{
    public string Message { get; } = message;

    public Color ForegroundColor { get; } = foregroundColor;

    public Exception? Exception { get; } = exception;
}
