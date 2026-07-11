namespace DevTools.RuntimeDeploy.Engine.Copying;

/// <summary>
///  Receives textual progress output from <see cref="CopyEngine"/> as it copies
///  files. Implementations decide how (and whether) to render the messages -
///  e.g. a WinForms console window, a plain console, or an in-memory log.
/// </summary>
public interface ICopyProgressSink
{
    Task WriteInfoAsync(string? message);

    Task WriteWarningAsync(string? message);

    Task WriteErrorAsync(string? message);

    Task WriteLineInfoAsync(string? message);

    Task WriteLineWarningAsync(string? message);

    Task WriteLineErrorAsync(string? message);
}
