using NuGet.Versioning;

namespace DevTools.Libs;

public class FrameworkInfo
{
    private const string PathToNetDesktopLibs = "\\dotnet\\shared\\Microsoft.WindowsDesktop.App";
    private const string PathToNetDesktopRefs = "\\dotnet\\packs\\Microsoft.WindowsDesktop.App.Ref";

    private static DirectoryInfo? s_netDesktopLibsDirectory;
    private static DirectoryInfo? s_netDesktopRefsDirectory;

    public static Dictionary<string, DirectoryInfo>? GetDotNetDesktopSdk(bool getRefPath = false)
    {
        string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

        DirectoryInfo netDesktopVersionsDirectory =
          new(programFiles +
            (getRefPath
              ? PathToNetDesktopRefs
              : PathToNetDesktopLibs));

        if (!netDesktopVersionsDirectory.Exists)
            return null;

        var sorted = netDesktopVersionsDirectory
          .GetDirectories()
          .Select(dir => new
          {
              Directory = dir,
              dir.Name,
              ParsedVersion = NuGetVersion.TryParse(dir.Name, out var ver) ? ver : new NuGetVersion("0.0.0")
          })
          .OrderBy(x => x.ParsedVersion)
          .ToDictionary(x => x.Name, x => x.Directory);

        return sorted;
    }

    public static DirectoryInfo NetDesktopLibsDirectory
        => s_netDesktopLibsDirectory ??=
            new($"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}" +
                $"{PathToNetDesktopLibs}");

    public static DirectoryInfo NetDesktopRefsDirectory
        => s_netDesktopRefsDirectory ??=
            new($"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}" +
                $"{PathToNetDesktopRefs}");
}
