namespace DevTools.RuntimeDeploy.Engine.Domain;

/// <summary>
///  Resolves the destination directories/files for a given
///  <see cref="BuildArtefactsScanner.TargetFrameworkTargetItem"/>, factored out
///  of the copy-command flow so both the actual copy operation and any
///  preview/comparison UI (e.g. showing source/destination dates before
///  copying) compute the exact same destination paths and never drift apart.
/// </summary>
public static class AssemblyDeploymentTargetResolver
{
    private const string AnalyzersPrefix = "System.Windows.Forms.Analyzers";
    private const string VisualBasicSubfolderPath = "vb";
    private const string CSharpSubfolderPath = "cs";

    /// <summary>
    ///  The set of destination directories for a given deployment target,
    ///  used both to decide where a specific assembly file goes and to
    ///  create the required analyzer subfolders.
    /// </summary>
    public readonly struct TargetPaths
    {
        public required DirectoryInfo TargetSharedAssemblyBasePath { get; init; }
        public required DirectoryInfo TargetRefAssemblyBasePath { get; init; }
        public required DirectoryInfo TargetRefAssemblyPath { get; init; }
        public required DirectoryInfo AnalyzersDir { get; init; }
        public required DirectoryInfo CSharpAnalyzersDir { get; init; }
        public required DirectoryInfo VisualBasicAnalyzersDir { get; init; }
    }

    /// <summary>
    ///  Computes the destination directories for the given target framework.
    /// </summary>
    public static TargetPaths GetTargetPaths(BuildArtefactsScanner.TargetFrameworkTargetItem target)
    {
        ArgumentNullException.ThrowIfNull(target);

        DirectoryInfo targetSharedAssemblyBasePath = new($"{FrameworkInfo.NetDesktopLibsDirectory}\\{target.Name}");
        DirectoryInfo targetRefAssemblyBasePath = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\{target.Name}");
        DirectoryInfo targetRefAssemblyPath = new(
            $"{targetRefAssemblyBasePath}\\ref\\net{FrameworkVersionFormatter.ToMajorMinor(target.Name)}");
        DirectoryInfo analyzersDir = new($"{FrameworkInfo.NetDesktopRefsDirectory}\\{target.Name}\\analyzers\\dotnet");
        DirectoryInfo cSharpAnalyzersDir = new($"{analyzersDir.FullName}\\{CSharpSubfolderPath}");
        DirectoryInfo visualBasicAnalyzersDir = new($"{analyzersDir.FullName}\\{VisualBasicSubfolderPath}");

        return new TargetPaths
        {
            TargetSharedAssemblyBasePath = targetSharedAssemblyBasePath,
            TargetRefAssemblyBasePath = targetRefAssemblyBasePath,
            TargetRefAssemblyPath = targetRefAssemblyPath,
            AnalyzersDir = analyzersDir,
            CSharpAnalyzersDir = cSharpAnalyzersDir,
            VisualBasicAnalyzersDir = visualBasicAnalyzersDir
        };
    }

    /// <summary>
    ///  Picks the destination directory for a single (non-ref) assembly file,
    ///  mirroring the special-casing the copy command applies to
    ///  <c>System.Windows.Forms.Analyzers*</c> files (which are split into
    ///  "cs"/"vb" subfolders under the analyzers directory).
    /// </summary>
    public static DirectoryInfo GetAssemblyTargetDirectory(string fileName, in TargetPaths paths)
    {
        ArgumentNullException.ThrowIfNull(fileName);

        if (!fileName.StartsWith(AnalyzersPrefix, StringComparison.Ordinal))
        {
            return paths.TargetSharedAssemblyBasePath;
        }

        if (fileName.EndsWith("VisualBasic.dll", StringComparison.Ordinal))
        {
            return paths.VisualBasicAnalyzersDir;
        }

        if (fileName.EndsWith("CSharp.dll", StringComparison.Ordinal))
        {
            return paths.CSharpAnalyzersDir;
        }

        return paths.AnalyzersDir;
    }

    /// <summary>
    ///  Resolves the representative source assembly file (the main
    ///  <c>.dll</c> matching the assembly's name) and its corresponding
    ///  destination file for a given deployment target, for use in
    ///  preview/comparison UI. Ref-assembly files are not considered here -
    ///  the comparison is about the primary runtime assembly.
    /// </summary>
    public static (FileInfo? SourceFile, FileInfo? DestinationFile) ResolveComparisonFiles(
        BuildArtefactsScanner.DesktopAssemblyInfo assemblyInfo,
        in TargetPaths paths)
    {
        ArgumentNullException.ThrowIfNull(assemblyInfo);

        FileInfo? sourceFile = assemblyInfo.AssemblyFiles?.FirstOrDefault(
            file => file.Extension.Equals(".dll", StringComparison.OrdinalIgnoreCase)
                && string.Equals(
                    Path.GetFileNameWithoutExtension(file.Name),
                    assemblyInfo.Name,
                    StringComparison.OrdinalIgnoreCase))
            ?? assemblyInfo.AssemblyFiles?.FirstOrDefault();

        if (sourceFile is null)
        {
            return (null, null);
        }

        DirectoryInfo targetDir = GetAssemblyTargetDirectory(sourceFile.Name, paths);
        FileInfo destinationFile = new(Path.Combine(targetDir.FullName, sourceFile.Name));

        return (sourceFile, destinationFile);
    }
}
