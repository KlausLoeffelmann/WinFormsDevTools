namespace DevTools.GitHubBuddy.ViewModels;

internal class Repository
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsPrivate { get; set; }
    public bool IsFork { get; set; }
    public List<Branch> Branches { get; set; } = new List<Branch>();
}

