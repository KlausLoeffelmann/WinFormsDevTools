using DevTools.RuntimeDeploy.Domain;
using DevTools.RuntimeDeploy.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WinForms;
using System.ComponentModel;
using WarpToolkit.ComponentModel;
using static DevTools.RuntimeDeploy.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy;

public partial class MainForm : Form
{
    private readonly DeployRuntimeView _deployRuntimeView;
    private readonly OverView _overView;

    public MainForm()
    {
        InitializeComponent();

        UserSettings = WinFormsApplication.Services.GetRequiredService<IUserSettingsService>();

        _overView = new OverView();
        _deployRuntimeView = new DeployRuntimeView();

        _tabControl.AddTab("Overview", _overView);
        _tabControl.AddTab("Deploy Runtime", _deployRuntimeView);
        _tabControl.TabChanged += TabControl_TabChanged;

        SdkFolders = FrameworkInfo.GetDotNetDesktopSdk(false);

        if (SdkFolders is null)
        {
            return;
        }

        SdkTargets = [.. SdkFolders
            .Values
            .Select(item => new TargetFrameworkTargetItem()
            {
                Name = item.Name,
                PathFullName = item.FullName,
                Directory = item
            })];
    }

    private void TabControl_TabChanged(object? sender, EventArgs e)
    {
    }

    /// <summary>
    ///  Per-user settings service, resolved from the DI container set up
    ///  by <c>WinFormsApplication.CreateBuilder</c> in <c>Program.Main</c>.
    ///  Hosted UserControls reach it through this property via their
    ///  <c>ParentForm</c> cast.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IUserSettingsService UserSettings { get; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Dictionary<string, DirectoryInfo>? SdkFolders { get; private set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal TargetFrameworkTargetItem[] SdkTargets { get; private set; } = null!;
}
