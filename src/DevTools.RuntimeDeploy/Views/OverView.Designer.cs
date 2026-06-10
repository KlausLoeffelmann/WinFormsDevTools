using WarpToolkit.WinForms.Controls;

namespace DevTools.RuntimeDeploy.Views;

partial class OverView
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
        groupBox1 = new GroupBox();
        _netDesktopSdksListView = new ListView();
        _netSdkVersionColumn = new ColumnHeader();
        _netSdkPath = new ColumnHeader();
        groupBox2 = new GroupBox();
        _pathLayout = new TableLayoutPanel();
        label5 = new Label();
        _pscWinFormsGitHubRepo = new FilePathPicker();
        label6 = new Label();
        _pscNetSdkAssemblies = new FilePathPicker();
        label7 = new Label();
        _pscNewSdkRefAssemblies = new FilePathPicker();
        label8 = new Label();
        _pscTemplateCache = new FilePathPicker();
        _rootLayout.SuspendLayout();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        _pathLayout.SuspendLayout();
        SuspendLayout();
        // 
        // _rootLayout
        // 
        _rootLayout.ColumnCount = 1;
        _rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _rootLayout.Controls.Add(groupBox1, 0, 0);
        _rootLayout.Controls.Add(groupBox2, 0, 1);
        _rootLayout.Dock = DockStyle.Fill;
        _rootLayout.Location = new Point(0, 0);
        _rootLayout.Name = "_rootLayout";
        _rootLayout.Padding = new Padding(12);
        _rootLayout.RowCount = 2;
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _rootLayout.Size = new Size(1355, 738);
        _rootLayout.TabIndex = 0;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(_netDesktopSdksListView);
        groupBox1.Dock = DockStyle.Fill;
        groupBox1.Margin = new Padding(3, 4, 3, 4);
        groupBox1.Name = "groupBox1";
        groupBox1.Padding = new Padding(12, 8, 12, 12);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = ".NET Desktop SDKs:";
        // 
        // _netDesktopSdksListView
        // 
        _netDesktopSdksListView.Columns.AddRange(new ColumnHeader[] { _netSdkVersionColumn, _netSdkPath });
        _netDesktopSdksListView.Dock = DockStyle.Fill;
        _netDesktopSdksListView.FullRowSelect = true;
        _netDesktopSdksListView.GridLines = true;
        _netDesktopSdksListView.Location = new Point(12, 35);
        _netDesktopSdksListView.Margin = new Padding(3, 4, 3, 4);
        _netDesktopSdksListView.Name = "_netDesktopSdksListView";
        _netDesktopSdksListView.TabIndex = 0;
        _netDesktopSdksListView.UseCompatibleStateImageBehavior = false;
        _netDesktopSdksListView.View = View.Details;
        // 
        // _netSdkVersionColumn
        // 
        _netSdkVersionColumn.Text = ".NET SDK Version";
        _netSdkVersionColumn.Width = 149;
        // 
        // _netSdkPath
        // 
        _netSdkPath.Text = "Path";
        _netSdkPath.Width = 968;
        // 
        // groupBox2
        // 
        groupBox2.AutoSize = true;
        groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        groupBox2.Controls.Add(_pathLayout);
        groupBox2.Dock = DockStyle.Fill;
        groupBox2.Margin = new Padding(3, 12, 3, 4);
        groupBox2.Name = "groupBox2";
        groupBox2.Padding = new Padding(12, 8, 12, 12);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "Path shortcuts";
        // 
        // _pathLayout
        // 
        _pathLayout.AutoSize = true;
        _pathLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _pathLayout.ColumnCount = 2;
        _pathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pathLayout.Controls.Add(label5, 0, 0);
        _pathLayout.Controls.Add(_pscWinFormsGitHubRepo, 1, 0);
        _pathLayout.Controls.Add(label6, 0, 1);
        _pathLayout.Controls.Add(_pscNetSdkAssemblies, 1, 1);
        _pathLayout.Controls.Add(label7, 0, 2);
        _pathLayout.Controls.Add(_pscNewSdkRefAssemblies, 1, 2);
        _pathLayout.Controls.Add(label8, 0, 3);
        _pathLayout.Controls.Add(_pscTemplateCache, 1, 3);
        _pathLayout.Dock = DockStyle.Fill;
        _pathLayout.Location = new Point(12, 35);
        _pathLayout.Name = "_pathLayout";
        _pathLayout.RowCount = 4;
        _pathLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pathLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pathLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pathLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pathLayout.TabIndex = 0;
        // 
        // label5
        // 
        label5.Anchor = AnchorStyles.Left;
        label5.AutoSize = true;
        label5.Margin = new Padding(3, 0, 12, 0);
        label5.Name = "label5";
        label5.TabIndex = 0;
        label5.Text = "WinForms Github Repo:";
        // 
        // _pscWinFormsGitHubRepo
        // 
        _pscWinFormsGitHubRepo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _pscWinFormsGitHubRepo.Margin = new Padding(2);
        _pscWinFormsGitHubRepo.Name = "_winFormsGithubRepo";
        _pscWinFormsGitHubRepo.PickerMode = FilePathPickerMode.FolderBrowser;
        _pscWinFormsGitHubRepo.ShowPickButton = false;
        _pscWinFormsGitHubRepo.ShowRevealButton = true;
        _pscWinFormsGitHubRepo.Size = new Size(956, 50);
        _pscWinFormsGitHubRepo.TabIndex = 1;
        // 
        // label6
        // 
        label6.Anchor = AnchorStyles.Left;
        label6.AutoSize = true;
        label6.Margin = new Padding(3, 0, 12, 0);
        label6.Name = "label6";
        label6.TabIndex = 2;
        label6.Text = ".NET SDK Assemblies";
        // 
        // _pscNetSdkAssemblies
        // 
        _pscNetSdkAssemblies.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _pscNetSdkAssemblies.Margin = new Padding(2);
        _pscNetSdkAssemblies.Name = "_pscNetSdkAssemblies";
        _pscNetSdkAssemblies.PickerMode = FilePathPickerMode.FolderBrowser;
        _pscNetSdkAssemblies.ShowPickButton = false;
        _pscNetSdkAssemblies.ShowRevealButton = true;
        _pscNetSdkAssemblies.Size = new Size(956, 50);
        _pscNetSdkAssemblies.TabIndex = 3;
        // 
        // label7
        // 
        label7.Anchor = AnchorStyles.Left;
        label7.AutoSize = true;
        label7.Margin = new Padding(3, 0, 12, 0);
        label7.Name = "label7";
        label7.TabIndex = 4;
        label7.Text = ".NET SDK Ref Assemblies";
        // 
        // _pscNewSdkRefAssemblies
        // 
        _pscNewSdkRefAssemblies.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _pscNewSdkRefAssemblies.Margin = new Padding(2);
        _pscNewSdkRefAssemblies.Name = "_pscNewSdkRefAssemblies";
        _pscNewSdkRefAssemblies.PickerMode = FilePathPickerMode.FolderBrowser;
        _pscNewSdkRefAssemblies.ShowPickButton = false;
        _pscNewSdkRefAssemblies.ShowRevealButton = true;
        _pscNewSdkRefAssemblies.Size = new Size(956, 50);
        _pscNewSdkRefAssemblies.TabIndex = 5;
        // 
        // label8
        // 
        label8.Anchor = AnchorStyles.Left;
        label8.AutoSize = true;
        label8.Margin = new Padding(3, 0, 12, 0);
        label8.Name = "label8";
        label8.TabIndex = 6;
        label8.Text = "Template cache";
        // 
        // _pscTemplateCache
        // 
        _pscTemplateCache.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _pscTemplateCache.Margin = new Padding(2);
        _pscTemplateCache.Name = "_pscTemplateCache";
        _pscTemplateCache.PickerMode = FilePathPickerMode.FolderBrowser;
        _pscTemplateCache.ShowPickButton = false;
        _pscTemplateCache.ShowRevealButton = true;
        _pscTemplateCache.Size = new Size(956, 50);
        _pscTemplateCache.TabIndex = 7;
        // 
        // OverView
        // 
        AutoScaleDimensions = new SizeF(12F, 30F);
        AutoScaleMode = AutoScaleMode.Inherit;
        Controls.Add(_rootLayout);
        Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
        Name = "OverView";
        Size = new Size(1355, 738);
        _rootLayout.ResumeLayout(false);
        _rootLayout.PerformLayout();
        groupBox1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        _pathLayout.ResumeLayout(false);
        _pathLayout.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _rootLayout;
    private GroupBox groupBox1;
    private ListView _netDesktopSdksListView;
    private ColumnHeader _netSdkVersionColumn;
    private ColumnHeader _netSdkPath;
    private GroupBox groupBox2;
    private TableLayoutPanel _pathLayout;
    private Label label5;
    private FilePathPicker _pscWinFormsGitHubRepo;
    private Label label6;
    private FilePathPicker _pscNetSdkAssemblies;
    private Label label7;
    private FilePathPicker _pscNewSdkRefAssemblies;
    private Label label8;
    private FilePathPicker _pscTemplateCache;
}
