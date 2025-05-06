using Octokit;

namespace DevTools.GitHubBuddy.Controls;

public class GitHubIssuesListView : ListView
{
    public GitHubIssuesListView()
    {
        View = View.Details;
        FullRowSelect = true;
        MultiSelect = false;
        HideSelection = false;
        AllowColumnReorder = true;
        Sorting = SortOrder.Ascending;
        SetUpColumns();
    }

    public void SetUpColumns()
    {
        Columns.Add("Number", 200, HorizontalAlignment.Left);
        Columns.Add("Title", 400, HorizontalAlignment.Left);
        Columns.Add("Prio", 400, HorizontalAlignment.Left);
        Columns.Add("Labels", 400, HorizontalAlignment.Left);
        Columns.Add("State", 100, HorizontalAlignment.Left);
        Columns.Add("Created At", 150, HorizontalAlignment.Left);
        Columns.Add("Created By", 150, HorizontalAlignment.Left);
        Columns.Add("Last updated", 150, HorizontalAlignment.Left);
    }

    public static int? GetPriorityFromLabels(IReadOnlyList<Octokit.Label> labels)
    {
        var priorityLabel = labels.FirstOrDefault(label => label.Name.StartsWith("Prio"));
        if (priorityLabel != null && int.TryParse(priorityLabel.Name.Split(':')[1].Trim(), out int priority))
        {
            return priority;
        }
        return null;
    }

    public async Task PopulateIssuesForUsersAsync(GitHubClient client, User user, Repository repository)
    {
        RepositoryIssueRequest issueRequest = new()
        {
            Filter = IssueFilter.All,
            State = ItemStateFilter.All,
            Creator = user.Login
        };

        var issues = await client.Issue.GetAllForRepository(
            repositoryId: repository.Id,
            request: issueRequest);

        if (issues == null || issues.Count == 0)
        {
            return;
        }

        await InvokeAsync(() =>
        {
            ClearIssues();
            PopulateIssues(issues);
        });
    }

    public void ClearIssues()
    {
        Items.Clear();
    }

    public void PopulateAndClearIssues(IEnumerable<Issue> issues)
    {
        ClearIssues();
        PopulateIssues(issues);
    }

    public void PopulateIssues(IEnumerable<Issue> issues)
    {
        foreach (var issue in issues)
        {
            var item = new ListViewItem(issue.Number.ToString())
            {
                Tag = issue,
                SubItems =
                {
                    issue.Title,
                    GetPriorityFromLabels(issue.Labels)?.ToString() ?? "N/A",
                    GetChainedLabels(issue.Labels),
                    issue.State.ToString(),
                    issue.CreatedAt.ToString("g"),
                    issue.User.Login,
                    issue.UpdatedAt?.ToString("g") ?? "N/A"
                }
            };
            Items.Add(item);
        }

        static string GetChainedLabels(IReadOnlyList<Octokit.Label> labels)
        {
            return string.Join(", ", labels.Select(label => label.Name));
        }
    }
}
