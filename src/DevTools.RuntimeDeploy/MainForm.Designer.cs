using WarpToolkit.WinForms.Containers;

namespace DevTools.RuntimeDeploy;

partial class MainForm
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
        _tabControl = new FluentTabControl();
        SuspendLayout();
        // 
        // _tabControl
        // 
        _tabControl.Dock = DockStyle.Fill;
        _tabControl.Location = new Point(5, 5);
        _tabControl.Margin = new Padding(4, 4, 4, 4);
        _tabControl.Name = "_tabControl";
        _tabControl.Size = new Size(1246, 726);
        _tabControl.TabIndex = 0;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(14F, 36F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1256, 736);
        Controls.Add(_tabControl);
        Font = new Font("Segoe UI", 11.1428576F, FontStyle.Regular, GraphicsUnit.Point, 0);
        Margin = new Padding(4, 5, 4, 5);
        MinimumSize = new Size(1280, 800);
        Name = "MainForm";
        Padding = new Padding(5);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "WinFormsDevTool";
        ResumeLayout(false);
    }

    #endregion

    private FluentTabControl _tabControl;
}