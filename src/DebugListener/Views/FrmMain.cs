using CommunityToolkit.WinForms.ComponentModel;
using CommunityToolkit.WinForms.Extensions;
using DebugListener.ViewModels;

using Timer = System.Threading.Timer;

namespace DebugListener;

public partial class FrmMain : Form
{
    private readonly IUserSettingsService _settingsService;
    private readonly VmMain _vmMain;
    private Timer? _clockTimer;

    public FrmMain()
    {
        InitializeComponent();
        _settingsService = WinFormsUserSettingsService.CreateAndLoad();

        _vmMain = _settingsService.GetInstance<VmMain>(
            nameof(VmMain),
            new VmMain());

        DataContext = _vmMain;

        _vmMain.MenuOptions.PropertyChanged += (s, e) =>
        {
            // We could also bind those properties directly to the view.
            switch (e.PropertyName)
            {
                case nameof(VmMenuOptionSettings.ColorProcesses):
                    _logView.ColorProcesses = _vmMain.MenuOptions.ColorProcesses;
                    break;
                case nameof(VmMenuOptionSettings.OnlyShowExtendedDebugInfo):
                    _logView.OnlyShowExtendedDebugInfo = _vmMain.MenuOptions.OnlyShowExtendedDebugInfo;
                    break;
                case nameof(VmMenuOptionSettings.LeaveSpaceBetweenProcesses):
                    _logView.LeaveSpaceBetweenProcesses = _vmMain.MenuOptions.LeaveSpaceBetweenProcesses;
                    break;
            }
        };
    }

    private async void UpdateClock(object? state)
        => await InvokeAsync(() => _tslDateTime.Text = DateTime.Now.ToString("ddd, yyyy-mm-dd HH:mm:ss-f"));

    protected async override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        _clockTimer = new Timer(UpdateClock, null, 0, 100);
        _tslStartTime.Text = $"Start: {DateTime.Now.ToString(_vmMain.Options.DateTimeFormatString)}";
        _tslDuration.Text = $"Duration: {TimeSpan.Zero.ToString(_vmMain.Options.TimeSpanFormatString)}";
        _tslProcessTime.Text = $"Process Time: {TimeSpan.Zero.ToString(_vmMain.Options.TimeSpanFormatString)}";
        _tslThreadTime.Text = $"Thread Time: {TimeSpan.Zero.ToString(_vmMain.Options.TimeSpanFormatString)}";

        _logView.TimeSpanFormatString = _vmMain.Options.TimeSpanFormatString;
        _logView.DateTimeFormatString = _vmMain.Options.DateTimeFormatString;

        var bounds = _settingsService.GetInstance(
            "bounds",
            this.CenterOnScreen(
                horizontalFillGrade: 80,
                verticalFillGrade: 80));

        Bounds = bounds;

        await _logView.StartListeningAsync();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        _menuBindingSource.DataSource = _vmMain.MenuOptions;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        _settingsService.SetInstance("bounds", Bounds);
        _settingsService.SetInstance(nameof(VmMain), _vmMain);
        _settingsService.Save();
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

    private void LogViewSelectionChanged(object sender, DebugListener.Views.LogViewSelectionChangedEventArgs e)
    {
        string format = _vmMain.Options.TimeSpanFormatString;

        if (e.SelectedMessages.Length == 0)
        {
            return;
        }

        if (e.SelectedMessages.Length == 1)
        {
            _tslProcessTime.Text = $"Process Time: {e.TotalProcessTime?.ToString(format)} ";
            _tslProcessTime.Visible = true;
            _tslThreadTime.Text = $"Thread Time: {e.TotalThreadTime?.ToString(format)} ";
            _tslThreadTime.Visible = true;
            _tslDuration.Text = $"Duration: {e.TotalDuration?.ToString(format)} ";
            _tslDuration.Visible = true;
        }
        else
        {
            _tslProcessTime.Visible = false;
            _tslThreadTime.Visible = false;
            _tslDuration.Text = $"Duration:{e.TotalDuration?.ToString(format)} ";
            _tslDuration.Visible = true;
        }
    }
}
