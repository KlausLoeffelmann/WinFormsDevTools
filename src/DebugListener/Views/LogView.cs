using CommunityToolkit.WinForms.AsyncSupport;
using DevTools.Libs.DebugListener;
using System.ComponentModel;
using System.Diagnostics;
using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DebugListener.Views;

public partial class LogView : DataGridView
{
    private const int MaxRows = 10000;
    private readonly BindingList<LogEntry> _logEntries;
    private readonly DebugMessageListener _debugMessageListener;
    private readonly AsyncTaskQueue _taskQueue = new();
    private readonly List<Color> _processForeColors;
    private readonly Dictionary<int, Color> _processColors = [];
    private int _colorProcessIndex = 0;
    private int _lastProcessId = 0;
    private DateTime _lastTimestamp;

    public LogView()
    {
        InitializeComponents();
        _debugMessageListener = new DebugMessageListener();
        _debugMessageListener.DebugMessageReceivedAsync += DebugMessageListener_DebugMessageReceivedAsync; ;
        _logEntries = [];
        VirtualMode = true;
        CellValueNeeded += LogView_CellValueNeeded;
        RowCount = _logEntries.Count;

        _processForeColors = Application.IsDarkModeEnabled 
            ? GetGoodContrastDarkModeForeColors() 
            : GetGoodContrastLightModeForeColors();
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
        await _taskQueue.EnqueueAsync(() => AddLogEntryAsync(e.Timestamp, e.ProcessId, e.Message, e.DebugInfo));
    }

    [DefaultValue(true)]
    public bool ColorProcesses { get; set; } = true;

    [DefaultValue(true)]
    public bool LeaveSpaceBetweenProcesses { get; set; } = true;

    [DefaultValue(true)]
    public bool OnlyShowExtendedDebugInfo { get; set; } = true;

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
        Columns.Add("Duration", "Duration");
        Columns.Add("ProcessId", "Process ID");
        Columns.Add("ThreadId", "Thread ID");
        Columns.Add("Method", "Method");
        Columns.Add("LineNo", "Line No");
        Columns.Add("Message", "Message");
        Columns.Add("CodeFile", "CodeFile");

        // Adjust column widths
        Columns["Timestamp"]!.Width = 170;
        Columns["Duration"]!.Width = 150;
        Columns["ProcessId"]!.Width = 150;
        Columns["ThreadId"]!.Width = 150;
        Columns["Method"]!.Width = 250;
        Columns["LineNo"]!.Width = 140;
        Columns["Message"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        Columns["CodeFile"]!.Width = 200;
    }

    private void LogView_CellValueNeeded(object? sender, DataGridViewCellValueEventArgs e)
    {
        if (e.RowIndex >= 0 && e.RowIndex < _logEntries.Count)
        {
            LogEntry logEntry = _logEntries[e.RowIndex];
            if (logEntry.DebugInfo is ExtendedDebugInfo debugInfo)
            {
                logEntry.Timestamp = debugInfo.Timestamp;

            }

            TimeSpan duration = logEntry.Timestamp - _lastTimestamp;
            logEntry.Duration = duration;

            if (OnlyShowExtendedDebugInfo)
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        e.Value = $"[{logEntry.Timestamp:HH:mm:ss-fff}]";
                        break;

                    case 1:
                        e.Value = $"[{logEntry.Duration:HH\\:mm\\:ss\\-fff}]";
                        break;

                    case 2:
                        e.Value = $"[{logEntry.ProcessId:00000}]";
                        SetProcessRowColor(e, logEntry);
                        break;

                    case 6:
                        e.Value = logEntry.Message;
                        break;

                    default:
                        e.Value = $"- - -";
                        break;
                }

                _lastTimestamp = logEntry.Timestamp;

                return;
            }

            if (logEntry.DebugInfo is null)
            {
                return;
            }

            switch (e.ColumnIndex)
            {
                case 0:
                    e.Value = $"[{logEntry.Timestamp:HH:mm:ss-fff}]";
                    break;

                case 1:
                    e.Value = $"[{logEntry.Duration:HH\\:mm\\:ss\\-fff}]";
                    break;

                case 2:
                    e.Value = $"[{logEntry.ProcessId:00000}]";
                    SetProcessRowColor(e, logEntry);
                    break;

                case 3:
                    e.Value = $"[{logEntry.DebugInfo.Value.ThreadId:00000}]";
                    break;

                case 4:
                    e.Value = $"[{logEntry.DebugInfo.Value.Method}]";
                    break;

                case 5:
                    e.Value = $"[{logEntry.DebugInfo.Value.LineNo:00000}]";
                    break;

                case 6:
                    e.Value = logEntry.Message;
                    break;

                case 7:
                    e.Value = logEntry.DebugInfo.Value.Filename;
                    break;

                case 8:
                    e.Value = logEntry.DebugInfo.Value.Category;
                    break;
            }

            _lastTimestamp = logEntry.Timestamp;
        }

        void SetProcessRowColor(DataGridViewCellValueEventArgs e, LogEntry logEntry)
        {
            if (ColorProcesses && _lastProcessId != logEntry.ProcessId)
            {
                _lastProcessId = logEntry.ProcessId;
                if (_processColors.TryGetValue(logEntry.ProcessId, out var color))
                {
                    Rows[e.RowIndex].Tag = color;
                }
                else
                {
                    Rows[e.RowIndex].Tag = _processForeColors[_colorProcessIndex++ % _processForeColors.Count];
                }
            }
        }
    }

    //private void LogView_RowPrePaint(object? sender, DataGridViewRowPrePaintEventArgs e)
    //{
    //    if (e.RowIndex >= 0 && e.RowIndex < _logEntries.Count)
    //    {
    //        DataGridViewCellStyle style = Rows[e.RowIndex].DefaultCellStyle;
    //        if (Rows[e.RowIndex].Tag is Color color)
    //        {
    //            style.ForeColor = color;
    //        }
    //        else
    //        {
    //            style.ForeColor = ForeColor;
    //        }
    //    }

    //    Debug.Print("Row pre-paint!");
    //}

    public async Task AddLogEntryAsync(DateTime timestamp, int processId, string message, ExtendedDebugInfo? debugInfo)
    {
        await InvokeAsync(() =>
        {
            _logEntries.Add(new LogEntry(timestamp, processId, message, debugInfo));

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

    private static List<Color> GetGoodContrastDarkModeForeColors()
    {
        // Return a list of colors, which can be well read in dark mode on a black background:
        return
        [
            Color.White,
            Color.Yellow,
            Color.Cyan,
            Color.Lime,
            Color.Magenta,
            Color.Orange,
            Color.Pink,
            Color.Purple,
            Color.Red,
            Color.Silver,
            Color.Turquoise,
            Color.Violet,
            Color.White,
            Color.Yellow,
            Color.Cyan,
            Color.Lime,
            Color.Magenta,
            Color.Orange,
            Color.Pink,
            Color.Purple,
            Color.Red,
            Color.Silver,
            Color.Turquoise,
            Color.Violet
        ];
    }

    private static List<Color> GetGoodContrastLightModeForeColors()
    {
        // Return a list of colors, which can be well read in light mode on a white background:
        return
        [
            Color.Black,
            Color.Blue,
            Color.Green,
            Color.Indigo,
            Color.Maroon,
            Color.Navy,
            Color.Olive,
            Color.Teal,
            Color.YellowGreen,
            Color.DarkRed,
            Color.DarkGreen,
            Color.DarkBlue,
            Color.DarkOrange,
            Color.DarkViolet,
            Color.DarkTurquoise,
            Color.DarkOrange,
            Color.DarkMagenta,
            Color.DarkCyan,
        ];
    }
}
