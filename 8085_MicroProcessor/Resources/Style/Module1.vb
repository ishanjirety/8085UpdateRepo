Imports System.Drawing.Text
Imports System.Runtime.InteropServices
Module Module1
    Private _f As PrivateFontCollection
    Public ReadOnly Property Getinstance(ByVal Size As Single, ByVal Style As FontStyle)
        Get
            If _f Is Nothing Then LoadFont()
            Return New Font(_f.Families(0), Size, Style)
        End Get
    End Property

    Private Sub LoadFont()
        Try
            _f = New PrivateFontCollection
            Dim fontPointer As IntPtr = Marshal.AllocCoTaskMem(My.Resources.fonts.DSEG7Modern_Bold.Length)
            Marshal.Copy(My.Resources.fonts.DSEG7Modern_Bold, 0, fontPointer, My.Resources.fonts.DSEG7Modern_Bold.Length)
            _f.AddMemoryFont(fontPointer, My.Resources.fonts.DSEG7Modern_Bold.Length)
            Marshal.FreeCoTaskMem(fontPointer)
        Catch ex As Exception
        End Try
    End Sub

End Module
