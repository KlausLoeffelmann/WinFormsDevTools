namespace LayoutScalingIssues
{
    public record struct DebugPadding
    {
        public DebugPadding(int all)
        {
            Left = all;
            Top = all;
            Right = all;
            Bottom = all;
        }

        public DebugPadding(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static DebugPadding Empty => new DebugPadding(0, 0, 0, 0);

        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }
}