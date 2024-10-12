using BenchmarkDotNet.Attributes;
using DevTools.Libs.DebugListener;
using System.Diagnostics;
using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DevTools.Benchmarks;

public class DebugInfoTestClass
{
    [Benchmark]
    public void TestExtendedDebugInfo()
    {
        int processId = Environment.ProcessId;
        int threadId = Environment.CurrentManagedThreadId;

        //ExtendedDebugInfo.AddOrGetProcessStringIndex(processId, "Category", out var categoryId);
        
        //var debugInfo = new ExtendedDebugInfo(
        //    timestamp: TimeSpan.FromMilliseconds(1234567890),
        //    processId: (ushort)processId,
        //    threadId: (ushort)threadId,
        //    methodId: 0,
        //    filenameId: 0,
        //    categoryId: categoryId,
        //    command: ExtendedDebugInfo.DebugInfoCommandId.CategoryDefinition);

        //Trace.WriteLine($"{debugInfo.ToBase64()}Category");

        ExtendedDebugInfo.AddOrGetProcessStringIndex(
            processId,
            "d:\\git\\someProject\\someFolder\\someClassName.cs",
            out var fileNameId);

        var debugInfo = new ExtendedDebugInfo(
            timestamp: TimeSpan.FromMilliseconds(1234567890),
            processId: (ushort)processId,
            threadId: (ushort)threadId,
            methodId: 0,
            filenameId: fileNameId,
            categoryId: 0,
            command: ExtendedDebugInfo.DebugInfoCommandId.FileNameDefinition);

        Trace.WriteLine($"{debugInfo.ToBase64()}d:\\git\\someProject\\someFolder\\someClassName.cs");

        ExtendedDebugInfo.AddOrGetProcessStringIndex(processId, "SomeMethod", out var methodId);

        debugInfo = new ExtendedDebugInfo(
            timestamp: TimeSpan.FromMilliseconds(1234567890),
            processId: (ushort)processId,
            threadId: (ushort)threadId,
            methodId: methodId,
            filenameId: 0,
            categoryId: 0,
            command: ExtendedDebugInfo.DebugInfoCommandId.MethodNameDefinition);

        Trace.WriteLine($"{debugInfo.ToBase64()}SomeMethod");

        debugInfo = new ExtendedDebugInfo(
            timestamp: TimeSpan.FromMilliseconds(1234567890),
            processId: (ushort)processId,
            threadId: (ushort)threadId,
            methodId: methodId,
            filenameId: fileNameId,
            categoryId: 0,
            command: ExtendedDebugInfo.DebugInfoCommandId.Message);

        Trace.WriteLine($"{debugInfo.ToBase64()}Message for the debug info with all other data combined.");
    }
}
