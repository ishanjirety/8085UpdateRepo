Public Class SaveWindow

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = Nothing Then
            Me.ErrorProvider1.SetError(Me.TextBox1, "Program Name Required")
        Else
            NameCheck(TextBox1.Text)
            Form1.ComboBox2.Items.Clear()
            preloadedPrograms()
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Me.ErrorProvider1.SetError(Me.TextBox1, "")
    End Sub
End Class