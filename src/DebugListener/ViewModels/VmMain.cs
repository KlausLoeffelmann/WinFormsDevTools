using CommunityToolkit.Mvvm.ComponentModel;

namespace DebugListener.ViewModels;

public partial class VmMain : ObservableObject
{
    [ObservableProperty]
    private VmMenuOptionSettings _menuOptions = new()
    {
        ColorProcesses = true,
        OnlyShowExtendedDebugInfo = true,
        LeaveSpaceBetweenProcesses = true
    };

    [ObservableProperty]
    private VmOptions _options = new();
}
