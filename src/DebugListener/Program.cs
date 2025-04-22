using CommunityToolkit.WinForms.AppServices.ServiceExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WinForms;
using System.Diagnostics;

namespace DebugListener;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        var builder = WinFormsApplication.CreateBuilder();

        builder.Services.AddWinFormsUserSettingsService();
        builder.Services.AddWinFormsExceptionService();

        Debug.Assert(Thread.CurrentThread.GetApartmentState() == ApartmentState.STA);

        // Configure logging
        builder.Logging.AddConsole();
        builder.Services.AddScoped<FrmMain>();

        // Configure WinForms-specific options
        builder.UseStartupForm<FrmMain>()
            .UseHighDpiMode(HighDpiMode.SystemAware)
            .UseColorMode(SystemColorMode.System);

        // Build and run the application
        WinFormsApplication app = builder.Build();

        app.Run();
    }
}
