Public Class Key
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "e8fae442-e726" Then
            Me.Close()
            UserCreation.Show()

        Else
            MsgBox("Invalid Key", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub Key_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class