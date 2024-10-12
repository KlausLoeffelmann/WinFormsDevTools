namespace DevTools.Libs.DebugListener;

/// <summary>
///  Provides data for the <see cref="DebugMessageListener.DebugMessageReceivedAsync"/> event.
/// </summary>
/// <remarks>
///  Initializes a new instance of the <see cref="DebugMessageEventArgs"/> class.
/// </remarks>
public class DebugMessageEventArgs(DateTime timeStamp, int processId, ExtendedDebugInfo? debugInfo, string message) 
    : EventArgs
{
    /// <summary>
    ///  Gets the process ID of the source of the debug message.
    /// </summary>
    public int ProcessId { get; } = processId;

    /// <summary>
    ///  Gets the debug message.
    /// </summary>
    public string Message { get; } = message;

    /// <summary>
    ///  Get the timestamp of the debug message.
    /// </summary>
    public DateTime Timestamp { get; } = timeStamp;

    public ExtendedDebugInfo? DebugInfo { get; } = debugInfo;
}
