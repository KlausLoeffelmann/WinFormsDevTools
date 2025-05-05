using CToolkit.Desktop.Async.Collections;
using DevTools.Libs.DebugListener;
using System.ComponentModel;
using System.Text;
using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DebugListener.Views;

/// <summary>
///  Represents a view for displaying log messages in a DataGridView.
/// </summary>
public partial class LogView : DataGridView
{
    /// <summary>
    ///  Occurs when the selection changes in the LogView.
    /// </summary>
    public event EventHandler<LogViewSelectionChangedEventArgs>? LogViewSelectionChanged;

    private const int MaxRows = 10000;
    private const string DefaultDateTimeFormatString = "HH:mm:ss-fff";
    private const string DefaultTimeSpanFormatString = "hh\\:mm\\:ss\\-fff";

    private readonly BindingList<DebugMessage> _debugMessages;
    private readonly DebugMessageListener _debugMessageListener;
    private readonly AsyncTaskQueue _taskQueue = new();
    private readonly List<Color> _foreColors;
    private readonly Dictionary<int, Color> _processColors = [];
    private int _colorProcessIndex = -1;
    private DateTime _startTime = DateTime.MinValue;

    private Font? _boldFont;
    private bool _totalSecondsTimeStamps;
    private TimeSpan _longestDuration = TimeSpan.Zero;
    private ContextMenuStrip? _contextMenu;

    /// <summary>
    ///  Initializes a new instance of the <see cref="LogView"/> class.
    /// </summary>
    public LogView()
    {
        _debugMessageListener = new DebugMessageListener();
        _debugMessageListener.DebugMessageReceivedAsync += DebugMessageListener_DebugMessageReceivedAsync;
        _debugMessages = [];

        _foreColors = Application.IsDarkModeEnabled
            ? GetGoodContrastDarkModeForeColors()
            : GetGoodContrastLightModeForeColors();
    }

    /// <summary>
    ///  Starts listening for debug messages asynchronously.
    /// </summary>
    public async Task StartListeningAsync()
        => await _debugMessageListener.StartListeningAsync();

    private async Task DebugMessageListener_DebugMessageReceivedAsync(object sender, DebugMessageEventArgs e)
    {
        if (OnlyShowExtendedDebugInfo && e.DebugMessage.DebugInfo.HasValue || !OnlyShowExtendedDebugInfo)
        {
            await _taskQueue.EnqueueAsync(() => AddLogEntryAsync(e.DebugMessage));
        }
    }

    /// <summary>
    /// Adds a log entry asynchronously.
    /// </summary>
    /// <param name="debugMessage">The debug message to add.</param>
    public async Task AddLogEntryAsync(DebugMessage debugMessage)
        => await InvokeAsync(() =>
        {
            _debugMessages.Add(debugMessage);
            RowCount = _debugMessages.Count;

            // Limit the number of entries
            if (_debugMessages.Count > MaxRows)
            {
                _debugMessages.RemoveAt(0);
                RowCount = _debugMessages.Count;
            }

            // Auto-scroll to the latest entry
            if (Rows.Count > 0)
            {
                FirstDisplayedScrollingRowIndex = Rows.Count - 1;
            }
        });

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        // Let's prevent the designer from running the timer and serializing the DataGridView properties.
        if (IsAncestorSiteInDesignMode)
        {
            return;
        }

        SetupDataGridView();

        MultiSelect = true;
        VirtualMode = true;
        RowCount = _debugMessages.Count;
    }

    protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
    {
        base.OnRowsAdded(e);

        if (_startTime == DateTime.MinValue)
        {
            _startTime = _debugMessages[0].Timestamp;
        }

        for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
        {
            DebugMessage debugMessage = _debugMessages[i];

            TimeSpan duration = i == 0
                ? TimeSpan.Zero
                : debugMessage.Timestamp - _debugMessages[i - 1].Timestamp;

            debugMessage.Duration = duration;

            if (debugMessage.Duration > _longestDuration)
            {
                _longestDuration = debugMessage.Duration;
            }

            var processId = debugMessage.ProcessId;
            if (_processColors.TryGetValue(processId, out Color value))
            {
                debugMessage.ForeColor = value;
            }
            else
            {
                _colorProcessIndex++;
                if (_colorProcessIndex >= _foreColors.Count)
                {
                    _colorProcessIndex = 0;
                }

                debugMessage.ForeColor = _foreColors[_colorProcessIndex];
                _processColors.Add(processId, _foreColors[_colorProcessIndex]);
            }
        }
    }

    protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
    {
        base.OnRowPrePaint(e);

        DataGridViewCellStyle style = Rows[e.RowIndex].DefaultCellStyle;
        if (_debugMessages[e.RowIndex].DebugInfo?.Command == ExtendedDebugInfo.DebugInfoCommandId.ImportantMessage)
        {
            style.Font = BoldFont;
        }

        style.ForeColor = _debugMessages[e.RowIndex].ForeColor ?? ForeColor;
    }

    protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
    {
        if (_debugMessages.Count == 0 || e.RowIndex == -1)
        {
            return;
        }

        if (e.ColumnIndex == 0)
        {
            // First we paint the original cell background:
            e.PaintBackground(e.CellBounds, true);

            DebugMessage debugMessage = _debugMessages[e.RowIndex];
            double durationPercentage = debugMessage.Duration.TotalMilliseconds / _longestDuration.TotalMilliseconds;

            // Adjust the real estate space: give more space to lower durations
            double adjustedPercentage = Math.Sqrt(durationPercentage); // Square root to give lower values more space
            int barWidth = (int)(e.CellBounds.Width * adjustedPercentage);

            // Determine color based on percentage ranges
            Color color;
            bool isDarkMode = Application.IsDarkModeEnabled;

            if (durationPercentage <= 0.10) // 0-10%
            {
                color = isDarkMode 
                    ? Color.FromArgb(0, 0, 150) 
                    : Color.FromArgb(144, 144, 238); // Dark blue for dark mode, light blue for light mode
            }
            else if (durationPercentage <= 0.75) // 11-75%
            {
                color = isDarkMode 
                    ? Color.FromArgb(139, 69, 19) 
                    : Color.FromArgb(255, 165, 0); // Dark orange for dark mode, light orange for light mode
            }
            else // 76-100%
            {
                color = isDarkMode 
                    ? Color.FromArgb(139, 0, 0) 
                    : Color.FromArgb(255, 99, 71); // Dark red for dark mode, light red for light mode
            }

            // Draw a rectangle based on the adjusted percentage
            Rectangle rectDurationIndicator = new(
                e.CellBounds.X,
                e.CellBounds.Y,
                barWidth,
                e.CellBounds.Height);

            using Brush brush = new SolidBrush(color);
            e.Graphics?.FillRectangle(brush, rectDurationIndicator);

            e.PaintContent(e.CellBounds);
            e.Handled = true;
        }
        else
        {
            base.OnCellPainting(e);
        }
    }

    protected override void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
    {
        base.OnCellValueNeeded(e);
        if (e.RowIndex >= 0 && e.RowIndex < _debugMessages.Count)
        {
            DebugMessage debugMessage = _debugMessages[e.RowIndex];

            if (debugMessage.DebugInfo is null)
            {
                e.Value = e.ColumnIndex switch
                {
                    0 => $"{DateTimeFormat(debugMessage.Timestamp, TotalSecondsTimestamps)}",
                    1 => $"{debugMessage.Duration.ToString(TimeSpanFormatString)}",
                    2 => $"{debugMessage.ProcessId:00000}",
                    6 => debugMessage.Message,
                    _ => $"- - -",
                };

                return;
            }

            e.Value = e.ColumnIndex switch
            {
                0 => $"{DateTimeFormat(debugMessage.Timestamp, TotalSecondsTimestamps)}",
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

    protected override void OnSelectionChanged(EventArgs e)
    {
        LogViewSelectionChangedEventArgs eArgs;

        base.OnSelectionChanged(e);

        if (SelectedRows.Count == 0)
        {
            eArgs = new(-1,DateTime.Now, TimeSpan.Zero, [], TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero);
            LogViewSelectionChanged?.Invoke(this, eArgs);

            return;
        }

        if (SelectedRows.Count == 1)
        {
            var processDuration = CalculateProcessDuration(_debugMessages[SelectedRows[0].Index].ProcessId);
            var threadDuration = CalculateThreadDuration((short)(_debugMessages[SelectedRows[0].Index].DebugInfo?.ThreadId ?? 0));
            var totalDuration = CalculateTotalDuration();

            eArgs = new(
                SelectedRows[0].Index,
                _debugMessages[SelectedRows[0].Index].Timestamp,
                _debugMessages[SelectedRows[0].Index].Duration,
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
            _debugMessages[SelectedRows[0].Index].Timestamp,
            _debugMessages[SelectedRows[0].Index].Duration,
            SelectedDebugMessages,
            rangeDuration,
            rangeDuration,
            rangeDuration);

        LogViewSelectionChanged?.Invoke(this, eArgs);
    }

    private TimeSpan CalculateThreadDuration(short threadId)
    {
        TimeSpan duration = TimeSpan.Zero;
        for (int i = 0; i < _debugMessages.Count; i++)
        {
            if (_debugMessages[i].DebugInfo?.ThreadId == threadId)
            {
                duration += _debugMessages[i].Duration;
            }
        }
        return duration;
    }

    private TimeSpan CalculateProcessDuration(int processId)
    {
        TimeSpan duration = TimeSpan.Zero;
        for (int i = 0; i < _debugMessages.Count; i++)
        {
            if (_debugMessages[i].ProcessId == processId)
            {
                duration += _debugMessages[i].Duration;
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
                duration += _debugMessages[i].Duration;
            }

            return duration;
        }

        for (int i = 0; i < _debugMessages.Count; i++)
        {
            if (Rows[i].Selected)
            {
                duration += _debugMessages[i].Duration;
            }
        }

        return duration;
    }

    /// <summary>
    /// Formats a DateTime value.
    /// </summary>
    /// <param name="dateTime">The DateTime value to format.</param>
    /// <param name="totalSeconds">Whether to use total seconds for formatting.</param>
    /// <returns>The formatted DateTime string.</returns>
    private string DateTimeFormat(DateTime dateTime, bool totalSeconds = false)
    {
        if (totalSeconds)
        {
            TimeSpan diff = dateTime - _startTime;
            return $"{diff.TotalSeconds:#,##0.000\\'000}";
        }

        return dateTime.ToString(DateTimeFormatString);
    }

    /// <summary>
    /// Clears the logs.
    /// </summary>
    public void ClearLogs()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(ClearLogs));
        }
        else
        {
            _debugMessages.Clear();
            RowCount = 0;

            _startTime = DateTime.MinValue;
            _colorProcessIndex = -1;
            _longestDuration = TimeSpan.Zero;
        }
    }

    /// <summary>
    /// Saves the logs to a file.
    /// </summary>
    /// <param name="fileName">The file name to save the logs to.</param>
    public void SaveToFile(string fileName)
    {
        // Write the log entries as a CSV file
        using StreamWriter writer = new(fileName);

        writer.WriteLine("Timestamp,Duration,ProcessId,ThreadId,Method,LineNo,Message,CodeFile,Category");
        foreach (var debugMessage in _debugMessages)
        {
            writer.WriteLine($"{debugMessage.Timestamp.Ticks}," +
                $"{debugMessage.Duration.Ticks}," +
                $"{debugMessage.ProcessId}," +
                $"{debugMessage.DebugInfo?.ThreadId ?? 0}," +
                $"\"{debugMessage.MethodName}\"," +
                $"{debugMessage.DebugInfo?.LineNo ?? 0}," +
                $"\"{debugMessage.Message?.Replace(Environment.NewLine, "{{newline}}")}\"," +
                $"\"{debugMessage.FilePath}\"," +
                $"\"{debugMessage.Category}\"," +
                $"\"{debugMessage.DebugInfo?.Command ?? ExtendedDebugInfo.DebugInfoCommandId.Message}\"");
        }
    }

    /// <summary>
    /// Loads the logs from a file.
    /// </summary>
    /// <param name="fileName">The file name to load the logs from.</param>
    /// <returns>The timestamp of the last log entry.</returns>
    public DateTime LoadFromFile(string fileName)
    {
        int exceptionCounter = 0;
        ClearLogs();

        // Load the log entries from a CSV file
        using StreamReader reader = new(fileName);
        DebugMessage? lastDebugMessage = null;

        DateTime timeStamp;
        ushort processId;
        ushort threadId;
        ushort lineNo;
        string? line;
        ExtendedDebugInfo.DebugInfoCommandId command;

        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = [.. ParseCsvLine(line)];

            try
            {
                if (parts.Length >= 9)
                {
                    timeStamp = new(long.Parse(parts[0]));
                    processId = ushort.Parse(parts[2]);
                    threadId = ushort.Parse(parts[3]);
                    lineNo = ushort.Parse(parts[5]);

                    command = parts.Length == 10
                        ? Enum.Parse<ExtendedDebugInfo.DebugInfoCommandId>(parts[9])
                        : ExtendedDebugInfo.DebugInfoCommandId.Message;

                    DebugMessage debugMessage = new(timeStamp, processId)
                    {
                        DebugInfo = new(
                            timestamp: timeStamp,
                            processId: processId,
                            threadId: threadId,
                            lineNo: lineNo,
                            command: command),
                        MethodName = parts[4],
                        Message = parts[6].Replace("{{newline}}", Environment.NewLine),
                        FilePath = parts[^2],
                        Category = parts[^1],
                    };

                    _debugMessages.Add(debugMessage);
                    lastDebugMessage = debugMessage;
                }
            }
            catch (Exception ex) when (ex is not StackOverflowException
                and not OutOfMemoryException)
            {
                exceptionCounter++;
                if (exceptionCounter > 10)
                {
                    // We've had enough of this.
                    break;
                }
            }
        }

        RowCount = _debugMessages.Count;

        // Returns the timestamp of the last log entry.
        // That's the file info, if you will.
        return _debugMessages[^1].Timestamp;
    }

    private void SetupDataGridView()
    {
        // Set properties
        Dock = DockStyle.Fill;
        ReadOnly = true;
        AllowUserToAddRows = false;
        AllowUserToDeleteRows = false;
        SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        MultiSelect = true;
        AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

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
        Columns["Timestamp"]!.Width = 160;
        Columns["Timestamp"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        Columns["Timestamp"]!.DefaultCellStyle.Padding = new(2, 2, 10, 2);

        Columns["Duration"]!.Width = 170;
        Columns["Duration"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        Columns["ProcessId"]!.Width = 160;
        Columns["ProcessId"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        Columns["ThreadId"]!.Width = 150;
        Columns["ThreadId"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        Columns["Method"]!.Width = 300;
        Columns["LineNo"]!.Width = 140;

        Columns["Message"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        Columns["Message"]!.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        Columns["CodeFile"]!.Width = 200;

        SetupContextMenu();
        ContextMenuStrip = _contextMenu;

        if (Application.IsDarkModeEnabled)
        {
            // Style the column headers for dark mode
            DataGridViewCellStyle headerStyle = new()
            {
                BackColor = Color.FromArgb(45, 45, 48), // Dark background color
                ForeColor = Color.White, // White text for contrast
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                WrapMode = DataGridViewTriState.True,
                Padding = new(10)
            };

            ColumnHeadersDefaultCellStyle = headerStyle;

            // Row style for dark mode (including selection)
            DataGridViewCellStyle rowStyle = new()
            {
                BackColor = Color.FromArgb(30, 30, 30), // Darker background for rows
                ForeColor = Color.White, // White text for readability
                Font = new Font("Segoe UI", 11),
                SelectionBackColor = Color.FromArgb(0, 122, 204), // Blue selection background
                SelectionForeColor = Color.White, // White text during selection
                Padding = new(2),
                WrapMode = DataGridViewTriState.True
            };

            DefaultCellStyle = rowStyle;

            // Disable column header highlighting on selection
            EnableHeadersVisualStyles = false;

            // Adjust gridlines for a more subtle look in dark mode
            GridColor = Color.FromArgb(50, 50, 50); // Subtle grid color in dark mode
        }
    }

    private void SetupContextMenu()
    {
        // Create the context menu
        _contextMenu = new ContextMenuStrip()
        {
            Font = new Font("Segoe UI", 11)
        };

        // Add menu items
        ToolStripMenuItem tsmCopy = new("Copy", null, CopyMenuItem_Click);
        ToolStripMenuItem tsmCopyMessage = new("Copy Message", null, CopyMessageMenuItem_Click);

        ToolStripSeparator separator = new();

        ToolStripMenuItem tsmSelectProcessItems = new("Select same process items", null, SelectProcessItems_Click);
        ToolStripMenuItem tsmSelectThreadItems = new("Select same thread items", null, SelectThreadItems_Click);

        _contextMenu.Items.AddRange(
        [
            tsmCopy,
            tsmCopyMessage,
            separator,
            tsmSelectProcessItems,
            tsmSelectThreadItems
        ]);
    }

    private void SelectThreadItems_Click(object? sender, EventArgs e)
    {
        // If we haven't selected a row, we can't do anything.
        if (SelectedRows.Count == 0)
        {
            return;
        }

        // Get the thread ID of the selected row
        short threadId = (short)(_debugMessages[SelectedRows[0].Index].DebugInfo?.ThreadId ?? 0);

        // Select all rows with the same thread ID
        for (int i = 0; i < _debugMessages.Count; i++)
        {
            if (_debugMessages[i].DebugInfo?.ThreadId == threadId)
            {
                Rows[i].Selected = true;
            }
        }
    }

    private void SelectProcessItems_Click(object? sender, EventArgs e)
    {
        // If we haven't selected a row, we can't do anything.
        if (SelectedRows.Count == 0)
        {
            return;
        }
        // Get the process ID of the selected row
        int processId = _debugMessages[SelectedRows[0].Index].ProcessId;
        // Select all rows with the same process ID
        for (int i = 0; i < _debugMessages.Count; i++)
        {
            if (_debugMessages[i].ProcessId == processId)
            {
                Rows[i].Selected = true;
            }
        }
    }

    private void CopyMessageMenuItem_Click(object? sender, EventArgs e)
    {
        // If we haven't selected a row, we can't do anything.
        if (SelectedRows.Count == 0)
        {
            return;
        }

        // Get the message of the selected row
        string? message = _debugMessages[SelectedRows[0].Index].Message;

        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        // Copy the message to the clipboard
        Clipboard.SetText(message);
    }

    private void CopyMenuItem_Click(object? sender, EventArgs e)
    {
        // If we haven't selected a row, we can't do anything.
        if (SelectedRows.Count == 0)
        {
            return;
        }

        // Get the selected row
        DataGridViewRow row = SelectedRows[0];

        // Copy the selected row to the clipboard
        StringBuilder sb = new();

        for (int i = 0; i < Columns.Count; i++)
        {
            sb.Append(row.Cells[i].Value);
            sb.Append(i < Columns.Count - 1 ? "," : Environment.NewLine);
        }

        Clipboard.SetText(sb.ToString());
    }

    private static List<string> ParseCsvLine(string line)
    {
        List<string> result = [];
        StringBuilder currentField = new();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char currentChar = line[i];

            if (currentChar == '"')
            {
                // Toggle the inQuotes flag when encountering a quote
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Handle escaped quotes by adding one and skipping the next char
                    currentField.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (currentChar == ',' && !inQuotes)
            {
                // Field separator found and not inside quotes, add field to result
                result.Add(currentField.ToString());
                currentField.Clear();
            }
            else
            {
                // Add normal characters to the current field
                currentField.Append(currentChar);
            }
        }

        // Add the last field
        result.Add(currentField.ToString());

        return result;
    }

    private static List<Color> GetGoodContrastDarkModeForeColors() =>
        // Return a list of colors with high contrast on a black background
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

    private static List<Color> GetGoodContrastLightModeForeColors() =>
        // Return a list of colors, which can be well read in light mode on a white background:
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
