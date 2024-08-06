Public Class frmAcademicYear
    Public Shared AcadID As Integer = 0


#Region "Drag Form"

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles systemSign.MouseDown, Panel1.MouseDown  ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles systemSign.MouseMove, Panel1.MouseMove  ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles systemSign.MouseUp, Panel1.MouseUp   ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

#End Region

    Private Sub frmAcademicYear_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
        If frmMain.lblRole.Text = "Administrator" Then
            settingsPanel.Visible = True
        Else
            settingsPanel.Visible = False
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel?", vbYesNo + vbQuestion) = vbYes Then
            If btnSave.Visible = True Then
                ResetControls(Me)
            ElseIf btnUpdate.Visible = True Then
                Me.Close()
            End If
        Else
        End If
    End Sub

    Private Sub frmCourse_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ResetControls(Me)
        AcadID = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim acadName As String = ""
            If IS_EMPTY(txtStart) = True Then Return
            If IS_EMPTY(txtEnd) = True Then Return
            If IS_EMPTY(cbSemester) = True Then Return
            If IS_EMPTY(cbStatus) = True Then Return
            If CInt(txtStart.Text.Trim) <> CInt(txtEnd.Text.Trim) AndAlso cbSemester.Text = "Summer" Then
                MsgBox("Warning: Invalid Academic Year", vbExclamation)
                Return
            End If
            If CInt(txtStart.Text.Trim) = CInt(txtEnd.Text.Trim) AndAlso cbSemester.Text = "1st Semester" Or CInt(txtStart.Text.Trim) = CInt(txtEnd.Text.Trim) AndAlso cbSemester.Text = "2nd Semester" Then
                MsgBox("Warning: Invalid Academic Year", vbExclamation)
                Return
            End If
            If CHECK_EXISTING("SELECT * FROM tbl_period WHERE period_start_year = '" & txtStart.Text.Trim & "' and period_end_year = '" & txtEnd.Text.Trim & "' and period_semester = '" & cbSemester.Text & "'") = True Then Return
            If MsgBox("Are you sure you want to add this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    If CInt(txtStart.Text.Trim) = CInt(txtEnd.Text.Trim) AndAlso cbSemester.Text = "Summer" Then
                        query("INSERT INTO tbl_period (period_start_year, period_end_year, period_name, period_semester, period_date_created, period_status,period_enrollment_status,period_enrollment_ad_status,period_balance_check,period_enrollment_startdate,period_enrollment_enddate) values ('" & txtStart.Text.Trim & "', '" & txtEnd.Text.Trim & "', '" & txtEnd.Text.Trim & "', '" & cbSemester.Text.Trim & "', NOW(), '" & cbStatus.Text.Trim & "', '" & cbEnroll.Text.Trim & "', '" & cbAD.Text.Trim & "', '" & cbBalance.Text.Trim & "', '" & dtStart.Text & "', '" & dtEnd.Text & "')")
                    Else

                        If cbSemester.Text = "2nd Semester" Then
                            query("INSERT INTO tbl_period (period_start_year, period_end_year, period_name, period_semester, period_date_created, period_status,period_enrollment_status,period_enrollment_ad_status,period_balance_check,period_graduation,period_enrollment_startdate,period_enrollment_enddate) values ('" & txtStart.Text.Trim & "', '" & txtEnd.Text.Trim & "', '" & txtStart.Text.Trim & "-" & txtEnd.Text.Trim & "', '" & cbSemester.Text.Trim & "', NOW(), '" & cbStatus.Text.Trim & "', '" & cbEnroll.Text.Trim & "', '" & cbAD.Text.Trim & "', '" & cbBalance.Text.Trim & "', " & dtGrad.Value & ", '" & dtStart.Text & "', '" & dtEnd.Text & "')")
                        Else
                            query("INSERT INTO tbl_period (period_start_year, period_end_year, period_name, period_semester, period_date_created, period_status,period_enrollment_status,period_enrollment_ad_status,period_balance_check,period_enrollment_startdate,period_enrollment_enddate) values ('" & txtStart.Text.Trim & "', '" & txtEnd.Text.Trim & "', '" & txtStart.Text.Trim & "-" & txtEnd.Text.Trim & "', '" & cbSemester.Text.Trim & "', NOW(), '" & cbStatus.Text.Trim & "', '" & cbEnroll.Text.Trim & "', '" & cbAD.Text.Trim & "', '" & cbBalance.Text.Trim & "', '" & dtStart.Text & "', '" & dtEnd.Text & "')")
                        End If
                    End If
                    'UserActivity("Added a new academic year '" & txtStart.Text.Trim & "-" & txtEnd.Text.Trim & " " & cbSemester.Text & "'.")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("New academic year has been successfully saved.", vbInformation, "")
                    LibraryAcadList()
                    Me.Close()
                Catch ex As Exception
                    cn.Close()
                    MsgBox(ex.Message, vbCritical, "")
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "")
        End Try
    End Sub

    Private Sub updateAcademicYearStatus()
        If CHECK_EXISTING("SELECT * FROM tbl_period WHERE period_status = 'Active'") = True Then
            If cbSemester.Text = "Active" Then
                query("update tbl_period set period_status = 'Inactive' WHERE period_status = 'Active'")
            End If
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If IS_EMPTY(txtStart) = True Then Return
            If IS_EMPTY(txtEnd) = True Then Return
            If IS_EMPTY(cbSemester) = True Then Return
            If IS_EMPTY(cbStatus) = True Then Return
            If CInt(txtStart.Text.Trim) <> CInt(txtEnd.Text.Trim) AndAlso cbSemester.Text = "Summer" Then
                MsgBox("Warning: Invalid Academic Year", vbExclamation)
                Return
            End If
            If CInt(txtStart.Text.Trim) = CInt(txtEnd.Text.Trim) AndAlso cbSemester.Text = "1st Semester" Or CInt(txtStart.Text.Trim) = CInt(txtEnd.Text.Trim) AndAlso cbSemester.Text = "2nd Semester" Then
                MsgBox("Warning: Invalid Academic Year", vbExclamation)
                Return
            End If
            'If CHECK_EXISTING("") = True Then Return
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    If CInt(txtStart.Text.Trim) = CInt(txtEnd.Text.Trim) AndAlso cbSemester.Text = "Summer" Then
                        updateAcademicYearStatus()
                        query("update tbl_period set period_start_year = '" & txtStart.Text.Trim & "', period_end_year = '" & txtEnd.Text.Trim & "', period_name = '" & txtEnd.Text.Trim & "', period_semester = '" & cbSemester.Text & "', period_status = '" & cbStatus.Text & "', period_enrollment_status = '" & cbEnroll.Text & "', period_enrollment_ad_status = '" & cbAD.Text & "', period_balance_check = '" & cbBalance.Text & "', period_enrollment_startdate = " & dtStart.Value & ", period_enrollment_enddate = " & dtEnd.Value & " where period_id = " & AcadID & "")
                    Else
                        If cbSemester.Text = "2nd Semester" Then
                            updateAcademicYearStatus()
                            query("update tbl_period set period_start_year = '" & txtStart.Text.Trim & "', period_end_year = '" & txtEnd.Text.Trim & "', period_name = '" & txtEnd.Text.Trim & "-" & txtEnd.Text.Trim & "', period_semester = '" & cbSemester.Text & "', period_status = '" & cbStatus.Text & "', period_enrollment_status = '" & cbEnroll.Text & "', period_enrollment_ad_status = '" & cbAD.Text & "', period_balance_check = '" & cbBalance.Text & "', period_enrollment_startdate = " & dtStart.Value & ", period_enrollment_enddate = " & dtEnd.Value & " where period_id = " & AcadID & "")
                        Else
                            updateAcademicYearStatus()
                            query("update tbl_period set period_start_year = '" & txtStart.Text.Trim & "', period_end_year = '" & txtEnd.Text.Trim & "', period_name = '" & txtEnd.Text.Trim & "-" & txtEnd.Text.Trim & "', period_semester = '" & cbSemester.Text & "', period_status = '" & cbStatus.Text & "', period_enrollment_status = '" & cbEnroll.Text & "', period_enrollment_ad_status = '" & cbAD.Text & "', period_balance_check = '" & cbBalance.Text & "', period_graduation = " & dtGrad.Value & ", period_enrollment_startdate = " & dtStart.Value & ", period_enrollment_enddate = " & dtEnd.Value & " where period_id = " & AcadID & "")
                        End If
                    End If
                    UserActivity("Updated academic year '" & txtStart.Text.Trim & "-" & txtEnd.Text.Trim & " " & cbSemester.Text & "' details.", "LIBRARY ACADEMIC YEAR")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("Record has been successfully updated.", vbInformation, "")
                    LibraryAcadList()
                    Me.Close()
                Catch ex As Exception
                    cn.Close()
                    MsgBox(ex.Message, vbCritical, "")
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "")
        End Try
    End Sub

    Private Sub txtStart_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStart.KeyPress, txtEnd.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub cbSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSemester.SelectedIndexChanged
        If cbSemester.Text = "2nd Semester" Or cbSemester.Text = "2nd Trimester" Or cbSemester.Text = "3rd Trimester" Then
            dtGrad.Visible = True
            lblGrad.Visible = True
        Else
            dtGrad.Visible = False
            lblGrad.Visible = False
        End If
    End Sub

    Private Sub txtStart_TextChanged(sender As Object, e As EventArgs) Handles txtStart.TextChanged

    End Sub
End Class