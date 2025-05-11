using CToolkit.WinForms.Containers;

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
        _tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _tabControl.Location = new Point(20, 23);
        _tabControl.Name = "_tabControl";
        _tabControl.Size = new Size(1384, 845);
        _tabControl.TabIndex = 0;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(12F, 30F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1429, 893);
        Controls.Add(_tabControl);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(1339, 722);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "WinFormsDevTool";
        ResumeLayout(false);
    }

    #endregion

    private FluentTabControl _tabControl;
}