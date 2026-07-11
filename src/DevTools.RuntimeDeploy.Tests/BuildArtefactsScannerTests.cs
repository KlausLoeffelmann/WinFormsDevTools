using DevTools.RuntimeDeploy.Engine.Domain;
using static DevTools.RuntimeDeploy.Engine.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy.Tests;

public sealed class BuildArtefactsScannerTests : IDisposable
{
    private readonly string _tempFolder = Path.Combine(
        Path.GetTempPath(),
        $"BuildArtefactsScannerTests-{Guid.NewGuid():N}");

    [Fact]
    public void GetAvailableTargets_QualifiesNetStandardPathWithConfiguration()
    {
        string configurationFolder = Path.Combine(
            _tempFolder,
            "bin",
            "System.Windows.Forms",
            "Debug");
        Directory.CreateDirectory(Path.Combine(configurationFolder, "net10.0"));
        Directory.CreateDirectory(Path.Combine(configurationFolder, "netstandard2.0"));

        BuildArtefactsScanner scanner = new(_tempFolder);

        TargetFrameworkSourceItem target = Assert.Single(
            scanner.GetAvailableTargets(),
            item => item.Name == "Debug - net10.0");

        Assert.Equal(
            [@"\Debug\net10.0", @"\Debug\netstandard2.0"],
            target.TfmPaths);
    }

    [Fact]
    public void GetWinFormsRuntimeAssemblies_HonorsNetStandardSelection()
    {
        string primaryFolder = Path.Combine(
            _tempFolder,
            "bin",
            "Sample.Assembly",
            "Debug",
            "net10.0");
        string netStandardFolder = Path.Combine(
            _tempFolder,
            "bin",
            "Sample.Assembly",
            "Debug",
            "netstandard2.0");
        string releaseNetStandardFolder = Path.Combine(
            _tempFolder,
            "bin",
            "Sample.Assembly",
            "Release",
            "netstandard2.0");

        Directory.CreateDirectory(primaryFolder);
        Directory.CreateDirectory(netStandardFolder);
        Directory.CreateDirectory(releaseNetStandardFolder);
        File.WriteAllBytes(Path.Combine(primaryFolder, "Primary.dll"), [1]);
        File.WriteAllBytes(Path.Combine(netStandardFolder, "NetStandard.dll"), [2]);
        File.WriteAllBytes(Path.Combine(releaseNetStandardFolder, "ReleaseNetStandard.dll"), [3]);

        TargetFrameworkSourceItem target = new(
            "Debug - net10.0",
            [@"\Debug\net10.0", @"\Debug\netstandard2.0"],
            new DirectoryInfo(primaryFolder));
        BuildArtefactsScanner scanner = new(_tempFolder);

        DesktopAssemblyInfo withoutNetStandard = Assert.Single(
            scanner.GetWinFormsRuntimeAssemblies(
                target,
                includeRefAssemblies: false,
                includeNetStandardAssemblies: false));
        DesktopAssemblyInfo withNetStandard = Assert.Single(
            scanner.GetWinFormsRuntimeAssemblies(
                target,
                includeRefAssemblies: false,
                includeNetStandardAssemblies: true));

        Assert.Equal(
            ["Primary.dll"],
            withoutNetStandard.AssemblyFiles.Select(file => file.Name));
        Assert.Equal(
            ["NetStandard.dll", "Primary.dll"],
            withNetStandard.AssemblyFiles.Select(file => file.Name).Order());
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempFolder))
        {
            Directory.Delete(_tempFolder, recursive: true);
        }
    }
}
