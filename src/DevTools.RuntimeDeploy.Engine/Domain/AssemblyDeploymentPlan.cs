namespace DevTools.RuntimeDeploy.Engine.Domain;

using static BuildArtefactsScanner;

/// <summary>
///  Builds a deterministic, destination-based copy plan for selected build artifacts.
/// </summary>
public static class AssemblyDeploymentPlan
{
    /// <summary>
    ///  Describes one source assembly and its final deployment destination.
    /// </summary>
    public sealed record CopyItem(FileInfo SourceFile, FileInfo DestinationFile);

    /// <summary>
    ///  Creates the runtime/analyzer copy plan, keeping only one source for each destination.
    /// </summary>
    public static CopyItem[] CreateRuntimeCopyPlan(
        IEnumerable<DesktopAssemblyInfo> assemblies,
        TargetFrameworkSourceItem sourceTarget,
        AssemblyDeploymentTargetResolver.TargetPaths targetPaths)
    {
        ArgumentNullException.ThrowIfNull(assemblies);
        ArgumentNullException.ThrowIfNull(sourceTarget);

        return CreatePlan(
            assemblies.SelectMany(
                assembly => assembly.AssemblyFiles.Select(file => (Assembly: assembly, File: file))),
            sourceTarget.TfmPaths[0],
            file =>
            {
                DirectoryInfo destinationDirectory =
                    AssemblyDeploymentTargetResolver.GetAssemblyTargetDirectory(file.Name, targetPaths);
                return new FileInfo(Path.Combine(destinationDirectory.FullName, file.Name));
            });
    }

    /// <summary>
    ///  Creates the reference-assembly copy plan, keeping only one source for each destination.
    /// </summary>
    public static CopyItem[] CreateReferenceCopyPlan(
        IEnumerable<DesktopAssemblyInfo> assemblies,
        TargetFrameworkSourceItem sourceTarget,
        AssemblyDeploymentTargetResolver.TargetPaths targetPaths)
    {
        ArgumentNullException.ThrowIfNull(assemblies);
        ArgumentNullException.ThrowIfNull(sourceTarget);

        return CreatePlan(
            assemblies.SelectMany(
                assembly => (assembly.RefAssemblyFiles ?? [])
                    .Select(file => (Assembly: assembly, File: file))),
            sourceTarget.TfmPaths[0],
            file => new FileInfo(Path.Combine(targetPaths.TargetRefAssemblyPath.FullName, file.Name)));
    }

    private static CopyItem[] CreatePlan(
        IEnumerable<(DesktopAssemblyInfo Assembly, FileInfo File)> candidates,
        string primaryTfmPath,
        Func<FileInfo, FileInfo> getDestination)
    {
        return
        [
            .. candidates
                .Select(candidate => new
                {
                    candidate.Assembly,
                    candidate.File,
                    Destination = getDestination(candidate.File)
                })
                .GroupBy(
                    candidate => candidate.Destination.FullName,
                    StringComparer.OrdinalIgnoreCase)
                .Select(group =>
                {
                    var preferred = group
                        .OrderByDescending(candidate => IsProjectOutput(candidate.Assembly, candidate.File))
                        .ThenByDescending(candidate => IsPrimaryTarget(candidate.File, primaryTfmPath))
                        .ThenBy(candidate => candidate.File.FullName, StringComparer.OrdinalIgnoreCase)
                        .First();

                    return new CopyItem(preferred.File, preferred.Destination);
                })
                .OrderBy(item => item.DestinationFile.FullName, StringComparer.OrdinalIgnoreCase)
        ];
    }

    private static bool IsProjectOutput(DesktopAssemblyInfo assembly, FileInfo file)
        => string.Equals(
            assembly.Name,
            Path.GetFileNameWithoutExtension(file.Name),
            StringComparison.OrdinalIgnoreCase);

    private static bool IsPrimaryTarget(FileInfo file, string primaryTfmPath)
        => file.Directory?.FullName.EndsWith(primaryTfmPath, StringComparison.OrdinalIgnoreCase) is true;
}
