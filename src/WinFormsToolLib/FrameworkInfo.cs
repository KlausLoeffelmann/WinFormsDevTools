using System.Diagnostics;

namespace WinFormsDevToolsLib
{
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

            Dictionary<string, DirectoryInfo>? result = null;

            if (netDesktopVersionsDirectory.Exists)
            {
                result = netDesktopVersionsDirectory
                    .GetDirectories()
                    .ToDictionary(item => item.Name);
            }

            return result;
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
}
