using DebugListener.Views;

namespace DebugListener
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
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            _tsmSaveLog = new ToolStripMenuItem();
            _tsmLoadLog = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            _tsmQuit = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            _tsmClear = new ToolStripMenuItem();
            _tsmShowStopWatch = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            _tsmOnlyShowExtendedInfoData = new ToolStripMenuItem();
            _menuBindingSource = new BindingSource(components);
            _tsmShowDifferentProcessColors = new ToolStripMenuItem();
            _tsmLeaveSpaceBetweenProcesses = new ToolStripMenuItem();
            _tsmTotalSecondsTimestamps = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            _tsmToolsOptions = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            _tslItemNo = new ToolStripStatusLabel();
            _tslProcessTime = new ToolStripStatusLabel();
            _tslThreadTime = new ToolStripStatusLabel();
            _tslDurationFromStart = new ToolStripStatusLabel();
            _tslSpaceHolder = new ToolStripStatusLabel();
            _tslDateTime = new ToolStripStatusLabel();
            _logView = new LogView();
            _tslStarttimeDuration = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_menuBindingSource).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_logView).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem, toolsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1411, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _tsmSaveLog, _tsmLoadLog, toolStripMenuItem1, _tsmQuit });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(55, 29);
            fileToolStripMenuItem.Text = "&File";
            // 
            // _tsmSaveLog
            // 
            _tsmSaveLog.Name = "_tsmSaveLog";
            _tsmSaveLog.Size = new Size(183, 30);
            _tsmSaveLog.Text = "Save log...";
            _tsmSaveLog.Click += SaveLog_Click;
            // 
            // _tsmLoadLog
            // 
            _tsmLoadLog.Name = "_tsmLoadLog";
            _tsmLoadLog.Size = new Size(183, 30);
            _tsmLoadLog.Text = "Load log...";
            _tsmLoadLog.Click += LoadLog_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(180, 6);
            // 
            // _tsmQuit
            // 
            _tsmQuit.Name = "_tsmQuit";
            _tsmQuit.Size = new Size(183, 30);
            _tsmQuit.Text = "Quit";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _tsmClear, _tsmShowStopWatch, toolStripMenuItem2, _tsmOnlyShowExtendedInfoData, _tsmShowDifferentProcessColors, _tsmLeaveSpaceBetweenProcesses, _tsmTotalSecondsTimestamps });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(67, 29);
            viewToolStripMenuItem.Text = "View";
            // 
            // _tsmClear
            // 
            _tsmClear.Name = "_tsmClear";
            _tsmClear.Size = new Size(462, 30);
            _tsmClear.Text = "Clear";
            _tsmClear.Click += ClearLogs_Click;
            // 
            // _tsmShowStopWatch
            // 
            _tsmShowStopWatch.Name = "_tsmShowStopWatch";
            _tsmShowStopWatch.Size = new Size(462, 30);
            _tsmShowStopWatch.Text = "Show Stopwatch";
            _tsmShowStopWatch.Click += TsmShowStopWatch_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(459, 6);
            // 
            // _tsmOnlyShowExtendedInfoData
            // 
            _tsmOnlyShowExtendedInfoData.DataBindings.Add(new Binding("Checked", _menuBindingSource, "OnlyShowExtendedDebugInfo", true, DataSourceUpdateMode.OnPropertyChanged));
            _tsmOnlyShowExtendedInfoData.DataBindings.Add(new Binding("Command", _menuBindingSource, "ToggleOnlyShowExtendedDebugInfoCommand", true));
            _tsmOnlyShowExtendedInfoData.Name = "_tsmOnlyShowExtendedInfoData";
            _tsmOnlyShowExtendedInfoData.Size = new Size(462, 30);
            _tsmOnlyShowExtendedInfoData.Text = "Only show entries with extended info data";
            // 
            // _menuBindingSource
            // 
            _menuBindingSource.DataSource = typeof(ViewModels.VmMenuOptionSettings);
            // 
            // _tsmShowDifferentProcessColors
            // 
            _tsmShowDifferentProcessColors.DataBindings.Add(new Binding("Checked", _menuBindingSource, "ColorProcesses", true, DataSourceUpdateMode.OnPropertyChanged));
            _tsmShowDifferentProcessColors.DataBindings.Add(new Binding("Command", _menuBindingSource, "ToggleColorProcessesCommand", true));
            _tsmShowDifferentProcessColors.Name = "_tsmShowDifferentProcessColors";
            _tsmShowDifferentProcessColors.Size = new Size(462, 30);
            _tsmShowDifferentProcessColors.Text = "Show different processes in different colors";
            // 
            // _tsmLeaveSpaceBetweenProcesses
            // 
            _tsmLeaveSpaceBetweenProcesses.DataBindings.Add(new Binding("Checked", _menuBindingSource, "LeaveSpaceBetweenProcesses", true, DataSourceUpdateMode.OnPropertyChanged));
            _tsmLeaveSpaceBetweenProcesses.DataBindings.Add(new Binding("Command", _menuBindingSource, "ToggleLeaveSpaceBetweenProcessesCommand", true));
            _tsmLeaveSpaceBetweenProcesses.Name = "_tsmLeaveSpaceBetweenProcesses";
            _tsmLeaveSpaceBetweenProcesses.Size = new Size(462, 30);
            _tsmLeaveSpaceBetweenProcesses.Text = "Leave space between processes changes";
            // 
            // _tsmTotalSecondsTimestamps
            // 
            _tsmTotalSecondsTimestamps.DataBindings.Add(new Binding("Checked", _menuBindingSource, "TotalSecondsTimestamps", true, DataSourceUpdateMode.OnPropertyChanged));
            _tsmTotalSecondsTimestamps.DataBindings.Add(new Binding("Command", _menuBindingSource, "ToggleTotalSecondsTimestampsCommand", true));
            _tsmTotalSecondsTimestamps.Name = "_tsmTotalSecondsTimestamps";
            _tsmTotalSecondsTimestamps.Size = new Size(462, 30);
            _tsmTotalSecondsTimestamps.Text = "Show Timestamps in total seconds";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _tsmToolsOptions });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(69, 29);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // _tsmToolsOptions
            // 
            _tsmToolsOptions.Name = "_tsmToolsOptions";
            _tsmToolsOptions.Size = new Size(164, 30);
            _tsmToolsOptions.Text = "Options";
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { _tslItemNo, _tslStarttimeDuration, _tslDurationFromStart, _tslProcessTime, _tslThreadTime, _tslSpaceHolder, _tslDateTime });
            statusStrip1.Location = new Point(0, 724);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1411, 39);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // _tslItemNo
            // 
            _tslItemNo.Name = "_tslItemNo";
            _tslItemNo.Padding = new Padding(4);
            _tslItemNo.Size = new Size(93, 33);
            _tslItemNo.Text = "#itemNo";
            // 
            // _tslProcessTime
            // 
            _tslProcessTime.Name = "_tslProcessTime";
            _tslProcessTime.Padding = new Padding(4);
            _tslProcessTime.Size = new Size(136, 33);
            _tslProcessTime.Text = "#processTime";
            // 
            // _tslThreadTime
            // 
            _tslThreadTime.Name = "_tslThreadTime";
            _tslThreadTime.Padding = new Padding(4);
            _tslThreadTime.Size = new Size(127, 33);
            _tslThreadTime.Text = "#threadTime";
            // 
            // _tslDurationFromStart
            // 
            _tslDurationFromStart.Name = "_tslDurationFromStart";
            _tslDurationFromStart.Padding = new Padding(4);
            _tslDurationFromStart.Size = new Size(103, 33);
            _tslDurationFromStart.Text = "#duration";
            // 
            // _tslSpaceHolder
            // 
            _tslSpaceHolder.Name = "_tslSpaceHolder";
            _tslSpaceHolder.Size = new Size(581, 33);
            _tslSpaceHolder.Spring = true;
            // 
            // _tslDateTime
            // 
            _tslDateTime.Name = "_tslDateTime";
            _tslDateTime.Padding = new Padding(4);
            _tslDateTime.Size = new Size(133, 33);
            _tslDateTime.Text = "#currentTime";
            // 
            // _logView
            // 
            _logView.AllowUserToAddRows = false;
            _logView.AllowUserToDeleteRows = false;
            _logView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _logView.DataBindings.Add(new Binding("ColorProcesses", _menuBindingSource, "ColorProcesses", true, DataSourceUpdateMode.OnPropertyChanged));
            _logView.DataBindings.Add(new Binding("LeaveSpaceBetweenProcesses", _menuBindingSource, "LeaveSpaceBetweenProcesses", true, DataSourceUpdateMode.OnPropertyChanged));
            _logView.DataBindings.Add(new Binding("OnlyShowExtendedDebugInfo", _menuBindingSource, "OnlyShowExtendedDebugInfo", true, DataSourceUpdateMode.OnPropertyChanged));
            _logView.DataBindings.Add(new Binding("TotalSecondsTimestamps", _menuBindingSource, "TotalSecondsTimestamps", true, DataSourceUpdateMode.OnPropertyChanged));
            _logView.Dock = DockStyle.Fill;
            _logView.Location = new Point(0, 33);
            _logView.Name = "_logView";
            _logView.ReadOnly = true;
            _logView.RowHeadersVisible = false;
            _logView.RowHeadersWidth = 62;
            _logView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _logView.Size = new Size(1411, 691);
            _logView.TabIndex = 2;
            _logView.TimeSpanFormatString = "HH:mm:ss-fff";
            _logView.VirtualMode = true;
            _logView.LogViewSelectionChanged += LogViewSelectionChanged;
            // 
            // _tslStarttimeDuration
            // 
            _tslStarttimeDuration.Name = "_tslStarttimeDuration";
            _tslStarttimeDuration.Padding = new Padding(4);
            _tslStarttimeDuration.Size = new Size(184, 33);
            _tslStarttimeDuration.Text = "#starttime/duration";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1411, 763);
            Controls.Add(_logView);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            Name = "FrmMain";
            Text = "WinForms Debug Listener";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_menuBindingSource).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_logView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem _tsmSaveLog;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem _tsmQuit;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem _tsmClear;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel _tslItemNo;
        private ToolStripStatusLabel _tslDateTime;
        private LogView _logView;
        private ToolStripMenuItem _tsmShowStopWatch;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem _tsmOnlyShowExtendedInfoData;
        private ToolStripMenuItem _tsmShowDifferentProcessColors;
        private ToolStripMenuItem _tsmLeaveSpaceBetweenProcesses;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem _tsmToolsOptions;
        private BindingSource _menuBindingSource;
        private ToolStripStatusLabel _tslProcessTime;
        private ToolStripStatusLabel _tslThreadTime;
        private ToolStripStatusLabel _tslSpaceHolder;
        private ToolStripStatusLabel _tslDurationFromStart;
        private ToolStripMenuItem _tsmLoadLog;
        private ToolStripMenuItem _tsmTotalSecondsTimestamps;
        private ToolStripStatusLabel _tslStarttimeDuration;
    }
}
