namespace DevTools.RuntimeDeploy.Views;

partial class CreateRuntimePatcherForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        _rootLayout = new TableLayoutPanel();
        _actionLayout = new TableLayoutPanel();
        _createPackageButton = new Button();
        _closeButton = new Button();
        _rootLayout.SuspendLayout();
        _actionLayout.SuspendLayout();
        SuspendLayout();
        // 
        // _rootLayout
        // 
        _rootLayout.ColumnCount = 1;
        _rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _rootLayout.Controls.Add(_actionLayout, 0, 1);
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
        // _actionLayout
        // 
        _actionLayout.ColumnCount = 3;
        _actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _actionLayout.Controls.Add(_createPackageButton, 1, 0);
        _actionLayout.Controls.Add(_closeButton, 2, 0);
        _actionLayout.Dock = DockStyle.Fill;
        _actionLayout.Margin = new Padding(0, 8, 0, 0);
        _actionLayout.Name = "_actionLayout";
        _actionLayout.RowCount = 1;
        _actionLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _actionLayout.TabIndex = 1;
        // 
        // _createPackageButton
        // 
        _createPackageButton.Anchor = AnchorStyles.Right;
        _createPackageButton.Margin = new Padding(3, 4, 3, 4);
        _createPackageButton.Size = new Size(260, 50);
        _createPackageButton.Name = "_createPackageButton";
        _createPackageButton.TabIndex = 0;
        _createPackageButton.Text = "Create package installer...";
        _createPackageButton.UseVisualStyleBackColor = true;
        _createPackageButton.Click += CreatePackageButton_Click;
        // 
        // _closeButton
        // 
        _closeButton.Anchor = AnchorStyles.Right;
        _closeButton.DialogResult = DialogResult.Cancel;
        _closeButton.Margin = new Padding(3, 4, 3, 4);
        _closeButton.Name = "_closeButton";
        _closeButton.Size = new Size(115, 50);
        _closeButton.TabIndex = 1;
        _closeButton.Text = "Close";
        _closeButton.UseVisualStyleBackColor = true;
        // 
        // CreateRuntimePatcherForm
        // 
        AutoScaleDimensions = new SizeF(12F, 30F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _closeButton;
        ClientSize = new Size(1302, 781);
        Controls.Add(_rootLayout);
        MinimumSize = new Size(900, 600);
        Name = "CreateRuntimePatcherForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Create Runtime patcher...";
        _rootLayout.ResumeLayout(false);
        _actionLayout.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _rootLayout;
    private TableLayoutPanel _actionLayout;
    private Button _createPackageButton;
    private Button _closeButton;
}
