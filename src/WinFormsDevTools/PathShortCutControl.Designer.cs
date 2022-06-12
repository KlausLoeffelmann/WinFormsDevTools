namespace WinFormsDevTools
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._pathTextBox = new System.Windows.Forms.TextBox();
            this._revealInExplorerButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._pathTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._revealInExplorerButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 39);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _pathTextBox
            // 
            this._pathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pathTextBox.Location = new System.Drawing.Point(3, 3);
            this._pathTextBox.Name = "_pathTextBox";
            this._pathTextBox.ReadOnly = true;
            this._pathTextBox.Size = new System.Drawing.Size(395, 31);
            this._pathTextBox.TabIndex = 0;
            // 
            // _revealInExplorerButton
            // 
            this._revealInExplorerButton.Location = new System.Drawing.Point(404, 3);
            this._revealInExplorerButton.Name = "_revealInExplorerButton";
            this._revealInExplorerButton.Size = new System.Drawing.Size(63, 31);
            this._revealInExplorerButton.TabIndex = 1;
            this._revealInExplorerButton.Text = "->";
            this._revealInExplorerButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._revealInExplorerButton.UseVisualStyleBackColor = true;
            this._revealInExplorerButton.Click += new System.EventHandler(this._revealInExplorerButton_Click);
            // 
            // PathShortCutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PathShortCutControl";
            this.Size = new System.Drawing.Size(470, 39);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TextBox _pathTextBox;
        private Button _revealInExplorerButton;
    }
}
