using DevTools.RuntimeDeploy.Engine.Domain;
using static DevTools.RuntimeDeploy.Engine.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy.Tests;

public sealed class AssemblyDeploymentPlanTests
{
    [Fact]
    public void CreateRuntimeCopyPlan_DeduplicatesByDestinationAndPrefersOwningProject()
    {
        TargetFrameworkSourceItem sourceTarget = new(
            "Release - net11.0",
            [@"\Release\net11.0", @"\Release\netstandard2.0"],
            new DirectoryInfo(@"C:\artifacts\bin\System.Windows.Forms\Release\net11.0"));
        AssemblyDeploymentTargetResolver.TargetPaths targetPaths = CreateTargetPaths();
        DesktopAssemblyInfo forms = CreateAssembly(
            "System.Windows.Forms",
            @"C:\artifacts\bin\System.Windows.Forms\Release\net11.0",
            "System.Windows.Forms.dll",
            "System.Drawing.Common.dll");
        DesktopAssemblyInfo drawing = CreateAssembly(
            "System.Drawing.Common",
            @"C:\artifacts\bin\System.Drawing.Common\Release\net11.0",
            "System.Drawing.Common.dll");

        AssemblyDeploymentPlan.CopyItem[] plan =
            AssemblyDeploymentPlan.CreateRuntimeCopyPlan([forms, drawing], sourceTarget, targetPaths);

        Assert.Equal(2, plan.Length);
        AssemblyDeploymentPlan.CopyItem drawingCopy = Assert.Single(
            plan,
            item => item.DestinationFile.Name == "System.Drawing.Common.dll");
        Assert.Contains(
            @"\System.Drawing.Common\Release\net11.0\",
            drawingCopy.SourceFile.FullName,
            StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CreateRuntimeCopyPlan_RoutesAnalyzerFromNetStandardFallback()
    {
        TargetFrameworkSourceItem sourceTarget = new(
            "Release - net11.0",
            [@"\Release\net11.0", @"\Release\netstandard2.0"],
            new DirectoryInfo(@"C:\artifacts\bin\System.Windows.Forms\Release\net11.0"));
        AssemblyDeploymentTargetResolver.TargetPaths targetPaths = CreateTargetPaths();
        DesktopAssemblyInfo analyzer = CreateAssembly(
            "System.Windows.Forms.Analyzers.CSharp",
            @"C:\artifacts\bin\System.Windows.Forms.Analyzers.CSharp\Release\netstandard2.0",
            "System.Windows.Forms.Analyzers.CSharp.dll");

        AssemblyDeploymentPlan.CopyItem copy = Assert.Single(
            AssemblyDeploymentPlan.CreateRuntimeCopyPlan([analyzer], sourceTarget, targetPaths));

        Assert.Equal(
            Path.Combine(targetPaths.CSharpAnalyzersDir.FullName, "System.Windows.Forms.Analyzers.CSharp.dll"),
            copy.DestinationFile.FullName);
    }

    private static DesktopAssemblyInfo CreateAssembly(
        string name,
        string directory,
        params string[] fileNames)
    {
        return new DesktopAssemblyInfo
        {
            Name = name,
            Path = new DirectoryInfo(Path.GetDirectoryName(Path.GetDirectoryName(directory)!)!),
            AssemblyFiles = [.. fileNames.Select(fileName => new FileInfo(Path.Combine(directory, fileName)))],
            RefAssemblyFiles = []
        };
    }

    private static AssemblyDeploymentTargetResolver.TargetPaths CreateTargetPaths()
    {
        DirectoryInfo refBase = new(@"C:\dotnet\packs\Microsoft.WindowsDesktop.App.Ref\11.0.0");
        DirectoryInfo analyzers = new(Path.Combine(refBase.FullName, "analyzers", "dotnet"));

        return new AssemblyDeploymentTargetResolver.TargetPaths
        {
            TargetSharedAssemblyBasePath = new(@"C:\dotnet\shared\Microsoft.WindowsDesktop.App\11.0.0"),
            TargetRefAssemblyBasePath = refBase,
            TargetRefAssemblyPath = new(Path.Combine(refBase.FullName, "ref", "net11.0")),
            AnalyzersDir = analyzers,
            CSharpAnalyzersDir = new(Path.Combine(analyzers.FullName, "cs")),
            VisualBasicAnalyzersDir = new(Path.Combine(analyzers.FullName, "vb"))
        };
    }
}
