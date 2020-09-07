Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Drawing.Text
Public Class Form1
    '----------------'
    Dim time As Boolean = True
    Public flag As Integer = 1             ' Counter
    Dim tem As String
    Dim i As Integer = 0
    Dim n As Integer
    Dim count As Integer = 0
    Dim syntax_chk As Integer = 0
    Dim syntax_array(10) As String
    '-----------------'
    Dim temp(10) As String              'Temp Array
    Dim temp1(10) As String             'Temp Array 2
    '-----------------'
    Dim shft As Integer = 0             'Identifying Variable
    Dim strt As Integer = 0             'Identifying Variable
    Dim rdy As Integer = 0              'Identifying Variable
    Dim cnt As Integer = 0              'Identifying Variable
    Dim indexCount As Integer = 0
    Dim SaveSignal As Boolean = False
    '-----------------'
    Dim loadedProg As Boolean = False
    Dim programloaded As Boolean = False
    Dim keydown1 As Boolean = False       'To Determine Wether A Key Is Pressed Or Not
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Fnt()
        Rst_Off()
        dis()
        shft = 0
        Panel5.Show()
        Button2.Show()
        Button4.Show()
        Button4.Enabled = False
        Button2.Enabled = False
        Me.KeyPreview = True
        Button6.Show()
        Label6.Text = "Hardware State OFF"
        Label6.ForeColor = Color.Maroon
        EraseData()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TextBox3.Text = "8." And TextBox4.Text = "8." And TextBox5.Text = "8." And TextBox6.Text = "8." And TextBox7.Text = "8." And TextBox8.Text = "8." Then
            clr()
        ElseIf TextBox3.Text = Nothing And TextBox4.Text = Nothing And TextBox5.Text = Nothing And TextBox6.Text = Nothing And TextBox7.Text = Nothing And TextBox8.Text = Nothing Then
            Rst()
        End If
    End Sub
    Private Sub Form1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Rst()
        If Asc(e.KeyChar) > 1 Then
            If shft = 4 Then                                    'exmem full
                rdy1(e)
            Else
                txtshift(e)
            End If
        End If
    End Sub
    Private Sub Form1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If flag = 0 Then
            If e.Shift = True And e.KeyCode = Keys.X Then
                shft = 1
            ElseIf e.Shift = True And e.KeyCode = Keys.N Then
                shft = 2
            ElseIf e.KeyCode = 190 Then
                shft = 3
            End If
        End If
    End Sub
    Private Sub Btn_C_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_C.Click
        If flag = 0 Then
            Register_Signal("C")
        End If
    End Sub
    Private Sub Btn_Rst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Rst.Click
        If flag = 0 Then
            Rst()
            el85()
            strt = 0
            shft = 0
            indexCount = -1
            ListBox1.Items.Clear()
            ListBox2.Items.Clear()
            ListBox3.Items.Add("Reset Button Clicked")
            ResetAll()   'Function Resets Every Counter And Array Present To Prevent Overlapping Of Addresses And Counts
            Me.ErrorProvider1.SetError(Me.Button3, "")
            If programloaded = True Then
                timinglabelsHide()
            End If
        End If
    End Sub
    Private Sub Btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn2.Click    'On Off
        If flag = 1 Then
            Me.ErrorProvider1.SetError(Me.Btn2, "")
            on_click()
            Label6.Text = "Hardware State ON"
            Label6.ForeColor = Color.Green
            ListBox3.Items.Add("Hardware State Changed To ON")
            Button2.Enabled = True
            preloadedPrograms()
            Button2.Enabled = True
            Button6.Enabled = True
            'Button6.Show()
        Else
            Me.ErrorProvider1.SetError(Me.Button3, "")                  'To Prevent Double Error 
            off_click()
            EraseData()
            Label6.Text = "Hardware State To OFF"
            Label6.ForeColor = Color.Maroon
            ListBox1.Items.Clear()
            ListBox2.Items.Clear()
            Array.Clear(MemLocation, 0, MemLocation.Length)
            Array.Clear(Instructions, 0, Instructions.Length)
            ListBox3.Items.Add("Hardware State Changed To OFF")
            ComboBox2.Items.Clear()
            ComboBox2.Text = Nothing
            timinglabelsHide()
            Button2.Enabled = False
            Button6.Enabled = False
            chck = 1                                                'Prevent Null Exception If Once Closed
            time = False
        End If
        'timinglabels()

    End Sub
    Private Sub Btn_VctInt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_VctInt.Click
        If flag = 0 Then
            Rst()
            el85()
            ListBox3.Items.Add("VCT Button Clicked")
        End If
    End Sub
    Private Sub Btn_shft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_shft.Click
        If flag = 0 Then
            Rst()
            el85()
            shft = 1
            ListBox3.Items.Add("Shift Button Clicked")
        End If
    End Sub
    Private Sub Btn_EXREG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_EXREG.Click
        If flag = 0 Then
            If shft = 1 Then
                Rst_Off()
                TextBox5.ForeColor = Color.Red
                TextBox5.Text = "."
                shft = 0
            Else
                aa()
            End If
        End If
    End Sub
    Private Sub Btn_InsData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_InsData.Click
        If flag = 0 Then
            Rst()
            aa()
        End If
    End Sub
    Private Sub Btn_DelData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_DelData.Click
        If flag = 0 Then
            Rst()
            aa()
        End If
    End Sub
    Private Sub Btn_DelGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_DelGo.Click
        If flag = 0 Then
            indexCountTransfer = indexCount
            CHECKCODE()
            If ErrorSignal = 0 Then
                SaveSignal = True
                el85()
                TextBox7.Text = "8"
                Me.ErrorProvider1.SetError(Me.Button6, "")
            End If
        End If
    End Sub
    Private Sub Btn_RelEXmem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_RelEXmem.Click
        If flag = 0 Then
            If loadedProg = True Then
                MsgBox("Unloaded Loaded Program", MsgBoxStyle.Critical)
            Else
                Rst_Off()
                TextBox5.ForeColor = Color.Red
                TextBox5.Text = "."
                strt = 1
            End If
        End If
    End Sub
    Private Sub Btn_InsBM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_InsBM.Click
        If flag = 0 Then
            Rst_Off()
            TextBox5.ForeColor = Color.Red
            TextBox5.Text = "."
        End If
    End Sub
    Private Sub Btn_StrPre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_StrPre.Click
        If flag = 0 Then
            Rst()
            err()
        End If
    End Sub
    Private Sub Btn_MemcNxt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_MemcNxt.Click
        If flag = 0 Then                                                            'If Button Turned On 
            If TextBox8.Text = "8." Or TextBox8.Text = Nothing Then                 'To Check Wether 16BIT Address Is given
                Rst()
                err()
                Array.Clear(MemLocation, 0, MemLocation.Length)
                Array.Clear(Instructions, 0, Instructions.Length)
                ListBox1.Items.Clear()
            Else
                cnt = cnt + 1
                Show_Hex()
                If Conversion.Val(TextBox8.Text) < 3 And Conversion.Val(TextBox8.Text) > 1 Then
                    shft = 4                                                        'Memory Limit Check
                    strt = 0
                    If cnt > 1 Then
                        TextBox4.ForeColor = Color.Firebrick
                        TextBox3.ForeColor = Color.Firebrick
                        If Conversion.Val(TextBox5.Text) = 9 Then                   'Auto Increment Last Digit If exceed 9
                            TextBox6.Text = Conversion.Val(TextBox6.Text) + 1
                            TextBox5.Text = "0"
                            temp(2) = TextBox6.Text
                            temp(3) = 0
                        Else
                            TextBox5.Text = Conversion.Val(TextBox5.Text) + 1
                        End If
                        AddIP()
                        count = count + 1
                    End If
                Else
                    Rst()
                    err()
                    ListBox1.Items.Clear()
                    ListBox2.Items.Clear()
                    Instructions = Nothing
                    MemLocation = Nothing
                    Me.Refresh()
                End If
            End If
        End If
        TextBox4.ForeColor = Color.Red
        TextBox3.ForeColor = Color.Red
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If flag = 0 And indexCount <> 0 And SaveSignal = True Then
            SaveWindow.Show()
        ElseIf flag <> 0 Then
            Me.ErrorProvider1.SetError(Me.Btn2, "Hardware State OFF")
        ElseIf indexCount = 0 Then
            Me.ErrorProvider1.SetError(Me.Button3, "No Program Written")
        ElseIf SaveSignal = False Then
            Me.ErrorProvider1.SetError(Me.Button3, "Either U Program Not Compiled Or Error Occured")
        End If
    End Sub
    Private Sub Btn_Fill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Fill.Click
        If flag = 0 Then
            shft = 0
            syntax_check()
            If syntax_chk = 1 Then
                UpdateInst()
            End If
        End If
    End Sub
    Public Sub syntax_check()
        If syntax_array(count) <> "EF" Then
            Rst()
            err()
            ListBox1.Items.Add("HTA Not Found")
            ListBox2.Items.Add("HTA Not Found")
            ListBox3.Items.Add("Syntax Error Generated")
        Else
            Operation_check(count, syntax_array)                                    'Checks Hex Codes Working       Module DbFunctions
            syntax_chk = 1                                                          'Sets Correct Syntax Signal
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListBox3.Items.Clear()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If loadedProg = False Then
            If ComboBox2.SelectedIndex = -1 Then
                MsgBox("NO PROGRAM SELECTED", MsgBoxStyle.Exclamation)
            Else
                Dim msg As Integer = MsgBox("Your Progress Might Get Overwritten Are You Sure To Load This Program", MsgBoxStyle.OkCancel)
                If msg = DialogResult.OK Then
                    ListBox1.Items.Clear()
                    ListBox2.Items.Clear()
                    LoadProgramsToMemory()
                    Button6.Enabled = True
                    Button4.Enabled = True
                    loadedProg = True
                    Button2.Enabled = False
                    programloaded = True
                End If
            End If
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
    End Sub

    Private Sub Btn_D_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_D.Click
        If flag = 0 Then
            If strt = 1 Then
                Register_Signal("D")
            End If
        End If
    End Sub
    Private Sub Btn_E_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_E.Click
        If flag = 0 Then
            If strt = 1 Then
                Register_Signal("E")
            End If
        End If
    End Sub
    Private Sub Btn_F_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_F.Click
        If flag = 0 Then
            If strt = 1 Then
                Register_Signal("F")
            End If
        End If
    End Sub
    Private Sub Btn_8H_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_8H.Click
        If flag = 0 Then
            If strt = 1 Then
                Register_Signal("H")
            End If
        End If
    End Sub
    Private Sub Btn_9L_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_9L.Click
        If flag = 0 Then
            If strt = 1 Then
                Register_Signal("L")
            End If
        End If
    End Sub
    Private Sub Btn_A_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_A.Click
        If flag = 0 Then
            If strt = 1 Then
                Register_Signal("A")
            End If
        End If
    End Sub
    Private Sub Btn_B_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_B.Click
        If flag = 0 Then
            If strt = 1 Then
                Register_Signal("B")
                End
            End If
        End If
    End Sub
    Private Sub Btn_4PCH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_4PCH.Click
        If flag = 0 Then
            Rst()
            err()
        End If
    End Sub
    Private Sub Btn_5PCL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_5PCL.Click
        If flag = 0 Then
            Rst()
            err()
        End If
    End Sub
    Private Sub Btn_6SPH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_6SPH.Click
        If flag = 0 Then
            Rst()
            err()
        End If
    End Sub
    Private Sub Btn_7SPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_7SPL.Click
        If flag = 0 Then
            Rst()
            err()
        End If
    End Sub
    Private Sub Btn_0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_0.Click
        If flag = 0 Then
            Rst()
            err()
        End If
    End Sub
    Private Sub Btn_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_1.Click
        If flag = 0 Then
            Rst()
            err()
        End If
    End Sub
    Private Sub Btn_2SER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_2SER.Click
        If flag = 0 Then
            Rst()
            err()
        End If
    End Sub
    Private Sub Btn_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_3.Click
        If flag = 0 Then
            Rst()
            err()
        End If
    End Sub
    '''Functions'''
    Public Sub on_click()                   'ON BUTTON
        en()
        Rst()
        el85()
        flag = 0
        Btn2.BackgroundImage = My.Resources.download2
    End Sub
    Public Sub off_click()                  'OFF BUTTON
        Rst_Off()
        dis()
        flag = 1
        Btn2.BackgroundImage = My.Resources.download1
    End Sub
    Public Sub aa()                         'DUMMY MEMORY DISPLAY AAAA AA
        TextBox8.ForeColor = Color.Red
        TextBox7.ForeColor = Color.Red
        TextBox6.ForeColor = Color.Red
        TextBox5.ForeColor = Color.Red
        TextBox4.ForeColor = Color.Red
        TextBox3.ForeColor = Color.Red
        TextBox3.Text = "A."
        TextBox4.Text = "A."
        TextBox5.Text = "A."
        TextBox6.Text = "A."
        TextBox7.Text = "A."
        TextBox8.Text = "A."
    End Sub
    Public Sub Rst()                        'RESET IN ON STATE
        TextBox8.ForeColor = Color.Red
        TextBox7.ForeColor = Color.Red
        TextBox6.ForeColor = Color.Red
        TextBox5.ForeColor = Color.Red
        TextBox4.ForeColor = Color.Red
        TextBox3.ForeColor = Color.Red
        TextBox3.Text = "8."
        TextBox4.Text = "8."
        TextBox5.Text = "8."
        TextBox6.Text = "8."
        TextBox7.Text = "8."
        TextBox8.Text = "8."
    End Sub
    Public Sub Rst_Off()                     'RESET OFF STATE
        TextBox8.ForeColor = Color.Firebrick
        TextBox7.ForeColor = Color.Firebrick
        TextBox6.ForeColor = Color.Firebrick
        TextBox5.ForeColor = Color.Firebrick
        TextBox4.ForeColor = Color.Firebrick
        TextBox3.ForeColor = Color.Firebrick
        TextBox3.Text = "8."
        TextBox4.Text = "8."
        TextBox5.Text = "8."
        TextBox6.Text = "8."
        TextBox7.Text = "8."
        TextBox8.Text = "8."
    End Sub
    Public Sub clr()                         'CLEAR TEXTBOX
        TextBox3.Text = Nothing
        TextBox4.Text = Nothing
        TextBox5.Text = Nothing
        TextBox6.Text = Nothing
        TextBox7.Text = Nothing
        TextBox8.Text = Nothing
    End Sub
    Public Sub err()                        'ERROR SHOW
        TextBox7.Text = "E"
        TextBox6.Text = "r"
        TextBox5.Text = "r."
        TextBox8.ForeColor = Color.Firebrick
        TextBox4.ForeColor = Color.Firebrick
        TextBox3.ForeColor = Color.Firebrick
    End Sub
    Public Sub el85()                       'EL85 SHOW
        TextBox3.Text = "5"
        TextBox4.Text = "8"
        TextBox5.Text = "L"
        TextBox6.Text = "E"
        TextBox8.Text = "-"
        TextBox7.ForeColor = Color.Firebrick
    End Sub
    Public Sub Fnt()                        'FONT INITIALISATION
        Dim pfc As New PrivateFontCollection
        pfc.AddFontFile("F:\8085gitclone\8085UpdateRepo-master\8085_MicroProcessor\Resources\font.ttf")
        TextBox3.Font = New Font(pfc.Families(0), 28, FontStyle.Bold)
        TextBox4.Font = New Font(pfc.Families(0), 28, FontStyle.Bold)
        TextBox5.Font = New Font(pfc.Families(0), 28, FontStyle.Bold)
        TextBox6.Font = New Font(pfc.Families(0), 28, FontStyle.Bold)
        TextBox7.Font = New Font(pfc.Families(0), 28, FontStyle.Bold)
        TextBox8.Font = New Font(pfc.Families(0), 28, FontStyle.Bold)
    End Sub
    Public Sub dis()                       'HARDWARE OFF STATE
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
    End Sub
    Public Sub en()                         'HARDWARE ON STATE
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
        TextBox8.Enabled = True
    End Sub
    Public Sub txtshift(ByVal e)            'Char Shifting While Input
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 65 Or Asc(e.KeyChar) > 122 Then
                If strt = 1 Then
                    Rst_Off()
                    TextBox5.ForeColor = Color.Red
                    TextBox5.Text = e.KeyChar
                    If Conversion.Val(e.keychar) >= 2 And Conversion.Val(e.keychar) < 3 Then            'Less Than And Greater Than
                        temp(0) = e.KeyChar
                        strt = strt + 1
                    Else
                        Rst()
                        err()
                    End If
                ElseIf strt = 2 Then
                    Rst_Off()
                    TextBox6.ForeColor = Color.Red
                    TextBox5.ForeColor = Color.Red
                    TextBox6.Text = temp(0)
                    temp(1) = e.KeyChar
                    TextBox5.Text = e.KeyChar
                    strt = strt + 1
                ElseIf strt = 3 Then
                    Rst_Off()
                    TextBox7.ForeColor = Color.Red
                    TextBox6.ForeColor = Color.Red
                    TextBox5.ForeColor = Color.Red
                    TextBox7.Text = temp(0)
                    TextBox6.Text = temp(1)
                    temp(2) = e.KeyChar
                    TextBox5.Text = e.KeyChar
                    strt = strt + 1
                ElseIf strt = 4 Then
                    Rst_Off()
                    TextBox8.ForeColor = Color.Red
                    TextBox7.ForeColor = Color.Red
                    TextBox6.ForeColor = Color.Red
                    TextBox5.ForeColor = Color.Red
                    TextBox8.Text = temp(0)
                    TextBox7.Text = temp(1)
                    TextBox6.Text = temp(2)
                    temp(3) = e.KeyChar
                    TextBox5.Text = e.KeyChar
                    strt = strt + 1

                    'Address Array Input

                    If TextBox8.Text <> "8." Then
                        AddIP()
                    Else
                        Rst()
                        err()
                    End If
                Else
                    Rst()
                    el85()
                End If
            End If
        End If
    End Sub
    Public Sub AddIP()              'Address Input
        MemLocation(i) = temp(0) & temp(1) & temp(2) & temp(3)
        ListBox1.Items.Add(MemLocation(i))
        i = i + 1
        indexCount += 1
    End Sub
    Public Sub InstIP()               'Instruction Input
        Instructions(n) = temp1(0).ToUpper & temp1(1).ToUpper
        syntax_array(n) = temp1(0).ToUpper & temp1(1).ToUpper
        ListBox2.Items.Add(Instructions(n))
        n = n + 1
    End Sub
    Public Sub rdy1(ByVal e)        'Reads Memory Input From User
        If flag = 0 Then
            If strt = 0 Then
                TextBox4.ForeColor = Color.Red
                TextBox3.ForeColor = Color.Red
                TextBox3.Text = e.KeyChar
                temp1(0) = e.KeyChar
                strt = strt + 1
                temparrange()
            ElseIf strt = 1 Then
                temparrange()
                TextBox4.ForeColor = Color.Red
                TextBox3.ForeColor = Color.Red
                TextBox4.Text = temp1(0)
                temp1(1) = e.KeyChar
                TextBox3.Text = e.KeyChar
                If temp(3) <= 9 Then
                    temp(3) = 1 + temp(3)
                Else
                    temp(3) = 0
                    temp(2) = temp(2) + 1
                End If
                strt = 0
                InstIP()                'Storing Input
            End If
            End If
    End Sub
    Public Sub temparrange()    'Puts Addresses And Arranges In display
        TextBox8.Text = temp(0)
        TextBox7.Text = temp(1)
        TextBox6.Text = temp(2)
        TextBox5.Text = temp(3)
    End Sub
    Public Sub Loaded()         'Outputs LOADED In Display
        TextBox8.Text = "L"
        TextBox7.ForeColor = Color.Red
        TextBox7.Text = "O"
        TextBox6.Text = "A"
        TextBox5.Text = "D"
        TextBox4.Text = "E"
        TextBox3.Text = "D"
    End Sub
    Public Sub UnLoaded()       'Outputs UNLOAD In Display
        TextBox8.Text = "U"
        TextBox7.ForeColor = Color.Red
        TextBox7.Text = "N"
        TextBox6.Text = "L"
        TextBox5.Text = "O"
        TextBox4.Text = "A"
        TextBox3.Text = "D"
    End Sub
    Public Sub ResetAll()       'Function Resets Every Counter And Array Present To Prevent Overlapping Of Addresses And Counts
        Array.Clear(MemLocation, 0, MemLocation.Length)
        Array.Clear(Instructions, 0, Instructions.Length)
        Array.Clear(temp, 0, temp.Length)
        Array.Clear(temp1, 0, temp1.Length)
        i = 0
        n = 0
        count = 0
        shft = 0
        strt = 0
        rdy = 0
        cnt = 0
        indexCount = 0
        syntax_chk = 0
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If loadedProg = True Then
            Dim msg As Integer = MsgBox("Are You Sure You Want To Unload This Code This Will Erase All Current Codes!!", MsgBoxStyle.OkCancel)
            If msg = DialogResult.OK Then
                Button2.Enabled = True
                EraseData()
                UnLoaded()
                Button5.PerformClick()
                timinglabelsHide()
                loadedProg = False
                Button4.Enabled = False
                Button2.Enabled = True
                Button6.Enabled = False
            End If
        Else
            MsgBox("No Program Loaded Yet", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If flag = 0 Then                'If Hardware Is Not Powered ON
            If SaveSignal = True And programloaded = False Then   'If GO Pressed Means Code Written
                timingaddressWritten()
                timinglabels()
            Else
                If programloaded = True Then                                        'Else Code Loaded  
                    timinglabels()
                    timingaddress()
                Else
                    Me.ErrorProvider1.SetError(Me.Button6, "Program Not Loaded Neither Written")
                End If
            End If
        End If
    End Sub
End Class
