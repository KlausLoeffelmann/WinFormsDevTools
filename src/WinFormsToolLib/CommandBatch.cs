using System.Text;

namespace WinFormsToolLib;

public class CommandBatch
{
    private bool _showCommandBatchWindow;
    private bool _dryRun;
    private bool _batchStarted;

    private CommandBatchForm? _commandBatchWindow;
    private StringBuilder? _protocolStorage;
    private bool _newline = true;

    public async Task StartBatchAsync(
        bool showCommandBatchWindow = true,
        bool dryRun = false,
        string? windowTitle = null)
    {
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
            await _commandBatchWindow.StartBatchAsync();
        }

        _protocolStorage = new();
    }

    public string EndBatch(string? endOfBatchComment)
    {
        if (!string.IsNullOrEmpty(endOfBatchComment))
        {
            InfoPrintLineAsync(endOfBatchComment);
        }

        _batchStarted = false;
        _commandBatchWindow?.EndBatch();
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
        await InfoPrintAsync(comment);

        if (!sourceFile.Exists)
        {
            await InfoPrintLineAsync($"Source file [{sourceFile.Name}] does NOT exists. --> SKIPPING.");
            return;
        }

        if (!destinationFile.Exists)
        {
            await InfoPrintAsync($"Copying [{sourceFile.Name}] to [{destinationFile.Name}] ... ");

            if (!_dryRun)
            {
                await InfoPrintLineAsync($"{await sourceFile.CopyToAsync(destinationFile.FullName)}");
            }
            else
            {
                await InfoPrintLineAsync($"OK.");
            }

            return;
        }

        await InfoPrintAsync($"Copying [{sourceFile.Name}] - destination file [{destinationFile.Name}] exists! ");

        if (overrideIfExist)
        {
            await InfoPrintAsync($"--> Overwriting... ");

            if (!_dryRun)
            {
                await InfoPrintLineAsync($"{await sourceFile.CopyToAsync(destinationFile.FullName)}");
            }
            else
            {
                await InfoPrintLineAsync($"OK.");
            }
        }
        else
        {
            await InfoPrintLineAsync($"--> SKIPPING.");
        }

    }

    private string MessageHeader(string? message)
    {
        message ??= String.Empty;

        if (_newline)
        {
            message = $"[{DateTime.Now:(MM/dd) HH:mm:ss-ff}]: " + message;
            _newline = false;
        }

        return message;
    }

    public Task InfoPrintAsync(string? message)
    {
        message = MessageHeader(message);

        var task = _commandBatchWindow?.PrintAsync(message);

        _protocolStorage!.Append(message);

        return task ?? Task.CompletedTask;
    }

    public Task InfoPrintLineAsync(string? message)
    {
        message = MessageHeader(message);

        var task = _commandBatchWindow?.PrintLineAsync(message);

        _protocolStorage!.Append(message + "\r\n");
        _newline = true;

        return task ?? Task.CompletedTask;
    }
}
