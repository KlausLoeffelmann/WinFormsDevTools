using DevTools.RuntimeDeploy.Infrastructure;
using DevTools.RuntimeDeploy.Views;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json.Nodes;
using WarpToolkit.ComponentModel;
using WarpToolkit.WinForms.AppServices;
using WarpToolkit.WinForms.Controls;

namespace DevTools.RuntimeDeploy.Tests;

public sealed class ArtifactsFolderPersistenceTests
{
    private const string LegacyArtifactsFolderKey = "PathToWinFormsGitHubRepo";

    [Fact]
    public Task PickerSelection_IsPersistedAndRestoredOnNextControlLoad()
        => RunInStaAsync(() =>
        {
            string tempFolder = Path.Combine(
                Path.GetTempPath(),
                $"RuntimeDeploySettingsTests-{Guid.NewGuid():N}");
            string settingsFile = Path.Combine(tempFolder, "userSettings.json");
            string artifactsFolder = Path.Combine(tempFolder, "artifacts");

            Directory.CreateDirectory(artifactsFolder);

            try
            {
                using (WinFormsUserSettingsService firstSettingsStore = CreateSettingsStore(settingsFile))
                {
                    RuntimeDeploySettingsService firstSettings = new(firstSettingsStore);
                    using TestableAssetSelectionControl firstControl = new(firstSettings);

                    FindPicker(firstControl).FileOrFolderPath = artifactsFolder;

                    Assert.Equal(artifactsFolder, firstSettings.SourceArtefactsFolder);
                }

                JsonNode settingsJson = JsonNode.Parse(File.ReadAllText(settingsFile))!;
                Assert.Equal(
                    artifactsFolder,
                    settingsJson["settings"]?["SourceArtefactsFolder"]?.GetValue<string>());

                using WinFormsUserSettingsService restoredSettingsStore = CreateSettingsStore(settingsFile);
                RuntimeDeploySettingsService restoredSettings = new(restoredSettingsStore);
                using TestableAssetSelectionControl restoredControl = new(restoredSettings);

                restoredControl.LoadFromSettings();

                Assert.Equal(
                    artifactsFolder,
                    FindPicker(restoredControl).FileOrFolderPath);
            }
            finally
            {
                Directory.Delete(tempFolder, recursive: true);
            }
        });

    [Fact]
    public Task ExplicitlyClearedFolder_DoesNotRestoreLegacyFallback()
        => RunInStaAsync(() =>
        {
            string tempFolder = Path.Combine(
                Path.GetTempPath(),
                $"RuntimeDeploySettingsTests-{Guid.NewGuid():N}");
            string settingsFile = Path.Combine(tempFolder, "userSettings.json");
            string legacyArtifactsFolder = Path.Combine(tempFolder, "legacy-artifacts");

            Directory.CreateDirectory(legacyArtifactsFolder);

            try
            {
                using (WinFormsUserSettingsService firstSettingsStore = CreateSettingsStore(settingsFile))
                {
                    firstSettingsStore.Set(LegacyArtifactsFolderKey, legacyArtifactsFolder);
                    firstSettingsStore.Flush();

                    RuntimeDeploySettingsService firstSettings = new(firstSettingsStore);
                    using TestableAssetSelectionControl firstControl = new(firstSettings);

                    firstControl.LoadFromSettings();
                    FilePathPicker firstPicker = FindPicker(firstControl);
                    Assert.Equal(legacyArtifactsFolder, firstPicker.FileOrFolderPath);

                    firstPicker.FileOrFolderPath = string.Empty;
                    Assert.Equal(string.Empty, firstSettings.SourceArtefactsFolder);
                }

                using WinFormsUserSettingsService restoredSettingsStore = CreateSettingsStore(settingsFile);
                RuntimeDeploySettingsService restoredSettings = new(restoredSettingsStore);
                using TestableAssetSelectionControl restoredControl = new(restoredSettings);

                restoredControl.LoadFromSettings();

                Assert.Equal(string.Empty, FindPicker(restoredControl).FileOrFolderPath);
            }
            finally
            {
                Directory.Delete(tempFolder, recursive: true);
            }
        });

    private static WinFormsUserSettingsService CreateSettingsStore(string settingsFile)
        => new(new WinFormsUserSettingsServiceOptions
        {
            SettingsFileProvider = () => new FileInfo(settingsFile),
            SaveMode = UserSettingsSaveMode.ExplicitFlush
        });

    private static FilePathPicker FindPicker(Control parent)
        => FindDescendant<FilePathPicker>(parent)
            ?? throw new InvalidOperationException("The artifacts folder picker was not found.");

    private static TControl? FindDescendant<TControl>(Control parent)
        where TControl : Control
    {
        foreach (Control child in parent.Controls)
        {
            if (child is TControl matchingChild)
            {
                return matchingChild;
            }

            TControl? descendant = FindDescendant<TControl>(child);
            if (descendant is not null)
            {
                return descendant;
            }
        }

        return null;
    }

    private static Task RunInStaAsync(Action action)
    {
        TaskCompletionSource completion = new(TaskCreationOptions.RunContinuationsAsynchronously);
        Thread thread = new(() =>
        {
            try
            {
                action();
                completion.SetResult();
            }
            catch (Exception ex)
            {
                completion.SetException(ex);
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();

        return completion.Task;
    }

    private sealed class TestableAssetSelectionControl : AssetSelectionControl
    {
        public TestableAssetSelectionControl(RuntimeDeploySettingsService settings)
            : base(
                settings,
                new RuntimeDeployStatusService(),
                NullLogger<AssetSelectionControl>.Instance)
        {
        }

        public void LoadFromSettings()
            => OnLoad(EventArgs.Empty);
    }
}
