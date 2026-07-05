using System.ComponentModel;
using System.Globalization;
using WarpToolkit.ComponentModel;

namespace DevTools.RuntimeDeploy.Infrastructure;

public sealed class RuntimeDeploySettingsService(IUserSettingsService userSettings)
{
    private static readonly FontConverter s_fontConverter = new();

    /// <summary>
    ///  Default font for the application UI.
    /// </summary>
    public static Font DefaultUiFont { get; } = new("Segoe UI", 11F);

    /// <summary>
    ///  Default (monospaced) font for the command-batch output window.
    /// </summary>
    public static Font DefaultOutputFont { get; } = new("Consolas", 11F);

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

    /// <summary>
    ///  Font used for the application UI. Setting the value persists it immediately.
    /// </summary>
    public Font UiFont
    {
        get => GetFont(SettingKeys.UiFont, DefaultUiFont);
        set => SetFont(SettingKeys.UiFont, value);
    }

    /// <summary>
    ///  Font used for the command-batch output window. Setting the value persists
    ///  it immediately.
    /// </summary>
    public Font OutputFont
    {
        get => GetFont(SettingKeys.OutputFont, DefaultOutputFont);
        set => SetFont(SettingKeys.OutputFont, value);
    }

    private Font GetFont(string key, Font fallback)
    {
        string serialized = userSettings.Get(key, string.Empty);
        if (string.IsNullOrWhiteSpace(serialized))
        {
            return fallback;
        }

        try
        {
            return s_fontConverter.ConvertFromString(null, CultureInfo.InvariantCulture, serialized) as Font
                ?? fallback;
        }
        catch
        {
            return fallback;
        }
    }

    private void SetFont(string key, Font value)
    {
        ArgumentNullException.ThrowIfNull(value);

        string serialized = s_fontConverter.ConvertToString(null, CultureInfo.InvariantCulture, value)
            ?? string.Empty;

        userSettings.Set(key, serialized);
        userSettings.Flush();
    }
}
