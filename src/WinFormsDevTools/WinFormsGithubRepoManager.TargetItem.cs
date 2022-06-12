using System.Diagnostics.CodeAnalysis;

namespace WinFormsDevTools
{
    internal partial class WinFormsGithubRepoManager
    {
        internal class TargetFrameworkSourceItem
        {
            [AllowNull]
            public string Name { get; init; }

            [AllowNull]
            public string TfmPath { get; init; }

            [AllowNull]
            public DirectoryInfo Directory { get; init; }

            public override string ToString()
                => Name;
        }
    }
}
