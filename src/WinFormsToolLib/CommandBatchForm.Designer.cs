namespace WinFormsDevToolsLib
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
            _printableTextBox.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            _printableTextBox.Location = new Point(10, 10);
            _printableTextBox.Margin = new Padding(2, 2, 2, 2);
            _printableTextBox.Multiline = true;
            _printableTextBox.Name = "_printableTextBox";
            _printableTextBox.ReadOnly = true;
            _printableTextBox.ScrollBars = ScrollBars.Both;
            _printableTextBox.Size = new Size(821, 375);
            _printableTextBox.TabIndex = 0;
            // 
            // _okButton
            // 
            _okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _okButton.Location = new Point(710, 392);
            _okButton.Margin = new Padding(2, 2, 2, 2);
            _okButton.Name = "_okButton";
            _okButton.Size = new Size(121, 24);
            _okButton.TabIndex = 1;
            _okButton.Text = "OK";
            _okButton.UseVisualStyleBackColor = true;
            _okButton.Click += OkButton_Click;
            // 
            // CommandBatchForm
            // 
            AcceptButton = _okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(838, 424);
            Controls.Add(_okButton);
            Controls.Add(_printableTextBox);
            Margin = new Padding(2, 2, 2, 2);
            Name = "CommandBatchForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ExecuteCommandForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WinFormsDevToolsLib.PrintableTextBox _printableTextBox;
        private Button _okButton;
    }
}