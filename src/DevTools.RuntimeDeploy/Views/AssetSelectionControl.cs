using DevTools.RuntimeDeploy.Engine.Domain;
using DevTools.RuntimeDeploy.Infrastructure;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using static DevTools.RuntimeDeploy.Engine.Domain.BuildArtefactsScanner;

namespace DevTools.RuntimeDeploy.Views;

/// <summary>
///  Reusable picker UI for the WinForms runtime artefacts source folder, the
///  target framework, and the set of assemblies to include. Extracted out of
///  <see cref="DeployRuntimeView"/> so it can also be hosted by
///  <see cref="CreateRuntimePatcherForm"/> - both share this selection UI but
///  present a different action panel next to it ("Copy" vs. "Create package
///  installer...").
/// </summary>
public partial class AssetSelectionControl : UserControl
{
    private readonly Control[] _controlsForEnablingHandling;
    private BuildArtefactsScanner? _gitHubRepoManager;
    private RuntimeDeploySettingsService? _settings;
    private RuntimeDeployStatusService? _statusService;
    private ILogger<AssetSelectionControl>? _logger;

    private Func<DesktopAssemblyInfo, (FileInfo? SourceFile, FileInfo? DestinationFile)>? _deploymentComparisonResolver;
    private Font? _deploymentComparisonBoldFont;

    private const string ACCESSIBILITY = "Accessibility";
    private const string MICROSOFT_VISUALBASIC = "Microsoft.VisualBasic";
    private const string MICROSOFT_VISUALBASIC_FACADE = "Microsoft.VisualBasic.Facade";
    private const string MICROSOFT_VISUALBASIC_FORMS = "Microsoft.VisualBasic.Forms";
    private const string MICROSOFT_PRIVATE_WINFORMS = "Microsoft.Private.Winforms";
    private const string SYSTEM_DESIGN_FACADE = "System.Design.Facade";
    private const string SYSTEM_DRAWING_COMMON = "System.Drawing.Common";
    private const string SYSTEM_DRAWING_DESIGN_FACADE = "System.Drawing.Design.Facade";
    private const string SYSTEM_DRAWING_FACADE = "System.Drawing.Facade";
    private const string SYSTEM_PRIVATE_WINDOWS_CORE = "System.Private.Windows.Core";
    private const string SYSTEM_PRIVATE_WINDOWS_GDIPLUS = "System.Private.Windows.GdiPlus";
    private const string SYSTEM_WINDOWS_FORMS = "System.Windows.Forms";
    private const string SYSTEM_WINDOWS_FORMS_ANALYZERS = "System.Windows.Forms.Analyzers";
    private const string SYSTEM_WINDOWS_FORMS_ANALYZERS_CSHARP = "System.Windows.Forms.Analyzers.CSharp";
    private const string SYSTEM_WINDOWS_FORMS_ANALYZERS_VISUALBASIC = "System.Windows.Forms.Analyzers.VisualBasic";
    private const string SYSTEM_WINDOWS_FORMS_ANALYZERS_CODEFIXES_CSHARP = "System.Windows.Forms.Analyzers.CodeFixes.CSharp";
    private const string SYSTEM_WINDOWS_FORMS_ANALYZERS_CODEFIXES_VISUALBASIC = "System.Windows.Forms.Analyzers.CodeFixes.VisualBasic";
    private const string SYSTEM_WINDOWS_FORMS_DESIGN = "System.Windows.Forms.Design";
    private const string SYSTEM_WINDOWS_FORMS_PRIMITIVES = "System.Windows.Forms.Primitives";
    private const string SYSTEM_WINDOWS_FORMS_PRIVATESOURCEGENERATORS = "System.Windows.Forms.PrivateSourceGenerators";

    private readonly string[] s_preCheckItems =
    [
        ACCESSIBILITY,
        MICROSOFT_VISUALBASIC,
        MICROSOFT_VISUALBASIC_FACADE,
        MICROSOFT_VISUALBASIC_FORMS,
        MICROSOFT_PRIVATE_WINFORMS,
        SYSTEM_DESIGN_FACADE,
        SYSTEM_DRAWING_COMMON,
        SYSTEM_DRAWING_DESIGN_FACADE,
        SYSTEM_DRAWING_FACADE,
        SYSTEM_PRIVATE_WINDOWS_CORE,
        SYSTEM_PRIVATE_WINDOWS_GDIPLUS,
        SYSTEM_WINDOWS_FORMS,
        SYSTEM_WINDOWS_FORMS_ANALYZERS,
        SYSTEM_WINDOWS_FORMS_ANALYZERS_CSHARP,
        SYSTEM_WINDOWS_FORMS_ANALYZERS_VISUALBASIC,
        SYSTEM_WINDOWS_FORMS_ANALYZERS_CODEFIXES_CSHARP,
        SYSTEM_WINDOWS_FORMS_ANALYZERS_CODEFIXES_VISUALBASIC,
        SYSTEM_WINDOWS_FORMS_DESIGN,
        SYSTEM_WINDOWS_FORMS_PRIMITIVES,
        SYSTEM_WINDOWS_FORMS_PRIVATESOURCEGENERATORS
    ];

    /// <summary>
    ///  Raised whenever the set of available/checked assemblies changes
    ///  (e.g. after picking a new source folder or target framework), so a
    ///  host can enable/disable its own action controls accordingly.
    /// </summary>
    public event EventHandler? AvailabilityChanged;

    /// <summary>
    ///  When <see langword="true"/> (set only by the actual copy dialog -
    ///  <see cref="DeployRuntimeView"/> - not the package-creation dialog or
    ///  the Options tools list, which host the same shared control), the
    ///  assembly list also shows "Source Date" / "Destination Date" columns
    ///  and colors each row to flag assemblies whose destination copy is
    ///  older than the source (and will therefore be replaced by a copy).
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowDeploymentDateComparison { get; set; }

    public AssetSelectionControl()
    {
        InitializeComponent();

        _pathToArtefactsRepo.PathChanged += PathToArtefactsRepo_PathChanged;

        _availableDesktopRuntimesComboBox.SelectedIndexChanged +=
            (sender, e) => DeployAvailableAssemblies();

        _checkForRespectiveRefAssembliesCheckBox.CheckedChanged +=
            (sender, e) => DeployAvailableAssemblies();

        _chkStandardAssemblies.CheckedChanged +=
            (sender, e) => DeployAvailableAssemblies();

        _controlsForEnablingHandling =
        [
            _availableDesktopRuntimesComboBox,
            _checkForRespectiveRefAssembliesCheckBox,
            _chkStandardAssemblies,
            _availableAssembliesListView,
        ];

        // Dispose(bool) lives in the Designer file and must not be hand-edited;
        // the Disposed event is the supported hook for extra cleanup.
        Disposed += (sender, e) => _deploymentComparisonBoldFont?.Dispose();
    }

    public AssetSelectionControl(
        RuntimeDeploySettingsService settings,
        RuntimeDeployStatusService statusService,
        ILogger<AssetSelectionControl> logger) : this()
    {
        _settings = settings;
        _statusService = statusService;
        _logger = logger;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        string sourceArtefactsFolder = _settings?.SourceArtefactsFolder ?? string.Empty;
        if (!string.Equals(
            _pathToArtefactsRepo.FileOrFolderPath,
            sourceArtefactsFolder,
            StringComparison.OrdinalIgnoreCase))
        {
            _pathToArtefactsRepo.FileOrFolderPath = sourceArtefactsFolder;
            return;
        }

        DeployAvailableRuntimes();
    }

    /// <summary>
    ///  Re-reads the source folder from settings and, if unchanged, refreshes
    ///  the assembly list (e.g. because the exclusion list changed).
    /// </summary>
    public void RefreshFromSettings()
    {
        string sourceArtefactsFolder = _settings?.SourceArtefactsFolder ?? string.Empty;
        if (!string.Equals(
            _pathToArtefactsRepo.FileOrFolderPath,
            sourceArtefactsFolder,
            StringComparison.OrdinalIgnoreCase))
        {
            _pathToArtefactsRepo.FileOrFolderPath = sourceArtefactsFolder;
            return;
        }

        DeployAvailableAssemblies();
    }

    /// <summary>Whether any assemblies are currently listed (a source folder + target were picked).</summary>
    public bool HasAssemblies => _availableAssembliesListView.Items.Count > 0;

    /// <summary>The currently checked assemblies.</summary>
    public DesktopAssemblyInfo[] GetCheckedAssemblies()
        =>
        [
            .. from ListViewItem item in _availableAssembliesListView.Items
               where item.Checked
               select (DesktopAssemblyInfo)item.Tag!
        ];

    /// <summary>The currently selected source target's TFM/configuration item, if any.</summary>
    public TargetFrameworkSourceItem? SelectedSourceTarget
        => _availableDesktopRuntimesComboBox.SelectedItem as TargetFrameworkSourceItem;

    /// <summary>
    ///  Sets (or clears, with <see langword="null"/>) the function used to
    ///  resolve the source/destination file pair for each listed assembly,
    ///  and immediately refreshes the date-comparison columns/colors to
    ///  reflect it. Only takes effect when <see cref="ShowDeploymentDateComparison"/>
    ///  is <see langword="true"/>.
    /// </summary>
    public void SetDeploymentComparisonResolver(
        Func<DesktopAssemblyInfo, (FileInfo? SourceFile, FileInfo? DestinationFile)>? resolver)
    {
        _deploymentComparisonResolver = resolver;
        RefreshDeploymentDateComparison();
    }

    /// <summary>
    ///  Finds the first listed assembly (checked or not) that has at least
    ///  one runtime assembly file.
    /// </summary>
    public DesktopAssemblyInfo? FindFirstAssembly()
        => (from ListViewItem item in _availableAssembliesListView.Items
            let assemblyInfo = (DesktopAssemblyInfo)item.Tag!
            where assemblyInfo.AssemblyFiles.Length > 0
            select assemblyInfo)
           .FirstOrDefault();

    private void HandleControlEnabling(bool enable, params Control[] excludeControlsForHandling)
    {
        foreach (var control in _controlsForEnablingHandling)
        {
            if (!excludeControlsForHandling.Contains(control))
            {
                control.Enabled = enable;
            }
        }
    }

    private void DeployAvailableRuntimes()
    {
        if (string.IsNullOrWhiteSpace(_pathToArtefactsRepo.FileOrFolderPath))
        {
            _availableDesktopRuntimesComboBox.Items.Clear();
            _availableAssembliesListView.Items.Clear();
            HandleControlEnabling(false);
            AvailabilityChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            try
            {
                _availableDesktopRuntimesComboBox.Items.Clear();
                _availableAssembliesListView.Items.Clear();
                _gitHubRepoManager = new(_pathToArtefactsRepo.FileOrFolderPath);

                var targets = _gitHubRepoManager
                    .GetAvailableTargets();

                _availableDesktopRuntimesComboBox.Items.AddRange(targets);
                if (targets.Length > 0)
                {
                    _availableDesktopRuntimesComboBox.SelectedIndex = targets.Length - 1;
                }

                HandleControlEnabling(true);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Could not enumerate RuntimeDeploy source targets.");
                _statusService?.ReportException(ex);
                HandleControlEnabling(false, _availableDesktopRuntimesComboBox);
            }
            finally
            {
                AvailabilityChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void DeployAvailableAssemblies()
    {
        if (_availableDesktopRuntimesComboBox.SelectedItem is null ||
            _gitHubRepoManager is null)
        {
            return;
        }

        HashSet<string> excludedAssemblyNames = _settings?.GetExcludedAssemblyNames() ?? [];

        var assemblies = _gitHubRepoManager.GetWinFormsRuntimeAssemblies(
            (TargetFrameworkSourceItem)_availableDesktopRuntimesComboBox.SelectedItem,
            includeRefAssemblies: _checkForRespectiveRefAssembliesCheckBox.Checked,
            includeNetStandardAssemblies: _chkStandardAssemblies.Checked)
            .Where(assembly => !excludedAssemblyNames.Contains(assembly.Name))
            .ToArray();

        _availableAssembliesListView.ConfigureDetailsView(checkBoxes: true);

        _availableAssembliesListView.AddItemsWithColumnHeadersFromType(
            assemblies,
            addSourceDataToTag: true,
            (nameof(DesktopAssemblyInfo.Name), "Assembly name"),
            (nameof(DesktopAssemblyInfo.Path), "Path"));

        _availableAssembliesListView.CheckItemsInFirstColumn(s_preCheckItems);

        // AddItemsWithColumnHeadersFromType just cleared and rebuilt the
        // columns/items above, so the date-comparison columns (if enabled)
        // need to be (re-)added here before being populated.
        if (ShowDeploymentDateComparison)
        {
            _availableAssembliesListView.Columns.Add("Source Date");
            _availableAssembliesListView.Columns.Add("Destination Date");
        }

        RefreshDeploymentDateComparison();

        AvailabilityChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    ///  Populates (or updates) the "Source Date"/"Destination Date" columns
    ///  and colors each row dark red (Classic)/light red (DarkMode) and bold
    ///  when the destination assembly is older than the source (i.e. it will
    ///  be replaced by a copy), or dark green (Classic)/light green
    ///  (DarkMode) and normal otherwise. No-op unless
    ///  <see cref="ShowDeploymentDateComparison"/> is <see langword="true"/>.
    /// </summary>
    private void RefreshDeploymentDateComparison()
    {
        if (!ShowDeploymentDateComparison)
        {
            return;
        }

        Font? previousBoldFont = _deploymentComparisonBoldFont;
        _deploymentComparisonBoldFont = new Font(_availableAssembliesListView.Font, FontStyle.Bold);
        previousBoldFont?.Dispose();

        bool isDarkMode = Application.IsDarkModeEnabled;
        Color replacedColor = isDarkMode ? Color.LightCoral : Color.DarkRed;
        Color upToDateColor = isDarkMode ? Color.LightGreen : Color.DarkGreen;

        foreach (ListViewItem item in _availableAssembliesListView.Items)
        {
            if (item.Tag is not DesktopAssemblyInfo assemblyInfo)
            {
                continue;
            }

            (FileInfo? sourceFile, FileInfo? destinationFile) = _deploymentComparisonResolver is null
                ? (null, null)
                : _deploymentComparisonResolver(assemblyInfo);

            string sourceText = sourceFile?.Exists == true
                ? sourceFile.LastWriteTime.ToString("g")
                : "-";

            string destinationText = destinationFile?.Exists == true
                ? destinationFile.LastWriteTime.ToString("g")
                : "(new)";

            bool willBeReplaced = sourceFile?.Exists == true
                && (destinationFile?.Exists != true || destinationFile.LastWriteTime < sourceFile.LastWriteTime);

            while (item.SubItems.Count < 4)
            {
                item.SubItems.Add(string.Empty);
            }

            item.SubItems[2].Text = sourceText;
            item.SubItems[3].Text = destinationText;

            item.ForeColor = willBeReplaced ? replacedColor : upToDateColor;
            item.Font = willBeReplaced ? _deploymentComparisonBoldFont : _availableAssembliesListView.Font;
        }

        foreach (ColumnHeader columnItem in _availableAssembliesListView.Columns)
        {
            columnItem.Width = -2;
        }
    }

    private void PathToArtefactsRepo_PathChanged(object? sender, EventArgs e)
    {
        string sourceArtefactsFolder = _pathToArtefactsRepo.FileOrFolderPath;
        if (_settings is not null &&
            !string.Equals(
                _settings.SourceArtefactsFolder,
                sourceArtefactsFolder,
                StringComparison.OrdinalIgnoreCase))
        {
            _settings.SourceArtefactsFolder = sourceArtefactsFolder;
        }

        DeployAvailableRuntimes();
    }
}
