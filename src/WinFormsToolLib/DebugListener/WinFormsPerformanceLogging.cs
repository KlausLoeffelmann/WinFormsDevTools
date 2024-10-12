// -------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All Rights Reserved.
// -------------------------------------------------------------------

using CommunityToolkit.WinForms.AsyncSupport;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DevTools.Libs.DebugListener;

public partial class WinFormsPerformanceLogging
{
    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    private readonly AsyncTaskQueue? _taskQueue = new();

    [Conditional("DEBUG")]
    public void DPrint(
        string message,
        string? category = null,
        [CallerMemberName] string? memberName = default,
        [CallerFilePath] string? filePath = default,
        [CallerLineNumber] int lineNumber = -1)
    {
        TimeSpan elapsedMilliseconds = _stopwatch.Elapsed;
        int processId = Process.GetCurrentProcess().Id;

        ushort categoryStringId = 0;
        ushort memberNameStringId = 0;
        ushort filePathStringId = 0;

        ExtendedDebugInfo debugInfo;

        if (!string.IsNullOrEmpty(category))
        {
            if (ExtendedDebugInfo.AddOrGetProcessStringIndex(
                processId: processId,
                value: category!,
                stringId: out ushort stringId))
            {
                debugInfo = new(
                    timestamp: elapsedMilliseconds,
                    processId: (ushort)processId,
                    categoryId: stringId,
                    command: ExtendedDebugInfo.DebugInfoCommandId.CategoryDefinition);

                Debug.Print($"{debugInfo.ToBase64()}{category}");
                categoryStringId = stringId;
            }
        }

        if (!string.IsNullOrEmpty(memberName))
        {
            if (ExtendedDebugInfo.AddOrGetProcessStringIndex(processId, memberName!, out ushort stringId))
            {
                debugInfo = new(
                    timestamp: elapsedMilliseconds,
                    processId: (ushort)processId,
                    methodId: stringId,
                    command: ExtendedDebugInfo.DebugInfoCommandId.MethodNameDefinition);

                Debug.Print($"{debugInfo.ToBase64()}{memberName}");
                memberNameStringId = stringId;
            }
        }

        if (!string.IsNullOrEmpty(filePath))
        {
            // Separate filename from path, and only use the filename
            filePath = Path.GetFileName(filePath);

            if (ExtendedDebugInfo.AddOrGetProcessStringIndex(processId, filePath!, out ushort stringId))
            {
                debugInfo = new(
                    timestamp: elapsedMilliseconds,
                    processId: (ushort)processId,
                    filenameId: stringId,
                    command: ExtendedDebugInfo.DebugInfoCommandId.FileNameDefinition);

                Debug.Print($"{debugInfo.ToBase64()}{filePath}");
                filePathStringId = stringId;
            }
        }

        debugInfo = new(
            timestamp: elapsedMilliseconds,
            processId: (ushort)processId,
            threadId: (ushort)Thread.CurrentThread.ManagedThreadId,
            methodId: memberNameStringId,
            filenameId: filePathStringId,
            categoryId: categoryStringId,
            command: ExtendedDebugInfo.DebugInfoCommandId.Message);

        Debug.Print($"{debugInfo.ToBase64()}{message}");
    }
}
