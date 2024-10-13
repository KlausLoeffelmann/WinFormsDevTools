using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DebugListener.Views;

public partial class LogView
{
    public record LogEntry(DateTime timestamp, int ProcessId, string? Message, ExtendedDebugInfo? DebugInfo)
    {
        public DateTime Timestamp { get; set; } = timestamp;
        public TimeSpan Duration { get; set; }
    }
}
