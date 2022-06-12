namespace WinFormsToolLib
{
    partial class CommandBatchForm
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
            this._printableTextBox = new WinFormsToolLib.PrintableTextBox();
            this._okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _printableTextBox
            // 
            this._printableTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._printableTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this._printableTextBox.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._printableTextBox.Location = new System.Drawing.Point(14, 17);
            this._printableTextBox.Multiline = true;
            this._printableTextBox.Name = "_printableTextBox";
            this._printableTextBox.ReadOnly = true;
            this._printableTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._printableTextBox.Size = new System.Drawing.Size(885, 443);
            this._printableTextBox.TabIndex = 0;
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.Location = new System.Drawing.Point(728, 474);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(173, 40);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CommandBatchForm
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 526);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._printableTextBox);
            this.Name = "CommandBatchForm";
            this.Text = "ExecuteCommandForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinFormsToolLib.PrintableTextBox _printableTextBox;
        private Button _okButton;
    }
}