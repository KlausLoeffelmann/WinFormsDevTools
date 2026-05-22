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
}
