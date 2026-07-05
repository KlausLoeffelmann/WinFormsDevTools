namespace DevTools.RuntimeDeploy.Views;

partial class RestoreBackupForm
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
        _backupsListView = new ListView();
        _actionLayout = new TableLayoutPanel();
        _refreshButton = new Button();
        _restoreButton = new Button();
        _closeButton = new Button();
        _rootLayout.SuspendLayout();
        _actionLayout.SuspendLayout();
        SuspendLayout();
        // 
        // _rootLayout
        // 
        _rootLayout.ColumnCount = 1;
        _rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _rootLayout.Controls.Add(_backupsListView, 0, 0);
        _rootLayout.Controls.Add(_actionLayout, 0, 1);
        _rootLayout.Dock = DockStyle.Fill;
        _rootLayout.Location = new Point(0, 0);
        _rootLayout.Name = "_rootLayout";
        _rootLayout.Padding = new Padding(12);
        _rootLayout.RowCount = 2;
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _rootLayout.Size = new Size(1100, 650);
        _rootLayout.TabIndex = 0;
        // 
        // _backupsListView
        // 
        _backupsListView.Dock = DockStyle.Fill;
        _backupsListView.FullRowSelect = true;
        _backupsListView.GridLines = true;
        _backupsListView.MultiSelect = false;
        _backupsListView.Margin = new Padding(3, 4, 3, 4);
        _backupsListView.Name = "_backupsListView";
        _backupsListView.UseCompatibleStateImageBehavior = false;
        _backupsListView.View = View.Details;
        // 
        // _actionLayout
        // 
        _actionLayout.ColumnCount = 4;
        _actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _actionLayout.Controls.Add(_refreshButton, 0, 0);
        _actionLayout.Controls.Add(_restoreButton, 2, 0);
        _actionLayout.Controls.Add(_closeButton, 3, 0);
        _actionLayout.Dock = DockStyle.Fill;
        _actionLayout.Margin = new Padding(0, 8, 0, 0);
        _actionLayout.Name = "_actionLayout";
        _actionLayout.RowCount = 1;
        _actionLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _actionLayout.TabIndex = 1;
        // 
        // _refreshButton
        // 
        _refreshButton.Anchor = AnchorStyles.Left;
        _refreshButton.Margin = new Padding(3, 4, 3, 4);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.Size = new Size(140, 44);
        _refreshButton.TabIndex = 0;
        _refreshButton.Text = "Refresh";
        _refreshButton.UseVisualStyleBackColor = true;
        _refreshButton.Click += RefreshButton_Click;
        // 
        // _restoreButton
        // 
        _restoreButton.Anchor = AnchorStyles.Right;
        _restoreButton.Margin = new Padding(3, 4, 3, 4);
        _restoreButton.Name = "_restoreButton";
        _restoreButton.Size = new Size(160, 44);
        _restoreButton.TabIndex = 1;
        _restoreButton.Text = "Restore...";
        _restoreButton.UseVisualStyleBackColor = true;
        _restoreButton.Click += RestoreButton_Click;
        // 
        // _closeButton
        // 
        _closeButton.Anchor = AnchorStyles.Right;
        _closeButton.DialogResult = DialogResult.Cancel;
        _closeButton.Margin = new Padding(3, 4, 3, 4);
        _closeButton.Name = "_closeButton";
        _closeButton.Size = new Size(115, 44);
        _closeButton.TabIndex = 2;
        _closeButton.Text = "Close";
        _closeButton.UseVisualStyleBackColor = true;
        // 
        // RestoreBackupForm
        // 
        AutoScaleDimensions = new SizeF(12F, 30F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _closeButton;
        ClientSize = new Size(1100, 650);
        Controls.Add(_rootLayout);
        MinimumSize = new Size(800, 480);
        Name = "RestoreBackupForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Restore Backup...";
        _rootLayout.ResumeLayout(false);
        _actionLayout.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _rootLayout;
    private ListView _backupsListView;
    private TableLayoutPanel _actionLayout;
    private Button _refreshButton;
    private Button _restoreButton;
    private Button _closeButton;
}
