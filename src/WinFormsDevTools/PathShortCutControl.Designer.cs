namespace WfRuntimeDeploy
{
    partial class PathShortCutControl
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
            tableLayoutPanel1 = new TableLayoutPanel();
            _pathTextBox = new TextBox();
            _revealInExplorerButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(_pathTextBox, 0, 0);
            tableLayoutPanel1.Controls.Add(_revealInExplorerButton, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4, 4, 4, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(552, 49);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // _pathTextBox
            // 
            _pathTextBox.Dock = DockStyle.Fill;
            _pathTextBox.Location = new Point(4, 4);
            _pathTextBox.Margin = new Padding(4, 4, 4, 4);
            _pathTextBox.Name = "_pathTextBox";
            _pathTextBox.ReadOnly = true;
            _pathTextBox.Size = new Size(460, 37);
            _pathTextBox.TabIndex = 0;
            // 
            // _revealInExplorerButton
            // 
            _revealInExplorerButton.Location = new Point(472, 4);
            _revealInExplorerButton.Margin = new Padding(4, 4, 4, 4);
            _revealInExplorerButton.Name = "_revealInExplorerButton";
            _revealInExplorerButton.Size = new Size(76, 37);
            _revealInExplorerButton.TabIndex = 1;
            _revealInExplorerButton.Text = "->";
            _revealInExplorerButton.TextAlign = ContentAlignment.TopCenter;
            _revealInExplorerButton.UseVisualStyleBackColor = true;
            _revealInExplorerButton.Click += _revealInExplorerButton_Click;
            // 
            // PathShortCutControl
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "PathShortCutControl";
            Size = new Size(552, 49);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TextBox _pathTextBox;
        private Button _revealInExplorerButton;
    }
}
