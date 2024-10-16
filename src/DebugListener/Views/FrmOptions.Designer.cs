namespace DebugListener.Views
{
    partial class FrmOptions
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
            _txtTimeSpanFormatting = new TextBox();
            groupBox1 = new GroupBox();
            label1 = new Label();
            _btnOK = new Button();
            _btnCancel = new Button();
            label2 = new Label();
            textBox1 = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // _txtTimeSpanFormatting
            // 
            _txtTimeSpanFormatting.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _txtTimeSpanFormatting.Location = new Point(292, 38);
            _txtTimeSpanFormatting.Margin = new Padding(4, 4, 4, 4);
            _txtTimeSpanFormatting.Name = "_txtTimeSpanFormatting";
            _txtTimeSpanFormatting.Size = new Size(491, 31);
            _txtTimeSpanFormatting.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(_txtTimeSpanFormatting);
            groupBox1.Location = new Point(15, 15);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            groupBox1.Size = new Size(802, 371);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Formatting:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 41);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(187, 25);
            label1.TabIndex = 1;
            label1.Text = "Time span formatting:";
            // 
            // _btnOK
            // 
            _btnOK.DialogResult = DialogResult.OK;
            _btnOK.Location = new Point(836, 38);
            _btnOK.Margin = new Padding(4, 4, 4, 4);
            _btnOK.Name = "_btnOK";
            _btnOK.Size = new Size(149, 44);
            _btnOK.TabIndex = 2;
            _btnOK.Text = "OK";
            _btnOK.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            _btnCancel.DialogResult = DialogResult.Cancel;
            _btnCancel.Location = new Point(836, 89);
            _btnCancel.Margin = new Padding(4, 4, 4, 4);
            _btnCancel.Name = "_btnCancel";
            _btnCancel.Size = new Size(149, 44);
            _btnCancel.TabIndex = 3;
            _btnCancel.Text = "Cancel";
            _btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 90);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(196, 25);
            label2.TabIndex = 3;
            label2.Text = "(Date) Time formatting:";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(292, 87);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(491, 31);
            textBox1.TabIndex = 2;
            // 
            // FrmOptions
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 415);
            Controls.Add(_btnCancel);
            Controls.Add(_btnOK);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 4, 4, 4);
            Name = "FrmOptions";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Options";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox _txtTimeSpanFormatting;
        private GroupBox groupBox1;
        private Label label1;
        private Button _btnOK;
        private Button _btnCancel;
        private Label label2;
        private TextBox textBox1;
    }
}