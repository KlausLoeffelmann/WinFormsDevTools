namespace DevTools.RuntimeDeploy.Cli;

/// <summary>
///  Parsed command-line (or JSON control-file) options driving the CLI.
/// </summary>
public sealed class CliOptions
{
    /// <summary>Path to a <c>.netdeploy</c> package to apply ("patch" mode).</summary>
    public string? PackagePath { get; set; }

    /// <summary>Overrides the deploy target directory instead of the machine-detected one.</summary>
    public string? TargetDirectory { get; set; }

    public bool DryRun { get; set; }

    /// <summary>Skips the interactive confirmation prompt.</summary>
    public bool Yes { get; set; }

    /// <summary>
    ///  When set, switches the tool into "restore" mode. The value may be a
    ///  specific <c>.netbackup</c> file, a folder to search, or
    ///  <see langword="null"/> (search the well-known default backup root).
    /// </summary>
    public bool RestoreMode { get; set; }

    public string? RestorePath { get; set; }

    public string? BackupRoot { get; set; }

    public int MaxDepth { get; set; } = Engine.PatchBackup.BackupFinder.DefaultMaxDepth;

    public string? TfmFilter { get; set; }

    public string? ConfigFilter { get; set; }

    public bool ShowHelp { get; set; }
}
