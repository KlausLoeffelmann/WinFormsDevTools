using DevTools.RuntimeDeploy.Engine.PatchBackup;

namespace DevTools.RuntimeDeploy.Cli;

/// <summary>
///  Minimal in-terminal list picker used when a backup search finds more
///  than one candidate. Uses ANSI/VT100 escape sequences (supported by the
///  modern Windows console/terminal) for a scrolling highlighted selection
///  bar; falls back to direct number-key selection when there are 9 or
///  fewer candidates.
/// </summary>
public static class ConsoleBackupPicker
{
    /// <summary>Number of entries shown at once by the scrolling selection bar.</summary>
    private const int VisibleEntries = 10;

    private const string EnterReverseVideo = "\x1b[7m";
    private const string ResetFormatting = "\x1b[0m";
    private const string ClearScreenAndHome = "\x1b[2J\x1b[H";

    public static BackupSearchResult? Pick(IReadOnlyList<BackupSearchResult> results)
    {
        if (results.Count == 0)
        {
            return null;
        }

        if (results.Count == 1)
        {
            return results[0];
        }

        return results.Count <= 9
            ? PickWithNumberKeys(results)
            : PickWithSelectionBar(results);
    }

    private static BackupSearchResult PickWithNumberKeys(IReadOnlyList<BackupSearchResult> results)
    {
        Console.WriteLine("Multiple backups found. Select one:");
        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {Describe(results[i])}");
        }

        while (true)
        {
            Console.Write("> ");
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            Console.WriteLine(key.KeyChar);

            int index = key.KeyChar - '1';
            if (index >= 0 && index < results.Count)
            {
                return results[index];
            }
        }
    }

    private static BackupSearchResult PickWithSelectionBar(IReadOnlyList<BackupSearchResult> results)
    {
        int selectedIndex = 0;
        int windowStart = 0;

        bool cursorVisible = TryGetCursorVisible();
        Console.CursorVisible = false;

        try
        {
            RenderWindow(results, selectedIndex, windowStart);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = Math.Max(0, selectedIndex - 1);
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex = Math.Min(results.Count - 1, selectedIndex + 1);
                        break;

                    case ConsoleKey.Enter:
                        return results[selectedIndex];

                    default:
                        continue;
                }

                if (selectedIndex < windowStart)
                {
                    windowStart = selectedIndex;
                }
                else if (selectedIndex >= windowStart + VisibleEntries)
                {
                    windowStart = selectedIndex - VisibleEntries + 1;
                }

                RenderWindow(results, selectedIndex, windowStart);
            }
        }
        finally
        {
            Console.CursorVisible = cursorVisible;
        }
    }

    private static void RenderWindow(IReadOnlyList<BackupSearchResult> results, int selectedIndex, int windowStart)
    {
        Console.Write(ClearScreenAndHome);
        Console.WriteLine("Multiple backups found. Use Up/Down + Enter to select:");
        Console.WriteLine();

        int end = Math.Min(results.Count, windowStart + VisibleEntries);
        for (int i = windowStart; i < end; i++)
        {
            string line = Describe(results[i]);

            if (i == selectedIndex)
            {
                Console.Write(EnterReverseVideo);
                Console.Write("> " + line);
                Console.Write(ResetFormatting);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("  " + line);
            }
        }
    }

    private static bool TryGetCursorVisible()
    {
        try
        {
            return Console.CursorVisible;
        }
        catch
        {
            // CursorVisible getter throws when output is redirected; default to
            // "visible" so the finally-block restore is a no-op in that case.
            return true;
        }
    }

    private static string Describe(BackupSearchResult result)
        => $"{result.Manifest.CreatedUtc:yyyy-MM-dd HH:mm} UTC  TFM={result.Manifest.Tfm}  Config={result.Manifest.Configuration}  {result.BackupFile.Name}";
}
