using WarpToolkit.ComponentModel;

namespace DevTools.RuntimeDeploy.Infrastructure;

public sealed class RuntimeDeploySettingsService(IUserSettingsService userSettings)
{
    public string SourceArtefactsFolder
    {
        get
        {
            string path = userSettings.Get(SettingKeys.SourceArtefactsFolder, string.Empty);
            return string.IsNullOrWhiteSpace(path)
                ? userSettings.Get(SettingKeys.PathToWinFormsGitHubRepo, string.Empty)
                : path;
        }
        set
        {
            userSettings.Set(SettingKeys.SourceArtefactsFolder, value);
            userSettings.Flush();
        }
    }

    public HashSet<string> GetExcludedAssemblyNames()
    {
        string[] excludedNames = userSettings.Get(SettingKeys.ExcludedAssemblyNames, Array.Empty<string>());
        return new HashSet<string>(excludedNames, StringComparer.OrdinalIgnoreCase);
    }

    public void SaveExcludedAssemblyNames(IEnumerable<string> assemblyNames)
    {
        string[] normalizedNames =
        [
            .. assemblyNames
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => Path.GetFileNameWithoutExtension(name.Trim()))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Order(StringComparer.OrdinalIgnoreCase)
        ];

        userSettings.Set(SettingKeys.ExcludedAssemblyNames, normalizedNames);
        userSettings.Flush();
    }
}
