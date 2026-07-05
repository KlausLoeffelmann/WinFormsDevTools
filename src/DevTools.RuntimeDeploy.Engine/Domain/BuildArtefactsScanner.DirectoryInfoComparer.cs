using System.Diagnostics.CodeAnalysis;

namespace DevTools.RuntimeDeploy.Engine.Domain;

public partial class BuildArtefactsScanner
{
    private static DirectoryInfoComparer? s_instance;

    public class DirectoryInfoComparer : IEqualityComparer<DirectoryInfo>
    {
        public bool Equals(DirectoryInfo? x, DirectoryInfo? y)
            => string.Equals(x?.Name, y?.Name, StringComparison.OrdinalIgnoreCase);

        public int GetHashCode([DisallowNull] DirectoryInfo obj)
            => StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Name);

        public static DirectoryInfoComparer Instance
            => s_instance ??= new DirectoryInfoComparer();
    }
}
