namespace DevTools.RuntimeDeploy.Views
{
    partial class DeleteBinariesView
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
            decoratorPanel1 = new CToolkit.WinForms.Containers.DecoratorPanel();
            splitContainer1 = new SplitContainer();
            _tlpSourceFiles = new TableLayoutPanel();
            _btnAddFolders = new Button();
            _lvwSourceFolders = new ListView();
            _lvwPurgeTargets = new ListView();
            decoratorPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            _tlpSourceFiles.SuspendLayout();
            SuspendLayout();
            // 
            // decoratorPanel1
            // 
            decoratorPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            decoratorPanel1.BorderThickness = 1;
            decoratorPanel1.Controls.Add(splitContainer1);
            decoratorPanel1.Location = new Point(3, 3);
            decoratorPanel1.MinimumSize = new Size(28, 28);
            decoratorPanel1.Name = "decoratorPanel1";
            decoratorPanel1.Size = new Size(985, 565);
            decoratorPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(4, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(_tlpSourceFiles);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_lvwPurgeTargets);
            splitContainer1.Size = new Size(977, 557);
            splitContainer1.SplitterDistance = 325;
            splitContainer1.TabIndex = 0;
            // 
            // _tlpSourceFiles
            // 
            _tlpSourceFiles.ColumnCount = 1;
            _tlpSourceFiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _tlpSourceFiles.Controls.Add(_btnAddFolders, 0, 0);
            _tlpSourceFiles.Controls.Add(_lvwSourceFolders, 0, 1);
            _tlpSourceFiles.Dock = DockStyle.Fill;
            _tlpSourceFiles.Location = new Point(0, 0);
            _tlpSourceFiles.Name = "_tlpSourceFiles";
            _tlpSourceFiles.RowCount = 2;
            _tlpSourceFiles.RowStyles.Add(new RowStyle());
            _tlpSourceFiles.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _tlpSourceFiles.Size = new Size(325, 557);
            _tlpSourceFiles.TabIndex = 0;
            // 
            // _btnAddFolders
            // 
            _btnAddFolders.Dock = DockStyle.Top;
            _btnAddFolders.Location = new Point(10, 10);
            _btnAddFolders.Margin = new Padding(10);
            _btnAddFolders.Name = "_btnAddFolders";
            _btnAddFolders.Size = new Size(305, 48);
            _btnAddFolders.TabIndex = 3;
            _btnAddFolders.Text = "Add for bin purging...";
            _btnAddFolders.UseVisualStyleBackColor = true;
            _btnAddFolders.Click += AddFolders_Click;
            // 
            // _lvwSourceFolders
            // 
            _lvwSourceFolders.Dock = DockStyle.Fill;
            _lvwSourceFolders.Location = new Point(10, 78);
            _lvwSourceFolders.Margin = new Padding(10);
            _lvwSourceFolders.Name = "_lvwSourceFolders";
            _lvwSourceFolders.Size = new Size(305, 469);
            _lvwSourceFolders.TabIndex = 2;
            _lvwSourceFolders.UseCompatibleStateImageBehavior = false;
            // 
            // _lvwPurgeTargets
            // 
            _lvwPurgeTargets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _lvwPurgeTargets.Location = new Point(14, 10);
            _lvwPurgeTargets.Name = "_lvwPurgeTargets";
            _lvwPurgeTargets.Size = new Size(631, 537);
            _lvwPurgeTargets.TabIndex = 0;
            _lvwPurgeTargets.UseCompatibleStateImageBehavior = false;
            // 
            // DeleteBinariesView
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(decoratorPanel1);
            Name = "DeleteBinariesView";
            Size = new Size(991, 571);
            decoratorPanel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            _tlpSourceFiles.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CToolkit.WinForms.Containers.DecoratorPanel decoratorPanel1;
        private SplitContainer splitContainer1;
        private ListView _lvwPurgeTargets;
        private TableLayoutPanel _tlpSourceFiles;
        private Button _btnAddFolders;
        private ListView _lvwSourceFolders;
    }
}
