Public Class Key
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "e8fae442-e726" Then
            Me.Close()
            UserCreation.Show()

        Else
            MsgBox("Invalid Key", MsgBoxStyle.Critical)
        End If
    End Sub
End Class