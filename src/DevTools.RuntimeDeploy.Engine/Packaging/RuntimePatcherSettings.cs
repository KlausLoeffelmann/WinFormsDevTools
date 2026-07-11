namespace DevTools.RuntimeDeploy.Engine.Packaging;

/// <summary>
///  Settings written by the "Create Runtime patcher..." UI flow into
///  <c>runtimepatcher.settings.json</c>, placed next to the bundled CLI exe
///  and the <c>.netdeploy</c> package. When the CLI is launched with no
///  command-line arguments it looks for this file in its own directory and,
///  if found, uses it exactly as if the same values had been passed as CLI
///  arguments - this is what makes the generated installer folder
///  double-click runnable.
/// </summary>
public sealed record RuntimePatcherSettings(
    string PackageFileName,
    bool DryRun,
    bool Yes)
{
    public const string DefaultFileName = "runtimepatcher.settings.json";
}
