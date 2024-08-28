Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Module Modules
    Public id, transno As String
    Public amount As Double
    Public sysTime As String




    Sub centerForm(ByVal frm As Form)
        Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height
        Dim formWidth As Integer = frm.Width
        Dim formHeight As Integer = frm.Height

        Dim newX As Integer = (screenWidth - formWidth) \ 2
        Dim newY As Integer = (screenHeight - formHeight) \ 2

        frm.Location = New Point(newX, newY)
    End Sub
    Function CountRecords(ByVal sql As String) As Integer
        Dim count As Integer
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        count = CInt(cm.ExecuteScalar)
        cn.Close()
        Return count
    End Function

    Sub UserActivity(ByVal summary, ByVal area)
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("INSERT INTO tbl_user_logs (log_user_id, log_area, log_action, log_date, log_time, log_location) values (@2, @1, @3, NOW(), NOW(), @6)", cn)
            With cm
                .Parameters.AddWithValue("@2", str_userid)
                .Parameters.AddWithValue("@3", summary)
                .Parameters.AddWithValue("@6", "PC Name: " & strHostName + " - " + strIPAddress)
                .Parameters.AddWithValue("@1", area)
                .ExecuteNonQuery()
            End With
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Date_Time()
        cn.Close()
        cn.Open()
        'cm = New MySqlCommand("SELECT CONCAT(date_format(curdate(), '%b %d, %Y'), ' | ', time_format(curtime(), '%h:%i %p')), dayname(curdate()), LEFT(time_format(curtime(), '%h:%i:%s %p'),5), RIGHT(time_format(curtime(), '%h:%i %p'),2), LEFT(time_format(curtime(), '%h:%i:%s %p'),8), date_format(curdate(), '%M %d, %Y')", cn)
        cm = New MySqlCommand("SELECT CONCAT(date_format(curdate(), '%b %d, %Y')), dayname(curdate()), LEFT(time_format(curtime(), '%h:%i:%s %p'),5), RIGHT(time_format(curtime(), '%h:%i %p'),2), LEFT(time_format(curtime(), '%h:%i:%s %p'),8), date_format(curdate(), '%M %d, %Y')", cn)
        frmMain.lblDate.Text = cm.ExecuteScalar
        cn.Close()

    End Sub

    Sub Date_Today()
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT date_format(curdate(), '%M %d %Y')", cn)
        DateToday = cm.ExecuteScalar & "  "
        cn.Close()

        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT date_format(curdate(), '%Y')", cn)
        YearToday = cm.ExecuteScalar
        cn.Close()

        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT CONCAT(LEFT(time_format(curtime(), '%h:%i:%s %p'),5),' ', RIGHT(time_format(curtime(), '%h:%i %p'),2))", cn)
        sysTime = cm.ExecuteScalar
        cn.Close()
    End Sub

    Public Function IS_EMPTY(ByRef sText As Object) As Boolean
        On Error Resume Next
        If sText.Text = String.Empty Then
            IS_EMPTY = True
            sText.BackColor = Color.FromArgb(255, 192, 192)

            ' Scroll to the control
            Dim parentControl As Control = sText.Parent
            If TypeOf parentControl Is ScrollableControl Then
                Dim scrollableParent As ScrollableControl = CType(parentControl, ScrollableControl)
                scrollableParent.ScrollControlIntoView(sText)
            End If

            sText.SetFocus()
            MsgBox("Warning: Required missing field. Please fill in all required fields marked in red.", vbExclamation)
        Else
            IS_EMPTY = False
            sText.BackColor = Color.White
        End If
        Return IS_EMPTY
    End Function

    Sub ShowPanelForEmptyTextbox(emptyTextbox As TextBox)

    End Sub

    Public Function CHECK_EXISTING(ByVal sql As String) As Boolean
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            CHECK_EXISTING = True
            MsgBox("Warning: Record already exists.", vbExclamation)
        Else
            CHECK_EXISTING = False
        End If
        dr.Close()
        cn.Close()
        Return CHECK_EXISTING
    End Function

    'Sub OpenForm2(frm As Form, ByVal title As String)
    '    frmMain.formPanels.Visible = True
    '    frmMain.formTitle.Text = title
    '    If frm.IsHandleCreated Then
    '        frm.BringToFront()
    '    Else
    '        frm.TopLevel = False
    '        frmMain.formPanel.Controls.Add(frm)
    '        frm.BringToFront()
    '        frm.Show()
    '    End If
    'End Sub

    Sub HideAllDatagridViewInPanelExcept(DGPanel As Panel, DGToShow As DataGridView)
        For Each control As Control In DGPanel.Controls
            If TypeOf control Is DataGridView Then
                Dim DGToHide As DataGridView = DirectCast(control, DataGridView)
                DGToHide.Hide()
            End If
        Next
        DGToShow.Show()
    End Sub

    Public Function GetCurrentAge(ByVal dob As Date) As Integer
        Dim TodayDate As DateTime = Convert.ToDateTime(DateToday)
        Dim age As Integer
        age = TodayDate.Year - dob.Year
        If (dob > Today.AddYears(-age)) Then age -= 1
        Return age
    End Function

    Public Function ComboCourseName(ByVal courseID As ComboBox) As String
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT course_name from tbl_course where course_id = '" & If(courseID.SelectedValue Is Nothing, "", CInt(courseID.SelectedValue.ToString)) & "'", cn)
            ComboCourseName = cm.ExecuteScalar
            cn.Close()
        Catch ex As Exception

        End Try

    End Function

    Public Function FormsOpenPanelCount() As Integer
        For Each control As Control In frmMain.formPanels.Controls
            FormsOpenPanelCount += 1
        Next
    End Function

    Sub comboStudentLevel(ByVal cb As ComboBox, ByVal label1 As Label, ByVal label2 As Label)
        If frmMain.systemModule.Text = "College Module" Then
            With cb
                .Items.Clear()
                .Items.Add("1st Year")
                .Items.Add("2nd Year")
                .Items.Add("3rd Year")
                .Items.Add("4th Year")
                .Items.Add("5th Year")
                label1.Text = "Course"
                label2.Text = "Year Level"
                .SelectedIndex = 0
            End With
        ElseIf frmMain.systemModule.Text = "Basic Education Module" Then
            With cb
                .Items.Clear()
                .Items.Add("Kinder 1")
                .Items.Add("Kinder 2")
                .Items.Add("Kinder 3")
                .Items.Add("Grade 1")
                .Items.Add("Grade 2")
                .Items.Add("Grade 3")
                .Items.Add("Grade 4")
                .Items.Add("Grade 5")
                .Items.Add("Grade 6")
                .Items.Add("Grade 7")
                .Items.Add("Grade 8")
                .Items.Add("Grade 9")
                .Items.Add("Grade 10")
                .Items.Add("Grade 11")
                .Items.Add("Grade 12")
                label1.Text = "Strand/Grade"
                label2.Text = "Grade Level"
                .SelectedIndex = 0
            End With
        End If

    End Sub

    Sub comboStudentLevelWithIrreg(ByVal cb As ComboBox, ByVal label1 As Label, ByVal label2 As Label)
        If frmMain.systemModule.Text = "College Module" Then
            With cb
                .Items.Clear()
                .Items.Add("1st Year")
                .Items.Add("2nd Year")
                .Items.Add("3rd Year")
                .Items.Add("4th Year")
                .Items.Add("5th Year")
                .Items.Add("1st Year Irreg.")
                .Items.Add("2nd Year Irreg.")
                .Items.Add("3rd Year Irreg.")
                .Items.Add("4th Year Irreg.")
                .Items.Add("5th Year Irreg.")
                label1.Text = "Course"
                label2.Text = "Year Level"
                .SelectedIndex = 0
            End With
        ElseIf frmMain.systemModule.Text = "Basic Education Module" Then
            With cb
                .Items.Clear()
                .Items.Add("Kinder 1")
                .Items.Add("Kinder 2")
                .Items.Add("Kinder 3")
                .Items.Add("Grade 1")
                .Items.Add("Grade 2")
                .Items.Add("Grade 3")
                .Items.Add("Grade 4")
                .Items.Add("Grade 5")
                .Items.Add("Grade 6")
                .Items.Add("Grade 7")
                .Items.Add("Grade 8")
                .Items.Add("Grade 9")
                .Items.Add("Grade 10")
                .Items.Add("Grade 11")
                .Items.Add("Grade 12")
                label1.Text = "Strand/Grade"
                label2.Text = "Grade Level"
                .SelectedIndex = 0
            End With
        End If

    End Sub

    Sub ResetControls(ByVal container As Control)
        For Each ctrl As Control In container.Controls
            Try
                If TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).Text = String.Empty
                    CType(ctrl, TextBox).Enabled = True
                ElseIf TypeOf ctrl Is ComboBox Then
                    If CType(ctrl, ComboBox).DropDownStyle = ComboBoxStyle.DropDownList Then
                        CType(ctrl, ComboBox).SelectedIndex = 0
                    Else
                        CType(ctrl, ComboBox).SelectedIndex = -1
                        'CType(ctrl, ComboBox).Text = String.Empty
                    End If
                    CType(ctrl, ComboBox).Enabled = True
                ElseIf TypeOf ctrl Is DateTimePicker Then
                    CType(ctrl, DateTimePicker).Text = DateToday
                    CType(ctrl, DateTimePicker).Enabled = True
                ElseIf TypeOf ctrl Is PictureBox Then
                    If CType(ctrl, PictureBox).Name.Contains("btn") Then
                    Else
                        'CType(ctrl, PictureBox).Image = Nothing
                    End If
                ElseIf TypeOf ctrl Is CrystalReportViewer Then
                    CType(ctrl, CrystalReportViewer).ReportSource = Nothing
                    ElseIf TypeOf ctrl Is Panel OrElse TypeOf ctrl Is GroupBox Then
                        ResetControls(ctrl)
                End If
            Catch ex As Exception

            End Try
        Next
    End Sub

    Public Sub ApplyHoverEffectToControls(control As Control)
        If TypeOf control Is Button Then
            Dim buttonControl As Button = DirectCast(control, Button)
            buttonControl.Cursor = Cursors.Hand
            If buttonControl.Image IsNot Nothing Then
                AddHandler control.MouseHover, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = Color.White
                                               End Sub

                AddHandler control.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = Color.White
                                               End Sub
            ElseIf buttonControl.Text = "SAVE" Or buttonControl.Text = "UPDATE" Or buttonControl.Text = "SELECT" Or buttonControl.Text = "IMPORT" Or buttonControl.Text = "UPLOAD" Or buttonControl.Text = "GENERATE" Or buttonControl.Text = "PRINT" Or buttonControl.Text = "PRINTER SETUP" Or buttonControl.Text = "ADD" Or buttonControl.Text = "ASSIGN" Or buttonControl.Text = "START CAMERA" Or buttonControl.Text = "COPY" Or buttonControl.Text = "CAPTURE" Or buttonControl.Text = "BROWSE" Or buttonControl.Text = "LINK" Or buttonControl.Text = "REMOVE PRE-REQUISITE" Or buttonControl.Text = "ADD SUBJECT" Or buttonControl.Text = "RETURN" Or buttonControl.Text = "PRINT PREVIEW" Then
                AddHandler control.MouseHover, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = Color.FromArgb(15, 101, 208)
                                               End Sub

                AddHandler control.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                   If control.BackColor = Color.FromArgb(30, 39, 46) Then
                                                       control.ForeColor = Color.White
                                                   Else
                                                       control.ForeColor = Color.Black
                                                   End If
                                               End Sub
            ElseIf buttonControl.Text = "LOGIN" Then
                AddHandler control.MouseHover, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = Color.White
                                               End Sub

                AddHandler control.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = Color.White
                                               End Sub
            ElseIf buttonControl.Text = "EXIT" Then
                AddHandler control.MouseHover, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = Color.White
                                               End Sub

                AddHandler control.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = Color.White
                                               End Sub
            ElseIf buttonControl.Text = "CANCEL" Or buttonControl.Text = "REMOVE" Or buttonControl.Text = "CANCEL PAYMENT" Then
                Dim btncolor As Color = buttonControl.ForeColor
                AddHandler control.MouseHover, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = Color.Red
                                               End Sub

                AddHandler control.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = btncolor
                                               End Sub
            Else
                AddHandler control.MouseHover, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = Color.FromArgb(15, 101, 208)
                                               End Sub

                AddHandler control.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                   control.ForeColor = Color.White
                                               End Sub
            End If
        ElseIf TypeOf control Is Label Then
            Dim labelControl As Label = DirectCast(control, Label)

            If labelControl.Text = "✕" Then
                Dim lblcolor As Color = labelControl.ForeColor
                AddHandler labelControl.MouseHover, Sub(sender As Object, e As EventArgs)
                                                        labelControl.ForeColor = Color.Red
                                                    End Sub

                AddHandler labelControl.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                        labelControl.ForeColor = lblcolor
                                                    End Sub
            ElseIf labelControl.Text = "[ ADD ]" Or labelControl.Text = "[ CAPTURE ]" Or labelControl.Text = "[ UPLOAD ]" Or labelControl.Text = "🔍" Or labelControl.Text = "[ View SOA ]" Or labelControl.Text = "[ View Entry ]" Or labelControl.Text = "[ PRINT ]" Or labelControl.Text = "[ FETCH ]" Or labelControl.Text = "[ FETCH SCHEDULE ]" Or labelControl.Text = "[ SET ]" Or labelControl.Text = "[ ASSIGN ]" Or labelControl.Text = "[ SUBJECTS ]" Or labelControl.Text = "[ Generate Supply Item List ]" Then
                AddHandler labelControl.MouseHover, Sub(sender As Object, e As EventArgs)
                                                        labelControl.ForeColor = Color.FromArgb(15, 101, 208)

                                                    End Sub

                AddHandler labelControl.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                        labelControl.ForeColor = Color.Black
                                                    End Sub
            ElseIf labelControl.Text = "[ CLEAR ]" Then
                AddHandler labelControl.MouseHover, Sub(sender As Object, e As EventArgs)
                                                        labelControl.ForeColor = Color.Red

                                                    End Sub

                AddHandler labelControl.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                        labelControl.ForeColor = Color.Black
                                                    End Sub
            ElseIf labelControl.Text = "⭾" Or labelControl.Text = "↻" Then
                AddHandler labelControl.MouseHover, Sub(sender As Object, e As EventArgs)
                                                        labelControl.ForeColor = Color.FromArgb(15, 101, 208)

                                                    End Sub

                AddHandler labelControl.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                        labelControl.ForeColor = Color.Black
                                                    End Sub
            ElseIf labelControl.Text = "  ▼  " Or labelControl.Text = "﹤" Or labelControl.Text = "﹥" Or labelControl.Text = " 🗘 " Or labelControl.Text = "﹤﹤" Or labelControl.Text = "﹥﹥" Then
                AddHandler labelControl.MouseHover, Sub(sender As Object, e As EventArgs)
                                                        labelControl.ForeColor = Color.FromArgb(15, 101, 208)

                                                    End Sub

                AddHandler labelControl.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                        labelControl.ForeColor = Color.FromName("ControlDark")
                                                    End Sub
            End If
        ElseIf TypeOf control Is TextBox Then
            control.Cursor = Cursors.IBeam
            AddHandler control.MouseHover, Sub(sender As Object, e As EventArgs)
                                               control.BackColor = Color.FromName("Control")
                                           End Sub

            AddHandler control.MouseLeave, Sub(sender As Object, e As EventArgs)
                                               control.BackColor = Color.White
                                           End Sub
        ElseIf TypeOf control Is ComboBox Then
            control.Cursor = Cursors.Hand
            AddHandler control.MouseHover, Sub(sender As Object, e As EventArgs)
                                               If control.BackColor = Color.FromArgb(72, 84, 96) Then
                                               Else
                                                   control.BackColor = Color.FromName("Control")
                                               End If
                                           End Sub
            AddHandler control.MouseLeave, Sub(sender As Object, e As EventArgs)
                                               If control.BackColor = Color.FromArgb(72, 84, 96) Then
                                               Else
                                                   control.BackColor = Color.White
                                               End If
                                           End Sub
        ElseIf TypeOf control Is DateTimePicker Then
            control.Cursor = Cursors.Hand
            AddHandler control.MouseHover, Sub(sender As Object, e As EventArgs)
                                               control.BackColor = Color.FromName("Control")
                                           End Sub

            AddHandler control.MouseLeave, Sub(sender As Object, e As EventArgs)
                                               control.BackColor = Color.White
                                           End Sub
        End If

        For Each childControl As Control In control.Controls
            ApplyHoverEffectToControls(childControl)
        Next
    End Sub

    Sub SetFormIcon(form As Form)
        Try
            ' Get the path to the application folder
            Dim appFolderPath As String = Application.StartupPath
            ' Combine the path with the icon file name
            Dim iconFilePath As String = System.IO.Path.Combine(appFolderPath, "icon.ico")

            ' Check if the icon file exists
            If System.IO.File.Exists(iconFilePath) Then
                ' Set the form icon
                form.Icon = New Icon(iconFilePath)
            Else
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub AccessArea(areaName As String, accessObject As Object)
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT (uaa_area_id) as 'ID', (area_name) as 'Description' from tbl_system_areas2, tbl_user_account_areas2 where tbl_system_areas2.area_id = tbl_user_account_areas2.uaa_area_id and uaa_area_user_id = " & str_userid & " and area_name = '" & areaName & "' and area_status = 'OPEN'", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            accessObject.Visible = True
        Else
            accessObject.Visible = False
        End If
        dr.Close()
        cn.Close()
    End Sub

    Sub AccessArea2(areaName As String, accessObject As Object)
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT (uaa_area_id) as 'ID', (area_name) as 'Description' from tbl_system_areas2, tbl_user_account_areas2 where tbl_system_areas2.area_id = tbl_user_account_areas2.uaa_area_id and uaa_area_user_id = " & str_userid & " and area_status = 'OPEN' and area_name IN (" & areaName & ")", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            accessObject.Visible = True
        Else
            accessObject.Visible = False
        End If
        dr.Close()
        cn.Close()
    End Sub

    Public Function CheckVisibleObjectInPanel(panelObject As Panel) As Integer
        Dim visibleControlCount As Integer = 0
        For Each ctrl As Control In panelObject.Controls
            If ctrl.Visible Then
                visibleControlCount += 1
            End If
        Next
        Return visibleControlCount
    End Function
End Module
