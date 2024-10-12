using DevTools.RuntimeDeploy.Views;
using System.ComponentModel;
using WinFormsDevToolsLib;
using static DevTools.RuntimeDeploy.WinFormsGitHubRepoManager;

namespace DevTools.RuntimeDeploy;

public partial class MainForm : Form
{
    private readonly DeployRuntimeView _deployRuntimeView;
    private readonly OverView _overView;

    public MainForm()
    {
        InitializeComponent();

        SdkFolders = FrameworkInfo.GetDotNetDesktopSdk(false);

        if (SdkFolders is null)
        {
            return;
        }

        SdkTargets = SdkFolders
            .Values
            .Select(item => new TargetFrameworkTargetItem()
            {
                Name = item.Name,
                PathFullName = item.FullName,
                Directory = item
            })
            .ToArray();

        _overView = new OverView();
        _deployRuntimeView = new DeployRuntimeView();

        _tabControl.AddTab("Overview", _overView);
        _tabControl.AddTab("Deploy Runtime", _deployRuntimeView);
        _tabControl.TabChanged += TabControl_TabChanged;
    }

    private void TabControl_TabChanged(object? sender, EventArgs e)
    {
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Dictionary<string, DirectoryInfo>? SdkFolders { get; private set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal TargetFrameworkTargetItem[] SdkTargets { get; private set; } = null!;
}
