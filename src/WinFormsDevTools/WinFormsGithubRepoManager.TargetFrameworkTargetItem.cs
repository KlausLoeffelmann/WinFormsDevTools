using System.Diagnostics.CodeAnalysis;

namespace WinFormsDevTools
{
    internal partial class WinFormsGithubRepoManager
    {
        internal class TargetFrameworkTargetItem
        {
            [AllowNull]
            public string Name { get; set; }

            [AllowNull]
            public string PathFullname { get; set; }

            [AllowNull]
            public DirectoryInfo Directory { get; set; }

            public override string ToString() => Name;
        }
    }
}
