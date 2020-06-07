Imports System.Data.OleDb
Public Class loginpage
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = Nothing Then
            MsgBox("Enter proper requirements!", MsgBoxStyle.Critical)

        ElseIf TextBox2.Text = Nothing Then
            MsgBox("Enter proper requirements!", MsgBoxStyle.Critical)
            TextBox1.Text = Nothing
            TextBox2.Text = Nothing
        Else
            conn.Open()
            Dim cmd As New OleDbCommand
            cmd = New OleDbCommand("SELECT * FROM login WHERE Nme='" + TextBox1.Text + "'and password = '" + TextBox2.Text + "'", conn)

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

End Class