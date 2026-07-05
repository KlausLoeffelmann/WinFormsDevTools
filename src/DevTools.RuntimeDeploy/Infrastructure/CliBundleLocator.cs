namespace DevTools.RuntimeDeploy.Infrastructure;

/// <summary>
///  Locates the pre-published, self-contained win-x64
///  <c>DevTools.RuntimeDeploy.Cli</c> ("RuntimePatcher.exe") binary so it can
///  be bundled into a generated installer folder.
/// </summary>
/// <remarks>
///  v1 requires the CLI project to have been published locally
///  (<c>dotnet publish -c Release</c>) before "Create package installer..."
///  can bundle it; only win-x64 is supported. Multi-RID publishing/bundling
///  is a documented future enhancement.
/// </remarks>
internal static class CliBundleLocator
{
    private const string CliProjectFolderName = "DevTools.RuntimeDeploy.Cli";
    private const string CliExeName = "RuntimePatcher.exe";
    private static readonly string[] s_publishRelativePaths =
    [
        @"bin\Release\net10.0-windows\win-x64\publish",
        @"bin\Debug\net10.0-windows\win-x64\publish",
    ];

    /// <summary>
    ///  Searches upward from the running app's base directory for a sibling
    ///  <c>DevTools.RuntimeDeploy.Cli</c> project folder and returns its
    ///  published <c>RuntimePatcher.exe</c>, if one has been published.
    /// </summary>
    public static FileInfo? TryFindPublishedCli()
    {
        DirectoryInfo? current = new(AppContext.BaseDirectory);

        // Walk up looking for a "src" (or similar) folder that has a
        // DevTools.RuntimeDeploy.Cli sibling next to it.
        while (current is not null)
        {
            DirectoryInfo candidateCliProject = new(Path.Combine(current.FullName, CliProjectFolderName));
            if (candidateCliProject.Exists)
            {
                foreach (string relativePublishPath in s_publishRelativePaths)
                {
                    FileInfo exe = new(Path.Combine(candidateCliProject.FullName, relativePublishPath, CliExeName));
                    if (exe.Exists)
                    {
                        return exe;
                    }
                }
            }

            current = current.Parent;
        }

        return null;
    }
}
