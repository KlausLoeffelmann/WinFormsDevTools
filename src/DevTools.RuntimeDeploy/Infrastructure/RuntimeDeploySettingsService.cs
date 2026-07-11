using System.ComponentModel;
using System.Globalization;
using WarpToolkit.ComponentModel;

namespace DevTools.RuntimeDeploy.Infrastructure;

public sealed class RuntimeDeploySettingsService(IUserSettingsService userSettings)
{
    private static readonly FontConverter s_fontConverter = new();

    /// <summary>
    ///  Creates a new default font instance for the application UI.
    /// </summary>
    /// <remarks>
    ///  <para>
    ///   Exposed as a factory rather than a cached singleton so each caller gets its own
    ///   <see cref="Font"/> instance to own and dispose. A single shared instance could be
    ///   disposed by one owner (e.g. a closed dialog) while still referenced - and in use -
    ///   by another control, leading to GDI+ failures ("Parameter is not valid") when that
    ///   other control later tries to measure or render text with it.
    ///  </para>
    /// </remarks>
    public static Func<Font> CreateDefaultUiFont { get; } = static () => new Font("Segoe UI", 11F);

    /// <summary>
    ///  Creates a new default (monospaced) font instance for the command-batch output window.
    /// </summary>
    /// <remarks>
    ///  <para>
    ///   See <see cref="CreateDefaultUiFont"/> for why this is a factory instead of a cached
    ///   singleton instance.
    ///  </para>
    /// </remarks>
    public static Func<Font> CreateDefaultOutputFont { get; } = static () => new Font("Consolas", 11F);

    public string SourceArtefactsFolder
    {
        get
        {
            return userSettings.Contains(SettingKeys.SourceArtefactsFolder)
                ? userSettings.Get(SettingKeys.SourceArtefactsFolder, string.Empty)
                : userSettings.Get(SettingKeys.PathToWinFormsGitHubRepo, string.Empty);
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
        // Names here are bare assembly directory names (e.g. "System.Windows.Forms.Design"),
        // not file paths, so Path.GetFileNameWithoutExtension must not be used: it would
        // misinterpret the trailing ".Design" segment as a file extension and strip it.
        string[] normalizedNames =
        [
            .. assemblyNames
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => name.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Order(StringComparer.OrdinalIgnoreCase)
        ];

        userSettings.Set(SettingKeys.ExcludedAssemblyNames, normalizedNames);
        userSettings.Flush();
    }

    /// <summary>
    ///  Root folder under which backups (created before overwriting existing
    ///  runtime assemblies) are stored. Defaults to
    ///  <see cref="Engine.PatchBackup.BackupService.DefaultBackupRoot"/>.
    /// </summary>
    public string BackupRootFolder
    {
        get
        {
            string path = userSettings.Get(SettingKeys.BackupRootFolder, string.Empty);
            return string.IsNullOrWhiteSpace(path)
                ? Engine.PatchBackup.BackupService.DefaultBackupRoot.FullName
                : path;
        }
        set
        {
            userSettings.Set(SettingKeys.BackupRootFolder, value);
            userSettings.Flush();
        }
    }

    /// <summary>
    ///  Font used for the application UI. Setting the value persists it immediately.
    /// </summary>
    public Font UiFont
    {
        get => GetFont(SettingKeys.UiFont, CreateDefaultUiFont);
        set => SetFont(SettingKeys.UiFont, value);
    }

    /// <summary>
    ///  Font used for the command-batch output window. Setting the value persists
    ///  it immediately.
    /// </summary>
    public Font OutputFont
    {
        get => GetFont(SettingKeys.OutputFont, CreateDefaultOutputFont);
        set => SetFont(SettingKeys.OutputFont, value);
    }

    /// <summary>
    ///  Whether window positions/sizes are persisted across sessions. Shared by
    ///  the main window and the command-batch console window.
    /// </summary>
    public bool SaveWindowPositions
    {
        get => userSettings.Get(SettingKeys.SaveWindowPositions, true);
        set
        {
            userSettings.Set(SettingKeys.SaveWindowPositions, value);
            userSettings.Flush();
        }
    }

    /// <summary>
    ///  The last saved bounds of the command-batch ("console") output window,
    ///  or <see langword="null"/> if none have been saved yet.
    /// </summary>
    public Rectangle? CommandBatchFormBounds
    {
        get => userSettings.Contains(SettingKeys.CommandBatchFormBounds)
            ? userSettings.Get(SettingKeys.CommandBatchFormBounds, Rectangle.Empty)
            : null;
        set
        {
            if (value is not Rectangle bounds)
            {
                return;
            }

            userSettings.Set(SettingKeys.CommandBatchFormBounds, bounds);
            userSettings.Flush();
        }
    }

    private Font GetFont(string key, Func<Font> createFallback)
    {
        string serialized = userSettings.Get(key, string.Empty);
        if (string.IsNullOrWhiteSpace(serialized))
        {
            return createFallback();
        }

        try
        {
            return s_fontConverter.ConvertFromString(
                context: null,
                culture: CultureInfo.InvariantCulture,
                text: serialized) as Font
                ?? createFallback();
        }
        catch
        {
            return createFallback();
        }
    }

    private void SetFont(string key, Font value)
    {
        ArgumentNullException.ThrowIfNull(value);

        string serialized = s_fontConverter.ConvertToString(
            context: null,
            culture: CultureInfo.InvariantCulture,
            value: value) ?? string.Empty;

        userSettings.Set(key, serialized);
        userSettings.Flush();
    }
}
