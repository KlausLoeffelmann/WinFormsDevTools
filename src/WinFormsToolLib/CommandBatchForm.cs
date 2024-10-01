namespace WinFormsToolLib;

public partial class CommandBatchForm : Form
{
    public CommandBatchForm(string? windowTitle)
    {
        InitializeComponent();

        windowTitle ??= $"Command Batch";

        Text = windowTitle + $" - started {DateTime.Now: ddd, yy/MM/dd hh:mm:ss}";
    }

    public Task PrintAsync(string? message)
        => _printableTextBox.PrintAsync(message);

    public Task PrintLineAsync(string? message)
        => _printableTextBox.PrintLineAsync(message);

    public Task StartBatchAsync()
    {
        _okButton.Enabled = false;
        return ShowAsync();
    }

    public void EndBatch() 
        => _okButton.Enabled = true;

    private void OkButton_Click(object sender, EventArgs e) 
        => Close();
}
