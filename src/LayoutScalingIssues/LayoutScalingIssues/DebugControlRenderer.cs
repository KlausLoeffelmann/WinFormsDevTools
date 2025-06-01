namespace LayoutScalingIssues;

public static class DebugControlRenderer
{
    public static void DrawDebugContent(
        Graphics graphics, 
        string Text,
        DebugPadding cornerMargin,
        Rectangle bounds,
        Rectangle? parentBounds,
        Color backColor, 
        Color foreColor, 
        Pen pen, 
        Brush brush, 
        Font standardFont, 
        Font smallFont)
    {
        graphics.Clear(backColor);

        TextFormatFlags textFormatFlags = TextFormatFlags.HorizontalCenter
            | TextFormatFlags.VerticalCenter
            | (TextFormatFlags)1073741824;

        // Draw the text first (default button behavior)
        TextRenderer.DrawText(
            graphics,
            Text,
            standardFont,
            bounds,
            foreColor,
            textFormatFlags);

        parentBounds ??= Rectangle.Empty;

        // Draw location in upper left with debug color
        string locationText = $"({parentBounds?.Left},{parentBounds?.Top})";

        graphics.DrawString(
            locationText,
            smallFont,
            brush,
            cornerMargin.Left,
            cornerMargin.Top);

        // Draw size in lower right with debug color
        string sizeText = $"{bounds.Width}x{bounds.Height}";
        SizeF sizeTextSize = graphics.MeasureString(sizeText, smallFont);

        graphics.DrawString(sizeText, smallFont, brush,
            bounds.Width - sizeTextSize.Width - cornerMargin.Right,
            bounds.Height - sizeTextSize.Height - cornerMargin.Bottom);

        graphics.DrawRectangle(pen, bounds);
    }
}
