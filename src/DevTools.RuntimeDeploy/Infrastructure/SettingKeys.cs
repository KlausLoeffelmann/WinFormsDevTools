namespace DevTools.RuntimeDeploy.Infrastructure;

/// <summary>
///  Stable keys used with <see cref="WarpToolkit.ComponentModel.IUserSettingsService"/>.
/// </summary>
internal static class SettingKeys
{
    /// <summary>
    ///  Filesystem path to the locally cloned WinForms GitHub repository
    ///  (the source of the assembly artefacts to deploy).
    /// </summary>
    public const string PathToWinFormsGitHubRepo = "PathToWinFormsGitHubRepo";

    public const string SourceArtefactsFolder = "SourceArtefactsFolder";

    public const string ExcludedAssemblyNames = "ExcludedAssemblyNames";

    /// <summary>
    ///  Root folder under which backups (created before overwriting existing
    ///  runtime assemblies) are stored.
    /// </summary>
    public const string BackupRootFolder = "BackupRootFolder";


    public const string MainFormBounds = "MainForm.Bounds";

    /// <summary>
    ///  Bounds of the command-batch ("console") output window.
    /// </summary>
    public const string CommandBatchFormBounds = "CommandBatchForm.Bounds";

    /// <summary>
    ///  Whether window positions/sizes (main window and command-batch console
    ///  window) are persisted across sessions. When disabled, bounds are
    ///  neither saved on exit nor restored on launch for either window.
    /// </summary>
    public const string SaveWindowPositions = "MainForm.SaveWindowPositions";

    /// <summary>
    ///  The font used for the application UI (forms, menu strip, status strip).
    /// </summary>
    public const string UiFont = "Fonts.Ui";

    /// <summary>
    ///  The font used for the command-batch output (console) window.
    /// </summary>
    public const string OutputFont = "Fonts.Output";

    /// <summary>
    ///  Whether a user-initiated close hides the main window in the system tray.
    /// </summary>
    public const string CloseMainWindowToTray = "MainForm.CloseToTray";

    /// <summary>
    ///  Whether one left-click, rather than a double-click, restores the main window.
    /// </summary>
    public const string RestoreFromTrayOnSingleClick = "MainForm.RestoreFromTrayOnSingleClick";

    /// <summary>
    ///  Whether an elevated scheduled task starts the app when the user signs in.
    /// </summary>
    public const string StartWithWindows = "Application.StartWithWindows";
}
