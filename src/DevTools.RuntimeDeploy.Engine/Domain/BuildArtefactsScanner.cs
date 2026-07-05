namespace DevTools.RuntimeDeploy.Engine.Domain;

/// <summary>
///  Provides functionality to extract the available generated assemblies
///  from the Artifacts folder of the WinForms GitHub repository.
/// </summary>
public partial class BuildArtefactsScanner
{
    public const string BinSystemWindowsFormsPath = "\\bin\\System.Windows.Forms";
    public const string BinPath = "\\bin";
    public const string ObjPath = "\\obj";

    // Definition of additional potential Framework Moniker paths,
    // which are TFM independent (.NET Standard) and ship alongside the
    // main TFM-specific build outputs.
    private static readonly string[] s_additionalTfmPaths =
    [
        "\\netstandard2.0",
        "\\netstandard2.1",
    ];

    public BuildArtefactsScanner(string pathToGitHubRepo)
        => PathToGitHubRepo = pathToGitHubRepo;

    public TargetFrameworkSourceItem[] GetAvailableTargets()
    {
        DirectoryInfo binWinForms = new(PathToGitHubRepo + BinSystemWindowsFormsPath);

        return [..
            binWinForms.GetDirectories(
                searchPattern: "*.*",
                enumerationOptions: new EnumerationOptions
                {
                    RecurseSubdirectories = true,
                })
            .Select(item =>
            {
                string middlePath = item.FullName.Replace(PathToBinSystemWindowsForms, string.Empty);

                // Each item carries every TFM path the deploy step should pull files from:
                // index 0 is the primary (config + main TFM, e.g. "\Debug\net10.0") and is
                // also used to locate the ref-assembly source directory; subsequent entries
                // are sibling netstandard fallbacks that exist on disk next to the primary.
                List<string> tfmPaths = [middlePath];

                if (item.Parent is DirectoryInfo parent)
                {
                    foreach (string fallback in s_additionalTfmPaths)
                    {
                        DirectoryInfo additional = new(parent.FullName + fallback);
                        if (additional.Exists)
                        {
                            tfmPaths.Add(fallback);
                        }
                    }
                }

                return new TargetFrameworkSourceItem(
                    name: middlePath[1..].Replace("\\", " - "),
                    tfmPaths: tfmPaths,
                    directory: item);
            })];
    }

    public DesktopAssemblyInfo[] GetWinFormsRuntimeAssemblies(TargetFrameworkSourceItem target, bool includeRefAssemblies)
    {
        DirectoryInfo binWinForms = new(PathToGitHubRepo + BinPath);

        return [.. binWinForms.GetFiles(
                searchPattern: "*.dll",
                enumerationOptions: new EnumerationOptions() { RecurseSubdirectories = true })

            // TfmPaths is the list of TFM directory suffixes to include for this source item
            // (the primary main-TFM path plus any present netstandard fallbacks). A DLL is
            // considered "in scope" if its parent directory's full name ends with any of them.
            .Where(item => target.TfmPaths.Any(tfmItem => item.Directory!.FullName.EndsWith(tfmItem, StringComparison.OrdinalIgnoreCase)))
            .GroupBy(
                keySelector: item => item.Directory!.Parent!.Parent!,
                elementSelector: elementItem => elementItem,
                comparer: DirectoryInfoComparer.Instance)
            .Select(group =>
            {
                // UniqueFiles is used to keep track of unique assembly files based on their name and size
                var uniqueFiles = new HashSet<(string Name, long Size)>();

                // AssemblyFiles contains the unique assembly files within the group
                var assemblyFiles = group
                    .Where(file => uniqueFiles.Add((file.Name, file.Length)))
                    .ToArray();

                return new DesktopAssemblyInfo()
                {
                    Path = group.Key,
                    Name = group.Key.Name,
                    AssemblyFiles = assemblyFiles,
                    RefAssemblyFiles = includeRefAssemblies
                        ? FindRefAssemblySourceFiles(group.Key, target.TfmPaths[0])
                        : [],
                };
            })];

        FileInfo[] FindRefAssemblySourceFiles(DirectoryInfo directory, string primaryTfmPath)
        {
            var uniqueFiles = new HashSet<(string Name, long Size)>();
            var allFiles = new List<FileInfo>();

            var refDirectory = new DirectoryInfo($"{PathToGitHubRepo}{ObjPath}\\{directory.Name}\\{primaryTfmPath}\\ref");
            if (refDirectory.Exists)
            {
                foreach (var file in refDirectory.GetFiles("*.dll"))
                {
                    var fileKey = (file.Name, file.Length);
                    if (uniqueFiles.Add(fileKey))
                    {
                        allFiles.Add(file);
                    }
                }
            }

            return [.. allFiles];
        }
    }

    public string PathToGitHubRepo { get; }

    public string PathToBinSystemWindowsForms
        => PathToGitHubRepo + BinSystemWindowsFormsPath;
}
