using System.Diagnostics.CodeAnalysis;

namespace WinFormsDevTools;

internal partial class WinFormsGitHubRepoManager
{
    internal class TargetFrameworkSourceItem(string name, string tfmPaths, DirectoryInfo directory)
    {
        public string Name { get; init; } = name;

        public string TfmPaths { get; init; } = tfmPaths;

        public DirectoryInfo Directory { get; init; } = directory;

        public override string ToString()
            => $"{Name} ({Directory.Name}";
    }
}
