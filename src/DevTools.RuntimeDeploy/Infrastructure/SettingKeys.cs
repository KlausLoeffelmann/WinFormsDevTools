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

    public const string MainFormBounds = "MainForm.Bounds";

    /// <summary>
    ///  Whether the main window position/size is persisted across sessions.
    ///  When disabled, bounds are neither saved on exit nor restored on launch.
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
}
