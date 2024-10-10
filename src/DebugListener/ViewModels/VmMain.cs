using CommunityToolkit.Mvvm.ComponentModel;

namespace DebugListener.ViewModels;

internal partial class VmMain : ObservableObject
{
    [ObservableProperty]
    private VmMenuOptionSettings _menuOptions = new();
}

internal partial class VmMenuOptionSettings : ObservableObject
{
    [ObservableProperty]
    private bool _onlyShowExtendedDebugInfo;

    [ObservableProperty]
    private bool _colorProcesses;

    [ObservableProperty]
    private bool _leaveSpaceBetweenProcesses;
}
