using CommunityToolkit.Mvvm.ComponentModel;

namespace DebugListener.ViewModels;

public partial class VmOptions : ObservableObject
{
    [ObservableProperty]
    private string _timeSpanFormatString = @"hh'h'\ mm'm'\ \:\ ss's'\-fff";

    [ObservableProperty]
    private string _dateTimeFormatString = "HH:mm:ss-fff";
}
