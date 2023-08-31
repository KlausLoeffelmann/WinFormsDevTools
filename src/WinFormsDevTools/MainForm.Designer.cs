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
            this._mainTabControl = new System.Windows.Forms.TabControl();
            this._overviewTabPage = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pathShortCutControl4 = new WinFormsDevTools.PathShortCutControl();
            this.label7 = new System.Windows.Forms.Label();
            this.pathShortCutControl3 = new WinFormsDevTools.PathShortCutControl();
            this.label6 = new System.Windows.Forms.Label();
            this.pathShortCutControl2 = new WinFormsDevTools.PathShortCutControl();
            this.label5 = new System.Windows.Forms.Label();
            this.pathShortCutControl1 = new WinFormsDevTools.PathShortCutControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._netDesktopSdksListView = new System.Windows.Forms.ListView();
            this._netSdkVersionColumn = new System.Windows.Forms.ColumnHeader();
            this._netSdkPath = new System.Windows.Forms.ColumnHeader();
            this._deployRuntimeBinariesTabPage = new System.Windows.Forms.TabPage();
            this._replaceTargetSDKVersionComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._copyCommandButton = new System.Windows.Forms.Button();
            this._checkForRespectiveRefAssembliesCheckBox = new System.Windows.Forms.CheckBox();
            this._availableAssembliesListView = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this._availableDesktopRuntimesComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._pickPathToArtefactsButton = new System.Windows.Forms.Button();
            this._pathToArtefactsRepoTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._dryRunCheckBox = new System.Windows.Forms.CheckBox();
            this._mainTabControl.SuspendLayout();
            this._overviewTabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this._deployRuntimeBinariesTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainTabControl
            // 
            this._mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mainTabControl.Controls.Add(this._overviewTabPage);
            this._mainTabControl.Controls.Add(this._deployRuntimeBinariesTabPage);
            this._mainTabControl.Location = new System.Drawing.Point(20, 23);
            this._mainTabControl.Name = "_mainTabControl";
            this._mainTabControl.SelectedIndex = 0;
            this._mainTabControl.Size = new System.Drawing.Size(1193, 779);
            this._mainTabControl.TabIndex = 0;
            // 
            // _overviewTabPage
            // 
            this._overviewTabPage.Controls.Add(this.groupBox2);
            this._overviewTabPage.Controls.Add(this.groupBox1);
            this._overviewTabPage.Location = new System.Drawing.Point(4, 34);
            this._overviewTabPage.Name = "_overviewTabPage";
            this._overviewTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._overviewTabPage.Size = new System.Drawing.Size(1185, 741);
            this._overviewTabPage.TabIndex = 0;
            this._overviewTabPage.Text = "Overview";
            this._overviewTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.pathShortCutControl4);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.pathShortCutControl3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.pathShortCutControl2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.pathShortCutControl1);
            this.groupBox2.Location = new System.Drawing.Point(19, 410);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1151, 325);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Path shortcuts";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 199);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 25);
            this.label8.TabIndex = 7;
            this.label8.Text = "Template cache";
            // 
            // pathShortCutControl4
            // 
            this.pathShortCutControl4.Location = new System.Drawing.Point(263, 190);
            this.pathShortCutControl4.Name = "pathShortCutControl4";
            this.pathShortCutControl4.Size = new System.Drawing.Size(873, 43);
            this.pathShortCutControl4.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(209, 25);
            this.label7.TabIndex = 5;
            this.label7.Text = ".NET SDK Ref Assemblies";
            // 
            // pathShortCutControl3
            // 
            this.pathShortCutControl3.Location = new System.Drawing.Point(263, 141);
            this.pathShortCutControl3.Name = "pathShortCutControl3";
            this.pathShortCutControl3.Size = new System.Drawing.Size(873, 43);
            this.pathShortCutControl3.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 25);
            this.label6.TabIndex = 3;
            this.label6.Text = ".NET SDK Assemblies";
            // 
            // pathShortCutControl2
            // 
            this.pathShortCutControl2.Location = new System.Drawing.Point(263, 90);
            this.pathShortCutControl2.Name = "pathShortCutControl2";
            this.pathShortCutControl2.Size = new System.Drawing.Size(873, 43);
            this.pathShortCutControl2.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "WinForms Github Repo:";
            // 
            // pathShortCutControl1
            // 
            this.pathShortCutControl1.Location = new System.Drawing.Point(263, 41);
            this.pathShortCutControl1.Name = "pathShortCutControl1";
            this.pathShortCutControl1.Size = new System.Drawing.Size(873, 43);
            this.pathShortCutControl1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._netDesktopSdksListView);
            this.groupBox1.Location = new System.Drawing.Point(19, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1151, 368);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = ".NET Desktop SDKs:";
            // 
            // _netDesktopSdksListView
            // 
            this._netDesktopSdksListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._netDesktopSdksListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._netSdkVersionColumn,
            this._netSdkPath});
            this._netDesktopSdksListView.FullRowSelect = true;
            this._netDesktopSdksListView.GridLines = true;
            this._netDesktopSdksListView.Location = new System.Drawing.Point(15, 30);
            this._netDesktopSdksListView.Name = "_netDesktopSdksListView";
            this._netDesktopSdksListView.Size = new System.Drawing.Size(1121, 318);
            this._netDesktopSdksListView.TabIndex = 2;
            this._netDesktopSdksListView.UseCompatibleStateImageBehavior = false;
            this._netDesktopSdksListView.View = System.Windows.Forms.View.Details;
            // 
            // _netSdkVersionColumn
            // 
            this._netSdkVersionColumn.Text = ".NET SDK Version";
            this._netSdkVersionColumn.Width = 149;
            // 
            // _netSdkPath
            // 
            this._netSdkPath.Text = "Path";
            this._netSdkPath.Width = 968;
            // 
            // _deployRuntimeBinariesTabPage
            // 
            this._deployRuntimeBinariesTabPage.Controls.Add(this._dryRunCheckBox);
            this._deployRuntimeBinariesTabPage.Controls.Add(this._replaceTargetSDKVersionComboBox);
            this._deployRuntimeBinariesTabPage.Controls.Add(this.label4);
            this._deployRuntimeBinariesTabPage.Controls.Add(this._copyCommandButton);
            this._deployRuntimeBinariesTabPage.Controls.Add(this._checkForRespectiveRefAssembliesCheckBox);
            this._deployRuntimeBinariesTabPage.Controls.Add(this._availableAssembliesListView);
            this._deployRuntimeBinariesTabPage.Controls.Add(this.label3);
            this._deployRuntimeBinariesTabPage.Controls.Add(this._availableDesktopRuntimesComboBox);
            this._deployRuntimeBinariesTabPage.Controls.Add(this.label2);
            this._deployRuntimeBinariesTabPage.Controls.Add(this._pickPathToArtefactsButton);
            this._deployRuntimeBinariesTabPage.Controls.Add(this._pathToArtefactsRepoTextBox);
            this._deployRuntimeBinariesTabPage.Controls.Add(this.label1);
            this._deployRuntimeBinariesTabPage.Location = new System.Drawing.Point(4, 34);
            this._deployRuntimeBinariesTabPage.Name = "_deployRuntimeBinariesTabPage";
            this._deployRuntimeBinariesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._deployRuntimeBinariesTabPage.Size = new System.Drawing.Size(1185, 741);
            this._deployRuntimeBinariesTabPage.TabIndex = 1;
            this._deployRuntimeBinariesTabPage.Text = "Deploy WinForms runtime binaries";
            this._deployRuntimeBinariesTabPage.UseVisualStyleBackColor = true;
            // 
            // _replaceTargetSDKVersionComboBox
            // 
            this._replaceTargetSDKVersionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._replaceTargetSDKVersionComboBox.FormattingEnabled = true;
            this._replaceTargetSDKVersionComboBox.Location = new System.Drawing.Point(344, 690);
            this._replaceTargetSDKVersionComboBox.Name = "_replaceTargetSDKVersionComboBox";
            this._replaceTargetSDKVersionComboBox.Size = new System.Drawing.Size(337, 33);
            this._replaceTargetSDKVersionComboBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 693);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Replace Target SDK Version:";
            // 
            // _copyCommandButton
            // 
            this._copyCommandButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._copyCommandButton.Location = new System.Drawing.Point(999, 685);
            this._copyCommandButton.Name = "_copyCommandButton";
            this._copyCommandButton.Size = new System.Drawing.Size(168, 41);
            this._copyCommandButton.TabIndex = 11;
            this._copyCommandButton.Text = "Copy...";
            this._copyCommandButton.UseVisualStyleBackColor = true;
            this._copyCommandButton.Click += new System.EventHandler(this.CopyCommandButton_Click);
            // 
            // _checkForRespectiveRefAssembliesCheckBox
            // 
            this._checkForRespectiveRefAssembliesCheckBox.AutoSize = true;
            this._checkForRespectiveRefAssembliesCheckBox.Checked = true;
            this._checkForRespectiveRefAssembliesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkForRespectiveRefAssembliesCheckBox.Location = new System.Drawing.Point(354, 163);
            this._checkForRespectiveRefAssembliesCheckBox.Name = "_checkForRespectiveRefAssembliesCheckBox";
            this._checkForRespectiveRefAssembliesCheckBox.Size = new System.Drawing.Size(327, 29);
            this._checkForRespectiveRefAssembliesCheckBox.TabIndex = 6;
            this._checkForRespectiveRefAssembliesCheckBox.Text = "Check for respective REF-Assemblies";
            this._checkForRespectiveRefAssembliesCheckBox.UseVisualStyleBackColor = true;
            // 
            // _availableAssembliesListView
            // 
            this._availableAssembliesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._availableAssembliesListView.Location = new System.Drawing.Point(6, 198);
            this._availableAssembliesListView.Name = "_availableAssembliesListView";
            this._availableAssembliesListView.Size = new System.Drawing.Size(1161, 470);
            this._availableAssembliesListView.TabIndex = 7;
            this._availableAssembliesListView.UseCompatibleStateImageBehavior = false;
            this._availableAssembliesListView.View = System.Windows.Forms.View.Details;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Available Assemblies:";
            // 
            // _availableDesktopRuntimesComboBox
            // 
            this._availableDesktopRuntimesComboBox.FormattingEnabled = true;
            this._availableDesktopRuntimesComboBox.Location = new System.Drawing.Point(354, 96);
            this._availableDesktopRuntimesComboBox.Name = "_availableDesktopRuntimesComboBox";
            this._availableDesktopRuntimesComboBox.Size = new System.Drawing.Size(327, 33);
            this._availableDesktopRuntimesComboBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(334, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Available WinForms artefacts binaries TF:";
            // 
            // _pickPathToArtefactsButton
            // 
            this._pickPathToArtefactsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._pickPathToArtefactsButton.Location = new System.Drawing.Point(1120, 33);
            this._pickPathToArtefactsButton.Name = "_pickPathToArtefactsButton";
            this._pickPathToArtefactsButton.Size = new System.Drawing.Size(43, 34);
            this._pickPathToArtefactsButton.TabIndex = 2;
            this._pickPathToArtefactsButton.Text = "...";
            this._pickPathToArtefactsButton.UseVisualStyleBackColor = true;
            this._pickPathToArtefactsButton.Click += new System.EventHandler(this.PickPathToArtefactsButton_Click);
            // 
            // _pathToArtefactsRepoTextBox
            // 
            this._pathToArtefactsRepoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pathToArtefactsRepoTextBox.Location = new System.Drawing.Point(354, 35);
            this._pathToArtefactsRepoTextBox.Name = "_pathToArtefactsRepoTextBox";
            this._pathToArtefactsRepoTextBox.ReadOnly = true;
            this._pathToArtefactsRepoTextBox.Size = new System.Drawing.Size(760, 31);
            this._pathToArtefactsRepoTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path to WinForms Repo Artefacts:";
            // 
            // _dryRunCheckBox
            // 
            this._dryRunCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._dryRunCheckBox.AutoSize = true;
            this._dryRunCheckBox.Checked = true;
            this._dryRunCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._dryRunCheckBox.Location = new System.Drawing.Point(887, 692);
            this._dryRunCheckBox.Name = "_dryRunCheckBox";
            this._dryRunCheckBox.Size = new System.Drawing.Size(97, 29);
            this._dryRunCheckBox.TabIndex = 10;
            this._dryRunCheckBox.Text = "Dry run";
            this._dryRunCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 825);
            this.Controls.Add(this._mainTabControl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinFormsDevTool";
            this._mainTabControl.ResumeLayout(false);
            this._overviewTabPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this._deployRuntimeBinariesTabPage.ResumeLayout(false);
            this._deployRuntimeBinariesTabPage.PerformLayout();
            this.ResumeLayout(false);

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
    }
}