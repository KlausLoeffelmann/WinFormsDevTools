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
            toolStripMenuItem1 = new ToolStripSeparator();
            _tsmQuit = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            _tsmClear = new ToolStripMenuItem();
            _tsmShowStopWatch = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            _tsmOnlyShowExtendedInfoData = new ToolStripMenuItem();
            vmMenuOptionSettingsBindingSource = new BindingSource(components);
            _tsmShowDifferentProcessColors = new ToolStripMenuItem();
            _tsmLeaveSpaceBetweenProcesses = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            _tsmToolsOptions = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            _tslInfo = new ToolStripStatusLabel();
            _tslDateTime = new ToolStripStatusLabel();
            _logView = new LogView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)vmMenuOptionSettingsBindingSource).BeginInit();
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
            menuStrip1.Size = new Size(960, 38);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _tsmSaveLog, toolStripMenuItem1, _tsmQuit });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(62, 34);
            fileToolStripMenuItem.Text = "&File";
            // 
            // _tsmSaveLog
            // 
            _tsmSaveLog.Name = "_tsmSaveLog";
            _tsmSaveLog.Size = new Size(214, 38);
            _tsmSaveLog.Text = "Save log...";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(211, 6);
            // 
            // _tsmQuit
            // 
            _tsmQuit.Name = "_tsmQuit";
            _tsmQuit.Size = new Size(214, 38);
            _tsmQuit.Text = "Quit";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _tsmClear, _tsmShowStopWatch, toolStripMenuItem2, _tsmOnlyShowExtendedInfoData, _tsmShowDifferentProcessColors, _tsmLeaveSpaceBetweenProcesses });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(76, 34);
            viewToolStripMenuItem.Text = "View";
            // 
            // _tsmClear
            // 
            _tsmClear.Name = "_tsmClear";
            _tsmClear.Size = new Size(535, 38);
            _tsmClear.Text = "Clear";
            _tsmClear.Click += _tsmClear_Click;
            // 
            // _tsmShowStopWatch
            // 
            _tsmShowStopWatch.Name = "_tsmShowStopWatch";
            _tsmShowStopWatch.Size = new Size(535, 38);
            _tsmShowStopWatch.Text = "Show Stopwatch";
            _tsmShowStopWatch.Click += TsmShowStopWatch_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(532, 6);
            // 
            // _tsmOnlyShowExtendedInfoData
            // 
            _tsmOnlyShowExtendedInfoData.DataBindings.Add(new Binding("Checked", vmMenuOptionSettingsBindingSource, "OnlyShowExtendedDebugInfo", true));
            _tsmOnlyShowExtendedInfoData.Name = "_tsmOnlyShowExtendedInfoData";
            _tsmOnlyShowExtendedInfoData.Size = new Size(535, 38);
            _tsmOnlyShowExtendedInfoData.Text = "Only show entries with extended info data";
            // 
            // vmMenuOptionSettingsBindingSource
            // 
            vmMenuOptionSettingsBindingSource.DataSource = typeof(ViewModels.VmMenuOptionSettings);
            // 
            // _tsmShowDifferentProcessColors
            // 
            _tsmShowDifferentProcessColors.Name = "_tsmShowDifferentProcessColors";
            _tsmShowDifferentProcessColors.Size = new Size(535, 38);
            _tsmShowDifferentProcessColors.Text = "Show different processes in different colors";
            // 
            // _tsmLeaveSpaceBetweenProcesses
            // 
            _tsmLeaveSpaceBetweenProcesses.Name = "_tsmLeaveSpaceBetweenProcesses";
            _tsmLeaveSpaceBetweenProcesses.Size = new Size(535, 38);
            _tsmLeaveSpaceBetweenProcesses.Text = "Leave space between processes changes";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _tsmToolsOptions });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(79, 34);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // _tsmToolsOptions
            // 
            _tsmToolsOptions.Name = "_tsmToolsOptions";
            _tsmToolsOptions.Size = new Size(192, 38);
            _tsmToolsOptions.Text = "Options";
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { _tslInfo, _tslDateTime });
            statusStrip1.Location = new Point(0, 503);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(960, 37);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // _tslInfo
            // 
            _tslInfo.Name = "_tslInfo";
            _tslInfo.Size = new Size(876, 30);
            _tslInfo.Spring = true;
            _tslInfo.Text = "#info";
            // 
            // _tslDateTime
            // 
            _tslDateTime.Name = "_tslDateTime";
            _tslDateTime.Size = new Size(69, 30);
            _tslDateTime.Text = "#time";
            // 
            // _logView
            // 
            _logView.AllowUserToAddRows = false;
            _logView.AllowUserToDeleteRows = false;
            _logView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _logView.Dock = DockStyle.Fill;
            _logView.Location = new Point(0, 38);
            _logView.MultiSelect = false;
            _logView.Name = "_logView";
            _logView.ReadOnly = true;
            _logView.RowHeadersVisible = false;
            _logView.RowHeadersWidth = 62;
            _logView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _logView.Size = new Size(960, 465);
            _logView.TabIndex = 2;
            _logView.VirtualMode = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Timestamp";
            dataGridViewTextBoxColumn1.MinimumWidth = 8;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 170;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Duration";
            dataGridViewTextBoxColumn2.MinimumWidth = 8;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 160;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Process ID";
            dataGridViewTextBoxColumn3.MinimumWidth = 8;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Thread ID";
            dataGridViewTextBoxColumn4.MinimumWidth = 8;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Method";
            dataGridViewTextBoxColumn5.MinimumWidth = 8;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.Width = 250;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Line No";
            dataGridViewTextBoxColumn6.MinimumWidth = 8;
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.Width = 140;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn7.HeaderText = "Message";
            dataGridViewTextBoxColumn7.MinimumWidth = 8;
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.HeaderText = "CodeFile";
            dataGridViewTextBoxColumn8.MinimumWidth = 8;
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.ReadOnly = true;
            dataGridViewTextBoxColumn8.Width = 200;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 540);
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
            ((System.ComponentModel.ISupportInitialize)vmMenuOptionSettingsBindingSource).EndInit();
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
        private ToolStripStatusLabel _tslInfo;
        private ToolStripStatusLabel _tslDateTime;
        private LogView _logView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private ToolStripMenuItem _tsmShowStopWatch;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem _tsmOnlyShowExtendedInfoData;
        private ToolStripMenuItem _tsmShowDifferentProcessColors;
        private ToolStripMenuItem _tsmLeaveSpaceBetweenProcesses;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem _tsmToolsOptions;
        private BindingSource vmMenuOptionSettingsBindingSource;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}
