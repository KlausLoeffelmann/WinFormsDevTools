using DevTools.RuntimeDeploy.Infrastructure;
using System.Diagnostics;
using System.Security.Principal;

namespace DevTools.RuntimeDeploy.Tests;

public sealed class WindowsStartupRegistrationServiceTests
{
    [Fact]
    public void RegistrationCommand_UsesElevatedInteractiveLogonTask()
    {
        const string executablePath = @"C:\Program Files\Runtime Deploy\DevTools.RuntimeDeploy.exe";

        ProcessStartInfo startInfo =
            WindowsStartupRegistrationService.CreateRegistrationStartInfo(executablePath);

        Assert.Equal("schtasks.exe", startInfo.FileName);
        Assert.Equal(
            [
                "/Create",
                "/SC",
                "ONLOGON",
                "/TN",
                WindowsStartupRegistrationService.TaskName,
                "/TR",
                $"\"{executablePath}\" {RuntimeDeployLaunchOptions.StartInTrayArgument}",
                "/RL",
                "HIGHEST",
                "/IT",
                "/F",
                "/HResult"
            ],
            startInfo.ArgumentList);
        Assert.Contains(
            WindowsIdentity.GetCurrent().User!.Value,
            WindowsStartupRegistrationService.TaskName);
    }

    [Fact]
    public async Task EnablingStartup_CreatesTaskForCurrentExecutable()
    {
        List<ProcessStartInfo> invocations = [];
        WindowsStartupRegistrationService service = new(
            (startInfo, cancellationToken) =>
            {
                invocations.Add(startInfo);
                return Task.FromResult(new ProcessExecutionResult(0, string.Empty, string.Empty));
            },
            () => @"C:\RuntimeDeploy\DevTools.RuntimeDeploy.exe");

        await service.SetEnabledAsync(true);

        ProcessStartInfo invocation = Assert.Single(invocations);
        Assert.Contains("/Create", invocation.ArgumentList);
        Assert.Contains(RuntimeDeployLaunchOptions.StartInTrayArgument, invocation.ArgumentList[6]);
    }

    [Fact]
    public async Task DisablingMissingStartupTask_IsIdempotent()
    {
        List<ProcessStartInfo> invocations = [];
        WindowsStartupRegistrationService service = new(
            (startInfo, cancellationToken) =>
            {
                invocations.Add(startInfo);
                return Task.FromResult(new ProcessExecutionResult(
                    WindowsStartupRegistrationService.TaskNotFoundExitCode,
                    string.Empty,
                    "not found"));
            },
            () => @"C:\RuntimeDeploy\DevTools.RuntimeDeploy.exe");

        await service.SetEnabledAsync(false);

        ProcessStartInfo invocation = Assert.Single(invocations);
        Assert.Contains("/Query", invocation.ArgumentList);
    }

    [Fact]
    public async Task DisablingStartup_QueryFailureIsSurfaced()
    {
        WindowsStartupRegistrationService service = new(
            (startInfo, cancellationToken) =>
                Task.FromResult(new ProcessExecutionResult(
                    unchecked((int)0x80070005),
                    string.Empty,
                    "Access is denied.")),
            () => @"C:\RuntimeDeploy\DevTools.RuntimeDeploy.exe");

        StartupRegistrationException exception = await Assert.ThrowsAsync<StartupRegistrationException>(
            () => service.SetEnabledAsync(false));

        Assert.Contains("Access is denied.", exception.Message);
    }

    [Fact]
    public async Task DisablingExistingStartupTask_QueriesThenDeletesIt()
    {
        Queue<ProcessExecutionResult> results = new(
        [
            new(0, string.Empty, string.Empty),
            new(0, string.Empty, string.Empty)
        ]);
        List<ProcessStartInfo> invocations = [];
        WindowsStartupRegistrationService service = new(
            (startInfo, cancellationToken) =>
            {
                invocations.Add(startInfo);
                return Task.FromResult(results.Dequeue());
            },
            () => @"C:\RuntimeDeploy\DevTools.RuntimeDeploy.exe");

        await service.SetEnabledAsync(false);

        Assert.Equal(2, invocations.Count);
        Assert.Contains("/Query", invocations[0].ArgumentList);
        Assert.Contains("/Delete", invocations[1].ArgumentList);
    }

    [Fact]
    public async Task DisablingStartup_TaskRemovedAfterQueryIsStillSuccessful()
    {
        Queue<ProcessExecutionResult> results = new(
        [
            new(0, string.Empty, string.Empty),
            new(
                WindowsStartupRegistrationService.TaskNotFoundExitCode,
                string.Empty,
                "not found")
        ]);
        WindowsStartupRegistrationService service = new(
            (startInfo, cancellationToken) => Task.FromResult(results.Dequeue()),
            () => @"C:\RuntimeDeploy\DevTools.RuntimeDeploy.exe");

        await service.SetEnabledAsync(false);

        Assert.Empty(results);
    }

    [Fact]
    public async Task RegistrationFailure_IsSurfaced()
    {
        WindowsStartupRegistrationService service = new(
            (startInfo, cancellationToken) =>
                Task.FromResult(new ProcessExecutionResult(5, string.Empty, "Access is denied.")),
            () => @"C:\RuntimeDeploy\DevTools.RuntimeDeploy.exe");

        StartupRegistrationException exception = await Assert.ThrowsAsync<StartupRegistrationException>(
            () => service.SetEnabledAsync(true));

        Assert.Contains("Access is denied.", exception.Message);
    }
}
