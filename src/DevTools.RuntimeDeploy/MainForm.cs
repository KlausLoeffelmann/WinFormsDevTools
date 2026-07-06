using DevTools.RuntimeDeploy.Engine.Domain;
using DevTools.RuntimeDeploy.Infrastructure;
using DevTools.RuntimeDeploy.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using WarpToolkit.ComponentModel;
using WarpToolkit.Desktop.AppServices;
using static DevTools.RuntimeDeploy.Engine.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy;

public partial class MainForm : Form, IServiceProvider
{
    private readonly IServiceProvider? _serviceProvider;
    private readonly ILogger<MainForm>? _logger;
    private readonly IUserSettingsService? _userSettings;
    private readonly IWinFormsAppExceptionService? _exceptionService;
    private readonly RuntimeDeployStatusService? _statusService;
    private readonly RuntimeDeploySettingsService? _settingsService;
    private readonly DeployRuntimeView? _deployRuntimeView;
    private readonly OverView? _overView;
    private bool _allowClose;
    private bool _tabsInitialized;

    public MainForm()
    {
        InitializeComponent();
    }

    public MainForm(
        IServiceProvider serviceProvider,
        ILogger<MainForm> logger,
        IUserSettingsService userSettings,
        IWinFormsAppExceptionService exceptionService,
        RuntimeDeployStatusService statusService,
        RuntimeDeploySettingsService settingsService,
        OverView overView,
        DeployRuntimeView deployRuntimeView) : this()
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        _serviceProvider = new DeferredServiceProvider(serviceProvider);
        _logger = logger;
        _userSettings = userSettings;
        _exceptionService = exceptionService;
        _statusService = statusService;
        _settingsService = settingsService;
        _overView = overView;
        _deployRuntimeView = deployRuntimeView;

        _exceptionService.RegisterExceptionHandler(OnApplicationThreadException);
        _statusService.StatusReported += StatusService_StatusReported;
    }

    object? IServiceProvider.GetService(Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        if (_serviceProvider is null)
        {
            throw new InvalidOperationException(
                "MainForm was constructed without a DI service provider. Resolve it from WinFormsApplication instead of calling new MainForm().");
        }

        return _serviceProvider.GetService(serviceType)
            ?? throw new InvalidOperationException($"Service of type '{serviceType.Name}' is not registered.");
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Assign the configured fonts to the Form (and the non-ambient MenuStrip /
        // StatusStrip) BEFORE restoring the saved bounds, so the explicit bounds win
        // over any auto-scale resize triggered by the font change.
        ApplyFontsFromSettings();
        LoadSaveWindowPositionsSetting();
        RestoreSavedBounds();
        InitializeSdkTargets();
        InitializeTabs();
    }

    private void ApplyFontsFromSettings()
    {
        if (_settingsService is null)
        {
            return;
        }

        Font uiFont = _settingsService.UiFont;

        Font = uiFont;

        // MenuStrip and StatusStrip do not inherit the Form's ambient Font, so they
        // must be assigned explicitly.
        _menuStrip.Font = uiFont;
        _statusStrip.Font = uiFont;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        if (!_allowClose && e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
 
            InvokeAsync(() => MinimizeToTray());
            return;
        }

        SaveBounds();
        _notifyIcon.Visible = false;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _statusService?.StatusReported -= StatusService_StatusReported;

        base.OnFormClosed(e);
    }

    private void InitializeSdkTargets()
    {
        SdkFolders = FrameworkInfo.GetDotNetDesktopSdk(false);

        if (SdkFolders is null)
        {
            SdkTargets = [];
            ShowStatus("No .NET Desktop SDK folders were found.", Color.DarkOrange);
            return;
        }

        SdkTargets = [.. SdkFolders
            .Values
            .Select(item => new TargetFrameworkTargetItem()
            {
                Name = item.Name,
                PathFullName = item.FullName,
                Directory = item
            })];
    }

    private void InitializeTabs()
    {
        if (_tabsInitialized || _overView is null || _deployRuntimeView is null)
        {
            return;
        }

        _tabControl.AddTab("Overview", _overView);
        _tabControl.AddTab("Deploy Runtime", _deployRuntimeView);
        _tabsInitialized = true;
    }

    private void OnApplicationThreadException(object? sender, ThreadExceptionEventArgs e)
        => ReportException(e.Exception);

    internal void ReportException(Exception exception)
    {
        _logger?.LogError(exception, "Unhandled RuntimeDeploy exception");
        ShowStatus(exception.Message, Color.DarkRed);
    }

    private void StatusService_StatusReported(object? sender, RuntimeDeployStatusEventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(() => StatusService_StatusReported(sender, e));
            return;
        }

        if (e.Exception is not null)
        {
            _logger?.LogError(e.Exception, "RuntimeDeploy exception");
        }

        ShowStatus(e.Message, e.ForegroundColor);
    }

    private void ShowStatus(string message, Color foregroundColor)
    {
        _statusMessageLabel.Text = message;
        _statusMessageLabel.ForeColor = foregroundColor;
    }

    private void LoadSaveWindowPositionsSetting()
    {
        bool savePositions = _userSettings?.Get(SettingKeys.SaveWindowPositions, true) ?? true;
        _saveWindowPositionsMenuItem.Checked = savePositions;
    }

    private void RestoreSavedBounds()
    {
        if (_userSettings is null)
        {
            return;
        }

        Rectangle workingArea = Screen.FromControl(this).WorkingArea;

        Rectangle defaultBounds = new(
            workingArea.Left + ((workingArea.Width - Width) / 2),
            workingArea.Top + ((workingArea.Height - Height) / 2),
            Width,
            Height);

        if (!_saveWindowPositionsMenuItem.Checked)
        {
            StartPosition = FormStartPosition.Manual;
            Bounds = defaultBounds;
            return;
        }

        Rectangle bounds = _userSettings.Get(SettingKeys.MainFormBounds, defaultBounds);

        if (!Screen.AllScreens.Any(screen => screen.WorkingArea.IntersectsWith(bounds)))
        {
            bounds = defaultBounds;
        }

        StartPosition = FormStartPosition.Manual;
        Bounds = bounds;
    }

    private void SaveBounds()
    {
        if (_userSettings is null || !_saveWindowPositionsMenuItem.Checked)
        {
            return;
        }

        Rectangle bounds = WindowState == FormWindowState.Normal
            ? Bounds
            : RestoreBounds;

        _userSettings.Set(SettingKeys.MainFormBounds, bounds);
        _userSettings.Flush();
    }

    private void SaveWindowPositionsMenuItem_Click(object sender, EventArgs e)
    {
        if (_userSettings is null)
        {
            return;
        }

        _userSettings.Set(SettingKeys.SaveWindowPositions, _saveWindowPositionsMenuItem.Checked);
        _userSettings.Flush();
    }

    private void MinimizeToTray()
    {
        Hide();
        _notifyIcon.Visible = true;

        // Recreted handle, so it should be called after Hide() to avoid
        // issues with taskbar pinning and focus stealing.
        ShowInTaskbar = false;

        WindowState = FormWindowState.Minimized;
    }

    private void RestoreFromTray()
    {
        ShowInTaskbar = true;
        Show();

        WindowState = FormWindowState.Normal;
        _notifyIcon.Visible = false;
        Activate();

        ShowStatus("Ready.", SystemColors.ControlText);
    }

    private void RestoreMenuItem_Click(object sender, EventArgs e)
        => RestoreFromTray();

    private void OptionsMenuItem_Click(object sender, EventArgs e)
        => ShowOptionsDialog();

    private void CreateRuntimePatcherMenuItem_Click(object sender, EventArgs e)
    {
        if (_serviceProvider is null)
        {
            return;
        }

        using CreateRuntimePatcherForm form = _serviceProvider.GetRequiredService<CreateRuntimePatcherForm>();
        form.ShowDialog(this);
    }

    private void RestoreBackupMenuItem_Click(object sender, EventArgs e)
    {
        if (_serviceProvider is null)
        {
            return;
        }

        using RestoreBackupForm form = _serviceProvider.GetRequiredService<RestoreBackupForm>();
        form.ShowDialog(this);
    }

    private void QuitMenuItem_Click(object sender, EventArgs e)
    {
        _allowClose = true;
        Close();
    }

    private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        => RestoreFromTray();

    private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _trayContextMenu.Show(Cursor.Position);
        }
    }

    private void ShowOptionsDialog()
    {
        if (_serviceProvider is null)
        {
            return;
        }

        using OptionsForm optionsForm = _serviceProvider.GetRequiredService<OptionsForm>();
        DialogResult result = optionsForm.ShowDialog(this);

        // Fonts are persisted immediately when changed in the dialog, so re-apply
        // them regardless of how the dialog was dismissed.
        ApplyFontsFromSettings();

        if (result == DialogResult.OK)
        {
            _overView?.RefreshFromSettings();
            _deployRuntimeView?.RefreshFromSettings();
            _statusService?.ReportInfo("Options saved.");
        }
    }

    /// <summary>
    ///  Per-user settings service, resolved from the DI container set up
    ///  by <c>WinFormsApplication.CreateBuilder</c> in <c>Program.Main</c>.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IUserSettingsService UserSettings
        => _userSettings ?? throw new InvalidOperationException("MainForm was not resolved through DI.");

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Dictionary<string, DirectoryInfo>? SdkFolders { get; private set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal TargetFrameworkTargetItem[] SdkTargets { get; private set; } = null!;

    private sealed class DeferredServiceProvider(IServiceProvider serviceProvider) : IServiceProvider
    {
        public object? GetService(Type serviceType)
            => serviceProvider.GetService(serviceType);
    }
}
