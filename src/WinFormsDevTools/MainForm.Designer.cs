﻿namespace WinFormsDevTools
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
            _mainTabControl = new TabControl();
            _overviewTabPage = new TabPage();
            groupBox2 = new GroupBox();
            label8 = new Label();
            pathShortCutControl4 = new PathShortCutControl();
            label7 = new Label();
            pathShortCutControl3 = new PathShortCutControl();
            label6 = new Label();
            pathShortCutControl2 = new PathShortCutControl();
            label5 = new Label();
            pathShortCutControl1 = new PathShortCutControl();
            groupBox1 = new GroupBox();
            _netDesktopSdksListView = new ListView();
            _netSdkVersionColumn = new ColumnHeader();
            _netSdkPath = new ColumnHeader();
            _deployRuntimeBinariesTabPage = new TabPage();
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
            _chkStandardAssemblies = new CheckBox();
            _mainTabControl.SuspendLayout();
            _overviewTabPage.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            _deployRuntimeBinariesTabPage.SuspendLayout();
            SuspendLayout();
            // 
            // _mainTabControl
            // 
            _mainTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _mainTabControl.Controls.Add(_overviewTabPage);
            _mainTabControl.Controls.Add(_deployRuntimeBinariesTabPage);
            _mainTabControl.Location = new Point(14, 14);
            _mainTabControl.Margin = new Padding(2, 2, 2, 2);
            _mainTabControl.Name = "_mainTabControl";
            _mainTabControl.SelectedIndex = 0;
            _mainTabControl.Size = new Size(835, 467);
            _mainTabControl.TabIndex = 0;
            // 
            // _overviewTabPage
            // 
            _overviewTabPage.Controls.Add(groupBox2);
            _overviewTabPage.Controls.Add(groupBox1);
            _overviewTabPage.Location = new Point(4, 24);
            _overviewTabPage.Margin = new Padding(2, 2, 2, 2);
            _overviewTabPage.Name = "_overviewTabPage";
            _overviewTabPage.Padding = new Padding(2, 2, 2, 2);
            _overviewTabPage.Size = new Size(827, 439);
            _overviewTabPage.TabIndex = 0;
            _overviewTabPage.Text = "Overview";
            _overviewTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(pathShortCutControl4);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(pathShortCutControl3);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(pathShortCutControl2);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(pathShortCutControl1);
            groupBox2.Location = new Point(13, 246);
            groupBox2.Margin = new Padding(2, 2, 2, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2, 2, 2, 2);
            groupBox2.Size = new Size(806, 195);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Path shortcuts";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 119);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(89, 15);
            label8.TabIndex = 7;
            label8.Text = "Template cache";
            // 
            // pathShortCutControl4
            // 
            pathShortCutControl4.Location = new Point(184, 114);
            pathShortCutControl4.Margin = new Padding(1, 1, 1, 1);
            pathShortCutControl4.Name = "pathShortCutControl4";
            pathShortCutControl4.Size = new Size(611, 26);
            pathShortCutControl4.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 90);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(137, 15);
            label7.TabIndex = 5;
            label7.Text = ".NET SDK Ref Assemblies";
            // 
            // pathShortCutControl3
            // 
            pathShortCutControl3.Location = new Point(184, 85);
            pathShortCutControl3.Margin = new Padding(1, 1, 1, 1);
            pathShortCutControl3.Name = "pathShortCutControl3";
            pathShortCutControl3.Size = new Size(611, 26);
            pathShortCutControl3.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 59);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(117, 15);
            label6.TabIndex = 3;
            label6.Text = ".NET SDK Assemblies";
            // 
            // pathShortCutControl2
            // 
            pathShortCutControl2.Location = new Point(184, 54);
            pathShortCutControl2.Margin = new Padding(1, 1, 1, 1);
            pathShortCutControl2.Name = "pathShortCutControl2";
            pathShortCutControl2.Size = new Size(611, 26);
            pathShortCutControl2.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 30);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(133, 15);
            label5.TabIndex = 1;
            label5.Text = "WinForms Github Repo:";
            // 
            // pathShortCutControl1
            // 
            pathShortCutControl1.Location = new Point(184, 25);
            pathShortCutControl1.Margin = new Padding(1, 1, 1, 1);
            pathShortCutControl1.Name = "pathShortCutControl1";
            pathShortCutControl1.Size = new Size(611, 26);
            pathShortCutControl1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(_netDesktopSdksListView);
            groupBox1.Location = new Point(13, 9);
            groupBox1.Margin = new Padding(2, 2, 2, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 2, 2, 2);
            groupBox1.Size = new Size(806, 221);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = ".NET Desktop SDKs:";
            // 
            // _netDesktopSdksListView
            // 
            _netDesktopSdksListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _netDesktopSdksListView.Columns.AddRange(new ColumnHeader[] { _netSdkVersionColumn, _netSdkPath });
            _netDesktopSdksListView.FullRowSelect = true;
            _netDesktopSdksListView.GridLines = true;
            _netDesktopSdksListView.Location = new Point(10, 18);
            _netDesktopSdksListView.Margin = new Padding(2, 2, 2, 2);
            _netDesktopSdksListView.Name = "_netDesktopSdksListView";
            _netDesktopSdksListView.Size = new Size(786, 192);
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
            // _deployRuntimeBinariesTabPage
            // 
            _deployRuntimeBinariesTabPage.Controls.Add(_chkStandardAssemblies);
            _deployRuntimeBinariesTabPage.Controls.Add(_dryRunCheckBox);
            _deployRuntimeBinariesTabPage.Controls.Add(_replaceTargetSDKVersionComboBox);
            _deployRuntimeBinariesTabPage.Controls.Add(label4);
            _deployRuntimeBinariesTabPage.Controls.Add(_copyCommandButton);
            _deployRuntimeBinariesTabPage.Controls.Add(_checkForRespectiveRefAssembliesCheckBox);
            _deployRuntimeBinariesTabPage.Controls.Add(_availableAssembliesListView);
            _deployRuntimeBinariesTabPage.Controls.Add(label3);
            _deployRuntimeBinariesTabPage.Controls.Add(_availableDesktopRuntimesComboBox);
            _deployRuntimeBinariesTabPage.Controls.Add(label2);
            _deployRuntimeBinariesTabPage.Controls.Add(_pickPathToArtefactsButton);
            _deployRuntimeBinariesTabPage.Controls.Add(_pathToArtefactsRepoTextBox);
            _deployRuntimeBinariesTabPage.Controls.Add(label1);
            _deployRuntimeBinariesTabPage.Location = new Point(4, 24);
            _deployRuntimeBinariesTabPage.Margin = new Padding(2, 2, 2, 2);
            _deployRuntimeBinariesTabPage.Name = "_deployRuntimeBinariesTabPage";
            _deployRuntimeBinariesTabPage.Padding = new Padding(2, 2, 2, 2);
            _deployRuntimeBinariesTabPage.Size = new Size(827, 439);
            _deployRuntimeBinariesTabPage.TabIndex = 1;
            _deployRuntimeBinariesTabPage.Text = "Deploy WinForms runtime binaries";
            _deployRuntimeBinariesTabPage.UseVisualStyleBackColor = true;
            // 
            // _dryRunCheckBox
            // 
            _dryRunCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _dryRunCheckBox.AutoSize = true;
            _dryRunCheckBox.Checked = true;
            _dryRunCheckBox.CheckState = CheckState.Checked;
            _dryRunCheckBox.Location = new Point(624, 413);
            _dryRunCheckBox.Margin = new Padding(2, 2, 2, 2);
            _dryRunCheckBox.Name = "_dryRunCheckBox";
            _dryRunCheckBox.Size = new Size(65, 19);
            _dryRunCheckBox.TabIndex = 10;
            _dryRunCheckBox.Text = "Dry run";
            _dryRunCheckBox.UseVisualStyleBackColor = true;
            // 
            // _replaceTargetSDKVersionComboBox
            // 
            _replaceTargetSDKVersionComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _replaceTargetSDKVersionComboBox.FormattingEnabled = true;
            _replaceTargetSDKVersionComboBox.Location = new Point(241, 414);
            _replaceTargetSDKVersionComboBox.Margin = new Padding(2, 2, 2, 2);
            _replaceTargetSDKVersionComboBox.Name = "_replaceTargetSDKVersionComboBox";
            _replaceTargetSDKVersionComboBox.Size = new Size(237, 23);
            _replaceTargetSDKVersionComboBox.TabIndex = 9;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(4, 416);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(151, 15);
            label4.TabIndex = 8;
            label4.Text = "Replace Target SDK Version:";
            // 
            // _copyCommandButton
            // 
            _copyCommandButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _copyCommandButton.Location = new Point(699, 411);
            _copyCommandButton.Margin = new Padding(2, 2, 2, 2);
            _copyCommandButton.Name = "_copyCommandButton";
            _copyCommandButton.Size = new Size(118, 25);
            _copyCommandButton.TabIndex = 11;
            _copyCommandButton.Text = "Copy...";
            _copyCommandButton.UseVisualStyleBackColor = true;
            _copyCommandButton.Click += CopyCommandButton_Click;
            // 
            // _checkForRespectiveRefAssembliesCheckBox
            // 
            _checkForRespectiveRefAssembliesCheckBox.AutoSize = true;
            _checkForRespectiveRefAssembliesCheckBox.Checked = true;
            _checkForRespectiveRefAssembliesCheckBox.CheckState = CheckState.Checked;
            _checkForRespectiveRefAssembliesCheckBox.Location = new Point(248, 98);
            _checkForRespectiveRefAssembliesCheckBox.Margin = new Padding(2, 2, 2, 2);
            _checkForRespectiveRefAssembliesCheckBox.Name = "_checkForRespectiveRefAssembliesCheckBox";
            _checkForRespectiveRefAssembliesCheckBox.Size = new Size(219, 19);
            _checkForRespectiveRefAssembliesCheckBox.TabIndex = 6;
            _checkForRespectiveRefAssembliesCheckBox.Text = "Check for respective REF-Assemblies";
            _checkForRespectiveRefAssembliesCheckBox.UseVisualStyleBackColor = true;
            // 
            // _availableAssembliesListView
            // 
            _availableAssembliesListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _availableAssembliesListView.Location = new Point(4, 121);
            _availableAssembliesListView.Margin = new Padding(2, 2, 2, 2);
            _availableAssembliesListView.Name = "_availableAssembliesListView";
            _availableAssembliesListView.Size = new Size(814, 282);
            _availableAssembliesListView.TabIndex = 7;
            _availableAssembliesListView.UseCompatibleStateImageBehavior = false;
            _availableAssembliesListView.View = View.Details;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 98);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(120, 15);
            label3.TabIndex = 5;
            label3.Text = "Available Assemblies:";
            // 
            // _availableDesktopRuntimesComboBox
            // 
            _availableDesktopRuntimesComboBox.FormattingEnabled = true;
            _availableDesktopRuntimesComboBox.Location = new Point(248, 58);
            _availableDesktopRuntimesComboBox.Margin = new Padding(2, 2, 2, 2);
            _availableDesktopRuntimesComboBox.Name = "_availableDesktopRuntimesComboBox";
            _availableDesktopRuntimesComboBox.Size = new Size(230, 23);
            _availableDesktopRuntimesComboBox.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 59);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(222, 15);
            label2.TabIndex = 3;
            label2.Text = "Available WinForms artefacts binaries TF:";
            // 
            // _pickPathToArtefactsButton
            // 
            _pickPathToArtefactsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _pickPathToArtefactsButton.Location = new Point(784, 20);
            _pickPathToArtefactsButton.Margin = new Padding(2, 2, 2, 2);
            _pickPathToArtefactsButton.Name = "_pickPathToArtefactsButton";
            _pickPathToArtefactsButton.Size = new Size(30, 20);
            _pickPathToArtefactsButton.TabIndex = 2;
            _pickPathToArtefactsButton.Text = "...";
            _pickPathToArtefactsButton.UseVisualStyleBackColor = true;
            _pickPathToArtefactsButton.Click += PickPathToArtefactsButton_Click;
            // 
            // _pathToArtefactsRepoTextBox
            // 
            _pathToArtefactsRepoTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _pathToArtefactsRepoTextBox.Location = new Point(248, 21);
            _pathToArtefactsRepoTextBox.Margin = new Padding(2, 2, 2, 2);
            _pathToArtefactsRepoTextBox.Name = "_pathToArtefactsRepoTextBox";
            _pathToArtefactsRepoTextBox.ReadOnly = true;
            _pathToArtefactsRepoTextBox.Size = new Size(533, 23);
            _pathToArtefactsRepoTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 23);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(185, 15);
            label1.TabIndex = 0;
            label1.Text = "Path to WinForms Repo Artefacts:";
            // 
            // _chkStandardAssemblies
            // 
            _chkStandardAssemblies.AutoSize = true;
            _chkStandardAssemblies.Checked = true;
            _chkStandardAssemblies.CheckState = CheckState.Checked;
            _chkStandardAssemblies.Location = new Point(491, 98);
            _chkStandardAssemblies.Margin = new Padding(2);
            _chkStandardAssemblies.Name = "_chkStandardAssemblies";
            _chkStandardAssemblies.Size = new Size(204, 19);
            _chkStandardAssemblies.TabIndex = 12;
            _chkStandardAssemblies.Text = "Include .NET Standard Assemblies";
            _chkStandardAssemblies.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(864, 495);
            Controls.Add(_mainTabControl);
            Margin = new Padding(2, 2, 2, 2);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinFormsDevTool";
            _mainTabControl.ResumeLayout(false);
            _overviewTabPage.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            _deployRuntimeBinariesTabPage.ResumeLayout(false);
            _deployRuntimeBinariesTabPage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl _mainTabControl;
        private TabPage _overviewTabPage;
        private TabPage _deployRuntimeBinariesTabPage;
        private Button _pickPathToArtefactsButton;
        private TextBox _pathToArtefactsRepoTextBox;
        private Label label1;
        private CheckBox _checkForRespectiveRefAssembliesCheckBox;
        private ListView _availableAssembliesListView;
        private Label label3;
        private ComboBox _availableDesktopRuntimesComboBox;
        private Label label2;
        private ComboBox _replaceTargetSDKVersionComboBox;
        private Label label4;
        private Button _copyCommandButton;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private ListView _netDesktopSdksListView;
        private ColumnHeader _netSdkVersionColumn;
        private ColumnHeader _netSdkPath;
        private Label label5;
        private PathShortCutControl pathShortCutControl1;
        private Label label7;
        private PathShortCutControl pathShortCutControl3;
        private Label label6;
        private PathShortCutControl pathShortCutControl2;
        private Label label8;
        private PathShortCutControl pathShortCutControl4;
        private CheckBox _dryRunCheckBox;
        private CheckBox _chkStandardAssemblies;
    }
}