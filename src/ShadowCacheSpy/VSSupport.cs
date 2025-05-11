using CommunityToolkit.WinForms.Tooling;
using Microsoft.VisualStudio.Setup.Configuration;

namespace DevTools.ShadowCacheSpy;

internal class VSSupport
{
    static internal List<ISetupInstance2> GetInstalledVisualStudioInstances()
    {
        var setupConfiguration = new SetupConfiguration();
        var instanceEnumerator = setupConfiguration.EnumInstances();
        var instances = new List<ISetupInstance2>();

        int fetched;
        var instancesArray = new ISetupInstance[1];

        do
        {
            instanceEnumerator.Next(1, instancesArray, out fetched);

            if (fetched > 0 && instancesArray[0] is ISetupInstance2 instance)
            {
                instances.Add(instance);
            }
        }
        while (fetched > 0);

        return instances;
    }

    static internal async Task ListVisualStudioInstancesAsync(ConsoleControl console)
    {
        try
        {
            var instances = GetInstalledVisualStudioInstances();

            foreach (var instance in instances)
            {
                // Output the instance name, version, and installation path
                await console.WriteLineAsync($"Instance: {instance.GetDisplayName()} - Version: {instance.GetInstallationVersion()}");
                await console.WriteLineAsync($"Installation Path: {instance.GetInstallationPath()}");

                string installationVersion = instance.GetInstallationVersion();
                string installationPath = instance.GetInstallationPath();
                string installationName = instance.GetInstallationName();
                string displayName = instance.GetDisplayName();
                string enginePath = instance.GetEnginePath();
                string productPath = instance.GetProductPath();
                string instanceId = instance.GetInstanceId();
                var properties = instance.GetProperties();
                foreach (var propertyName in properties.GetNames())
                {
                    await console.WriteLineAsync($" - {propertyName}: {properties.GetValue(propertyName)}");
                }

                // Print all that info to the console:
                await console.WriteLineAsync($"Installation Version: {installationVersion}");
                await console.WriteLineAsync($"Installation Path: {installationPath}");
                await console.WriteLineAsync($"Installation Name: {installationName}");
                await console.WriteLineAsync($"Display Name: {displayName}");
                await console.WriteLineAsync($"Engine Path: {enginePath}");
                await console.WriteLineAsync($"Product Path: {productPath}");

                //// Construct and display the user data path based on the instance path
                //string userDataPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Microsoft\VisualStudio\{instance.GetInstallationName()}";
                //await console.WriteLineAsync($"User Data Path: {userDataPath}");

                //// List directories within the user data path (e.g., experimental hives)
                //if (System.IO.Directory.Exists(userDataPath))
                //{
                //    string[] subdirectories = System.IO.Directory.GetDirectories(userDataPath);
                //    foreach (var subdirectory in subdirectories)
                //    {
                //        await console.WriteLineAsync($" - {subdirectory}");
                //    }
                //}
                await console.WriteLineAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving Visual Studio instances: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
