using DevTools.RuntimeDeploy.Engine.Domain;
using DevTools.RuntimeDeploy.Infrastructure;
using Microsoft.Extensions.Logging;
using static DevTools.RuntimeDeploy.Engine.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy.Views;

public partial class OverView : UserControl
{
    private RuntimeDeploySettingsService? _settings;
    private ILogger<OverView>? _logger;

    public OverView()
    {
        InitializeComponent();
        _netDesktopSdksListView.ConfigureDetailsView();
    }

    public OverView(RuntimeDeploySettingsService settings, ILogger<OverView> logger) : this()
    {
        _settings = settings;
        _logger = logger;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        RefreshFromSettings();
    }

    internal void RefreshFromSettings()
    {
        if (ParentForm is not MainForm mainForm)
        {
            return;
        }

        _netDesktopSdksListView.Items.Clear();
        _netDesktopSdksListView.AddItemsWithColumnHeadersFromType(
            mainForm.SdkTargets!,
            addSourceDataToTag: true,
            (nameof(TargetFrameworkTargetItem.Name), ".NET SDK Version"),
            (nameof(TargetFrameworkTargetItem.PathFullName), "Path"));

        _pscWinFormsGitHubRepo.FileOrFolderPath = _settings?.SourceArtefactsFolder ?? "- Not defined yet. -";
        _pscNetSdkAssemblies.FileOrFolderPath = FrameworkInfo.NetDesktopLibsDirectory.FullName;
        _pscNewSdkRefAssemblies.FileOrFolderPath = FrameworkInfo.NetDesktopRefsDirectory.FullName;

        _logger?.LogDebug("Overview refreshed from RuntimeDeploy settings.");
    }
}
