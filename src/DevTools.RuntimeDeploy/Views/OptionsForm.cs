using DevTools.RuntimeDeploy.Engine.Domain;
using DevTools.RuntimeDeploy.Infrastructure;
using Microsoft.Extensions.Logging;
using static DevTools.RuntimeDeploy.Engine.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy.Views;

public partial class OptionsForm : Form
{
    private RuntimeDeploySettingsService? _settings;
    private RuntimeDeployStatusService? _statusService;
    private ILogger<OptionsForm>? _logger;
    private BuildArtefactsScanner? _scanner;
    private bool _assembliesLoaded;
    private Font _uiFont = RuntimeDeploySettingsService.CreateDefaultUiFont();
    private Font _outputFont = RuntimeDeploySettingsService.CreateDefaultOutputFont();

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

        _backupFolderTextBox.Text = _settings.BackupRootFolder;

        _uiFont = _settings.UiFont;
        _outputFont = _settings.OutputFont;
        UpdateFontPreview(_uiFontPreviewLabel, _uiFont);
        UpdateFontPreview(_outputFontPreviewLabel, _outputFont);
    }

    private static void UpdateFontPreview(Label previewLabel, Font font)
    {
        previewLabel.Font = font;
        previewLabel.Text = $"{font.Name}, {font.SizeInPoints:0.#}pt"
            + (font.Style == FontStyle.Regular ? string.Empty : $", {font.Style}");
    }

    private void ChangeUiFontButton_Click(object sender, EventArgs e)
        => PickFont(ref _uiFont, _uiFontPreviewLabel, isUiFont: true);

    private void ChangeOutputFontButton_Click(object sender, EventArgs e)
        => PickFont(ref _outputFont, _outputFontPreviewLabel, isUiFont: false);

    private void PickFont(ref Font currentFont, Label previewLabel, bool isUiFont)
    {
        using FontDialog dialog = new()
        {
            Font = currentFont,
            ShowEffects = false,
            FontMustExist = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        currentFont = dialog.Font;
        UpdateFontPreview(previewLabel, currentFont);

        // Persist the new font immediately, as requested.
        if (_settings is not null)
        {
            if (isUiFont)
            {
                _settings.UiFont = currentFont;
            }
            else
            {
                _settings.OutputFont = currentFont;
            }
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

    private void BrowseBackupFolderButton_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new()
        {
            Description = "Pick the folder where backups of overwritten runtime assemblies are stored:",
            SelectedPath = Directory.Exists(_backupFolderTextBox.Text) ? _backupFolderTextBox.Text : string.Empty
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _backupFolderTextBox.Text = dialog.SelectedPath;
        }
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

        if (!string.IsNullOrWhiteSpace(_backupFolderTextBox.Text))
        {
            _settings.BackupRootFolder = _backupFolderTextBox.Text;
        }

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
