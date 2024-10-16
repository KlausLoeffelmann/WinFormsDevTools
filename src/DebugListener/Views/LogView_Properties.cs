using CommunityToolkit.WinForms.AsyncSupport;
using DevTools.Libs.DebugListener;
using System.ComponentModel;
using System.Text;
using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DebugListener.Views;

public partial class LogView
{
    /// <summary>
    ///  Gets or sets a value indicating whether to color processes.
    /// </summary>
    [DefaultValue(true)]
    [Description("Indicates whether to color processes.")]
    public bool ColorProcesses { get; set; } = true;

    /// <summary>
    ///  Gets or sets a value indicating whether to leave space between processes.
    /// </summary>
    [DefaultValue(true)]
    [Description("Indicates whether to leave space between processes.")]
    public bool LeaveSpaceBetweenProcesses { get; set; } = true;

    /// <summary>
    ///  Gets or sets a value indicating whether to only show extended debug information.
    /// </summary>
    [DefaultValue(true)]
    [Description("Indicates whether to only show extended debug information.")]
    public bool OnlyShowExtendedDebugInfo { get; set; } = true;

    /// <summary>
    ///  Gets or sets a value indicating whether to use total seconds for timestamps.
    /// </summary>
    [DefaultValue(false)]
    [Description("Indicates whether to use total seconds for timestamps.")]
    public bool TotalSecondsTimestamps
    {
        get => _totalSecondsTimeStamps;
        set
        {
            if (_totalSecondsTimeStamps == value)
            {
                return;
            }

            _totalSecondsTimeStamps = value;
            Refresh();
        }
    }

    /// <summary>
    ///  Gets or sets the format string for TimeSpan values.
    /// </summary>
    [DefaultValue(DefaultTimeSpanFormatString)]
    [Description("The format string for TimeSpan values.")]
    public string TimeSpanFormatString { get; set; } = DefaultDateTimeFormatString;

    /// <summary>
    ///  Gets or sets the format string for DateTime values.
    /// </summary>
    [DefaultValue(DefaultDateTimeFormatString)]
    [Description("The format string for DateTime values.")]
    public string DateTimeFormatString { get; set; } = DefaultDateTimeFormatString;

    private Font BoldFont => _boldFont ??= new(Font, FontStyle.Bold);

    private DebugMessage[] SelectedDebugMessages
    {
        get
        {
            List<DebugMessage> selectedRows = [];
            foreach (DataGridViewRow row in SelectedRows)
            {
                selectedRows.Add(_debugMessages[row.Index]);
            }

            return [.. selectedRows];
        }
    }
}
