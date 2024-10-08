using WinFormsDevToolsLib;
using static WfRuntimeDeploy.WinFormsGitHubRepoManager;

namespace WfRuntimeDeploy.Views;

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
    }
}
