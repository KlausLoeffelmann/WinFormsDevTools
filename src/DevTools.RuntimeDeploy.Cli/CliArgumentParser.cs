using System.Text.Json;
using DevTools.RuntimeDeploy.Engine.Json;
using DevTools.RuntimeDeploy.Engine.Packaging;

namespace DevTools.RuntimeDeploy.Cli;

/// <summary>
///  Parses CLI arguments, or - when none are supplied - looks for
///  <c>runtimepatcher.settings.json</c> next to the running executable and
///  uses it instead. This is what makes a generated installer folder
///  double-click runnable without any command line at all.
/// </summary>
public static class CliArgumentParser
{
    public static CliOptions Parse(string[] args)
    {
        if (args.Length == 0)
        {
            CliOptions? fromSettingsFile = TryLoadFromSettingsFile();
            if (fromSettingsFile is not null)
            {
                return fromSettingsFile;
            }
        }

        CliOptions options = new();

        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];

            switch (arg)
            {
                case "--package":
                    options.PackagePath = RequireValue(args, ref i, arg);
                    break;

                case "--target":
                    options.TargetDirectory = RequireValue(args, ref i, arg);
                    break;

                case "--dry-run":
                    options.DryRun = true;
                    break;

                case "--yes":
                    options.Yes = true;
                    break;

                case "--restore":
                    options.RestoreMode = true;
                    options.RestorePath = TryTakeOptionalValue(args, ref i);
                    break;

                case "--backup-root":
                    options.BackupRoot = RequireValue(args, ref i, arg);
                    break;

                case "--max-depth":
                    options.MaxDepth = int.Parse(RequireValue(args, ref i, arg));
                    break;

                case "--tfm-filter":
                    options.TfmFilter = RequireValue(args, ref i, arg);
                    break;

                case "--config-filter":
                    options.ConfigFilter = RequireValue(args, ref i, arg);
                    break;

                case "--help":
                case "-h":
                case "-?":
                    options.ShowHelp = true;
                    break;

                default:
                    throw new ArgumentException($"Unrecognized argument: '{arg}'.");
            }
        }

        return options;
    }

    private static string RequireValue(string[] args, ref int i, string optionName)
    {
        if (i + 1 >= args.Length)
        {
            throw new ArgumentException($"Option '{optionName}' requires a value.");
        }

        return args[++i];
    }

    // --restore's value is optional: if the next token looks like another
    // option (starts with "--") or there is no next token, treat --restore as
    // "search the default backup root" rather than consuming that token.
    private static string? TryTakeOptionalValue(string[] args, ref int i)
    {
        if (i + 1 < args.Length && !args[i + 1].StartsWith("--", StringComparison.Ordinal))
        {
            return args[++i];
        }

        return null;
    }

    private static CliOptions? TryLoadFromSettingsFile()
    {
        string exeDirectory = AppContext.BaseDirectory;
        string settingsPath = Path.Combine(exeDirectory, RuntimePatcherSettings.DefaultFileName);

        if (!File.Exists(settingsPath))
        {
            return null;
        }

        using FileStream stream = File.OpenRead(settingsPath);
        RuntimePatcherSettings? settings = JsonSerializer.Deserialize(stream, EngineJsonContext.Default.RuntimePatcherSettings);

        if (settings is null)
        {
            return null;
        }

        return new CliOptions
        {
            PackagePath = Path.Combine(exeDirectory, settings.PackageFileName),
            DryRun = settings.DryRun,
            Yes = settings.Yes,
        };
    }
}
