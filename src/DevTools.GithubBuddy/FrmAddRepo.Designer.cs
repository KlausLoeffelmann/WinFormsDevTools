namespace DevTools.GitHubBuddy
{
    partial class FrmAddRepo
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _mainTableLayout = new TableLayoutPanel();
            _tlpButtonLayout = new TableLayoutPanel();
            button1 = new Button();
            _btnOK = new Button();
            _grpDataGroup = new GroupBox();
            _tlpDataLayout = new TableLayoutPanel();
            label1 = new Label();
            _mainTableLayout.SuspendLayout();
            _tlpButtonLayout.SuspendLayout();
            _grpDataGroup.SuspendLayout();
            _tlpDataLayout.SuspendLayout();
            SuspendLayout();
            // 
            // _mainTableLayout
            // 
            _mainTableLayout.ColumnCount = 2;
            _mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _mainTableLayout.ColumnStyles.Add(new ColumnStyle());
            _mainTableLayout.Controls.Add(_tlpButtonLayout, 1, 0);
            _mainTableLayout.Controls.Add(_grpDataGroup, 0, 0);
            _mainTableLayout.Location = new Point(30, 32);
            _mainTableLayout.Margin = new Padding(4);
            _mainTableLayout.Name = "_mainTableLayout";
            _mainTableLayout.RowCount = 1;
            _mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _mainTableLayout.Size = new Size(1192, 649);
            _mainTableLayout.TabIndex = 0;
            // 
            // _tlpButtonLayout
            // 
            _tlpButtonLayout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _tlpButtonLayout.AutoSize = true;
            _tlpButtonLayout.ColumnCount = 1;
            _tlpButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _tlpButtonLayout.Controls.Add(button1, 0, 1);
            _tlpButtonLayout.Controls.Add(_btnOK, 0, 0);
            _tlpButtonLayout.Location = new Point(996, 4);
            _tlpButtonLayout.Margin = new Padding(24, 4, 4, 4);
            _tlpButtonLayout.Name = "_tlpButtonLayout";
            _tlpButtonLayout.RowCount = 2;
            _tlpButtonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            _tlpButtonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            _tlpButtonLayout.Size = new Size(192, 144);
            _tlpButtonLayout.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(6, 78);
            button1.Margin = new Padding(6);
            button1.Name = "button1";
            button1.Size = new Size(180, 60);
            button1.TabIndex = 1;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            // 
            // _btnOK
            // 
            _btnOK.Location = new Point(6, 6);
            _btnOK.Margin = new Padding(6);
            _btnOK.Name = "_btnOK";
            _btnOK.Size = new Size(180, 60);
            _btnOK.TabIndex = 0;
            _btnOK.Text = "OK";
            _btnOK.UseVisualStyleBackColor = true;
            // 
            // _grpDataGroup
            // 
            _grpDataGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grpDataGroup.Controls.Add(_tlpDataLayout);
            _grpDataGroup.Location = new Point(3, 3);
            _grpDataGroup.Name = "_grpDataGroup";
            _grpDataGroup.Size = new Size(966, 643);
            _grpDataGroup.TabIndex = 1;
            _grpDataGroup.TabStop = false;
            _grpDataGroup.Text = "Repositories for Org: personal";
            // 
            // _tlpDataLayout
            // 
            _tlpDataLayout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _tlpDataLayout.ColumnCount = 2;
            _tlpDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _tlpDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _tlpDataLayout.Controls.Add(label1, 0, 0);
            _tlpDataLayout.Location = new Point(16, 48);
            _tlpDataLayout.Margin = new Padding(4);
            _tlpDataLayout.Name = "_tlpDataLayout";
            _tlpDataLayout.RowCount = 2;
            _tlpDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            _tlpDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            _tlpDataLayout.Size = new Size(943, 588);
            _tlpDataLayout.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 0);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(121, 30);
            label1.TabIndex = 0;
            label1.Text = "Repository:";
            // 
            // FrmAddRepo
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1255, 705);
            Controls.Add(_mainTableLayout);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormScreenCaptureMode = ScreenCaptureMode.Allow;
            Margin = new Padding(4);
            Name = "FrmAddRepo";
            Text = "Add existing Repo:";
            _mainTableLayout.ResumeLayout(false);
            _mainTableLayout.PerformLayout();
            _tlpButtonLayout.ResumeLayout(false);
            _grpDataGroup.ResumeLayout(false);
            _tlpDataLayout.ResumeLayout(false);
            _tlpDataLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel _mainTableLayout;
        private TableLayoutPanel _tlpButtonLayout;
        private Button button1;
        private Button _btnOK;
        private GroupBox _grpDataGroup;
        private TableLayoutPanel _tlpDataLayout;
        private Label label1;
    }
}