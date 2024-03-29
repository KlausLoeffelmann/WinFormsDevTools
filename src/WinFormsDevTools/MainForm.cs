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
        private const string SYSTEM_WINDOWS_FORMS = "System.Windows.Forms";
        private const string SYSTEM_WINDOWS_FORMS_DESIGN = "System.Windows.Forms.Design";
        private const string SYSTEM_WINDOWS_FORMS_PRIMITIVES = "System.Windows.Forms.Primitives";

        string[] s_preCheckItems = new string[]
        {
            ACCESSIBILITY,
            MICROSOFT_VISUALBASIC,
            MICROSOFT_VISUALBASIC_FACADE,
            MICROSOFT_VISUALBASIC_FORMS,
            SYSTEM_DESIGN_FACADE,
            SYSTEM_DRAWING_COMMON,
            SYSTEM_DRAWING_DESIGN_FACADE,
            SYSTEM_DRAWING_FACADE,
            SYSTEM_WINDOWS_FORMS,
            SYSTEM_WINDOWS_FORMS_DESIGN,
            SYSTEM_WINDOWS_FORMS_PRIMITIVES
        };

        public MainForm()
        {
            InitializeComponent();

            _overviewPanel.Visible = true;
            _deployRuntimePanel.Visible = false;
            _overviewPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _deployRuntimePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _overviewPanel.Location = _deployRuntimePanel.Location;
            _overviewPanel.Size = _deployRuntimePanel.Size;

            _pathToArtefactsRepoTextBox.TextChanged +=
                (sender, e) => DeployAvailableRuntimes();

            _availableDesktopRuntimesComboBox.SelectedIndexChanged +=
                (sender, e) => DeployAvailableAssemblies();

            _controlsForEnablingHandling = new Control[]
            {
                _availableDesktopRuntimesComboBox,
                _checkForRespectiveRefAssembliesCheckBox,
                _availableAssembliesListView,
                _replaceTargetSDKVersionComboBox,
                _copyCommandButton
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            var sdkFolders = FrameworkInfo.GetDotNetDesktopSdk(false);

            if (sdkFolders is null)
            {
                return;
            }

            var sdkTargets = sdkFolders
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
        {
            _pathToArtefactsRepoTextBox.Text = Properties.Settings.Default.PathToWinFormsGitHubRepo;
        }

        private void HandleControlEnabling_DeployRuntimeBinariesTab(bool enable, params Control[] excludeControlsForHandling)
        {
            Array.ForEach(
                _controlsForEnablingHandling.Where(
                    item => excludeControlsForHandling.Any(
                        excludeItem => excludeItem == item)).ToArray(),
                control => control.Enabled = enable);
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

            if (_availableDesktopRuntimesComboBox.SelectedItem is not null &&
                _gitHubRepoManager is not null)
            {
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

        private void CopyCommandButton_Click(object sender, EventArgs e)
        {
            CommandBatch commandBatch = new();
            commandBatch.StartBatch(
                windowTitle: "Copy .NET Desktop runtime assemblies",
                showCommandBatchWindow: true,
                dryRun: _dryRunCheckBox.Checked);

            TargetFrameworkTargetItem targetFrameworkTarget = (TargetFrameworkTargetItem)_replaceTargetSDKVersionComboBox.SelectedItem;
            DirectoryInfo targetAssemblyPath = targetFrameworkTarget.Directory;
            DirectoryInfo targetRefAssemblyPath = targetFrameworkTarget.Directory;

            bool foundCheckedItems = false;

            foreach (ListViewItem item in _availableAssembliesListView.Items)
            {
                if (item.Checked)
                {
                    foundCheckedItems = true;

                    DesktopAssemblyInfo assemblyInfo = (DesktopAssemblyInfo)item.Tag;

                    foreach (FileInfo fileItem in assemblyInfo.AssemblyFiles)
                    {
                        commandBatch.CopyFileCommand(
                            fileItem,
                            new DirectoryInfo($"{FrameworkInfo.NetDesktopLibsDirectory}\\{targetFrameworkTarget.Name}"),
                            overrideIfExist: true);
                    }

                    if (assemblyInfo.RefAssemblyFiles is not null)
                    {
                        foreach (FileInfo fileItem in assemblyInfo.RefAssemblyFiles)
                        {
                            DirectoryInfo refDir = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\" +
                                $"{targetFrameworkTarget.Name}\\ref\\" +
                                $"net{MajorMinorVersionString(targetFrameworkTarget.Name)}");

                            commandBatch.CopyFileCommand(
                                fileItem,
                                refDir,
                                overrideIfExist: true);
                        }
                    }
                }

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

            if (!foundCheckedItems)
            {
                commandBatch.InfoPrintLine("No items were selected, found nothing to copy.");
            }

            commandBatch.EndBatch("End of Command Batch.");
        }

        private void overviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _overviewPanel.Visible = true;
            _deployRuntimePanel.Visible = false;
        }

        private void deployRuntimeBinariesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _overviewPanel.Visible = false;
            _deployRuntimePanel.Visible = true;
        }
    }
}
