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
        groupBox2 = new GroupBox();
        label8 = new Label();
        _pscTemplateCache = new PathShortCutControl();
        label7 = new Label();
        _pscNewSdkRefAssemblies = new PathShortCutControl();
        label6 = new Label();
        _pscNetSdkAssemblies = new PathShortCutControl();
        label5 = new Label();
        _pscWinFormsGitHubRepo = new PathShortCutControl();
        groupBox1 = new GroupBox();
        _netDesktopSdksListView = new ListView();
        _netSdkVersionColumn = new ColumnHeader();
        _netSdkPath = new ColumnHeader();
        groupBox2.SuspendLayout();
        groupBox1.SuspendLayout();
        SuspendLayout();
        // 
        // groupBox2
        // 
        groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(_pscTemplateCache);
        groupBox2.Controls.Add(label7);
        groupBox2.Controls.Add(_pscNewSdkRefAssemblies);
        groupBox2.Controls.Add(label6);
        groupBox2.Controls.Add(_pscNetSdkAssemblies);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(_pscWinFormsGitHubRepo);
        groupBox2.Location = new Point(15, 427);
        groupBox2.Margin = new Padding(3, 4, 3, 4);
        groupBox2.Name = "groupBox2";
        groupBox2.Padding = new Padding(3, 4, 3, 4);
        groupBox2.Size = new Size(1325, 286);
        groupBox2.TabIndex = 5;
        groupBox2.TabStop = false;
        groupBox2.Text = "Path shortcuts";
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(16, 222);
        label8.Name = "label8";
        label8.Size = new Size(163, 30);
        label8.TabIndex = 7;
        label8.Text = "Template cache";
        // 
        // _pscTemplateCache
        // 
        _pscTemplateCache.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _pscTemplateCache.Location = new Point(347, 213);
        _pscTemplateCache.Margin = new Padding(2);
        _pscTemplateCache.Name = "_pscTemplateCache";
        _pscTemplateCache.Size = new Size(956, 50);
        _pscTemplateCache.TabIndex = 6;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(16, 167);
        label7.Name = "label7";
        label7.Size = new Size(253, 30);
        label7.TabIndex = 5;
        label7.Text = ".NET SDK Ref Assemblies";
        // 
        // _pscNewSdkRefAssemblies
        // 
        _pscNewSdkRefAssemblies.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _pscNewSdkRefAssemblies.Location = new Point(347, 159);
        _pscNewSdkRefAssemblies.Margin = new Padding(2);
        _pscNewSdkRefAssemblies.Name = "_pscNewSdkRefAssemblies";
        _pscNewSdkRefAssemblies.Size = new Size(956, 50);
        _pscNewSdkRefAssemblies.TabIndex = 4;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(16, 114);
        label6.Name = "label6";
        label6.Size = new Size(216, 30);
        label6.TabIndex = 3;
        label6.Text = ".NET SDK Assemblies";
        // 
        // _pscNetSdkAssemblies
        // 
        _pscNetSdkAssemblies.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _pscNetSdkAssemblies.Location = new Point(347, 105);
        _pscNetSdkAssemblies.Margin = new Padding(2);
        _pscNetSdkAssemblies.Name = "_pscNetSdkAssemblies";
        _pscNetSdkAssemblies.Size = new Size(956, 50);
        _pscNetSdkAssemblies.TabIndex = 2;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(16, 60);
        label5.Name = "label5";
        label5.Size = new Size(242, 30);
        label5.TabIndex = 1;
        label5.Text = "WinForms Github Repo:";
        // 
        // _winFormsGithubRepo
        // 
        _pscWinFormsGitHubRepo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _pscWinFormsGitHubRepo.Location = new Point(347, 51);
        _pscWinFormsGitHubRepo.Margin = new Padding(2);
        _pscWinFormsGitHubRepo.Name = "_winFormsGithubRepo";
        _pscWinFormsGitHubRepo.Size = new Size(956, 50);
        _pscWinFormsGitHubRepo.TabIndex = 0;
        // 
        // groupBox1
        // 
        groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        groupBox1.Controls.Add(_netDesktopSdksListView);
        groupBox1.Location = new Point(15, 13);
        groupBox1.Margin = new Padding(3, 4, 3, 4);
        groupBox1.Name = "groupBox1";
        groupBox1.Padding = new Padding(3, 4, 3, 4);
        groupBox1.Size = new Size(1325, 406);
        groupBox1.TabIndex = 4;
        groupBox1.TabStop = false;
        groupBox1.Text = ".NET Desktop SDKs:";
        // 
        // _netDesktopSdksListView
        // 
        _netDesktopSdksListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _netDesktopSdksListView.Columns.AddRange(new ColumnHeader[] { _netSdkVersionColumn, _netSdkPath });
        _netDesktopSdksListView.FullRowSelect = true;
        _netDesktopSdksListView.GridLines = true;
        _netDesktopSdksListView.Location = new Point(16, 38);
        _netDesktopSdksListView.Margin = new Padding(3, 4, 3, 4);
        _netDesktopSdksListView.Name = "_netDesktopSdksListView";
        _netDesktopSdksListView.Size = new Size(1287, 344);
        _netDesktopSdksListView.TabIndex = 2;
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
        // OverView
        // 
        AutoScaleDimensions = new SizeF(12F, 30F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
        Name = "OverView";
        Size = new Size(1355, 738);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private GroupBox groupBox2;
    private Label label8;
    private PathShortCutControl _pscTemplateCache;
    private Label label7;
    private PathShortCutControl _pscNewSdkRefAssemblies;
    private Label label6;
    private PathShortCutControl _pscNetSdkAssemblies;
    private Label label5;
    private PathShortCutControl _pscWinFormsGitHubRepo;
    private GroupBox groupBox1;
    private ListView _netDesktopSdksListView;
    private ColumnHeader _netSdkVersionColumn;
    private ColumnHeader _netSdkPath;
}
