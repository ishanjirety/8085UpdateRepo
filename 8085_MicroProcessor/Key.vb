Public Class Key
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim piyush, ishan As String
        If TextBox1.Text = "456789" Then
            Me.Close()
            UserCreation.Show()


        Else
            MsgBox("Invalid Key", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub Key_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class