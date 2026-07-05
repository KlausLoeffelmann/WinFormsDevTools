using System.IO.Compression;
using DevTools.RuntimeDeploy.Engine.Domain;

namespace DevTools.RuntimeDeploy.Engine.PatchBackup;

/// <summary>
///  Restores a <c>.netbackup</c> package created by <see cref="BackupService"/>,
///  after checking that the backup is plausible for this machine (its TFM
///  major version must be among the .NET Desktop runtimes installed here).
/// </summary>
public static class RestoreService
{
    /// <summary>
    ///  Restores every file recorded in <paramref name="backupFile"/>'s manifest
    ///  to its original absolute path (or, when <paramref name="targetOverride"/>
    ///  is supplied, into that directory instead, using each file's simple name).
    /// </summary>
    /// <param name="backupFile">The <c>.netbackup</c> file to restore.</param>
    /// <param name="targetOverride">
    ///  Optional directory to restore into instead of each file's original
    ///  recorded location.
    /// </param>
    /// <param name="installedTfmMajorVersions">
    ///  The TFM major versions installed on this machine, used for the
    ///  plausibility check. When omitted, <see cref="FrameworkInfo"/> is
    ///  queried for the installed .NET Desktop shared-runtime versions.
    /// </param>
    /// <exception cref="RestorePlausibilityException">
    ///  The backup's TFM major version is not installed on this machine.
    /// </exception>
    public static async Task<int> RestoreAsync(
        FileInfo backupFile,
        DirectoryInfo? targetOverride = null,
        IEnumerable<int>? installedTfmMajorVersions = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(backupFile);

        BackupManifest manifest = await BackupService.ReadManifestAsync(backupFile, cancellationToken);

        HashSet<int> installedMajors = installedTfmMajorVersions is not null
            ? [.. installedTfmMajorVersions]
            : GetInstalledMajorVersions();

        if (!installedMajors.Contains(manifest.TfmMajorVersion))
        {
            throw new RestorePlausibilityException(
                $"Backup '{backupFile.Name}' targets .NET {manifest.TfmMajorVersion} (TFM '{manifest.Tfm}'), " +
                "but no matching major version of the .NET Desktop runtime is installed on this machine. Refusing to restore.");
        }

        using FileStream zipStream = backupFile.OpenRead();
        using ZipArchive archive = new(zipStream, ZipArchiveMode.Read);

        int restoredCount = 0;

        foreach (BackupFileEntry fileEntry in manifest.Files)
        {
            ZipArchiveEntry? zipEntry = archive.GetEntry(fileEntry.ArchiveEntryName);
            if (zipEntry is null)
            {
                continue;
            }

            string destinationPath = targetOverride is null
                ? fileEntry.OriginalFullPath
                : Path.Combine(targetOverride.FullName, fileEntry.FileName);

            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
            zipEntry.ExtractToFile(destinationPath, overwrite: true);
            restoredCount++;
        }

        return restoredCount;
    }

    /// <summary>
    ///  Returns the major version numbers of every .NET Desktop shared-runtime
    ///  installation found on this machine (e.g. <c>10.0.5</c> -&gt; <c>10</c>).
    /// </summary>
    public static HashSet<int> GetInstalledMajorVersions()
    {
        HashSet<int> majors = [];

        Dictionary<string, DirectoryInfo>? sdks = FrameworkInfo.GetDotNetDesktopSdk(getRefPath: false);
        if (sdks is null)
        {
            return majors;
        }

        foreach (string version in sdks.Keys)
        {
            if (TfmMajorVersionParser.TryParse(version, out int major))
            {
                majors.Add(major);
            }
        }

        return majors;
    }
}
