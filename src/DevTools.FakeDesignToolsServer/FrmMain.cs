using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DevTools.FakeDesignToolsServer;

public partial class FrmMain : Form
{
    private readonly System.Threading.Timer _timer;

    public FrmMain()
    {
        InitializeComponent();
        _timer = new System.Threading.Timer(new TimerCallback(KeepAliveNotifier), null, 0, 5000);

        _tsmQuitProcess.Click += (sender, e) =>
        {
            PerfDbgTrace("Fake DesignToolsServer is quitting.", markImportant: true);
            _timer.Dispose();
            Application.Exit();
        };
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Visible = false;
        PerfDbgTrace("Initialized Fake DesignToolsServer process.", markImportant: true);
    }

    private void KeepAliveNotifier(object? state)
    {
        PerfDbgTrace("Fake DesignToolsServer is still running.", markImportant: true);
    }
}
