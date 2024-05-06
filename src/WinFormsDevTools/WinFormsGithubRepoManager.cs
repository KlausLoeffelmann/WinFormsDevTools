﻿namespace WinFormsDevTools
{
    /// <summary>
    ///  Provides functionality to extract the available generated assemblies 
    ///  from the Artifacts folder of the WinForms GitHub repository.
    /// </summary>
    internal partial class WinFormsGitHubRepoManager
    {
        public const string BinSystemWindowsFormsPath = "\\bin\\System.Windows.Forms";
        public const string BinPath = "\\bin";
        public const string ObjPath = "\\obj";

        public WinFormsGitHubRepoManager(string pathToGitHubRepo)
        {
            PathToGitHubRepo = pathToGitHubRepo;
        }

        public TargetFrameworkSourceItem[] GetAvailableTargets()
        {
            DirectoryInfo binWinForms = new(PathToGitHubRepo + BinSystemWindowsFormsPath);

            return binWinForms.GetDirectories(
                searchPattern: "*.*",
                enumerationOptions: new EnumerationOptions()
                {
                    RecurseSubdirectories = true
                })

                .Select(item => new
                {
                    Directoy = item,
                    MiddlePath = item.FullName.Replace(PathToBinSystemWindowsForms, "")
                })

                .Select(item => new TargetFrameworkSourceItem(
                    name: item.MiddlePath[1..].Replace("\\", " - "),
                    tfmPaths: [item.MiddlePath],
                    directory: item.Directoy))
                .ToArray();
        }

        public DesktopAssemblyInfo[] GetWinFormsRuntimeAssemblies(TargetFrameworkSourceItem target, bool includeRefAssemblies)
        {
            DirectoryInfo binWinForms = new(PathToGitHubRepo + BinPath);

            return binWinForms.GetFiles(
                    searchPattern: "*.dll",
                    enumerationOptions: new EnumerationOptions() { RecurseSubdirectories = true })

                // TfmPaths holds the TFM, for example ".NET9" but also ".NET Standard 2.0", which would apply for ALL TFMs.
                .Where(item => target.TfmPaths.Any(tfmItem => item.Directory!.FullName.EndsWith(tfmItem)))
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
                            ? FindRefAssemblySourceFiles(group.Key, target.TfmPaths)
                            : []
                    };
                })
                .ToArray();

            FileInfo[] FindRefAssemblySourceFiles(DirectoryInfo directory, string[] tfmPaths)
            {
                var uniqueFiles = new HashSet<(string Name, long Size)>();
                var allFiles = new List<FileInfo>();

                foreach (var tfmPath in tfmPaths)
                {
                    var refDirectory = new DirectoryInfo($"{PathToGitHubRepo}{ObjPath}\\{directory.Name}{tfmPath}");
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
                }

                return [.. allFiles];
            }
        }

        public string PathToGitHubRepo { get; }
        public string PathToBinSystemWindowsForms => PathToGitHubRepo + BinSystemWindowsFormsPath;
    }
}
