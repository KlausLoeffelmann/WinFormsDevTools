using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DevTools.FakeDesignToolsServer;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        PerfDbgTrace("**** Reached main of Fake DesignToolsServer.", markImportant: true);

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        PerfDbgTrace("Initialized Fake DesignToolsServer process.", markImportant: true);
        
        Application.Run(new FrmMain());
    }
}