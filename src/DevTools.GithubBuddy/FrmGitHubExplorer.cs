using DevTools.GitHubBuddy.GitHub;
using Octokit;

namespace DevTools.GitHubBuddy;

public partial class FrmGitHubExplorer : Form
{
    private readonly GitHubClient? _client;
    private IReadOnlyList<Organization>? _orgs;
    private IReadOnlyList<Repository>? _repos;
    private User? _user;

    public FrmGitHubExplorer()
    {
        InitializeComponent();
        this.FormCornerPreference = FormCornerPreference.Round;
    }

    private async void LoginToGitHubCommand(object sender, EventArgs e)
    {
        var token = GcmGitHubCredentialHelper.GetToken();

        if (token is not null)
        {
            var client = new GitHubClient(new ProductHeaderValue("WinFormsGitHubExplorer"))
            {
                Credentials = new Credentials(token)
            };

            _user = await client.User.Current();
            _repos = await client.Repository.GetAllForCurrent();
            _orgs = await client.Organization.GetAllForCurrent();
        }
        else
        {
            MessageBox.Show("No GitHub GCM token found. Has the user logged in via Git or GitHub CLI?", "Token Missing");
        }
    }


}

