using System.IO.Compression;
using System.Text.Json;
using DevTools.RuntimeDeploy.Engine.Domain;
using DevTools.RuntimeDeploy.Engine.Json;
using DevTools.RuntimeDeploy.Engine.Packaging;

namespace DevTools.RuntimeDeploy.Engine.PatchBackup;

/// <summary>
///  Creates <c>.netbackup</c> packages: a snapshot of the exact files that
///  are about to be overwritten by a deploy/patch operation, together with a
///  <c>backup-manifest.json</c> recording each file's original absolute
///  target path so a later restore can put it back exactly where it came
///  from.
/// </summary>
public static class BackupService
{
    public const string ManifestEntryName = "backup-manifest.json";

    /// <summary>
    ///  Default backup root: <c>%LOCALAPPDATA%\WinFormsDevTools\RuntimeDeploy\Backups</c>.
    ///  Contains no spaces, per project convention for generated file/folder names.
    /// </summary>
    public static DirectoryInfo DefaultBackupRoot { get; } = new(
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "WinFormsDevTools",
            "RuntimeDeploy",
            "Backups"));

    /// <summary>
    ///  Backs up every file in <paramref name="filesAboutToBeOverwritten"/> - which
    ///  must currently exist on disk - into a new <c>.netbackup</c> file under
    ///  <paramref name="backupRoot"/>, named
    ///  <c>Backup_{yyyyMMdd-HHmmss}_{tfm}_{configuration}.netbackup</c> (no spaces).
    /// </summary>
    /// <returns>The created backup file, or <see langword="null"/> if there was nothing to back up.</returns>
    public static async Task<FileInfo?> CreateBackupAsync(
        IEnumerable<FileInfo> filesAboutToBeOverwritten,
        string tfm,
        string configuration,
        DirectoryInfo backupRoot,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filesAboutToBeOverwritten);
        ArgumentException.ThrowIfNullOrEmpty(tfm);
        ArgumentException.ThrowIfNullOrEmpty(configuration);
        ArgumentNullException.ThrowIfNull(backupRoot);

        FileInfo[] existingFiles = [.. filesAboutToBeOverwritten.Where(f => f.Exists)];
        if (existingFiles.Length == 0)
        {
            return null;
        }

        backupRoot.Create();

        string safeTfm = MakeSafeNameSegment(tfm);
        string safeConfiguration = MakeSafeNameSegment(configuration);
        string fileName = $"Backup_{DateTime.Now:yyyyMMdd-HHmmss}_{safeTfm}_{safeConfiguration}.netbackup";
        FileInfo backupFile = new(Path.Combine(backupRoot.FullName, fileName));

        List<BackupFileEntry> entries = [];

        using (FileStream zipStream = backupFile.Create())
        using (ZipArchive archive = new(zipStream, ZipArchiveMode.Create))
        {
            HashSet<string> usedEntryNames = new(StringComparer.OrdinalIgnoreCase);

            foreach (FileInfo file in existingFiles)
            {
                string entryName = MakeUniqueEntryName(file.Name, usedEntryNames);
                string sha256 = await PackageBuilder.ComputeSha256Async(file, cancellationToken);

                archive.CreateEntryFromFile(file.FullName, entryName, CompressionLevel.Optimal);

                entries.Add(new BackupFileEntry(
                    FileName: file.Name,
                    ArchiveEntryName: entryName,
                    OriginalFullPath: file.FullName,
                    SizeBytes: file.Length,
                    Sha256: sha256));
            }

            BackupManifest manifest = new(
                SchemaVersion: BackupManifest.CurrentSchemaVersion,
                CreatedUtc: DateTime.UtcNow,
                Tfm: tfm,
                TfmMajorVersion: TfmMajorVersionParser.Parse(tfm),
                Configuration: configuration,
                Files: entries);

            ZipArchiveEntry manifestEntry = archive.CreateEntry(ManifestEntryName, CompressionLevel.Optimal);
            using Stream manifestStream = manifestEntry.Open();
            await JsonSerializer.SerializeAsync(manifestStream, manifest, EngineJsonContext.Default.BackupManifest, cancellationToken);
        }

        return backupFile;
    }

    /// <summary>
    ///  Reads the <see cref="BackupManifest"/> out of an existing
    ///  <c>.netbackup</c> package.
    /// </summary>
    public static async Task<BackupManifest> ReadManifestAsync(FileInfo backupFile, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(backupFile);

        using FileStream zipStream = backupFile.OpenRead();
        using ZipArchive archive = new(zipStream, ZipArchiveMode.Read);

        ZipArchiveEntry manifestEntry = archive.GetEntry(ManifestEntryName)
            ?? throw new InvalidDataException($"'{backupFile.FullName}' does not contain a '{ManifestEntryName}' entry.");

        using Stream manifestStream = manifestEntry.Open();
        return await JsonSerializer.DeserializeAsync(manifestStream, EngineJsonContext.Default.BackupManifest, cancellationToken)
            ?? throw new InvalidDataException($"'{backupFile.FullName}' has an unreadable manifest.");
    }

    /// <summary>
    ///  Replaces characters that are awkward or illegal in file names (spaces,
    ///  path separators, wildcards, etc.) with '-' so generated backup/package
    ///  names never contain spaces.
    /// </summary>
    internal static string MakeSafeNameSegment(string value)
    {
        Span<char> buffer = stackalloc char[value.Length];
        for (int i = 0; i < value.Length; i++)
        {
            char c = value[i];
            buffer[i] = char.IsLetterOrDigit(c) || c is '-' or '_' or '.' ? c : '-';
        }

        return new string(buffer);
    }

    private static string MakeUniqueEntryName(string fileName, HashSet<string> usedEntryNames)
    {
        if (usedEntryNames.Add(fileName))
        {
            return fileName;
        }

        string baseName = Path.GetFileNameWithoutExtension(fileName);
        string extension = Path.GetExtension(fileName);

        for (int suffix = 2; ; suffix++)
        {
            string candidate = $"{baseName}~{suffix}{extension}";
            if (usedEntryNames.Add(candidate))
            {
                return candidate;
            }
        }
    }
}
