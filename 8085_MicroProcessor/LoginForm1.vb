Imports System.Data.OleDb
Public Class LoginForm1
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If UsernameTextBox.Text = Nothing Then
            MsgBox("Enter proper requirements!", MsgBoxStyle.Critical)

        ElseIf PasswordTextBox.Text = Nothing Then
            MsgBox("Enter proper requirements!", MsgBoxStyle.Critical)
            UsernameTextBox.Text = Nothing
            UsernameTextBox.Text = Nothing
        Else
            conn.Open()
            Dim cmd As New OleDbCommand
            cmd = New OleDbCommand("SELECT * FROM login WHERE Nme='" + UsernameTextBox.Text + "'and password = '" + PasswordTextBox.Text + "'", conn)

            da = New OleDbDataAdapter(cmd)

            da.Fill(ds)

            Dim i As Integer
            i = ds.Tables(0).Rows.Count
            If i = 0 Then
                MsgBox("Login failed!", MsgBoxStyle.Critical)
                conn.Close()
            Else
                MsgBox("Login Successfull!", MsgBoxStyle.Information)
                login = True
                Intorduction.LinkLabel1.Hide()
                Intorduction.Label1.Hide()
                Intorduction.LinkLabel4.Location = New Point(12, 9)
                conn.Close()
                Me.Close()
            End If
            Intorduction.LinkLabel3.Enabled = True

        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

End Class
