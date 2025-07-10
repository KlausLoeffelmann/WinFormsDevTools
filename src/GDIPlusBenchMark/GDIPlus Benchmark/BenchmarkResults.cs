namespace GDIPlus_Benchmark;

public class BenchmarkResults
{
    public int RunNumber { get; set; }
    public int FigureRuns { get; set; }
    public double Benchmark1Result { get; set; }
    public double Benchmark1Average { get; set; }
    public double? Benchmark2Result { get; set; } // Nullable for "None"
    public double? Benchmark2Average { get; set; }
    public string Benchmark1Name { get; set; } = string.Empty;
    public string? Benchmark2Name { get; set; }

    public BenchmarkResults(
        int runNumber,
        int figureRuns,
        double benchmark1Result,
        double benchmark1Average,
        double? benchmark2Result,
        double? benchmark2Average,
        string benchmark1Name,
        string? benchmark2Name)
    {
        RunNumber = runNumber;
        FigureRuns = figureRuns;
        Benchmark1Result = benchmark1Result;
        Benchmark1Average = benchmark1Average;
        Benchmark2Result = benchmark2Result;
        Benchmark2Average = benchmark2Average;
        Benchmark1Name = benchmark1Name;
        Benchmark2Name = benchmark2Name;
    }
}