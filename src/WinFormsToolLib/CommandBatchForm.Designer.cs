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
            _printableTextBox = new PrintableTextBox();
            _okButton = new Button();
            SuspendLayout();
            // 
            // _printableTextBox
            // 
            _printableTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _printableTextBox.BackColor = SystemColors.ButtonHighlight;
            _printableTextBox.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            _printableTextBox.Location = new Point(12, 16);
            _printableTextBox.Margin = new Padding(2, 2, 2, 2);
            _printableTextBox.Multiline = true;
            _printableTextBox.Name = "_printableTextBox";
            _printableTextBox.ReadOnly = true;
            _printableTextBox.ScrollBars = ScrollBars.Both;
            _printableTextBox.Size = new Size(797, 408);
            _printableTextBox.TabIndex = 0;
            // 
            // _okButton
            // 
            _okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _okButton.Location = new Point(655, 436);
            _okButton.Margin = new Padding(2, 2, 2, 2);
            _okButton.Name = "_okButton";
            _okButton.Size = new Size(155, 37);
            _okButton.TabIndex = 1;
            _okButton.Text = "OK";
            _okButton.UseVisualStyleBackColor = true;
            _okButton.Click += OkButton_Click;
            // 
            // CommandBatchForm
            // 
            AcceptButton = _okButton;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 484);
            Controls.Add(_okButton);
            Controls.Add(_printableTextBox);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(2, 2, 2, 2);
            Name = "CommandBatchForm";
            Text = "ExecuteCommandForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WinFormsToolLib.PrintableTextBox _printableTextBox;
        private Button _okButton;
    }
}