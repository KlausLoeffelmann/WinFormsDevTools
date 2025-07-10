using System.ComponentModel;

namespace GDIPlus_Benchmark;

/// <summary>
/// Benchmark class for measuring line drawing performance using GDI+
/// </summary>
[Description("GDI+ Line Drawing: Constant Pen allocation.")]
public class LineDrawingAllocatePens : FigureBase
{
    private static readonly Random _random= new();

    public LineDrawingAllocatePens()
    {
    }

    /// <summary>
    /// Draws random lines within the specified bounds
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

            // Generate random color
            var color = Color.FromArgb(
                _random.Next(256), // R
                _random.Next(256), // G
                _random.Next(256)  // B
            );

            using var pen = new Pen(color);
            graphics.DrawLine(pen, startPoint, endPoint);
        }
    }
}
