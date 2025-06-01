<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Button1 = New Button()
        Button2 = New Button()
        Panel1 = New Panel()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(959, 19)
        Button1.Margin = New Padding(10)
        Button1.Name = "Button1"
        Button1.Size = New Size(200, 80)
        Button1.TabIndex = 0
        Button1.Text = "OK"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(959, 119)
        Button2.Margin = New Padding(10)
        Button2.Name = "Button2"
        Button2.Size = New Size(200, 80)
        Button2.TabIndex = 1
        Button2.Text = "Cancel"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Panel1
        ' 
        Panel1.Location = New Point(19, 19)
        Panel1.Margin = New Padding(10)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(920, 506)
        Panel1.TabIndex = 2
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 544)
        Controls.Add(Panel1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Margin = New Padding(2)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Panel1 As Panel

End Class
