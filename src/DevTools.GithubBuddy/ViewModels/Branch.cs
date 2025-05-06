namespace DevTools.GitHubBuddy.ViewModels;

internal class Branch
{
    public string Name { get; set; } = string.Empty;
    public string CommitSha { get; set; } = string.Empty;
    public DateTime LastCommitDate { get; set; }
    public string LastCommitMessage { get; set; } = string.Empty;
}

