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
        label1 = new Label();
        _pathToArtefactsRepoTextBox = new TextBox();
        _pickPathToArtefactsButton = new Button();
        _runtimeLayout = new TableLayoutPanel();
        label2 = new Label();
        _availableDesktopRuntimesComboBox = new ComboBox();
        _checkForRespectiveRefAssembliesCheckBox = new CheckBox();
        _chkStandardAssemblies = new CheckBox();
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
        _rootLayout.Controls.Add(_runtimeLayout, 0, 1);
        _rootLayout.Controls.Add(label3, 0, 2);
        _rootLayout.Controls.Add(_availableAssembliesListView, 0, 3);
        _rootLayout.Dock = DockStyle.Fill;
        _rootLayout.Location = new Point(0, 0);
        _rootLayout.Name = "_rootLayout";
        _rootLayout.RowCount = 4;
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _rootLayout.Size = new Size(1302, 700);
        _rootLayout.TabIndex = 0;
        // 
        // _pathLayout
        // 
        _pathLayout.ColumnCount = 3;
        _pathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pathLayout.Controls.Add(label1, 0, 0);
        _pathLayout.Controls.Add(_pathToArtefactsRepoTextBox, 1, 0);
        _pathLayout.Controls.Add(_pickPathToArtefactsButton, 2, 0);
        _pathLayout.Dock = DockStyle.Fill;
        _pathLayout.Margin = new Padding(0);
        _pathLayout.Name = "_pathLayout";
        _pathLayout.RowCount = 1;
        _pathLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pathLayout.TabIndex = 0;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Left;
        label1.AutoSize = true;
        label1.Margin = new Padding(3, 0, 12, 0);
        label1.Name = "label1";
        label1.TabIndex = 0;
        label1.Text = "Path to WinForms Repo Artefacts:";
        // 
        // _pathToArtefactsRepoTextBox
        // 
        _pathToArtefactsRepoTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _pathToArtefactsRepoTextBox.Margin = new Padding(3, 4, 3, 4);
        _pathToArtefactsRepoTextBox.Name = "_pathToArtefactsRepoTextBox";
        _pathToArtefactsRepoTextBox.ReadOnly = true;
        _pathToArtefactsRepoTextBox.Size = new Size(756, 37);
        _pathToArtefactsRepoTextBox.TabIndex = 1;
        // 
        // _pickPathToArtefactsButton
        // 
        _pickPathToArtefactsButton.Anchor = AnchorStyles.Left;
        _pickPathToArtefactsButton.Margin = new Padding(3, 4, 3, 4);
        _pickPathToArtefactsButton.Name = "_pickPathToArtefactsButton";
        _pickPathToArtefactsButton.Size = new Size(51, 40);
        _pickPathToArtefactsButton.TabIndex = 2;
        _pickPathToArtefactsButton.Text = "...";
        _pickPathToArtefactsButton.UseVisualStyleBackColor = true;
        _pickPathToArtefactsButton.Click += PickPathToArtefactsButton_Click;
        // 
        // _runtimeLayout
        // 
        _runtimeLayout.ColumnCount = 5;
        _runtimeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _runtimeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _runtimeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _runtimeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _runtimeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _runtimeLayout.Controls.Add(label2, 0, 0);
        _runtimeLayout.Controls.Add(_availableDesktopRuntimesComboBox, 1, 0);
        _runtimeLayout.Controls.Add(_checkForRespectiveRefAssembliesCheckBox, 2, 0);
        _runtimeLayout.Controls.Add(_chkStandardAssemblies, 3, 0);
        _runtimeLayout.Dock = DockStyle.Fill;
        _runtimeLayout.Margin = new Padding(0, 8, 0, 8);
        _runtimeLayout.Name = "_runtimeLayout";
        _runtimeLayout.RowCount = 1;
        _runtimeLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _runtimeLayout.TabIndex = 1;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Left;
        label2.AutoSize = true;
        label2.Margin = new Padding(3, 0, 12, 0);
        label2.Name = "label2";
        label2.TabIndex = 0;
        label2.Text = "Available WinForms artefacts binaries TF:";
        // 
        // _availableDesktopRuntimesComboBox
        // 
        _availableDesktopRuntimesComboBox.Anchor = AnchorStyles.Left;
        _availableDesktopRuntimesComboBox.FormattingEnabled = true;
        _availableDesktopRuntimesComboBox.Margin = new Padding(3, 4, 12, 4);
        _availableDesktopRuntimesComboBox.Name = "_availableDesktopRuntimesComboBox";
        _availableDesktopRuntimesComboBox.Size = new Size(391, 38);
        _availableDesktopRuntimesComboBox.TabIndex = 1;
        // 
        // _checkForRespectiveRefAssembliesCheckBox
        // 
        _checkForRespectiveRefAssembliesCheckBox.Anchor = AnchorStyles.Left;
        _checkForRespectiveRefAssembliesCheckBox.AutoSize = true;
        _checkForRespectiveRefAssembliesCheckBox.Checked = true;
        _checkForRespectiveRefAssembliesCheckBox.CheckState = CheckState.Checked;
        _checkForRespectiveRefAssembliesCheckBox.Margin = new Padding(3, 4, 12, 4);
        _checkForRespectiveRefAssembliesCheckBox.Name = "_checkForRespectiveRefAssembliesCheckBox";
        _checkForRespectiveRefAssembliesCheckBox.TabIndex = 2;
        _checkForRespectiveRefAssembliesCheckBox.Text = "Check for respective REF-Assemblies";
        _checkForRespectiveRefAssembliesCheckBox.UseVisualStyleBackColor = true;
        // 
        // _chkStandardAssemblies
        // 
        _chkStandardAssemblies.Anchor = AnchorStyles.Left;
        _chkStandardAssemblies.AutoSize = true;
        _chkStandardAssemblies.Checked = true;
        _chkStandardAssemblies.CheckState = CheckState.Checked;
        _chkStandardAssemblies.Margin = new Padding(3, 4, 3, 4);
        _chkStandardAssemblies.Name = "_chkStandardAssemblies";
        _chkStandardAssemblies.TabIndex = 3;
        _chkStandardAssemblies.Text = "Include .NET Standard Assemblies";
        _chkStandardAssemblies.UseVisualStyleBackColor = true;
        // 
        // label3
        // 
        label3.Anchor = AnchorStyles.Left;
        label3.AutoSize = true;
        label3.Margin = new Padding(3, 4, 3, 4);
        label3.Name = "label3";
        label3.TabIndex = 2;
        label3.Text = "Available Assemblies:";
        // 
        // _availableAssembliesListView
        // 
        _availableAssembliesListView.Dock = DockStyle.Fill;
        _availableAssembliesListView.Margin = new Padding(3, 4, 3, 4);
        _availableAssembliesListView.Name = "_availableAssembliesListView";
        _availableAssembliesListView.TabIndex = 3;
        _availableAssembliesListView.UseCompatibleStateImageBehavior = false;
        _availableAssembliesListView.View = View.Details;
        // 
        // AssetSelectionControl
        // 
        AutoScaleDimensions = new SizeF(12F, 30F);
        AutoScaleMode = AutoScaleMode.Inherit;
        Controls.Add(_rootLayout);
        Name = "AssetSelectionControl";
        Size = new Size(1302, 700);
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
    private TextBox _pathToArtefactsRepoTextBox;
    private Button _pickPathToArtefactsButton;
    private TableLayoutPanel _runtimeLayout;
    private Label label2;
    private ComboBox _availableDesktopRuntimesComboBox;
    private CheckBox _checkForRespectiveRefAssembliesCheckBox;
    private CheckBox _chkStandardAssemblies;
    private Label label3;
    private ListView _availableAssembliesListView;
}
