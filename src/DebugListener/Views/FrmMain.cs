using CommunityToolkit.WinForms.ComponentModel;
using CommunityToolkit.WinForms.Extensions;

namespace DebugListener;

public partial class FrmMain : Form
{
    private readonly IUserSettingsService _settingsService;

    public FrmMain()
    {
        InitializeComponent();
        _settingsService = WinFormsUserSettingsService.CreateAndLoad();
    }

    protected async override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        var bounds = _settingsService.GetInstance(
            "bounds",
            this.CenterOnScreen(
                horizontalFillGrade: 80,
                verticalFillGrade: 80));

        Bounds = bounds;

        await _logView.StartListeningAsync();
    }

    private void _tsmClear_Click(object sender, EventArgs e)
    {
        _logView.Clear();
    }

    private void TsmShowStopWatch_Click(object sender, EventArgs e)
    {
        FrmStopwatch frmStopwatch = new();
        frmStopwatch.Show();
    }
}
