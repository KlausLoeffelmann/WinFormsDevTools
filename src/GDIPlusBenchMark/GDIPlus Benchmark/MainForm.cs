using System.ComponentModel;
using System.Reflection;

namespace GDIPlus_Benchmark;

public partial class MainForm : Form
{
    // Backing fields for the combo box selections
    private int _runCount = 10;
    private int _figureCount = 100;

    // Add resolution menu items
    private readonly static Dictionary<string, (string Text, Size Size)> _resolutions =
        new()
        {
            { "SVGA", ("SVGA (800 x 600)", new Size(800, 600)) },
            { "XGA", ("XGA (1024 x 768)", new Size(1024, 768)) },
            { "WXGA", ("WXGA (1280 x 800)", new Size(1280, 800)) },
            { "SXGA", ("SXGA (1280 x 1024)", new Size(1280, 1024)) },
            { "FHD", ("FHD (1920 x 1080)", new Size(1920, 1080)) }
        };

    // Add total runs menu items
    private readonly int[] _totalRuns = [10, 50, 100, 200, 300, 400, 500, 1000];
    private readonly int[] _figureRuns = [10, 50, 100, 500, 1000, 5000, 10000];

    // Update _selectedResolution to store only the key
    private string _selectedResolution = "WXGA";

    private List<Type> _benchmarkTypes = new();
    private List<string> _benchmarkNames = new();

    public MainForm()
    {
        InitializeComponent();
        CenterForm();
        InitializeBenchmarkSettings();

        // Set initial status bar values
        UpdateStatusBar();
    }

    private void CenterForm()
    {
        // Get the desired render surface size from the selected resolution
        if (!_resolutions.TryGetValue(_selectedResolution, out (string Text, Size Size) resInfo))
            return;

        Size desiredPanelSize = resInfo.Size;

        // Calculate the difference between the current panel size and the desired size
        int widthDelta = desiredPanelSize.Width - _pnlRenderSurface.Width;
        int heightDelta = desiredPanelSize.Height - _pnlRenderSurface.Height;

        // Calculate the new client size required to fit the panel at the desired size
        Size newClientSize = ClientSize;
        newClientSize.Width += widthDelta;
        newClientSize.Height += heightDelta;

        // Set the form's client size to accommodate the resized panel and all other controls
        ClientSize = newClientSize;
    }

    /// <summary>
    /// Initializes the benchmark settings by populating combo boxes and setting up event handlers
    /// </summary>
    private void InitializeBenchmarkSettings()
    {
        // Populate resolution menu items with both name and resolution
        foreach ((string key, (string Text, Size Size) value) in _resolutions)
        {
            ToolStripMenuItem item = new ToolStripMenuItem($"{value.Text}")
            {
                Tag = key // Store the key as Tag
            };

            item.Click += ResolutionMenuItem_Click;
            toolStripMenuItem1.DropDownItems.Add(item);
        }

        foreach (int run in _totalRuns)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(run.ToString())
            {
                Tag = run
            };

            item.Click += TotalRunsMenuItem_Click;
            totalRunsToolStripMenuItem.DropDownItems.Add(item);
        }

        foreach (int run in _figureRuns)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(run.ToString())
            {
                Tag = run
            };

            item.Click += FigureRunsMenuItem_Click;
            figureRunsToolStripMenuItem.DropDownItems.Add(item);
        }

        // Discover and populate benchmark combo boxes
        DiscoverBenchmarks();
        PopulateBenchmarkComboBoxes();
    }

    /// <summary>
    /// Uses reflection to find all non-abstract FigureBase subclasses in the current assembly
    /// </summary>
    private void DiscoverBenchmarks()
    {
        _benchmarkTypes.Clear();
        _benchmarkNames.Clear();

        Type baseType = typeof(FigureBase);
        Assembly asm = Assembly.GetExecutingAssembly();

        foreach (Type t in asm.GetTypes())
        {
            if (t.IsAbstract || !baseType.IsAssignableFrom(t))
                continue;

            // Use DescriptionAttribute if present, else class name
            DescriptionAttribute? descAttr = t.GetCustomAttribute<DescriptionAttribute>();
            string name = descAttr?.Description ?? t.Name;

            _benchmarkTypes.Add(t);
            _benchmarkNames.Add(name);
        }
    }

    /// <summary>
    /// Populates the benchmark combo boxes with discovered benchmarks
    /// </summary>
    private void PopulateBenchmarkComboBoxes()
    {
        _tscBenchmark1.Items.Clear();
        _tscBenchmark2.Items.Clear();
        foreach (string name in _benchmarkNames)
        {
            _tscBenchmark1.Items.Add(name);
            _tscBenchmark2.Items.Add(name);
        }
        // Add "None" to Benchmark2 for optional comparison
        _tscBenchmark2.Items.Insert(0, "None");
        // Optionally select first item by default
        if (_tscBenchmark1.Items.Count > 0)
            _tscBenchmark1.SelectedIndex = 0;

        if (_tscBenchmark2.Items.Count > 0)
            _tscBenchmark2.SelectedIndex = 0;
    }

    /// <summary>
    /// Handles changes to the Run Count combo box selection
    /// </summary>
    private void OnRunCountChanged(object? sender, EventArgs e)
    {
        if (_tscBenchmark1.SelectedItem != null &&
            int.TryParse(_tscBenchmark1.SelectedItem.ToString(), out int newRunCount))
        {
            _runCount = newRunCount;
            UpdateStatusBar();
        }
    }

    /// <summary>
    /// Handles changes to the Figure Count combo box selection
    /// </summary>
    private void OnFigureCountChanged(object? sender, EventArgs e)
    {
        if (_tscBenchmark2.SelectedItem != null &&
            int.TryParse(_tscBenchmark2.SelectedItem.ToString(), out int newFigureCount))
        {
            _figureCount = newFigureCount;
            UpdateStatusBar();
        }
    }

    private void ResolutionMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem item && item.Tag is string key && _resolutions.TryGetValue(key, out (string Text, Size Size) resInfo))
        {
            _selectedResolution = key;
            CenterForm();
            UpdateStatusBar();
        }
    }

    private void TotalRunsMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem item && int.TryParse(item.Tag!.ToString(), out int runs))
        {
            _runCount = runs;
            UpdateStatusBar();
        }
    }

    private void FigureRunsMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem item && int.TryParse(item.Tag!.ToString(), out int runs))
        {
            _figureCount = runs;
            UpdateStatusBar();
        }
    }

    private void UpdateStatusBar()
    {
        if (_resolutions.TryGetValue(_selectedResolution, out (string Text, Size Size) resInfo))
        {
            _tslTestResolution.Text = $"{resInfo.Text}";
        }
        else
        {
            _tslTestResolution.Text = "Unknown";
        }
        _tslTotalRuns.Text = _runCount.ToString();
        _tslFigureRuns.Text = _figureCount.ToString();
    }

    /// <summary>
    /// Gets the current run count setting
    /// </summary>
    public int RunCount => _runCount;

    /// <summary>
    /// Gets the current figure count setting
    /// </summary>
    public int FigureCount => _figureCount;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        _lvwResults.View = View.Details;
        _lvwResults.Columns.Clear();
        _lvwResults.Columns.Add("Run #", 70, HorizontalAlignment.Left);
        _lvwResults.Columns.Add("Figure Runs", 100, HorizontalAlignment.Left);
        _lvwResults.Columns.Add("Benchmark 1 result", 150, HorizontalAlignment.Left);
        _lvwResults.Columns.Add("Benchmark 1 average", 150, HorizontalAlignment.Left);
        _lvwResults.Columns.Add("Benchmark 2 result", 150, HorizontalAlignment.Left);
        _lvwResults.Columns.Add("Benchmark 2 average", 150, HorizontalAlignment.Left);
    }

    private async void OnStartBenchmarkClicked(object? sender, EventArgs e)
    {
        // Clear previous results
        _lvwResults.Items.Clear();

        int totalRuns = _runCount;
        int figureRuns = _figureCount;

        int benchmark1Index = _tscBenchmark1.SelectedIndex;
        int benchmark2Index = _tscBenchmark2.SelectedIndex;

        string benchmark1Name = _benchmarkNames[benchmark1Index];
        string? benchmark2Name = benchmark2Index > 0 ? _benchmarkNames[benchmark2Index - 1] : null;

        Type benchmark1Type = _benchmarkTypes[benchmark1Index];
        Type? benchmark2Type = benchmark2Index > 0 ? _benchmarkTypes[benchmark2Index - 1] : null;

        var results = new List<BenchmarkResults>();
        var b1Times = new List<double>();
        var b2Times = new List<double>();

        for (int run = 1; run <= totalRuns; run++)
        {
            double? b1Result = null, b2Result = null;

            if (benchmark2Type == null)
            {
                // Only Benchmark 1
                var b1 = (FigureBase)Activator.CreateInstance(benchmark1Type)!;
                b1.FigureCount = figureRuns;
                var sw = System.Diagnostics.Stopwatch.StartNew();
                using (var bmp = new Bitmap(100, 100))
                using (var g = Graphics.FromImage(bmp))
                {
                    b1.Bounds = new Rectangle(0, 0, 100, 100);
                    b1.Draw(g);
                }
                sw.Stop();
                b1Result = sw.Elapsed.TotalMilliseconds;
                b1Times.Add(b1Result.Value);
            }
            else
            {
                // Alternate: odd runs = Benchmark 1, even runs = Benchmark 2
                if (run % 2 == 1)
                {
                    var b1 = (FigureBase)Activator.CreateInstance(benchmark1Type)!;
                    b1.FigureCount = figureRuns;
                    var sw = System.Diagnostics.Stopwatch.StartNew();
                    using (var bmp = new Bitmap(100, 100))
                    using (var g = Graphics.FromImage(bmp))
                    {
                        b1.Bounds = new Rectangle(0, 0, 100, 100);
                        b1.Draw(g);
                    }
                    sw.Stop();
                    b1Result = sw.Elapsed.TotalMilliseconds;
                    b1Times.Add(b1Result.Value);
                }
                else
                {
                    var b2 = (FigureBase)Activator.CreateInstance(benchmark2Type!)!;
                    b2.FigureCount = figureRuns;
                    var sw = System.Diagnostics.Stopwatch.StartNew();
                    using (var bmp = new Bitmap(100, 100))
                    using (var g = Graphics.FromImage(bmp))
                    {
                        b2.Bounds = new Rectangle(0, 0, 100, 100);
                        b2.Draw(g);
                    }
                    sw.Stop();
                    b2Result = sw.Elapsed.TotalMilliseconds;
                    b2Times.Add(b2Result.Value);
                }
            }

            // Calculate averages so far
            double b1Avg = b1Times.Count > 0 ? b1Times.Average() : 0;
            double? b2Avg = b2Times.Count > 0 ? b2Times.Average() : (double?)null;

            var result = new BenchmarkResults(
                run,
                figureRuns,
                b1Result ?? 0,
                b1Avg,
                b2Result,
                b2Avg,
                benchmark1Name,
                benchmark2Name
            );
            results.Add(result);

            // Add to ListView
            var lvi = new ListViewItem(result.RunNumber.ToString());
            lvi.SubItems.Add(result.FigureRuns.ToString());
            lvi.SubItems.Add(result.Benchmark1Result.ToString("F2"));
            lvi.SubItems.Add(result.Benchmark1Average.ToString("F2"));
            lvi.SubItems.Add(result.Benchmark2Result?.ToString("F2") ?? "");
            lvi.SubItems.Add(result.Benchmark2Average?.ToString("F2") ?? "");
            _lvwResults.Items.Add(lvi);

            // Allow UI to update
            await Task.Delay(10);
        }

        // Save results to JSON file after benchmark completes
        SaveBenchmarkResults(results);
    }

    private void SaveBenchmarkResults(List<BenchmarkResults> results)
    {
        try
        {
            // Create app data folder path
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolderPath = Path.Combine(appDataPath, "GDIPlus Benchmark");
            
            // Ensure directory exists
            Directory.CreateDirectory(appFolderPath);
            
            // Generate filename with timestamp
            string timestamp = DateTime.Now.ToString("yy-MM-dd-HHmm");
            string filename = $"GDIP_BM_{timestamp}.json";
            string fullPath = Path.Combine(appFolderPath, filename);
            
            // Serialize results to JSON
            string json = System.Text.Json.JsonSerializer.Serialize(results, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });
            
            // Write to file
            File.WriteAllText(fullPath, json);
            
            // Optional: Show status to user
            _tslTestResolution.Text = $"Results saved to {filename}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to save benchmark results: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
