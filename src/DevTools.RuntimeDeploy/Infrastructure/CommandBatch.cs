using System.Text;
using DevTools.RuntimeDeploy.Engine.Copying;

namespace DevTools.RuntimeDeploy.Infrastructure;

/// <summary>
///  WinForms adapter around <see cref="CopyEngine"/>: renders progress into an
///  optional <see cref="CommandBatchForm"/> console window and accumulates the
///  full transcript into a protocol string, while delegating the actual file
///  copy work (and lock-holder diagnostics) to the headless engine.
/// </summary>
public class CommandBatch : ICopyProgressSink
{
    private bool _batchStarted;

    private CommandBatchForm? _commandBatchWindow;
    private StringBuilder? _protocolStorage;
    private bool _newline = true;

    private CopyEngine? _copyEngine;

    public Task StartBatchAsync(
        bool showCommandBatchWindow = true,
        bool dryRun = false,
        string? windowTitle = null,
        Font? outputFont = null,
        RuntimeDeploySettingsService? settingsService = null)
    {
        Task batchTask = Task.CompletedTask;

        if (_batchStarted)
        {
            throw new ArgumentException("Batch has already started and cannot be started twice.");
        }

        _batchStarted = true;
        _copyEngine = new CopyEngine(this, dryRun);

        if (showCommandBatchWindow)
        {
            _commandBatchWindow = new CommandBatchForm(windowTitle, outputFont, settingsService);
            batchTask = _commandBatchWindow.StartBatchAsync();
        }

        _protocolStorage = new();

        return batchTask;
    }

    public async Task<string> EndBatchAsync(string? endOfBatchComment)
    {
        await (_copyEngine?.WriteFailureSummaryAsync() ?? Task.CompletedTask);

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
        CheckBatchStarted();
        return _copyEngine!.CopyFileAsync(sourceFile, destinationDirectory, overrideIfExist, comment);
    }

    public Task CopyFileCommandAsync(FileInfo sourceFile, FileInfo destinationFile, bool overrideIfExist, string? comment)
    {
        CheckBatchStarted();
        return _copyEngine!.CopyFileAsync(sourceFile, destinationFile, overrideIfExist, comment);
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
