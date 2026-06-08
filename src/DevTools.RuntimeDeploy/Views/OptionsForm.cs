using DevTools.RuntimeDeploy.Domain;
using DevTools.RuntimeDeploy.Infrastructure;
using Microsoft.Extensions.Logging;
using static DevTools.RuntimeDeploy.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy.Views;

public partial class OptionsForm : Form
{
    private RuntimeDeploySettingsService? _settings;
    private RuntimeDeployStatusService? _statusService;
    private ILogger<OptionsForm>? _logger;
    private BuildArtefactsScanner? _scanner;
    private bool _assembliesLoaded;

    public OptionsForm()
    {
        InitializeComponent();
        _assembliesListView.ConfigureDetailsView(checkBoxes: true);
    }

    public OptionsForm(
        RuntimeDeploySettingsService settings,
        RuntimeDeployStatusService statusService,
        ILogger<OptionsForm> logger) : this()
    {
        _settings = settings;
        _statusService = statusService;
        _logger = logger;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (_settings is null)
        {
            return;
        }

        _sourceFolderTextBox.Text = _settings.SourceArtefactsFolder;
        if (!string.IsNullOrWhiteSpace(_sourceFolderTextBox.Text))
        {
            LoadTargets();
        }
    }

    private void BrowseButton_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new()
        {
            Description = "Pick the path to the WinForms artifacts folder:",
            SelectedPath = Directory.Exists(_sourceFolderTextBox.Text) ? _sourceFolderTextBox.Text : string.Empty
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _sourceFolderTextBox.Text = dialog.SelectedPath;
            LoadTargets();
        }
    }

    private void SourceFolderTextBox_TextChanged(object sender, EventArgs e)
    {
        _targetsComboBox.Items.Clear();
        _assembliesListView.Items.Clear();
        _assembliesLoaded = false;
    }

    private void LoadAssembliesButton_Click(object sender, EventArgs e)
        => LoadTargets();

    private void TargetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        => LoadAssemblies();

    private void LoadTargets()
    {
        try
        {
            _targetsComboBox.Items.Clear();
            _assembliesListView.Items.Clear();
            _assembliesLoaded = false;

            if (string.IsNullOrWhiteSpace(_sourceFolderTextBox.Text))
            {
                return;
            }

            _scanner = new BuildArtefactsScanner(_sourceFolderTextBox.Text);
            TargetFrameworkSourceItem[] targets = _scanner.GetAvailableTargets();
            _targetsComboBox.Items.AddRange(targets);

            if (targets.Length > 0)
            {
                _targetsComboBox.SelectedIndex = targets.Length - 1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Could not load RuntimeDeploy options targets.");
            _statusService?.ReportException(ex);
            MessageBox.Show(this, ex.Message, "Could not load assemblies", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadAssemblies()
    {
        if (_scanner is null ||
            _targetsComboBox.SelectedItem is not TargetFrameworkSourceItem target)
        {
            return;
        }

        try
        {
            DesktopAssemblyInfo[] assemblies = _scanner.GetWinFormsRuntimeAssemblies(target, includeRefAssemblies: true);
            HashSet<string> excludedNames = _settings?.GetExcludedAssemblyNames() ?? [];

            _assembliesListView.AddItemsWithColumnHeadersFromType(
                assemblies,
                addSourceDataToTag: true,
                (nameof(DesktopAssemblyInfo.Name), "Assembly name"),
                (nameof(DesktopAssemblyInfo.Path), "Path"));

            foreach (ListViewItem item in _assembliesListView.Items)
            {
                if (excludedNames.Contains(item.Text))
                {
                    item.Checked = true;
                }
            }

            _assembliesLoaded = true;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Could not load RuntimeDeploy options assemblies.");
            _statusService?.ReportException(ex);
            MessageBox.Show(this, ex.Message, "Could not load assemblies", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        if (_settings is null)
        {
            DialogResult = DialogResult.OK;
            return;
        }

        _settings.SourceArtefactsFolder = _sourceFolderTextBox.Text;

        if (_assembliesLoaded)
        {
            string[] excludedNames =
            [
                .. from ListViewItem item in _assembliesListView.Items
                   where item.Checked
                   select item.Text
            ];

            _settings.SaveExcludedAssemblyNames(excludedNames);
        }

        DialogResult = DialogResult.OK;
    }
}
