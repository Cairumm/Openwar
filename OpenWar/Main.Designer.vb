<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnBuildBase = New System.Windows.Forms.Button()
        Me.btnSpy = New System.Windows.Forms.Button()
        Me.btnDeclareWar = New System.Windows.Forms.Button()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.btnBuildABMBase = New System.Windows.Forms.Button()
        Me.btnBuildSubBase = New System.Windows.Forms.Button()
        Me.btnBuildMissileBase = New System.Windows.Forms.Button()
        Me.btnBuildBomberBase = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnBuildBase
        '
        Me.btnBuildBase.Location = New System.Drawing.Point(783, 38)
        Me.btnBuildBase.Name = "btnBuildBase"
        Me.btnBuildBase.Size = New System.Drawing.Size(123, 19)
        Me.btnBuildBase.TabIndex = 0
        Me.btnBuildBase.Text = "Build"
        Me.btnBuildBase.UseVisualStyleBackColor = True
        '
        'btnSpy
        '
        Me.btnSpy.Location = New System.Drawing.Point(783, 63)
        Me.btnSpy.Name = "btnSpy"
        Me.btnSpy.Size = New System.Drawing.Size(123, 19)
        Me.btnSpy.TabIndex = 1
        Me.btnSpy.Text = "Spy"
        Me.btnSpy.UseVisualStyleBackColor = True
        '
        'btnDeclareWar
        '
        Me.btnDeclareWar.Location = New System.Drawing.Point(783, 88)
        Me.btnDeclareWar.Name = "btnDeclareWar"
        Me.btnDeclareWar.Size = New System.Drawing.Size(123, 19)
        Me.btnDeclareWar.TabIndex = 2
        Me.btnDeclareWar.Text = "Declare War"
        Me.btnDeclareWar.UseVisualStyleBackColor = True
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(783, 12)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(123, 20)
        Me.txtMessage.TabIndex = 3
        '
        'btnBuildABMBase
        '
        Me.btnBuildABMBase.Location = New System.Drawing.Point(783, 183)
        Me.btnBuildABMBase.Name = "btnBuildABMBase"
        Me.btnBuildABMBase.Size = New System.Drawing.Size(123, 19)
        Me.btnBuildABMBase.TabIndex = 6
        Me.btnBuildABMBase.Text = "ABM"
        Me.btnBuildABMBase.UseVisualStyleBackColor = True
        '
        'btnBuildSubBase
        '
        Me.btnBuildSubBase.Location = New System.Drawing.Point(783, 158)
        Me.btnBuildSubBase.Name = "btnBuildSubBase"
        Me.btnBuildSubBase.Size = New System.Drawing.Size(123, 19)
        Me.btnBuildSubBase.TabIndex = 5
        Me.btnBuildSubBase.Text = "Sub"
        Me.btnBuildSubBase.UseVisualStyleBackColor = True
        '
        'btnBuildMissileBase
        '
        Me.btnBuildMissileBase.Location = New System.Drawing.Point(783, 133)
        Me.btnBuildMissileBase.Name = "btnBuildMissileBase"
        Me.btnBuildMissileBase.Size = New System.Drawing.Size(123, 19)
        Me.btnBuildMissileBase.TabIndex = 4
        Me.btnBuildMissileBase.Text = "Missile"
        Me.btnBuildMissileBase.UseVisualStyleBackColor = True
        '
        'btnBuildBomberBase
        '
        Me.btnBuildBomberBase.Location = New System.Drawing.Point(783, 208)
        Me.btnBuildBomberBase.Name = "btnBuildBomberBase"
        Me.btnBuildBomberBase.Size = New System.Drawing.Size(123, 19)
        Me.btnBuildBomberBase.TabIndex = 7
        Me.btnBuildBomberBase.Text = "Bomber"
        Me.btnBuildBomberBase.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 539)
        Me.Controls.Add(Me.btnBuildBomberBase)
        Me.Controls.Add(Me.btnBuildABMBase)
        Me.Controls.Add(Me.btnBuildSubBase)
        Me.Controls.Add(Me.btnBuildMissileBase)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.btnDeclareWar)
        Me.Controls.Add(Me.btnSpy)
        Me.Controls.Add(Me.btnBuildBase)
        Me.Name = "Main"
        Me.Text = "Main"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuildBase As System.Windows.Forms.Button
    Friend WithEvents btnSpy As System.Windows.Forms.Button
    Friend WithEvents btnDeclareWar As System.Windows.Forms.Button
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnBuildABMBase As System.Windows.Forms.Button
    Friend WithEvents btnBuildSubBase As System.Windows.Forms.Button
    Friend WithEvents btnBuildMissileBase As System.Windows.Forms.Button
    Friend WithEvents btnBuildBomberBase As System.Windows.Forms.Button
End Class
