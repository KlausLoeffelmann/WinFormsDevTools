namespace DevTools.RuntimeDeploy.Infrastructure;

public sealed record RuntimeDeployLaunchOptions(bool StartInTray)
{
    public const string StartInTrayArgument = "--start-in-tray";

    public static RuntimeDeployLaunchOptions FromArguments(IEnumerable<string> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);

        return new(arguments.Contains(StartInTrayArgument, StringComparer.OrdinalIgnoreCase));
    }
}
