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
            _tslUserCaption = new ToolStripStatusLabel();
            _tslUser = new ToolStripStatusLabel();
            _tslOrgCaption = new ToolStripStatusLabel();
            _tsdCurrentOrg = new ToolStripDropDownButton();
            _tslStatusCaption = new ToolStripStatusLabel();
            _tslInfo = new ToolStripStatusLabel();
            _tslTime = new ToolStripStatusLabel();
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
            toolStripSeparator1 = new ToolStripSeparator();
            splitContainer1 = new SplitContainer();
            decoratorPanel1 = new CommunityToolkit.WinForms.Containers.DecoratorPanel();
            _tvwExplorerTree = new DevTools.GitHubBuddy.Controls.GitHubExplorerTreeView();
            decoratorPanel2 = new CommunityToolkit.WinForms.Containers.DecoratorPanel();
            _lvwIssues = new DevTools.GitHubBuddy.Controls.GitHubIssuesListView();
            _statusStrip.SuspendLayout();
            _menuStrip.SuspendLayout();
            _toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            decoratorPanel1.SuspendLayout();
            decoratorPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // _statusStrip
            // 
            _statusStrip.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _statusStrip.ImageScalingSize = new Size(24, 24);
            _statusStrip.Items.AddRange(new ToolStripItem[] { _tslUserCaption, _tslUser, _tslOrgCaption, _tsdCurrentOrg, _tslStatusCaption, _tslInfo, _tslTime });
            _statusStrip.Location = new Point(0, 558);
            _statusStrip.Name = "_statusStrip";
            _statusStrip.Padding = new Padding(1, 0, 17, 0);
            _statusStrip.Size = new Size(1061, 37);
            _statusStrip.TabIndex = 3;
            _statusStrip.Text = "statusStrip1";
            // 
            // _tslUserCaption
            // 
            _tslUserCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _tslUserCaption.Name = "_tslUserCaption";
            _tslUserCaption.Size = new Size(67, 30);
            _tslUserCaption.Text = "User:";
            // 
            // _tslUser
            // 
            _tslUser.Name = "_tslUser";
            _tslUser.Size = new Size(70, 30);
            _tslUser.Text = "#User";
            // 
            // _tslOrgCaption
            // 
            _tslOrgCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _tslOrgCaption.Name = "_tslOrgCaption";
            _tslOrgCaption.Size = new Size(139, 30);
            _tslOrgCaption.Text = "Github-Org:";
            // 
            // _tsdCurrentOrg
            // 
            _tsdCurrentOrg.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _tsdCurrentOrg.Image = (Image)resources.GetObject("_tsdCurrentOrg.Image");
            _tsdCurrentOrg.ImageTransparentColor = Color.Magenta;
            _tsdCurrentOrg.Name = "_tsdCurrentOrg";
            _tsdCurrentOrg.Size = new Size(173, 34);
            _tsdCurrentOrg.Text = "#Current Repo";
            _tsdCurrentOrg.ToolTipText = "#Current Org";
            // 
            // _tslStatusCaption
            // 
            _tslStatusCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _tslStatusCaption.Name = "_tslStatusCaption";
            _tslStatusCaption.Size = new Size(83, 30);
            _tslStatusCaption.Text = "Status:";
            // 
            // _tslInfo
            // 
            _tslInfo.Name = "_tslInfo";
            _tslInfo.Size = new Size(437, 30);
            _tslInfo.Spring = true;
            _tslInfo.Text = "#Info";
            _tslInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _tslTime
            // 
            _tslTime.Name = "_tslTime";
            _tslTime.Size = new Size(74, 30);
            _tslTime.Text = "#Time";
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
            _toolStrip.Items.AddRange(new ToolStripItem[] { toolStripSeparator1 });
            _toolStrip.Location = new Point(0, 38);
            _toolStrip.Name = "_toolStrip";
            _toolStrip.Size = new Size(1061, 28);
            _toolStrip.TabIndex = 4;
            _toolStrip.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 28);
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 66);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(decoratorPanel1);
            splitContainer1.Panel1.Padding = new Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(decoratorPanel2);
            splitContainer1.Panel2.Padding = new Padding(5);
            splitContainer1.Size = new Size(1061, 492);
            splitContainer1.SplitterDistance = 353;
            splitContainer1.TabIndex = 5;
            // 
            // decoratorPanel1
            // 
            decoratorPanel1.BorderThickness = 1;
            decoratorPanel1.Controls.Add(_tvwExplorerTree);
            decoratorPanel1.Dock = DockStyle.Fill;
            decoratorPanel1.Location = new Point(5, 5);
            decoratorPanel1.MinimumSize = new Size(28, 28);
            decoratorPanel1.Name = "decoratorPanel1";
            decoratorPanel1.Padding = new Padding(5);
            decoratorPanel1.Size = new Size(343, 482);
            decoratorPanel1.TabIndex = 1;
            // 
            // _tvwExplorerTree
            // 
            _tvwExplorerTree.Location = new Point(6, 6);
            _tvwExplorerTree.Name = "_tvwExplorerTree";
            _tvwExplorerTree.Size = new Size(331, 470);
            _tvwExplorerTree.TabIndex = 0;
            _tvwExplorerTree.AfterSelect += ExplorerTree_AfterSelect;
            // 
            // decoratorPanel2
            // 
            decoratorPanel2.BorderThickness = 1;
            decoratorPanel2.Controls.Add(_lvwIssues);
            decoratorPanel2.Dock = DockStyle.Fill;
            decoratorPanel2.Location = new Point(5, 5);
            decoratorPanel2.MinimumSize = new Size(28, 28);
            decoratorPanel2.Name = "decoratorPanel2";
            decoratorPanel2.Padding = new Padding(5);
            decoratorPanel2.Size = new Size(694, 482);
            decoratorPanel2.TabIndex = 1;
            // 
            // _lvwIssues
            // 
            _lvwIssues.AllowColumnReorder = true;
            _lvwIssues.FullRowSelect = true;
            _lvwIssues.Location = new Point(6, 6);
            _lvwIssues.MultiSelect = false;
            _lvwIssues.Name = "_lvwIssues";
            _lvwIssues.Size = new Size(682, 470);
            _lvwIssues.Sorting = SortOrder.Ascending;
            _lvwIssues.TabIndex = 0;
            _lvwIssues.UseCompatibleStateImageBehavior = false;
            _lvwIssues.View = View.Details;
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
            decoratorPanel1.ResumeLayout(false);
            decoratorPanel2.ResumeLayout(false);
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
        private ToolStripStatusLabel _tslTime;
        private ToolStrip _toolStrip;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem quitToolStripMenuItem;
        private SplitContainer splitContainer1;
        private Controls.GitHubExplorerTreeView _tvwExplorerTree;
        private Controls.GitHubIssuesListView _lvwIssues;
        private ToolStripMenuItem addQueryToolStripMenuItem;
        private ToolStripMenuItem agentsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem hideContentFromCaptureToolStripMenuItem;
        private ToolStripMenuItem hideWindowFromCaptureToolStripMenuItem;
        private ToolStripStatusLabel _tslUser;
        private ToolStripDropDownButton _tsdCurrentOrg;
        private ToolStripStatusLabel _tslInfo;
        private CommunityToolkit.WinForms.Containers.DecoratorPanel decoratorPanel1;
        private CommunityToolkit.WinForms.Containers.DecoratorPanel decoratorPanel2;
        private ToolStripStatusLabel _tslUserCaption;
        private ToolStripStatusLabel _tslStatusCaption;
        private ToolStripStatusLabel _tslOrgCaption;
        private ToolStripSeparator toolStripSeparator1;
    }
}
