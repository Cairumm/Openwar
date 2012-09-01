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
        Me.btnTurnAction_Build = New System.Windows.Forms.Button()
        Me.btnTurnAction_Spy = New System.Windows.Forms.Button()
        Me.btnTurnAction_War = New System.Windows.Forms.Button()
        Me.btnBuildBase_ABM = New System.Windows.Forms.Button()
        Me.btnBuildBase_Sub = New System.Windows.Forms.Button()
        Me.btnBuildBase_Missile = New System.Windows.Forms.Button()
        Me.btnBuildBase_Bomber = New System.Windows.Forms.Button()
        Me.btnConfirmSelection = New System.Windows.Forms.Button()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RectangleShape2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.SuspendLayout()
        '
        'btnTurnAction_Build
        '
        Me.btnTurnAction_Build.Location = New System.Drawing.Point(790, 10)
        Me.btnTurnAction_Build.Name = "btnTurnAction_Build"
        Me.btnTurnAction_Build.Size = New System.Drawing.Size(100, 100)
        Me.btnTurnAction_Build.TabIndex = 0
        Me.btnTurnAction_Build.Text = "Build"
        Me.btnTurnAction_Build.UseVisualStyleBackColor = True
        '
        'btnTurnAction_Spy
        '
        Me.btnTurnAction_Spy.Location = New System.Drawing.Point(790, 116)
        Me.btnTurnAction_Spy.Name = "btnTurnAction_Spy"
        Me.btnTurnAction_Spy.Size = New System.Drawing.Size(100, 100)
        Me.btnTurnAction_Spy.TabIndex = 1
        Me.btnTurnAction_Spy.Text = "Spy"
        Me.btnTurnAction_Spy.UseVisualStyleBackColor = True
        '
        'btnTurnAction_War
        '
        Me.btnTurnAction_War.Location = New System.Drawing.Point(790, 222)
        Me.btnTurnAction_War.Name = "btnTurnAction_War"
        Me.btnTurnAction_War.Size = New System.Drawing.Size(100, 100)
        Me.btnTurnAction_War.TabIndex = 2
        Me.btnTurnAction_War.Text = "Declare War"
        Me.btnTurnAction_War.UseVisualStyleBackColor = True
        '
        'btnBuildBase_ABM
        '
        Me.btnBuildBase_ABM.Location = New System.Drawing.Point(790, 328)
        Me.btnBuildBase_ABM.Name = "btnBuildBase_ABM"
        Me.btnBuildBase_ABM.Size = New System.Drawing.Size(100, 100)
        Me.btnBuildBase_ABM.TabIndex = 6
        Me.btnBuildBase_ABM.Text = "ABM"
        Me.btnBuildBase_ABM.UseVisualStyleBackColor = True
        '
        'btnBuildBase_Sub
        '
        Me.btnBuildBase_Sub.Location = New System.Drawing.Point(790, 222)
        Me.btnBuildBase_Sub.Name = "btnBuildBase_Sub"
        Me.btnBuildBase_Sub.Size = New System.Drawing.Size(100, 100)
        Me.btnBuildBase_Sub.TabIndex = 5
        Me.btnBuildBase_Sub.Text = "Sub"
        Me.btnBuildBase_Sub.UseVisualStyleBackColor = True
        '
        'btnBuildBase_Missile
        '
        Me.btnBuildBase_Missile.Location = New System.Drawing.Point(790, 10)
        Me.btnBuildBase_Missile.Name = "btnBuildBase_Missile"
        Me.btnBuildBase_Missile.Size = New System.Drawing.Size(100, 100)
        Me.btnBuildBase_Missile.TabIndex = 4
        Me.btnBuildBase_Missile.Text = "Missile"
        Me.btnBuildBase_Missile.UseVisualStyleBackColor = True
        '
        'btnBuildBase_Bomber
        '
        Me.btnBuildBase_Bomber.Location = New System.Drawing.Point(790, 116)
        Me.btnBuildBase_Bomber.Name = "btnBuildBase_Bomber"
        Me.btnBuildBase_Bomber.Size = New System.Drawing.Size(100, 100)
        Me.btnBuildBase_Bomber.TabIndex = 7
        Me.btnBuildBase_Bomber.Text = "Bomber"
        Me.btnBuildBase_Bomber.UseVisualStyleBackColor = True
        '
        'btnConfirmSelection
        '
        Me.btnConfirmSelection.Location = New System.Drawing.Point(790, 10)
        Me.btnConfirmSelection.Name = "btnConfirmSelection"
        Me.btnConfirmSelection.Size = New System.Drawing.Size(100, 100)
        Me.btnConfirmSelection.TabIndex = 8
        Me.btnConfirmSelection.Text = "Confirm Selection"
        Me.btnConfirmSelection.UseVisualStyleBackColor = True
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape2, Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(955, 539)
        Me.ShapeContainer1.TabIndex = 9
        Me.ShapeContainer1.TabStop = False
        '
        'RectangleShape1
        '
        Me.RectangleShape1.Location = New System.Drawing.Point(10, 10)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(360, 436)
        '
        'RectangleShape2
        '
        Me.RectangleShape2.Location = New System.Drawing.Point(380, 10)
        Me.RectangleShape2.Name = "RectangleShape2"
        Me.RectangleShape2.Size = New System.Drawing.Size(360, 436)
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 539)
        Me.Controls.Add(Me.btnConfirmSelection)
        Me.Controls.Add(Me.btnBuildBase_Bomber)
        Me.Controls.Add(Me.btnBuildBase_ABM)
        Me.Controls.Add(Me.btnBuildBase_Sub)
        Me.Controls.Add(Me.btnBuildBase_Missile)
        Me.Controls.Add(Me.btnTurnAction_War)
        Me.Controls.Add(Me.btnTurnAction_Spy)
        Me.Controls.Add(Me.btnTurnAction_Build)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Name = "Main"
        Me.Text = "Main"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnTurnAction_Build As System.Windows.Forms.Button
    Friend WithEvents btnTurnAction_Spy As System.Windows.Forms.Button
    Friend WithEvents btnTurnAction_War As System.Windows.Forms.Button
    Friend WithEvents btnBuildBase_ABM As System.Windows.Forms.Button
    Friend WithEvents btnBuildBase_Sub As System.Windows.Forms.Button
    Friend WithEvents btnBuildBase_Missile As System.Windows.Forms.Button
    Friend WithEvents btnBuildBase_Bomber As System.Windows.Forms.Button
    Friend WithEvents btnConfirmSelection As System.Windows.Forms.Button
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents RectangleShape2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
End Class
