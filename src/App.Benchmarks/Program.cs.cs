namespace DevTools.Benchmarks;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        BenchmarkDotNet.Running.BenchmarkRunner.Run<DebugInfoTestClass>();
        Console.ReadLine();
    }
}
