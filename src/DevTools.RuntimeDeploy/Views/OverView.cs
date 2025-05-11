using DevTools.Libs;
using static DevTools.RuntimeDeploy.WinFormsBuildArtefactsManager;

namespace DevTools.RuntimeDeploy.Views;

public partial class OverView : UserControl
{
    public OverView()
    {
        InitializeComponent();
        _netDesktopSdksListView.ConfigureDetailsView();
    }

    override protected void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        MainForm mainForm = (MainForm)ParentForm!;

        _netDesktopSdksListView.AddItemsWithColumnHeadersFromType(
            mainForm.SdkTargets!,
            addSourceDataToTag: true,
            (nameof(TargetFrameworkTargetItem.Name), ".NET SDK Version"),
            (nameof(TargetFrameworkTargetItem.PathFullName), "Path"));

        _pscWinFormsGitHubRepo.PathFullName = Properties.Settings.Default.PathToWinFormsGitHubRepo ?? "- Not defined yet. -";
        _pscNetSdkAssemblies.PathFullName = FrameworkInfo.NetDesktopLibsDirectory.FullName;
        _pscNewSdkRefAssemblies.PathFullName = FrameworkInfo.NetDesktopRefsDirectory.FullName;
    }
}
