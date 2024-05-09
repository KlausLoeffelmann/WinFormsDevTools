using System.Diagnostics.CodeAnalysis;

namespace WinFormsDevTools;

internal partial class WinFormsGitHubRepoManager
{
    internal class TargetFrameworkSourceItem
    {
        public TargetFrameworkSourceItem(string name, string tfmPaths, DirectoryInfo directory)
        {
            Name = name;
            TfmPaths = tfmPaths;
            Directory = directory;
        }

        public string Name { get; init; }

        public string TfmPaths { get; init; }

        public DirectoryInfo Directory { get; init; }

        public override string ToString()
            => $"{Name} ({Directory.Name}";
    }
}
