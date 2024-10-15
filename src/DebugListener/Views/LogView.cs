using CommunityToolkit.WinForms.AsyncSupport;
using DevTools.Libs.DebugListener;
using System.ComponentModel;
using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DebugListener.Views;

public partial class LogView : DataGridView
{
    private const int MaxRows = 10000;

    private readonly BindingList<DebugMessage> _debugMessage;
    private readonly DebugMessageListener _debugMessageListener;
    private readonly AsyncTaskQueue _taskQueue = new();
    private readonly List<Color> _foreColors;
    private readonly Dictionary<int, Color> _processColors = [];
    private int _colorProcessIndex = -1;

    public LogView()
    {
        InitializeComponents();
        _debugMessageListener = new DebugMessageListener();
        _debugMessageListener.DebugMessageReceivedAsync += DebugMessageListener_DebugMessageReceivedAsync; ;
        _debugMessage = [];
        VirtualMode = true;
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
                    0 => $"{debugMessage.Timestamp:HH:mm:ss-fff}",
                    1 => $"{debugMessage.Duration:hh\\:mm\\:ss\\-fff}",
                    2 => $"{debugMessage.ProcessId:00000}",
                    6 => debugMessage.Message,
                    _ => $"- - -",
                };

                return;
            }

            e.Value = e.ColumnIndex switch
            {
                0 => $"{debugMessage.Timestamp:HH:mm:ss-fff}",
                1 => $"{debugMessage.Duration:hh\\:mm\\:ss\\-fff}",
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

    internal void Clear()
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
