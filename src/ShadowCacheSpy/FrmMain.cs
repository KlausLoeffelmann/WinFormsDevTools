using CommunityToolkit.WinForms.Controls;
using CommunityToolkit.WinForms.Extensions;

namespace ShadowCacheSpy;

public partial class FrmMain : Form
{
    private const int WriteLineTestSamples = 1000;
    private readonly WinFormsUserSettingsService _settingsService;
    private AppSettings _appSettings;
    private DateTime _lastEventTime;

    public FrmMain()
    {
        InitializeComponent();

        _settingsService = new();
        _settingsService.Load();
        _appSettings = _settingsService.GetInstance("appSettings", new AppSettings());

        _fileSystemWatcher.EnableRaisingEvents = true;

        _fileSystemWatcher.Created += FileSystemWatcher_Changed;
        _fileSystemWatcher.Changed += FileSystemWatcher_Changed;
        _fileSystemWatcher.Deleted += FileSystemWatcher_Changed;
        _fileSystemWatcher.Renamed += FileSystemWatcher_Changed;
    }

    protected async override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        _tslWatchPath.Text = _appSettings.WatchFolder ?? "No folder has been setup to being watched.";

        var bounds = _settingsService.GetInstance(
            "bounds",
            this.CenterOnScreen(
                horizontalFillGrade: 80,
                verticalFillGrade: 80));

        Bounds = bounds;

        await _console.WriteLineAsync("Hello, fellow Developer!", size: FontSize.Larger, style: CustomFontStyle.Bold);
        await _console.WriteLineAsync();
        await _console.WriteLineAsync($"Today is {DateTime.Now:dddd, MMMM dd yyyy}.");
        await _console.WriteLineAsync($"It is now {DateTime.Now: HH:mm:ss} in time zone {DateTimeOffset.Now:zz}.");
        await _console.WriteLineAsync();

        if (_appSettings.WatchFolder is not null)
        {
            await _console.WriteLineAsync($"Watching folder: {_appSettings.WatchFolder}");
            await _console.WriteLineAsync();
            _fileSystemWatcher.Path = _appSettings.WatchFolder;
            _fileSystemWatcher.IncludeSubdirectories = true;
        }
        else
        {
            await _console.WriteLineAsync("No folder has been setup to being watched.");
            await _console.WriteLineAsync("Please choose 'File' and 'Options' to set the watch folder.");
            await _console.WriteLineAsync();
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        _settingsService.SetInstance("appSettings", _appSettings);
        _settingsService.SetInstance("bounds", this.GetRestorableBounds());
        _settingsService.Save();
    }

    private void Options_Click(object sender, EventArgs e)
    {
        var optionsDialog = new FrmOptions();
        var result = optionsDialog.ShowDialog(ref _appSettings!);

        if (result.FormDialogResult == DialogResult.OK)
        {
            _appSettings = result.Result!;
            _settingsService.Save();
            _fileSystemWatcher.Path = _appSettings.WatchFolder!;
            _fileSystemWatcher.IncludeSubdirectories = true;
        }
    }

    private async void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
    {
        // If the last entry was older than 10 seconds, we insert a blank line
        // to separate the events:
        if (DateTime.Now - _lastEventTime > TimeSpan.FromSeconds(10))
            await _console.WriteLineAsync();

        Color textColor = e.ChangeType switch
        {
            WatcherChangeTypes.Created => Color.Green,
            WatcherChangeTypes.Changed => Color.LightBlue,
            WatcherChangeTypes.Deleted => Color.Red,
            WatcherChangeTypes.Renamed => Color.Purple,
            _ => Color.Black
        };

        await _console.WriteLineAsync(
            $"[{DateTime.Now:HH:mm:ss fff}]: {e.ChangeType} {e.Name}",
            textColor: textColor);

        _lastEventTime = DateTime.Now;
    }
}
