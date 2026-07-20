using DevTools.RuntimeDeploy.Infrastructure;
using DevTools.RuntimeDeploy.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WinForms;
using WarpToolkit.WinForms.AppServices;
using WarpToolkit.WinForms.AppServices.ServiceExtensions;

namespace DevTools.RuntimeDeploy;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        WinFormsApplicationBuilder builder = WinFormsApplication.CreateBuilder(args);

        builder
            .UseHighDpiMode(HighDpiMode.SystemAware)
            .UseColorMode(SystemColorMode.System)
            .UseStartupForm<MainForm>();

        builder.Logging.AddConsole();

        builder.Services
            .AddWinFormsUserSettingsService()
            .AddWinFormsExceptionService();

        builder.Services
            .AddSingleton(RuntimeDeployLaunchOptions.FromArguments(args))
            .AddScoped<IStartupRegistrationService, WindowsStartupRegistrationService>()
            .AddScoped<RuntimeDeploySettingsService>()
            .AddScoped<RuntimeDeployStatusService>()
            .AddScoped<OverView>()
            .AddTransient<AssetSelectionControl>()
            .AddScoped<DeployRuntimeView>()
            .AddTransient<OptionsForm>()
            .AddTransient<CreateRuntimePatcherForm>()
            .AddTransient<RestoreBackupForm>();

        WinFormsApplication app = builder.Build();
        app.Run();
    }
}