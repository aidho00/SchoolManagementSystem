Imports MySql.Data.MySqlClient

Public Class frmEvaluationSubjects


    '#Region "Drag Form"

    '    Public MoveForm As Boolean
    '    Public MoveForm_MousePosition As Point
    '    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles systemSign.MouseDown, Panel1.MouseDown  ' Add more handles here (Example: PictureBox1.MouseDown)
    '        If e.Button = MouseButtons.Left Then
    '            MoveForm = True
    '            Me.Cursor = Cursors.Default
    '            MoveForm_MousePosition = e.Location
    '        End If
    '    End Sub

    '    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles systemSign.MouseMove, Panel1.MouseMove  ' Add more handles here (Example: PictureBox1.MouseMove)
    '        If MoveForm Then
    '            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
    '        End If
    '    End Sub

    '    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles systemSign.MouseUp, Panel1.MouseUp   ' Add more handles here (Example: PictureBox1.MouseUp)
    '        If e.Button = MouseButtons.Left Then
    '            MoveForm = False
    '            Me.Cursor = Cursors.Default
    '        End If
    '    End Sub

    '#End Region


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        dgClassSchedList.Rows.Clear()
    End Sub


    Private Sub dgClassSchedList_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgClassSchedList.RowsAdded
        Dim rowCount = dgClassSchedList.Rows.Count
        lblTotalSubjects.Text = rowCount

        Dim columnIndex As Integer = 4
        Dim columnSum As Double = GetColumnSum(dgClassSchedList, columnIndex)
        lblTotalUnits.Text = columnSum
    End Sub

    Private Sub dgClassSchedList_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgClassSchedList.RowsRemoved
        Dim rowCount = dgClassSchedList.Rows.Count
        lblTotalSubjects.Text = rowCount

        Dim columnIndex As Integer = 4
        Dim columnSum As Double = GetColumnSum(dgClassSchedList, columnIndex)
        lblTotalUnits.Text = columnSum
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If frmStudentEvaluation.txtStudent.Text = String.Empty Then
        Else

            If MsgBox("Are you sure you want to save this subject schedules for student " & frmStudentEvaluation.txtStudent.Text & " enrollment in academic year " & cbAcademicYear.Text & "?", vbYesNo + vbQuestion) = vbYes Then
                If dgClassSchedList.RowCount = 0 Then
                    MsgBox("There are no added subject schedules to save.", vbCritical)
                Else
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT * FROM tbl_students_grades WHERE sg_student_id = '" & frmStudentEvaluation.StudentID & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        dr.Close()
                        cn.Close()
                        MsgBox("Student " & frmStudentEvaluation.StudentName & " with ID Number " & frmStudentEvaluation.StudentID & " already has grades or is enrolled for this Academic Year.'", vbCritical)
                    Else
                        dr.Close()
                        cn.Close()

                        For Each row As DataGridViewRow In dgClassSchedList.Rows
                            query("INSERT INTO `tbl_enrollment_subjects`(`es_student_id`, `es_class_schedule_id`, `es_period_id`, `es_userid`) VALUES ('" & frmStudentEvaluation.StudentID & "', " & row.Cells(0).Value & ", " & CInt(cbAcademicYear.SelectedValue) & ", " & str_userid & ")")
                        Next
                        UserActivity("Saved subject schedules for student " & frmStudentEvaluation.txtStudent.Text & " enrollment in academic year " & cbAcademicYear.Text & ".", "STUDENT EVALUATION")

                        query("UPDATE tbl_students_curriculum SET `sc_total_units` = " & CInt(frmStudentEvaluation.dgStudentCurrList.CurrentRow.Cells(6).Value) & ", `sc_status` = '" & If(CInt(frmStudentEvaluation.dgStudentCurrList.CurrentRow.Cells(5).Value) >= CInt(frmStudentEvaluation.dgStudentCurrList.CurrentRow.Cells(6).Value), "Completed", "Ongoing") & "' WHERE sc_student_id = '" & frmStudentEvaluation.StudentID & "' and scg_curr_id = " & frmStudentEvaluation.currid & "")
                        UserActivity("Updated student " & frmStudentEvaluation.txtStudent.Text & " curriculum " & frmStudentEvaluation.txtCurr.Text & " record. " & frmStudentEvaluation.dgStudentCurrList.CurrentRow.Cells(5).Value & " units earned out of " & frmStudentEvaluation.dgStudentCurrList.CurrentRow.Cells(6).Value & ".", "STUDENT EVALUATION")
                        Dim imageFromPictureBox As Image = frmStudentEvaluation.pic2.Image
                        frmStudentEvaluation.dgStudentCurrList.CurrentRow.Cells(9).Value = imageFromPictureBox

                        MsgBox("Successfully saved subject schedules for enrollment.", vbInformation)
                        evaluationSubjects()
                        Me.Close()
                    End If
                End If
            End If

        End If
    End Sub

    Sub evaluationSubjects()
        cn.Close()
        cn.Open()
        frmReportViewer.Show()
        Try
            Dim dtable As DataTable
            Dim dbcommand As New MySqlCommand("Select (class_schedule_id) As 'ID', (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (subject_units) as 'Units', if(ds_code = 'M T W TH F SAT SUN', 'DAILY', ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (room_code) as 'Room', (Instructor) as 'Instructor', DATE_FORMAT(tbl_enrollment.eenrolledby_datetime,'%M %d, %Y') as 'DateEnrolled', CONCAT(tbl_user_account.ua_first_name,' ',tbl_user_account.ua_middle_name, ' ', tbl_user_account.ua_last_name) as 'EnrolledBy', ewithdrawn_datetime from from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_enrollment_subjects where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_enrollment_subjects.es_class_schedule_id and tbl_enrollment_subjects.es_student_id = '" & frmStudentEvaluation.StudentID & "' and tbl_enrollment_subjects.es_period_id = " & CInt(cbAcademicYear.SelectedValue) & " order by Days asc, STR_TO_DATE(`Start Time`,'%l:%i:%s %p') asc", cn)
            Dim adt As New MySqlDataAdapter
            adt.SelectCommand = dbcommand
            dtable = New DataTable
            adt.Fill(dtable)
            dg_report.DataSource = dtable
            adt.Dispose()
            dbcommand.Dispose()

            dt.Columns.Clear()
            dt.Rows.Clear()
            With dt
                .Columns.Add("cb_code")
                .Columns.Add("subject_code")
                .Columns.Add("subject_description")
                .Columns.Add("ds_code")
                .Columns.Add("time_start_schedule")
                .Columns.Add("time_end_schedule")
                .Columns.Add("subject_units")
                .Columns.Add("room_code")
                .Columns.Add("instructor")
                .Columns.Add("eenrolledby_datetime")
            End With

            For Each dr As DataGridViewRow In dg_report.Rows
                dt.Rows.Add(dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(4).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value)
            Next
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New Enrollment_Student_SUBJECTS
            rptdoc.SetDataSource(dt)
            frmReportViewer.ReportViewer.ReportSource = rptdoc
            dg_report.DataSource = Nothing
            frmReportViewer.ReportViewer.Select()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Private Sub frmEvaluationSubjects_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
    End Sub
End Class