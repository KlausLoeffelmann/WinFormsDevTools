using System.ComponentModel;

namespace GDIPlus_Benchmark;

/// <summary>
/// Benchmark class for measuring rectangle drawing performance using GDI+
/// </summary>
[Description("GDI+ Rectangle Drawing: Constant Pen/Brush allocation.")]
public class RectangleDrawingAllocates : FigureBase
{
    private static readonly Random _random = new();

    public RectangleDrawingAllocates() { }

    /// <summary>
    /// Draws random rectangles (outlined, filled, and filled+outlined) within the specified bounds
    /// </summary>
    /// <param name="graphics">Graphics context to draw on</param>
    public override void Draw(Graphics graphics)
    {
        for (int i = 0; i < FigureCount; i++)
        {
            // Generate random rectangle
            int x = _random.Next(Bounds.Left, Bounds.Right - 1);
            int y = _random.Next(Bounds.Top, Bounds.Bottom - 1);
            int w = _random.Next(5, Math.Max(6, Bounds.Right - x));
            int h = _random.Next(5, Math.Max(6, Bounds.Bottom - y));
            var rect = new Rectangle(x, y, w, h);

            // Randomly choose drawing mode: 0=outline, 1=filled, 2=filled+outline
            int mode = _random.Next(3);

            // Generate random colors
            var penColor = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
            var brushColor = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));

            using var pen = new Pen(penColor, 1);
            using var brush = new SolidBrush(brushColor);

            switch (mode)
            {
                case 0:
                    graphics.DrawRectangle(pen, rect);
                    break;
                case 1:
                    graphics.FillRectangle(brush, rect);
                    break;
                case 2:
                    graphics.FillRectangle(brush, rect);
                    graphics.DrawRectangle(pen, rect);
                    break;
            }
        }
    }
}
