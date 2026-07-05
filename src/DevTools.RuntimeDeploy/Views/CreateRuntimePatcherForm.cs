using DevTools.RuntimeDeploy.Engine.Domain;
using DevTools.RuntimeDeploy.Engine.Packaging;
using DevTools.RuntimeDeploy.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using static DevTools.RuntimeDeploy.Engine.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy.Views;

/// <summary>
///  "Create Runtime patcher..." modal dialog: hosts the shared
///  <see cref="AssetSelectionControl"/> picker UI (source folder, target
///  framework/configuration, assemblies) next to a "Create package
///  installer..." action panel instead of the direct-copy "Copy" button used
///  by <see cref="DeployRuntimeView"/>.
/// </summary>
public partial class CreateRuntimePatcherForm : Form
{
    private const string PackageFileName = "RuntimePatch.netdeploy";
    private const string CliExeFileName = "RuntimePatcher.exe";

    private readonly AssetSelectionControl? _assetSelectionControl;
    private RuntimeDeployStatusService? _statusService;
    private ILogger<CreateRuntimePatcherForm>? _logger;

    public CreateRuntimePatcherForm()
    {
        InitializeComponent();
    }

    public CreateRuntimePatcherForm(
        AssetSelectionControl assetSelectionControl,
        RuntimeDeployStatusService statusService,
        ILogger<CreateRuntimePatcherForm> logger) : this()
    {
        _assetSelectionControl = assetSelectionControl;
        _assetSelectionControl.Dock = DockStyle.Fill;
        _assetSelectionControl.AvailabilityChanged += (sender, e) => UpdateActionControlsEnabled();
        _rootLayout.Controls.Add(_assetSelectionControl, 0, 0);

        _statusService = statusService;
        _logger = logger;

        UpdateActionControlsEnabled();
    }

    private void UpdateActionControlsEnabled()
        => _createPackageButton.Enabled = _assetSelectionControl?.HasAssemblies ?? false;

    private async void CreatePackageButton_Click(object sender, EventArgs e)
    {
        if (_assetSelectionControl is null || !_assetSelectionControl.HasAssemblies)
        {
            return;
        }

        TargetFrameworkSourceItem? sourceTarget = _assetSelectionControl.SelectedSourceTarget;
        if (sourceTarget is null)
        {
            return;
        }

        // TargetFrameworkSourceItem.Name has the shape "Configuration - net-tfm"
        // (see BuildArtefactsScanner.GetAvailableTargets).
        string[] nameParts = sourceTarget.Name.Split(" - ", 2, StringSplitOptions.TrimEntries);
        string configuration = nameParts.Length > 0 ? nameParts[0] : "Release";
        string tfm = nameParts.Length > 1 ? nameParts[1] : sourceTarget.Name;

        DesktopAssemblyInfo[] checkedAssemblies = _assetSelectionControl.GetCheckedAssemblies();
        FileInfo[] filesToPackage =
        [
            .. checkedAssemblies.SelectMany(assembly => assembly.AssemblyFiles)
        ];

        if (filesToPackage.Length == 0)
        {
            MessageBox.Show(
                this,
                "No assemblies are checked. Please check at least one assembly to package.",
                "Nothing to package",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        using FolderBrowserDialog folderDialog = new()
        {
            Description = "Pick the folder where the runtime patcher installer should be created:"
        };

        if (folderDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        FileInfo? cliExe = CliBundleLocator.TryFindPublishedCli();
        if (cliExe is null)
        {
            MessageBox.Show(
                this,
                "Could not find a published RuntimePatcher.exe next to this project. " +
                "Run 'dotnet publish -c Release' on DevTools.RuntimeDeploy.Cli first.",
                "Runtime Patcher CLI not found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        _createPackageButton.Enabled = false;

        try
        {
            DirectoryInfo outputDirectory = new(folderDialog.SelectedPath);
            FileInfo packageFile = new(Path.Combine(outputDirectory.FullName, PackageFileName));

            await PackageBuilder.CreatePackageAsync(filesToPackage, tfm, configuration, packageFile);

            File.Copy(cliExe.FullName, Path.Combine(outputDirectory.FullName, CliExeFileName), overwrite: true);

            RuntimePatcherSettings settings = new(
                PackageFileName: packageFile.Name,
                DryRun: false,
                Yes: false);

            string settingsPath = Path.Combine(outputDirectory.FullName, RuntimePatcherSettings.DefaultFileName);
            await using (FileStream settingsStream = File.Create(settingsPath))
            {
                await JsonSerializer.SerializeAsync(
                    settingsStream,
                    settings,
                    Engine.Json.EngineJsonContext.Default.RuntimePatcherSettings);
            }

            _statusService?.ReportInfo($"Runtime patcher installer created in '{outputDirectory.FullName}'.");

            MessageBox.Show(
                this,
                $"Created runtime patcher installer in:\n{outputDirectory.FullName}\n\n" +
                $"Includes {filesToPackage.Length} assembly file(s) for {tfm} ({configuration}).",
                "Runtime patcher installer created",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to create runtime patcher installer.");
            _statusService?.ReportException(ex);
            MessageBox.Show(this, ex.Message, "Could not create installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _createPackageButton.Enabled = true;
        }
    }
}
