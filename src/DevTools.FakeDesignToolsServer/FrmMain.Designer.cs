namespace DevTools.FakeDesignToolsServer
{
    partial class FrmMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            _notifyIcon = new NotifyIcon(components);
            _contextMenu = new ContextMenuStrip(components);
            _tsmQuitProcess = new ToolStripMenuItem();
            _contextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // _notifyIcon
            // 
            _notifyIcon.ContextMenuStrip = _contextMenu;
            _notifyIcon.Icon = (Icon)resources.GetObject("_notifyIcon.Icon");
            _notifyIcon.Text = "Fake .NET Design Process";
            _notifyIcon.Visible = true;
            // 
            // _contextMenu
            // 
            _contextMenu.ImageScalingSize = new Size(20, 20);
            _contextMenu.Items.AddRange(new ToolStripItem[] { _tsmQuitProcess });
            _contextMenu.Name = "_tsmQuitProcess";
            _contextMenu.Size = new Size(320, 56);
            // 
            // _tsmQuitProcess
            // 
            _tsmQuitProcess.Name = "_tsmQuitProcess";
            _tsmQuitProcess.Size = new Size(319, 24);
            _tsmQuitProcess.Text = "Quit Fake DesignToolsServer process";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FrmMain";
            Opacity = 0D;
            ShowInTaskbar = false;
            Text = "Form1";
            WindowState = FormWindowState.Minimized;
            _contextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon _notifyIcon;
        private ContextMenuStrip _contextMenu;
        private ToolStripMenuItem _tsmQuitProcess;
    }
}
