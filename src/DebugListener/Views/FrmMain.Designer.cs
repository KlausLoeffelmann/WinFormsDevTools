﻿using DebugListener.Views;

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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            _tsmSaveLog = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            _tsmQuit = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            _tsmClear = new ToolStripMenuItem();
            _tsmShowStopWatch = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            _tslInfo = new ToolStripStatusLabel();
            _tslDateTime = new ToolStripStatusLabel();
            _logView = new LogView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            toolStripMenuItem2 = new ToolStripSeparator();
            onlyShowEntriesWithExtendedInfoDataToolStripMenuItem = new ToolStripMenuItem();
            showDifferentProcessesInDifferentColorsToolStripMenuItem = new ToolStripMenuItem();
            leaveSpaceBetweenProcessesToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
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
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _tsmClear, _tsmShowStopWatch, toolStripMenuItem2, onlyShowEntriesWithExtendedInfoDataToolStripMenuItem, showDifferentProcessesInDifferentColorsToolStripMenuItem, leaveSpaceBetweenProcessesToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(76, 34);
            viewToolStripMenuItem.Text = "View";
            // 
            // _tsmClear
            // 
            _tsmClear.Name = "_tsmClear";
            _tsmClear.Size = new Size(535, 38);
            _tsmClear.Text = "Clear";
            // 
            // _tsmShowStopWatch
            // 
            _tsmShowStopWatch.Name = "_tsmShowStopWatch";
            _tsmShowStopWatch.Size = new Size(535, 38);
            _tsmShowStopWatch.Text = "Show Stopwatch";
            _tsmShowStopWatch.Click += TsmShowStopWatch_Click;
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
            dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Process ID";
            dataGridViewTextBoxColumn2.MinimumWidth = 8;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn3.HeaderText = "Message";
            dataGridViewTextBoxColumn3.MinimumWidth = 8;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(532, 6);
            // 
            // onlyShowEntriesWithExtendedInfoDataToolStripMenuItem
            // 
            onlyShowEntriesWithExtendedInfoDataToolStripMenuItem.Name = "onlyShowEntriesWithExtendedInfoDataToolStripMenuItem";
            onlyShowEntriesWithExtendedInfoDataToolStripMenuItem.Size = new Size(535, 38);
            onlyShowEntriesWithExtendedInfoDataToolStripMenuItem.Text = "Only show entries with extended info data";
            // 
            // showDifferentProcessesInDifferentColorsToolStripMenuItem
            // 
            showDifferentProcessesInDifferentColorsToolStripMenuItem.Name = "showDifferentProcessesInDifferentColorsToolStripMenuItem";
            showDifferentProcessesInDifferentColorsToolStripMenuItem.Size = new Size(535, 38);
            showDifferentProcessesInDifferentColorsToolStripMenuItem.Text = "Show different processes in different colors";
            // 
            // leaveSpaceBetweenProcessesToolStripMenuItem
            // 
            leaveSpaceBetweenProcessesToolStripMenuItem.Name = "leaveSpaceBetweenProcessesToolStripMenuItem";
            leaveSpaceBetweenProcessesToolStripMenuItem.Size = new Size(535, 38);
            leaveSpaceBetweenProcessesToolStripMenuItem.Text = "Leave space between processes changes";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { optionsToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(79, 34);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(270, 38);
            optionsToolStripMenuItem.Text = "Options";
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
        private ToolStripMenuItem onlyShowEntriesWithExtendedInfoDataToolStripMenuItem;
        private ToolStripMenuItem showDifferentProcessesInDifferentColorsToolStripMenuItem;
        private ToolStripMenuItem leaveSpaceBetweenProcessesToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
    }
}
