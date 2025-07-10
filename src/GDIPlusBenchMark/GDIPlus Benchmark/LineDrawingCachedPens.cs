using System.ComponentModel;

namespace GDIPlus_Benchmark;

/// <summary>
/// Benchmark class for measuring line drawing performance using GDI+
/// </summary>
[Description("GDI+ Line Drawing: Only using cached Pens.")]
public class LineDrawingCachedPens : FigureBase
{
    private static readonly Random _random = new();

    // Allocate 10 static Pen objects with different colors
    private static readonly Pen[] _pens = new Pen[]
    {
        new Pen(Color.Red, 1),
        new Pen(Color.Green, 1),
        new Pen(Color.Blue, 1),
        new Pen(Color.Yellow, 1),
        new Pen(Color.Magenta, 1),
        new Pen(Color.Cyan, 1),
        new Pen(Color.Orange, 1),
        new Pen(Color.Purple, 1),
        new Pen(Color.Brown, 1),
        new Pen(Color.Black, 1)
    };

    public LineDrawingCachedPens()
    {
    }

    /// <summary>
    /// Draws random lines within the specified bounds using cached Pens
    /// </summary>
    /// <param name="graphics">Graphics context to draw on</param>
    public override void Draw(Graphics graphics)
    {
        for (int i = 0; i < FigureCount; i++)
        {
            // Generate random start and end points
            var startPoint = new Point(
                _random.Next(Bounds.Left, Bounds.Right),
                _random.Next(Bounds.Top, Bounds.Bottom)
            );

            var endPoint = new Point(
                _random.Next(Bounds.Left, Bounds.Right),
                _random.Next(Bounds.Top, Bounds.Bottom)
            );

            // Select a random Pen from the cached array
            var pen = _pens[_random.Next(_pens.Length)];
            graphics.DrawLine(pen, startPoint, endPoint);
        }
    }
}
