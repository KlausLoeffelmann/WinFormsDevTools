using System.Diagnostics.CodeAnalysis;

namespace DevTools.RuntimeDeploy.Engine.Domain;

public partial class BuildArtefactsScanner
{
    public class TargetFrameworkTargetItem
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
