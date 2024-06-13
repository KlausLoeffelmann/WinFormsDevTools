using WinFormsToolLib;

using static WinFormsDevTools.WinFormsGitHubRepoManager;

namespace WinFormsDevTools
{
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
                (sender,e) => DeployAvailableAssemblies();

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
                = (TargetFrameworkTargetItem) _replaceTargetSDKVersionComboBox.SelectedItem;

            DirectoryInfo targetAssemblyPath = new($"{FrameworkInfo.NetDesktopLibsDirectory}\\{targetFrameworkTarget.Name}");

            DirectoryInfo refDir = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\" +
                $"{targetFrameworkTarget.Name}\\ref\\" +
                $"net{MajorMinorVersionString(targetFrameworkTarget.Name)}");

            // Create a new DirectoryInfo for the analyzers directory, which is the same as the ref directory
            // but with the last part of the path changed to "analyzers".
            DirectoryInfo analyzersDir = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\" +
                $"{targetFrameworkTarget.Name}\\analyzers\\dotnet");

            DirectoryInfo csharpAnalyzersDir = new($"{analyzersDir.FullName}\\{CSharpSubfolderPath}");
            DirectoryInfo visualBasicAnalyzersDir = new($"{analyzersDir.FullName}\\{VisualBasicSubfolderPath}");

            await commandBatch.InfoPrintLineAsync($"Destination Assembly directory:{targetAssemblyPath}");
            await commandBatch.InfoPrintLineAsync($"Destination REF-Assembly directory:{refDir.FullName}");
            await commandBatch.InfoPrintLineAsync($"Destination Analyzers directory:{analyzersDir.FullName}");
            await commandBatch.InfoPrintLineAsync($"");

            bool foundCheckedItems = false;

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
                    // If the file starts with "System.Windows.Forms.Analyzers", copy it to the analyzers directory.
                    // But. If the file ends with "VisualBasic.dll", we need to copy it in the SubFolder "\\vb", and
                    // if it ends with "CSharp.dll", we need to copy it in the SubFolder "\\cs".
                    if (fileItem.Name.StartsWith("System.Windows.Forms.Analyzers"))
                    {
                        if (fileItem.Name.EndsWith("VisualBasic.dll"))
                        {
                            if (!vbFirst)
                            {
                                vbFirst = true;

                                // Create the subfolder "vb" in the analyzers directory if it does not exist:
                                if (!Directory.Exists(visualBasicAnalyzersDir.FullName))
                                {
                                    Directory.CreateDirectory(visualBasicAnalyzersDir.FullName);
                                }
                            }

                            await commandBatch.CopyFileCommandAsync(
                                fileItem,
                                visualBasicAnalyzersDir,
                                overrideIfExist: true);

                            continue;
                        }
                        else if (fileItem.Name.EndsWith("CSharp.dll"))
                        {
                            if (!csFirst)
                            {
                                csFirst = true;

                                // Create the subfolder "cs" in the analyzers directory if it does not exist:
                                if (!Directory.Exists($"{csharpAnalyzersDir}"))
                                {
                                    Directory.CreateDirectory(csharpAnalyzersDir.FullName);
                                }
                            }

                            await commandBatch.CopyFileCommandAsync(
                                fileItem,
                                csharpAnalyzersDir,
                                overrideIfExist: true);

                            continue;
                        }

                        await commandBatch.CopyFileCommandAsync(
                            fileItem,
                            analyzersDir,
                            overrideIfExist: true);

                        continue;
                    }

                    await commandBatch.CopyFileCommandAsync(
                        fileItem,
                        targetAssemblyPath,
                        overrideIfExist: true);
                }

                if (assemblyInfo.RefAssemblyFiles is not null)
                {
                    foreach (FileInfo fileItem in assemblyInfo.RefAssemblyFiles)
                    {
                        await commandBatch.CopyFileCommandAsync(
                            fileItem,
                            refDir,
                            overrideIfExist: true);
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
        }
    }
}
