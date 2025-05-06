using CommunityToolkit.ComponentModel;
using CommunityToolkit.WinForms.Extensions.UIExtensions;
using DevTools.GitHubBuddy.GitHub;
using Microsoft.Extensions.DependencyInjection;
using Octokit;
using System.Diagnostics;
using App = System.Windows.Forms.Application;

namespace DevTools.GitHubBuddy;

public partial class FrmGitHubExplorer : Form
{
    private GitHubClient? _client;
    private IReadOnlyList<Organization>? _orgs;
    private IReadOnlyList<Repository>? _repos;
    private User? _user;

    // Settings service and keys
    private readonly IUserSettingsService? _settingsService;
    private const string Key_WindowBounds = "MainWindow_Bounds";
    private const string Key_LastUser = "GitHub_LastUser";

    /// <summary>
    ///  Default constructor required for designer
    /// </summary>
    public FrmGitHubExplorer()
    {
        InitializeComponent();
        this.FormCornerPreference = FormCornerPreference.Round;
    }

    /// <summary>
    ///  Constructor with dependency injection support
    /// </summary>
    public FrmGitHubExplorer(IServiceProvider serviceProvider) : this()
    {
        // Get the settings service from the service provider
        _settingsService = serviceProvider.GetRequiredService<IUserSettingsService>();
        Debug.Assert(_settingsService is not null, "Settings service is null.");
    }

    protected async override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Restore window position and size from settings
        if (_settingsService != null)
        {
            Rectangle bounds = _settingsService.GetSetting(
                Key_WindowBounds,
                this.CenterToScreen(
                    horizontalFillGrade: 80,
                    verticalFillGrade: 80));

            Bounds = bounds;

            try
            {
                await LoadReposAndOrgs();
            }
            catch (Exception ex)
            {
                App.OnThreadException(ex);
            }
        }
    }

    private async Task LoadReposAndOrgs()
    {
        var token = GcmGitHubCredentialHelper.GetToken();

        if (token is null)
        {
            MessageBox.Show(
                "No GitHub GCM token found. Please login first via Git or the Git Command Line Interface.",
                "Missing Logon Token.");

            return;
        }

        _client = new GitHubClient(new ProductHeaderValue("WinFormsGitHubExplorer"))
        {
            Credentials = new Credentials(token)
        };

        // Load repositories and organizations from GitHub API
        if (_client != null)
        {
            _user = await _client.User.Current();
            Debug.Print($"User: {_user?.Login}");

            _orgs = await _client.Organization.GetAllForCurrent();
            Debug.Print($"Organizations: {_orgs?.Count}");

            _repos = await _client.Repository.GetAllForCurrent();
            Debug.Print($"Repositories: {_repos?.Count}");
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);

        // Save settings when form is closed
        if (_settingsService != null)
        {
            _settingsService.SaveSetting(Key_WindowBounds, this.GetRestorableBounds());

            // Save user info if available
            if (_user != null)
            {
                _settingsService.SaveSetting(Key_LastUser, _user.Login);
            }

            _settingsService.Save();
        }
    }

    private void HideContentFromCaptureToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Toggle the checked state
        var menuItem = (ToolStripMenuItem)sender;
        menuItem.Checked = !menuItem.Checked;
        
        if (menuItem.Checked)
        {
            // If this item is now checked, ensure the other option is unchecked
            FormScreenCaptureMode = ScreenCaptureMode.HideContent;
            
            // Find and uncheck the HideWindow option if it's checked
            if (hideWindowFromCaptureToolStripMenuItem.Checked)
                hideWindowFromCaptureToolStripMenuItem.Checked = false;
        }
        else
        {
            // If both items are unchecked, set to Allow mode
            if (!hideWindowFromCaptureToolStripMenuItem.Checked)
                FormScreenCaptureMode = ScreenCaptureMode.Allow;
        }
    }

    private void HideWindowFromCaptureToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Toggle the checked state
        var menuItem = (ToolStripMenuItem)sender;
        menuItem.Checked = !menuItem.Checked;
        
        if (menuItem.Checked)
        {
            // If this item is now checked, ensure the other option is unchecked
            FormScreenCaptureMode = ScreenCaptureMode.HideWindow;
            
            // Find and uncheck the HideContent option if it's checked
            if (hideContentFromCaptureToolStripMenuItem.Checked)
                hideContentFromCaptureToolStripMenuItem.Checked = false;
        }
        else
        {
            // If both items are unchecked, set to Allow mode
            if (!hideContentFromCaptureToolStripMenuItem.Checked)
                FormScreenCaptureMode = ScreenCaptureMode.Allow;
        }
    }

}
