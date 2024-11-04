using CommunityToolkit.WinForms.Controls;

namespace ShadowCacheSpy
{
    partial class FrmMain
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
            _menuStrip = new MenuStrip();
            _tsmFile = new ToolStripMenuItem();
            _tsmOptions = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            _tsmQuit = new ToolStripMenuItem();
            testToolStripMenuItem = new ToolStripMenuItem();
            testConsoleToolStripMenuItem = new ToolStripMenuItem();
            _statusStrip = new StatusStrip();
            _tslWatchPath = new ToolStripStatusLabel();
            _tslClock = new ToolStripStatusLabel();
            _console = new CommunityToolkit.WinForms.Controls.ConsoleControl();
            _fileSystemWatcher = new FileSystemWatcher();
            _tslTickDuration = new ToolStripStatusLabel();
            _menuStrip.SuspendLayout();
            _statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_fileSystemWatcher).BeginInit();
            SuspendLayout();
            // 
            // _menuStrip
            // 
            _menuStrip.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _menuStrip.ImageScalingSize = new Size(24, 24);
            _menuStrip.Items.AddRange(new ToolStripItem[] { _tsmFile, testToolStripMenuItem });
            _menuStrip.Location = new Point(0, 0);
            _menuStrip.Name = "_menuStrip";
            _menuStrip.Padding = new Padding(7, 2, 0, 2);
            _menuStrip.Size = new Size(1063, 38);
            _menuStrip.TabIndex = 0;
            _menuStrip.Text = "menuStrip1";
            // 
            // _tsmFile
            // 
            _tsmFile.DropDownItems.AddRange(new ToolStripItem[] { _tsmOptions, toolStripMenuItem1, _tsmQuit });
            _tsmFile.Name = "_tsmFile";
            _tsmFile.Size = new Size(62, 34);
            _tsmFile.Text = "&File";
            // 
            // _tsmOptions
            // 
            _tsmOptions.Name = "_tsmOptions";
            _tsmOptions.Size = new Size(207, 38);
            _tsmOptions.Text = "Options...";
            _tsmOptions.Click += Options_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(204, 6);
            // 
            // _tsmQuit
            // 
            _tsmQuit.Name = "_tsmQuit";
            _tsmQuit.Size = new Size(207, 38);
            _tsmQuit.Text = "Quit";
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { testConsoleToolStripMenuItem });
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(67, 34);
            testToolStripMenuItem.Text = "&Test";
            // 
            // testConsoleToolStripMenuItem
            // 
            testConsoleToolStripMenuItem.Name = "testConsoleToolStripMenuItem";
            testConsoleToolStripMenuItem.Size = new Size(238, 38);
            testConsoleToolStripMenuItem.Text = "Test Console";
            // 
            // _statusStrip
            // 
            _statusStrip.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _statusStrip.ImageScalingSize = new Size(24, 24);
            _statusStrip.Items.AddRange(new ToolStripItem[] { _tslWatchPath, _tslTickDuration, _tslClock });
            _statusStrip.Location = new Point(0, 491);
            _statusStrip.Name = "_statusStrip";
            _statusStrip.Padding = new Padding(1, 0, 17, 0);
            _statusStrip.Size = new Size(1063, 43);
            _statusStrip.TabIndex = 1;
            _statusStrip.Text = "statusStrip1";
            // 
            // _tslWatchPath
            // 
            _tslWatchPath.Name = "_tslWatchPath";
            _tslWatchPath.Padding = new Padding(3);
            _tslWatchPath.Size = new Size(779, 36);
            _tslWatchPath.Spring = true;
            _tslWatchPath.Text = "#WatchPath#";
            _tslWatchPath.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _tslClock
            // 
            _tslClock.Name = "_tslClock";
            _tslClock.Size = new Size(167, 36);
            _tslClock.Text = "#Time and Date";
            // 
            // _console
            // 
            _console.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _console.Location = new Point(12, 41);
            _console.Name = "_console";
            _console.Size = new Size(1039, 441);
            _console.TabIndex = 2;
            _console.Text = "";
            // 
            // _fileSystemWatcher
            // 
            _fileSystemWatcher.EnableRaisingEvents = true;
            _fileSystemWatcher.SynchronizingObject = this;
            // 
            // _tslTickDuration
            // 
            _tslTickDuration.Name = "_tslTickDuration";
            _tslTickDuration.Size = new Size(53, 36);
            _tslTickDuration.Text = "#TD";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1063, 534);
            Controls.Add(_console);
            Controls.Add(_statusStrip);
            Controls.Add(_menuStrip);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = _menuStrip;
            Margin = new Padding(4);
            Name = "FrmMain";
            Text = "Shadow-Cache Spy";
            _menuStrip.ResumeLayout(false);
            _menuStrip.PerformLayout();
            _statusStrip.ResumeLayout(false);
            _statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_fileSystemWatcher).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip _menuStrip;
        private ToolStripMenuItem _tsmFile;
        private ToolStripMenuItem _tsmOptions;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem _tsmQuit;
        private StatusStrip _statusStrip;
        private ConsoleControl _console;
        private ToolStripStatusLabel _tslWatchPath;
        private ToolStripStatusLabel _tslClock;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripMenuItem testConsoleToolStripMenuItem;
        private FileSystemWatcher _fileSystemWatcher;
        private ToolStripStatusLabel _tslTickDuration;
    }
}
