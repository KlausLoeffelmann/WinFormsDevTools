namespace WinFormsDevTools.Views
{
    partial class DeployRuntimeView
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
            _chkStandardAssemblies = new CheckBox();
            _dryRunCheckBox = new CheckBox();
            _replaceTargetSDKVersionComboBox = new ComboBox();
            label4 = new Label();
            _copyCommandButton = new Button();
            _checkForRespectiveRefAssembliesCheckBox = new CheckBox();
            _availableAssembliesListView = new ListView();
            label3 = new Label();
            _availableDesktopRuntimesComboBox = new ComboBox();
            label2 = new Label();
            _pathToArtefactsRepoTextBox = new TextBox();
            label1 = new Label();
            _pickPathToArtefactsButton = new Button();
            SuspendLayout();
            // 
            // _chkStandardAssemblies
            // 
            _chkStandardAssemblies.AutoSize = true;
            _chkStandardAssemblies.Checked = true;
            _chkStandardAssemblies.CheckState = CheckState.Checked;
            _chkStandardAssemblies.Location = new Point(901, 164);
            _chkStandardAssemblies.Margin = new Padding(3, 4, 3, 4);
            _chkStandardAssemblies.Name = "_chkStandardAssemblies";
            _chkStandardAssemblies.Size = new Size(364, 34);
            _chkStandardAssemblies.TabIndex = 24;
            _chkStandardAssemblies.Text = "Include .NET Standard Assemblies";
            _chkStandardAssemblies.UseVisualStyleBackColor = true;
            // 
            // _dryRunCheckBox
            // 
            _dryRunCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _dryRunCheckBox.AutoSize = true;
            _dryRunCheckBox.Checked = true;
            _dryRunCheckBox.CheckState = CheckState.Checked;
            _dryRunCheckBox.Location = new Point(964, 708);
            _dryRunCheckBox.Margin = new Padding(3, 4, 3, 4);
            _dryRunCheckBox.Name = "_dryRunCheckBox";
            _dryRunCheckBox.Size = new Size(111, 34);
            _dryRunCheckBox.TabIndex = 22;
            _dryRunCheckBox.Text = "Dry run";
            _dryRunCheckBox.UseVisualStyleBackColor = true;
            // 
            // _replaceTargetSDKVersionComboBox
            // 
            _replaceTargetSDKVersionComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _replaceTargetSDKVersionComboBox.FormattingEnabled = true;
            _replaceTargetSDKVersionComboBox.Location = new Point(423, 705);
            _replaceTargetSDKVersionComboBox.Margin = new Padding(3, 4, 3, 4);
            _replaceTargetSDKVersionComboBox.Name = "_replaceTargetSDKVersionComboBox";
            _replaceTargetSDKVersionComboBox.Size = new Size(403, 38);
            _replaceTargetSDKVersionComboBox.TabIndex = 21;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(19, 710);
            label4.Name = "label4";
            label4.Size = new Size(284, 30);
            label4.TabIndex = 20;
            label4.Text = "Replace Target SDK Version:";
            // 
            // _copyCommandButton
            // 
            _copyCommandButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _copyCommandButton.Location = new Point(1092, 699);
            _copyCommandButton.Margin = new Padding(3, 4, 3, 4);
            _copyCommandButton.Name = "_copyCommandButton";
            _copyCommandButton.Size = new Size(202, 50);
            _copyCommandButton.TabIndex = 23;
            _copyCommandButton.Text = "Copy...";
            _copyCommandButton.UseVisualStyleBackColor = true;
            // 
            // _checkForRespectiveRefAssembliesCheckBox
            // 
            _checkForRespectiveRefAssembliesCheckBox.AutoSize = true;
            _checkForRespectiveRefAssembliesCheckBox.Checked = true;
            _checkForRespectiveRefAssembliesCheckBox.CheckState = CheckState.Checked;
            _checkForRespectiveRefAssembliesCheckBox.Location = new Point(464, 164);
            _checkForRespectiveRefAssembliesCheckBox.Margin = new Padding(3, 4, 3, 4);
            _checkForRespectiveRefAssembliesCheckBox.Name = "_checkForRespectiveRefAssembliesCheckBox";
            _checkForRespectiveRefAssembliesCheckBox.Size = new Size(394, 34);
            _checkForRespectiveRefAssembliesCheckBox.TabIndex = 18;
            _checkForRespectiveRefAssembliesCheckBox.Text = "Check for respective REF-Assemblies";
            _checkForRespectiveRefAssembliesCheckBox.UseVisualStyleBackColor = true;
            // 
            // _availableAssembliesListView
            // 
            _availableAssembliesListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _availableAssembliesListView.Location = new Point(18, 199);
            _availableAssembliesListView.Margin = new Padding(3, 4, 3, 4);
            _availableAssembliesListView.Name = "_availableAssembliesListView";
            _availableAssembliesListView.Size = new Size(1276, 473);
            _availableAssembliesListView.TabIndex = 19;
            _availableAssembliesListView.UseCompatibleStateImageBehavior = false;
            _availableAssembliesListView.View = View.Details;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 165);
            label3.Name = "label3";
            label3.Size = new Size(218, 30);
            label3.TabIndex = 17;
            label3.Text = "Available Assemblies:";
            // 
            // _availableDesktopRuntimesComboBox
            // 
            _availableDesktopRuntimesComboBox.FormattingEnabled = true;
            _availableDesktopRuntimesComboBox.Location = new Point(464, 66);
            _availableDesktopRuntimesComboBox.Margin = new Padding(3, 4, 3, 4);
            _availableDesktopRuntimesComboBox.Name = "_availableDesktopRuntimesComboBox";
            _availableDesktopRuntimesComboBox.Size = new Size(391, 38);
            _availableDesktopRuntimesComboBox.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 69);
            label2.Name = "label2";
            label2.Size = new Size(407, 30);
            label2.TabIndex = 15;
            label2.Text = "Available WinForms artefacts binaries TF:";
            // 
            // _pathToArtefactsRepoTextBox
            // 
            _pathToArtefactsRepoTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _pathToArtefactsRepoTextBox.Location = new Point(464, 11);
            _pathToArtefactsRepoTextBox.Margin = new Padding(3, 4, 3, 4);
            _pathToArtefactsRepoTextBox.Name = "_pathToArtefactsRepoTextBox";
            _pathToArtefactsRepoTextBox.ReadOnly = true;
            _pathToArtefactsRepoTextBox.Size = new Size(765, 37);
            _pathToArtefactsRepoTextBox.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 15);
            label1.Name = "label1";
            label1.Size = new Size(336, 30);
            label1.TabIndex = 13;
            label1.Text = "Path to WinForms Repo Artefacts:";
            // 
            // _pickPathToArtefactsButton
            // 
            _pickPathToArtefactsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _pickPathToArtefactsButton.Location = new Point(1243, 11);
            _pickPathToArtefactsButton.Margin = new Padding(3, 4, 3, 4);
            _pickPathToArtefactsButton.Name = "_pickPathToArtefactsButton";
            _pickPathToArtefactsButton.Size = new Size(51, 40);
            _pickPathToArtefactsButton.TabIndex = 25;
            _pickPathToArtefactsButton.Text = "...";
            _pickPathToArtefactsButton.UseVisualStyleBackColor = true;
            // 
            // DeployRuntimeView
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_pickPathToArtefactsButton);
            Controls.Add(_chkStandardAssemblies);
            Controls.Add(_dryRunCheckBox);
            Controls.Add(_replaceTargetSDKVersionComboBox);
            Controls.Add(label4);
            Controls.Add(_copyCommandButton);
            Controls.Add(_checkForRespectiveRefAssembliesCheckBox);
            Controls.Add(_availableAssembliesListView);
            Controls.Add(label3);
            Controls.Add(_availableDesktopRuntimesComboBox);
            Controls.Add(label2);
            Controls.Add(_pathToArtefactsRepoTextBox);
            Controls.Add(label1);
            Name = "DeployRuntimeView";
            Size = new Size(1311, 772);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox _chkStandardAssemblies;
        private CheckBox _dryRunCheckBox;
        private ComboBox _replaceTargetSDKVersionComboBox;
        private Label label4;
        private Button _copyCommandButton;
        private CheckBox _checkForRespectiveRefAssembliesCheckBox;
        private ListView _availableAssembliesListView;
        private Label label3;
        private ComboBox _availableDesktopRuntimesComboBox;
        private Label label2;
        private TextBox _pathToArtefactsRepoTextBox;
        private Label label1;
        private Button _pickPathToArtefactsButton;
    }
}
