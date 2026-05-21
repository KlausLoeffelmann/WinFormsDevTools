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

        builder.Services.AddWinFormsUserSettingsService();

        WinFormsApplication app = builder.Build();
        app.Run();
    }
}