using CommunityToolkit.Mvvm.ComponentModel;

namespace DebugListener.ViewModels;

public partial class VmMain : ObservableObject
{
    [ObservableProperty]
    private VmMenuOptionSettings _menuOptions = new();
}

public partial class VmMenuOptionSettings : ObservableObject
{
    [ObservableProperty]
    private bool _onlyShowExtendedDebugInfo;

    [ObservableProperty]
    private bool _colorProcesses;

    [ObservableProperty]
    private bool _leaveSpaceBetweenProcesses;
}
