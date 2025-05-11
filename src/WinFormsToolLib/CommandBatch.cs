using System.Text;

namespace DevTools.Libs;

public class CommandBatch
{
    private bool _showCommandBatchWindow;
    private bool _dryRun;
    private bool _batchStarted;

    private CommandBatchForm? _commandBatchWindow;
    private StringBuilder? _protocolStorage;
    private bool _newline = true;

    public Task StartBatchAsync(
        bool showCommandBatchWindow = true,
        bool dryRun = false,
        string? windowTitle = null)
    {
        Task batchTask = Task.CompletedTask;

        if (_batchStarted)
        {
            throw new ArgumentException("Batch has already started and cannot be started twice.");
        }

        _batchStarted = true;
        _showCommandBatchWindow = showCommandBatchWindow;
        _dryRun = dryRun;

        if (_showCommandBatchWindow)
        {
            _commandBatchWindow = new CommandBatchForm(windowTitle);
            batchTask = _commandBatchWindow.StartBatchAsync();
        }

        _protocolStorage = new();

        return batchTask;
    }

    public async Task<string> EndBatchAsync(string? endOfBatchComment)
    {
        if (!string.IsNullOrEmpty(endOfBatchComment))
        {
            await WriteLineInfoAsync(endOfBatchComment);
        }

        _batchStarted = false;
        await (_commandBatchWindow?.EndBatchAsync() ?? Task.CompletedTask);
        return _protocolStorage!.ToString();
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

            if (!_dryRun)
            {
                await WriteLineInfoAsync($"{await sourceFile.CopyToAsync(destinationFile.FullName)}");
            }
            else
            {
                await WriteLineInfoAsync($"OK.");
            }

            return;
        }

        await WriteInfoAsync($"Copying [{sourceFile.Name}] - destination file [{destinationFile.Name}] exists! ");

        if (overrideIfExist)
        {
            await WriteInfoAsync($"--> Overwriting... ");

            if (!_dryRun)
            {
                await WriteLineInfoAsync($"{await sourceFile.CopyToAsync(destinationFile.FullName)}");
            }
            else
            {
                await WriteLineInfoAsync($"OK.");
            }
        }
        else
        {
            await WriteLineWarningAsync($"--> SKIPPING.");
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
}
