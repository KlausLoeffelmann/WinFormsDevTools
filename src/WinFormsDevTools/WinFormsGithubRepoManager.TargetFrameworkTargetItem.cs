using System.Diagnostics.CodeAnalysis;

namespace WinFormsDevTools
{
    internal partial class WinFormsGitHubRepoManager
    {
        internal class TargetFrameworkTargetItem
        {
            [AllowNull]
            public string Name { get; set; }

            [AllowNull]
            public string PathFullName { get; set; }

            [AllowNull]
            public DirectoryInfo Directory { get; set; }

            public override string ToString() => Name;
        }
    }
}
