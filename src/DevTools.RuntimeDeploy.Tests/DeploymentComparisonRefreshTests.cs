using DevTools.RuntimeDeploy.Engine.Domain;
using DevTools.RuntimeDeploy.Views;
using static DevTools.RuntimeDeploy.Engine.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy.Tests;

public sealed class DeploymentComparisonRefreshTests
{
    [Fact]
    public Task FocusedRefresh_UsesCurrentDestinationState()
        => RunInStaAsync(() =>
        {
            string tempFolder = Path.Combine(
                Path.GetTempPath(),
                $"DeploymentComparisonRefreshTests-{Guid.NewGuid():N}");
            Directory.CreateDirectory(tempFolder);

            string sourcePath = Path.Combine(tempFolder, "Test.Assembly.dll");
            string destinationPath = Path.Combine(tempFolder, "destination", "Test.Assembly.dll");
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
            File.WriteAllText(sourcePath, "new");
            File.WriteAllText(destinationPath, "old");
            File.SetLastWriteTime(sourcePath, DateTime.Now);
            File.SetLastWriteTime(destinationPath, DateTime.Now.AddMinutes(-5));

            try
            {
                using AssetSelectionControl control = new()
                {
                    ShowDeploymentDateComparison = true
                };

                ListView listView = FindDescendant<ListView>(control)
                    ?? throw new InvalidOperationException("Assembly ListView was not found.");
                listView.Columns.Add("Assembly name");
                listView.Columns.Add("Path");

                DesktopAssemblyInfo assemblyInfo = new()
                {
                    Name = "Test.Assembly",
                    Path = new DirectoryInfo(tempFolder),
                    AssemblyFiles = [new FileInfo(sourcePath)]
                };

                ListViewItem item = new("Test.Assembly")
                {
                    Tag = assemblyInfo
                };
                item.SubItems.Add(tempFolder);
                listView.Items.Add(item);

                control.SetDeploymentComparisonResolver(
                    _ => (new FileInfo(sourcePath), new FileInfo(destinationPath)));

                Assert.True(item.Font.Bold);

                control.RefreshDeploymentDateComparison(assemblyInfo);
                Assert.True(item.Font.Bold);

                File.Copy(sourcePath, destinationPath, overwrite: true);
                control.RefreshDeploymentDateComparison(assemblyInfo);

                Assert.False(item.Font.Bold);
                Assert.NotEqual("(new)", item.SubItems[3].Text);
            }
            finally
            {
                Directory.Delete(tempFolder, recursive: true);
            }
        });

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
}
