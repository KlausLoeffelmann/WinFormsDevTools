using CToolkit.WinForms.Containers;
using CToolkit.WinForms.Tooling;

namespace DevTools.Libs;

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
        _okButton = new Button();
        _console = new ConsoleControl();
        _dpnConsole = new DecoratorPanel();
        _dpnConsole.SuspendLayout();
        SuspendLayout();
        // 
        // _okButton
        // 
        _okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _okButton.Location = new Point(1039, 653);
        _okButton.Name = "_okButton";
        _okButton.Size = new Size(148, 40);
        _okButton.TabIndex = 1;
        _okButton.Text = "OK";
        _okButton.UseVisualStyleBackColor = true;
        _okButton.Click += OkButton_Click;
        // 
        // _console
        // 
        _console.BorderStyle = BorderStyle.None;
        _console.Location = new Point(9, 9);
        _console.Name = "_console";
        _console.Size = new Size(1155, 604);
        _console.TabIndex = 2;
        _console.Text = "";
        // 
        // _dpnConsole
        // 
        _dpnConsole.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _dpnConsole.BorderThickness = 1;
        _dpnConsole.Controls.Add(_console);
        _dpnConsole.Location = new Point(12, 12);
        _dpnConsole.MinimumSize = new Size(28, 28);
        _dpnConsole.Name = "_dpnConsole";
        _dpnConsole.Padding = new Padding(8);
        _dpnConsole.Size = new Size(1173, 622);
        _dpnConsole.TabIndex = 3;
        // 
        // CommandBatchForm
        // 
        AcceptButton = _okButton;
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1197, 707);
        Controls.Add(_dpnConsole);
        Controls.Add(_okButton);
        Name = "CommandBatchForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "ExecuteCommandForm";
        _dpnConsole.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Button _okButton;
    private ConsoleControl _console;
    private DecoratorPanel _dpnConsole;
}