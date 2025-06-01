using System.Drawing.Drawing2D;

namespace LayoutScalingIssues;

public class DebugPanel : Panel
{
    private static readonly DebugPadding DefaultDebugPadding = new(10);

    private Pen? _borderPen;
    private Font? SmallFont { get; set; }
    private Brush? DebugBrush { get; set; }

    public DebugPanel()
    {
    }

    protected override void OnLocationChanged(EventArgs e)
    {
        base.OnLocationChanged(e);
        Invalidate();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        // Cache half font size for debug info
        SmallFont ??= new(Font.FontFamily, Font.Size / 2);
        DebugBrush ??= new SolidBrush(DebugColor);

        _borderPen ??= new Pen(BorderColor, BorderWidth)
        {
            Alignment = PenAlignment.Inset
        };

        // Set the smoothing mode for better rendering
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // call the renderer
        DebugControlRenderer.DrawDebugContent(
            pevent.Graphics,
            Text,
            CornerMargin,
            ClientRectangle,
            Bounds,
            BackColor,
            ForeColor,
            _borderPen,
            DebugBrush,
            Font,
            SmallFont);
    }

    public DebugPadding CornerMargin { get; set; } = DefaultDebugPadding;
    private bool ShouldSerializeCornerMargin() => CornerMargin != DefaultDebugPadding;
    private void ResetCornerMargin() => CornerMargin = DefaultDebugPadding;

    public Color DebugColor { get; set; } = Color.Red;
    private bool ShouldSerializeDebugColor() => DebugColor != Color.Red;
    private void ResetDebugColor() => DebugColor = Color.Red;

    public Color BorderColor { get; set; } = Color.Blue;
    private bool ShouldSerializeBorderColor() => BorderColor != Color.Blue;
    private void ResetBorderColor() => BorderColor = Color.Blue;

    public int BorderWidth { get; set; } = 2;
    private bool ShouldSerializeBorderWidth() => BorderWidth != 2;
    private void ResetBorderWidth() => BorderWidth = 2;
}
