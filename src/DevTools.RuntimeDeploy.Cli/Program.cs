using DevTools.RuntimeDeploy.Cli;

CliOptions options;

try
{
    options = CliArgumentParser.Parse(args);
}
catch (ArgumentException ex)
{
    Console.Error.WriteLine($"ERROR: {ex.Message}");
    PrintUsage();
    return 1;
}

if (options.ShowHelp)
{
    PrintUsage();
    return 0;
}

return options.RestoreMode
    ? await RestoreCommand.RunAsync(options)
    : await PatchCommand.RunAsync(options);

static void PrintUsage()
{
    Console.WriteLine("""
        RuntimePatcher - applies or restores WinForms .NET Desktop runtime patches.

        Patch mode (default):
          RuntimePatcher --package <path-to.netdeploy> [--target <dir>] [--dry-run] [--yes]
                          [--backup-root <dir>]

          With no arguments at all, RuntimePatcher looks for a
          "runtimepatcher.settings.json" file next to itself and uses it instead.

        Restore mode:
          RuntimePatcher --restore [<backup-file-or-folder>] [--target <dir>] [--yes]
                          [--backup-root <dir>] [--max-depth <n>]
                          [--tfm-filter <tfm>] [--config-filter <configuration>]

          With no path, the default backup root is searched. When more than one
          backup matches, an interactive list picker is shown.

        Options:
          --package <path>        .netdeploy package to apply.
          --target <dir>          Overrides the auto-detected target directory.
          --dry-run               Reports what would happen without changing files.
          --yes                   Skips the confirmation prompt.
          --restore [<path>]      Switches to restore mode.
          --backup-root <dir>     Overrides the default backup root folder.
          --max-depth <n>         Recursion depth when searching for backups (default 3).
          --tfm-filter <tfm>      Only considers backups matching this TFM.
          --config-filter <cfg>   Only considers backups matching this configuration.
          --help                  Shows this help text.
        """);
}
