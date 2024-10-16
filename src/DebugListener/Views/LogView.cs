using CommunityToolkit.WinForms.AsyncSupport;
using DevTools.Libs.DebugListener;
using System.ComponentModel;
using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DebugListener.Views;

public partial class LogView : DataGridView
{
    public event EventHandler<LogViewSelectionChangedEventArgs>? LogViewSelectionChanged;
    
    private const int MaxRows = 10000;
    private const string DefaultDateTimeFormatString = "HH:mm:ss-fff";
    private const string DefaultTimeSpanFormatString = "hh\\:mm\\:ss\\-fff";

    private readonly BindingList<DebugMessage> _debugMessage;
    private readonly DebugMessageListener _debugMessageListener;
    private readonly AsyncTaskQueue _taskQueue = new();
    private readonly List<Color> _foreColors;
    private readonly Dictionary<int, Color> _processColors = [];
    private int _colorProcessIndex = -1;

    private Font? _boldFont;

    public LogView()
    {
        InitializeComponents();

        MultiSelect = true;
        VirtualMode = true;

        _debugMessageListener = new DebugMessageListener();
        _debugMessageListener.DebugMessageReceivedAsync += DebugMessageListener_DebugMessageReceivedAsync; ;
        _debugMessage = [];
        RowCount = _debugMessage.Count;

        _foreColors = Application.IsDarkModeEnabled 
            ? GetGoodContrastDarkModeForeColors() 
            : GetGoodContrastLightModeForeColors();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new DataGridViewColumnCollection? Columns => base.Columns;

    public async Task StartListeningAsync() 
        => await _debugMessageListener.StartListeningAsync();

    private async Task DebugMessageListener_DebugMessageReceivedAsync(object sender, DebugMessageEventArgs e)
    {
        if (OnlyShowExtendedDebugInfo && e.DebugMessage.DebugInfo.HasValue || !OnlyShowExtendedDebugInfo)
        {
            await _taskQueue.EnqueueAsync(() => AddLogEntryAsync(e.DebugMessage));
        }
    }

    protected override void OnSelectionChanged(EventArgs e)
    {
        LogViewSelectionChangedEventArgs eArgs;
            
        base.OnSelectionChanged(e);

        if (SelectedRows.Count == 0)
        {
            eArgs = new(-1, [], TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero);
            LogViewSelectionChanged?.Invoke(this, eArgs);

            return;
        }

        if (SelectedRows.Count == 1)
        {
            var processDuration = CalculateProcessDuration(_debugMessage[SelectedRows[0].Index].ProcessId);
            var threadDuration = CalculateThreadDuration((short)(_debugMessage[SelectedRows[0].Index].DebugInfo?.ThreadId ?? 0));
            var totalDuration = CalculateTotalDuration();

            eArgs = new(
                SelectedRows[0].Index,
                SelectedDebugMessages,
                processDuration,
                threadDuration,
                totalDuration);

            LogViewSelectionChanged?.Invoke(this, eArgs);

            return;
        }

        var rangeDuration = CalculateTotalDuration();

        eArgs = new(
            SelectedRows[0].Index,
            SelectedDebugMessages,
            rangeDuration,
            rangeDuration,
            rangeDuration);

        LogViewSelectionChanged?.Invoke(this, eArgs);
    }

    private DebugMessage[] SelectedDebugMessages
    {
        get
        {
            List<DebugMessage> selectedRows = [];
            foreach (DataGridViewRow row in SelectedRows)
            {
                selectedRows.Add(_debugMessage[row.Index]);
            }

            return [.. selectedRows];
        }
    }

    private TimeSpan CalculateThreadDuration(short threadId)
    {
        TimeSpan duration = TimeSpan.Zero;
        for (int i = 0; i < _debugMessage.Count; i++)
        {
            if (_debugMessage[i].DebugInfo?.ThreadId == threadId)
            {
                duration += _debugMessage[i].Duration;
            }
        }
        return duration;
    }

    private TimeSpan CalculateProcessDuration(int processId)
    {
        TimeSpan duration = TimeSpan.Zero;
        for (int i = 0; i < _debugMessage.Count; i++)
        {
            if (_debugMessage[i].ProcessId == processId)
            {
                duration += _debugMessage[i].Duration;
            }
        }
        return duration;
    }

    private TimeSpan CalculateTotalDuration()
    {
        TimeSpan duration = TimeSpan.Zero;

        if (SelectedRows.Count == 1)
        {
            // We calculate the total duration from the first row to the selected row:
            for (int i = 0; i <= SelectedRows[0].Index; i++)
            {
                duration += _debugMessage[i].Duration;
            }

            return duration;
        }

        for (int i = 0; i < _debugMessage.Count; i++)
        {
            if (Rows[i].Selected)
            {
                duration += _debugMessage[i].Duration;
            }
        }

        return duration;
    }

    protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
    {
        base.OnRowsAdded(e);

        if (ColorProcesses && e.RowIndex > 0)
        {
            var processId = _debugMessage[e.RowIndex].ProcessId;
            if (_processColors.TryGetValue(processId, out Color value))
            {
                _debugMessage[e.RowIndex].ForeColor = value;
            }
            else
            {
                _colorProcessIndex++;
                if (_colorProcessIndex >= _foreColors.Count)
                {
                    _colorProcessIndex = 0;
                }

                _debugMessage[e.RowIndex].ForeColor = _foreColors[_colorProcessIndex];
                _processColors.Add(processId, _foreColors[_colorProcessIndex]);
            }
        }
    }

    [DefaultValue(true)]
    public bool ColorProcesses { get; set; } = true;

    [DefaultValue(true)]
    public bool LeaveSpaceBetweenProcesses { get; set; } = true;

    [DefaultValue(true)]
    public bool OnlyShowExtendedDebugInfo { get; set; } = true;

    [DefaultValue(DefaultTimeSpanFormatString)]
    public string TimeSpanFormatString { get; set; } = DefaultDateTimeFormatString;

    [DefaultValue(DefaultDateTimeFormatString)]
    public string DateTimeFormatString { get; set; } = DefaultDateTimeFormatString;

    private Font BoldFont => _boldFont ??= new(Font, FontStyle.Bold);

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
        Columns["Duration"]!.Width = 160;
        Columns["ProcessId"]!.Width = 150;
        Columns["ThreadId"]!.Width = 150;
        Columns["Method"]!.Width = 300;
        Columns["LineNo"]!.Width = 140;
        Columns["Message"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        Columns["CodeFile"]!.Width = 200;
    }

    protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
    {
        base.OnRowPrePaint(e);

        DataGridViewCellStyle style = Rows[e.RowIndex].DefaultCellStyle;
        if (_debugMessage[e.RowIndex].DebugInfo?.Command== ExtendedDebugInfo.DebugInfoCommandId.ImportantMessage)
        {
            style.Font = BoldFont;
        }

        style.ForeColor = _debugMessage[e.RowIndex].ForeColor ?? ForeColor;
    }

    protected override void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
    {
        base.OnCellValueNeeded(e);
        if (e.RowIndex >= 0 && e.RowIndex < _debugMessage.Count)
        {
            DebugMessage debugMessage = _debugMessage[e.RowIndex];

            TimeSpan duration = e.RowIndex == 0
                ? TimeSpan.Zero
                : debugMessage.Timestamp - _debugMessage[e.RowIndex - 1].Timestamp;

            debugMessage.Duration = duration;

            if (debugMessage.DebugInfo is null)
            {
                e.Value = e.ColumnIndex switch
                {
                    0 => $"{debugMessage.Timestamp.ToString(DateTimeFormatString)}",
                    1 => $"{debugMessage.Duration.ToString(TimeSpanFormatString)}",
                    2 => $"{debugMessage.ProcessId:00000}",
                    6 => debugMessage.Message,
                    _ => $"- - -",
                };

                return;
            }

            e.Value = e.ColumnIndex switch
            {
                0 => $"{debugMessage.Timestamp.ToString(DateTimeFormatString)}",
                1 => $"{debugMessage.Duration.ToString(TimeSpanFormatString)}",
                2 => $"{debugMessage.ProcessId:00000}",
                3 => $"{debugMessage.DebugInfo.Value.ThreadId:00000}",
                4 => $"{debugMessage.MethodName}",
                5 => $"{debugMessage.DebugInfo.Value.LineNo:00000}",
                6 => debugMessage.Message,
                7 => debugMessage.FilePath,
                8 => debugMessage.Category,
                _ => $"- - -",
            };
        }
    }

    public void Clear()
    {
        _debugMessage.Clear();
        RowCount = 0;
    }

    public async Task AddLogEntryAsync(DebugMessage debugMessage) 
        => await InvokeAsync(() =>
            {
                _debugMessage.Add(debugMessage);
                RowCount = _debugMessage.Count;

                // Limit the number of entries
                if (_debugMessage.Count > MaxRows)
                {
                    _debugMessage.RemoveAt(0);
                    RowCount = _debugMessage.Count;
                }

                // Auto-scroll to the latest entry
                if (Rows.Count > 0)
                {
                    FirstDisplayedScrollingRowIndex = Rows.Count - 1;
                }
            });

    public void ClearLogs()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(ClearLogs));
        }
        else
        {
            _debugMessage.Clear();
            RowCount = 0;
        }
    }

    private static List<Color> GetGoodContrastDarkModeForeColors()
    {
        // Return a list of colors with high contrast on a black background
        return
        [
            Color.FromArgb(255, 255, 255), // White
            Color.FromArgb(255, 255, 0),   // Yellow
            Color.FromArgb(0, 255, 255),   // Cyan
            Color.FromArgb(0, 255, 0),     // Lime
            Color.FromArgb(255, 0, 255),   // Magenta
            Color.FromArgb(255, 165, 0),   // Orange
            Color.FromArgb(255, 192, 203), // Pink
            Color.FromArgb(255, 105, 180), // Hot Pink
            Color.FromArgb(173, 216, 230), // Light Blue
            Color.FromArgb(144, 238, 144), // Light Green
            Color.FromArgb(255, 182, 193), // Light Pink
            Color.FromArgb(250, 250, 210), // Light Goldenrod Yellow
            Color.FromArgb(240, 230, 140), // Khaki
            Color.FromArgb(255, 228, 225), // Misty Rose
            Color.FromArgb(255, 240, 245), // Lavender Blush
            Color.FromArgb(245, 245, 220), // Beige
            Color.FromArgb(230, 230, 250), // Lavender
            Color.FromArgb(240, 255, 255), // Azure
            Color.FromArgb(255, 250, 240), // Floral White
            Color.FromArgb(255, 239, 213), // Papaya Whip
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
