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
            cmd = New OleDbCommand("Select * From login where Nme='" + UsernameTextBox.Text + "'and Password='" + PasswordTextBox.Text + "'", conn)
            da = New OleDbDataAdapter(cmd)
            ds = New Data.DataSet
            da.Fill(ds)
            Dim i As Integer
            i = ds.Tables(0).Rows.Count
            If i = 0 Then
                MsgBox("Login Failed Check Your Username/Password", MsgBoxStyle.Critical)
                conn.Close()
            Else
                login = True
                Intorduction.LinkLabel1.Hide()
                Intorduction.Label1.Hide()
                LinkLabel1.Hide()
                conn.Close()
                Intorduction.Show()
                Me.Close()

            End If
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
        Intorduction.Show()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        'Me.Close()
        Key.Show()
    End Sub

    Private Sub LoginForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
