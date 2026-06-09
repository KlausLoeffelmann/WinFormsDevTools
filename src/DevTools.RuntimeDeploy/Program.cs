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
            .UseDefaultFont(new Font("Segoe UI", 11f))
            .UseStartupForm<MainForm>();

        builder.Logging.AddConsole();

        builder.Services
            .AddWinFormsUserSettingsService()
            .AddWinFormsExceptionService();

        builder.Services
            .AddScoped<RuntimeDeploySettingsService>()
            .AddScoped<RuntimeDeployStatusService>()
            .AddScoped<OverView>()
            .AddScoped<DeployRuntimeView>()
            .AddTransient<OptionsForm>();

        WinFormsApplication app = builder.Build();
        app.Run();
    }
}