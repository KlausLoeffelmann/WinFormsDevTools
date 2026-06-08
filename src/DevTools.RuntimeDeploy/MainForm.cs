using DevTools.RuntimeDeploy.Infrastructure;
using DevTools.RuntimeDeploy.Domain;
using DevTools.RuntimeDeploy.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Threading;
using WarpToolkit.ComponentModel;
using WarpToolkit.Desktop.AppServices;
using static DevTools.RuntimeDeploy.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy;

public partial class MainForm : Form, IServiceProvider
{
    private IServiceProvider? _serviceProvider;
    private ILogger<MainForm>? _logger;
    private IUserSettingsService? _userSettings;
    private IWinFormsAppExceptionService? _exceptionService;
    private RuntimeDeployStatusService? _statusService;
    private DeployRuntimeView? _deployRuntimeView;
    private OverView? _overView;
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
        OverView overView,
        DeployRuntimeView deployRuntimeView) : this()
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        _serviceProvider = new DeferredServiceProvider(serviceProvider);
        _logger = logger;
        _userSettings = userSettings;
        _exceptionService = exceptionService;
        _statusService = statusService;
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

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        RestoreSavedBounds();
        InitializeSdkTargets();
        InitializeTabs();
        ShowStatus("Ready.", SystemColors.ControlText);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (!_allowClose && e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            MinimizeToTray();
            return;
        }

        SaveBounds();
        _notifyIcon.Visible = false;
        base.OnFormClosing(e);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (_statusService is not null)
        {
            _statusService.StatusReported -= StatusService_StatusReported;
        }

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
        _tabControl.TabChanged += TabControl_TabChanged;
        _tabsInitialized = true;
    }

    private void TabControl_TabChanged(object? sender, EventArgs e)
    {
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
        if (_userSettings is null)
        {
            return;
        }

        Rectangle bounds = WindowState == FormWindowState.Normal
            ? Bounds
            : RestoreBounds;

        _userSettings.Set(SettingKeys.MainFormBounds, bounds);
        _userSettings.Flush();
    }

    private void MinimizeToTray()
    {
        _notifyIcon.Visible = true;
        ShowInTaskbar = false;
        WindowState = FormWindowState.Minimized;
        Hide();
        ShowStatus("Runtime Deploy is still running in the notification area.", SystemColors.ControlText);
    }

    private void RestoreFromTray()
    {
        Show();
        ShowInTaskbar = true;
        WindowState = FormWindowState.Normal;
        _notifyIcon.Visible = false;
        Activate();
        ShowStatus("Ready.", SystemColors.ControlText);
    }

    private void RestoreMenuItem_Click(object sender, EventArgs e)
        => RestoreFromTray();

    private void OptionsMenuItem_Click(object sender, EventArgs e)
        => ShowOptionsDialog();

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
        if (optionsForm.ShowDialog(this) == DialogResult.OK)
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
