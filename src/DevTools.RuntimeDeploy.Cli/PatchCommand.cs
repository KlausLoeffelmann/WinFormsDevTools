using DevTools.RuntimeDeploy.Engine.Domain;
using DevTools.RuntimeDeploy.Engine.PatchBackup;
using DevTools.RuntimeDeploy.Engine.Packaging;

namespace DevTools.RuntimeDeploy.Cli;

/// <summary>
///  Implements the "patch" (apply a <c>.netdeploy</c> package) command.
/// </summary>
public static class PatchCommand
{
    public static async Task<int> RunAsync(CliOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.PackagePath))
        {
            Console.Error.WriteLine("No package specified. Use --package <path-to.netdeploy> or run next to a runtimepatcher.settings.json file.");
            return 1;
        }

        FileInfo packageFile = new(options.PackagePath);
        if (!packageFile.Exists)
        {
            Console.Error.WriteLine($"Package file not found: '{packageFile.FullName}'.");
            return 1;
        }

        PackageManifest manifest = await PackageBuilder.ReadManifestAsync(packageFile);

        Console.WriteLine($"Package: {packageFile.Name}");
        Console.WriteLine($"  TFM:           {manifest.Tfm} (major version {manifest.TfmMajorVersion})");
        Console.WriteLine($"  Configuration: {manifest.Configuration}");
        Console.WriteLine($"  Assemblies:    {manifest.Assemblies.Count}");
        Console.WriteLine();

        DirectoryInfo targetDirectory;

        if (!string.IsNullOrWhiteSpace(options.TargetDirectory))
        {
            targetDirectory = new DirectoryInfo(options.TargetDirectory);
        }
        else
        {
            Dictionary<string, DirectoryInfo>? installedSdks = FrameworkInfo.GetDotNetDesktopSdk(getRefPath: false);

            var matches = (installedSdks ?? [])
                .Where(kvp => TfmMajorVersionParser.TryParse(kvp.Key, out int major) && major == manifest.TfmMajorVersion)
                .ToList();

            if (matches.Count == 0)
            {
                Console.Error.WriteLine(
                    $"ERROR: No .NET {manifest.TfmMajorVersion} Desktop runtime is installed on this machine. " +
                    "Refusing to apply the patch. Install a matching .NET Desktop runtime first.");
                return 1;
            }

            // FrameworkInfo.GetDotNetDesktopSdk orders ascending by version, so the
            // last matching entry is the highest installed version for this major.
            KeyValuePair<string, DirectoryInfo> selected = matches[^1];
            targetDirectory = selected.Value;

            Console.WriteLine($"Detected installed .NET Desktop runtime: {selected.Key} at '{targetDirectory.FullName}'.");
        }

        if (!options.Yes)
        {
            Console.Write($"Apply this patch to '{targetDirectory.FullName}'? [y/N] ");
            string? response = Console.ReadLine();
            if (!string.Equals(response?.Trim(), "y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Aborted - no changes were made.");
                return 2;
            }
        }

        IEnumerable<FileInfo> filesAboutToBeOverwritten = manifest.Assemblies
            .Select(a => new FileInfo(Path.Combine(targetDirectory.FullName, a.FileName)))
            .Where(f => f.Exists);

        DirectoryInfo backupRoot = string.IsNullOrWhiteSpace(options.BackupRoot)
            ? BackupService.DefaultBackupRoot
            : new DirectoryInfo(options.BackupRoot);

        FileInfo? backupFile = await BackupService.CreateBackupAsync(
            filesAboutToBeOverwritten,
            manifest.Tfm,
            manifest.Configuration,
            backupRoot);

        if (backupFile is not null)
        {
            Console.WriteLine($"Backed up existing files to: {backupFile.FullName}");
        }
        else
        {
            Console.WriteLine("No existing files needed backing up.");
        }

        if (options.DryRun)
        {
            Console.WriteLine($"[Dry run] Would extract {manifest.Assemblies.Count} file(s) to '{targetDirectory.FullName}'.");
            return 0;
        }

        PackageBuilder.ExtractAssemblies(packageFile, targetDirectory);
        Console.WriteLine($"Applied {manifest.Assemblies.Count} file(s) to '{targetDirectory.FullName}'.");

        return 0;
    }
}
