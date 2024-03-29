﻿namespace WinFormsDevTools
{
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

                .Select(item => new TargetFrameworkSourceItem
                {
                    Name = item.MiddlePath[1..].Replace("\\", " - "),
                    TfmPath = item.MiddlePath,
                    Directory = item.Directoy
                })
                .ToArray();
        }

        public DesktopAssemblyInfo[] GetWinFormsRuntimeAssemblies(TargetFrameworkSourceItem target, bool includeRefAssemblies)
        {
            DirectoryInfo binWinForms = new(PathToGitHubRepo + BinPath);

            return binWinForms.GetFiles(
                searchPattern: "*.dll",
                enumerationOptions: new EnumerationOptions()
                {
                    RecurseSubdirectories = true
                })
                .Where(item => item.Directory!.FullName.EndsWith(target.TfmPath))
                .GroupBy(
                    keySelector: item => item.Directory!.Parent!.Parent!,
                    elementSelector: elementItem => elementItem,
                    comparer: DirectoryInfoComparer.Instance)
                .Select(item => new DesktopAssemblyInfo()
                {
                    Path = item.Key,
                    Name = item.Key.Name,
                    AssemblyFiles = item.ToArray(),
                    RefAssemblyFiles = FindRefAssemblySourceFiles(item.Key, target.TfmPath)
                })
                .ToArray();

            FileInfo[]? FindRefAssemblySourceFiles(DirectoryInfo directory, string TfmPath)
            {
                var refDirectory = new DirectoryInfo($"{PathToGitHubRepo}{ObjPath}\\{directory.Name}{TfmPath}");
                if (refDirectory.Exists)
                {
                    var filesToReturn=refDirectory.GetFiles("*.dll");
                    return filesToReturn;
                }

                return null;
            }
        }

        public string PathToGitHubRepo { get; }
        public string PathToBinSystemWindowsForms => PathToGitHubRepo + BinSystemWindowsFormsPath;
    }
}
