using BenchmarkDotNet.Attributes;
using DevTools.Libs.DebugListener;

namespace App.Benchmarks;

public class DebugInfoTestClass
{
    [Benchmark]
    public void TestExtendedDebugInfo()
    {
        ExtendedDebugInfo debugInfo = new();
    }
}
