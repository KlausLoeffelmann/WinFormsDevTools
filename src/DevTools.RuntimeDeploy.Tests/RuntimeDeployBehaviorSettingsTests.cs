using DevTools.RuntimeDeploy.Infrastructure;
using WarpToolkit.ComponentModel;
using WarpToolkit.WinForms.AppServices;

namespace DevTools.RuntimeDeploy.Tests;

public sealed class RuntimeDeployBehaviorSettingsTests
{
    [Fact]
    public void NewBehaviorSettings_DefaultToEnabled()
    {
        string settingsFile = CreateSettingsFilePath();

        try
        {
            using WinFormsUserSettingsService settingsStore = CreateSettingsStore(settingsFile);
            RuntimeDeploySettingsService settings = new(settingsStore);

            Assert.True(settings.CloseMainWindowToTray);
            Assert.True(settings.RestoreFromTrayOnSingleClick);
            Assert.True(settings.StartWithWindows);
        }
        finally
        {
            Directory.Delete(Path.GetDirectoryName(settingsFile)!, recursive: true);
        }
    }

    [Fact]
    public void NewBehaviorSettings_PersistDisabledValues()
    {
        string settingsFile = CreateSettingsFilePath();

        try
        {
            using (WinFormsUserSettingsService settingsStore = CreateSettingsStore(settingsFile))
            {
                RuntimeDeploySettingsService settings = new(settingsStore)
                {
                    CloseMainWindowToTray = false,
                    RestoreFromTrayOnSingleClick = false,
                    StartWithWindows = false
                };
            }

            using WinFormsUserSettingsService restoredSettingsStore = CreateSettingsStore(settingsFile);
            RuntimeDeploySettingsService restoredSettings = new(restoredSettingsStore);

            Assert.False(restoredSettings.CloseMainWindowToTray);
            Assert.False(restoredSettings.RestoreFromTrayOnSingleClick);
            Assert.False(restoredSettings.StartWithWindows);
        }
        finally
        {
            Directory.Delete(Path.GetDirectoryName(settingsFile)!, recursive: true);
        }
    }

    private static string CreateSettingsFilePath()
    {
        string tempFolder = Path.Combine(
            Path.GetTempPath(),
            $"RuntimeDeployBehaviorSettingsTests-{Guid.NewGuid():N}");
        Directory.CreateDirectory(tempFolder);
        return Path.Combine(tempFolder, "userSettings.json");
    }

    private static WinFormsUserSettingsService CreateSettingsStore(string settingsFile)
        => new(new WinFormsUserSettingsServiceOptions
        {
            SettingsFileProvider = () => new FileInfo(settingsFile),
            SaveMode = UserSettingsSaveMode.ExplicitFlush
        });
}
