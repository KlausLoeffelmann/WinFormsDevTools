using BenchmarkDotNet.Attributes;
using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DevTools.Benchmarks;

public class DebugInfoTestClass
{
    [Benchmark]
    public void BenchmarkSimpleDebugMessage()
    {
        TPrint("Test message");
    }

    [Benchmark]
    public void BenchmarkSimpleDebugMessageWithCategory()
    {
        TPrint("Test message", category: "Benchmark");
    }
}
