using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DebugListener.ViewModels;

public partial class VmMenuOptionSettings : ObservableObject
{
    [ObservableProperty]
    private bool _onlyShowExtendedDebugInfo;

    [ObservableProperty]
    private bool _colorProcesses;

    [ObservableProperty]
    private bool _leaveSpaceBetweenProcesses;

    [ObservableProperty]
    private bool _totalSecondsTimestamps;

    [RelayCommand]
    public void ToggleOnlyShowExtendedDebugInfo()
        => OnlyShowExtendedDebugInfo = !OnlyShowExtendedDebugInfo;

    [RelayCommand]
    public void ToggleColorProcesses()
        => ColorProcesses = !ColorProcesses;

    [RelayCommand]
    public void ToggleLeaveSpaceBetweenProcesses()
        => LeaveSpaceBetweenProcesses = !LeaveSpaceBetweenProcesses;

    [RelayCommand]
    public void ToggleTotalSecondsTimestamps()
        => TotalSecondsTimestamps = !TotalSecondsTimestamps;
}
