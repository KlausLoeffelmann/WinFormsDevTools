using DevTools.RuntimeDeploy.Engine.PatchBackup;

namespace DevTools.RuntimeDeploy.Cli;

/// <summary>
///  Implements the "restore" (apply a <c>.netbackup</c>) command.
/// </summary>
public static class RestoreCommand
{
    public static async Task<int> RunAsync(CliOptions options)
    {
        FileInfo? backupFile = await ResolveBackupFileAsync(options);
        if (backupFile is null)
        {
            return 1;
        }

        BackupManifest manifest = await BackupService.ReadManifestAsync(backupFile);

        Console.WriteLine($"Backup: {backupFile.Name}");
        Console.WriteLine($"  Created:       {manifest.CreatedUtc:yyyy-MM-dd HH:mm} UTC");
        Console.WriteLine($"  TFM:           {manifest.Tfm} (major version {manifest.TfmMajorVersion})");
        Console.WriteLine($"  Configuration: {manifest.Configuration}");
        Console.WriteLine($"  Files:         {manifest.Files.Count}");
        Console.WriteLine();

        if (!options.Yes)
        {
            Console.Write("Restore this backup? [y/N] ");
            string? response = Console.ReadLine();
            if (!string.Equals(response?.Trim(), "y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Aborted - no changes were made.");
                return 2;
            }
        }

        DirectoryInfo? targetOverride = string.IsNullOrWhiteSpace(options.TargetDirectory)
            ? null
            : new DirectoryInfo(options.TargetDirectory);

        try
        {
            int restoredCount = await RestoreService.RestoreAsync(backupFile, targetOverride);
            Console.WriteLine($"Restored {restoredCount} file(s).");
            return 0;
        }
        catch (RestorePlausibilityException ex)
        {
            Console.Error.WriteLine($"ERROR: {ex.Message}");
            return 1;
        }
    }

    private static async Task<FileInfo?> ResolveBackupFileAsync(CliOptions options)
    {
        if (!string.IsNullOrWhiteSpace(options.RestorePath) && File.Exists(options.RestorePath) &&
            options.RestorePath.EndsWith(BackupFinder.BackupFileExtension, StringComparison.OrdinalIgnoreCase))
        {
            return new FileInfo(options.RestorePath);
        }

        DirectoryInfo searchRoot = !string.IsNullOrWhiteSpace(options.RestorePath) && Directory.Exists(options.RestorePath)
            ? new DirectoryInfo(options.RestorePath)
            : string.IsNullOrWhiteSpace(options.BackupRoot)
                ? BackupService.DefaultBackupRoot
                : new DirectoryInfo(options.BackupRoot);

        IReadOnlyList<BackupSearchResult> results = await BackupFinder.FindBackupsAsync(
            searchRoot,
            maxDepth: options.MaxDepth,
            tfmFilter: options.TfmFilter,
            configurationFilter: options.ConfigFilter);

        if (results.Count == 0)
        {
            Console.Error.WriteLine($"No backups found under '{searchRoot.FullName}'.");
            return null;
        }

        BackupSearchResult? selected = ConsoleBackupPicker.Pick(results);
        return selected?.BackupFile;
    }
}
