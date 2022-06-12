using WinFormsToolLib;
using static WinFormsDevTools.WinFormsGithubRepoManager;

namespace WinFormsDevTools
{
    public partial class MainForm : Form
    {
        private Control[] _controlsForEnablingHandling;
        private WinFormsGithubRepoManager? _githubRepoManager;

        public MainForm()
        {
            InitializeComponent();
            
            _pathToArtefactsRepoTextBox.TextChanged += 
                (sender, e) => DeployAvailableRuntimes();

            _availableDesktopRuntimesComboBox.SelectedIndexChanged += 
                (sender,e) => DeployAvailableAssemblies();

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
                    PathFullname = item.FullName,
                    Directory = item
                })
                .ToArray();

            _netDesktopSdksListView.ConfigureDetailsView();
            _netDesktopSdksListView.AddItemsWithColumnHeadersFromType(
                sdkTargets,
                addSourceDataToTag: true,
                (nameof(TargetFrameworkTargetItem.Name), ".NET SDK Version"),
                (nameof(TargetFrameworkTargetItem.PathFullname), "Path"));

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
                        excludeitem => excludeitem == item)).ToArray(),
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
                _githubRepoManager = new(_pathToArtefactsRepoTextBox.Text);

                var targets = _githubRepoManager
                    .GetAvailableTargets();

                _availableDesktopRuntimesComboBox.Items.AddRange(targets);
                _availableDesktopRuntimesComboBox.SelectedIndex = targets.Count() - 1;
            }
        }

        private void DeployAvailableAssemblies()
        {

            if (_availableDesktopRuntimesComboBox.SelectedItem is not null && 
                _githubRepoManager is not null)
            {
                var assemblies = _githubRepoManager.GetWinFormsRuntimeAssemblies(
                    (TargetFrameworkSourceItem)_availableDesktopRuntimesComboBox.SelectedItem,
                    _checkForRespectiveRefAssembliesCheckBox.Checked);

                _availableAssembliesListView.ConfigureDetailsView(checkBoxes: true);

                _availableAssembliesListView.AddItemsWithColumnHeadersFromType(
                    assemblies,
                    addSourceDataToTag: true,
                    (nameof(DesktopAssemblyInfo.Name), "Assembly name"),
                    (nameof(DesktopAssemblyInfo.Path), "Path"));
            }
        }

        private void _pickPathToArtefactsButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new();
            browserDialog.Description = "Pick the path to the WinForms Github repo";
            DialogResult dialogResult = browserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _pathToArtefactsRepoTextBox.Text = browserDialog.SelectedPath;
                Properties.Settings.Default.PathToWinFormsGitHubRepo = browserDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void _copyCommandButton_Click(object sender, EventArgs e)
        {
            CommandBatch commandBatch = new();
            commandBatch.StartBatch(
                windowTitel: "Copy .NET Desktop runtime assemblies",
                showCommandBatchWindow: true,
                dryRun: _dryRunCheckBox.Checked);

            var targetFrameworkTarget = (TargetFrameworkTargetItem) _replaceTargetSDKVersionComboBox.SelectedItem;
            var targetAssemblyPath = targetFrameworkTarget.Directory;
            var targetRefAssemblyPath= targetFrameworkTarget.Directory;

            bool foundCheckedItems = false;

            foreach (ListViewItem item in _availableAssembliesListView.Items)
            {
                if (item.Checked)
                {
                    foundCheckedItems = true;

                    var assemblyInfo = (DesktopAssemblyInfo)item.Tag;

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
                            var refDir = new DirectoryInfo($"{FrameworkInfo.NetDesktopRefsDirectory}\\" +
                                $"{targetFrameworkTarget.Name}\\ref\\" +
                                $"net{MajorMinorVersionString(targetFrameworkTarget.Name)}");

                            commandBatch.CopyFileCommand(
                                fileItem,
                                refDir,
                                overrideIfExist: true);
                        }
                    }
                }

                string MajorMinorVersionString(string versionString)
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
    }
}
