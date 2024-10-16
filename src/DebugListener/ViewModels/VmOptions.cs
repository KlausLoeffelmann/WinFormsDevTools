using CommunityToolkit.Mvvm.ComponentModel;

namespace DebugListener.ViewModels;

public partial class VmOptions : ObservableObject
{
    [ObservableProperty]
    private string _timeSpanFormatString = @"mm'm'\ \:\ ss's'\-ffff";

    [ObservableProperty]
    private string _dateTimeFormatString = "HH:mm:ss-ffff";
}
