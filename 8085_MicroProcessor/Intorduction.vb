Public Class Intorduction
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If login = True Then
            MsgBox("Already logged in!")
        Else
            LoginForm1.Show()
        End If
    End Sub

    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        If login = True Then
            Form1.Show()
        Else
            LoginForm1.Show()
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If login = True Then
            Form1.Show()
        Else
            MsgBox("You Need To Login!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub LinkLabel4_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        If login = True Then
            login = False
            MsgBox("Logged out!")
            LinkLabel1.Show()
            Label1.Show()
            LinkLabel4.Location = New Point(80, 8)
        Else
            MsgBox("Need to login!")
        End If
    End Sub
End Class