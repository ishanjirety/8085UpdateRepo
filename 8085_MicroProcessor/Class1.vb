﻿Imports System.Drawing
Public Class btn1
    Inherits Windows.Forms.Button
    Public Sub New()
        Me.Size = New Point(32, 32)
        Me.FlatStyle = Windows.Forms.FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0
        Me.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BackgroundImage = My.Resources.download2
        Me.BackgroundImageLayout = Windows.Forms.ImageLayout.Stretch
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub
    Private Sub Backbutton_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BackgroundImage = My.Resources.download2
    End Sub
    Private Sub Backbutton_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BackgroundImage = My.Resources.download1
    End Sub
End Class
