using BenchmarkDotNet.Attributes;
using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DevTools.Benchmarks;

public class DebugInfoTestClass
{
    private static readonly List<string> s_sampleDebugStrings =
    [
        "This is a way longer test message that is used for benchmarking purposes",
        "This is another way longer test message that is used for benchmarking purposes",
        "This is a third way longer test message that is used for benchmarking purposes",
        "This is a fourth way longer test message that is used for benchmarking purposes",
        "A fifth way longer test message that is used for benchmarking purposes",
        "This is a sixth way longer test message that is used for benchmarking purposes",
        "This is a seventh way longer test message that is used for benchmarking purposes",
        "An eighth way longer test message that is used for benchmarking purposes",
        "This is a ninth way longer test message that is used for benchmarking purposes",
        "This is a tenth way longer test message that is used for benchmarking purposes"
    ];

    private static readonly List<string> s_sampleDebugCategories =
    [
        "Benchmark",
        "Performance",
        "Debug",
        "Information",
        "Warning",
        "Error",
        "Critical",
        "Fatal",
        "Trace",
        "Verbose"
    ];

    private static readonly Random s_random = new();

    // | Method                                    | Mean      | Error     | StdDev    | Median    |
    // |------------------------------------------ |----------:|----------:|----------:|----------:|
    // | BenchmarkSimpleDebugMessage00             | 0.5776 ns | 0.0130 ns | 0.0380 ns | 0.5740 ns |
    // | BenchmarkSimpleDebugMessage01             | 0.0271 ns | 0.0005 ns | 0.0015 ns | 0.0270 ns |
    // | BenchmarkSimpleDebugMessage02             | 0.0140 ns | 0.0003 ns | 0.0007 ns | 0.0140 ns |
    // | BenchmarkSimpleDebugMessageWithCategory00 | 0.6601 ns | 0.0248 ns | 0.0666 ns | 0.6383 ns |
    // | BenchmarkSimpleDebugMessageWithCategory01 | 0.0371 ns | 0.0013 ns | 0.0038 ns | 0.0365 ns |
    // | BenchmarkSimpleDebugMessageWithCategory02 | 0.0165 ns | 0.0003 ns | 0.0008 ns | 0.0165 ns |

    [Benchmark(OperationsPerInvoke = 500)]
    public void BenchmarkSimpleDebugMessage00()
    {
        TPrint(s_sampleDebugStrings[s_random.Next(0, s_sampleDebugStrings.Count)]);
    }

    [Benchmark(OperationsPerInvoke = 9999)]
    public void BenchmarkSimpleDebugMessage01()
    {
        TPrint(s_sampleDebugStrings[s_random.Next(0, s_sampleDebugStrings.Count)]);
    }

    [Benchmark(OperationsPerInvoke = 20000)]
    public void BenchmarkSimpleDebugMessage02()
    {
        TPrint(s_sampleDebugStrings[s_random.Next(0, s_sampleDebugStrings.Count)]);
    }


    [Benchmark(OperationsPerInvoke = 500)]
    public void BenchmarkSimpleDebugMessageWithCategory00()
    {
        TPrint(
            s_sampleDebugStrings[s_random.Next(0, s_sampleDebugStrings.Count)],
            s_sampleDebugCategories[s_random.Next(0, s_sampleDebugCategories.Count)]);
    }

    [Benchmark(OperationsPerInvoke = 9999)]
    public void BenchmarkSimpleDebugMessageWithCategory01()
    {
        TPrint(
            s_sampleDebugStrings[s_random.Next(0, s_sampleDebugStrings.Count)],
            s_sampleDebugCategories[s_random.Next(0, s_sampleDebugCategories.Count)]);
    }

    [Benchmark(OperationsPerInvoke = 20000)]
    public void BenchmarkSimpleDebugMessageWithCategory02()
    {
        TPrint(
            s_sampleDebugStrings[s_random.Next(0, s_sampleDebugStrings.Count)],
            s_sampleDebugCategories[s_random.Next(0, s_sampleDebugCategories.Count)]);
    }
}
