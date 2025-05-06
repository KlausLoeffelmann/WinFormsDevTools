using CommunityToolkit.WinForms;
using CommunityToolkit.WinForms.AppServices.ServiceExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WinForms;

namespace DevTools.GitHubBuddy
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create the builder
            var builder = WinFormsApplication.CreateBuilder();

            // Add services
            builder.Services.AddWinFormsUserSettingsService();
            builder.Services.AddScoped<FrmGitHubExplorer>();
            
            // Configure logging
            builder.Logging.AddConsole();
            
            // Configure WinForms-specific options
            builder.UseStartupForm<FrmGitHubExplorer>()
                .UseHighDpiMode(HighDpiMode.SystemAware)
                .UseColorMode(SystemColorMode.System)
                .UseVisualStyles();
                
            // Build and run the application
            var app = builder.Build();
            app.Run();
        }
    }
}
