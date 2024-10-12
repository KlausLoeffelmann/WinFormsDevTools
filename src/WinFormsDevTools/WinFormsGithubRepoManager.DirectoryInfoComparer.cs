using System.Diagnostics.CodeAnalysis;

namespace DevTools.RuntimeDeploy;

internal partial class WinFormsGitHubRepoManager
{
    private static DirectoryInfoComparer? s_instance;

    internal class DirectoryInfoComparer : IEqualityComparer<DirectoryInfo>
    {
        public bool Equals(DirectoryInfo? x, DirectoryInfo? y)
        {
            return Equals(x?.Name, y?.Name);
        }

        public int GetHashCode([DisallowNull] DirectoryInfo obj)
        {
            return obj.Name.GetHashCode();
        }

        public static DirectoryInfoComparer Instance
            => s_instance ??= new DirectoryInfoComparer();
    }
}
