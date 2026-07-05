using DevTools.RuntimeDeploy.Engine.Infrastructure;

namespace DevTools.RuntimeDeploy.Engine.Copying;

/// <summary>
///  Describes a single file copy that failed, including any process(es) found
///  to be holding a lock on the destination file.
/// </summary>
public sealed record CopyFailure(
    FileInfo SourceFile,
    FileInfo DestinationFile,
    string Reason,
    IReadOnlyList<FileLockProcessInfo> Lockers);

/// <summary>
///  Headless (no UI dependency) engine that copies files one at a time,
///  reporting progress through an <see cref="ICopyProgressSink"/> and
///  recording failures (including lock-holder diagnostics) for later
///  summarisation. Extracted from the former
///  <c>DevTools.RuntimeDeploy.Infrastructure.CommandBatch</c> so it can be
///  reused by both the WinForms UI and the CLI tool.
/// </summary>
public sealed class CopyEngine(ICopyProgressSink sink, bool dryRun = false)
{
    private readonly List<CopyFailure> _failures = [];

    public IReadOnlyList<CopyFailure> Failures => _failures;

    public Task CopyFileAsync(
        FileInfo sourceFile,
        DirectoryInfo destinationDirectory,
        bool overrideIfExist = false,
        string? comment = default)
    {
        return CopyFileAsync(
            sourceFile,
            new FileInfo($"{destinationDirectory.FullName}\\{sourceFile.Name}"),
            overrideIfExist,
            comment);
    }

    public async Task CopyFileAsync(FileInfo sourceFile, FileInfo destinationFile, bool overrideIfExist, string? comment)
    {
        // We print the comment first, then we check if the source file exists.
        await sink.WriteInfoAsync(comment);

        if (!sourceFile.Exists)
        {
            await sink.WriteLineWarningAsync($"Source file [{sourceFile.Name}] does NOT exists. --> SKIPPING.");
            return;
        }

        if (!destinationFile.Exists)
        {
            await sink.WriteInfoAsync($"Copying [{sourceFile.Name}] to [{destinationFile.Name}] ... ");
            await TryCopyFileAsync(sourceFile, destinationFile);
            return;
        }

        await sink.WriteInfoAsync($"Copying [{sourceFile.Name}] - destination file [{destinationFile.Name}] exists! ");

        if (overrideIfExist)
        {
            await sink.WriteInfoAsync($"--> Overwriting... ");
            await TryCopyFileAsync(sourceFile, destinationFile);
        }
        else
        {
            await sink.WriteLineWarningAsync($"--> SKIPPING.");
        }
    }

    // Performs the actual copy. On failure the result is reported through the sink,
    // the failure (with any locking process discovered via the Restart Manager) is
    // recorded, and the batch keeps going instead of aborting the whole run.
    private async Task TryCopyFileAsync(FileInfo sourceFile, FileInfo destinationFile)
    {
        if (dryRun)
        {
            await sink.WriteLineInfoAsync($"OK.");
            return;
        }

        try
        {
            File.Copy(sourceFile.FullName, destinationFile.FullName, overwrite: true);
            await sink.WriteLineInfoAsync($"OK.");
        }
        catch (Exception ex)
        {
            await sink.WriteLineErrorAsync($"FAILED: {ex.Message}");

            IReadOnlyList<FileLockProcessInfo> lockers =
                FileLockInspector.GetLockingProcesses(destinationFile.FullName);

            foreach (FileLockProcessInfo locker in lockers)
            {
                await sink.WriteLineErrorAsync($"    --> Locked by: {locker}");
            }

            _failures.Add(new CopyFailure(sourceFile, destinationFile, ex.Message, lockers));
        }
    }

    public async Task WriteFailureSummaryAsync()
    {
        if (_failures.Count == 0)
        {
            return;
        }

        await sink.WriteLineErrorAsync(string.Empty);
        await sink.WriteLineErrorAsync($"{_failures.Count} file(s) could NOT be written:");

        foreach (CopyFailure failure in _failures)
        {
            await sink.WriteLineErrorAsync($"  - {failure.DestinationFile.FullName}");
            await sink.WriteLineErrorAsync($"      Reason: {failure.Reason}");

            if (failure.Lockers.Count == 0)
            {
                await sink.WriteLineErrorAsync("      Locked by: (no holding process could be identified)");
                continue;
            }

            foreach (FileLockProcessInfo locker in failure.Lockers)
            {
                await sink.WriteLineErrorAsync($"      Locked by: {locker}");
            }
        }
    }
}
