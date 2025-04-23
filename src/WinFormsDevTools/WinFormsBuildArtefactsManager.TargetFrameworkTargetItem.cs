using System.Diagnostics.CodeAnalysis;

namespace DevTools.RuntimeDeploy;

internal partial class WinFormsBuildArtefactsManager
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
