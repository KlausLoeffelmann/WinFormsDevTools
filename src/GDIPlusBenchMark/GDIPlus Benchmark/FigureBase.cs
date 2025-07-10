namespace GDIPlus_Benchmark;

public abstract class FigureBase
{
    public int FigureCount { get; set; }
    public Rectangle Bounds { get; set; }

    public abstract void Draw(Graphics graphics);
}
