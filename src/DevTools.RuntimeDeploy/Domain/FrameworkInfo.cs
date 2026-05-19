using NuGet.Versioning;

namespace DevTools.RuntimeDeploy.Domain;

/// <summary>
///  Locates the installed .NET Desktop runtime / ref-pack folders under
///  Program Files and enumerates the available SDK versions.
/// </summary>
internal static class FrameworkInfo
{
    private const string PathToNetDesktopLibs = @"\dotnet\shared\Microsoft.WindowsDesktop.App";
    private const string PathToNetDesktopRefs = @"\dotnet\packs\Microsoft.WindowsDesktop.App.Ref";

    private static DirectoryInfo? s_netDesktopLibsDirectory;
    private static DirectoryInfo? s_netDesktopRefsDirectory;

    /// <summary>
    ///  Enumerates installed .NET Desktop SDK versions, ordered by NuGet version,
    ///  keyed by the folder name (which is the version string).
    /// </summary>
    /// <param name="getRefPath">
    ///  When <see langword="true"/>, scans the ref-pack folder
    ///  (<c>Microsoft.WindowsDesktop.App.Ref</c>) instead of the shared runtime folder.
    /// </param>
    /// <returns>
    ///  A dictionary of version-name to <see cref="DirectoryInfo"/>, or
    ///  <see langword="null"/> if the base folder does not exist.
    /// </returns>
    public static Dictionary<string, DirectoryInfo>? GetDotNetDesktopSdk(bool getRefPath = false)
    {
        string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

        DirectoryInfo netDesktopVersionsDirectory = new(
            programFiles + (getRefPath ? PathToNetDesktopRefs : PathToNetDesktopLibs));

        if (!netDesktopVersionsDirectory.Exists)
        {
            return null;
        }

        return netDesktopVersionsDirectory
            .GetDirectories()
            .Select(dir => new
            {
                Directory = dir,
                dir.Name,
                ParsedVersion = NuGetVersion.TryParse(dir.Name, out NuGetVersion? ver)
                    ? ver
                    : new NuGetVersion("0.0.0")
            })
            .OrderBy(x => x.ParsedVersion)
            .ToDictionary(x => x.Name, x => x.Directory);
    }

    /// <summary>
    ///  Lazy-initialised <see cref="DirectoryInfo"/> for the shared
    ///  <c>Microsoft.WindowsDesktop.App</c> folder.
    /// </summary>
    public static DirectoryInfo NetDesktopLibsDirectory
        => s_netDesktopLibsDirectory ??= new(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + PathToNetDesktopLibs);

    /// <summary>
    ///  Lazy-initialised <see cref="DirectoryInfo"/> for the
    ///  <c>Microsoft.WindowsDesktop.App.Ref</c> ref-pack folder.
    /// </summary>
    public static DirectoryInfo NetDesktopRefsDirectory
        => s_netDesktopRefsDirectory ??= new(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + PathToNetDesktopRefs);
}
