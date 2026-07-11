namespace DevTools.RuntimeDeploy.Views;

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
        _rootLayout = new TableLayoutPanel();
        _commandLayout = new TableLayoutPanel();
        label4 = new Label();
        _replaceTargetSDKVersionComboBox = new ComboBox();
        _dryRunCheckBox = new CheckBox();
        _copyCommandButton = new Button();
        _rootLayout.SuspendLayout();
        _commandLayout.SuspendLayout();
        SuspendLayout();
        // 
        // _rootLayout
        // 
        _rootLayout.ColumnCount = 1;
        _rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _rootLayout.Controls.Add(_commandLayout, 0, 1);
        _rootLayout.Dock = DockStyle.Fill;
        _rootLayout.Location = new Point(0, 0);
        _rootLayout.Name = "_rootLayout";
        _rootLayout.Padding = new Padding(12);
        _rootLayout.RowCount = 2;
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _rootLayout.Size = new Size(1302, 781);
        _rootLayout.TabIndex = 0;
        // 
        // _commandLayout
        // 
        _commandLayout.ColumnCount = 5;
        _commandLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _commandLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _commandLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _commandLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _commandLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _commandLayout.Controls.Add(label4, 0, 0);
        _commandLayout.Controls.Add(_replaceTargetSDKVersionComboBox, 1, 0);
        _commandLayout.Controls.Add(_dryRunCheckBox, 3, 0);
        _commandLayout.Controls.Add(_copyCommandButton, 4, 0);
        _commandLayout.Dock = DockStyle.Fill;
        _commandLayout.Margin = new Padding(0, 8, 0, 0);
        _commandLayout.Name = "_commandLayout";
        _commandLayout.RowCount = 1;
        _commandLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _commandLayout.TabIndex = 4;
        // 
        // label4
        // 
        label4.Anchor = AnchorStyles.Left;
        label4.AutoSize = true;
        label4.Margin = new Padding(3, 0, 12, 0);
        label4.Name = "label4";
        label4.TabIndex = 0;
        label4.Text = "Replace Target SDK Version:";
        // 
        // _replaceTargetSDKVersionComboBox
        // 
        _replaceTargetSDKVersionComboBox.Anchor = AnchorStyles.Left;
        _replaceTargetSDKVersionComboBox.FormattingEnabled = true;
        _replaceTargetSDKVersionComboBox.Margin = new Padding(3, 4, 3, 4);
        _replaceTargetSDKVersionComboBox.Name = "_replaceTargetSDKVersionComboBox";
        _replaceTargetSDKVersionComboBox.Size = new Size(403, 38);
        _replaceTargetSDKVersionComboBox.TabIndex = 1;
        // 
        // _dryRunCheckBox
        // 
        _dryRunCheckBox.Anchor = AnchorStyles.Right;
        _dryRunCheckBox.AutoSize = true;
        _dryRunCheckBox.Checked = true;
        _dryRunCheckBox.CheckState = CheckState.Checked;
        _dryRunCheckBox.Margin = new Padding(3, 4, 12, 4);
        _dryRunCheckBox.Name = "_dryRunCheckBox";
        _dryRunCheckBox.TabIndex = 2;
        _dryRunCheckBox.Text = "Dry run";
        _dryRunCheckBox.UseVisualStyleBackColor = true;
        // 
        // _copyCommandButton
        // 
        _copyCommandButton.Anchor = AnchorStyles.Right;
        _copyCommandButton.Margin = new Padding(3, 4, 3, 4);
        _copyCommandButton.Name = "_copyCommandButton";
        _copyCommandButton.Size = new Size(202, 50);
        _copyCommandButton.TabIndex = 3;
        _copyCommandButton.Text = "Copy...";
        _copyCommandButton.UseVisualStyleBackColor = true;
        _copyCommandButton.Click += CopyCommandButton_Click;
        // 
        // DeployRuntimeView
        // 
        AutoScaleDimensions = new SizeF(12F, 30F);
        AutoScaleMode = AutoScaleMode.Inherit;
        Controls.Add(_rootLayout);
        Name = "DeployRuntimeView";
        Size = new Size(1302, 781);
        _rootLayout.ResumeLayout(false);
        _commandLayout.ResumeLayout(false);
        _commandLayout.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _rootLayout;
    private TableLayoutPanel _commandLayout;
    private Label label4;
    private ComboBox _replaceTargetSDKVersionComboBox;
    private CheckBox _dryRunCheckBox;
    private Button _copyCommandButton;
}
