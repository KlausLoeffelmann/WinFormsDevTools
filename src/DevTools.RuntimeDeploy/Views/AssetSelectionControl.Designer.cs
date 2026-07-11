namespace DevTools.RuntimeDeploy.Views;

partial class AssetSelectionControl
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _rootLayout = new TableLayoutPanel();
        _pathLayout = new TableLayoutPanel();
        _runtimeLayout = new TableLayoutPanel();
        _checkForRespectiveRefAssembliesCheckBox = new CheckBox();
        _chkStandardAssemblies = new CheckBox();
        _availableDesktopRuntimesComboBox = new ComboBox();
        label2 = new Label();
        label1 = new Label();
        _pathToArtefactsRepo = new WarpToolkit.WinForms.Controls.FilePathPicker();
        label3 = new Label();
        _availableAssembliesListView = new ListView();
        _rootLayout.SuspendLayout();
        _pathLayout.SuspendLayout();
        _runtimeLayout.SuspendLayout();
        SuspendLayout();
        // 
        // _rootLayout
        // 
        _rootLayout.ColumnCount = 1;
        _rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _rootLayout.Controls.Add(_pathLayout, 0, 0);
        _rootLayout.Controls.Add(label3, 0, 2);
        _rootLayout.Controls.Add(_availableAssembliesListView, 0, 3);
        _rootLayout.Dock = DockStyle.Fill;
        _rootLayout.Location = new Point(0, 0);
        _rootLayout.Name = "_rootLayout";
        _rootLayout.RowCount = 4;
        _rootLayout.RowStyles.Add(new RowStyle());
        _rootLayout.RowStyles.Add(new RowStyle());
        _rootLayout.RowStyles.Add(new RowStyle());
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _rootLayout.Size = new Size(1239, 700);
        _rootLayout.TabIndex = 0;
        // 
        // _pathLayout
        // 
        _pathLayout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _pathLayout.AutoSize = true;
        _pathLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _pathLayout.ColumnCount = 2;
        _pathLayout.ColumnStyles.Add(new ColumnStyle());
        _pathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pathLayout.Controls.Add(_runtimeLayout, 1, 2);
        _pathLayout.Controls.Add(_availableDesktopRuntimesComboBox, 1, 1);
        _pathLayout.Controls.Add(label2, 0, 1);
        _pathLayout.Controls.Add(label1, 0, 0);
        _pathLayout.Controls.Add(_pathToArtefactsRepo, 1, 0);
        _pathLayout.Location = new Point(0, 0);
        _pathLayout.Margin = new Padding(0);
        _pathLayout.Name = "_pathLayout";
        _pathLayout.RowCount = 3;
        _pathLayout.RowStyles.Add(new RowStyle());
        _pathLayout.RowStyles.Add(new RowStyle());
        _pathLayout.RowStyles.Add(new RowStyle());
        _pathLayout.Size = new Size(1239, 167);
        _pathLayout.TabIndex = 0;
        // 
        // _runtimeLayout
        // 
        _runtimeLayout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _runtimeLayout.AutoSize = true;
        _runtimeLayout.ColumnCount = 2;
        _runtimeLayout.ColumnStyles.Add(new ColumnStyle());
        _runtimeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _runtimeLayout.Controls.Add(_checkForRespectiveRefAssembliesCheckBox, 0, 0);
        _runtimeLayout.Controls.Add(_chkStandardAssemblies, 1, 0);
        _runtimeLayout.Location = new Point(354, 110);
        _runtimeLayout.Margin = new Padding(0, 8, 0, 8);
        _runtimeLayout.Name = "_runtimeLayout";
        _runtimeLayout.RowCount = 1;
        _runtimeLayout.RowStyles.Add(new RowStyle());
        _runtimeLayout.Size = new Size(885, 49);
        _runtimeLayout.TabIndex = 4;
        // 
        // _checkForRespectiveRefAssembliesCheckBox
        // 
        _checkForRespectiveRefAssembliesCheckBox.Anchor = AnchorStyles.Left;
        _checkForRespectiveRefAssembliesCheckBox.AutoSize = true;
        _checkForRespectiveRefAssembliesCheckBox.Checked = true;
        _checkForRespectiveRefAssembliesCheckBox.CheckState = CheckState.Checked;
        _checkForRespectiveRefAssembliesCheckBox.Location = new Point(10, 10);
        _checkForRespectiveRefAssembliesCheckBox.Margin = new Padding(10);
        _checkForRespectiveRefAssembliesCheckBox.Name = "_checkForRespectiveRefAssembliesCheckBox";
        _checkForRespectiveRefAssembliesCheckBox.Size = new Size(327, 29);
        _checkForRespectiveRefAssembliesCheckBox.TabIndex = 0;
        _checkForRespectiveRefAssembliesCheckBox.Text = "Check for respective REF-Assemblies";
        _checkForRespectiveRefAssembliesCheckBox.UseVisualStyleBackColor = true;
        // 
        // _chkStandardAssemblies
        // 
        _chkStandardAssemblies.Anchor = AnchorStyles.Left;
        _chkStandardAssemblies.AutoSize = true;
        _chkStandardAssemblies.Checked = true;
        _chkStandardAssemblies.CheckState = CheckState.Checked;
        _chkStandardAssemblies.Location = new Point(357, 10);
        _chkStandardAssemblies.Margin = new Padding(10);
        _chkStandardAssemblies.Name = "_chkStandardAssemblies";
        _chkStandardAssemblies.Size = new Size(305, 29);
        _chkStandardAssemblies.TabIndex = 1;
        _chkStandardAssemblies.Text = "Include .NET Standard Assemblies";
        _chkStandardAssemblies.UseVisualStyleBackColor = true;
        // 
        // _availableDesktopRuntimesComboBox
        // 
        _availableDesktopRuntimesComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _availableDesktopRuntimesComboBox.FormattingEnabled = true;
        _availableDesktopRuntimesComboBox.Location = new Point(364, 59);
        _availableDesktopRuntimesComboBox.Margin = new Padding(10);
        _availableDesktopRuntimesComboBox.Name = "_availableDesktopRuntimesComboBox";
        _availableDesktopRuntimesComboBox.Size = new Size(865, 33);
        _availableDesktopRuntimesComboBox.TabIndex = 3;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Left;
        label2.AutoSize = true;
        label2.Location = new Point(10, 63);
        label2.Margin = new Padding(10);
        label2.Name = "label2";
        label2.Size = new Size(334, 25);
        label2.TabIndex = 2;
        label2.Text = "Available WinForms artefacts binaries TF:";
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Left;
        label1.AutoSize = true;
        label1.Location = new Point(10, 12);
        label1.Margin = new Padding(10);
        label1.Name = "label1";
        label1.Size = new Size(279, 25);
        label1.TabIndex = 0;
        label1.Text = "Path to WinForms Repo Artefacts:";
        // 
        // _pathToArtefactsRepo
        // 
        _pathToArtefactsRepo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _pathToArtefactsRepo.DialogTitle = "Pick the path to the WinForms artifacts folder:";
        _pathToArtefactsRepo.Location = new Point(364, 10);
        _pathToArtefactsRepo.Margin = new Padding(10);
        _pathToArtefactsRepo.Name = "_pathToArtefactsRepo";
        _pathToArtefactsRepo.PickerMode = WarpToolkit.WinForms.Controls.FilePathPickerMode.FolderBrowser;
        _pathToArtefactsRepo.Size = new Size(865, 29);
        _pathToArtefactsRepo.TabIndex = 1;
        // 
        // label3
        // 
        label3.Anchor = AnchorStyles.Left;
        label3.AutoSize = true;
        label3.Location = new Point(10, 177);
        label3.Margin = new Padding(10);
        label3.Name = "label3";
        label3.Size = new Size(181, 25);
        label3.TabIndex = 1;
        label3.Text = "Available Assemblies:";
        // 
        // _availableAssembliesListView
        // 
        _availableAssembliesListView.Dock = DockStyle.Fill;
        _availableAssembliesListView.Location = new Point(10, 222);
        _availableAssembliesListView.Margin = new Padding(10);
        _availableAssembliesListView.Name = "_availableAssembliesListView";
        _availableAssembliesListView.Size = new Size(1219, 468);
        _availableAssembliesListView.TabIndex = 2;
        _availableAssembliesListView.UseCompatibleStateImageBehavior = false;
        _availableAssembliesListView.View = View.Details;
        // 
        // AssetSelectionControl
        // 
        AutoScaleMode = AutoScaleMode.Inherit;
        AutoSize = true;
        Controls.Add(_rootLayout);
        Name = "AssetSelectionControl";
        Size = new Size(1239, 700);
        _rootLayout.ResumeLayout(false);
        _rootLayout.PerformLayout();
        _pathLayout.ResumeLayout(false);
        _pathLayout.PerformLayout();
        _runtimeLayout.ResumeLayout(false);
        _runtimeLayout.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _rootLayout;
    private TableLayoutPanel _pathLayout;
    private Label label1;
    private Label label2;
    private ComboBox _availableDesktopRuntimesComboBox;
    private Label label3;
    private ListView _availableAssembliesListView;
    private WarpToolkit.WinForms.Controls.FilePathPicker _pathToArtefactsRepo;
    private TableLayoutPanel _runtimeLayout;
    private CheckBox _checkForRespectiveRefAssembliesCheckBox;
    private CheckBox _chkStandardAssemblies;
}
