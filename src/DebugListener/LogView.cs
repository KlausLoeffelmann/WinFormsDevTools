using CommunityToolkit.WinForms.AsyncSupport;
using System.ComponentModel;

namespace DebugListener;

public class LogView : DataGridView
{
    private const int MaxRows = 10000;
    private readonly BindingList<LogEntry> _logEntries;
    private readonly DebugMessageListener _debugMessageListener;
    private AsyncTaskQueue _taskQueue = new();

    public LogView()
    {
        InitializeComponents();
        _debugMessageListener = new DebugMessageListener();
        _debugMessageListener.DebugMessageReceivedAsync += DebugMessageListener_DebugMessageReceivedAsync; ;
        _logEntries = [];
        VirtualMode = true;
        CellValueNeeded += LogView_CellValueNeeded;
        RowPrePaint += LogView_RowPrePaint;
        RowCount = _logEntries.Count;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new DataGridViewColumnCollection? Columns => base.Columns;

    public async Task StartListeningAsync()
    {
        await _debugMessageListener.StartListeningAsync();
    }

    private async Task DebugMessageListener_DebugMessageReceivedAsync(object sender, DebugMessageEventArgs e)
    {
        await _taskQueue.EnqueueAsync(() => AddLogEntryAsync(e.Timestamp, e.ProcessId, e.Message));
    }

    private void InitializeComponents()
    {
        // Set properties
        Dock = DockStyle.Fill;
        ReadOnly = true;
        AllowUserToAddRows = false;
        AllowUserToDeleteRows = false;
        SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        MultiSelect = false;

        // Enable double buffering
        DoubleBuffered = true;

        // Add columns
        Columns!.Add("Timestamp", "Timestamp");
        Columns.Add("ProcessId", "Process ID");
        Columns.Add("Message", "Message");

        // Adjust column widths
        Columns["Timestamp"]!.Width = 150;
        Columns["ProcessId"]!.Width = 150;
        Columns["Message"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    }

    private void LogView_CellValueNeeded(object? sender, DataGridViewCellValueEventArgs e)
    {
        if (e.RowIndex >= 0 && e.RowIndex < _logEntries.Count)
        {
            var logEntry = _logEntries[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 0:
                    e.Value = $"[{logEntry.Timestamp:HH:mm:ss-fff}]";
                    break;
                case 1:
                    e.Value = $"[{logEntry.ProcessId:00000}]";
                    break;
                case 2:
                    e.Value = logEntry.Message;
                    break;
            }
        }
    }

    private void LogView_RowPrePaint(object? sender, DataGridViewRowPrePaintEventArgs e)
    {
        if (e.RowIndex >= 0 && e.RowIndex < _logEntries.Count)
        {
            DataGridViewCellStyle style = Rows[e.RowIndex].DefaultCellStyle;
            style.ForeColor = Application.IsDarkModeEnabled ? Color.White : Color.Black;
        }
    }

    public async Task AddLogEntryAsync(DateTime timestamp, int processId, string message)
    {
        await InvokeAsync(() => 
        {
            _logEntries.Add(new LogEntry(timestamp, processId, message));

            RowCount = _logEntries.Count;

            // Limit the number of entries
            if (_logEntries.Count > MaxRows)
            {
                _logEntries.RemoveAt(0);
                RowCount = _logEntries.Count;
            }

            // Auto-scroll to the latest entry
            if (Rows.Count > 0)
            {
                FirstDisplayedScrollingRowIndex = Rows.Count - 1;
            }
        });
    }

    public void ClearLogs()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(ClearLogs));
        }
        else
        {
            _logEntries.Clear();
            RowCount = 0;
        }
    }

    private record LogEntry(DateTime Timestamp, int ProcessId, string? Message);
}
