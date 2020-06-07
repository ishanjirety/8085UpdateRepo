Imports System.Data
Imports System.Data.OleDb
Module DbFunctioins
    Public login As Boolean
    Public MemLocation(50) As String       'Memory Locations
    Public Instructions(50) As String      'Instructioins
    Public da As New OleDbDataAdapter
    Public ds As New DataSet
    Public conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=D:\8085.accdb")
    Public cmd As New OleDbCommand
    Public INTARRAY(100) As String
    Public decision As Boolean
    Public chck As Integer = 0
    Public indexCountTransfer As Integer
    Public outputPlace As String
    Public outputResult As String
    Public ErrorSignal As Integer = 0
    Public Sub Register_Signal(ByVal Data As String)
        Try
            conn.Close()
            conn.Open()
            Form1.TextBox4.ForeColor = Color.Red
            Form1.TextBox3.ForeColor = Color.Red
            cmd = New OleDbCommand("SELECT Hex FROM Instructions WHERE Memory ='" + Data + "'", conn)
            Dim dr As OleDbDataReader = cmd.ExecuteReader()
            If dr.Read() Then
                Dim temp1 As String = dr.GetValue(0)
                Dim temp2 As String = dr.GetValue(0)
                Form1.TextBox6.Text = "8."
                Form1.TextBox7.Text = "8."
                Form1.TextBox8.Text = "8."
                Form1.TextBox6.ForeColor = Color.Firebrick
                Form1.TextBox7.ForeColor = Color.Firebrick
                Form1.TextBox8.ForeColor = Color.Firebrick
                Form1.TextBox5.Text = Data.ToUpper
                Form1.TextBox5.ForeColor = Color.Red
                Form1.TextBox4.Text = temp1.Remove(1, 1)
                Form1.TextBox3.Text = temp2.Remove(0, 1)
            End If
        Catch ex As Exception
            MsgBox("Error", ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub
    Public Sub UpdateInst()                     'Update Instructions
        Dim n As Integer
        conn.Open()
        For n = 0 To MemLocation.Length - 1
            cmd = New OleDbCommand("Update Instructions set Hex='" & Instructions(n) & "' where Memory='" & MemLocation(n) & "'", conn)
            cmd.ExecuteNonQuery()
        Next
        conn.Close()
    End Sub
    Public Sub Show_Hex()
        Try
            conn.Close()
            conn.Open()
            Dim Clubing_Var As String = Form1.TextBox8.Text & Form1.TextBox7.Text & Form1.TextBox6.Text & Form1.TextBox5.Text
            cmd = New OleDbCommand("SELECT Hex FROM Instructions WHERE Memory ='" + Clubing_Var + "'", conn)
            Dim dr As OleDbDataReader = cmd.ExecuteReader()
            If dr.Read() Then
                Dim temp1 As String = dr.GetValue(0)
                Dim temp2 As String = dr.GetValue(0)
                Form1.TextBox4.Text = temp1.Remove(1, 1)
                Form1.TextBox3.Text = temp2.Remove(0, 1)
                Form1.TextBox4.ForeColor = Color.Red
                Form1.TextBox3.ForeColor = Color.Red
            End If
        Catch ex As Exception
            MsgBox("Error", ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub
    Public Sub EraseData()                      ' Function To Erase Existing Data When Hardware Turned Off
        Dim n As Integer
        If chck = 0 Then
            conn.Open()
            For n = 0 To MemLocation.Length - 1
                cmd = New OleDbCommand("Update Instructions set Hex='AA'", conn)
                cmd.ExecuteNonQuery()
            Next
            conn.Close()
        End If
    End Sub
    Public Sub Operation_check(ByVal count As Integer, ByVal syntax_array() As String)
        conn.Open()
        Dim n As Integer = 0
        Try
            For v As Integer = 0 To count
                cmd = New OleDbCommand("SELECT Use FROM Codes WHERE Opcode='" + syntax_array(n) + "'", conn)
                da = New OleDbDataAdapter(cmd)
                ds = New Data.DataSet
                da.Fill(ds)
                Dim i As Integer
                i = ds.Tables(0).Rows.Count
                If i = 0 Then
                    Form1.ListBox1.Items.Add("Wrong HEX " & n)
                    Form1.ListBox2.Items.Add("Wrong HEX " & n)
                    Form1.ListBox3.Items.Add("An error occured")
                    ErrorSignal += 1
                    n = n + 1
                    Form1.Rst()
                    Form1.err()
                Else
                    Dim dr As OleDbDataReader = cmd.ExecuteReader
                    If dr.Read() Then
                        Form1.ComboBox1.Items.Add(dr.GetValue(0))
                        INTARRAY(n) = dr.GetValue(0)
                        n = n + 1
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox("Error", ex.Message)
        End Try
        conn.Close()
    End Sub
    Public Sub CHECKCODE()                                      ''Checks Code Format ADD,SUB,MOV''
        Dim flag As Integer = 0
        Dim c As Integer
        For i As Integer = 0 To INTARRAY.Length - 1
            If INTARRAY(i) = "ADD B" Then                           'IF ADD Code Present
                For c = 0 To INTARRAY.Length - 1
                    If INTARRAY(c) = "B" Then                       'Wether B Is Present In Code Or Not
                        flag = 1
                    End If
                Next
                If flag = 1 Then
                    flag = 0
                    For c = 0 To INTARRAY.Length - 1
                        If INTARRAY(c) = "STA" Then             'Wether STA is Also Present or Not
                            Dim store As Integer = c
                            flag = flag + 1                     'IF Present Flag Increments
                            Exit For
                        End If
                    Next
                    If flag <> 0 Then               'STA True
                        AddCode_WithSTA(c)
                    Else
                        AddCode()                   'STA False
                    End If
                Else
                    Form1.ListBox1.Items.Add("B Not Present In ADD B")          'If B Not Present
                    Form1.ListBox2.Items.Add("B Not Present In ADD B")
                    Form1.ListBox3.Items.Add("An error occured")
                    ErrorSignal += 1
                End If
            ElseIf INTARRAY(i) = "SUB B" Then                   'IF SUB Code Present
                For c = 0 To INTARRAY.Length - 1
                    If INTARRAY(c) = "B" Then                   'Wether B Is Present In Code Or Not
                        flag = 1
                    End If
                Next
                If flag = 1 Then
                    flag = 0
                    For c = 0 To INTARRAY.Length - 1
                        If INTARRAY(c) = "STA" Then             'Wether STA is Also Present or Not
                            Dim store As Integer = c
                            flag = flag + 1                     'IF Present Flag Increments
                            Exit For
                        End If
                    Next
                    If flag <> 0 Then               'STA True
                        SubCode_WithSTA(c)
                    Else
                        SubCode() 'STA False
                    End If
                Else
                    Form1.ListBox1.Items.Add("B Not Present In SUB B")          'If B Not Present
                    Form1.ListBox2.Items.Add("B Not Present In SUB B")
                    Form1.ListBox3.Items.Add("An error occured")
                    ErrorSignal += 1
                End If
            End If
        Next
    End Sub
    Public Sub AddCode()                                    'Specifically For Addition
        Dim A As Integer = Conversion.Val(Instructions(1))
        Dim B As Integer = Conversion.Val(Instructions(3))
        A = A + B
        Try
            conn.Close()
            conn.Open()
            cmd = New OleDbCommand("Update Instructions set Hex='" & A & "' WHERE Memory ='A'", conn)
            cmd.ExecuteNonQuery()
            outputPlace = "A"
            outputResult = A
        Catch ex As Exception
            MsgBox("Error", ex.Message)
            conn.Close()
        End Try
    End Sub
    Public Sub AddCode_WithSTA(ByVal c As Integer)
        Dim A As Integer = Conversion.Val(Instructions(1))
        Dim B As Integer = Conversion.Val(Instructions(3))
        A = A + B
        Dim TemLocation As String = Instructions(c + 2) & Instructions(c + 1)
        MsgBox(TemLocation)
        Try
            conn.Open()
            cmd = New OleDbCommand("Update Instructions set Hex='" & A & "' WHERE Memory ='" & TemLocation & "'", conn)
            cmd.ExecuteNonQuery()
            outputPlace = TemLocation
            outputResult = A
        Catch ex As Exception
            MsgBox("Error", ex.Message)
            conn.Close()
        End Try
    End Sub
    Public Sub SubCode_WithSTA(ByVal c As Integer)
        Dim A As Integer = Conversion.Val(Instructions(1))
        Dim B As Integer = Conversion.Val(Instructions(3))
        A = A - B
        Dim TemLocation As String = Instructions(c + 2) & Instructions(c + 1)
        Try
            conn.Open()
            cmd = New OleDbCommand("Update Instructions set Hex='" & A & "' WHERE Memory = '" & TemLocation & "'", conn)
            cmd.ExecuteNonQuery()
            outputPlace = TemLocation
            outputResult = A
        Catch ex As Exception
            MsgBox("Error", ex.Message)
            conn.Close()
        End Try
    End Sub
    Public Sub SubCode()
        Dim A As Integer = Conversion.Val(Instructions(1))
        Dim B As Integer = Conversion.Val(Instructions(3))
        A = A - B
        Try
            conn.Close()
            conn.Open()
            cmd = New OleDbCommand("Update Instructions set Hex='" & A & "' WHERE Memory ='A'", conn)
            cmd.ExecuteNonQuery()
            outputPlace = "A"
            outputResult = A
        Catch ex As Exception
            MsgBox("Error", ex.Message)
            conn.Close()
        End Try
    End Sub
    Public Sub Save(ByVal ProgName)
        Try
            conn.Close()
            conn.Open()
            For i = 0 To indexCountTransfer - 1
                cmd = New OleDbCommand("INSERT INTO SavedPrograms(Memory,Hex,PrgName) Values('" & MemLocation(i) & "','" & Instructions(i) & "','" & ProgName & "')", conn)
                cmd.ExecuteNonQuery()
            Next
            indexCountTransfer = 0
        Catch ex As Exception
            MsgBox("Error", ex.Message)
            conn.Close()
        End Try
        conn.Close()
        MsgBox("Program Saved Successfully As " & ProgName)
    End Sub
    Public Sub NameCheckPoints(ByVal ProgName)
        conn.Close()
        conn.Open()
        Try
            cmd = New OleDbCommand("INSERT INTO SavedProgramCheckPoint(MemStart,MemEnd,OutPutLocation,Output2,ProgramName) VALUES('" & MemLocation(0) & "','" & MemLocation(indexCountTransfer - 1) & "','" & outputPlace & "','" & outputResult & "','" & ProgName & "')", conn) ',OutPutLocation,Output,'" & outputPlace & "','" & outputResult & "',
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Error", ex.Message)
            conn.Close()
        End Try
        conn.Close()
        Save(ProgName)
    End Sub
    Public Sub NameCheck(ByVal Name)
        conn.Close()
        conn.Open()
        cmd = New OleDbCommand("Select * From SavedProgramCheckPoint where ProgramName='" + Name + "'", conn)
        da = New OleDbDataAdapter(cmd)
        ds = New Data.DataSet
        da.Fill(ds)
        Dim i As Integer
        i = ds.Tables(0).Rows.Count
        If i = 0 Then
            NameCheckPoints(Name)
        Else
            MsgBox("This Name Already Exists", MsgBoxStyle.Critical)
            SaveWindow.TextBox1.Text = Nothing
        End If
    End Sub
End Module
