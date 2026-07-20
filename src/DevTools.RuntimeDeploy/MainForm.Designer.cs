using WarpToolkit.WinForms.Containers;

namespace DevTools.RuntimeDeploy;

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
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        _tabControl = new FluentTabControl();
        _menuStrip = new MenuStrip();
        _toolsMenuItem = new ToolStripMenuItem();
        _optionsMenuItem = new ToolStripMenuItem();
        _createRuntimePatcherMenuItem = new ToolStripMenuItem();
        _restoreBackupMenuItem = new ToolStripMenuItem();
        _saveWindowPositionsMenuItem = new ToolStripMenuItem();
        _toolsSeparator = new ToolStripSeparator();
        _quitMenuItem = new ToolStripMenuItem();
        _statusStrip = new StatusStrip();
        _statusMessageLabel = new ToolStripStatusLabel();
        _notifyIcon = new NotifyIcon(components);
        _trayContextMenu = new ContextMenuStrip(components);
        _restoreTrayMenuItem = new ToolStripMenuItem();
        _optionsTrayMenuItem = new ToolStripMenuItem();
        _traySeparator = new ToolStripSeparator();
        _quitTrayMenuItem = new ToolStripMenuItem();
        _menuStrip.SuspendLayout();
        _statusStrip.SuspendLayout();
        _trayContextMenu.SuspendLayout();
        SuspendLayout();
        // 
        // _tabControl
        // 
        _tabControl.Dock = DockStyle.Fill;
        _tabControl.Location = new Point(0, 33);
        _tabControl.Margin = new Padding(3, 4, 3, 4);
        _tabControl.Name = "_tabControl";
        _tabControl.Size = new Size(1317, 597);
        _tabControl.TabIndex = 0;
        // 
        // _menuStrip
        // 
        _menuStrip.ImageScalingSize = new Size(20, 20);
        _menuStrip.Items.AddRange(new ToolStripItem[] { _toolsMenuItem });
        _menuStrip.Location = new Point(0, 0);
        _menuStrip.Name = "_menuStrip";
        _menuStrip.Size = new Size(1317, 33);
        _menuStrip.TabIndex = 1;
        _menuStrip.Text = "menuStrip1";
        // 
        // _toolsMenuItem
        // 
        _toolsMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _optionsMenuItem, _createRuntimePatcherMenuItem, _restoreBackupMenuItem, _saveWindowPositionsMenuItem, _toolsSeparator, _quitMenuItem });
        _toolsMenuItem.Name = "_toolsMenuItem";
        _toolsMenuItem.Size = new Size(69, 29);
        _toolsMenuItem.Text = "&Tools";
        // 
        // _optionsMenuItem
        // 
        _optionsMenuItem.Name = "_optionsMenuItem";
        _optionsMenuItem.Size = new Size(311, 34);
        _optionsMenuItem.Text = "&Options...";
        _optionsMenuItem.Click += OptionsMenuItem_Click;
        // 
        // _createRuntimePatcherMenuItem
        // 
        _createRuntimePatcherMenuItem.Name = "_createRuntimePatcherMenuItem";
        _createRuntimePatcherMenuItem.Size = new Size(311, 34);
        _createRuntimePatcherMenuItem.Text = "Create Runtime patcher...";
        _createRuntimePatcherMenuItem.Click += CreateRuntimePatcherMenuItem_Click;
        // 
        // _restoreBackupMenuItem
        // 
        _restoreBackupMenuItem.Name = "_restoreBackupMenuItem";
        _restoreBackupMenuItem.Size = new Size(311, 34);
        _restoreBackupMenuItem.Text = "Restore Backup...";
        _restoreBackupMenuItem.Click += RestoreBackupMenuItem_Click;
        // 
        // _saveWindowPositionsMenuItem
        // 
        _saveWindowPositionsMenuItem.Checked = true;
        _saveWindowPositionsMenuItem.CheckOnClick = true;
        _saveWindowPositionsMenuItem.CheckState = CheckState.Checked;
        _saveWindowPositionsMenuItem.Name = "_saveWindowPositionsMenuItem";
        _saveWindowPositionsMenuItem.Size = new Size(311, 34);
        _saveWindowPositionsMenuItem.Text = "&Save Window positions";
        _saveWindowPositionsMenuItem.Click += SaveWindowPositionsMenuItem_Click;
        // 
        // _toolsSeparator
        // 
        _toolsSeparator.Name = "_toolsSeparator";
        _toolsSeparator.Size = new Size(308, 6);
        // 
        // _quitMenuItem
        // 
        _quitMenuItem.Name = "_quitMenuItem";
        _quitMenuItem.Size = new Size(311, 34);
        _quitMenuItem.Text = "&Quit";
        _quitMenuItem.Click += QuitMenuItem_Click;
        // 
        // _statusStrip
        // 
        _statusStrip.ImageScalingSize = new Size(20, 20);
        _statusStrip.Items.AddRange(new ToolStripItem[] { _statusMessageLabel });
        _statusStrip.Location = new Point(0, 630);
        _statusStrip.Name = "_statusStrip";
        _statusStrip.Size = new Size(1317, 36);
        _statusStrip.TabIndex = 2;
        _statusStrip.Text = "statusStrip1";
        // 
        // _statusMessageLabel
        // 
        _statusMessageLabel.ForeColor = Color.FromArgb(128, 128, 255);
        _statusMessageLabel.Name = "_statusMessageLabel";
        _statusMessageLabel.Padding = new Padding(2);
        _statusMessageLabel.Size = new Size(1302, 29);
        _statusMessageLabel.Spring = true;
        _statusMessageLabel.Text = "Ready.";
        _statusMessageLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _notifyIcon
        // 
        _notifyIcon.ContextMenuStrip = _trayContextMenu;
        _notifyIcon.Icon = (Icon)resources.GetObject("_notifyIcon.Icon");
        _notifyIcon.Text = "WinForms Runtime Deploy";
        _notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        _notifyIcon.MouseClick += NotifyIcon_MouseClick;
        // 
        // _trayContextMenu
        // 
        _trayContextMenu.ImageScalingSize = new Size(20, 20);
        _trayContextMenu.Items.AddRange(new ToolStripItem[] { _restoreTrayMenuItem, _optionsTrayMenuItem, _traySeparator, _quitTrayMenuItem });
        _trayContextMenu.Name = "_trayContextMenu";
        _trayContextMenu.Size = new Size(195, 106);
        // 
        // _restoreTrayMenuItem
        // 
        _restoreTrayMenuItem.Name = "_restoreTrayMenuItem";
        _restoreTrayMenuItem.Size = new Size(194, 32);
        _restoreTrayMenuItem.Text = "Restore App";
        _restoreTrayMenuItem.Click += RestoreMenuItem_Click;
        // 
        // _optionsTrayMenuItem
        // 
        _optionsTrayMenuItem.Name = "_optionsTrayMenuItem";
        _optionsTrayMenuItem.Size = new Size(194, 32);
        _optionsTrayMenuItem.Text = "Options...";
        _optionsTrayMenuItem.Click += OptionsMenuItem_Click;
        // 
        // _traySeparator
        // 
        _traySeparator.Name = "_traySeparator";
        _traySeparator.Size = new Size(191, 6);
        // 
        // _quitTrayMenuItem
        // 
        _quitTrayMenuItem.Name = "_quitTrayMenuItem";
        _quitTrayMenuItem.Size = new Size(194, 32);
        _quitTrayMenuItem.Text = "Quit";
        _quitTrayMenuItem.Click += QuitMenuItem_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(12F, 30F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1317, 666);
        Controls.Add(_tabControl);
        Controls.Add(_statusStrip);
        Controls.Add(_menuStrip);
        Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = _menuStrip;
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(1339, 722);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "WinFormsDevTool";
        _menuStrip.ResumeLayout(false);
        _menuStrip.PerformLayout();
        _statusStrip.ResumeLayout(false);
        _statusStrip.PerformLayout();
        _trayContextMenu.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private FluentTabControl _tabControl;
    private MenuStrip _menuStrip;
    private ToolStripMenuItem _toolsMenuItem;
    private ToolStripMenuItem _optionsMenuItem;
    private ToolStripMenuItem _createRuntimePatcherMenuItem;
    private ToolStripMenuItem _restoreBackupMenuItem;
    private ToolStripMenuItem _saveWindowPositionsMenuItem;
    private ToolStripSeparator _toolsSeparator;
    private ToolStripMenuItem _quitMenuItem;
    private StatusStrip _statusStrip;
    private ToolStripStatusLabel _statusMessageLabel;
    private NotifyIcon _notifyIcon;
    private ContextMenuStrip _trayContextMenu;
    private ToolStripMenuItem _restoreTrayMenuItem;
    private ToolStripMenuItem _optionsTrayMenuItem;
    private ToolStripSeparator _traySeparator;
    private ToolStripMenuItem _quitTrayMenuItem;
}