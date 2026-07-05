namespace DevTools.RuntimeDeploy.Engine.PatchBackup;

/// <summary>
///  One backup file discovered by <see cref="BackupFinder"/>, together with
///  its manifest so callers can display/filter without re-reading the zip.
/// </summary>
public sealed record BackupSearchResult(FileInfo BackupFile, BackupManifest Manifest);

/// <summary>
///  Recursively finds <c>.netbackup</c> files under a root folder, optionally
///  filtered by TFM or configuration, sorted by date then TFM (both descending).
/// </summary>
public static class BackupFinder
{
    public const string BackupFileExtension = ".netbackup";

    /// <summary>
    ///  Default recursion depth used when the caller does not specify one.
    /// </summary>
    public const int DefaultMaxDepth = 3;

    /// <summary>
    ///  Finds every <c>.netbackup</c> file under <paramref name="root"/>, up to
    ///  <paramref name="maxDepth"/> levels deep, optionally filtered by TFM
    ///  and/or configuration. Results are sorted by creation date descending,
    ///  then by TFM major version descending.
    /// </summary>
    public static async Task<IReadOnlyList<BackupSearchResult>> FindBackupsAsync(
        DirectoryInfo root,
        int maxDepth = DefaultMaxDepth,
        string? tfmFilter = null,
        string? configurationFilter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(root);

        List<BackupSearchResult> results = [];

        if (!root.Exists)
        {
            return results;
        }

        foreach (FileInfo file in EnumerateBackupFiles(root, maxDepth))
        {
            BackupManifest manifest;
            try
            {
                manifest = await BackupService.ReadManifestAsync(file, cancellationToken);
            }
            catch
            {
                // Skip files that are not readable backup packages (e.g. corrupted
                // or partially-written) instead of failing the whole search.
                continue;
            }

            if (tfmFilter is not null && !string.Equals(manifest.Tfm, tfmFilter, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (configurationFilter is not null && !string.Equals(manifest.Configuration, configurationFilter, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            results.Add(new BackupSearchResult(file, manifest));
        }

        return [.. results
            .OrderByDescending(r => r.Manifest.CreatedUtc)
            .ThenByDescending(r => r.Manifest.TfmMajorVersion)];
    }

    private static IEnumerable<FileInfo> EnumerateBackupFiles(DirectoryInfo directory, int depthRemaining)
    {
        foreach (FileInfo file in directory.EnumerateFiles($"*{BackupFileExtension}"))
        {
            yield return file;
        }

        if (depthRemaining <= 0)
        {
            yield break;
        }

        foreach (DirectoryInfo subDirectory in directory.EnumerateDirectories())
        {
            foreach (FileInfo file in EnumerateBackupFiles(subDirectory, depthRemaining - 1))
            {
                yield return file;
            }
        }
    }
}
