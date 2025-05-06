using Octokit;

namespace DevTools.GitHubBuddy.Controls;

public class GitHubExplorerTreeView : TreeView
{
    private string _user = string.Empty;

    public GitHubExplorerTreeView()
    {
    }

    public async Task PopulateReposAsync(string user, IEnumerable<Repository> repos)
    {
        _user = user;
        await InvokeAsync(() => PopulateRepos(repos));

        void PopulateRepos(IEnumerable<Repository> repos)
        {
            Nodes.Clear();

            // Add user as the root node
            var userNode = new TreeNode(_user) { Tag = _user };
            Nodes.Add(userNode);

            foreach (var repo in repos)
            {
                var node = new TreeNode(repo.Name) { Tag = repo };
                userNode.Nodes.Add(node);
            }
        }
    }
}
