namespace GDIPlus_Benchmark;

public partial class Form1 : Form
{ 
    // Backing fields for the combo box selections
    private int _runCount = 10;
    private int _figureCount = 100;
    private Size _desiredFormClientSize = new(1920, 1080);
    private Size _selectedResolution = new(1920, 1080); // Backing field for selected resolution

    public Form1()
    {
        InitializeComponent();
        CenterForm();
        InitializeBenchmarkSettings();
        // Set initial status bar values
        UpdateStatusBar();
    }

    private void CenterForm()
    {
        // Set the form's client size to the desired size
        ClientSize = _desiredFormClientSize;
        // Center the form on the screen
        StartPosition = FormStartPosition.CenterScreen;
    }

    /// <summary>
    /// Initializes the benchmark settings by populating combo boxes and setting up event handlers
    /// </summary>
    private void InitializeBenchmarkSettings()
    {
        // Add resolution menu items
        var resolutions = new (string Text, Size Size)[]
        {
                ("1280 x 720", new Size(1280, 720)),
                ("1920 x 1080", new Size(1920, 1080)),
                ("2560 x 1440", new Size(2560, 1440)),
                ("3840 x 2160", new Size(3840, 2160)),
        };

        foreach (var (text, size) in resolutions)
        {
            var item = new ToolStripMenuItem(text)
            {
                Tag = size
            };
            item.Click += ResolutionMenuItem_Click;
            toolStripMenuItem1.DropDownItems.Add(item);
        }

        // Add total runs menu items
        int[] totalRuns = [10, 50, 100, 500, 1000];

        foreach (var run in totalRuns)
        {
            var item = new ToolStripMenuItem(run.ToString())
            {
                Tag = run
            };
            item.Click += TotalRunsMenuItem_Click;
            totalRunsToolStripMenuItem.DropDownItems.Add(item);
        }

        // Add figure runs menu items
        int[] figureRuns = [100, 500, 1000, 5000, 10000];
        foreach (var run in figureRuns)
        {
            var item = new ToolStripMenuItem(run.ToString())
            {
                Tag = run
            };

            item.Click += FigureRunsMenuItem_Click;
            figureRunsToolStripMenuItem.DropDownItems.Add(item);
        }
    }

    /// <summary>
    /// Initializes the Run Count combo box with values:
    /// - 10 to 100 in increments of 10
    /// - 100 to 1000 in increments of 100
    /// </summary>
    private void InitializeRunCountComboBox()
    {
        _tscBenchmark1.Items.Clear();

        // Add values from 10 to 100 in increments of 10
        for (int i = 10; i <= 100; i += 10)
        {
            _tscBenchmark1.Items.Add(i.ToString());
        }

        // Add values from 200 to 1000 in increments of 100 (skipping 100 as it's already added)
        for (int i = 200; i <= 1000; i += 100)
        {
            _tscBenchmark1.Items.Add(i.ToString());
        }

        // Set default selection
        _tscBenchmark1.SelectedItem = _runCount.ToString();
    }

    /// <summary>
    /// Initializes the Figure Count combo box with values:
    /// - 100 to 2000 in increments of 100
    /// - 2000 to 10000 in increments of 1000
    /// </summary>
    private void InitializeFigureCountComboBox()
    {
        _tscBenchmark2.Items.Clear();

        // Add values from 100 to 2000 in increments of 100
        for (int i = 100; i <= 2000; i += 100)
        {
            _tscBenchmark2.Items.Add(i.ToString());
        }

        // Add values from 3000 to 10000 in increments of 1000 (skipping 2000 as it's already added)
        for (int i = 3000; i <= 10000; i += 1000)
        {
            _tscBenchmark2.Items.Add(i.ToString());
        }

        // Set default selection
        _tscBenchmark2.SelectedItem = _figureCount.ToString();
    }

    /// <summary>
    /// Sets up event handlers for combo box selection changes
    /// </summary>
    private void SetupComboBoxEventHandlers()
    {
        _tscBenchmark1.SelectedIndexChanged += OnRunCountChanged;
        _tscBenchmark2.SelectedIndexChanged += OnFigureCountChanged;
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

    private void BenchmarkLines_Click(object sender, EventArgs e)
    {
        FigureRenderer<LineDrawing> figureRenderer = new(
            _figureCount, 
            _runCount);

        Graphics graphics = CreateGraphics();
        graphics.Clear(Color.White);

        try
        {
            TimeSpan elapsed = figureRenderer.BenchmarksFigure(
                graphics, 
                ClientRectangle);

            MessageBox.Show($"Line drawing benchmark completed in {elapsed.TotalMilliseconds} ms");
        }
        finally
        {
            graphics.Dispose();
        }
    }

    private void ResolutionMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem item && item.Tag is Size size)
        {
            _selectedResolution = size;
            _desiredFormClientSize = size;
            CenterForm();
            UpdateStatusBar();
        }
    }

    private void TotalRunsMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem item && int.TryParse(item.Tag.ToString(), out int runs))
        {
            _runCount = runs;
            UpdateStatusBar();
        }
    }

    private void FigureRunsMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem item && int.TryParse(item.Tag.ToString(), out int runs))
        {
            _figureCount = runs;
            UpdateStatusBar();
        }
    }

    private void UpdateStatusBar()
    {
        _tslTestResolution.Text = $"{_selectedResolution.Width} x {_selectedResolution.Height}";
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
}
