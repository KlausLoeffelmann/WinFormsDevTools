using System.ComponentModel;

namespace GDIPlus_Benchmark;

/// <summary>
/// Benchmark class for measuring rectangle drawing performance using cached Pens/Brushes
/// </summary>
[Description("GDI+ Rectangle Drawing: Only using cached Pens/Brushes.")]
public class RectangleDrawingCaches : FigureBase
{
    private static readonly Random _random = new();

    // Allocate 10 static Pen and 10 static Brush objects with different colors
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
    private static readonly SolidBrush[] _brushes = new SolidBrush[]
    {
        new SolidBrush(Color.Red),
        new SolidBrush(Color.Green),
        new SolidBrush(Color.Blue),
        new SolidBrush(Color.Yellow),
        new SolidBrush(Color.Magenta),
        new SolidBrush(Color.Cyan),
        new SolidBrush(Color.Orange),
        new SolidBrush(Color.Purple),
        new SolidBrush(Color.Brown),
        new SolidBrush(Color.Black)
    };

    public RectangleDrawingCaches() { }

    /// <summary>
    /// Draws random rectangles (outlined, filled, and filled+outlined) within the specified bounds using cached Pens/Brushes
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

            // Select a random Pen and Brush from the cached arrays
            var pen = _pens[_random.Next(_pens.Length)];
            var brush = _brushes[_random.Next(_brushes.Length)];

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
