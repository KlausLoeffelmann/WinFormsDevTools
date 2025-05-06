namespace DevTools.GitHubBuddy.ViewModels;

internal class Organization
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Repository> Repositories { get; set; } = new List<Repository>();
    public bool IsPersonalAccount { get; set; } = false;
}
