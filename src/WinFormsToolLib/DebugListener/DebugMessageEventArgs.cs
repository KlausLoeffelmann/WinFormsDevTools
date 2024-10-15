using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DevTools.Libs.DebugListener;

/// <summary>
///  Provides data for the <see cref="DebugMessageListener.DebugMessageReceivedAsync"/> event.
/// </summary>
/// <remarks>
///  Initializes a new instance of the <see cref="DebugMessageEventArgs"/> class.
/// </remarks>
public class DebugMessageEventArgs(DebugMessage debugMessage) 
    : EventArgs
{
    /// <summary>
    ///  Gets the debug message.
    /// </summary>
    public DebugMessage DebugMessage { get; } = debugMessage;
}
