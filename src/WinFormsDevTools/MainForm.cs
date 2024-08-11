using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;
using WinFormsToolLib;

using static WinFormsDevTools.WinFormsGitHubRepoManager;

namespace WinFormsDevTools;

public partial class MainForm : Form
{
    private readonly Control[] _controlsForEnablingHandling;
    private WinFormsGitHubRepoManager? _gitHubRepoManager;

    private const string ACCESSIBILITY = "Accessibility";
    private const string MICROSOFT_VISUALBASIC = "Microsoft.VisualBasic";
    private const string MICROSOFT_VISUALBASIC_FACADE = "Microsoft.VisualBasic.Facade";
    private const string MICROSOFT_VISUALBASIC_FORMS = "Microsoft.VisualBasic.Forms";
    private const string SYSTEM_DESIGN_FACADE = "System.Design.Facade";
    private const string SYSTEM_DRAWING_COMMON = "System.Drawing.Common";
    private const string SYSTEM_DRAWING_DESIGN_FACADE = "System.Drawing.Design.Facade";
    private const string SYSTEM_DRAWING_FACADE = "System.Drawing.Facade";
    private const string SYSTEM_PRIVATE_WINDOWS_CORE = "System.Private.Windows.Core";
    private const string SYSTEM_WINDOWS_FORMS = "System.Windows.Forms";
    private const string SYSTEM_WINDOWS_FORMS_ANALYZERS = "System.Windows.Forms.Analyzers";
    private const string SYSTEM_WINDOWS_FORMS_ANALYZERS_CSHARP = "System.Windows.Forms.Analyzers.CSharp";
    private const string SYSTEM_WINDOWS_FORMS_ANALYZERS_VISUALBASIC = "System.Windows.Forms.Analyzers.VisualBasic";
    private const string SYSTEM_WINDOWS_FORMS_ANALYZERS_CODEFIXES_CSHARP = "System.Windows.Forms.Analyzers.CodeFixes.CSharp";
    private const string SYSTEM_WINDOWS_FORMS_ANALYZERS_CODEFIXES_VISUALBASIC = "System.Windows.Forms.Analyzers.CodeFixes.VisualBasic";
    private const string SYSTEM_WINDOWS_FORMS_DESIGN = "System.Windows.Forms.Design";
    private const string SYSTEM_WINDOWS_FORMS_PRIMITIVES = "System.Windows.Forms.Primitives";
    private const string SYSTEM_WINDOWS_FORMS_PRIVATESOURCEGENERATORS = "System.Windows.Forms.PrivateSourceGenerators";

    private const string VisualBasicSubfolderPath = "vb";
    private const string CSharpSubfolderPath = "cs";

    private readonly string[] s_preCheckItems =
    [
        ACCESSIBILITY,
        MICROSOFT_VISUALBASIC,
        MICROSOFT_VISUALBASIC_FACADE,
        MICROSOFT_VISUALBASIC_FORMS,
        SYSTEM_DESIGN_FACADE,
        SYSTEM_DRAWING_COMMON,
        SYSTEM_DRAWING_DESIGN_FACADE,
        SYSTEM_DRAWING_FACADE,
        SYSTEM_PRIVATE_WINDOWS_CORE,
        SYSTEM_WINDOWS_FORMS,
        SYSTEM_WINDOWS_FORMS_ANALYZERS,
        SYSTEM_WINDOWS_FORMS_ANALYZERS_CSHARP,
        SYSTEM_WINDOWS_FORMS_ANALYZERS_VISUALBASIC,
        SYSTEM_WINDOWS_FORMS_ANALYZERS_CODEFIXES_CSHARP,
        SYSTEM_WINDOWS_FORMS_ANALYZERS_CODEFIXES_VISUALBASIC,
        SYSTEM_WINDOWS_FORMS_DESIGN,
        SYSTEM_WINDOWS_FORMS_PRIMITIVES,
        SYSTEM_WINDOWS_FORMS_PRIVATESOURCEGENERATORS
    ];

    public MainForm()
    {
        InitializeComponent();

        _pathToArtefactsRepoTextBox.TextChanged +=
            (sender, e) => DeployAvailableRuntimes();

        _availableDesktopRuntimesComboBox.SelectedIndexChanged +=
            (sender, e) => DeployAvailableAssemblies();

        _controlsForEnablingHandling =
        [
            _availableDesktopRuntimesComboBox,
            _checkForRespectiveRefAssembliesCheckBox,
            _availableAssembliesListView,
            _replaceTargetSDKVersionComboBox,
            _copyCommandButton
        ];
    }

    protected override void OnLoad(EventArgs e)
    {
        var sdkFolders = FrameworkInfo.GetDotNetDesktopSdk(false);

        if (sdkFolders is null)
        {
            return;
        }

        TargetFrameworkTargetItem[] sdkTargets = sdkFolders
            .Values
            .Select(item => new TargetFrameworkTargetItem()
            {
                Name = item.Name,
                PathFullName = item.FullName,
                Directory = item
            })
            .ToArray();

        _netDesktopSdksListView.ConfigureDetailsView();

        _netDesktopSdksListView.AddItemsWithColumnHeadersFromType(
            sdkTargets,
            addSourceDataToTag: true,
            (nameof(TargetFrameworkTargetItem.Name), ".NET SDK Version"),
            (nameof(TargetFrameworkTargetItem.PathFullName), "Path"));

        _replaceTargetSDKVersionComboBox.Items.AddRange(sdkTargets);
        _replaceTargetSDKVersionComboBox.SelectedIndex = _replaceTargetSDKVersionComboBox.Items.Count - 1;

        SetupControls_DeployRuntimeBinaries_Tab();
    }

    private void SetupControls_DeployRuntimeBinaries_Tab()
        => _pathToArtefactsRepoTextBox.Text = Properties.Settings.Default.PathToWinFormsGitHubRepo;

    private void HandleControlEnabling_DeployRuntimeBinariesTab(bool enable, params Control[] excludeControlsForHandling)
    {
        foreach (var control in _controlsForEnablingHandling)
        {
            if (!excludeControlsForHandling.Contains(control))
            {
                control.Enabled = enable;
            }
        }
    }

    private void DeployAvailableRuntimes()
    {
        if (string.IsNullOrWhiteSpace(_pathToArtefactsRepoTextBox.Text))
        {
            HandleControlEnabling_DeployRuntimeBinariesTab(false);
        }
        else
        {
            _gitHubRepoManager = new(_pathToArtefactsRepoTextBox.Text);

            var targets = _gitHubRepoManager
                .GetAvailableTargets();

            _availableDesktopRuntimesComboBox.Items.AddRange(targets);
            _availableDesktopRuntimesComboBox.SelectedIndex = targets.Length - 1;
        }
    }

    private void DeployAvailableAssemblies()
    {
        if (_availableDesktopRuntimesComboBox.SelectedItem is null ||
            _gitHubRepoManager is null)
        {
            return;
        }

        var assemblies = _gitHubRepoManager.GetWinFormsRuntimeAssemblies(
            (TargetFrameworkSourceItem)_availableDesktopRuntimesComboBox.SelectedItem,
            _checkForRespectiveRefAssembliesCheckBox.Checked);

        _availableAssembliesListView.ConfigureDetailsView(checkBoxes: true);

        _availableAssembliesListView.AddItemsWithColumnHeadersFromType(
            assemblies,
            addSourceDataToTag: true,
            (nameof(DesktopAssemblyInfo.Name), "Assembly name"),
            (nameof(DesktopAssemblyInfo.Path), "Path"));

        _availableAssembliesListView.CheckItemsInFirstColumn(s_preCheckItems);
    }

    private void PickPathToArtefactsButton_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog browserDialog = new()
        {
            Description = "Pick the path to the WinForms GitHub repo:"
        };

        DialogResult dialogResult = browserDialog.ShowDialog();
        if (dialogResult == DialogResult.OK)
        {
            _pathToArtefactsRepoTextBox.Text = browserDialog.SelectedPath;
            Properties.Settings.Default.PathToWinFormsGitHubRepo = browserDialog.SelectedPath;
            Properties.Settings.Default.Save();
        }
    }

    private async void CopyCommandButton_Click(object sender, EventArgs e)
    {
        if (_availableAssembliesListView.Items.Count == 0)
        {
            // Show a message box if there are no items in the list view.
            MessageBox.Show(
                "No items found in the list view. Please select a runtime version and try again.",
                "No items found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        // Find the first item in _availableAssembliesListView.Items, whose Tag has a list
        // of RefAssemblies with at least one item:
        DesktopAssemblyInfo? firstItem = (from ListViewItem item in _availableAssembliesListView.Items
                                          let assemblyInfo = (DesktopAssemblyInfo)item.Tag!
                                          where assemblyInfo.RefAssemblyFiles is not null 
                                                && assemblyInfo.RefAssemblyFiles.Length > 0
                                          select assemblyInfo).FirstOrDefault();

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

        // Get the source file directories from the first item in the list view.
        DirectoryInfo sourceAssemblyBasePath = firstItem.AssemblyFiles[0].Directory!;
        DirectoryInfo sourceRefAssemblyBasePath = default!;

        if (firstItem.RefAssemblyFiles is not null)
        {
            sourceRefAssemblyBasePath = firstItem.RefAssemblyFiles[0].Directory!;
        }

        _copyCommandButton.Enabled = false;
        CommandBatch commandBatch = new();

        if (_replaceTargetSDKVersionComboBox.SelectedItem is null)
        {
            return;
        }

        await commandBatch.StartBatchAsync(
            windowTitle: "Copy .NET Desktop runtime assemblies",
            showCommandBatchWindow: true,
            dryRun: _dryRunCheckBox.Checked);

        TargetFrameworkTargetItem targetFrameworkTarget
            = (TargetFrameworkTargetItem)_replaceTargetSDKVersionComboBox.SelectedItem;

        DirectoryInfo targetSharedAssemblyBasePath = new($"{FrameworkInfo.NetDesktopLibsDirectory}\\{targetFrameworkTarget.Name}");
        DirectoryInfo targetRefAssemblyBasePath= new($"{FrameworkInfo.NetDesktopRefsDirectory}\\" + $"{targetFrameworkTarget.Name}");
        DirectoryInfo targetRefAssemblyPath = new($"{targetRefAssemblyBasePath}\\ref\\net{MajorMinorVersionString(targetFrameworkTarget.Name)}");
        DirectoryInfo packageAssembliesManifestPath = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\{targetFrameworkTarget.Name}\\data");

        // Create a new DirectoryInfo for the analyzers directory, which is the same as the ref directory
        // but with the last part of the path changed to "analyzers".
        DirectoryInfo analyzersDir = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\{targetFrameworkTarget.Name}\\analyzers\\dotnet");
        DirectoryInfo cSharpAnalyzersDir = new($"{analyzersDir.FullName}\\{CSharpSubfolderPath}");
        DirectoryInfo visualBasicAnalyzersDir = new($"{analyzersDir.FullName}\\{VisualBasicSubfolderPath}");

        await commandBatch.InfoPrintLineAsync($"Destination Assembly directory:{targetSharedAssemblyBasePath}");
        await commandBatch.InfoPrintLineAsync($"Destination REF-Assembly directory:{targetRefAssemblyPath.FullName}");
        await commandBatch.InfoPrintLineAsync($"Destination Analyzers directory:{analyzersDir.FullName}");
        await commandBatch.InfoPrintLineAsync($"");

        await commandBatch.InfoPrintLineAsync($"Source Assembly directory:{sourceAssemblyBasePath}");
        await commandBatch.InfoPrintLineAsync($"Source RefAssembly directory:{sourceRefAssemblyBasePath}\\ref");
        await commandBatch.InfoPrintLineAsync($"");

        bool foundCheckedItems = false;

        string currentFileType = "Managed";

        DirectoryInfo targetDir;

        // Create a HashSet to store the processed files.
        HashSet<FileInfo> processedFiles = [];

        foreach (ListViewItem item in _availableAssembliesListView.Items)
        {
            if (!item.Checked)
            {
                continue;
            }

            foundCheckedItems = true;
            DesktopAssemblyInfo assemblyInfo = (DesktopAssemblyInfo)item.Tag!;

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

                // If the file starts with "System.Windows.Forms.Analyzers", copy it to the analyzers directory.
                // But. If the file ends with "VisualBasic.dll", we need to copy it in the SubFolder "\\vb", and
                // if it ends with "CSharp.dll", we need to copy it in the SubFolder "\\cs".
                if (!fileItem.Name.StartsWith("System.Windows.Forms.Analyzers"))
                {
                    currentFileType = "Managed";
                    targetDir = targetSharedAssemblyBasePath;
                }
                else
                {
                    currentFileType = "Analyzer";

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

                    //// Update the AssemblyInfo.xml file with the assembly information.
                    //AssemblyManifestProcessResult result = UpdateAssemblyInfo(
                    //    xmlFilePath: packageAssembliesManifestPath.FullName + "\\FrameworkList.xml",
                    //    destinationAssemblyFileInfo: (targetRefAssemblyBasePath, new FileInfo($"{targetDir}\\{fileItem.Name}")),
                    //    fileType: currentFileType,
                    //    targetFrameworkVersion: targetFrameworkTarget.Name,
                    //    updatePublicKey: false,
                    //    isRefAssembly: false);

                    //if (await ProcessManifestResult(commandBatch, fileItem, result))
                    //{
                    //    continue;
                    //}
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

                    // // Update the AssemblyInfo.xml file with the assembly information.
                    // AssemblyManifestProcessResult result = UpdateAssemblyInfo(
                    // xmlFilePath: packageAssembliesManifestPath.FullName + "\\FrameworkList.xml",
                    // destinationAssemblyFileInfo: (targetRefAssemblyBasePath, new FileInfo($"{targetRefAssemblyPath}\\{fileItem.Name}")),
                    // fileType: currentFileType,
                    // targetFrameworkVersion: targetFrameworkTarget.Name,
                    // updatePublicKey: false,
                    // isRefAssembly: true);

                    // if (await ProcessManifestResult(commandBatch, fileItem, result))
                    // {
                    //     continue;
                    // }

                    // Add the file to the processed files HashSet
                    processedFiles.Add(fileItem);

                    await commandBatch.CopyFileCommandAsync(
                        fileItem,
                        targetRefAssemblyPath,
                        overrideIfExist: true,
                        comment: "REF: ");
                }
            }
        }

        if (!foundCheckedItems)
        {
            await commandBatch.InfoPrintLineAsync("No items were selected, found nothing to copy.");
        }

        commandBatch.EndBatch("End of Command Batch.");

        _copyCommandButton.Enabled = true;

        static string MajorMinorVersionString(string versionString)
        {
            string[] items = versionString.Split('.');

            return items.Length switch
            {
                1 => $"{items[0]}.0",
                > 1 => $"{items[0]}.{items[1]}",
                _ => throw new ArgumentException(
                    "Could not figure out .NET Major/Minor Version for Ref Assemblies.")
            };
        }

        static async Task<bool> ProcessManifestResult(CommandBatch commandBatch, FileInfo fileItem, AssemblyManifestProcessResult result)
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
                await commandBatch.InfoPrintLineAsync(
                    $"Skipping {fileItem.Name} - {resultLogString}");
            }

            return skipOperation;
        }
    }

    private AssemblyManifestProcessResult UpdateAssemblyInfo(
        string xmlFilePath,
        (DirectoryInfo targetBasePath, FileInfo targetFile) destinationAssemblyFileInfo,
        string fileType,
        string targetFrameworkVersion,
        bool updatePublicKey,
        bool isRefAssembly)
    {
        if (!destinationAssemblyFileInfo.targetFile.Exists)
        {
            return AssemblyManifestProcessResult.MissingAssembly;
        }

        Assembly assembly = null!;
        AssemblyName? assemblyName;
        AssemblyMetadata? assemblyMetadata;

        if (isRefAssembly)
        {
            assemblyMetadata = AssemblyMetadataReader.GetAssemblyMetadata(
                destinationAssemblyFileInfo.targetFile.FullName);
        }
        else
        {

            try
            {
                assembly = Assembly.LoadFile(destinationAssemblyFileInfo.targetFile.FullName);

                if (assembly is null)
                {
                    return AssemblyManifestProcessResult.InvalidAssembly;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Couldn't load image of assembly: {ex.Message}");
                return AssemblyManifestProcessResult.InvalidAssembly;
            }
        }

        assemblyName = assembly?.GetName();

        if (assemblyName is null)
        {
            return AssemblyManifestProcessResult.InvalidAssembly;
        }

        var publicTokenBytes = assemblyName.GetPublicKeyToken();
        if (publicTokenBytes is null || publicTokenBytes.Length == 0)
        {
            return AssemblyManifestProcessResult.MissingPublicKey;
        }

        string publicKeyToken = BitConverter.ToString(publicTokenBytes).Replace("-", "").ToLowerInvariant();

        XDocument xmlDoc;

        try
        {
            xmlDoc = XDocument.Load(xmlFilePath);

            if (xmlDoc is null)
            {
                return AssemblyManifestProcessResult.InvalidXmlFile;
            }
        }
        catch (Exception)
        {
            return AssemblyManifestProcessResult.InvalidXmlFile;
        }

        XElement? fileList = xmlDoc.Element("FileList") ?? throw new Exception("Invalid XML format.");
        XElement existingFile = null!;

        // Get the delta-string between the targetBasePath and the targetFile:
        string deltaPath = destinationAssemblyFileInfo.targetFile.FullName
            .Replace(destinationAssemblyFileInfo.targetBasePath.FullName, string.Empty)
            .TrimStart('\\');

        // Create a HashSet which holds the end element of a new type, so we know where to insert a new entry.
        Dictionary<string, XElement> assemblyTypes = [];
        string? currentAssemblyType = null;
        bool multipleTypes = false;
        XElement lastEntry = null!;

        // Not the same as in that Silicon Valley episode, just saying! ;-)
        foreach (var file in fileList.Elements("File"))
        {
            // We need to build a list of assembly types, so we know where to insert a new entry.
            // That list then contains the respective last entry with that specific type.
            if (currentAssemblyType is null)
            {
                currentAssemblyType = file.Attribute("Type")?.Value;
            }
            else if (currentAssemblyType != file.Attribute("Type")?.Value)
            {
                multipleTypes = true;
                assemblyTypes.Add(currentAssemblyType, lastEntry);
                currentAssemblyType = file.Attribute("Type")?.Value;
            }

            var pathAttr = file.Attribute("Path")?.Value;

            lastEntry = file;

            if (pathAttr is not null && IsPathMatch(pathAttr.AsSpan(), deltaPath))
            {
                existingFile = file;
                break;
            }
        }

        if (existingFile is null && multipleTypes && lastEntry is not null) 
        {
            // List only remains important, when we need to extend the list
            // and need to know where to insert the entry.
            assemblyTypes.Add(currentAssemblyType!, lastEntry);
        }

        // Extra non-allocation mile gone for Paul Morgado (and probably half of DevDiv.)
        static bool IsPathMatch(ReadOnlySpan<char> path, string deltaPath)
        {
            if (path.Length != deltaPath.Length)
            {
                return false;
            }

            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != deltaPath[i] && !(path[i] == '/' && deltaPath[i] == '\\'))
                {
                    return false;
                }
            }

            return true;
        }

        if (existingFile is null)
        {
            string destinationAssemblyFilePath = destinationAssemblyFileInfo.targetFile.FullName;

            // Create a new entry
            XElement newFile = new("File",
                new XAttribute("Type", fileType),
                new XAttribute("Path", deltaPath.Replace('\\', '/')),
                new XAttribute("AssemblyName", assemblyName.Name!),
                new XAttribute("PublicKeyToken", publicKeyToken),
                new XAttribute(
                    "AssemblyVersion",
                    CreateMainFrameworkVersion(targetFrameworkVersion, GetAssemblyVersion(destinationAssemblyFilePath))),
                new XAttribute(
                    "FileVersion",
                    CreateMainFrameworkVersion(targetFrameworkVersion, GetFileVersion(destinationAssemblyFilePath))),
                new XAttribute("Profile", "WindowsForms"));

            // Insert the new entry behind the respective last type entry:
            if (assemblyTypes.TryGetValue(fileType, out XElement? lastTypeEntry))
            {
                lastTypeEntry.AddAfterSelf(newFile);
            }
            else
            {
                fileList.Add(newFile);
            }

            xmlDoc.Save(xmlFilePath);

            return AssemblyManifestProcessResult.Created;
        }
        else
        {
            // Check if the public key matches
            string? existingPublicKeyToken = existingFile.Attribute("PublicKeyToken")?.Value;

            if (existingPublicKeyToken != publicKeyToken)
            {
                if (updatePublicKey)
                {
                    existingFile.SetAttributeValue("PublicKeyToken", publicKeyToken);
                    xmlDoc.Save(xmlFilePath);

                    return AssemblyManifestProcessResult.PublicKeyUpdated;
                }
                else
                {
                    return AssemblyManifestProcessResult.PublicKeyDoesNotMatch;
                }
            }
        }

        // Default return value (although not all cases are covered).
        return AssemblyManifestProcessResult.OK;

        string GetFileVersion(string assemblyFilePath)
        {
            string version = "42.42.42.42424";

            try
            {
                var fileVersionInfo = FileVersionInfo.GetVersionInfo(assemblyFilePath);
                version = fileVersionInfo.FileVersion ?? version;
            }
            catch (Exception)
            {
                return version;
            }

            string[] parts = version.Split('.');
            if (parts.Length >= 4)
            {
                version = $"{parts[0]}.{parts[1]}.{parts[2]}.{parts[3]}";
            }

            return version;
        }

        string GetAssemblyVersion(string assemblyFilePath)
        {
            string version = "42.42.42.42424";

            try
            {
                var assembly = Assembly.LoadFile(assemblyFilePath);
                var assemblyName = assembly.GetName();

                version = assemblyName.Version?.ToString() ?? version;
            }
            catch (Exception)
            {
                return version;
            }

            string[] parts = version.Split('.');
            if (parts.Length >= 4)
            {
                version = $"{parts[0]}.{parts[1]}.{parts[2]}.{parts[3]}";
            }

            return version;
        }
    }

    /// <summary>
    /// Returns the "rounded" version of an assembly, meaning: 9.6.4-dev -> 9.0.0.0.
    /// If assembly version starts with "42", the rounded Framework version is returned.
    /// </summary>
    /// <param name="actualFrameworkVersion"></param>
    /// <param name="assemblyVersion"></param>
    /// <returns></returns>
    private string CreateMainFrameworkVersion(string actualFrameworkVersion, string assemblyVersion)
    {
        string version;

        if (assemblyVersion.StartsWith("42"))
        {
            version = actualFrameworkVersion;
        }
        else
        {
            version = assemblyVersion;
        }

        string[] parts = version.Split('.');
        if (parts.Length < 4)
        {
            return version;
        }

        return $"{parts[0]}.0.0.0";
    }
}
