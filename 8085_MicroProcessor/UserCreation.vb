Imports System.Data.OleDb
Public Class UserCreation
    Dim check As Boolean = True
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        UsernameCheck()
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Enter Details", MsgBoxStyle.Critical)
        Else
            UsernameCheck()
            If check = True Then
                Try
                    If TextBox2.Text = TextBox3.Text Then
                        conn.Close()
                        conn.Open()
                        cmd = New OleDbCommand("Insert Into Login values('" + TextBox1.Text + "','" + TextBox2.Text + "')", conn)
                        Dim i As Integer = cmd.ExecuteNonQuery
                        If (i > 0) Then
                            conn.Close()
                            MsgBox("User Created Successfully", MsgBoxStyle.Information)
                            Me.Close()
                            LoginForm1.Show()
                        End If
                    Else
                        MsgBox("Password Does Not Match", MsgBoxStyle.Critical)
                        TextBox2.Text = Nothing
                        TextBox3.Text = Nothing
                        TextBox1.Text = Nothing
                        TextBox2.Focus()
                    End If
                Catch ex As Exception
                    MsgBox("Error:", ex.Message)
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                Finally
                    conn.Close()
                End Try
            Else
                MsgBox("Username Already Exists!!", MsgBoxStyle.Critical)
                check = True
                TextBox1.Text = Nothing
                TextBox2.Text = Nothing
                TextBox3.Text = Nothing
            End If
        End If
    End Sub
    Public Sub UsernameCheck()
        conn.Close()
        conn.Open()
        ds.Clear()
        cmd = New OleDbCommand("select Nme From Login ", conn)
        da.SelectCommand = cmd
        da.Fill(ds, "Login")
        Dim a As Integer = ds.Tables("Login").Rows.Count - 1
        For i As Integer = 0 To a
            Dim name As String = ds.Tables("Login").Rows(i)(0).ToString()
            If name = TextBox1.Text Then
                check = False
                Exit For
            End If
        Next
        conn.Close()
    End Sub
End Class