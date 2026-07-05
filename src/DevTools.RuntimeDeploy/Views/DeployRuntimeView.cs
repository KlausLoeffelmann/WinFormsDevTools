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

    private const string VisualBasicSubfolderPath = "vb";
    private const string CSharpSubfolderPath = "cs";

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
        _rootLayout.Controls.Add(_assetSelectionControl, 0, 0);

        _settings = settings;
        _statusService = statusService;
        _logger = logger;
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

        // Find the first assembly (checked or not) whose Tag has a list of
        // RefAssemblies with at least one item.
        DesktopAssemblyInfo? firstItem = _assetSelectionControl.FindFirstWithRefAssemblies();

        if (firstItem is null)
        {
            // Show a message box if there are no items with RefAssemblies in the list view.
            MessageBox.Show(
                "No items found in the list view with RefAssemblies. Please select a runtime version and try again.",
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
        DirectoryInfo sourceRefAssemblyBasePath = default!;

        if (firstItem.RefAssemblyFiles is not null)
        {
            sourceRefAssemblyBasePath = firstItem.RefAssemblyFiles[0].Directory!;
        }

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
            outputFont: _settings?.OutputFont);

        var processTask = Task.Run(async () =>
        {
            DirectoryInfo targetSharedAssemblyBasePath = new($"{FrameworkInfo.NetDesktopLibsDirectory}\\{targetFrameworkTarget.Name}");
            DirectoryInfo targetRefAssemblyBasePath = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\" + $"{targetFrameworkTarget.Name}");
            DirectoryInfo targetRefAssemblyPath = new($"{targetRefAssemblyBasePath}\\ref\\net{FrameworkVersionFormatter.ToMajorMinor(targetFrameworkTarget.Name)}");
            DirectoryInfo packageAssembliesManifestPath = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\{targetFrameworkTarget.Name}\\data");

            // Create a new DirectoryInfo for the analyzers directory, which is the same as the ref directory
            // but with the last part of the path changed to "analyzers".
            DirectoryInfo analyzersDir = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\{targetFrameworkTarget.Name}\\analyzers\\dotnet");
            DirectoryInfo cSharpAnalyzersDir = new($"{analyzersDir.FullName}\\{CSharpSubfolderPath}");
            DirectoryInfo visualBasicAnalyzersDir = new($"{analyzersDir.FullName}\\{VisualBasicSubfolderPath}");

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
            await commandBatch.WriteLineInfoAsync($"Source RefAssembly directory:{sourceRefAssemblyBasePath}\\ref");
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

                    // If the file starts with "System.Windows.Forms.Analyzers", copy it to the analyzers directory.
                    // But. If the file ends with "VisualBasic.dll", we need to copy it in the SubFolder "\\vb", and
                    // if it ends with "CSharp.dll", we need to copy it in the SubFolder "\\cs".
                    if (!fileItem.Name.StartsWith("System.Windows.Forms.Analyzers"))
                    {
                        targetDir = targetSharedAssemblyBasePath;
                    }
                    else
                    {
                        if (fileItem.Name.EndsWith("VisualBasic.dll"))
                        {
                            if (!vbFirst)
                            {
                                vbFirst = true;

                                // Create the vb subfolder in the analyzers directory if it does not exist:
                                if (!Directory.Exists(visualBasicAnalyzersDir.FullName))
                                {
                                    Directory.CreateDirectory(visualBasicAnalyzersDir.FullName);
                                }
                            }

                            targetDir = visualBasicAnalyzersDir;

                        }
                        else if (fileItem.Name.EndsWith("CSharp.dll"))
                        {
                            if (!csFirst)
                            {
                                csFirst = true;

                                // Create the subfolder "cs" in the analyzers directory if it does not exist:
                                if (!Directory.Exists($"{cSharpAnalyzersDir}"))
                                {
                                    Directory.CreateDirectory(cSharpAnalyzersDir.FullName);
                                }
                            }

                            targetDir = cSharpAnalyzersDir;
                        }
                        else
                        {
                            targetDir = analyzersDir;
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

                if (assemblyInfo.RefAssemblyFiles is null)
                {
                    continue;
                }

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
