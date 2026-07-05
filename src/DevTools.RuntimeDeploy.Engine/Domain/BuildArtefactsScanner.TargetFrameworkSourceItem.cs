namespace DevTools.RuntimeDeploy.Engine.Domain;

public partial class BuildArtefactsScanner
{
    /// <summary>
    ///  Describes one available "source" entry inside the cloned WinForms
    ///  GitHub repository (e.g. <c>"Debug - net10.0"</c>): the directory
    ///  that contains the main TFM build output plus any TFM directory
    ///  suffixes the deploy step should also pull files from.
    /// </summary>
    /// <param name="name">Human-readable label shown in the ComboBox.</param>
    /// <param name="tfmPaths">
    ///  Ordered list of TFM directory suffixes (relative to
    ///  <c>bin\System.Windows.Forms</c>). Index 0 is the primary path and
    ///  is also used to locate the ref-assembly source directory under
    ///  <c>obj\System.Windows.Forms\{primary}\ref</c>. Subsequent entries
    ///  are sibling netstandard fallbacks (e.g. <c>"\netstandard2.0"</c>)
    ///  that physically exist on disk.
    /// </param>
    /// <param name="directory">The main TFM build output directory.</param>
    public class TargetFrameworkSourceItem(string name, IReadOnlyList<string> tfmPaths, DirectoryInfo directory)
    {
        public string Name { get; init; } = name;

        public IReadOnlyList<string> TfmPaths { get; init; } = tfmPaths;

        public DirectoryInfo Directory { get; init; } = directory;

        public override string ToString()
            => $"{Name} ({Directory.Name})";
    }
}
