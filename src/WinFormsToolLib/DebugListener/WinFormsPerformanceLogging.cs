// -------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All Rights Reserved.
// -------------------------------------------------------------------

using CommunityToolkit.WinForms.AsyncSupport;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DevTools.Libs.DebugListener;

public partial class WinFormsPerformanceLogging() : IDisposable
{
    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    private readonly AsyncTaskQueue _asyncTaskQueue = new();

    private static WinFormsPerformanceLogging? s_instance;
    private bool _disposedValue;

    /// <summary>
    ///  Gets the instance of <see cref="WinFormsPerformanceLogging"/>.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>The instance of <see cref="WinFormsPerformanceLogging"/>.</returns>
    public static WinFormsPerformanceLogging? GetInstance()
    {
        if (s_instance is not null)
        {
            ((IDisposable)s_instance).Dispose();
            s_instance = null;
        }

        return s_instance ??= new WinFormsPerformanceLogging();
    }

    /// <summary>
    ///  Gets the singleton instance of <see cref="WinFormsPerformanceLogging"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the instance has not been initialized.</exception>
    public static WinFormsPerformanceLogging Instance
        => s_instance ??= GetInstance()!;

    /// <summary>
    ///  Prints a trace message.
    /// </summary>
    /// <param name="message">The message to print.</param>
    /// <param name="category">The category of the message.</param>
    /// <param name="memberName">The name of the member calling this method.</param>
    /// <param name="filePath">The file path of the source code.</param>
    /// <param name="lineNumber">The line number in the source code.</param>
    public static void PerfTrace(
        string message,
        bool markImportant = false,
        string? category = null,
        [CallerMemberName] string? memberName = default,
        [CallerFilePath] string? filePath = default,
        [CallerLineNumber] int lineNumber = -1)
    {
        DateTime timestamp = DateTime.Now;
        int threadId = Environment.CurrentManagedThreadId;

        Instance._asyncTaskQueue.Enqueue(
            () => Instance.DebugPrintAsync(
                timestamp,
                threadId,
                message,
                markImportant,
                category,
                memberName,
                filePath,
                lineNumber));
    }

    /// <summary>
    ///  Prints a debug message.
    /// </summary>
    /// <param name="message">The message to print.</param>
    /// <param name="category">The category of the message.</param>
    /// <param name="memberName">The name of the member calling this method.</param>
    /// <param name="filePath">The file path of the source code.</param>
    /// <param name="lineNumber">The line number in the source code.</param>
    [Conditional("DEBUG")]
    public static void PerfDbgTrace(
        string message,
        bool markImportant = false,
        string? category = null,
        [CallerMemberName] string? memberName = default,
        [CallerFilePath] string? filePath = default,
        [CallerLineNumber] int lineNumber = -1)
    {
        DateTime timestamp = DateTime.Now;
        int threadId = Environment.CurrentManagedThreadId;

        Instance._asyncTaskQueue.Enqueue(
            () => Instance.DebugPrintAsync(
                timestamp,
                threadId,
                message,
                markImportant,
                category,
                memberName,
                filePath,
                lineNumber));
    }

    // Comes down to an Win32 OutputDebugString call, which we can intercept via Shared Memory.
    // By encoding some additional information in the message, we can provide more context.
    // For example, we can include the process ID, thread ID, line number, and category.
    // Also, and that's more important, we get the time stamp from the source, not from
    // the tool which then acts as the listener. This is important for performance analysis.
    private Task DebugPrintAsync(
        DateTime timeStamp,
        int threadId,
        string message,
        bool markImportant = false,
        string? category = null,
        [CallerMemberName] string? memberName = default,
        [CallerFilePath] string? filePath = default,
        [CallerLineNumber] int lineNumber = -1)
    {
        int processId = Process.GetCurrentProcess().Id;
        ExtendedDebugInfo debugInfo;

        debugInfo = new(
            timestamp: timeStamp,
            processId: (ushort)processId,
            threadId: (ushort)threadId,
            lineNo: (ushort)lineNumber,
            command: markImportant
                ? ExtendedDebugInfo.DebugInfoCommandId.ImportantMessage
                : ExtendedDebugInfo.DebugInfoCommandId.Message);

        string combineMessage = $"{debugInfo.ToBase64()}[[{(category ?? "---")}]][[{(memberName ?? "---")}]][[{(filePath ?? "---")}]][[{(message ?? "---")}]]";

        Debug.Print(combineMessage);

        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _asyncTaskQueue.Dispose();
            }

            _disposedValue = true;
        }
    }

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
