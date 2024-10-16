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
    }

    private async void UpdateClock(object? state)
        => await InvokeAsync(() => _tslDateTime.Text = DateTime.Now.ToString("ddd, yyyy-mm-dd HH:mm:ss-f"));

    protected async override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        DataContext = _vmMain;

        _clockTimer = new Timer(UpdateClock, null, 0, 100);
        _tslItemNo.Text = $"Start: {DateTime.Now.ToString(_vmMain.Options.DateTimeFormatString)}";
        _tslDurationFromStart.Text = $"Duration: {TimeSpan.Zero.ToString(_vmMain.Options.TimeSpanFormatString)}";
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

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        _clockTimer?.Dispose();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        _settingsService.SetInstance("bounds", Bounds);
        _settingsService.SetInstance(nameof(VmMain), _vmMain);
        _settingsService.Save();
    }

    private void ClearLogs_Click(object sender, EventArgs e)
    {
        _logView.ClearLogs();
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
            _tslItemNo.Text = $"Item #: {e.RowIndex} ";
            _tslItemNo.Visible = true;
            _tslStarttimeDuration.Text = $"{e.Timestamp:HH:mm:ss-fffffff} ({LongFormattedTimeSpan(e.Duration)})";
            _tslStarttimeDuration.Visible = true;
            _tslProcessTime.Text = $"Process Time: {FormattedTimeSpan(e.TotalProcessTime)} ";
            _tslProcessTime.Visible = true;
            _tslThreadTime.Text = $"Thread Time: {FormattedTimeSpan(e.TotalThreadTime)} ";
            _tslThreadTime.Visible = true;
            _tslDurationFromStart.Text = $"Duration: {FormattedTimeSpan(e.TotalDuration)} ";
            _tslDurationFromStart.Visible = true;
        }
        else
        {
            _tslItemNo.Visible = false;
            _tslStarttimeDuration.Visible = false;
            _tslProcessTime.Visible = false;
            _tslThreadTime.Visible = false;
            _tslDurationFromStart.Text = $"Duration:{e.TotalDuration?.ToString(format)} ";
            _tslDurationFromStart.Visible = true;
        }

        static string FormattedTimeSpan(TimeSpan? timespan)
        {
            if (timespan == null)
            {
                return "N/A";
            }

            return $"{timespan.Value.TotalSeconds:#,##0.000\\'000}";
        }

        static string LongFormattedTimeSpan(TimeSpan? timespan)
        {
            if (timespan == null)
            {
                return "N/A";
            }

            return $"{timespan.Value.TotalSeconds:#,##0.000\\'000\\'000}";
        }
    }

    private object FormattedTimeSpan(object timestamp)
    {
        throw new NotImplementedException();
    }

    private void SaveLog_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true,

            // Filename is 'dbglst-yyyy-mm-dd-HH-mm.csv'
            FileName = $"dbglst-{DateTime.Now:yyyy-mm-dd-HH-mm}.csv"
        };
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            _logView.SaveToFile(saveFileDialog.FileName);
        }
    }

    private void LoadLog_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "Text files (*.csv)|*.csv|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true
        };
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            _logView.LoadFromFile(openFileDialog.FileName);
        }
    }
}
