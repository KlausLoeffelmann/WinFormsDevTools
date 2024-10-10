using System.Diagnostics;

namespace DebugListener
{
    public partial class FrmStopwatch : Form
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private Bitmap? _backBuffer;

        public FrmStopwatch()
        {
            InitializeComponent();
            _cancellationTokenSource = new();
        }

        private void EnsureBitmapSize()
        {
            if (_backBuffer == null || _backBuffer.Width != Width || _backBuffer.Height != Height)
            {
                _backBuffer?.Dispose();
                _backBuffer = new(Width, Height);
            }
        }

        protected async override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            nint handle = this.Handle;

            await Task.Run(() =>
            {
                // Get the graphics object for this:
                Graphics gForm = Graphics.FromHwnd(handle);

                Font font = new("Cascadia Code", 72, FontStyle.Regular, GraphicsUnit.Pixel);

                // Get a ForeColor brush:
                Brush foreColorBrush = new SolidBrush(ForeColor);

                Stopwatch stopwatch = new();
                stopwatch.Start();
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    EnsureBitmapSize();
                    using (Graphics backBufferGraphics = Graphics.FromImage(_backBuffer!))
                    {
                        backBufferGraphics.Clear(BackColor);
                        string renderString = $"{stopwatch.Elapsed:hh\\:mm\\:ss\\-f}";

                        // Draw the text centered:
                        SizeF textSize = backBufferGraphics.MeasureString(renderString, font);
                        backBufferGraphics.DrawString(
                            s: renderString,
                            font: font,
                            brush: foreColorBrush,
                            x: (Width - textSize.Width) / 2,
                            y: (Height - textSize.Height) / 2);
                    }

                    gForm.DrawImageUnscaled(_backBuffer!, 0, 0);

                    Thread.Sleep(50);
                }
            });
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _cancellationTokenSource.Cancel();
        }
    }
}
