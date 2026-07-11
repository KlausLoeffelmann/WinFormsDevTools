using DevTools.RuntimeDeploy.Engine.PatchBackup;

namespace DevTools.RuntimeDeploy.Tests;

public sealed class BackupServiceTests : IDisposable
{
    private readonly string _tempFolder = Path.Combine(
        Path.GetTempPath(),
        $"BackupServiceTests-{Guid.NewGuid():N}");

    [Fact]
    public async Task CreateBackupAsync_UsesManifestUtcTimestampInFileName()
    {
        string sourcePath = Path.Combine(_tempFolder, "source", "Example.dll");
        Directory.CreateDirectory(Path.GetDirectoryName(sourcePath)!);
        await File.WriteAllBytesAsync(sourcePath, [1, 2, 3]);

        FileInfo backup = Assert.IsType<FileInfo>(
            await BackupService.CreateBackupAsync(
                [new FileInfo(sourcePath)],
                "net10.0",
                "Debug",
                new DirectoryInfo(Path.Combine(_tempFolder, "backups"))));
        BackupManifest manifest = await BackupService.ReadManifestAsync(backup);

        Assert.Equal(DateTimeKind.Utc, manifest.CreatedUtc.Kind);
        Assert.StartsWith(
            $"Backup_{manifest.CreatedUtc:yyyyMMdd-HHmmss}_",
            backup.Name);
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempFolder))
        {
            Directory.Delete(_tempFolder, recursive: true);
        }
    }
}
