using DevTools.RuntimeDeploy.Engine.PatchBackup;
using DevTools.RuntimeDeploy.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DevTools.RuntimeDeploy.Views;

/// <summary>
///  "Restore Backup..." modal dialog: lists the <c>.netbackup</c> files found
///  under the configured backup root (see
///  <see cref="RuntimeDeploySettingsService.BackupRootFolder"/>) and restores
///  the selected one via <see cref="RestoreService"/>, which enforces the
///  same TFM-major-version plausibility check as the CLI tool.
/// </summary>
public partial class RestoreBackupForm : Form
{
    private RuntimeDeploySettingsService? _settings;
    private RuntimeDeployStatusService? _statusService;
    private ILogger<RestoreBackupForm>? _logger;

    public RestoreBackupForm()
    {
        InitializeComponent();
        _backupsListView.SelectedIndexChanged += BackupsListView_SelectedIndexChanged;
    }

    public RestoreBackupForm(
        RuntimeDeploySettingsService settings,
        RuntimeDeployStatusService statusService,
        ILogger<RestoreBackupForm> logger) : this()
    {
        _settings = settings;
        _statusService = statusService;
        _logger = logger;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await LoadBackupsAsync();
    }

    private async void RefreshButton_Click(object sender, EventArgs e)
        => await LoadBackupsAsync();

    private async Task LoadBackupsAsync()
    {
        _backupsListView.Items.Clear();
        _restoreButton.Enabled = false;

        string backupRootPath = _settings?.BackupRootFolder ?? BackupService.DefaultBackupRoot.FullName;
        DirectoryInfo backupRoot = new(backupRootPath);

        try
        {
            IReadOnlyList<BackupSearchResult> results = await BackupFinder.FindBackupsAsync(backupRoot);

            _backupsListView.Columns.Clear();
            _backupsListView.Columns.Add("Created (UTC)", 220);
            _backupsListView.Columns.Add("TFM", 150);
            _backupsListView.Columns.Add("Configuration", 150);
            _backupsListView.Columns.Add("File", -2);

            foreach (BackupSearchResult result in results)
            {
                ListViewItem item = new(result.Manifest.CreatedUtc.ToString("yyyy-MM-dd HH:mm"));
                item.SubItems.Add(result.Manifest.Tfm);
                item.SubItems.Add(result.Manifest.Configuration);
                item.SubItems.Add(result.BackupFile.Name);
                item.Tag = result;

                _backupsListView.Items.Add(item);
            }

            if (results.Count == 0)
            {
                _statusService?.ReportInfo($"No backups found under '{backupRoot.FullName}'.");
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Could not enumerate backups.");
            _statusService?.ReportException(ex);
            MessageBox.Show(this, ex.Message, "Could not load backups", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BackupsListView_SelectedIndexChanged(object? sender, EventArgs e)
        => _restoreButton.Enabled = _backupsListView.SelectedItems.Count > 0;

    private async void RestoreButton_Click(object sender, EventArgs e)
    {
        if (_backupsListView.SelectedItems.Count == 0 ||
            _backupsListView.SelectedItems[0].Tag is not BackupSearchResult selected)
        {
            return;
        }

        DialogResult confirmResult = MessageBox.Show(
            this,
            $"Restore backup '{selected.BackupFile.Name}' (TFM {selected.Manifest.Tfm}, {selected.Manifest.Configuration})?\n\n" +
            "Files will be restored to their original locations, overwriting current files.",
            "Confirm restore",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirmResult != DialogResult.Yes)
        {
            return;
        }

        _restoreButton.Enabled = false;

        try
        {
            int restoredCount = await RestoreService.RestoreAsync(selected.BackupFile, targetOverride: null);
            _statusService?.ReportInfo($"Restored {restoredCount} file(s) from '{selected.BackupFile.Name}'.");

            MessageBox.Show(
                this,
                $"Restored {restoredCount} file(s).",
                "Restore complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (RestorePlausibilityException ex)
        {
            _logger?.LogError(ex, "Restore plausibility check failed.");
            MessageBox.Show(this, ex.Message, "Restore refused", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Restore failed.");
            _statusService?.ReportException(ex);
            MessageBox.Show(this, ex.Message, "Restore failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _restoreButton.Enabled = true;
        }
    }
}
