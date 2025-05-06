namespace DevTools.GitHubBuddy
{
    partial class FrmGitHubExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGitHubExplorer));
            _statusStrip = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            _menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            _tsmLoginToGithub = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            quitToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            hideContentFromCaptureToolStripMenuItem = new ToolStripMenuItem();
            hideWindowFromCaptureToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            _tsmAddRepo = new ToolStripMenuItem();
            addQueryToolStripMenuItem = new ToolStripMenuItem();
            agentsToolStripMenuItem = new ToolStripMenuItem();
            _toolStrip = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            splitContainer1 = new SplitContainer();
            gitHubExplorerTreeView1 = new DevTools.GitHubBuddy.Controls.GitHubExplorerTreeView();
            gitHubIssuesListView1 = new DevTools.GitHubBuddy.Controls.GitHubIssuesListView();
            _statusStrip.SuspendLayout();
            _menuStrip.SuspendLayout();
            _toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // _statusStrip
            // 
            _statusStrip.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _statusStrip.ImageScalingSize = new Size(24, 24);
            _statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            _statusStrip.Location = new Point(0, 558);
            _statusStrip.Name = "_statusStrip";
            _statusStrip.Padding = new Padding(1, 0, 17, 0);
            _statusStrip.Size = new Size(1061, 37);
            _statusStrip.TabIndex = 3;
            _statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(63, 30);
            toolStripStatusLabel1.Text = "#info";
            // 
            // _menuStrip
            // 
            _menuStrip.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _menuStrip.ImageScalingSize = new Size(24, 24);
            _menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, toolStripMenuItem2, editToolStripMenuItem, agentsToolStripMenuItem });
            _menuStrip.Location = new Point(0, 0);
            _menuStrip.Name = "_menuStrip";
            _menuStrip.Size = new Size(1061, 38);
            _menuStrip.TabIndex = 0;
            _menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _tsmLoginToGithub, toolStripMenuItem1, quitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(62, 34);
            fileToolStripMenuItem.Text = "&File";
            // 
            // _tsmLoginToGithub
            // 
            _tsmLoginToGithub.Name = "_tsmLoginToGithub";
            _tsmLoginToGithub.Size = new Size(329, 38);
            _tsmLoginToGithub.Text = "&Register with GitHub...";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(326, 6);
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(329, 38);
            quitToolStripMenuItem.Text = "Quit";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { hideContentFromCaptureToolStripMenuItem, hideWindowFromCaptureToolStripMenuItem });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(76, 34);
            toolStripMenuItem2.Text = "&View";
            // 
            // hideContentFromCaptureToolStripMenuItem
            // 
            hideContentFromCaptureToolStripMenuItem.Name = "hideContentFromCaptureToolStripMenuItem";
            hideContentFromCaptureToolStripMenuItem.Size = new Size(384, 38);
            hideContentFromCaptureToolStripMenuItem.Text = "Hide Content from Capture";
            hideContentFromCaptureToolStripMenuItem.Click += HideContentFromCaptureToolStripMenuItem_Click;
            // 
            // hideWindowFromCaptureToolStripMenuItem
            // 
            hideWindowFromCaptureToolStripMenuItem.Name = "hideWindowFromCaptureToolStripMenuItem";
            hideWindowFromCaptureToolStripMenuItem.Size = new Size(384, 38);
            hideWindowFromCaptureToolStripMenuItem.Text = "Hide Window from Capture";
            hideWindowFromCaptureToolStripMenuItem.Click += HideWindowFromCaptureToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _tsmAddRepo, addQueryToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(65, 34);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // _tsmAddRepo
            // 
            _tsmAddRepo.Name = "_tsmAddRepo";
            _tsmAddRepo.Size = new Size(237, 38);
            _tsmAddRepo.Text = "Add Repo...";
            // 
            // addQueryToolStripMenuItem
            // 
            addQueryToolStripMenuItem.Name = "addQueryToolStripMenuItem";
            addQueryToolStripMenuItem.Size = new Size(237, 38);
            addQueryToolStripMenuItem.Text = "Add Query...";
            // 
            // agentsToolStripMenuItem
            // 
            agentsToolStripMenuItem.Name = "agentsToolStripMenuItem";
            agentsToolStripMenuItem.Size = new Size(96, 34);
            agentsToolStripMenuItem.Text = "&Agents";
            // 
            // _toolStrip
            // 
            _toolStrip.ImageScalingSize = new Size(32, 32);
            _toolStrip.Items.AddRange(new ToolStripItem[] { toolStripButton1 });
            _toolStrip.Location = new Point(0, 38);
            _toolStrip.Name = "_toolStrip";
            _toolStrip.Size = new Size(1061, 41);
            _toolStrip.TabIndex = 4;
            _toolStrip.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(36, 36);
            toolStripButton1.Text = "toolStripButton1";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 79);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(gitHubExplorerTreeView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(gitHubIssuesListView1);
            splitContainer1.Size = new Size(1061, 479);
            splitContainer1.SplitterDistance = 353;
            splitContainer1.TabIndex = 5;
            // 
            // gitHubExplorerTreeView1
            // 
            gitHubExplorerTreeView1.Location = new Point(17, 19);
            gitHubExplorerTreeView1.Name = "gitHubExplorerTreeView1";
            gitHubExplorerTreeView1.Size = new Size(296, 399);
            gitHubExplorerTreeView1.TabIndex = 0;
            // 
            // gitHubIssuesListView1
            // 
            gitHubIssuesListView1.Location = new Point(28, 28);
            gitHubIssuesListView1.Name = "gitHubIssuesListView1";
            gitHubIssuesListView1.Size = new Size(636, 408);
            gitHubIssuesListView1.TabIndex = 0;
            gitHubIssuesListView1.UseCompatibleStateImageBehavior = false;
            // 
            // FrmGitHubExplorer
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1061, 595);
            Controls.Add(splitContainer1);
            Controls.Add(_toolStrip);
            Controls.Add(_statusStrip);
            Controls.Add(_menuStrip);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormScreenCaptureMode = ScreenCaptureMode.Allow;
            MainMenuStrip = _menuStrip;
            Margin = new Padding(4);
            Name = "FrmGitHubExplorer";
            Text = "WinForms GitHub Buddy";
            _statusStrip.ResumeLayout(false);
            _statusStrip.PerformLayout();
            _menuStrip.ResumeLayout(false);
            _menuStrip.PerformLayout();
            _toolStrip.ResumeLayout(false);
            _toolStrip.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip _statusStrip;
        private MenuStrip _menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem _tsmLoginToGithub;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem _tsmAddRepo;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStrip _toolStrip;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem quitToolStripMenuItem;
        private SplitContainer splitContainer1;
        private Controls.GitHubExplorerTreeView gitHubExplorerTreeView1;
        private Controls.GitHubIssuesListView gitHubIssuesListView1;
        private ToolStripMenuItem addQueryToolStripMenuItem;
        private ToolStripMenuItem agentsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem hideContentFromCaptureToolStripMenuItem;
        private ToolStripMenuItem hideWindowFromCaptureToolStripMenuItem;
    }
}
