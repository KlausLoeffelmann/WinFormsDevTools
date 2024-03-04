namespace WinFormsDevTools
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            overviewToolStripMenuItem = new ToolStripMenuItem();
            deployRuntimeBinariesToolStripMenuItem = new ToolStripMenuItem();
            _overviewPanel = new Panel();
            _grpPathShortCuts = new GroupBox();
            label8 = new Label();
            pathShortCutControl4 = new PathShortCutControl();
            label7 = new Label();
            pathShortCutControl3 = new PathShortCutControl();
            label6 = new Label();
            pathShortCutControl2 = new PathShortCutControl();
            label5 = new Label();
            pathShortCutControl1 = new PathShortCutControl();
            _grpNetDesktopSdks = new GroupBox();
            _netDesktopSdksListView = new ListView();
            _netSdkVersionColumn = new ColumnHeader();
            _netSdkPath = new ColumnHeader();
            _deployRuntimePanel = new Panel();
            _dryRunCheckBox = new CheckBox();
            _replaceTargetSDKVersionComboBox = new ComboBox();
            label4 = new Label();
            _copyCommandButton = new Button();
            _checkForRespectiveRefAssembliesCheckBox = new CheckBox();
            _availableAssembliesListView = new ListView();
            label3 = new Label();
            _availableDesktopRuntimesComboBox = new ComboBox();
            label2 = new Label();
            _pickPathToArtefactsButton = new Button();
            _pathToArtefactsRepoTextBox = new TextBox();
            label1 = new Label();
            panel3 = new Panel();
            menuStrip1.SuspendLayout();
            _overviewPanel.SuspendLayout();
            _grpPathShortCuts.SuspendLayout();
            _grpNetDesktopSdks.SuspendLayout();
            _deployRuntimePanel.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = DockStyle.Fill;
            menuStrip1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { overviewToolStripMenuItem, deployRuntimeBinariesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(915, 51);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // overviewToolStripMenuItem
            // 
            overviewToolStripMenuItem.Name = "overviewToolStripMenuItem";
            overviewToolStripMenuItem.Size = new Size(93, 47);
            overviewToolStripMenuItem.Text = "Overview";
            overviewToolStripMenuItem.Click += overviewToolStripMenuItem_Click;
            // 
            // deployRuntimeBinariesToolStripMenuItem
            // 
            deployRuntimeBinariesToolStripMenuItem.Name = "deployRuntimeBinariesToolStripMenuItem";
            deployRuntimeBinariesToolStripMenuItem.Size = new Size(210, 47);
            deployRuntimeBinariesToolStripMenuItem.Text = "Deploy Runtime Binaries";
            deployRuntimeBinariesToolStripMenuItem.Click += deployRuntimeBinariesToolStripMenuItem_Click;
            // 
            // _overviewPanel
            // 
            _overviewPanel.BorderStyle = BorderStyle.FixedSingle;
            _overviewPanel.Controls.Add(_grpPathShortCuts);
            _overviewPanel.Controls.Add(_grpNetDesktopSdks);
            _overviewPanel.Location = new Point(940, 65);
            _overviewPanel.Name = "_overviewPanel";
            _overviewPanel.Size = new Size(978, 850);
            _overviewPanel.TabIndex = 2;
            // 
            // _grpPathShortCuts
            // 
            _grpPathShortCuts.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grpPathShortCuts.Controls.Add(label8);
            _grpPathShortCuts.Controls.Add(pathShortCutControl4);
            _grpPathShortCuts.Controls.Add(label7);
            _grpPathShortCuts.Controls.Add(pathShortCutControl3);
            _grpPathShortCuts.Controls.Add(label6);
            _grpPathShortCuts.Controls.Add(pathShortCutControl2);
            _grpPathShortCuts.Controls.Add(label5);
            _grpPathShortCuts.Controls.Add(pathShortCutControl1);
            _grpPathShortCuts.Location = new Point(17, 542);
            _grpPathShortCuts.Margin = new Padding(2);
            _grpPathShortCuts.Name = "_grpPathShortCuts";
            _grpPathShortCuts.Padding = new Padding(2);
            _grpPathShortCuts.Size = new Size(940, 270);
            _grpPathShortCuts.TabIndex = 5;
            _grpPathShortCuts.TabStop = false;
            _grpPathShortCuts.Text = "Path shortcuts";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(13, 182);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(128, 23);
            label8.TabIndex = 7;
            label8.Text = "Template cache";
            // 
            // pathShortCutControl4
            // 
            pathShortCutControl4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pathShortCutControl4.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pathShortCutControl4.Location = new Point(236, 175);
            pathShortCutControl4.Margin = new Padding(2);
            pathShortCutControl4.Name = "pathShortCutControl4";
            pathShortCutControl4.Size = new Size(686, 39);
            pathShortCutControl4.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(13, 137);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(198, 23);
            label7.TabIndex = 5;
            label7.Text = ".NET SDK Ref Assemblies";
            // 
            // pathShortCutControl3
            // 
            pathShortCutControl3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pathShortCutControl3.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pathShortCutControl3.Location = new Point(236, 130);
            pathShortCutControl3.Margin = new Padding(2);
            pathShortCutControl3.Name = "pathShortCutControl3";
            pathShortCutControl3.Size = new Size(686, 39);
            pathShortCutControl3.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 90);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(169, 23);
            label6.TabIndex = 3;
            label6.Text = ".NET SDK Assemblies";
            // 
            // pathShortCutControl2
            // 
            pathShortCutControl2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pathShortCutControl2.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pathShortCutControl2.Location = new Point(236, 83);
            pathShortCutControl2.Margin = new Padding(2);
            pathShortCutControl2.Name = "pathShortCutControl2";
            pathShortCutControl2.Size = new Size(686, 39);
            pathShortCutControl2.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 45);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(191, 23);
            label5.TabIndex = 1;
            label5.Text = "WinForms Github Repo:";
            // 
            // pathShortCutControl1
            // 
            pathShortCutControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pathShortCutControl1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pathShortCutControl1.Location = new Point(236, 38);
            pathShortCutControl1.Margin = new Padding(2);
            pathShortCutControl1.Name = "pathShortCutControl1";
            pathShortCutControl1.Size = new Size(686, 39);
            pathShortCutControl1.TabIndex = 0;
            // 
            // _grpNetDesktopSdks
            // 
            _grpNetDesktopSdks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grpNetDesktopSdks.Controls.Add(_netDesktopSdksListView);
            _grpNetDesktopSdks.Location = new Point(17, 14);
            _grpNetDesktopSdks.Margin = new Padding(2);
            _grpNetDesktopSdks.Name = "_grpNetDesktopSdks";
            _grpNetDesktopSdks.Padding = new Padding(2);
            _grpNetDesktopSdks.Size = new Size(940, 503);
            _grpNetDesktopSdks.TabIndex = 4;
            _grpNetDesktopSdks.TabStop = false;
            _grpNetDesktopSdks.Text = ".NET Desktop SDKs:";
            // 
            // _netDesktopSdksListView
            // 
            _netDesktopSdksListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _netDesktopSdksListView.BorderStyle = BorderStyle.FixedSingle;
            _netDesktopSdksListView.Columns.AddRange(new ColumnHeader[] { _netSdkVersionColumn, _netSdkPath });
            _netDesktopSdksListView.FullRowSelect = true;
            _netDesktopSdksListView.Location = new Point(13, 27);
            _netDesktopSdksListView.Margin = new Padding(2);
            _netDesktopSdksListView.Name = "_netDesktopSdksListView";
            _netDesktopSdksListView.Size = new Size(909, 455);
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
            // _deployRuntimePanel
            // 
            _deployRuntimePanel.BorderStyle = BorderStyle.FixedSingle;
            _deployRuntimePanel.Controls.Add(_dryRunCheckBox);
            _deployRuntimePanel.Controls.Add(_replaceTargetSDKVersionComboBox);
            _deployRuntimePanel.Controls.Add(label4);
            _deployRuntimePanel.Controls.Add(_copyCommandButton);
            _deployRuntimePanel.Controls.Add(_checkForRespectiveRefAssembliesCheckBox);
            _deployRuntimePanel.Controls.Add(_availableAssembliesListView);
            _deployRuntimePanel.Controls.Add(label3);
            _deployRuntimePanel.Controls.Add(_availableDesktopRuntimesComboBox);
            _deployRuntimePanel.Controls.Add(label2);
            _deployRuntimePanel.Controls.Add(_pickPathToArtefactsButton);
            _deployRuntimePanel.Controls.Add(_pathToArtefactsRepoTextBox);
            _deployRuntimePanel.Controls.Add(label1);
            _deployRuntimePanel.Location = new Point(13, 65);
            _deployRuntimePanel.Name = "_deployRuntimePanel";
            _deployRuntimePanel.Size = new Size(921, 850);
            _deployRuntimePanel.TabIndex = 3;
            // 
            // _dryRunCheckBox
            // 
            _dryRunCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _dryRunCheckBox.AutoSize = true;
            _dryRunCheckBox.Checked = true;
            _dryRunCheckBox.CheckState = CheckState.Checked;
            _dryRunCheckBox.Location = new Point(642, 794);
            _dryRunCheckBox.Margin = new Padding(2);
            _dryRunCheckBox.Name = "_dryRunCheckBox";
            _dryRunCheckBox.Size = new Size(89, 27);
            _dryRunCheckBox.TabIndex = 22;
            _dryRunCheckBox.Text = "Dry run";
            _dryRunCheckBox.UseVisualStyleBackColor = true;
            // 
            // _replaceTargetSDKVersionComboBox
            // 
            _replaceTargetSDKVersionComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _replaceTargetSDKVersionComboBox.FormattingEnabled = true;
            _replaceTargetSDKVersionComboBox.Location = new Point(323, 792);
            _replaceTargetSDKVersionComboBox.Margin = new Padding(2);
            _replaceTargetSDKVersionComboBox.Name = "_replaceTargetSDKVersionComboBox";
            _replaceTargetSDKVersionComboBox.Size = new Size(303, 31);
            _replaceTargetSDKVersionComboBox.TabIndex = 21;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(20, 794);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(222, 23);
            label4.TabIndex = 20;
            label4.Text = "Replace Target SDK Version:";
            // 
            // _copyCommandButton
            // 
            _copyCommandButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _copyCommandButton.Location = new Point(745, 787);
            _copyCommandButton.Margin = new Padding(2);
            _copyCommandButton.Name = "_copyCommandButton";
            _copyCommandButton.Size = new Size(151, 38);
            _copyCommandButton.TabIndex = 23;
            _copyCommandButton.Text = "Copy...";
            _copyCommandButton.UseVisualStyleBackColor = true;
            _copyCommandButton.Click += CopyCommandButton_Click;
            // 
            // _checkForRespectiveRefAssembliesCheckBox
            // 
            _checkForRespectiveRefAssembliesCheckBox.AutoSize = true;
            _checkForRespectiveRefAssembliesCheckBox.Checked = true;
            _checkForRespectiveRefAssembliesCheckBox.CheckState = CheckState.Checked;
            _checkForRespectiveRefAssembliesCheckBox.Location = new Point(332, 143);
            _checkForRespectiveRefAssembliesCheckBox.Margin = new Padding(2);
            _checkForRespectiveRefAssembliesCheckBox.Name = "_checkForRespectiveRefAssembliesCheckBox";
            _checkForRespectiveRefAssembliesCheckBox.Size = new Size(307, 27);
            _checkForRespectiveRefAssembliesCheckBox.TabIndex = 18;
            _checkForRespectiveRefAssembliesCheckBox.Text = "Check for respective REF-Assemblies";
            _checkForRespectiveRefAssembliesCheckBox.UseVisualStyleBackColor = true;
            // 
            // _availableAssembliesListView
            // 
            _availableAssembliesListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _availableAssembliesListView.Location = new Point(20, 175);
            _availableAssembliesListView.Margin = new Padding(2);
            _availableAssembliesListView.Name = "_availableAssembliesListView";
            _availableAssembliesListView.Size = new Size(878, 597);
            _availableAssembliesListView.TabIndex = 19;
            _availableAssembliesListView.UseCompatibleStateImageBehavior = false;
            _availableAssembliesListView.View = View.Details;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 144);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(170, 23);
            label3.TabIndex = 17;
            label3.Text = "Available Assemblies:";
            // 
            // _availableDesktopRuntimesComboBox
            // 
            _availableDesktopRuntimesComboBox.FormattingEnabled = true;
            _availableDesktopRuntimesComboBox.Location = new Point(343, 82);
            _availableDesktopRuntimesComboBox.Margin = new Padding(2);
            _availableDesktopRuntimesComboBox.Name = "_availableDesktopRuntimesComboBox";
            _availableDesktopRuntimesComboBox.Size = new Size(283, 31);
            _availableDesktopRuntimesComboBox.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 84);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(319, 23);
            label2.TabIndex = 15;
            label2.Text = "Available WinForms artefacts binaries TF:";
            // 
            // _pickPathToArtefactsButton
            // 
            _pickPathToArtefactsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _pickPathToArtefactsButton.Location = new Point(854, 23);
            _pickPathToArtefactsButton.Margin = new Padding(2);
            _pickPathToArtefactsButton.Name = "_pickPathToArtefactsButton";
            _pickPathToArtefactsButton.Size = new Size(38, 31);
            _pickPathToArtefactsButton.TabIndex = 14;
            _pickPathToArtefactsButton.Text = "...";
            _pickPathToArtefactsButton.UseVisualStyleBackColor = true;
            _pickPathToArtefactsButton.Click += PickPathToArtefactsButton_Click;
            // 
            // _pathToArtefactsRepoTextBox
            // 
            _pathToArtefactsRepoTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _pathToArtefactsRepoTextBox.Location = new Point(343, 25);
            _pathToArtefactsRepoTextBox.Margin = new Padding(2);
            _pathToArtefactsRepoTextBox.Name = "_pathToArtefactsRepoTextBox";
            _pathToArtefactsRepoTextBox.ReadOnly = true;
            _pathToArtefactsRepoTextBox.Size = new Size(506, 30);
            _pathToArtefactsRepoTextBox.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 27);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(266, 23);
            label1.TabIndex = 12;
            label1.Text = "Path to WinForms Repo Artefacts:";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel3.Controls.Add(menuStrip1);
            panel3.Location = new Point(12, 12);
            panel3.Name = "panel3";
            panel3.Size = new Size(915, 51);
            panel3.TabIndex = 4;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(936, 921);
            Controls.Add(_deployRuntimePanel);
            Controls.Add(_overviewPanel);
            Controls.Add(panel3);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinFormsDevTool";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            _overviewPanel.ResumeLayout(false);
            _grpPathShortCuts.ResumeLayout(false);
            _grpPathShortCuts.PerformLayout();
            _grpNetDesktopSdks.ResumeLayout(false);
            _deployRuntimePanel.ResumeLayout(false);
            _deployRuntimePanel.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem overviewToolStripMenuItem;
        private ToolStripMenuItem deployRuntimeBinariesToolStripMenuItem;
        private Panel _overviewPanel;
        private GroupBox _grpPathShortCuts;
        private Label label8;
        private PathShortCutControl pathShortCutControl4;
        private Label label7;
        private PathShortCutControl pathShortCutControl3;
        private Label label6;
        private PathShortCutControl pathShortCutControl2;
        private Label label5;
        private PathShortCutControl pathShortCutControl1;
        private GroupBox _grpNetDesktopSdks;
        private ListView _netDesktopSdksListView;
        private ColumnHeader _netSdkVersionColumn;
        private ColumnHeader _netSdkPath;
        private Panel _deployRuntimePanel;
        private CheckBox _dryRunCheckBox;
        private ComboBox _replaceTargetSDKVersionComboBox;
        private Label label4;
        private Button _copyCommandButton;
        private CheckBox _checkForRespectiveRefAssembliesCheckBox;
        private ListView _availableAssembliesListView;
        private Label label3;
        private ComboBox _availableDesktopRuntimesComboBox;
        private Label label2;
        private Button _pickPathToArtefactsButton;
        private TextBox _pathToArtefactsRepoTextBox;
        private Label label1;
        private Panel panel3;
    }
}