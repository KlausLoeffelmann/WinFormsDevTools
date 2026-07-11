namespace DevTools.RuntimeDeploy.Engine.PatchBackup;

/// <summary>
///  Metadata describing one file captured by a backup, including the exact
///  absolute path it was copied from so a later restore can put it back in
///  precisely the same place.
/// </summary>
public sealed record BackupFileEntry(
    string FileName,
    string ArchiveEntryName,
    string OriginalFullPath,
    long SizeBytes,
    string Sha256);

/// <summary>
///  The <c>backup-manifest.json</c> entry stored alongside the captured
///  files inside a <c>.netbackup</c> package (a plain zip archive).
/// </summary>
public sealed record BackupManifest(
    int SchemaVersion,
    DateTime CreatedUtc,
    string Tfm,
    int TfmMajorVersion,
    string Configuration,
    IReadOnlyList<BackupFileEntry> Files)
{
    /// <summary>
    ///  Current schema version written by this build of the tool. Bump this
    ///  when the shape of <see cref="BackupManifest"/> changes in a way that
    ///  is not purely additive.
    /// </summary>
    public const int CurrentSchemaVersion = 1;
}
