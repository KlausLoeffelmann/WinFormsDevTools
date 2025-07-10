using System.Diagnostics;

namespace GDIPlus_Benchmark;

public class FigureRenderer<T> where T : FigureBase, new()
{
    private Stopwatch? _stopwatch;
    private readonly int _runs;
    private readonly int _figureCount;

    private readonly T _benchmarksFigure;

    public FigureRenderer(int runs, int figureCount)
    {
        _runs = runs;
        _figureCount = figureCount;

        _benchmarksFigure = new T
        {
            FigureCount = _figureCount
        };
    }

    public TimeSpan BenchmarksFigure(Graphics g, Rectangle bounds)
    {
        _stopwatch = Stopwatch.StartNew();
        _benchmarksFigure.Bounds = bounds;

        for (int i = 0; i < _runs; i++)
        {
            _benchmarksFigure.Draw(g);
        }

        _stopwatch.Stop();

        return _stopwatch.Elapsed;
    }
}
