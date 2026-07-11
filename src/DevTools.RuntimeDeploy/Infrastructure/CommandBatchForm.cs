namespace DevTools.RuntimeDeploy.Infrastructure;

public partial class CommandBatchForm : Form
{
    // Completes once the form has actually been shown (handle created and the
    // window pump is running). Writes to the embedded ConsoleControl marshal onto
    // the UI thread from a background thread; if they are issued before the modeless
    // ShowAsync() Show() call has completed they race with window creation, which
    // drops the early output and can dead-lock the next ShowAsync(). Gating every
    // write on this signal keeps the console reliable across repeated batches.
    private readonly TaskCompletionSource _shownCompletion =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    private readonly RuntimeDeploySettingsService? _settingsService;

    public CommandBatchForm(string? windowTitle, Font? consoleFont = null, RuntimeDeploySettingsService? settingsService = null)
    {
        InitializeComponent();

        _settingsService = settingsService;

        if (consoleFont is not null)
        {
            _console.Font = consoleFont;
        }

        windowTitle ??= $"Command Batch";

        Text = windowTitle + $" - started {DateTime.Now: ddd, yy/MM/dd hh:mm:ss}";

        // ShowAsync() shows the window modeless and does not dispose it on close,
        // which would otherwise leak the form and the ConsoleControl background
        // queue. Dispose deterministically once the window is closed.
        FormClosed += (_, _) => Dispose();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        RestoreSavedBounds();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        _shownCompletion.TrySetResult();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        SaveBounds();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        // Unblock any writes still waiting on the shown signal if the window is
        // closed before it was ever shown.
        _shownCompletion.TrySetResult();
        base.OnFormClosed(e);
    }

    private void RestoreSavedBounds()
    {
        if (_settingsService is null || !_settingsService.SaveWindowPositions)
        {
            return;
        }

        if (_settingsService.CommandBatchFormBounds is not Rectangle bounds)
        {
            return;
        }

        if (!Screen.AllScreens.Any(screen => screen.WorkingArea.IntersectsWith(bounds)))
        {
            return;
        }

        StartPosition = FormStartPosition.Manual;
        Bounds = bounds;
    }

    private void SaveBounds()
    {
        if (_settingsService is null || !_settingsService.SaveWindowPositions)
        {
            return;
        }

        Rectangle bounds = WindowState == FormWindowState.Normal
            ? Bounds
            : RestoreBounds;

        _settingsService.CommandBatchFormBounds = bounds;
    }

    public async Task WriteAsync(string message)
    {
        await _shownCompletion.Task;
        await _console.WriteAsync(message);
    }

    public async Task WriteWarningAsync(string message)
    {
        await _shownCompletion.Task;
        await _console.WriteAsync(message, textColor: Color.Yellow);
    }

    public async Task WriteErrorAsync(string message)
    {
        await _shownCompletion.Task;
        await _console.WriteAsync(message, textColor: Color.Red);
    }

    public async Task WriteLineAsync(string message)
    {
        await _shownCompletion.Task;
        await _console.WriteLineAsync(message);
    }

    public async Task WriteLineWarningAsync(string message)
    {
        await _shownCompletion.Task;
        await _console.WriteLineAsync(message, textColor: Color.Yellow);
    }

    public async Task WriteLineErrorAsync(string message)
    {
        await _shownCompletion.Task;
        await _console.WriteLineAsync(message, textColor: Color.Red);
    }

    public Task StartBatchAsync()
    {
        _okButton.Enabled = false;
        return ShowAsync();
    }

    public async Task EndBatchAsync()
    {
        await _shownCompletion.Task;

        // EndBatchAsync is awaited from a background Task.Run context in
        // CommandBatch/DeployRuntimeView, so the continuation after awaiting
        // _shownCompletion.Task has no UI SynchronizationContext. Marshal the
        // control update back to the UI thread instead of touching it directly.
        await InvokeAsync(() => _okButton.Enabled = true);
    }

    private void OkButton_Click(object sender, EventArgs e)
        => Close();
}
