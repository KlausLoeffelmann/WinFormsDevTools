using DevTools.RuntimeDeploy.Engine.Domain;
using DevTools.RuntimeDeploy.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Data;
using static DevTools.RuntimeDeploy.Engine.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy.Views;

public partial class DeployRuntimeView : UserControl
{
    private readonly AssetSelectionControl? _assetSelectionControl;
    private RuntimeDeploySettingsService? _settings;
    private RuntimeDeployStatusService? _statusService;
    private ILogger<DeployRuntimeView>? _logger;

    public DeployRuntimeView()
    {
        InitializeComponent();
    }

    public DeployRuntimeView(
        AssetSelectionControl assetSelectionControl,
        RuntimeDeploySettingsService settings,
        RuntimeDeployStatusService statusService,
        ILogger<DeployRuntimeView> logger) : this()
    {
        _assetSelectionControl = assetSelectionControl;
        _assetSelectionControl.Dock = DockStyle.Fill;
        _assetSelectionControl.AvailabilityChanged += (sender, e) => UpdateCommandControlsEnabled();

        // Source/destination date comparison (with red/green + bold coloring
        // for assemblies that will/won't be replaced) is only relevant for
        // the actual copy dialog, not for the "Create Runtime patcher..."
        // package-creation dialog, which hosts the same shared control.
        _assetSelectionControl.ShowDeploymentDateComparison = true;

        _rootLayout.Controls.Add(_assetSelectionControl, 0, 0);

        _settings = settings;
        _statusService = statusService;
        _logger = logger;

        _replaceTargetSDKVersionComboBox.SelectedIndexChanged += (sender, e) => UpdateDeploymentComparisonResolver();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        MainForm mainForm = (MainForm)ParentForm!;
        _replaceTargetSDKVersionComboBox.Items.AddRange(mainForm.SdkTargets);
        if (_replaceTargetSDKVersionComboBox.Items.Count > 0)
        {
            _replaceTargetSDKVersionComboBox.SelectedIndex = _replaceTargetSDKVersionComboBox.Items.Count - 1;
        }

        UpdateDeploymentComparisonResolver();
        UpdateCommandControlsEnabled();
    }

    internal void RefreshFromSettings()
        => _assetSelectionControl?.RefreshFromSettings();

    private void UpdateCommandControlsEnabled()
    {
        bool enable = _assetSelectionControl?.HasAssemblies ?? false;
        _replaceTargetSDKVersionComboBox.Enabled = enable;
        _copyCommandButton.Enabled = enable;
    }

    /// <summary>
    ///  Recomputes the source/destination file resolver used by the asset
    ///  list's date comparison whenever the destination TFM selection
    ///  changes (or on initial load).
    /// </summary>
    private void UpdateDeploymentComparisonResolver()
    {
        if (_assetSelectionControl is null)
        {
            return;
        }

        if (_replaceTargetSDKVersionComboBox.SelectedItem is not TargetFrameworkTargetItem targetFrameworkTarget)
        {
            _assetSelectionControl.SetDeploymentComparisonResolver(null);
            return;
        }

        AssemblyDeploymentTargetResolver.TargetPaths targetPaths = AssemblyDeploymentTargetResolver.GetTargetPaths(targetFrameworkTarget);

        _assetSelectionControl.SetDeploymentComparisonResolver(
            assemblyInfo => AssemblyDeploymentTargetResolver.ResolveComparisonFiles(assemblyInfo, targetPaths));
    }

    private async void CopyCommandButton_Click(object sender, EventArgs e)
    {
        try
        {
        if (_assetSelectionControl is null || !_assetSelectionControl.HasAssemblies)
        {
            // Show a message box if there are no items in the list view.
            MessageBox.Show(
                "No items found in the list view. Please select a runtime version and try again.",
                "No items found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        DesktopAssemblyInfo? firstItem = _assetSelectionControl.FindFirstAssembly();

        if (firstItem is null)
        {
            MessageBox.Show(
                "No assemblies were found for the selected runtime version.",
                "No items found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        if (_replaceTargetSDKVersionComboBox.SelectedItem is not TargetFrameworkTargetItem targetFrameworkTarget)
        {
            return;
        }

        // Get the source file directories from the first item in the list view.
        DirectoryInfo sourceAssemblyBasePath = firstItem.AssemblyFiles[0].Directory!;
        DirectoryInfo? sourceRefAssemblyBasePath = firstItem.RefAssemblyFiles?.FirstOrDefault()?.Directory;

        // Snapshot every piece of UI state the background work needs onto plain
        // locals on the UI thread. After this point Task.Run no longer touches any
        // control directly; UI writes happen through Control.InvokeAsync.
        bool dryRun = _dryRunCheckBox.Checked;
        DesktopAssemblyInfo[] checkedAssemblies = _assetSelectionControl.GetCheckedAssemblies();

        _copyCommandButton.Enabled = false;
        CommandBatch commandBatch = new();

        var batchTask = commandBatch.StartBatchAsync(
            windowTitle: "Copy .NET Desktop runtime assemblies",
            showCommandBatchWindow: true,
            dryRun: dryRun,
            outputFont: _settings?.OutputFont,
            settingsService: _settings);

        var processTask = Task.Run(async () =>
        {
            AssemblyDeploymentTargetResolver.TargetPaths targetPaths = AssemblyDeploymentTargetResolver.GetTargetPaths(targetFrameworkTarget);
            DirectoryInfo targetSharedAssemblyBasePath = targetPaths.TargetSharedAssemblyBasePath;
            DirectoryInfo targetRefAssemblyBasePath = targetPaths.TargetRefAssemblyBasePath;
            DirectoryInfo targetRefAssemblyPath = targetPaths.TargetRefAssemblyPath;
            DirectoryInfo packageAssembliesManifestPath = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\{targetFrameworkTarget.Name}\\data");

            DirectoryInfo analyzersDir = targetPaths.AnalyzersDir;
            DirectoryInfo cSharpAnalyzersDir = targetPaths.CSharpAnalyzersDir;
            DirectoryInfo visualBasicAnalyzersDir = targetPaths.VisualBasicAnalyzersDir;

            // Load the manifest once for the whole batch and save once at the
            // end (the old code re-loaded and re-saved per assembly).
            string manifestPath = $"{packageAssembliesManifestPath.FullName}\\FrameworkList.xml";
            FrameworkListManifestEditor? manifestEditor;
            try
            {
                manifestEditor = new FrameworkListManifestEditor(manifestPath);
            }
            catch (Exception ex)
            {
                await commandBatch.WriteLineErrorAsync(
                    $"Could not load FrameworkList manifest '{manifestPath}': {ex.Message}");
                await commandBatch.EndBatchAsync("End of Command Batch.");
                await InvokeAsync(() => _copyCommandButton.Enabled = true);
                return;
            }

            await commandBatch.WriteLineInfoAsync($"Destination Assembly directory:{targetSharedAssemblyBasePath}");
            await commandBatch.WriteLineInfoAsync($"Destination REF-Assembly directory:{targetRefAssemblyPath.FullName}");
            await commandBatch.WriteLineInfoAsync($"Destination Analyzers directory:{analyzersDir.FullName}");
            await commandBatch.WriteLineInfoAsync($"");

            await commandBatch.WriteLineInfoAsync($"Source Assembly directory:{sourceAssemblyBasePath}");
            if (sourceRefAssemblyBasePath is not null)
            {
                await commandBatch.WriteLineInfoAsync($"Source RefAssembly directory:{sourceRefAssemblyBasePath}");
            }

            await commandBatch.WriteLineInfoAsync($"");

            DirectoryInfo targetDir;

            // Create a HashSet to store the processed files.
            HashSet<FileInfo> processedFiles = [];

            foreach (DesktopAssemblyInfo assemblyInfo in checkedAssemblies)
            {
                bool vbFirst = false, csFirst = false;

                foreach (FileInfo fileItem in assemblyInfo.AssemblyFiles)
                {
                    // Check if the file has already been processed
                    if (processedFiles.Contains(fileItem))
                    {
                        continue;
                    }

                    // Add the file to the processed files HashSet
                    processedFiles.Add(fileItem);

                    // Determine the file type based on the file name without extension
                    string fileName = Path.GetFileNameWithoutExtension(fileItem.Name);
                    string currentFileType = AssemblyFileTypeClassifier.Classify(fileName);

                    // Uses the same resolution logic as the "will be replaced" date
                    // comparison shown in the asset list, so the two never drift apart.
                    targetDir = AssemblyDeploymentTargetResolver.GetAssemblyTargetDirectory(fileItem.Name, targetPaths);

                    if (fileItem.Name.StartsWith("System.Windows.Forms.Analyzers"))
                    {
                        if (targetDir == visualBasicAnalyzersDir && !vbFirst)
                        {
                            vbFirst = true;

                            // Create the vb subfolder in the analyzers directory if it does not exist:
                            if (!Directory.Exists(visualBasicAnalyzersDir.FullName))
                            {
                                Directory.CreateDirectory(visualBasicAnalyzersDir.FullName);
                            }
                        }
                        else if (targetDir == cSharpAnalyzersDir && !csFirst)
                        {
                            csFirst = true;

                            // Create the subfolder "cs" in the analyzers directory if it does not exist:
                            if (!Directory.Exists($"{cSharpAnalyzersDir}"))
                            {
                                Directory.CreateDirectory(cSharpAnalyzersDir.FullName);
                            }
                        }

                        // Update the AssemblyInfo.xml file with the assembly information.
                        AssemblyManifestProcessResult result = UpdateAssemblyInfo(
                            manifestEditor: manifestEditor,
                            destinationAssemblyFileInfo: (targetRefAssemblyBasePath, new FileInfo($"{targetDir}\\{fileItem.Name}")),
                            fileType: currentFileType,
                            targetFrameworkVersion: targetFrameworkTarget.Name,
                            updatePublicKey: false);

                        if (await ProcessManifestResult(commandBatch, fileItem, result))
                        {
                            continue;
                        }
                    }

                    await commandBatch.CopyFileCommandAsync(
                        fileItem,
                        targetDir,
                        overrideIfExist: true);
                }

                if (assemblyInfo.RefAssemblyFiles is not null)
                {
                    foreach (FileInfo fileItem in assemblyInfo.RefAssemblyFiles)
                    {
                        // Check if the file has already been processed
                        if (processedFiles.Contains(fileItem))
                        {
                            continue;
                        }

                        // Add the file to the processed files HashSet (mirror the non-ref-assembly
                        // loop above: mark as processed BEFORE invoking manifest logic so a skip
                        // from ProcessManifestResult does not cause the same file to be inspected
                        // again later in the iteration).
                        processedFiles.Add(fileItem);

                        // Determine the file type for ref assembly
                        string fileName = Path.GetFileNameWithoutExtension(fileItem.Name);
                        string currentFileType = AssemblyFileTypeClassifier.Classify(fileName);

                        // Update the AssemblyInfo.xml file with the assembly information.
                        AssemblyManifestProcessResult result = UpdateAssemblyInfo(
                            manifestEditor: manifestEditor,
                            destinationAssemblyFileInfo: (targetRefAssemblyBasePath, new FileInfo($"{targetRefAssemblyPath}\\{fileItem.Name}")),
                            fileType: currentFileType,
                            targetFrameworkVersion: targetFrameworkTarget.Name,
                            updatePublicKey: false);

                        if (await ProcessManifestResult(commandBatch, fileItem, result))
                        {
                            continue;
                        }

                        await commandBatch.CopyFileCommandAsync(
                            fileItem,
                            targetRefAssemblyPath,
                            overrideIfExist: true,
                            comment: "REF: ");
                    }
                }

                await InvokeAsync(
                    () => _assetSelectionControl.RefreshDeploymentDateComparison(assemblyInfo));
            }

            if (checkedAssemblies.Length == 0)
            {
                await commandBatch.WriteLineWarningAsync("No items were selected, found nothing to copy.");
            }

            // Persist all manifest mutations performed during the batch.
            try
            {
                manifestEditor.Save();
            }
            catch (Exception ex)
            {
                await commandBatch.WriteLineErrorAsync(
                    $"Failed to write FrameworkList manifest '{manifestPath}': {ex.Message}");
            }

            await commandBatch.EndBatchAsync("End of Command Batch.");

            await InvokeAsync(() => _copyCommandButton.Enabled = true);
        });

        await Task.WhenAll(batchTask, processTask);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Copy command failed.");
            _statusService?.ReportException(ex);
            _copyCommandButton.Enabled = true;
        }

        static async Task<bool> ProcessManifestResult(
            CommandBatch commandBatch, 
            FileInfo fileItem, 
            AssemblyManifestProcessResult result)
        {
            var (resultLogString, skipOperation) = result switch
            {
                AssemblyManifestProcessResult.MissingAssembly => ("Missing Assembly", true),
                AssemblyManifestProcessResult.InvalidAssembly => ("Invalid Assembly", true),
                AssemblyManifestProcessResult.MissingPublicKey => ("Missing Public Key", true),
                AssemblyManifestProcessResult.InvalidXmlFile => ("Invalid XML File", true),
                AssemblyManifestProcessResult.PublicKeyDoesNotMatch => ("Public Key does not match", true),
                AssemblyManifestProcessResult.OK => ("OK", false),
                AssemblyManifestProcessResult.PublicKeyUpdated => ("Public Key Updated", false),
                AssemblyManifestProcessResult.Created => ("Created", false),
                _ => ("Unknown", true)
            };

            if (skipOperation)
            {
                await commandBatch.WriteLineWarningAsync(
                    $"Skipping {fileItem.Name} - {resultLogString}");
            }

            return skipOperation;
        }
    }

    private static AssemblyManifestProcessResult UpdateAssemblyInfo(
        FrameworkListManifestEditor manifestEditor,
        (DirectoryInfo targetBasePath, FileInfo targetFile) destinationAssemblyFileInfo,
        string fileType,
        string targetFrameworkVersion,
        bool updatePublicKey)
    {
        if (!destinationAssemblyFileInfo.targetFile.Exists)
        {
            return AssemblyManifestProcessResult.MissingAssembly;
        }

        AssemblyProbeResult? probe = AssemblyProbe.TryRead(destinationAssemblyFileInfo.targetFile.FullName);
        if (probe is null)
        {
            return AssemblyManifestProcessResult.InvalidAssembly;
        }

        if (string.IsNullOrEmpty(probe.PublicKeyTokenHex))
        {
            return AssemblyManifestProcessResult.MissingPublicKey;
        }

        // Path of the destination file relative to the ref-pack base, in Windows
        // backslash form. The editor converts to forward-slash for the manifest.
        string deltaPath = destinationAssemblyFileInfo.targetFile.FullName
            .Replace(destinationAssemblyFileInfo.targetBasePath.FullName, string.Empty)
            .TrimStart('\\');

        FrameworkListEntry entry = new(
            FileType: fileType,
            RelativePath: deltaPath,
            AssemblyName: probe.Name,
            PublicKeyToken: probe.PublicKeyTokenHex,
            AssemblyVersion: FrameworkVersionFormatter.ToMajorOnly(targetFrameworkVersion, probe.Version),
            FileVersion: FrameworkVersionFormatter.ToMajorOnly(targetFrameworkVersion, NormalizeFileVersion(probe.FileVersion)),
            Profile: "WindowsForms");

        return manifestEditor.Upsert(entry, updatePublicKey);

        static string NormalizeFileVersion(string? fileVersion)
        {
            string version = fileVersion ?? FrameworkVersionFormatter.FailedReadSentinel;

            // Defensive: clamp to a clean dotted-numeric "a.b.c.d" form. FileVersion may carry
            // a build-suffix like "9.6.4-dev"; ToMajorOnly handles both shapes, but capping here
            // preserves the historical behaviour of the deleted GetFileVersion helper.
            string[] parts = version.Split('.');
            if (parts.Length >= 4)
            {
                version = $"{parts[0]}.{parts[1]}.{parts[2]}.{parts[3]}";
            }

            return version;
        }
    }
}
