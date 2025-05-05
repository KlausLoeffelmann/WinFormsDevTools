namespace DevTools.Libs;

public partial class CommandBatchForm : Form
{
    public CommandBatchForm(string? windowTitle)
    {
        InitializeComponent();

        windowTitle ??= $"Command Batch";

        Text = windowTitle + $" - started {DateTime.Now: ddd, yy/MM/dd hh:mm:ss}";
    }

    public Task WriteAsync(string message)
        => _console.WriteAsync(message);

    public Task WriteWarningAsync(string message)
        => _console.WriteAsync(message, textColor: Color.Yellow);

    public Task WriteErrorAsync(string message)
    => _console.WriteAsync(message, textColor: Color.DarkRed);

    public Task WriteLineAsync(string message)
        => _console.WriteLineAsync(message);

    public Task WriteLineWarningAsync(string message)
        => _console.WriteLineAsync(message, textColor: Color.Yellow);

    public Task WriteLineErrorAsync(string message)
        => _console.WriteLineAsync(message, textColor: Color.DarkRed);

    public Task StartBatchAsync()
    {
        _okButton.Enabled = false;
        return ShowAsync();
    }

    public async Task EndBatchAsync() 
    {
        _okButton.Enabled = true;
        await Task.CompletedTask;
    }

    private void OkButton_Click(object sender, EventArgs e) 
        => Close();
}
