namespace WinFormsDevTools
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Enable DarkMode:
            Application.SetDefaultDarkMode(DarkMode.Inherits);

            Application.Run(new MainForm());
        }
    }
}