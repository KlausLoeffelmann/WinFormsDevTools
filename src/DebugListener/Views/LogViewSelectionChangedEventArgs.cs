﻿using DevTools.Libs.DebugListener;
using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DebugListener.Views;

public class LogViewSelectionChangedEventArgs(
    int rowIndex,
WinFormsPerformanceLogging.DebugMessage[] selectedMessages,
    TimeSpan? totalProcessTime = default,
    TimeSpan? totalThreadTime = default,
    TimeSpan? totalDuration = default) : EventArgs
{
    public int RowIndex { get; } = rowIndex;
    public TimeSpan? TotalProcessTime { get; } = totalProcessTime;
    public TimeSpan? TotalThreadTime { get; } = totalThreadTime;
    public TimeSpan? TotalDuration { get; } = totalDuration;
    public DebugMessage[] SelectedMessages { get; set; } = selectedMessages;
}
