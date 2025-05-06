using CommunityToolkit.ComponentModel;
using CommunityToolkit.WinForms.Extensions.UIExtensions;
using CommunityToolkit.WinForms.Symbols;
using DevTools.GitHubBuddy.GitHub;
using Microsoft.Extensions.DependencyInjection;
using Octokit;
using System.Diagnostics;
using App = System.Windows.Forms.Application;

namespace DevTools.GitHubBuddy;

public partial class FrmGitHubExplorer : Form
{
    private const string Key_WindowBounds = "MainWindow_Bounds";
    private const string PersonalAccountText = "Personal Account";

    private GitHubClient? _client;
    private IReadOnlyList<Organization>? _orgs;
    private IReadOnlyList<Repository>? _repos;
    private User? _user;

    // Settings service and keys
    private readonly IUserSettingsService? _settingsService;

    private readonly CancellationTokenSource _shutDownCancellation = new();

    /// <summary>
    ///  Default constructor required for designer
    /// </summary>
    public FrmGitHubExplorer()
    {
        InitializeComponent();
        InitializeStatusBar();
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

            if (_repos is not null && _user is not null)
            {
                await _tvwExplorerTree.PopulateReposAsync(_user.Name, _repos);
            }
            else
            {
                await UpdateInfoAsync("No repositories found.");
            }

            // !!! Needs to be the last call, since its callback will only
            // return when the form is closed !!!
            await ShowTimeAsync(_shutDownCancellation.Token);

            // The loop which is showing the time is running in the background.
            async Task ShowTimeAsync(CancellationToken token)
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(500, token).ConfigureAwait(false);
                    await InvokeAsync(() =>
                    {
                        _tslTime.Text = $"{DateTime.Now:dddd, MMM dd yyyy - HH:mm:ss}";
                    }, token);
                }
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

        var request = new RepositoryRequest
        {
            Visibility = RepositoryRequestVisibility.All,
            Affiliation = RepositoryAffiliation.Owner
        };

        // Load repositories and organizations from GitHub API
        if (_client != null)
        {
            await UpdateInfoAsync("Retrieving user info...").ConfigureAwait(false);
            _user = await _client.User.Current().ConfigureAwait(false);
            Debug.Print($"User: {_user?.Login}");
            await UpdateUserAsync().ConfigureAwait(false);

            await UpdateInfoAsync("Retrieving organizations...").ConfigureAwait(false);
            _orgs = await _client.Organization.GetAllForCurrent().ConfigureAwait(false);
            Debug.Print($"Organizations: {_orgs?.Count}");
            await UpdateOrgsAsync().ConfigureAwait(false);

            await UpdateInfoAsync("Retrieving list of personal Repos...").ConfigureAwait(false);
            _repos = await _client.Repository.GetAllForCurrent(request);
            Debug.Print($"Repositories: {_repos?.Count}");

            await UpdateInfoAsync($"Found {_repos?.Count} repos.").ConfigureAwait(false);
        }

        async Task UpdateUserAsync()
        {
            await InvokeAsync(() =>
            {
                _tslUser.Text = _user?.Login ?? "- User not found -";
            });
        }

        async Task UpdateOrgsAsync()
        {
            if (_orgs is null)
                return;

            await InvokeAsync(() =>
            {
                _tsdCurrentOrg.DropDownItems.Add(PersonalAccountText);
                _tsdCurrentOrg.DropDownItems.Add(new ToolStripSeparator());

                // Add the organizations to the list of the item:
                foreach (var org in _orgs)
                {
                    var item = new ToolStripMenuItem(org.Name)
                    {
                        Tag = org
                    };

                    _tsdCurrentOrg.DropDownItems.Add(item.Name);
                }

                _tsdCurrentOrg.Text = PersonalAccountText;
            });
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);

        // Save settings when form is closed
        if (_settingsService != null)
        {
            _settingsService.SaveSetting(Key_WindowBounds, this.GetRestorableBounds());
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

    private async Task UpdateInfoAsync(string info)
    {
        await InvokeAsync(() =>
        {
            _tslInfo.Text = $"Status: {info}";
        });
    }

    private void InitializeStatusBar()
    {
        // Initialize the status bar with the current time
        _tslTime.Text = $"{DateTime.Now:dddd, MMM dd yyyy - HH:mm:ss}";
        _tslTime.AutoSize = true;
        _tslUser.Text = "- Retrieving user info... -";
        _tsdCurrentOrg.Text = "- Retrieving organizations... -";
        _tslInfo.Text = "Initializing App.";
    }

    private void SetupCommands()
    {
        _tsmAddRepo.ConfigureItem(
            symbol: FluentSymbols.AllSymbols.DictionaryAdd,
            eventHandler: (clickHandler: AddNewRepoCommand, removeBeforeAdd: true),
            tooltipText: "Begin new chat");
    }

    private void AddNewRepoCommand(object? sender, EventArgs e)
    {
    }

    private async void ExplorerTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
        // Handle the selection of a node in the tree view
        if (_client is not null 
            && _user is not null 
            && e.Node?.Tag is Repository repo)
        {
            try
            {
                await _lvwIssues.PopulateIssuesForUsersAsync(_client, _user, repo).ConfigureAwait(false);
            }
            catch (Exception)
            {
            }
        }
    }
}
