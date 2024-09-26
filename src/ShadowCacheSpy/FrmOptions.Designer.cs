namespace ShadowCacheSpy
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
            _btnOK = new Button();
            _btnCancel = new Button();
            groupBox1 = new GroupBox();
            _watchFolderPicker = new CommunityToolkit.WinForms.Controls.FilePathPicker();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // _btnOK
            // 
            _btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnOK.DialogResult = DialogResult.OK;
            _btnOK.Location = new Point(811, 26);
            _btnOK.Margin = new Padding(4);
            _btnOK.Name = "_btnOK";
            _btnOK.Size = new Size(136, 39);
            _btnOK.TabIndex = 0;
            _btnOK.Text = "OK";
            _btnOK.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            _btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnCancel.DialogResult = DialogResult.Cancel;
            _btnCancel.Location = new Point(811, 73);
            _btnCancel.Margin = new Padding(4);
            _btnCancel.Name = "_btnCancel";
            _btnCancel.Size = new Size(136, 41);
            _btnCancel.TabIndex = 1;
            _btnCancel.Text = "Cancel";
            _btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(_watchFolderPicker);
            groupBox1.Location = new Point(14, 14);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(761, 511);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Watch Settings";
            // 
            // _watchFolderPicker
            // 
            _watchFolderPicker.FilePath = "";
            _watchFolderPicker.FilePathType = CommunityToolkit.WinForms.Controls.FilePathType.Folder;
            _watchFolderPicker.Location = new Point(21, 59);
            _watchFolderPicker.Margin = new Padding(4);
            _watchFolderPicker.Name = "_watchFolderPicker";
            _watchFolderPicker.Size = new Size(732, 31);
            _watchFolderPicker.TabIndex = 0;
            _watchFolderPicker.Text = "filePathPicker1";
            // 
            // FrmOptions
            // 
            AcceptButton = _btnOK;
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _btnCancel;
            ClientSize = new Size(960, 540);
            Controls.Add(groupBox1);
            Controls.Add(_btnCancel);
            Controls.Add(_btnOK);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "FrmOptions";
            Text = "Options";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button _btnOK;
        private Button _btnCancel;
        private GroupBox groupBox1;
        private CommunityToolkit.WinForms.Controls.FilePathPicker _watchFolderPicker;
    }
}