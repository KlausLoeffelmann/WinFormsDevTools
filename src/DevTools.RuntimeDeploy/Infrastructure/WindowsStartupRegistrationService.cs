using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;

namespace DevTools.RuntimeDeploy.Infrastructure;

public interface IStartupRegistrationService
{
    Task SetEnabledAsync(bool enabled, CancellationToken cancellationToken = default);
}

public sealed class StartupRegistrationException : Exception
{
    public StartupRegistrationException(string message)
        : base(message)
    {
    }

    public StartupRegistrationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public sealed class WindowsStartupRegistrationService : IStartupRegistrationService
{
    internal const string TaskNamePrefix = "WinFormsDevTools Runtime Deploy";
    internal const int TaskNotFoundExitCode = unchecked((int)0x80070002);

    private readonly Func<ProcessStartInfo, CancellationToken, Task<ProcessExecutionResult>> _processRunner;
    private readonly Func<string> _executablePathProvider;

    public WindowsStartupRegistrationService()
        : this(RunProcessAsync, GetExecutablePath)
    {
    }

    internal WindowsStartupRegistrationService(
        Func<ProcessStartInfo, CancellationToken, Task<ProcessExecutionResult>> processRunner,
        Func<string> executablePathProvider)
    {
        _processRunner = processRunner;
        _executablePathProvider = executablePathProvider;
    }

    public async Task SetEnabledAsync(bool enabled, CancellationToken cancellationToken = default)
    {
        if (enabled)
        {
            await ExecuteRequiredAsync(
                CreateRegistrationStartInfo(_executablePathProvider()),
                "create or update",
                cancellationToken);
            return;
        }

        ProcessExecutionResult queryResult = await ExecuteAsync(
            CreateQueryStartInfo(),
            cancellationToken);

        if (queryResult.ExitCode == TaskNotFoundExitCode)
        {
            return;
        }

        ThrowIfFailed(queryResult, "query");

        ProcessExecutionResult deletionResult = await ExecuteAsync(
            CreateDeletionStartInfo(),
            cancellationToken);

        if (deletionResult.ExitCode != TaskNotFoundExitCode)
        {
            ThrowIfFailed(deletionResult, "remove");
        }
    }

    internal static string TaskName
    {
        get
        {
            using WindowsIdentity identity = WindowsIdentity.GetCurrent();
            string userSid = identity.User?.Value
                ?? throw new StartupRegistrationException(
                    "The current Windows user SID could not be determined.");

            return $"{TaskNamePrefix} ({userSid})";
        }
    }

    internal static ProcessStartInfo CreateRegistrationStartInfo(string executablePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(executablePath);

        ProcessStartInfo startInfo = CreateStartInfo();
        startInfo.ArgumentList.Add("/Create");
        startInfo.ArgumentList.Add("/SC");
        startInfo.ArgumentList.Add("ONLOGON");
        startInfo.ArgumentList.Add("/TN");
        startInfo.ArgumentList.Add(TaskName);
        startInfo.ArgumentList.Add("/TR");
        startInfo.ArgumentList.Add($"\"{executablePath}\" {RuntimeDeployLaunchOptions.StartInTrayArgument}");
        startInfo.ArgumentList.Add("/RL");
        startInfo.ArgumentList.Add("HIGHEST");
        startInfo.ArgumentList.Add("/IT");
        startInfo.ArgumentList.Add("/F");
        startInfo.ArgumentList.Add("/HResult");
        return startInfo;
    }

    internal static ProcessStartInfo CreateQueryStartInfo()
    {
        ProcessStartInfo startInfo = CreateStartInfo();
        startInfo.ArgumentList.Add("/Query");
        startInfo.ArgumentList.Add("/TN");
        startInfo.ArgumentList.Add(TaskName);
        startInfo.ArgumentList.Add("/HResult");
        return startInfo;
    }

    internal static ProcessStartInfo CreateDeletionStartInfo()
    {
        ProcessStartInfo startInfo = CreateStartInfo();
        startInfo.ArgumentList.Add("/Delete");
        startInfo.ArgumentList.Add("/TN");
        startInfo.ArgumentList.Add(TaskName);
        startInfo.ArgumentList.Add("/F");
        startInfo.ArgumentList.Add("/HResult");
        return startInfo;
    }

    private async Task ExecuteRequiredAsync(
        ProcessStartInfo startInfo,
        string operation,
        CancellationToken cancellationToken)
    {
        ProcessExecutionResult result = await ExecuteAsync(startInfo, cancellationToken);
        ThrowIfFailed(result, operation);
    }

    private static void ThrowIfFailed(ProcessExecutionResult result, string operation)
    {
        if (result.ExitCode == 0)
        {
            return;
        }

        string details = string.IsNullOrWhiteSpace(result.StandardError)
            ? result.StandardOutput
            : result.StandardError;

        throw new StartupRegistrationException(
            $"Could not {operation} the Windows startup task (exit code {result.ExitCode}). {details.Trim()}");
    }

    private async Task<ProcessExecutionResult> ExecuteAsync(
        ProcessStartInfo startInfo,
        CancellationToken cancellationToken)
    {
        try
        {
            return await _processRunner(startInfo, cancellationToken);
        }
        catch (Win32Exception ex)
        {
            throw new StartupRegistrationException(
                "Windows Task Scheduler could not be started.",
                ex);
        }
    }

    private static ProcessStartInfo CreateStartInfo()
        => new("schtasks.exe")
        {
            CreateNoWindow = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

    private static string GetExecutablePath()
        => Environment.ProcessPath
            ?? throw new StartupRegistrationException(
                "The current executable path could not be determined.");

    private static async Task<ProcessExecutionResult> RunProcessAsync(
        ProcessStartInfo startInfo,
        CancellationToken cancellationToken)
    {
        using Process process = new()
        {
            StartInfo = startInfo
        };

        if (!process.Start())
        {
            throw new StartupRegistrationException(
                $"Could not start '{startInfo.FileName}'.");
        }

        Task<string> standardOutputTask = process.StandardOutput.ReadToEndAsync(cancellationToken);
        Task<string> standardErrorTask = process.StandardError.ReadToEndAsync(cancellationToken);

        await process.WaitForExitAsync(cancellationToken);
        await Task.WhenAll(standardOutputTask, standardErrorTask);

        return new(
            process.ExitCode,
            await standardOutputTask,
            await standardErrorTask);
    }
}

internal sealed record ProcessExecutionResult(
    int ExitCode,
    string StandardOutput,
    string StandardError);
