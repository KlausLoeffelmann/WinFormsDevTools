using System.Text;
using DevTools.RuntimeDeploy.Engine.Infrastructure;

namespace DevTools.RuntimeDeploy.Infrastructure;

public class CommandBatch
{
    private bool _showCommandBatchWindow;
    private bool _dryRun;
    private bool _batchStarted;

    private CommandBatchForm? _commandBatchWindow;
    private StringBuilder? _protocolStorage;
    private bool _newline = true;

    private readonly List<CopyFailure> _failures = [];

    public Task StartBatchAsync(
        bool showCommandBatchWindow = true,
        bool dryRun = false,
        string? windowTitle = null,
        Font? outputFont = null)
    {
        Task batchTask = Task.CompletedTask;

        if (_batchStarted)
        {
            throw new ArgumentException("Batch has already started and cannot be started twice.");
        }

        _batchStarted = true;
        _showCommandBatchWindow = showCommandBatchWindow;
        _dryRun = dryRun;
        _failures.Clear();

        if (_showCommandBatchWindow)
        {
            _commandBatchWindow = new CommandBatchForm(windowTitle, outputFont);
            batchTask = _commandBatchWindow.StartBatchAsync();
        }

        _protocolStorage = new();

        return batchTask;
    }

    public async Task<string> EndBatchAsync(string? endOfBatchComment)
    {
        await WriteFailureSummaryAsync();

        if (!string.IsNullOrEmpty(endOfBatchComment))
        {
            await WriteLineInfoAsync(endOfBatchComment);
        }

        _batchStarted = false;
        await (_commandBatchWindow?.EndBatchAsync() ?? Task.CompletedTask);
        return _protocolStorage!.ToString();
    }

    private async Task WriteFailureSummaryAsync()
    {
        if (_failures.Count == 0)
        {
            return;
        }

        await WriteLineErrorAsync(string.Empty);
        await WriteLineErrorAsync($"{_failures.Count} file(s) could NOT be written:");

        foreach (CopyFailure failure in _failures)
        {
            await WriteLineErrorAsync($"  - {failure.DestinationFile.FullName}");
            await WriteLineErrorAsync($"      Reason: {failure.Reason}");

            if (failure.Lockers.Count == 0)
            {
                await WriteLineErrorAsync("      Locked by: (no holding process could be identified)");
                continue;
            }

            foreach (FileLockProcessInfo locker in failure.Lockers)
            {
                await WriteLineErrorAsync($"      Locked by: {locker}");
            }
        }
    }

    private void CheckBatchStarted()
    {
        if (!_batchStarted)
        {
            throw new ArgumentException("Cannot execute commands when the batch has not started!");
        }
    }

    public Task CopyFileCommandAsync(
        FileInfo sourceFile,
        DirectoryInfo destinationDirectory,
        bool overrideIfExist = false,
        string? comment = default)
    {
        return CopyFileCommandAsync(
            sourceFile,
            new FileInfo($"{destinationDirectory.FullName}\\{sourceFile.Name}"),
            overrideIfExist,
            comment);
    }

    public async Task CopyFileCommandAsync(FileInfo sourceFile, FileInfo destinationFile, bool overrideIfExist, string? comment)
    {
        CheckBatchStarted();

        // We print the comment first, then we check if the source file exists.
        await WriteInfoAsync(comment);

        if (!sourceFile.Exists)
        {
            await WriteLineWarningAsync($"Source file [{sourceFile.Name}] does NOT exists. --> SKIPPING.");
            return;
        }

        if (!destinationFile.Exists)
        {
            await WriteInfoAsync($"Copying [{sourceFile.Name}] to [{destinationFile.Name}] ... ");
            await TryCopyFileAsync(sourceFile, destinationFile);
            return;
        }

        await WriteInfoAsync($"Copying [{sourceFile.Name}] - destination file [{destinationFile.Name}] exists! ");

        if (overrideIfExist)
        {
            await WriteInfoAsync($"--> Overwriting... ");
            await TryCopyFileAsync(sourceFile, destinationFile);
        }
        else
        {
            await WriteLineWarningAsync($"--> SKIPPING.");
        }
    }

    // Performs the actual copy and writes the trailing "result" segment of the
    // current line. On failure the result is written in red, the failure (with any
    // locking process discovered via the Restart Manager) is recorded, and the
    // batch keeps going instead of aborting the whole run.
    private async Task TryCopyFileAsync(FileInfo sourceFile, FileInfo destinationFile)
    {
        if (_dryRun)
        {
            await WriteLineInfoAsync($"OK.");
            return;
        }

        try
        {
            File.Copy(sourceFile.FullName, destinationFile.FullName, overwrite: true);
            await WriteLineInfoAsync($"OK.");
        }
        catch (Exception ex)
        {
            await WriteLineErrorAsync($"FAILED: {ex.Message}");

            IReadOnlyList<FileLockProcessInfo> lockers =
                FileLockInspector.GetLockingProcesses(destinationFile.FullName);

            foreach (FileLockProcessInfo locker in lockers)
            {
                await WriteLineErrorAsync($"    --> Locked by: {locker}");
            }

            _failures.Add(new CopyFailure(sourceFile, destinationFile, ex.Message, lockers));
        }
    }

    private string MessageHeader(string? message)
    {
        message ??= string.Empty;

        if (_newline)
        {
            message = $"[{DateTime.Now:(MM/dd) HH:mm:ss-ff}]: " + message;
            _newline = false;
        }

        return message;
    }

    public async Task WriteInfoAsync(string? message)
    {
        message = MessageHeader(message);
        await (_commandBatchWindow?.WriteAsync(message) ?? Task.CompletedTask);
        _protocolStorage!.Append(message);
    }

    public async Task WriteWarningAsync(string? message)
    {
        message = MessageHeader(message);
        _protocolStorage!.Append(message);
        await (_commandBatchWindow?.WriteWarningAsync(message) ?? Task.CompletedTask);
    }

    public async Task WriteErrorAsync(string? message)
    {
        message = MessageHeader(message);
        _protocolStorage!.Append(message);
        await (_commandBatchWindow?.WriteErrorAsync(message) ?? Task.CompletedTask);
    }

    public async Task WriteLineInfoAsync(string? message)
    {
        message = MessageHeader(message);
        _protocolStorage!.Append(message + "\r\n");
        await (_commandBatchWindow?.WriteLineAsync(message) ?? Task.CompletedTask);
        _newline = true;
    }

    public async Task WriteLineWarningAsync(string? message)
    {
        message = MessageHeader(message);
        _protocolStorage!.Append(message + "\r\n");
        await (_commandBatchWindow?.WriteLineWarningAsync(message) ?? Task.CompletedTask);
        _newline = true;
    }

    public async Task WriteLineErrorAsync(string? message)
    {
        message = MessageHeader(message);
        _protocolStorage!.Append(message + "\r\n");
        await (_commandBatchWindow?.WriteLineErrorAsync(message) ?? Task.CompletedTask);
        _newline = true;
    }

    private sealed record CopyFailure(
        FileInfo SourceFile,
        FileInfo DestinationFile,
        string Reason,
        IReadOnlyList<FileLockProcessInfo> Lockers);
}
