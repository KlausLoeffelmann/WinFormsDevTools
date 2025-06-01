
namespace LayoutScalingIssues;

public class LayoutDebugButton : Button
{
    private static readonly DebugPadding DefaultDebugPadding = new(10);
    private Font? SmallFont { get; set; }
    private Brush? DebugBrush { get; set; }

    public LayoutDebugButton()
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
        pevent.Graphics.Clear(BackColor);

        TextFormatFlags textFormatFlags = TextFormatFlags.HorizontalCenter
            | TextFormatFlags.VerticalCenter
            | (TextFormatFlags)1073741824;

        // Draw the text first (default button behavior)
        TextRenderer.DrawText(
            pevent.Graphics, 
            Text, 
            Font, 
            ClientRectangle, 
            ForeColor, 
            textFormatFlags);

        // Cache half font size for debug info
        SmallFont ??= new(Font.FontFamily, Font.Size / 2);
        DebugBrush ??= new SolidBrush(DebugColor);

        // Draw location in upper left with debug color
        string locationText = $"({Left},{Top})";

        pevent.Graphics.DrawString(
            locationText, 
            SmallFont, 
            DebugBrush, 
            CornerMargin.Left, 
            CornerMargin.Top);
        
        // Draw size in lower right with debug color
        string sizeText = $"{Width}x{Height}";
        SizeF sizeTextSize = pevent.Graphics.MeasureString(sizeText, SmallFont);

        pevent.Graphics.DrawString(sizeText, SmallFont, DebugBrush, 
            Width - sizeTextSize.Width - CornerMargin.Right, 
            Height - sizeTextSize.Height - CornerMargin.Bottom);
    }

    public DebugPadding CornerMargin { get; set; } = DefaultDebugPadding;
    private bool ShouldSerializeCornerMargin() => CornerMargin != DefaultDebugPadding;
    private void ResetCornerMargin() => CornerMargin = DefaultDebugPadding;

    public Color DebugColor { get; set; } = Color.Red;
    private bool ShouldSerializeDebugColor() => DebugColor != Color.Red;
    private void ResetDebugColor() => DebugColor = Color.Red;
}
