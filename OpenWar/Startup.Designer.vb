﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Startup
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
        Me.txtHumanPlayerName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAIPlayerName = New System.Windows.Forms.TextBox()
        Me.btnHumanPlayerRandomName = New System.Windows.Forms.Button()
        Me.btnAIPlayerRandomName = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtHumanPlayerName
        '
        Me.txtHumanPlayerName.Location = New System.Drawing.Point(89, 12)
        Me.txtHumanPlayerName.Name = "txtHumanPlayerName"
        Me.txtHumanPlayerName.Size = New System.Drawing.Size(156, 20)
        Me.txtHumanPlayerName.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Human Player"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "AI Player"
        '
        'txtAIPlayerName
        '
        Me.txtAIPlayerName.Location = New System.Drawing.Point(89, 38)
        Me.txtAIPlayerName.Name = "txtAIPlayerName"
        Me.txtAIPlayerName.Size = New System.Drawing.Size(156, 20)
        Me.txtAIPlayerName.TabIndex = 2
        '
        'btnHumanPlayerRandomName
        '
        Me.btnHumanPlayerRandomName.Location = New System.Drawing.Point(251, 13)
        Me.btnHumanPlayerRandomName.Name = "btnHumanPlayerRandomName"
        Me.btnHumanPlayerRandomName.Size = New System.Drawing.Size(81, 20)
        Me.btnHumanPlayerRandomName.TabIndex = 4
        Me.btnHumanPlayerRandomName.Text = "Random"
        Me.btnHumanPlayerRandomName.UseVisualStyleBackColor = True
        '
        'btnAIPlayerRandomName
        '
        Me.btnAIPlayerRandomName.Location = New System.Drawing.Point(251, 38)
        Me.btnAIPlayerRandomName.Name = "btnAIPlayerRandomName"
        Me.btnAIPlayerRandomName.Size = New System.Drawing.Size(81, 20)
        Me.btnAIPlayerRandomName.TabIndex = 5
        Me.btnAIPlayerRandomName.Text = "Random"
        Me.btnAIPlayerRandomName.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(111, 64)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(115, 22)
        Me.btnStart.TabIndex = 6
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'Startup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(345, 102)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnAIPlayerRandomName)
        Me.Controls.Add(Me.btnHumanPlayerRandomName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtAIPlayerName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtHumanPlayerName)
        Me.Name = "Startup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Player Selection"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtHumanPlayerName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAIPlayerName As System.Windows.Forms.TextBox
    Friend WithEvents btnHumanPlayerRandomName As System.Windows.Forms.Button
    Friend WithEvents btnAIPlayerRandomName As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button

End Class
