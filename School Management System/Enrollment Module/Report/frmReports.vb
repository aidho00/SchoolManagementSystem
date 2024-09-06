Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System
Imports System.Globalization
Public Class frmReports

    Dim studentId As String = ""
    Dim studentName As String = ""
    Dim studentFName As String = ""
    Dim studentMName As String = ""
    Dim studentLName As String = ""
    Dim studentEXTName As String = ""
    Dim studentCourse As String = ""
    Dim studentCourseDesc As String = ""
    Dim studentCourseSector As String = ""
    Dim studentCourseId As Integer = 0
    Dim studentYearLevel As String = ""
    Dim studentGender As String = ""

    Dim studentGradeLevel As String = ""
    Dim studentGradeLevelCourse As String = ""
    Dim studentGradeLevelCourseName As String = ""
    Dim studentGradeLevelCourseCode As String = ""

    Dim classId As Integer = 0
    Dim subjectCode As String = ""
    Dim subjectDesc As String = ""
    Dim subjectUnits As String = ""
    Dim subjectDay As String = ""
    Dim subjectTstart As String = ""
    Dim subjectTend As String = ""
    Dim subjectRoom As String = ""
    Dim subjectInstructor As String = ""
    Dim subjectClass As String = ""

    Dim cbId As Integer = 0
    Dim cbCode As String = ""

    Dim insId As Integer = 0
    Dim insFName As String = ""
    Dim insName As String = ""

    Dim courseId As Integer = 0
    Dim courseName As String = ""
    Dim courseCode As String = ""
    Dim courseMajor As String = ""

    Dim schoolId As Integer = 0
    Dim schoolCode As String = ""
    Dim SchoolName As String = ""
    Dim SchoolAddress As String = ""

    Dim ReportGenerated As Boolean = False

    Private Sub cbAcademicYear_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbAcademicYear.KeyPress, cbAcadCoor.KeyPress, cbPresident.KeyPress, cbRegistrar.KeyPress, cbFontSizeCourse.KeyPress, cbFontSizeStudentName.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtStudent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStudent.KeyPress, txtClass.KeyPress, txtInstructor.KeyPress, txtSection.KeyPress, txtSubject.KeyPress, txtNote.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        btnNext.Visible = False
        btnPrev.Visible = True

        ReportsControlsPanel.Size = New Size(23, 484)
    End Sub
    Sub NextBtn()
        btnNext.Visible = False
        btnPrev.Visible = True

        ReportsControlsPanel.Size = New Size(23, 484)
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        PrevBtn()
    End Sub

    Sub PrevBtn()
        btnNext.Visible = True
        btnPrev.Visible = False
        ReportsControlsPanel.Size = New Size(313, 484)
    End Sub

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Public Sub HideAllReportPanels()
        For Each control As Control In FlowLayoutPanel2.Controls
            If TypeOf control Is Panel Then
                control.Visible = False
            End If
        Next
        Try
            SearchPanel.Visible = False
            ReportsControlsPanel.Visible = True
            Panel3.Visible = True
            frmTitle.Text = "Search"
            txtSearch.Text = String.Empty
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cbClass_Click(sender As Object, e As EventArgs) Handles cbClass.Click
        If cbClass.Checked = True Then
            cbClass.Checked = True
        Else
            cbClass.Checked = True
        End If
        cbInstructor.Checked = False
        cbSection.Checked = False
        PanelClass.Visible = True
        PanelInstructor.Visible = False
        PanelSection.Visible = False
        PanelClassSched3.Visible = False
    End Sub

    Private Sub cbInstructor_Click(sender As Object, e As EventArgs) Handles cbInstructor.Click
        If cbInstructor.Checked = True Then
            cbInstructor.Checked = True
        Else
            cbInstructor.Checked = True
        End If
        cbClass.Checked = False
        cbSection.Checked = False
        PanelClass.Visible = False
        PanelInstructor.Visible = True
        PanelSection.Visible = False

        PanelClassSched3.Visible = True
    End Sub

    Private Sub cbSection_Click(sender As Object, e As EventArgs) Handles cbSection.Click
        If cbSection.Checked = True Then
            cbSection.Checked = True
        Else
            cbSection.Checked = True
        End If
        cbClass.Checked = False
        cbInstructor.Checked = False
        PanelClass.Visible = False
        PanelInstructor.Visible = False
        PanelSection.Visible = True

        PanelClassSched3.Visible = True
    End Sub

    Sub LoadData()
        fillCombo("SELECT CONCAT(`emp_first_name`, ' ', LEFT(`emp_middle_name`, 1), '. ', `emp_last_name`) as NAME, emp_id FROM `tbl_employee` where emp_designation = 'College Registrar Director'", cbRegistrar, "tbl_employee", "NAME", "emp_id")
        fillCombo("SELECT CONCAT(`emp_first_name`, ' ', LEFT(`emp_middle_name`, 1), '. ', `emp_last_name`) as NAME, emp_id FROM `tbl_employee` where emp_designation = 'Academic Director'", cbAcadCoor, "tbl_employee", "NAME", "emp_id")
        fillCombo("SELECT CONCAT(`emp_first_name`, ' ', LEFT(`emp_middle_name`, 1), '. ', `emp_last_name`) as NAME, emp_id FROM `tbl_employee` where (emp_designation = 'School Administrator' or emp_designation = 'School President')", cbPresident, "tbl_employee", "NAME", "emp_id")
        fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_class_schedule t1 JOIN tbl_period t2 ON t1.csperiod_id = t2.period_id group by t1.csperiod_id order by `period_name` desc, `period_status` ASC, `period_semester` desc", cbAcademicYear, "tbl_period", "PERIOD", "period_id")
        cbFontSizeCourse.Text = "18"
        cbFontSizeStudentName.Text = "36"
        cbPurpose.Text = "Legal Purpose"
    End Sub


    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        SearchPanel.Visible = True
        dgStudentList.BringToFront()
        ReportsControlsPanel.Visible = False
        Panel3.Visible = False
        frmTitle.Text = "Search Student"
        ReportStudentList()
    End Sub
    Private Sub YearLevelStudentGradeLevel()
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT `sg_yearlevel` FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            studentGradeLevel = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT CONCAT(`course_code`,' - ',`course_name`) FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            studentGradeLevelCourse = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT `course_name` FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            studentGradeLevelCourseName = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT `course_code` FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            studentGradeLevelCourseCode = cm.ExecuteScalar
            cn.Close()
            'cn.Open()
            'cm = New MySqlCommand("SELECT sg_course_id FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            'studentCourseId = cm.ExecuteScalar
            'cn.Close()
            'cn.Open()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Select Case frmTitle.Text
            Case "Search Student"
                studentId = dgStudentList.CurrentRow.Cells(1).Value
                studentName = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
                studentLName = dgStudentList.CurrentRow.Cells(2).Value
                studentFName = dgStudentList.CurrentRow.Cells(3).Value
                studentMName = dgStudentList.CurrentRow.Cells(4).Value
                studentEXTName = dgStudentList.CurrentRow.Cells(5).Value

                studentYearLevel = dgStudentList.CurrentRow.Cells(7).Value
                studentCourse = dgStudentList.CurrentRow.Cells(8).Value
                studentCourseId = dgStudentList.CurrentRow.Cells(9).Value

                studentCourseDesc = dgStudentList.CurrentRow.Cells(10).Value

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT course_sector FROM tbl_course WHERE course_id = " & studentCourseId & "", cn)
                studentCourseSector = cm.ExecuteScalar
                cn.Close()

                studentGender = dgStudentList.CurrentRow.Cells(6).Value
                txtStudent.Text = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value

                fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_students_grades t1 JOIN tbl_period t2 ON t1.sg_period_id = t2.period_id where t1.sg_student_id = '" & studentId & "' group by t1.sg_period_id order by `period_name` desc, `period_status` ASC, `period_semester` desc", cbAcademicYear, "tbl_period", "PERIOD", "period_id")
                If cbAcademicYear.Items.Count = 0 Then
                    fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_withdraw_students_grades t1 JOIN tbl_period t2 ON t1.sg_period_id = t2.period_id where t1.sg_student_id = '" & studentId & "' group by t1.sg_period_id order by `period_name` desc, `period_status` ASC, `period_semester` desc", cbAcademicYear, "tbl_period", "PERIOD", "period_id")
                End If
                YearLevelStudentGradeLevel()
            Case "Search Class Schedule"
                classId = dgClassSchedList.CurrentRow.Cells(1).Value
                subjectCode = dgClassSchedList.CurrentRow.Cells(3).Value
                subjectDesc = dgClassSchedList.CurrentRow.Cells(4).Value
                subjectUnits = dgClassSchedList.CurrentRow.Cells(5).Value
                subjectDay = dgClassSchedList.CurrentRow.Cells(6).Value
                subjectTstart = dgClassSchedList.CurrentRow.Cells(7).Value
                subjectTend = dgClassSchedList.CurrentRow.Cells(8).Value
                subjectRoom = dgClassSchedList.CurrentRow.Cells(9).Value
                subjectInstructor = dgClassSchedList.CurrentRow.Cells(10).Value
                subjectClass = dgClassSchedList.CurrentRow.Cells(2).Value
                txtClass.Text = subjectClass & " - [" & subjectCode & "] " & subjectDesc
            Case "Search Instructor"
                insId = dgEmployeeList.CurrentRow.Cells(1).Value
                insName = dgEmployeeList.CurrentRow.Cells(3).Value & ", " & dgEmployeeList.CurrentRow.Cells(6).Value & ", " & dgEmployeeList.CurrentRow.Cells(4).Value & " " & dgEmployeeList.CurrentRow.Cells(5).Value
                insFName = dgEmployeeList.CurrentRow.Cells(3).Value
                txtInstructor.Text = insName
                ReportInstructorClassSchedList(insId)
                fetchClassdata()
            Case "Search Class Block/Section"
                cbId = dgSectionList.CurrentRow.Cells(1).Value
                cbCode = dgSectionList.CurrentRow.Cells(2).Value & " - " & dgSectionList.CurrentRow.Cells(3).Value
                txtSection.Text = cbCode
                ReportSectionClassSchedList(cbId)
                fetchClassdata()
            Case "Search Course"
                courseId = dgCourseList.CurrentRow.Cells(1).Value
                courseCode = dgCourseList.CurrentRow.Cells(2).Value
                courseName = dgCourseList.CurrentRow.Cells(3).Value
                courseMajor = dgCourseList.CurrentRow.Cells(4).Value
                txtCourse.Text = dgCourseList.CurrentRow.Cells(2).Value & " - " & dgCourseList.CurrentRow.Cells(3).Value
            Case "Search School"
                schoolId = dgSchoolList.CurrentRow.Cells(1).Value
                schoolCode = dgSchoolList.CurrentRow.Cells(2).Value
                SchoolName = dgSchoolList.CurrentRow.Cells(3).Value
                SchoolAddress = dgSchoolList.CurrentRow.Cells(4).Value
                txtSchool.Text = schoolCode & " - " & SchoolName
            Case "Search Student - Transmittal"

                dgStudTransmittal.Rows.Clear()
                Dim sql As String
                sql = "SELECT StudentFullName as 'Student', t2.doc_description as 'Credential' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where t1.`ref_code` = '" & dgAckList.CurrentRow.Cells(0).Value & "'"
                cn.Close()
                cn.Open()
                cm = New MySqlCommand(sql, cn)
                dr = cm.ExecuteReader
                While dr.Read
                    dgStudTransmittal.Rows.Add(dr.Item("Student").ToString, dr.Item("Credential").ToString)
                End While
                dr.Close()
                cn.Close()
                dgPanelPadding(dgStudTransmittal, dgPanel)
        End Select

        SearchPanel.Visible = False
        ReportsControlsPanel.Visible = True
        Panel3.Visible = True
        dgStudentList.Rows.Clear()
        frmTitle.Text = "Search"
        txtSearch.Text = String.Empty
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click


        If frmMain.formTitle.Text = "Generate Certificate Of Registration" Then
            If studentId = String.Empty Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Student.", vbCritical)
                btnSearchStudent.Select()
            ElseIf CInt(cbAcademicYear.SelectedValue) <= 0 Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Academic Year.", vbCritical)
                cbAcademicYear.Select()
            Else
                Try
                    Dim enrolledStatus As String = ""
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT * from tbl_enrollment where estudent_id = '" & studentId & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        enrolledStatus = "Enrolled"
                        dr.Close()
                        cn.Close()
                        cn.Open()
                        Dim dtable As DataTable
                        Dim dbcommand As New MySqlCommand("Select (class_schedule_id) As 'ID', (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (subject_units) as 'Units', if(ds_code = 'M T W TH F SAT SUN', 'DAILY', ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (room_code) as 'Room', (Instructor) as 'Instructor', DATE_FORMAT(tbl_enrollment.eenrolledby_datetime,'%M %d, %Y') as 'DateEnrolled', CONCAT(tbl_user_account.ua_first_name,' ',tbl_user_account.ua_middle_name, ' ', tbl_user_account.ua_last_name) as 'EnrolledBy', ewithdrawn_datetime from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_enrollment, tbl_students_grades, tbl_user_account where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_students_grades.sg_class_id and tbl_enrollment.estudent_id = tbl_students_grades.sg_student_id and tbl_class_schedule.csperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eenrolledby_id = tbl_user_account.ua_id and tbl_enrollment.estudent_id = '" & studentId & "' and tbl_enrollment.eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "  order by Days asc, STR_TO_DATE(`Start Time`,'%l:%i:%s %p') asc", cn)
                        Dim adt As New MySqlDataAdapter
                        adt.SelectCommand = dbcommand
                        dtable = New DataTable
                        adt.Fill(dtable)
                        dg_report.DataSource = dtable
                        adt.Dispose()
                        dbcommand.Dispose()
                        cn.Close()
                    Else
                        dr.Close()
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT * from tbl_withdraw_enrollment where estudent_id = '" & studentId & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            enrolledStatus = "Withdrawn"
                            dr.Close()
                            cn.Close()
                            cn.Open()
                            Dim dtable As DataTable
                            'Dim dbcommand As New MySqlCommand("Select (class_schedule_id) As 'ID', (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (subject_units) as 'Units', if(ds_code = 'M T W TH F SAT SUN', 'DAILY', ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (room_code) as 'Room', (Instructor) as 'Instructor', DATE_FORMAT(tbl_enrollment.eenrolledby_datetime,'%M %d, %Y') as 'DateEnrolled', CONCAT(tbl_user_account.ua_first_name,' ',tbl_user_account.ua_middle_name, ' ', tbl_user_account.ua_last_name) as 'EnrolledBy', ewithdrawn_datetime from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_enrollment, tbl_students_grades, tbl_user_account where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_students_grades.sg_class_id and tbl_enrollment.estudent_id = tbl_students_grades.sg_student_id and tbl_class_schedule.csperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eenrolledby_id = tbl_user_account.ua_id and tbl_enrollment.estudent_id = '" & studentId & "' and tbl_enrollment.eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & " order by Days asc, STR_TO_DATE(`Start Time`,'%l:%i:%s %p') asc", cn)
                            Dim dbcommand As New MySqlCommand("Select (class_schedule_id) As 'ID', (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (subject_units) as 'Units', (ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (room_code) as 'Room', (Instructor) as 'Instructor', DATE_FORMAT(tbl_withdraw_enrollment.eenrolledby_datetime,'%M %d, %Y') as 'DateEnrolled', CONCAT(tbl_user_account.ua_first_name,' ',tbl_user_account.ua_middle_name, ' ', tbl_user_account.ua_last_name) as 'EnrolledBy', e_withdrawn_date from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_withdraw_enrollment, tbl_withdraw_students_grades, tbl_user_account where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_withdraw_students_grades.sg_class_id and tbl_withdraw_enrollment.estudent_id = tbl_withdraw_students_grades.sg_student_id and tbl_class_schedule.csperiod_id = tbl_withdraw_students_grades.sg_period_id and tbl_withdraw_enrollment.eperiod_id = tbl_withdraw_students_grades.sg_period_id and tbl_withdraw_enrollment.eenrolledby_id = tbl_user_account.ua_id and tbl_withdraw_enrollment.estudent_id = '" & studentId & "' and tbl_withdraw_enrollment.eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & " order by Days asc, STR_TO_DATE(`Start Time`,'%l:%i:%s %p') asc", cn)
                            Dim adt As New MySqlDataAdapter
                            adt.SelectCommand = dbcommand
                            dtable = New DataTable
                            adt.Fill(dtable)
                            dg_report.DataSource = dtable
                            adt.Dispose()
                            dbcommand.Dispose()
                            cn.Close()
                        Else
                            enrolledStatus = "Not Enrolled"
                            dr.Close()
                            cn.Close()
                            MsgBox("Student '" & studentName & "' with ID Number '" & studentId & "' is not enrolled in Academic Year '" & cbAcademicYear.Text & "'.", vbCritical)
                            Return
                        End If
                    End If

                    NextBtn()

                    Dim dt As New DataTable
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

                    Dim iDate As String = DateToday
                    Dim oDate As DateTime = Convert.ToDateTime(iDate)
                    Dim iDate2 As String
                    Dim oDate2 As DateTime
                    Try
                        iDate2 = dg_report.Rows(0).Cells(12).Value.ToString()
                        oDate2 = Convert.ToDateTime(iDate2)
                    Catch ex As Exception
                    End Try

                    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rptdoc = New Enrollment_Student_COR
                    rptdoc.SetDataSource(dt)
                    rptdoc.SetParameterValue("studentname", studentName)

                    If enrolledStatus = "Withdrawn" Then
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT CONCAT(`course_code`,' - ',`course_name`) FROM `tbl_student` t1 JOIN tbl_course t2 ON t1.s_course_id = t2.course_id WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentGradeLevelCourseName = cm.ExecuteScalar
                        cn.Close()

                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT `s_yr_lvl` FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentGradeLevel = cm.ExecuteScalar
                        cn.Close()

                        rptdoc.SetParameterValue("studentcourse", studentGradeLevelCourseName)
                        rptdoc.SetParameterValue("studentyearlevel", studentGradeLevel)
                    ElseIf enrolledStatus = "Enrolled" Then
                        rptdoc.SetParameterValue("studentcourse", studentGradeLevelCourse)
                        rptdoc.SetParameterValue("studentyearlevel", studentGradeLevel)
                    End If

                    rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                    rptdoc.SetParameterValue("studentidnumber", studentId)
                    rptdoc.SetParameterValue("enrolledby", dg_report.CurrentRow.Cells(11).Value)
                    rptdoc.SetParameterValue("enrolleddate", dg_report.CurrentRow.Cells(10).Value)
                    rptdoc.SetParameterValue("dategenerated", oDate.ToString("MMMM' 'dd', 'yyyy"))

                    If enrolledStatus = "Withdrawn" Then
                        rptdoc.SetParameterValue("enrollment_status", "WITHDRAWN")
                        rptdoc.SetParameterValue("wdate", "Date Withdrawn:")
                        rptdoc.SetParameterValue("wdate2", oDate2.ToString("MMMM' 'dd', 'yyyy"))
                    ElseIf enrolledStatus = "Enrolled" Then
                        rptdoc.SetParameterValue("enrollment_status", " ")
                        rptdoc.SetParameterValue("wdate", " ")
                        rptdoc.SetParameterValue("wdate2", " ")
                    End If

                    ReportViewer.ReportSource = rptdoc
                    dg_report.DataSource = Nothing
                    ReportViewer.Select()
                    ReportGenerated = True
                Catch ex As Exception
                    MsgBox(ex.Message, vbCritical)
                    cn.Close()
                    PrevBtn()
                End Try
            End If
        ElseIf frmMain.formTitle.Text = "Generate Class Master List" Then
            If CInt(cbAcademicYear.SelectedValue) <= 0 Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Academic Year.", vbCritical)
                cbAcademicYear.Select()
            Else
                Try
                    NextBtn()
                    Dim dtable As DataTable
                    Dim sql As String
                    If cbClass.Checked = True Then
                        sql = "SELECT t2.enrollment_code, t1.sg_period_id, t1.sg_student_id, CONCAT(t4.s_ln, ', ', t4.s_fn, '  ', t4.s_mn) AS 'estudent', t4.s_gender, t5.course_code, t1.sg_yearlevel, (t3.class_schedule_id) AS 'ClassSchedule_ID', (t6.cb_code) AS 'Class', (t9.subject_code) AS 'Subject Code', (t9.subject_description) AS 'Subject Desc.', (t9.subject_units) AS 'Units', (t10.ds_code) AS 'Days', (time_start_schedule) AS 'Start Time', (t3.time_end_schedule) AS 'End Time', (t8.room_code) AS 'Room', (t7.Instructor) AS 'Instructor' FROM tbl_students_grades t1 JOIN tbl_class_schedule t3 ON t1.sg_class_id = t3.class_schedule_id AND t3.csperiod_id = t1.sg_period_id JOIN tbl_student t4 ON t1.sg_student_id = t4.s_id_no JOIN tbl_course t5 ON t1.sg_course_id = t5.course_id LEFT JOIN tbl_class_block t6 ON t3.class_block_id = t6.cb_id  LEFT JOIN employee t7 ON t3.csemp_id = t7.emp_id LEFT JOIN tbl_room t8 ON t3.csroom_id = t8.room_id LEFT JOIN tbl_subject t9 ON t3.cssubject_id = t9.subject_id LEFT JOIN tbl_day_schedule t10 ON t3.days_schedule = t10.ds_id JOIN tbl_enrollment t2 ON t4.s_id_no = t2.estudent_id and t1.sg_period_id = t2.eperiod_id WHERE t1.sg_class_id = " & classId & " and t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and t1.sg_grade_status = 'Enrolled' order by t4.s_ln"
                    Else
                        sql = "SELECT t2.enrollment_code, t1.sg_period_id, t1.sg_student_id, CONCAT(t4.s_ln, ', ', t4.s_fn, '  ', t4.s_mn) AS 'estudent', t4.s_gender, t5.course_code, t1.sg_yearlevel, (t3.class_schedule_id) AS 'ClassSchedule_ID', (t6.cb_code) AS 'Class', (t9.subject_code) AS 'Subject Code', (t9.subject_description) AS 'Subject Desc.', (t9.subject_units) AS 'Units', (t10.ds_code) AS 'Days', (time_start_schedule) AS 'Start Time', (t3.time_end_schedule) AS 'End Time', (t8.room_code) AS 'Room', (t7.Instructor) AS 'Instructor' FROM tbl_students_grades t1 JOIN tbl_class_schedule t3 ON t1.sg_class_id = t3.class_schedule_id AND t3.csperiod_id = t1.sg_period_id JOIN tbl_student t4 ON t1.sg_student_id = t4.s_id_no JOIN tbl_course t5 ON t1.sg_course_id = t5.course_id LEFT JOIN tbl_class_block t6 ON t3.class_block_id = t6.cb_id  LEFT JOIN employee t7 ON t3.csemp_id = t7.emp_id LEFT JOIN tbl_room t8 ON t3.csroom_id = t8.room_id LEFT JOIN tbl_subject t9 ON t3.cssubject_id = t9.subject_id LEFT JOIN tbl_day_schedule t10 ON t3.days_schedule = t10.ds_id JOIN tbl_enrollment t2 ON t4.s_id_no = t2.estudent_id and t1.sg_period_id = t2.eperiod_id WHERE t1.sg_class_id = " & CInt(dgClassSchedules.CurrentRow.Cells(1).Value) & " and t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and t1.sg_grade_status = 'Enrolled' order by t4.s_ln"
                    End If
                    Dim dbcommand As New MySqlCommand(sql, cn)
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
                        .Columns.Add("enrollment_code")
                        .Columns.Add("eperiod_id")
                        .Columns.Add("estudent_id")
                        .Columns.Add("estudent")
                        .Columns.Add("s_gender")
                        .Columns.Add("course_code")
                        .Columns.Add("s_yr_lvl")
                        .Columns.Add("class_schedule_id")
                        .Columns.Add("cb_code")
                        .Columns.Add("subject_code")
                        .Columns.Add("subject_description")
                        .Columns.Add("subject_units")
                        .Columns.Add("ds_code")
                        .Columns.Add("time_start_schedule")
                        .Columns.Add("time_end_schedule")
                        .Columns.Add("room_code")
                        .Columns.Add("Instructor")
                    End With

                    For Each dr As DataGridViewRow In dg_report.Rows
                        dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value)
                    Next

                    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rptdoc = New StudentMasterList
                    rptdoc.SetDataSource(dt)
                    rptdoc.SetParameterValue("subjectcode", subjectCode)
                    rptdoc.SetParameterValue("subjectdescription", subjectDesc)
                    rptdoc.SetParameterValue("subjectunits", subjectUnits)
                    rptdoc.SetParameterValue("subjectday", subjectDay)
                    rptdoc.SetParameterValue("subjecttimestart", subjectTstart)
                    rptdoc.SetParameterValue("subjecttimeend", subjectTend)
                    rptdoc.SetParameterValue("subjectroom", subjectRoom)
                    rptdoc.SetParameterValue("subjectclass", subjectClass)
                    rptdoc.SetParameterValue("subjectinstructor", subjectInstructor)
                    rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                    rptdoc.SetParameterValue("preparedby", str_name)
                    ReportViewer.ReportSource = rptdoc
                    dg_report.DataSource = Nothing
                    ReportViewer.Select()
                    ReportGenerated = True
                Catch ex As Exception
                    MsgBox(ex.Message, vbCritical)
                    cn.Close()
                    PrevBtn()
                End Try
            End If
        ElseIf frmMain.formTitle.Text = "Generate Academic Performance Slip" Then
            If studentId = String.Empty Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Student.", vbCritical)
                btnSearchStudent.Select()
            ElseIf CInt(cbAcademicYear.SelectedValue) <= 0 Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Academic Year.", vbCritical)
                cbAcademicYear.Select()
            Else
                Try
                    If cbAPS.Checked = True Then
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT * from tbl_enrollment where estudent_id = '" & studentId & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            NextBtn()
                            dr.Close()
                            cn.Close()

                            Dim grd_name As String = ""
                            Dim grd_course As String = ""
                            Dim grd_yrlevel As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT CONCAT(s_ln, ', ', s_fn,' ', s_mn) as 'sname', course_code, course_name, sg_yearlevel FROM tbl_student, tbl_course, tbl_students_grades where tbl_student.s_course_id = tbl_course.course_id and tbl_students_grades.sg_student_id = tbl_student.s_id_no and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "  and s_id_no = '" & studentId & "'", cn)
                            dr = cm.ExecuteReader
                            dr.Read()
                            If dr.HasRows Then
                                grd_name = dr.Item("sname").ToString.ToUpper
                                grd_yrlevel = dr.Item("sg_yearlevel").ToString.ToUpper
                                grd_course = dr.Item("course_code").ToString.ToUpper & " - " & dr.Item("course_name").ToString.ToUpper
                            Else
                            End If
                            dr.Close()
                            cn.Close()

                            cn.Open()
                            Dim dtable As DataTable
                            Dim dbcommand As New MySqlCommand("select CONCAT(PERIOD,' / ', course_name) as 'ACADEMIC YEAR', (subject_code) as 'CODE', (subject_description) as 'DESCRIPTION', IF(sg_grade > 0, ROUND(sg_grade,1),sg_grade) as 'GRADES', sg_credits as 'CREDIT' from tbl_students_grades, tbl_subject, period, tbl_course where tbl_students_grades.sg_subject_id = tbl_subject.subject_id and tbl_students_grades.sg_period_id = period.period_id and tbl_students_grades.sg_course_id = tbl_course.course_id and sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_grade_status = 'Enrolled'", cn)
                            Dim adt As New MySqlDataAdapter
                            adt.SelectCommand = dbcommand
                            dtable = New DataTable
                            adt.Fill(dtable)
                            dg_report.DataSource = dtable
                            adt.Dispose()
                            dbcommand.Dispose()
                            cn.Close()

                            Dim avg As String = ""
                            cn.Open()
                            cm = New MySqlCommand("select FORMAT(ROUND(AVG(sg_grade),1),1) as 'GRADES' from tbl_students_grades where sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and (sg_grade > 0 or sg_grade = 'D' or sg_grade  = 'W')", cn)
                            avg = cm.ExecuteScalar
                            cn.Close()

                            Dim total_subjects As String = ""
                            cn.Open()
                            cm = New MySqlCommand("select COUNT(sg_grade) as 'GRADES' from tbl_students_grades where sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            total_subjects = cm.ExecuteScalar
                            cn.Close()

                            Dim total_credits As String = ""
                            cn.Open()
                            cm = New MySqlCommand("select SUM(sg_credits) as 'CREDITS' from tbl_students_grades where sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            total_credits = cm.ExecuteScalar
                            cn.Close()

                            dt.Columns.Clear()
                            dt.Rows.Clear()
                            With dt
                                .Columns.Add("tor_code")
                                .Columns.Add("tor_description")
                                .Columns.Add("tor_grades")
                                .Columns.Add("tor_credit")
                            End With

                            For Each dr As DataGridViewRow In dg_report.Rows
                                dt.Rows.Add(dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value)
                            Next

                            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                            rptdoc = New Grading_Student_APS
                            rptdoc.SetDataSource(dt)
                            rptdoc.SetParameterValue("grd_prepared_by", str_name)
                            rptdoc.SetParameterValue("grd_name", grd_name)
                            rptdoc.SetParameterValue("grd_course", grd_course)
                            rptdoc.SetParameterValue("grd_year_level", grd_yrlevel)
                            rptdoc.SetParameterValue("grd_acad_year", cbAcademicYear.Text)
                            rptdoc.SetParameterValue("grd_date_issue", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
                            rptdoc.SetParameterValue("avgd", avg)

                            Dim average As Decimal
                            average = avg
                            If average = 1.1 Then
                                rptdoc.SetParameterValue("avg_eqv", "94% and above")
                            ElseIf average = 1.2 Then
                                rptdoc.SetParameterValue("avg_eqv", "93%")
                            ElseIf average = 1.3 Then
                                rptdoc.SetParameterValue("avg_eqv", "92%")
                            ElseIf average = 1.4 Then
                                rptdoc.SetParameterValue("avg_eqv", "91%")
                            ElseIf average = 1.5 Then
                                rptdoc.SetParameterValue("avg_eqv", "90%")
                            ElseIf average = 1.6 Then
                                rptdoc.SetParameterValue("avg_eqv", "89%")
                            ElseIf average = 1.7 Then
                                rptdoc.SetParameterValue("avg_eqv", "88%")
                            ElseIf average = 1.8 Then
                                rptdoc.SetParameterValue("avg_eqv", "87%")
                            ElseIf average = 1.9 Then
                                rptdoc.SetParameterValue("avg_eqv", "86%")
                            ElseIf average = 2.0 Then
                                rptdoc.SetParameterValue("avg_eqv", "85%")
                            ElseIf average = 2.1 Then
                                rptdoc.SetParameterValue("avg_eqv", "84%")
                            ElseIf average = 2.2 Then
                                rptdoc.SetParameterValue("avg_eqv", "83%")
                            ElseIf average = 2.3 Then
                                rptdoc.SetParameterValue("avg_eqv", "82%")
                            ElseIf average = 2.4 Then
                                rptdoc.SetParameterValue("avg_eqv", "81%")
                            ElseIf average = 2.5 Then
                                rptdoc.SetParameterValue("avg_eqv", "80%")
                            ElseIf average = 2.6 Then
                                rptdoc.SetParameterValue("avg_eqv", "79%")
                            ElseIf average = 2.7 Then
                                rptdoc.SetParameterValue("avg_eqv", "78%")
                            ElseIf average = 2.8 Then
                                rptdoc.SetParameterValue("avg_eqv", "77%")
                            ElseIf average = 2.9 Then
                                rptdoc.SetParameterValue("avg_eqv", "76%")
                            ElseIf average = 3.0 Then
                                rptdoc.SetParameterValue("avg_eqv", "75%")
                            ElseIf average > 3.0 Then
                                rptdoc.SetParameterValue("avg_eqv", "74% and below")
                            ElseIf average < 1 Then
                                rptdoc.SetParameterValue("avg_eqv", "0%")
                            End If

                            rptdoc.SetParameterValue("grd_total_subjects", total_subjects)
                            rptdoc.SetParameterValue("grd_total_credits", total_credits)
                            ReportViewer.ReportSource = rptdoc
                            dg_report.DataSource = Nothing
                            ReportViewer.Select()
                            ReportGenerated = True

                        Else
                            dr.Close()
                            cn.Close()
                            MsgBox("Student '" & txtStudent.Text & "' has no grades to be generated for this academic year " & cbAcademicYear.Text & ".", vbCritical)
                        End If
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, vbCritical)
                    cn.Close()
                    PrevBtn()
                End Try
            End If
        ElseIf frmMain.formTitle.Text = "Generate Instructor Subject Load" Then
            If insId = 0 Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Instructor.", vbCritical)
                btnSearchInstructor.Select()
            ElseIf CInt(cbAcademicYear.SelectedValue) <= 0 Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Academic Year.", vbCritical)
                cbAcademicYear.Select()
            Else
                Try
                    NextBtn()

                    cn.Close()
                    Dim periodname As String = ""
                    cn.Open()
                    cm = New MySqlCommand("SELECT `period_name` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                    periodname = cm.ExecuteScalar
                    cn.Close()

                    Dim periodsemester As String = ""
                    cn.Open()
                    cm = New MySqlCommand("SELECT `period_semester` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                    periodsemester = cm.ExecuteScalar
                    cn.Close()

                    Dim empBio As String = ""
                    cn.Open()
                    cm = New MySqlCommand("SELECT `emp_code` FROM `tbl_employee` WHERE `emp_id` = " & insId & "", cn)
                    empBio = cm.ExecuteScalar
                    cn.Close()

                    Dim empDesgination As String = ""
                    cn.Open()
                    cm = New MySqlCommand("SELECT `emp_designation` FROM `tbl_employee` WHERE `emp_id` = " & insId & "", cn)
                    empDesgination = cm.ExecuteScalar
                    cn.Close()


                    cn.Open()
                    Dim dtable As DataTable
                    Dim sql As String
                    If cbPetition.Checked = True Then
                        sql = "SELECT * FROM (SELECT t2.class_schedule_id, t8.cb_code, CONCAT(t5.period_name,' - ', t5.period_semester), t3.subject_code, t3.subject_description, if(t4.ds_code = 'M T W TH F SAT SUN', 'DAILY', t4.ds_code) as 'ClassDay', CONCAT(t2.time_start_schedule,' - ',t2.time_end_schedule) as 'ClassTime', CONCAT(t6.emp_first_name, ' ',t6.emp_middle_name, ' ', t6.emp_last_name), t7.room_code, t2.csemp_id, t2.csperiod_id, t2.cssubject_id, t3.subject_units, t9.Enrolled, t6.emp_last_name, t6.required_subject_load, t6.emp_code, t6.contact_no, t6.emp_status, t2.subject_load_status, STR_TO_DATE(t2.time_start_schedule,'%h:%i:%s %p')  as 'StartTime', t2.class_status FROM cfcissmsdb.tbl_class_schedule t2 JOIN cfcissmsdb.tbl_subject t3 ON t2.cssubject_id = t3.subject_id JOIN cfcissmsdb.tbl_day_schedule t4 ON t2.days_schedule = t4.ds_id JOIN cfcissmsdb.tbl_period t5 ON t2.csperiod_id = t5.period_id JOIN cfcissmsdb.tbl_employee t6 ON t2.csemp_id = t6.emp_id JOIN cfcissmsdb.tbl_room t7 ON t2.csroom_id = t7.room_id JOIN cfcissmsdb.tbl_class_block t8 ON t2.class_block_id = t8.cb_id JOIN (SELECT COUNT(sg_class_id) as 'Enrolled', t1.class_schedule_id, t1.csperiod_id FROM cfcissmsdb.tbl_class_schedule t1 LEFT JOIN cfcissmsdb.tbl_period t7 ON t1.csperiod_id = t7.period_id LEFT JOIN cfcissmsdb.tbl_students_grades t8 ON t1.class_schedule_id = t8.sg_class_id AND t1.csperiod_id = t8.sg_period_id Group by t1.class_schedule_id, t1.csperiod_id) t9 ON t2.class_schedule_id = t9.class_schedule_id and t2.csperiod_id = t9.csperiod_id WHERE t6.emp_code = '" & empBio & "' and t5.period_name = '" & periodname & "' and t5.period_semester = '" & periodsemester & "' and t2.cs_is_petition = 'Yes' and t2.is_active = 'Active' UNION ALL SELECT t2.class_schedule_id, t8.cb_code, CONCAT(t5.period_name,' - ', t5.period_semester), t3.subject_code, t3.subject_description, if(t4.ds_code = 'M T W TH F SAT SUN', 'DAILY', t4.ds_code) as 'ClassDay', CONCAT(t2.time_start_schedule,' - ',t2.time_end_schedule) as 'ClassTime', CONCAT(t6.emp_first_name, ' ',t6.emp_middle_name, ' ', t6.emp_last_name), t7.room_code, t2.csemp_id, t2.csperiod_id, t2.cssubject_id, t3.subject_units, t9.Enrolled, t6.emp_last_name, t6.required_subject_load, t6.emp_code, t6.contact_no, t6.emp_status, t2.subject_load_status, STR_TO_DATE(t2.time_start_schedule,'%h:%i:%s %p') as 'StartTime', t2.class_status FROM cfcissmsdbhighschool.tbl_class_schedule t2 JOIN cfcissmsdbhighschool.tbl_subject t3 ON t2.cssubject_id = t3.subject_id JOIN cfcissmsdbhighschool.tbl_day_schedule t4 ON t2.days_schedule = t4.ds_id JOIN cfcissmsdbhighschool.tbl_period t5 ON t2.csperiod_id = t5.period_id JOIN cfcissmsdbhighschool.tbl_employee t6 ON t2.csemp_id = t6.emp_id JOIN cfcissmsdbhighschool.tbl_room t7 ON t2.csroom_id = t7.room_id JOIN cfcissmsdbhighschool.tbl_class_block t8 ON t2.class_block_id = t8.cb_id JOIN (SELECT COUNT(sg_class_id) as 'Enrolled', t1.class_schedule_id, t1.csperiod_id FROM cfcissmsdbhighschool.tbl_class_schedule t1 LEFT JOIN cfcissmsdbhighschool.tbl_period t7 ON t1.csperiod_id = t7.period_id LEFT JOIN cfcissmsdbhighschool.tbl_students_grades t8 ON t1.class_schedule_id = t8.sg_class_id AND t1.csperiod_id = t8.sg_period_id Group by t1.class_schedule_id, t1.csperiod_id) t9 ON t2.class_schedule_id = t9.class_schedule_id and t2.csperiod_id = t9.csperiod_id WHERE t6.emp_code = '" & empBio & "' and t5.period_name = '" & periodname & "' and t5.period_semester = '" & periodsemester & "' and t2.cs_is_petition = 'Yes' and t2.is_active = 'Active') table1 order by ClassDay ASC, StartTime ASC"
                    Else
                        sql = "SELECT * FROM (SELECT t2.class_schedule_id, t8.cb_code, CONCAT(t5.period_name,' - ', t5.period_semester), t3.subject_code, t3.subject_description, if(t4.ds_code = 'M T W TH F SAT SUN', 'DAILY', t4.ds_code) as 'ClassDay', CONCAT(t2.time_start_schedule,' - ',t2.time_end_schedule) as 'ClassTime', CONCAT(t6.emp_first_name, ' ',t6.emp_middle_name, ' ', t6.emp_last_name), t7.room_code, t2.csemp_id, t2.csperiod_id, t2.cssubject_id, t3.subject_units, t9.Enrolled, t6.emp_last_name, t6.required_subject_load, t6.emp_code, t6.contact_no, t6.emp_status, t2.subject_load_status, STR_TO_DATE(t2.time_start_schedule,'%h:%i:%s %p')  as 'StartTime', t2.class_status FROM cfcissmsdb.tbl_class_schedule t2 JOIN cfcissmsdb.tbl_subject t3 ON t2.cssubject_id = t3.subject_id JOIN cfcissmsdb.tbl_day_schedule t4 ON t2.days_schedule = t4.ds_id JOIN cfcissmsdb.tbl_period t5 ON t2.csperiod_id = t5.period_id JOIN cfcissmsdb.tbl_employee t6 ON t2.csemp_id = t6.emp_id JOIN cfcissmsdb.tbl_room t7 ON t2.csroom_id = t7.room_id JOIN cfcissmsdb.tbl_class_block t8 ON t2.class_block_id = t8.cb_id JOIN (SELECT COUNT(sg_class_id) as 'Enrolled', t1.class_schedule_id, t1.csperiod_id FROM cfcissmsdb.tbl_class_schedule t1 LEFT JOIN cfcissmsdb.tbl_period t7 ON t1.csperiod_id = t7.period_id LEFT JOIN cfcissmsdb.tbl_students_grades t8 ON t1.class_schedule_id = t8.sg_class_id AND t1.csperiod_id = t8.sg_period_id Group by t1.class_schedule_id, t1.csperiod_id) t9 ON t2.class_schedule_id = t9.class_schedule_id and t2.csperiod_id = t9.csperiod_id WHERE t6.emp_code = '" & empBio & "' and t5.period_name = '" & periodname & "' and t5.period_semester = '" & periodsemester & "' and t2.cs_is_petition NOT IN ('Yes') and t2.is_active = 'Active' UNION ALL SELECT t2.class_schedule_id, t8.cb_code, CONCAT(t5.period_name,' - ', t5.period_semester), t3.subject_code, t3.subject_description, if(t4.ds_code = 'M T W TH F SAT SUN', 'DAILY', t4.ds_code) as 'ClassDay', CONCAT(t2.time_start_schedule,' - ',t2.time_end_schedule) as 'ClassTime', CONCAT(t6.emp_first_name, ' ',t6.emp_middle_name, ' ', t6.emp_last_name), t7.room_code, t2.csemp_id, t2.csperiod_id, t2.cssubject_id, t3.subject_units, t9.Enrolled, t6.emp_last_name, t6.required_subject_load, t6.emp_code, t6.contact_no, t6.emp_status, t2.subject_load_status, STR_TO_DATE(t2.time_start_schedule,'%h:%i:%s %p') as 'StartTime', t2.class_status FROM cfcissmsdbhighschool.tbl_class_schedule t2 JOIN cfcissmsdbhighschool.tbl_subject t3 ON t2.cssubject_id = t3.subject_id JOIN cfcissmsdbhighschool.tbl_day_schedule t4 ON t2.days_schedule = t4.ds_id JOIN cfcissmsdbhighschool.tbl_period t5 ON t2.csperiod_id = t5.period_id JOIN cfcissmsdbhighschool.tbl_employee t6 ON t2.csemp_id = t6.emp_id JOIN cfcissmsdbhighschool.tbl_room t7 ON t2.csroom_id = t7.room_id JOIN cfcissmsdbhighschool.tbl_class_block t8 ON t2.class_block_id = t8.cb_id JOIN (SELECT COUNT(sg_class_id) as 'Enrolled', t1.class_schedule_id, t1.csperiod_id FROM cfcissmsdbhighschool.tbl_class_schedule t1 LEFT JOIN cfcissmsdbhighschool.tbl_period t7 ON t1.csperiod_id = t7.period_id LEFT JOIN cfcissmsdbhighschool.tbl_students_grades t8 ON t1.class_schedule_id = t8.sg_class_id AND t1.csperiod_id = t8.sg_period_id Group by t1.class_schedule_id, t1.csperiod_id) t9 ON t2.class_schedule_id = t9.class_schedule_id and t2.csperiod_id = t9.csperiod_id WHERE t6.emp_code = '" & empBio & "' and t5.period_name = '" & periodname & "' and t5.period_semester = '" & periodsemester & "' and t2.cs_is_petition NOT IN ('Yes') and t2.is_active = 'Active') table1 order by ClassDay ASC, StartTime ASC"
                    End If
                    Dim dbcommand As New MySqlCommand(sql, cn)
                    Dim adt As New MySqlDataAdapter
                    adt.SelectCommand = dbcommand
                    dtable = New DataTable
                    adt.Fill(dtable)
                    dg_report.DataSource = dtable
                    adt.Dispose()
                    dbcommand.Dispose()
                    cn.Close()

                    dt.Columns.Clear()
                    dt.Rows.Clear()
                    With dt
                        .Columns.Add("class_schedule_id")
                        .Columns.Add("Class")
                        .Columns.Add("Period")
                        .Columns.Add("Code")
                        .Columns.Add("Subject")
                        .Columns.Add("Day")
                        .Columns.Add("Time")
                        .Columns.Add("Instructor")
                        .Columns.Add("Room")
                        .Columns.Add("cs_emp_id")
                        .Columns.Add("csperiod_id")
                        .Columns.Add("cssubject_id")
                        .Columns.Add("subject_units")
                        .Columns.Add("Population")
                        .Columns.Add("lastname")
                        .Columns.Add("required_load")
                        .Columns.Add("emp_code")
                        .Columns.Add("contact_number")
                        .Columns.Add("status")
                        .Columns.Add("loading_status")
                        .Columns.Add("class_status")
                    End With
                    For Each dr As DataGridViewRow In dg_report.Rows
                        dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value, dr.Cells(11).Value, dr.Cells(12).Value, dr.Cells(13).Value, dr.Cells(14).Value.ToString.ToUpper, dr.Cells(15).Value, dr.Cells(16).Value, dr.Cells(17).Value, dr.Cells(18).Value, dr.Cells(19).Value, dr.Cells(21).Value)
                    Next
                    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

                    If cbPetition.Checked = True Then
                        rptdoc = New InstructorLoadPetition
                        rptdoc.SetDataSource(dt)
                        rptdoc.SetParameterValue("instructor_name", insName)
                        rptdoc.SetParameterValue("date", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
                        rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                        rptdoc.SetParameterValue("president_admin", cbPresident.Text)
                        rptdoc.SetParameterValue("prepared_by", str_name)
                        rptdoc.SetParameterValue("instructor_lastname", insFName)
                        rptdoc.SetParameterValue("acad_coordinator", cbAcadCoor.Text)
                        ReportViewer.ReportSource = rptdoc
                    Else
                        rptdoc = New InstructorLoad
                        rptdoc.SetDataSource(dt)
                        rptdoc.SetParameterValue("instructor_name", insName)
                        rptdoc.SetParameterValue("date", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
                        rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                        rptdoc.SetParameterValue("president_admin", cbPresident.Text)
                        rptdoc.SetParameterValue("prepared_by", str_name)
                        rptdoc.SetParameterValue("instructor_lastname", insFName)
                        rptdoc.SetParameterValue("acad_coordinator", cbAcadCoor.Text)
                        rptdoc.SetParameterValue("designation", empDesgination)
                        ReportViewer.ReportSource = rptdoc
                    End If
                    dg_report.DataSource = Nothing
                    ReportViewer.Select()
                    ReportGenerated = True
                Catch ex As Exception
                    MsgBox(ex.Message, vbCritical)
                    cn.Close()
                    PrevBtn()
                End Try
            End If
        ElseIf frmMain.formTitle.Text = "Generate Statement Of Account" Then
            If studentId = String.Empty Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Student.", vbCritical)
                btnSearchStudent.Select()
            ElseIf CInt(cbAcademicYear.SelectedValue) <= 0 Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Academic Year.", vbCritical)
                cbAcademicYear.Select()
            Else
                If frmMain.systemModule.Text = "College Module" Then
                    If str_role = "Administrator" Or str_role = "Cashier" Then
                        Dim dr As DialogResult
                        dr = MessageBox.Show("Do you want to print SOA for scholarship purpose?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If dr = DialogResult.No Then
                            PrintSOA()
                        Else
                            PrintSOAScholar()
                        End If
                    Else
                        PrintSOA()
                    End If
                Else
                    PrintSOA()
                End If
            End If
        ElseIf frmMain.formTitle.Text = "Generate Transcript Of Records" Then
            If studentId = String.Empty Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Student.", vbCritical)
                btnSearchStudent.Select()
            Else
                Try
                    NextBtn()
                    Dim tor_name As String = ""
                    Dim tor_bday As String = ""
                    Dim tor_stud_address As String = ""
                    Dim tor_bplace As String = ""
                    Dim tor_parentguardian As String = ""
                    Dim tor_pg_address As String = ""
                    Dim tor_nationality As String = ""
                    Dim tor_entrance_date As String = ""
                    Dim tor_entrance_credential As String = ""
                    Dim tor_lastyearattendance As String = ""
                    Dim tor_date_graduation As String = ""
                    Dim tor_so_no As String = ""
                    Dim tor_elementary_school As String = ""
                    Dim tor_eschool_year As String = ""
                    Dim tor_js_school As String = ""
                    Dim tor_jsschool_year As String = ""
                    Dim tor_sschool As String = ""
                    Dim tor_sschool_year As String = ""
                    Dim tor_cschool As String = ""
                    Dim tor_cschool_year As String = ""
                    Dim tor_title_degree As String = ""
                    Dim tor_nstp_no As String = ""
                    Dim honors As String = ""
                    Dim tor_major As String = ""
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT s_id_no, CONCAT(s_ln, ', ', s_fn, ' ', s_mn) AS 'sname', s_dob, CONCAT(s_address,', BRGY.',refbrgy.brgyDesc,', ',refcitymun.citymunDesc,', ',refprovince.provDesc) as Address, s_pob, s_guardian_name, s_guardian_address, s_nationality, s_begin_date, s_ent_cred, DATE_FORMAT(COALESCE(STR_TO_DATE(s_grad_date, '%m/%d/%Y'),STR_TO_DATE(s_grad_date, '%M %d, %Y'),STR_TO_DATE(s_grad_date, '%M %d %Y')),'%M %Y') as lastattendance, DATE_FORMAT(COALESCE(STR_TO_DATE(s_grad_date, '%m/%d/%Y'),STR_TO_DATE(s_grad_date, '%M %d, %Y'),STR_TO_DATE(s_grad_date, '%M %d %Y')),'%M %d, %Y') as grad_date, s_so_no, IF( s_p_school_id = '', ' ', t3.schl_name) AS 'Elementary', IF( s_p_school_id = '', ' ', s_p_school_ya) as 'Elementary School Year', IF( s_sh_school_id = '', ' ', t5.schl_name) AS 'Junior High', IF( s_sh_school_id = '', ' ', s_s_school_ya) as 'Junior High School Year', IF( s_s_school_id = '', ' ', t4.schl_name) AS 'Senior High', IF( s_s_school_id = '', ' ', s_sh_school_ya) as 'Senior High School Year', course_code, course_name, s_nstp_no, IF( s_c_school_id = '', ' ', t6.schl_name) AS 'College', IF( s_c_school_id = '', ' ', s_c_school_ya) as 'College School Year', s_acad_awards, course_major FROM tbl_student t1 LEFT JOIN tbl_course t2 ON t1.s_course_id = t2.course_id LEFT JOIN tbl_schools t3 ON t1.s_p_school_id = t3.schl_id LEFT JOIN tbl_schools t4 ON t1.s_sh_school_id = t4.schl_id LEFT JOIN tbl_schools t5 ON t1.s_s_school_id = t5.schl_id LEFT JOIN tbl_schools t6 ON t1.s_c_school_id = t6.schl_id LEFT JOIN refprovince ON  t1.s_address_prov = refprovince.provCode LEFT JOIN refcitymun ON t1.s_address_citymun = refcitymun.citymunCode LEFT JOIN refbrgy ON t1.s_address_brgy = refbrgy.brgyCode WHERE t1.s_id_no = '" & studentId & "'", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        tor_name = dr.Item("sname").ToString
                        tor_bday = dr.Item("s_dob").ToString
                        tor_stud_address = dr.Item("Address").ToString
                        tor_bplace = dr.Item("s_pob").ToString
                        tor_parentguardian = dr.Item("s_guardian_name").ToString
                        tor_pg_address = dr.Item("s_guardian_address").ToString
                        tor_nationality = dr.Item("s_nationality").ToString
                        tor_entrance_date = dr.Item("s_begin_date").ToString
                        tor_entrance_credential = dr.Item("s_ent_cred").ToString
                        tor_lastyearattendance = dr.Item("lastattendance").ToString.ToUpper
                        tor_date_graduation = dr.Item("grad_date").ToString.ToUpper
                        tor_so_no = dr.Item("s_so_no").ToString
                        tor_elementary_school = dr.Item("Elementary").ToString
                        tor_eschool_year = dr.Item("Elementary School Year").ToString
                        tor_js_school = dr.Item("Junior High").ToString
                        tor_jsschool_year = dr.Item("Junior High School Year").ToString
                        tor_sschool = dr.Item("Senior High").ToString
                        tor_sschool_year = dr.Item("Senior High School Year").ToString
                        tor_title_degree = dr.Item("course_code").ToString + " - " + dr.Item("course_name").ToString
                        tor_nstp_no = dr.Item("s_nstp_no").ToString
                        tor_cschool = dr.Item("College").ToString
                        tor_cschool_year = dr.Item("College School Year").ToString
                        honors = dr.Item("s_acad_awards").ToString
                        tor_major = dr.Item("course_major").ToString
                    End If
                    dr.Close()
                    cn.Close()
                    cn.Open()
                    Dim dtable As DataTable
                    Dim dbcommand As New MySqlCommand("select (schl_name) as 'SCHOOL', concat(period_name,'-',period_semester) as 'ACADEMIC YEAR', (subject_code) as 'CODE', (subject_description) as 'DESCRIPTION', if(sg_grade REGEXP '^-?[0-9]+$' >  0 and sg_grade < 6 and sg_school_id = '0' , ROUND(sg_grade,1), sg_grade)  as 'GRADES', (sg_credits) as 'CREDIT', course_name as 'COURSE', sg_grade_remarks from tbl_students_grades, tbl_subject, tbl_period, tbl_schools, tbl_course where tbl_students_grades.sg_subject_id = tbl_subject.subject_id and tbl_students_grades.sg_period_id = tbl_period.period_id and tbl_students_grades.sg_course_id = tbl_course.course_id and tbl_students_grades.sg_school_id = tbl_schools.schl_id and sg_student_id = '" & studentId & "' and sg_grade_visibility NOT IN (1) and sg_grade_status NOT IN ('Pending') and period_semester NOT IN ('Review') order by period_name, period_semester, subject_code asc", cn)
                    Dim adt As New MySqlDataAdapter
                    adt.SelectCommand = dbcommand
                    dtable = New DataTable
                    adt.Fill(dtable)
                    dg_report.DataSource = dtable
                    adt.Dispose()
                    dbcommand.Dispose()
                    cn.Close()
                    cn.Open()
                    Dim avg As String = ""
                    cm = New MySqlCommand("select FORMAT(ROUND(AVG(sg_grade),1),1) as 'GRADES' from tbl_students_grades where sg_student_id = '" & studentId & "' and sg_school_id = '0' and sg_grade NOT IN('') and sg_grade_visibility NOT IN (1) and sg_grade_status NOT IN ('Pending')", cn)
                    avg = cm.ExecuteScalar
                    cn.Close()
                    Try
                        cn.Open()
                        cm = New MySqlCommand("select sp_profile_photo from cfcissmsdb.tbl_student_photos where sp_student_id = @1", cn)
                        With cm
                            .Parameters.AddWithValue("@1", studentId)
                        End With
                        dr = cm.ExecuteReader
                        While dr.Read
                            Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                            Dim array(CInt(len)) As Byte
                            dr.GetBytes(0, 0, array, 0, CInt(len))
                            Dim ms As New MemoryStream(array)
                            Dim bitmap As New System.Drawing.Bitmap(ms)
                            studentpicture.Image = bitmap
                        End While
                        dr.Close()
                        cn.Close()
                    Catch ex As Exception
                        cn.Close()
                        studentpicture.Image = dummySpicture.Image
                    End Try

                    Try
                        Dim sPath As String = IO.Path.Combine("" & Application.StartupPath() & "\STUDENTPHOTOS")
                        If Not IO.Directory.Exists(sPath) Then
                            IO.Directory.CreateDirectory(sPath)
                            studentpicture.Image.Save("" & Application.StartupPath() & "\STUDENTPHOTOS\" & studentId & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
                        Else
                            studentpicture.Image.Save("" & Application.StartupPath() & "\STUDENTPHOTOS\" & studentId & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
                        End If
                    Catch ex As Exception

                    End Try


                    dt.Columns.Clear()
                    dt.Rows.Clear()
                    With dt
                        .Columns.Add("tor_school")
                        .Columns.Add("tor_academic_year")
                        .Columns.Add("tor_code")
                        .Columns.Add("tor_description")
                        .Columns.Add("tor_grades")
                        .Columns.Add("tor_credit")
                        .Columns.Add("tor_course")
                    End With

                    For Each dr As DataGridViewRow In dg_report.Rows
                        dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value & "  " & dr.Cells(7).Value)
                    Next

                    If str_role = "Registrar" Or str_role = "Administrator" Then
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New Grading_Student_TOR
                        rptdoc.SetDataSource(dt)
                        rptdoc.SetParameterValue("tor_name", tor_name)
                        rptdoc.SetParameterValue("tor_bday", Format(Convert.ToDateTime(tor_bday), "MMMM d, yyyy").ToUpper)
                        rptdoc.SetParameterValue("tor_stud_address", tor_stud_address)
                        rptdoc.SetParameterValue("tor_bplace", tor_bplace)
                        rptdoc.SetParameterValue("tor_parentguardian", tor_parentguardian)
                        rptdoc.SetParameterValue("tor_pg_address", tor_pg_address)
                        rptdoc.SetParameterValue("tor_nationality", tor_nationality)
                        rptdoc.SetParameterValue("tor_entrance_date", Format(Convert.ToDateTime(tor_entrance_date), "MMMM d, yyyy").ToUpper)
                        rptdoc.SetParameterValue("tor_entrance_credential", tor_entrance_credential)
                        If cbGradDate.Checked = True Then
                            rptdoc.SetParameterValue("tor_date_graduation", tor_date_graduation)
                        Else
                            rptdoc.SetParameterValue("tor_date_graduation", " ")
                        End If
                        rptdoc.SetParameterValue("tor_so_no", tor_so_no)
                        rptdoc.SetParameterValue("tor_elementary_school", tor_elementary_school)
                        rptdoc.SetParameterValue("tor_eschool_year", tor_eschool_year)
                        rptdoc.SetParameterValue("tor_js_school", tor_js_school)
                        rptdoc.SetParameterValue("tor_jsschool_year", tor_jsschool_year)
                        rptdoc.SetParameterValue("tor_sschool", tor_sschool)
                        rptdoc.SetParameterValue("tor_sschool_year", tor_sschool_year)

                        If txtRemarks.Text = "VALID FOR BOARD EXAMINATION PURPOSES ONLY" AndAlso txtRemarksYear.Text IsNot String.Empty Then
                            rptdoc.SetParameterValue("tor_remarks", "VALID FOR " & txtRemarksYear.Text & " BOARD EXAMINATION PURPOSES ONLY")
                        ElseIf txtRemarks.Text = "VALID FOR REAL ESTATE BROKERS LICENSURE EXAMINATION PURPOSES ONLY" AndAlso txtRemarksYear.Text IsNot String.Empty Then
                            rptdoc.SetParameterValue("tor_remarks", "VALID FOR " & txtRemarksYear.Text & " REAL ESTATE BROKERS LICENSURE EXAMINATION PURPOSES ONLY")
                            'ElseIf txtRemarks.Text = "VALID FOR TRANSFER PURPOSES ONLY" Then
                            '    rptdoc.SetParameterValue("tor_remarks", "")
                        Else
                            rptdoc.SetParameterValue("tor_remarks", txtRemarks.Text)
                        End If

                        rptdoc.SetParameterValue("tor_prepared_by", str_name.ToUpper)
                        rptdoc.SetParameterValue("tor_date_issue", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
                        rptdoc.SetParameterValue("tor_registrar", cbRegistrar.Text)
                        rptdoc.SetParameterValue("tor_president_admin", cbPresident.Text)
                        rptdoc.SetParameterValue("tor_title_degree", tor_title_degree)

                        If cbNote.Checked = True Then
                            rptdoc.SetParameterValue("tor_rfg", txtNote.Text)
                        Else
                            rptdoc.SetParameterValue("tor_rfg", "")
                        End If
                        cn.Close()
                        cn.Open()
                        Dim empDesignation As String = ""
                        cm = New MySqlCommand("SELECT emp_designation FROM `tbl_employee` where emp_id = " & CInt(cbPresident.SelectedValue) & "", cn)
                        empDesignation = cm.ExecuteScalar
                        cn.Close()
                        rptdoc.SetParameterValue("tor_role", empDesignation)
                        rptdoc.SetParameterValue("tor_nstp_no", tor_nstp_no)
                        rptdoc.SetParameterValue("tor_pic", "" & Application.StartupPath() & "\STUDENTPHOTOS\" & studentId & ".jpg")
                        rptdoc.SetParameterValue("tor_c_school", tor_cschool)
                        rptdoc.SetParameterValue("tor_c_school_year", tor_cschool_year)
                        rptdoc.SetParameterValue("tor_avg", avg)

                        If cbGradDate.Checked = True Then
                            rptdoc.SetParameterValue("dategrad", "DATE OF GRADUATION")
                        Else
                            rptdoc.SetParameterValue("dategrad", " ")
                        End If

                        rptdoc.SetParameterValue("tor_lastyearattendance", tor_lastyearattendance)

                        rptdoc.SetParameterValue("honors", honors)

                        rptdoc.SetParameterValue("tor_major", tor_major)
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                    Else
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New Grading_Student_TOR_V2
                        rptdoc.SetDataSource(dt)
                        rptdoc.SetParameterValue("tor_name", tor_name)
                        rptdoc.SetParameterValue("tor_title_degree", tor_title_degree)
                        rptdoc.SetParameterValue("tor_rfg", "-Transcript Closed-")
                        rptdoc.SetParameterValue("tor_pic", "" & Application.StartupPath() & "\STUDENTPHOTOS\" & studentId & ".jpg")
                        rptdoc.SetParameterValue("tor_avg", avg)
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                    End If
                Catch ex As Exception
                    MsgBox("Student '" & txtStudent.Text & "' has no TOR yet to be generated.", vbCritical)
                    cn.Close()
                    PrevBtn()
                End Try
            End If
        ElseIf frmMain.formTitle.Text = "Generate Form 9" Then


            If studentId = String.Empty Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Student.", vbCritical)
                btnSearchStudent.Select()
            Else
                Try
                    NextBtn()
                    Dim tor_name As String = ""
                    Dim tor_bday As String = ""
                    Dim tor_stud_address As String = ""
                    Dim tor_bplace As String = ""
                    Dim tor_parentguardian As String = ""
                    Dim tor_pg_address As String = ""
                    Dim tor_nationality As String = ""
                    Dim tor_entrance_date As String = ""
                    Dim tor_entrance_credential As String = ""
                    Dim tor_lastyearattendance As String = ""
                    Dim tor_date_graduation As String = ""
                    Dim tor_so_no As String = ""
                    Dim tor_elementary_school As String = ""
                    Dim tor_eschool_year As String = ""
                    Dim tor_js_school As String = ""
                    Dim tor_jsschool_year As String = ""
                    Dim tor_sschool As String = ""
                    Dim tor_sschool_year As String = ""
                    Dim tor_cschool As String = ""
                    Dim tor_cschool_year As String = ""
                    Dim tor_title_degree As String = ""
                    Dim tor_nstp_no As String = ""
                    Dim honors As String = ""
                    Dim tor_major As String = ""
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT s_id_no, CONCAT(s_ln, ', ', s_fn, ' ', s_mn) AS 'sname', s_dob, CONCAT(s_address,', BRGY.',refbrgy.brgyDesc,', ',refcitymun.citymunDesc,', ',refprovince.provDesc) as Address, s_pob, s_guardian_name, s_guardian_address, s_nationality, s_begin_date, s_ent_cred, DATE_FORMAT(COALESCE(STR_TO_DATE(s_grad_date, '%m/%d/%Y'),STR_TO_DATE(s_grad_date, '%M %d, %Y'),STR_TO_DATE(s_grad_date, '%M %d %Y')),'%M %Y') as lastattendance, DATE_FORMAT(COALESCE(STR_TO_DATE(s_grad_date, '%m/%d/%Y'),STR_TO_DATE(s_grad_date, '%M %d, %Y'),STR_TO_DATE(s_grad_date, '%M %d %Y')),'%M %d, %Y') as grad_date, s_so_no, IF( s_p_school_id = '', ' ', t3.schl_name) AS 'Elementary', IF( s_p_school_id = '', ' ', s_p_school_ya) as 'Elementary School Year', IF( s_sh_school_id = '', ' ', t5.schl_name) AS 'Junior High', IF( s_sh_school_id = '', ' ', s_s_school_ya) as 'Junior High School Year', IF( s_s_school_id = '', ' ', t4.schl_name) AS 'Senior High', IF( s_s_school_id = '', ' ', s_sh_school_ya) as 'Senior High School Year', course_code, course_name, s_nstp_no, IF( s_c_school_id = '', ' ', t6.schl_name) AS 'College', IF( s_c_school_id = '', ' ', s_c_school_ya) as 'College School Year', s_acad_awards, course_major FROM tbl_student t1 LEFT JOIN tbl_course t2 ON t1.s_course_id = t2.course_id LEFT JOIN tbl_schools t3 ON t1.s_p_school_id = t3.schl_id LEFT JOIN tbl_schools t4 ON t1.s_sh_school_id = t4.schl_id LEFT JOIN tbl_schools t5 ON t1.s_s_school_id = t5.schl_id LEFT JOIN tbl_schools t6 ON t1.s_c_school_id = t6.schl_id LEFT JOIN refprovince ON  t1.s_address_prov = refprovince.provCode LEFT JOIN refcitymun ON t1.s_address_citymun = refcitymun.citymunCode LEFT JOIN refbrgy ON t1.s_address_brgy = refbrgy.brgyCode WHERE t1.s_id_no = '" & studentId & "'", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        tor_name = dr.Item("sname").ToString
                        tor_bday = dr.Item("s_dob").ToString
                        tor_stud_address = dr.Item("Address").ToString
                        tor_bplace = dr.Item("s_pob").ToString
                        tor_parentguardian = dr.Item("s_guardian_name").ToString
                        tor_pg_address = dr.Item("s_guardian_address").ToString
                        tor_nationality = dr.Item("s_nationality").ToString
                        tor_entrance_date = dr.Item("s_begin_date").ToString
                        tor_entrance_credential = dr.Item("s_ent_cred").ToString
                        tor_lastyearattendance = dr.Item("lastattendance").ToString.ToUpper
                        tor_date_graduation = dr.Item("grad_date").ToString.ToUpper
                        tor_so_no = dr.Item("s_so_no").ToString
                        tor_elementary_school = dr.Item("Elementary").ToString
                        tor_eschool_year = dr.Item("Elementary School Year").ToString
                        tor_js_school = dr.Item("Junior High").ToString
                        tor_jsschool_year = dr.Item("Junior High School Year").ToString
                        tor_sschool = dr.Item("Senior High").ToString
                        tor_sschool_year = dr.Item("Senior High School Year").ToString
                        tor_title_degree = dr.Item("course_code").ToString + " - " + dr.Item("course_name").ToString
                        tor_nstp_no = dr.Item("s_nstp_no").ToString
                        tor_cschool = dr.Item("College").ToString
                        tor_cschool_year = dr.Item("College School Year").ToString
                        honors = dr.Item("s_acad_awards").ToString
                        tor_major = dr.Item("course_major").ToString
                    End If
                    dr.Close()
                    cn.Close()
                    cn.Open()
                    Dim dtable As DataTable
                    Dim dbcommand As New MySqlCommand("select (schl_name) as 'SCHOOL', concat(period_name,'-',period_semester) as 'ACADEMIC YEAR', (subject_code) as 'CODE', (subject_description) as 'DESCRIPTION', sg_grade as 'GRADES', (subject_units) as 'CREDIT',IF(subject_group = 'English', IF (sg_credits = 0, 0, sg_credits), NULL) as English,IF(subject_group = 'Filipino', IF (sg_credits = 0, 0, sg_credits), NULL) as Filipino,IF(subject_group = 'Math', IF (sg_credits = 0, 0, sg_credits), NULL) as Math,IF(subject_group = 'Social Science', IF (sg_credits = 0, 0, sg_credits), NULL) as SocSci,IF(subject_group = 'Elective Major', IF (sg_credits = 0, 0, sg_credits), NULL) as Elecmajor,IF(subject_group = 'Elective Minor', IF (sg_credits = 0, 0, sg_credits), NULL) as Elecminor,IF(subject_group = 'Health Science', IF (sg_credits = 0, 0, sg_credits), NULL) as Healsci,IF(subject_group = 'Physical Education', IF (sg_credits = 0, 0, sg_credits), NULL) as PE,IF(subject_group = 'CWTS CMT', IF (sg_credits = 0, 0, sg_credits), NULL) as CWTSCMT, course_name from tbl_students_grades, tbl_subject, tbl_period, tbl_schools, tbl_course where tbl_students_grades.sg_subject_id = tbl_subject.subject_id and tbl_students_grades.sg_period_id = tbl_period.period_id and tbl_students_grades.sg_course_id = tbl_course.course_id and tbl_students_grades.sg_school_id = tbl_schools.schl_id and sg_student_id = '" & studentId & "' and sg_grade_visibility NOT IN (1) and sg_grade_status NOT IN ('Pending') and period_semester NOT IN ('Review') and period_status NOT IN ('Active') order by period_name, period_semester, subject_code asc", cn)
                    Dim adt As New MySqlDataAdapter
                    adt.SelectCommand = dbcommand
                    dtable = New DataTable
                    adt.Fill(dtable)
                    dg_report.DataSource = dtable
                    adt.Dispose()
                    dbcommand.Dispose()
                    cn.Close()
                    cn.Open()
                    Dim avg As String = ""
                    cm = New MySqlCommand("select FORMAT(ROUND(AVG(sg_grade),1),1) as 'GRADES' from tbl_students_grades where sg_student_id = '" & studentId & "' and sg_school_id = '0' and sg_grade NOT IN('') and sg_grade_visibility NOT IN (1) and sg_grade_status NOT IN ('Pending')", cn)
                    avg = cm.ExecuteScalar
                    cn.Close()

                    dt.Columns.Clear()
                    dt.Rows.Clear()
                    With dt
                        .Columns.Add("tor_school")
                        .Columns.Add("tor_academic_year")
                        .Columns.Add("tor_code")
                        .Columns.Add("tor_description")
                        .Columns.Add("tor_grades")
                        .Columns.Add("tor_credit")
                        .Columns.Add("form9_english")
                        .Columns.Add("form9_filipino")
                        .Columns.Add("form9_math")
                        .Columns.Add("form9_socsci")
                        .Columns.Add("form9_elecmajor")
                        .Columns.Add("form9_elecminor")
                        .Columns.Add("form9_healsci")
                        .Columns.Add("form9_pe")
                        .Columns.Add("form9_cwtscmt")
                        .Columns.Add("tor_course")
                    End With

                    For Each dr As DataGridViewRow In dg_report.Rows
                        dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value, dr.Cells(11).Value, dr.Cells(12).Value, dr.Cells(13).Value, dr.Cells(14).Value, dr.Cells(15).Value)
                    Next


                    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rptdoc = New Grading_Student_Form94
                    rptdoc.SetDataSource(dt)
                    rptdoc.SetParameterValue("tor_name", tor_name)
                    rptdoc.SetParameterValue("tor_bday", Format(Convert.ToDateTime(tor_bday), "MMMM d, yyyy").ToUpper)
                    rptdoc.SetParameterValue("tor_stud_address", tor_stud_address)
                    rptdoc.SetParameterValue("tor_bplace", tor_bplace)
                    rptdoc.SetParameterValue("tor_parentguardian", tor_parentguardian)
                    rptdoc.SetParameterValue("tor_pg_address", tor_pg_address)
                    rptdoc.SetParameterValue("tor_nationality", tor_nationality)
                    rptdoc.SetParameterValue("tor_entrance_date", Format(Convert.ToDateTime(tor_entrance_date), "MMMM d, yyyy").ToUpper)
                    rptdoc.SetParameterValue("tor_entrance_credential", tor_entrance_credential)
                    rptdoc.SetParameterValue("tor_date_graduation", tor_date_graduation)

                    rptdoc.SetParameterValue("tor_so_no", tor_so_no)
                    rptdoc.SetParameterValue("tor_elementary_school", tor_elementary_school)
                    rptdoc.SetParameterValue("tor_eschool_year", tor_eschool_year)
                    rptdoc.SetParameterValue("tor_js_school", tor_js_school)
                    rptdoc.SetParameterValue("tor_jsschool_year", tor_jsschool_year)
                    rptdoc.SetParameterValue("tor_sschool", tor_sschool)
                    rptdoc.SetParameterValue("tor_sschool_year", tor_sschool_year)

                    If txtRemarks.Text = "VALID FOR BOARD EXAMINATION PURPOSES ONLY" AndAlso txtRemarksYear.Text IsNot String.Empty Then
                        rptdoc.SetParameterValue("tor_remarks", "VALID FOR " & txtRemarksYear.Text & " BOARD EXAMINATION PURPOSES ONLY")
                    ElseIf txtRemarks.Text = "VALID FOR REAL ESTATE BROKERS LICENSURE EXAMINATION PURPOSES ONLY" AndAlso txtRemarksYear.Text IsNot String.Empty Then
                        rptdoc.SetParameterValue("tor_remarks", "VALID FOR " & txtRemarksYear.Text & " REAL ESTATE BROKERS LICENSURE EXAMINATION PURPOSES ONLY")
                    Else
                        rptdoc.SetParameterValue("tor_remarks", txtRemarks.Text)
                    End If

                    rptdoc.SetParameterValue("tor_prepared_by", str_name.ToUpper)
                    rptdoc.SetParameterValue("tor_date_issue", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
                    rptdoc.SetParameterValue("tor_registrar", cbRegistrar.Text)
                    'rptdoc.SetParameterValue("tor_president_admin", cbPresident.Text)
                    rptdoc.SetParameterValue("tor_title_degree", tor_title_degree)
                    If cbNote.Checked = True Then
                        rptdoc.SetParameterValue("tor_rfg", txtNote.Text)
                    Else
                        rptdoc.SetParameterValue("tor_rfg", "")
                    End If
                    cn.Close()
                    cn.Open()
                    Dim empDesignation As String = ""
                    cm = New MySqlCommand("SELECT emp_designation FROM `tbl_employee` where emp_id = " & CInt(cbPresident.SelectedValue) & "", cn)
                    empDesignation = cm.ExecuteScalar
                    cn.Close()
                    'rptdoc.SetParameterValue("tor_role", empDesignation)
                    rptdoc.SetParameterValue("tor_nstp_no", tor_nstp_no)
                    'rptdoc.SetParameterValue("tor_pic", "" & Application.StartupPath() & "\STUDENTPHOTOS\" & studentId & ".jpg")
                    'rptdoc.SetParameterValue("tor_c_school", tor_cschool)
                    'rptdoc.SetParameterValue("tor_c_school_year", tor_cschool_year)
                    'rptdoc.SetParameterValue("tor_avg", avg)
                    'If cbGradDate.Checked = True Then
                    '    rptdoc.SetParameterValue("dategrad", "DATE OF GRADUATION")
                    'Else
                    '    rptdoc.SetParameterValue("dategrad", " ")
                    'End If
                    rptdoc.SetParameterValue("tor_lastyearattendance", tor_lastyearattendance)
                    rptdoc.SetParameterValue("honors", honors)
                    rptdoc.SetParameterValue("tor_major", tor_major)

                    'cn.Open()
                    'Dim dtable2 As DataTable
                    'Dim dbcommand2 As New MySqlCommand("SELECT SUM(CASE WHEN subject_group = 'English' THEN subject_units ELSE 0 END) as 'English', SUM(CASE WHEN subject_group = 'Filipino' THEN subject_units ELSE 0 END) as 'Filipino', SUM(CASE WHEN subject_group = 'Math' THEN subject_units ELSE 0 END) as 'Mathematics', SUM(CASE WHEN subject_group = 'Social Science' THEN subject_units ELSE 0 END) as 'Socsci', SUM(CASE WHEN subject_group = 'Elective Major' THEN subject_units ELSE 0 END) as 'Major', SUM(CASE WHEN subject_group = 'Elective Minor' THEN subject_units ELSE 0 END) as 'Minor', SUM(CASE WHEN subject_group = 'Health Science' THEN subject_units ELSE 0 END) as 'HealthScience', SUM(CASE WHEN subject_group = 'Physical Education' THEN subject_units ELSE 0 END) as 'PE', SUM(CASE WHEN subject_group = 'CWTS CMT' THEN subject_units ELSE 0 END) as 'CWTS CMT', SUM(subject_units) as 'TOTAL', SUM(CASE WHEN subject_group = 'English' THEN sg_credits ELSE 0 END) as 'English', SUM(CASE WHEN subject_group = 'Filipino' THEN sg_credits ELSE 0 END) as 'Filipino', SUM(CASE WHEN subject_group = 'Math' THEN sg_credits ELSE 0 END) as 'Mathematics', SUM(CASE WHEN subject_group = 'Social Science' THEN sg_credits ELSE 0 END) as 'Socsci', SUM(CASE WHEN subject_group = 'Elective Major' THEN sg_credits ELSE 0 END) as 'Major', SUM(CASE WHEN subject_group = 'Elective Minor' THEN sg_credits ELSE 0 END) as 'Minor', SUM(CASE WHEN subject_group = 'Health Science' THEN sg_credits ELSE 0 END) as 'HealthScience', SUM(CASE WHEN subject_group = 'Physical Education' THEN sg_credits ELSE 0 END) as 'PE', SUM(CASE WHEN subject_group = 'CWTS CMT' THEN subject_units ELSE 0 END) as 'CWTS CMT', SUM(sg_credits) AS 'TOTAL2' from tbl_students_grades t1 INNER JOIN tbl_subject t2 ON t1.sg_subject_id = t2.subject_id and t1.sg_student_id = '" & studentId & "' and t1.sg_grade NOT IN ('') and sg_grade_visibility NOT IN (1) and sg_grade_status NOT IN ('Pending')", cn)
                    'Dim adt2 As New MySqlDataAdapter
                    'adt2.SelectCommand = dbcommand2
                    'dtable2 = New DataTable
                    'adt2.Fill(dtable2)
                    'dg_report2.DataSource = dtable2
                    'adt2.Dispose()
                    'dbcommand2.Dispose()
                    'cn.Close()

                    'rptdoc.SetParameterValue("total_cred_eng", dg_report.Rows(0).Cells(0).Value)
                    'rptdoc.SetParameterValue("total_cred_fil", dg_report.Rows(0).Cells(1).Value)
                    'rptdoc.SetParameterValue("total_cred_math", dg_report.Rows(0).Cells(2).Value)
                    'rptdoc.SetParameterValue("total_cred_socsci", dg_report.Rows(0).Cells(3).Value)
                    'rptdoc.SetParameterValue("total_cred_major", dg_report.Rows(0).Cells(4).Value)
                    'rptdoc.SetParameterValue("total_cred_minor", dg_report.Rows(0).Cells(5).Value)
                    'rptdoc.SetParameterValue("total_cred_health", dg_report.Rows(0).Cells(6).Value)
                    'rptdoc.SetParameterValue("total_cred_pe", dg_report.Rows(0).Cells(7).Value)
                    'rptdoc.SetParameterValue("total_cred_cwts", dg_report.Rows(0).Cells(8).Value)
                    'rptdoc.SetParameterValue("total_cred_total", dg_report.Rows(0).Cells(9).Value)

                    ReportViewer.ReportSource = rptdoc
                    dg_report.DataSource = Nothing
                    dg_report2.DataSource = Nothing
                    ReportViewer.Select()
                    ReportGenerated = True
                Catch ex As Exception
                    MsgBox("Student '" & txtStudent.Text & "' has no TOR yet to be generated.", vbCritical)
                    cn.Close()
                    PrevBtn()
                End Try
            End If
        ElseIf frmMain.formTitle.Text = "Generate Enrollment List" Or frmMain.formTitle.Text = "Generate Promotional List" Then

            If CheckBox1.Checked = True Then
                NextBtn()
                cn.Close()
                cn.Open()
                Dim dtable As DataTable
                Dim adt As New MySqlDataAdapter
                Dim dbcommand As New MySqlCommand("Select (course_code) As 'Course Code', (course_name) As 'Course Description', (course_levels) as 'Course Levels', COALESCE(FirstYearNewMale.FirstYear_New_Male,0) as FirstYear_New_Male, COALESCE(FirstYearNewFemale.FirstYear_New_Female,0) as FirstYear_New_Female,  COALESCE(SecondYearMale.SecondYear_Male,0) as SecondYear_Male, COALESCE(SecondYearFemale.SecondYear_Female,0) as SecondYear_Female, COALESCE(ThirdYearMale.ThirdYear_Male,0) as ThirdYear_Male, COALESCE(ThirdYearFemale.ThirdYear_Female,0) as ThirdYear_Female, COALESCE(FourthYearMale.FourthYear_Male,0) as FourthYear_Male, COALESCE(FourthYearFemale.FourthYear_Female,0) as FourthYear_Female, COALESCE(TotalCount.Total_Count,0) as TotalCount from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FirstYear_New_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%1st Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY course_code) as FirstYearNewMale on tbl_students_grades.sg_course_id = FirstYearNewMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FirstYear_New_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%1st Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as FirstYearNewFemale on tbl_students_grades.sg_course_id = FirstYearNewFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as SecondYear_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%2nd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as SecondYearMale on tbl_students_grades.sg_course_id = SecondYearMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as SecondYear_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%2nd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as SecondYearFemale on tbl_students_grades.sg_course_id = SecondYearFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as ThirdYear_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%3rd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as ThirdYearMale on tbl_students_grades.sg_course_id = ThirdYearMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as ThirdYear_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%3rd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as ThirdYearFemale on tbl_students_grades.sg_course_id = ThirdYearFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FourthYear_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%4th Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as FourthYearMale on tbl_students_grades.sg_course_id = FourthYearMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FourthYear_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%4th Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as FourthYearFemale on tbl_students_grades.sg_course_id = FourthYearFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as Total_Count from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as TotalCount on tbl_students_grades.sg_course_id = TotalCount.ID where tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  sg_course_id order by course_code asc", cn)
                adt.SelectCommand = dbcommand
                dtable = New DataTable
                adt.Fill(dtable)
                dg_report.DataSource = dtable
                adt.Dispose()
                dbcommand.Dispose()
                cn.Close()
                dt.Columns.Clear()
                dt.Rows.Clear()
                With dt
                    .Columns.Add("pr_course_code")
                    .Columns.Add("pr_course_name")
                    .Columns.Add("pr_1m")
                End With
                For Each dr As DataGridViewRow In dg_report.Rows
                    dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value)
                Next
                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New PromoCoverPage
                rptdoc.SetDataSource(dt)

                cn.Close()
                Dim periodsemester As String = ""
                cn.Open()
                cm = New MySqlCommand("SELECT `period_semester` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                periodsemester = cm.ExecuteScalar
                cn.Close()
                Dim semyear As String
                semyear = periodsemester.Substring(0, 3)
                If semyear = "1st" Then
                    rptdoc.SetParameterValue("report_semester", "First Semester")
                ElseIf semyear = "2nd" Then
                    rptdoc.SetParameterValue("report_semester", "Second Semester")
                Else
                    rptdoc.SetParameterValue("report_semester", "Summer")
                End If

                rptdoc.SetParameterValue("division_title", "COLLEGE")

                cn.Close()
                Dim periodname As String = ""
                cn.Open()
                cm = New MySqlCommand("SELECT `period_name` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                periodname = cm.ExecuteScalar
                cn.Close()

                rptdoc.SetParameterValue("report_acadyear", periodname)

                If frmMain.formTitle.Text = "Generate Enrollment List" Then
                    rptdoc.SetParameterValue("report_title", "ENROLLMENT")
                ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                    rptdoc.SetParameterValue("report_title", "PROMOTIONAL")
                End If

                ReportViewer.ReportSource = rptdoc
                dg_report.DataSource = Nothing
                ReportViewer.Select()
                ReportGenerated = True
            ElseIf CheckBox2.Checked = True Then
                NextBtn()
                Dim dtable As DataTable
                Dim adt As New MySqlDataAdapter
                Dim dbcommand As New MySqlCommand("Select (course_code) As 'Course Code', (course_name) As 'Course Description', (course_levels) as 'Course Levels', COALESCE(FirstYearNewMale.FirstYear_New_Male,0) as FirstYear_New_Male, COALESCE(FirstYearNewFemale.FirstYear_New_Female,0) as FirstYear_New_Female,  COALESCE(SecondYearMale.SecondYear_Male,0) as SecondYear_Male, COALESCE(SecondYearFemale.SecondYear_Female,0) as SecondYear_Female, COALESCE(ThirdYearMale.ThirdYear_Male,0) as ThirdYear_Male, COALESCE(ThirdYearFemale.ThirdYear_Female,0) as ThirdYear_Female, COALESCE(FourthYearMale.FourthYear_Male,0) as FourthYear_Male, COALESCE(FourthYearFemale.FourthYear_Female,0) as FourthYear_Female, COALESCE(TotalCount.Total_Count,0) as TotalCount from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FirstYear_New_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%1st Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY course_code) as FirstYearNewMale on tbl_students_grades.sg_course_id = FirstYearNewMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FirstYear_New_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%1st Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as FirstYearNewFemale on tbl_students_grades.sg_course_id = FirstYearNewFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as SecondYear_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%2nd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as SecondYearMale on tbl_students_grades.sg_course_id = SecondYearMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as SecondYear_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%2nd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as SecondYearFemale on tbl_students_grades.sg_course_id = SecondYearFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as ThirdYear_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%3rd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as ThirdYearMale on tbl_students_grades.sg_course_id = ThirdYearMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as ThirdYear_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%3rd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as ThirdYearFemale on tbl_students_grades.sg_course_id = ThirdYearFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FourthYear_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%4th Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as FourthYearMale on tbl_students_grades.sg_course_id = FourthYearMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FourthYear_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%4th Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as FourthYearFemale on tbl_students_grades.sg_course_id = FourthYearFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as Total_Count from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as TotalCount on tbl_students_grades.sg_course_id = TotalCount.ID where tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  sg_course_id order by course_code asc", cn)
                adt.SelectCommand = dbcommand
                dtable = New DataTable
                adt.Fill(dtable)
                dg_report.DataSource = dtable
                adt.Dispose()
                dbcommand.Dispose()

                dt.Columns.Clear()
                dt.Rows.Clear()
                With dt
                    .Columns.Add("pr_course_code")
                    .Columns.Add("pr_course_name")
                    .Columns.Add("pr_1m")
                End With

                dt2.Columns.Clear()
                dt2.Rows.Clear()
                With dt2
                    .Columns.Add("pr_course_code")
                    .Columns.Add("pr_course_name")
                    .Columns.Add("pr_1m")
                End With

                For Each dr As DataGridViewRow In dg_report.Rows
                    dt2.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value)
                Next

                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New PromoFirstPageMain
                rptdoc.SetDataSource(dt)
                rptdoc.Subreports(0).SetDataSource(dt2)
                Dim iDate As String = DateToday
                Dim oDate As DateTime = Convert.ToDateTime(iDate)

                cn.Close()
                Dim periodsemester As String = ""
                cn.Open()
                cm = New MySqlCommand("SELECT `period_semester` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                periodsemester = cm.ExecuteScalar
                cn.Close()
                Dim semyear As String
                semyear = periodsemester.Substring(0, 3)
                If semyear = "1st" Then
                    rptdoc.SetParameterValue("report_semester", "First Semester")
                ElseIf semyear = "2nd" Then
                    rptdoc.SetParameterValue("report_semester", "Second Semester")
                Else
                    rptdoc.SetParameterValue("report_semester", "Summer")
                End If

                rptdoc.SetParameterValue("report_date", oDate.ToString("MMMM' 'dd', 'yyyy"))

                cn.Close()
                Dim periodname As String = ""
                cn.Open()
                cm = New MySqlCommand("SELECT `period_name` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                periodname = cm.ExecuteScalar
                cn.Close()
                rptdoc.SetParameterValue("report_acadyear", periodname)
                rptdoc.SetParameterValue("report_registrar", cbRegistrar.Text)
                rptdoc.SetParameterValue("report_president", cbPresident.Text)

                If frmMain.formTitle.Text = "Generate Enrollment List" Then
                    rptdoc.SetParameterValue("report_title", "ENROLLMENT")
                    rptdoc.SetParameterValue("p_e", "Enrollment")
                ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                    rptdoc.SetParameterValue("report_title", "PROMOTIONAL")
                    rptdoc.SetParameterValue("p_e", "Promotional")
                End If

                ReportViewer.ReportSource = rptdoc
                dg_report.DataSource = Nothing
                ReportViewer.Select()
                ReportGenerated = True
            ElseIf CheckBox3.Checked = True Then
                NextBtn()
                Dim dtable As DataTable
                Dim adt As New MySqlDataAdapter
                Dim dbcommand As New MySqlCommand("Select (course_code) As 'Course Code', (course_name) As 'Course Description', COALESCE(FirstYearNewMale.FirstYear_New_Male,0) as FirstYear_New_Male, COALESCE(FirstYearNewFemale.FirstYear_New_Female,0) as FirstYear_New_Female,  COALESCE(SecondYearMale.SecondYear_Male,0) as SecondYear_Male, COALESCE(SecondYearFemale.SecondYear_Female,0) as SecondYear_Female, COALESCE(ThirdYearMale.ThirdYear_Male,0) as ThirdYear_Male, COALESCE(ThirdYearFemale.ThirdYear_Female,0) as ThirdYear_Female, COALESCE(FourthYearMale.FourthYear_Male,0) as FourthYear_Male, COALESCE(FourthYearFemale.FourthYear_Female,0) as FourthYear_Female, COALESCE(TotalCount.Total_Count,0) as TotalCount from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FirstYear_New_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%1st Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY course_code) as FirstYearNewMale on tbl_students_grades.sg_course_id = FirstYearNewMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FirstYear_New_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%1st Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as FirstYearNewFemale on tbl_students_grades.sg_course_id = FirstYearNewFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as SecondYear_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%2nd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as SecondYearMale on tbl_students_grades.sg_course_id = SecondYearMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as SecondYear_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%2nd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as SecondYearFemale on tbl_students_grades.sg_course_id = SecondYearFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as ThirdYear_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%3rd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as ThirdYearMale on tbl_students_grades.sg_course_id = ThirdYearMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as ThirdYear_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%3rd Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as ThirdYearFemale on tbl_students_grades.sg_course_id = ThirdYearFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FourthYear_Male from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Male' and sg_yearlevel like '%4th Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as FourthYearMale on tbl_students_grades.sg_course_id = FourthYearMale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as FourthYear_Female from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where s_gender = 'Female' and sg_yearlevel like '%4th Year%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as FourthYearFemale on tbl_students_grades.sg_course_id = FourthYearFemale.ID LEFT JOIN (SELECT (sg_course_id) AS 'ID', count(DISTINCT sg_student_id) as Total_Count from tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id LEFT JOIN period on tbl_students_grades.sg_period_id = period.period_id where tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  course_code) as TotalCount on tbl_students_grades.sg_course_id = TotalCount.ID where tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' GROUP BY  sg_course_id order by course_code asc", cn)
                adt.SelectCommand = dbcommand
                dtable = New DataTable
                adt.Fill(dtable)
                dg_report.DataSource = dtable
                adt.Dispose()
                dbcommand.Dispose()

                dt.Columns.Clear()
                dt.Rows.Clear()
                With dt

                    .Columns.Add("pr_course_name")
                    .Columns.Add("pr_1m")
                    .Columns.Add("pr_1f")
                    .Columns.Add("pr_2m")
                    .Columns.Add("pr_2f")
                    .Columns.Add("pr_3m")
                    .Columns.Add("pr_3f")
                    .Columns.Add("pr_4m")
                    .Columns.Add("pr_4f")
                    .Columns.Add("pr_total")
                End With

                dt2.Columns.Clear()
                dt2.Rows.Clear()
                With dt2

                    .Columns.Add("pr_course_name")
                    .Columns.Add("pr_1m")
                    .Columns.Add("pr_1f")
                    .Columns.Add("pr_2m")
                    .Columns.Add("pr_2f")
                    .Columns.Add("pr_3m")
                    .Columns.Add("pr_3f")
                    .Columns.Add("pr_4m")
                    .Columns.Add("pr_4f")
                    .Columns.Add("pr_total")
                End With

                For Each dr As DataGridViewRow In dg_report.Rows
                    dt2.Rows.Add(dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value)
                Next
                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New PromoSecondPageMain
                rptdoc.SetDataSource(dt)
                rptdoc.Subreports(0).SetDataSource(dt2)

                cn.Close()
                Dim periodsemester As String = ""
                cn.Open()
                cm = New MySqlCommand("SELECT `period_semester` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                periodsemester = cm.ExecuteScalar
                cn.Close()
                Dim semyear As String
                semyear = periodsemester.Substring(0, 3)
                If semyear = "1st" Then
                    rptdoc.SetParameterValue("report_semester", "First Semester")
                ElseIf semyear = "2nd" Then
                    rptdoc.SetParameterValue("report_semester", "Second Semester")
                Else
                    rptdoc.SetParameterValue("report_semester", "Summer")
                End If
                rptdoc.SetParameterValue("division_title", "COLLEGE")
                cn.Close()
                Dim periodname As String = ""
                cn.Open()
                cm = New MySqlCommand("SELECT `period_name` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                periodname = cm.ExecuteScalar
                cn.Close()
                rptdoc.SetParameterValue("report_acadyear", periodname)
                rptdoc.SetParameterValue("report_registrar", cbRegistrar.Text)

                If frmMain.formTitle.Text = "Generate Enrollment List" Then
                    rptdoc.SetParameterValue("report_title", "ENROLLMENT")
                ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                    rptdoc.SetParameterValue("report_title", "PROMOTIONAL")
                End If

                ReportViewer.ReportSource = rptdoc
                dg_report.DataSource = Nothing
                ReportViewer.Select()
                ReportGenerated = True
            ElseIf CheckBox4.Checked = True Then
                If CInt(cbAcademicYear.SelectedValue) <= 0 Then
                    ReportViewer.ReportSource = Nothing
                    MsgBox("Please select Academic Year.", vbCritical)
                    cbAcademicYear.Select()
                Else
                    ReportViewer.ReportSource = Nothing
                    dg_report.DataSource = Nothing
                    dg_report.Columns.Clear()
                    Dim dtable As DataTable
                    Dim adt As New MySqlDataAdapter
                    Dim sql As String = ""
                    If frmMain.formTitle.Text = "Generate Enrollment List" Then
                        'sql = "SELECT ID, LastName, FirstName, MiddleName, Extension, Sex, Course, CourseMajor, YearLevel, Birthdate,MAX(IF(SubjectNumber = 1, Subject, '')) as Subject1,MAX(IF(SubjectNumber = 1, Units, '')) as Units1,MAX(IF(SubjectNumber = 2, Subject, '')) as Subject2,MAX(IF(SubjectNumber = 2, Units, '')) as Units2,MAX(IF(SubjectNumber = 3, Subject, '')) as Subject3,MAX(IF(SubjectNumber = 3, Units, '')) as Units3,MAX(IF(SubjectNumber = 4, Subject, '')) as Subject4,MAX(IF(SubjectNumber = 4, Units, '')) as Units4,MAX(IF(SubjectNumber = 5, Subject, '')) as Subject5,MAX(IF(SubjectNumber = 5, Units, '')) as Units5,MAX(IF(SubjectNumber = 6, Subject, '')) as Subject6,MAX(IF(SubjectNumber = 6, Units, '')) as Units6,MAX(IF(SubjectNumber = 7, Subject, '')) as Subject7,MAX(IF(SubjectNumber = 7, Units, '')) as Units7,MAX(IF(SubjectNumber = 8, Subject, '')) as Subject8,MAX(IF(SubjectNumber = 8, Units, '')) as Units8,MAX(IF(SubjectNumber = 9, Subject, '')) as Subject9,MAX(IF(SubjectNumber = 9, Units, '')) as Units9,MAX(IF(SubjectNumber = 10, Subject, '')) as Subject10,MAX(IF(SubjectNumber = 10, Units, '')) as Units10,SUM(Units) as TotalUnits from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', s_ln as 'LastName', s_fn as 'FirstName', s_mn as 'MiddleName', (s_ext) as 'Extension',(s_gender) AS 'Sex',(course_name) AS 'Course',(course_major) AS 'CourseMajor', LEFT(sg_yearlevel, 1) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(subject_units) AS 'Units',Format(sg_grade, 1) AS 'Grade', (sg_grade_status) AS 'GradeStatus' FROM tbl_students_grades LEFT JOIN  tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' group by ID ORDER BY `promolist2`.`YearLevel` ASC, LastName ASC"
                        sql = "SELECT ID, LastName, FirstName, MiddleName, Extension, Sex, Course, CourseMajor, YearLevel, Birthdate, MAX(IF(SubjectNumber = 1, Units, '')) as Units1, MAX(IF(SubjectNumber = 1, Subject, '')) as Subject1, MAX(IF(SubjectNumber = 2, Units, '')) as Units2, MAX(IF(SubjectNumber = 2, Subject, '')) as Subject2, MAX(IF(SubjectNumber = 3, Units, '')) as Units3, MAX(IF(SubjectNumber = 3, Subject, '')) as Subject3, MAX(IF(SubjectNumber = 4, Units, '')) as Units4, MAX(IF(SubjectNumber = 4, Subject, '')) as Subject4, MAX(IF(SubjectNumber = 5, Units, '')) as Units5, MAX(IF(SubjectNumber = 5, Subject, '')) as Subject5, MAX(IF(SubjectNumber = 6, Units, '')) as Units6, MAX(IF(SubjectNumber = 6, Subject, '')) as Subject6, MAX(IF(SubjectNumber = 7, Units, '')) as Units7, MAX(IF(SubjectNumber = 7, Subject, '')) as Subject7, MAX(IF(SubjectNumber = 8, Units, '')) as Units8, MAX(IF(SubjectNumber = 8, Subject, '')) as Subject8, MAX(IF(SubjectNumber = 9, Units, '')) as Units9, MAX(IF(SubjectNumber = 9, Subject, '')) as Subject9, MAX(IF(SubjectNumber = 10, Units, '')) as Units10, MAX(IF(SubjectNumber = 10, Subject, '')) as Subject10, SUM(Units) as TotalUnits FROM (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', s_ln as 'LastName', s_fn as 'FirstName', s_mn as 'MiddleName', (s_ext) as 'Extension',(s_gender) AS 'Sex',(course_name) AS 'Course',(course_major) AS 'CourseMajor', LEFT(sg_yearlevel, 1) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(subject_units) AS 'Units',Format(sg_grade, 1) AS 'Grade', (sg_grade_status) AS 'GradeStatus' FROM tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id LEFT JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id WHERE sg_grade_status = 'Enrolled' ORDER BY subject_code) as promolist2 WHERE AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' GROUP BY ID ORDER BY `promolist2`.`YearLevel` ASC, LastName ASC"
                    ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                        'sql = "SELECT ID, LastName, FirstName, MiddleName, Extension, Sex, Course, CourseMajor, YearLevel, Birthdate, MAX(IF(SubjectNumber = 1, Subject, '')) as Subject1,MAX(IF(SubjectNumber = 1, Grade, '')) as Grade1,MAX(IF(SubjectNumber = 1, Units, '')) as Units1,MAX(IF(SubjectNumber = 2, Subject, '')) as Subject2,MAX(IF(SubjectNumber = 2, Grade, '')) as Grade2,MAX(IF(SubjectNumber = 2, Units, '')) as Units2,MAX(IF(SubjectNumber = 3, Subject, '')) as Subject3,MAX(IF(SubjectNumber = 3, Grade, '')) as Grade3,MAX(IF(SubjectNumber = 3, Units, '')) as Units3,MAX(IF(SubjectNumber = 4, Subject, '')) as Subject4,MAX(IF(SubjectNumber = 4, Grade, '')) as Grade4,MAX(IF(SubjectNumber = 4, Units, '')) as Units4,MAX(IF(SubjectNumber = 5, Subject, '')) as Subject5,MAX(IF(SubjectNumber = 5, Grade, '')) as Grade5,MAX(IF(SubjectNumber = 5, Units, '')) as Units5,MAX(IF(SubjectNumber = 6, Subject, '')) as Subject6,MAX(IF(SubjectNumber = 6, Grade, '')) as Grade6,MAX(IF(SubjectNumber = 6, Units, '')) as Units6,MAX(IF(SubjectNumber = 7, Subject, '')) as Subject7,MAX(IF(SubjectNumber = 7, Grade, '')) as Grade7,MAX(IF(SubjectNumber = 7, Units, '')) as Units7,MAX(IF(SubjectNumber = 8, Subject, '')) as Subject8,MAX(IF(SubjectNumber = 8, Grade, '')) as Grade8,MAX(IF(SubjectNumber = 8, Units, '')) as Units8,MAX(IF(SubjectNumber = 9, Subject, '')) as Subject9,MAX(IF(SubjectNumber = 9, Grade, '')) as Grade9,MAX(IF(SubjectNumber = 9, Units, '')) as Units9,MAX(IF(SubjectNumber = 10, Subject, '')) as Subject10,MAX(IF(SubjectNumber = 10, Grade, '')) as Grade10, MAX(IF(SubjectNumber = 10, Units, '')) as Units10,SUM(Units) as TotalUnits from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', s_ln as 'LastName', s_fn as 'FirstName', s_mn as 'MiddleName', (s_ext) as 'Extension',(s_gender) AS 'Sex',(course_name) AS 'Course',(course_major) AS 'CourseMajor', LEFT(sg_yearlevel, 1) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(subject_units) AS 'Units',Format(sg_grade, 1) AS 'Grade', (sg_grade_status) AS 'GradeStatus'FROM tbl_students_grades LEFT JOIN  tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' group by ID ORDER BY `promolist2`.`YearLevel` ASC, LastName ASC"
                        'sql = "SELECT ID, LastName, FirstName, MiddleName, Extension, Sex, Course, CourseMajor, YearLevel, Birthdate, MAX(IF(SubjectNumber = 1, Subject, '')) as Subject1, MAX(IF(SubjectNumber = 1, Units, '')) as Units1, MAX(IF(SubjectNumber = 1, Grade, '')) as Grade1, MAX(IF(SubjectNumber = 2, Subject, '')) as Subject2, MAX(IF(SubjectNumber = 2, Units, '')) as Units2, MAX(IF(SubjectNumber = 2, Grade, '')) as Grade2, MAX(IF(SubjectNumber = 3, Subject, '')) as Subject3, MAX(IF(SubjectNumber = 3, Units, '')) as Units3, MAX(IF(SubjectNumber = 3, Grade, '')) as Grade3, MAX(IF(SubjectNumber = 4, Subject, '')) as Subject4, MAX(IF(SubjectNumber = 4, Units, '')) as Units4, MAX(IF(SubjectNumber = 4, Grade, '')) as Grade4, MAX(IF(SubjectNumber = 5, Subject, '')) as Subject5, MAX(IF(SubjectNumber = 5, Units, '')) as Units5, MAX(IF(SubjectNumber = 5, Grade, '')) as Grade5, MAX(IF(SubjectNumber = 6, Subject, '')) as Subject6, MAX(IF(SubjectNumber = 6, Units, '')) as Units6, MAX(IF(SubjectNumber = 6, Grade, '')) as Grade6, MAX(IF(SubjectNumber = 7, Subject, '')) as Subject7, MAX(IF(SubjectNumber = 7, Units, '')) as Units7, MAX(IF(SubjectNumber = 7, Grade, '')) as Grade7, MAX(IF(SubjectNumber = 8, Subject, '')) as Subject8, MAX(IF(SubjectNumber = 8, Units, '')) as Units8, MAX(IF(SubjectNumber = 8, Grade, '')) as Grade8, MAX(IF(SubjectNumber = 9, Subject, '')) as Subject9, MAX(IF(SubjectNumber = 9, Units, '')) as Units9, MAX(IF(SubjectNumber = 9, Grade, '')) as Grade9, MAX(IF(SubjectNumber = 10, Subject, '')) as Subject10, MAX(IF(SubjectNumber = 10, Units, '')) as Units10, MAX(IF(SubjectNumber = 10, Grade, '')) as Grade10, SUM(Units) as TotalUnits FROM (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', s_ln as 'LastName', s_fn as 'FirstName', s_mn as 'MiddleName', (s_ext) as 'Extension',(s_gender) AS 'Sex',(course_name) AS 'Course',(course_major) AS 'CourseMajor', LEFT(sg_yearlevel, 1) AS 'YearLevel', date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID', CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(sg_credits) AS 'Units', Format(sg_grade, 1) AS 'Grade', (sg_grade_status) AS 'GradeStatus' FROM tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id LEFT JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id WHERE sg_grade_status = 'Enrolled' ORDER BY subject_code) as promolist2 WHERE AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' GROUP BY ID ORDER BY `promolist2`.`YearLevel` ASC, LastName ASC"
                        sql = "Select ID, s_ln as 'Last Name', s_fn as 'First Name', s_mn as 'Middle Name', Extension, Sex, Course, course_major as Major, YearLevel,Birthdate,Subject1,Units1,Grade1,Subject2,Units2,Grade2,Subject3,Units3,Grade3,Subject4,Units4,Grade4,Subject5,Units5,Grade5,Subject6,Units6,Grade6,Subject7,Units7,Grade7,Subject8,Units8,Grade8,Subject9,Units9,Grade9,Subject10,Units10,Grade10,'' as 'Remarks',TotalUnits,TotalSubject, MAX(GradeStatus) AS GradeStatus from ((SELECT ID, Student, Sex, Course, LEFT(YearLevel,1) as YearLevel, Birthdate,MAX(IF(SubjectNumber = 1, Subject, NULL)) as Subject1,MAX(IF(SubjectNumber = 1, Grade, NULL)) as Grade1,MAX(IF(SubjectNumber = 1, Units, NULL)) as Units1,MAX(IF(SubjectNumber = 2, Subject, NULL)) as Subject2,MAX(IF(SubjectNumber = 2, Grade, NULL)) as Grade2,MAX(IF(SubjectNumber = 2, Units, NULL)) as Units2,MAX(IF(SubjectNumber = 3, Subject, NULL)) as Subject3,MAX(IF(SubjectNumber = 3, Grade, NULL)) as Grade3,MAX(IF(SubjectNumber = 3, Units, NULL)) as Units3,MAX(IF(SubjectNumber = 4, Subject, NULL)) as Subject4,MAX(IF(SubjectNumber = 4, Grade, NULL)) as Grade4,MAX(IF(SubjectNumber = 4, Units, NULL)) as Units4,MAX(IF(SubjectNumber = 5, Subject, NULL)) as Subject5,MAX(IF(SubjectNumber = 5, Grade, NULL)) as Grade5,MAX(IF(SubjectNumber = 5, Units, NULL)) as Units5,MAX(IF(SubjectNumber = 6, Subject, NULL)) as Subject6,MAX(IF(SubjectNumber = 6, Grade, NULL)) as Grade6,MAX(IF(SubjectNumber = 6, Units, NULL)) as Units6,MAX(IF(SubjectNumber = 7, Subject, NULL)) as Subject7,MAX(IF(SubjectNumber = 7, Grade, NULL)) as Grade7,MAX(IF(SubjectNumber = 7, Units, NULL)) as Units7,MAX(IF(SubjectNumber = 8, Subject, NULL)) as Subject8,MAX(IF(SubjectNumber = 8, Grade, NULL)) as Grade8,MAX(IF(SubjectNumber = 8, Units, NULL)) as Units8,MAX(IF(SubjectNumber = 9, Subject, NULL)) as Subject9,MAX(IF(SubjectNumber = 9, Grade, NULL)) as Grade9,MAX(IF(SubjectNumber = 9, Units, NULL)) as Units9,MAX(IF(SubjectNumber = 10, Subject, NULL)) as Subject10,MAX(IF(SubjectNumber = 10, Grade, NULL)) as Grade10,MAX(IF(SubjectNumber = 10, Units, NULL)) as Units10,COUNT(Subject) as TotalSubject,SUM(Units) as TotalUnits, sg_course_id, s_fn,s_mn,s_ln,course_major, GradeStatus, Extension from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', CONCAT(s_ln, ', ', s_fn, ' ', LEFT(s_mn, 1),'.') as 'Student',(s_gender) AS 'Sex',(course_name) AS 'Course',(sg_yearlevel) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(sg_credits) AS 'Units',IF(sg_grade > 0, ROUND(sg_grade,1),sg_grade) AS 'Grade', (sg_grade_status) AS 'GradeStatus', sg_course_id, s_fn,s_mn,s_ln,course_major, (s_ext) as 'Extension' FROM tbl_students_grades LEFT JOIN  tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' group by ID UNION SELECT ID, Student, Sex, Course, LEFT(YearLevel,1) as YearLevel, Birthdate,MAX(IF(SubjectNumber = 1, Subject, NULL)) as Subject1,MAX(IF(SubjectNumber = 1, 'W', NULL)) as Grade1,MAX(IF(SubjectNumber = 1, 0, NULL)) as Units1,MAX(IF(SubjectNumber = 2, Subject, NULL)) as Subject2,MAX(IF(SubjectNumber = 2, 'W', NULL)) as Grade2,MAX(IF(SubjectNumber = 2, 0, NULL)) as Units2,MAX(IF(SubjectNumber = 3, Subject, NULL)) as Subject3,MAX(IF(SubjectNumber = 3, 'W', NULL)) as Grade3,MAX(IF(SubjectNumber = 3, 0, NULL)) as Units3,MAX(IF(SubjectNumber = 4, Subject, NULL)) as Subject4,MAX(IF(SubjectNumber = 4, 'W', NULL)) as Grade4,MAX(IF(SubjectNumber = 4, 0, NULL)) as Units4,MAX(IF(SubjectNumber = 5, Subject, NULL)) as Subject5,MAX(IF(SubjectNumber = 5, 'W', NULL)) as Grade5,MAX(IF(SubjectNumber = 5, 0, NULL)) as Units5,MAX(IF(SubjectNumber = 6, Subject, NULL)) as Subject6,MAX(IF(SubjectNumber = 6, 'W', NULL)) as Grade6,MAX(IF(SubjectNumber = 6, 0, NULL)) as Units6,MAX(IF(SubjectNumber = 7, Subject, NULL)) as Subject7,MAX(IF(SubjectNumber = 7, 'W', NULL)) as Grade7,MAX(IF(SubjectNumber = 7, 0, NULL)) as Units7,MAX(IF(SubjectNumber = 8, Subject, NULL)) as Subject8,MAX(IF(SubjectNumber = 8, 'W', NULL)) as Grade8,MAX(IF(SubjectNumber = 8, 0, NULL)) as Units8,MAX(IF(SubjectNumber = 9, Subject, NULL)) as Subject9,MAX(IF(SubjectNumber = 9, 'W', NULL)) as Grade9,MAX(IF(SubjectNumber = 9, 0, NULL)) as Units9,MAX(IF(SubjectNumber = 10, Subject, NULL)) as Subject10,MAX(IF(SubjectNumber = 10, 'W', NULL)) as Grade10,MAX(IF(SubjectNumber = 10, 0, NULL)) as Units10,COUNT(Subject) as TotalSubject,SUM(Units) as TotalUnits, sg_course_id, s_fn,s_mn,s_ln,course_major, GradeStatus, Extension from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', CONCAT(s_ln, ', ', s_fn, ' ', LEFT(s_mn, 1),'.') as 'Student',(s_gender) AS 'Sex',(course_name) AS 'Course',(sg_yearlevel) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(sg_credits) AS 'Units',IF(sg_grade > 0, ROUND(sg_grade,1),sg_grade) AS 'Grade', (sg_grade_status) AS 'GradeStatus', sg_course_id, s_fn,s_mn,s_ln,course_major, (s_ext) as 'Extension' FROM tbl_withdraw_students_grades LEFT JOIN  tbl_student ON tbl_withdraw_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_withdraw_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_withdraw_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_withdraw_students_grades.sg_course_id = tbl_course.course_id where sg_grade_status = 'Withdrawn' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Withdrawn' group by ID) ORDER BY Student) as t1 GROUP BY ID ORDER BY Student"
                    End If
                    Dim dbcommand As New MySqlCommand(sql, cn)
                    adt.SelectCommand = dbcommand
                    dtable = New DataTable
                    adt.Fill(dtable)
                    dg_report.DataSource = dtable
                    adt.Dispose()
                    dbcommand.Dispose()
                    If frmMain.formTitle.Text = "Generate Enrollment List" Then
                        Dim sPath As String = IO.Path.Combine("C:\HEMIS - " & cbAcademicYear.Text & "")
                        If Not IO.Directory.Exists(sPath) Then
                            IO.Directory.CreateDirectory(sPath)
                            exportexcel()
                            MessageBox.Show("HEMIS - Enrollment List data successfully exported. Browse the exported file in '" & sPath & "'", "Data exporting successful.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            exportexcel()
                            MessageBox.Show("HEMIS - Enrollment List data successfully exported. Browse the exported file in '" & sPath & "'", "Data exporting successful.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                        Dim sPath As String = IO.Path.Combine("C:\HEMIS - " & cbAcademicYear.Text & "")
                        If Not IO.Directory.Exists(sPath) Then
                            IO.Directory.CreateDirectory(sPath)
                            exportexcel()
                            MessageBox.Show("HEMIS - Promotional List data successfully exported. Browse the exported file in '" & sPath & "'", "Data exporting successful.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            exportexcel()
                            MessageBox.Show("HEMIS - Promotional List data successfully exported. Browse the exported file in '" & sPath & "'", "Data exporting successful.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            Else
                If CInt(cbAcademicYear.SelectedValue) <= 0 Then
                    ReportViewer.ReportSource = Nothing
                    MsgBox("Please select Academic Year.", vbCritical)
                    cbAcademicYear.Select()
                Else
                    NextBtn()
                    If cbAllCourse.Checked = True Then
                        Dim dtable As DataTable
                        Dim adt As New MySqlDataAdapter
                        Dim sql As String = ""
                        If frmMain.formTitle.Text = "Generate Enrollment List" Then
                            If cbEPlistYearLevel.Text = "All" Then
                                sql = "SELECT ID, Student, Sex, Course, LEFT(YearLevel,1), Birthdate,MAX(IF(SubjectNumber = 1, Subject, NULL)) as Subject1,MAX(IF(SubjectNumber = 1, NULL, NULL)) as Grade1,MAX(IF(SubjectNumber = 1, Units, NULL)) as Units1,MAX(IF(SubjectNumber = 2, Subject, NULL)) as Subject2,MAX(IF(SubjectNumber = 2, NULL, NULL)) as Grade2,MAX(IF(SubjectNumber = 2, Units, NULL)) as Units2,MAX(IF(SubjectNumber = 3, Subject, NULL)) as Subject3,MAX(IF(SubjectNumber = 3, NULL, NULL)) as Grade3,MAX(IF(SubjectNumber = 3, Units, NULL)) as Units3,MAX(IF(SubjectNumber = 4, Subject, NULL)) as Subject4,MAX(IF(SubjectNumber = 4, NULL, NULL)) as Grade4,MAX(IF(SubjectNumber = 4, Units, NULL)) as Units4,MAX(IF(SubjectNumber = 5, Subject, NULL)) as Subject5,MAX(IF(SubjectNumber = 5, NULL, NULL)) as Grade5,MAX(IF(SubjectNumber = 5, Units, NULL)) as Units5,MAX(IF(SubjectNumber = 6, Subject, NULL)) as Subject6,MAX(IF(SubjectNumber = 6, NULL, NULL)) as Grade6,MAX(IF(SubjectNumber = 6, Units, NULL)) as Units6,MAX(IF(SubjectNumber = 7, Subject, NULL)) as Subject7,MAX(IF(SubjectNumber = 7, NULL, NULL)) as Grade7,MAX(IF(SubjectNumber = 7, Units, NULL)) as Units7,MAX(IF(SubjectNumber = 8, Subject, NULL)) as Subject8,MAX(IF(SubjectNumber = 8, NULL, NULL)) as Grade8,MAX(IF(SubjectNumber = 8, Units, NULL)) as Units8,MAX(IF(SubjectNumber = 9, Subject, NULL)) as Subject9,MAX(IF(SubjectNumber = 9, NULL, NULL)) as Grade9,MAX(IF(SubjectNumber = 9, Units, NULL)) as Units9,MAX(IF(SubjectNumber = 10, Subject, NULL)) as Subject10,MAX(IF(SubjectNumber = 10, NULL, NULL)) as Grade10,MAX(IF(SubjectNumber = 10, Units, NULL)) as Units10,COUNT(Subject) as TotalSubject,SUM(Units) as TotalUnits, sg_course_id, s_fn,s_mn,s_ln,course_major from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', CONCAT(s_ln, ', ', s_fn, ' ', LEFT(s_mn, 1),'.') as 'Student',(s_gender) AS 'Sex',(course_code) AS 'Course',(sg_yearlevel) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(subject_units) AS 'Units',Format(sg_grade, 1) AS 'Grade', (sg_grade_status) AS 'GradeStatus', sg_course_id, s_fn,s_mn,s_ln,course_major FROM tbl_students_grades LEFT JOIN  tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' group by ID order by Student"
                            Else
                                sql = "SELECT ID, Student, Sex, Course, LEFT(YearLevel,1), Birthdate,MAX(IF(SubjectNumber = 1, Subject, NULL)) as Subject1,MAX(IF(SubjectNumber = 1, NULL, NULL)) as Grade1,MAX(IF(SubjectNumber = 1, Units, NULL)) as Units1,MAX(IF(SubjectNumber = 2, Subject, NULL)) as Subject2,MAX(IF(SubjectNumber = 2, NULL, NULL)) as Grade2,MAX(IF(SubjectNumber = 2, Units, NULL)) as Units2,MAX(IF(SubjectNumber = 3, Subject, NULL)) as Subject3,MAX(IF(SubjectNumber = 3, NULL, NULL)) as Grade3,MAX(IF(SubjectNumber = 3, Units, NULL)) as Units3,MAX(IF(SubjectNumber = 4, Subject, NULL)) as Subject4,MAX(IF(SubjectNumber = 4, NULL, NULL)) as Grade4,MAX(IF(SubjectNumber = 4, Units, NULL)) as Units4,MAX(IF(SubjectNumber = 5, Subject, NULL)) as Subject5,MAX(IF(SubjectNumber = 5, NULL, NULL)) as Grade5,MAX(IF(SubjectNumber = 5, Units, NULL)) as Units5,MAX(IF(SubjectNumber = 6, Subject, NULL)) as Subject6,MAX(IF(SubjectNumber = 6, NULL, NULL)) as Grade6,MAX(IF(SubjectNumber = 6, Units, NULL)) as Units6,MAX(IF(SubjectNumber = 7, Subject, NULL)) as Subject7,MAX(IF(SubjectNumber = 7, NULL, NULL)) as Grade7,MAX(IF(SubjectNumber = 7, Units, NULL)) as Units7,MAX(IF(SubjectNumber = 8, Subject, NULL)) as Subject8,MAX(IF(SubjectNumber = 8, NULL, NULL)) as Grade8,MAX(IF(SubjectNumber = 8, Units, NULL)) as Units8,MAX(IF(SubjectNumber = 9, Subject, NULL)) as Subject9,MAX(IF(SubjectNumber = 9, NULL, NULL)) as Grade9,MAX(IF(SubjectNumber = 9, Units, NULL)) as Units9,MAX(IF(SubjectNumber = 10, Subject, NULL)) as Subject10,MAX(IF(SubjectNumber = 10, NULL, NULL)) as Grade10,MAX(IF(SubjectNumber = 10, Units, NULL)) as Units10,COUNT(Subject) as TotalSubject,SUM(Units) as TotalUnits, sg_course_id, s_fn,s_mn,s_ln,course_major from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', CONCAT(s_ln, ', ', s_fn, ' ', LEFT(s_mn, 1),'.') as 'Student',(s_gender) AS 'Sex',(course_code) AS 'Course',(sg_yearlevel) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(subject_units) AS 'Units',Format(sg_grade, 1) AS 'Grade', (sg_grade_status) AS 'GradeStatus', sg_course_id, s_fn,s_mn,s_ln,course_major FROM tbl_students_grades LEFT JOIN  tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' and YearLevel like '%" & cbEPlistYearLevel.Text & "%' group by ID order by Student"
                            End If
                        ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                            If cbEPlistYearLevel.Text = "All" Then
                                sql = "SELECT ID, Student, Sex, Course, LEFT(YearLevel,1), Birthdate,MAX(IF(SubjectNumber = 1, Subject, NULL)) as Subject1,MAX(IF(SubjectNumber = 1, Grade, NULL)) as Grade1,MAX(IF(SubjectNumber = 1, Units, NULL)) as Units1,MAX(IF(SubjectNumber = 2, Subject, NULL)) as Subject2,MAX(IF(SubjectNumber = 2, Grade, NULL)) as Grade2,MAX(IF(SubjectNumber = 2, Units, NULL)) as Units2,MAX(IF(SubjectNumber = 3, Subject, NULL)) as Subject3,MAX(IF(SubjectNumber = 3, Grade, NULL)) as Grade3,MAX(IF(SubjectNumber = 3, Units, NULL)) as Units3,MAX(IF(SubjectNumber = 4, Subject, NULL)) as Subject4,MAX(IF(SubjectNumber = 4, Grade, NULL)) as Grade4,MAX(IF(SubjectNumber = 4, Units, NULL)) as Units4,MAX(IF(SubjectNumber = 5, Subject, NULL)) as Subject5,MAX(IF(SubjectNumber = 5, Grade, NULL)) as Grade5,MAX(IF(SubjectNumber = 5, Units, NULL)) as Units5,MAX(IF(SubjectNumber = 6, Subject, NULL)) as Subject6,MAX(IF(SubjectNumber = 6, Grade, NULL)) as Grade6,MAX(IF(SubjectNumber = 6, Units, NULL)) as Units6,MAX(IF(SubjectNumber = 7, Subject, NULL)) as Subject7,MAX(IF(SubjectNumber = 7, Grade, NULL)) as Grade7,MAX(IF(SubjectNumber = 7, Units, NULL)) as Units7,MAX(IF(SubjectNumber = 8, Subject, NULL)) as Subject8,MAX(IF(SubjectNumber = 8, Grade, NULL)) as Grade8,MAX(IF(SubjectNumber = 8, Units, NULL)) as Units8,MAX(IF(SubjectNumber = 9, Subject, NULL)) as Subject9,MAX(IF(SubjectNumber = 9, Grade, NULL)) as Grade9,MAX(IF(SubjectNumber = 9, Units, NULL)) as Units9,MAX(IF(SubjectNumber = 10, Subject, NULL)) as Subject10,MAX(IF(SubjectNumber = 10, Grade, NULL)) as Grade10,MAX(IF(SubjectNumber = 10, Units, NULL)) as Units10,COUNT(Subject) as TotalSubject,SUM(Units) as TotalUnits, sg_course_id, s_fn,s_mn,s_ln,course_major from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', CONCAT(s_ln, ', ', s_fn, ' ', LEFT(s_mn, 1),'.') as 'Student',(s_gender) AS 'Sex',(course_code) AS 'Course',(sg_yearlevel) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(sg_credits) AS 'Units',IF(sg_grade > 0, ROUND(sg_grade,1),sg_grade) as 'Grade', (sg_grade_status) AS 'GradeStatus', sg_course_id, s_fn,s_mn,s_ln,course_major FROM tbl_students_grades LEFT JOIN  tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' group by ID order by Student"
                            Else
                                sql = "SELECT ID, Student, Sex, Course, LEFT(YearLevel,1), Birthdate,MAX(IF(SubjectNumber = 1, Subject, NULL)) as Subject1,MAX(IF(SubjectNumber = 1, Grade, NULL)) as Grade1,MAX(IF(SubjectNumber = 1, Units, NULL)) as Units1,MAX(IF(SubjectNumber = 2, Subject, NULL)) as Subject2,MAX(IF(SubjectNumber = 2, Grade, NULL)) as Grade2,MAX(IF(SubjectNumber = 2, Units, NULL)) as Units2,MAX(IF(SubjectNumber = 3, Subject, NULL)) as Subject3,MAX(IF(SubjectNumber = 3, Grade, NULL)) as Grade3,MAX(IF(SubjectNumber = 3, Units, NULL)) as Units3,MAX(IF(SubjectNumber = 4, Subject, NULL)) as Subject4,MAX(IF(SubjectNumber = 4, Grade, NULL)) as Grade4,MAX(IF(SubjectNumber = 4, Units, NULL)) as Units4,MAX(IF(SubjectNumber = 5, Subject, NULL)) as Subject5,MAX(IF(SubjectNumber = 5, Grade, NULL)) as Grade5,MAX(IF(SubjectNumber = 5, Units, NULL)) as Units5,MAX(IF(SubjectNumber = 6, Subject, NULL)) as Subject6,MAX(IF(SubjectNumber = 6, Grade, NULL)) as Grade6,MAX(IF(SubjectNumber = 6, Units, NULL)) as Units6,MAX(IF(SubjectNumber = 7, Subject, NULL)) as Subject7,MAX(IF(SubjectNumber = 7, Grade, NULL)) as Grade7,MAX(IF(SubjectNumber = 7, Units, NULL)) as Units7,MAX(IF(SubjectNumber = 8, Subject, NULL)) as Subject8,MAX(IF(SubjectNumber = 8, Grade, NULL)) as Grade8,MAX(IF(SubjectNumber = 8, Units, NULL)) as Units8,MAX(IF(SubjectNumber = 9, Subject, NULL)) as Subject9,MAX(IF(SubjectNumber = 9, Grade, NULL)) as Grade9,MAX(IF(SubjectNumber = 9, Units, NULL)) as Units9,MAX(IF(SubjectNumber = 10, Subject, NULL)) as Subject10,MAX(IF(SubjectNumber = 10, Grade, NULL)) as Grade10,MAX(IF(SubjectNumber = 10, Units, NULL)) as Units10,COUNT(Subject) as TotalSubject,SUM(Units) as TotalUnits, sg_course_id, s_fn,s_mn,s_ln,course_major from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', CONCAT(s_ln, ', ', s_fn, ' ', LEFT(s_mn, 1),'.') as 'Student',(s_gender) AS 'Sex',(course_code) AS 'Course',(sg_yearlevel) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(sg_credits) AS 'Units',IF(sg_grade > 0, ROUND(sg_grade,1),sg_grade) as 'Grade', (sg_grade_status) AS 'GradeStatus', sg_course_id, s_fn,s_mn,s_ln,course_major FROM tbl_students_grades LEFT JOIN  tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' and YearLevel like '%" & cbEPlistYearLevel.Text & "%' group by ID order by Student"
                            End If
                        End If
                        Dim dbcommand As New MySqlCommand(sql, cn)
                        adt.SelectCommand = dbcommand
                        dtable = New DataTable
                        adt.Fill(dtable)
                        dg_report.DataSource = dtable
                        adt.Dispose()
                        dbcommand.Dispose()

                        dt.Columns.Clear()
                        dt.Rows.Clear()
                        With dt
                            .Columns.Add("ID")
                            .Columns.Add("Student")
                            .Columns.Add("Sex")
                            .Columns.Add("Course")
                            .Columns.Add("YearLevel")
                            .Columns.Add("Birthdate")
                            .Columns.Add("Subject1")
                            .Columns.Add("Grade1")
                            .Columns.Add("Units1")
                            .Columns.Add("Subject2")
                            .Columns.Add("Grade2")
                            .Columns.Add("Units2")
                            .Columns.Add("Subject3")
                            .Columns.Add("Grade3")
                            .Columns.Add("Units3")
                            .Columns.Add("Subject4")
                            .Columns.Add("Grade4")
                            .Columns.Add("Units4")
                            .Columns.Add("Subject5")
                            .Columns.Add("Grade5")
                            .Columns.Add("Units5")
                            .Columns.Add("Subject6")
                            .Columns.Add("Grade6")
                            .Columns.Add("Units6")
                            .Columns.Add("Subject7")
                            .Columns.Add("Grade7")
                            .Columns.Add("Units7")
                            .Columns.Add("Subject8")
                            .Columns.Add("Grade8")
                            .Columns.Add("Units8")
                            .Columns.Add("Subject9")
                            .Columns.Add("Grade9")
                            .Columns.Add("Units9")
                            .Columns.Add("Subject10")
                            .Columns.Add("Grade10")
                            .Columns.Add("Units10")
                            .Columns.Add("TotalSubject")
                            .Columns.Add("TotalUnits")
                            .Columns.Add("stud_first_name")
                            .Columns.Add("stud_middle_name")
                            .Columns.Add("stud_last_name")
                            .Columns.Add("course_major")
                        End With
                        For Each dr As DataGridViewRow In dg_report.Rows
                            dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value, dr.Cells(11).Value, dr.Cells(12).Value, dr.Cells(13).Value, dr.Cells(14).Value, dr.Cells(15).Value, dr.Cells(16).Value, dr.Cells(17).Value, dr.Cells(18).Value, dr.Cells(19).Value, dr.Cells(20).Value, dr.Cells(21).Value, dr.Cells(22).Value, dr.Cells(23).Value, dr.Cells(24).Value, dr.Cells(25).Value, dr.Cells(26).Value, dr.Cells(27).Value, dr.Cells(28).Value, dr.Cells(29).Value, dr.Cells(30).Value, dr.Cells(31).Value, dr.Cells(32).Value, dr.Cells(33).Value, dr.Cells(34).Value, dr.Cells(35).Value, dr.Cells(36).Value, dr.Cells(37).Value, dr.Cells(39).Value, dr.Cells(40).Value, dr.Cells(41).Value, dr.Cells(42).Value)
                        Next
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        'If frmMain.formTitle.Text = "Generate Enrollment List" Then
                        '    rptdoc = New Promotional
                        '    rptdoc.SetDataSource(dt)
                        '    rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                        '    rptdoc.SetParameterValue("promo_president_admin", cbPresident.Text)
                        '    rptdoc.SetParameterValue("promo_prepared_by", cbRegistrar.Text)
                        '    rptdoc.SetParameterValue("course_grade_strand", "Course")
                        '    rptdoc.SetParameterValue("year_grade", "Year Level")
                        '    ReportViewer.Select() ReportGenerated = True
                        'ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                        rptdoc = New Promotional
                        rptdoc.SetDataSource(dt)
                        rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                        rptdoc.SetParameterValue("promo_president_admin", cbPresident.Text)
                        rptdoc.SetParameterValue("promo_prepared_by", cbRegistrar.Text)
                        rptdoc.SetParameterValue("course_grade_strand", "Course")
                        rptdoc.SetParameterValue("year_grade", "Year Level")
                        If frmMain.formTitle.Text = "Generate Enrollment List" Then
                            rptdoc.SetParameterValue("reportName", "ENROLLMENT")
                        ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                            rptdoc.SetParameterValue("reportName", "PROMOTIONAL")
                        End If
                        ReportViewer.ReportSource = rptdoc
                        'End If
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                    Else
                        If txtCourse.Text = String.Empty Then
                            ReportViewer.ReportSource = Nothing
                            MsgBox("Please select Course.", vbCritical)
                            btnSearchCourse.Select()
                        Else
                            Dim dtable As DataTable
                            Dim adt As New MySqlDataAdapter
                            Dim sql As String = ""
                            If frmMain.formTitle.Text = "Generate Enrollment List" Then
                                If cbEPlistYearLevel.Text = "All" Then
                                    sql = "SELECT ID, Student, Sex, Course, LEFT(YearLevel,1), Birthdate,MAX(IF(SubjectNumber = 1, Subject, NULL)) as Subject1,MAX(IF(SubjectNumber = 1, NULL, NULL)) as Grade1,MAX(IF(SubjectNumber = 1, Units, NULL)) as Units1,MAX(IF(SubjectNumber = 2, Subject, NULL)) as Subject2,MAX(IF(SubjectNumber = 2, NULL, NULL)) as Grade2,MAX(IF(SubjectNumber = 2, Units, NULL)) as Units2,MAX(IF(SubjectNumber = 3, Subject, NULL)) as Subject3,MAX(IF(SubjectNumber = 3, NULL, NULL)) as Grade3,MAX(IF(SubjectNumber = 3, Units, NULL)) as Units3,MAX(IF(SubjectNumber = 4, Subject, NULL)) as Subject4,MAX(IF(SubjectNumber = 4, NULL, NULL)) as Grade4,MAX(IF(SubjectNumber = 4, Units, NULL)) as Units4,MAX(IF(SubjectNumber = 5, Subject, NULL)) as Subject5,MAX(IF(SubjectNumber = 5, NULL, NULL)) as Grade5,MAX(IF(SubjectNumber = 5, Units, NULL)) as Units5,MAX(IF(SubjectNumber = 6, Subject, NULL)) as Subject6,MAX(IF(SubjectNumber = 6, NULL, NULL)) as Grade6,MAX(IF(SubjectNumber = 6, Units, NULL)) as Units6,MAX(IF(SubjectNumber = 7, Subject, NULL)) as Subject7,MAX(IF(SubjectNumber = 7, NULL, NULL)) as Grade7,MAX(IF(SubjectNumber = 7, Units, NULL)) as Units7,MAX(IF(SubjectNumber = 8, Subject, NULL)) as Subject8,MAX(IF(SubjectNumber = 8, NULL, NULL)) as Grade8,MAX(IF(SubjectNumber = 8, Units, NULL)) as Units8,MAX(IF(SubjectNumber = 9, Subject, NULL)) as Subject9,MAX(IF(SubjectNumber = 9, NULL, NULL)) as Grade9,MAX(IF(SubjectNumber = 9, Units, NULL)) as Units9,MAX(IF(SubjectNumber = 10, Subject, NULL)) as Subject10,MAX(IF(SubjectNumber = 10, NULL, NULL)) as Grade10,MAX(IF(SubjectNumber = 10, Units, NULL)) as Units10,COUNT(Subject) as TotalSubject,SUM(Units) as TotalUnits, sg_course_id, s_fn,s_mn,s_ln,course_major from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', CONCAT(s_ln, ', ', s_fn, ' ', LEFT(s_mn, 1),'.') as 'Student',(s_gender) AS 'Sex',(course_code) AS 'Course',(sg_yearlevel) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(subject_units) AS 'Units',Format(sg_grade, 1) AS 'Grade', (sg_grade_status) AS 'GradeStatus', sg_course_id, s_fn,s_mn,s_ln,course_major FROM tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' and sg_course_id = " & courseId & " group by ID order by Student"
                                Else
                                    sql = "SELECT ID, Student, Sex, Course, LEFT(YearLevel,1), Birthdate,MAX(IF(SubjectNumber = 1, Subject, NULL)) as Subject1,MAX(IF(SubjectNumber = 1, NULL, NULL)) as Grade1,MAX(IF(SubjectNumber = 1, Units, NULL)) as Units1,MAX(IF(SubjectNumber = 2, Subject, NULL)) as Subject2,MAX(IF(SubjectNumber = 2, NULL, NULL)) as Grade2,MAX(IF(SubjectNumber = 2, Units, NULL)) as Units2,MAX(IF(SubjectNumber = 3, Subject, NULL)) as Subject3,MAX(IF(SubjectNumber = 3, NULL, NULL)) as Grade3,MAX(IF(SubjectNumber = 3, Units, NULL)) as Units3,MAX(IF(SubjectNumber = 4, Subject, NULL)) as Subject4,MAX(IF(SubjectNumber = 4, NULL, NULL)) as Grade4,MAX(IF(SubjectNumber = 4, Units, NULL)) as Units4,MAX(IF(SubjectNumber = 5, Subject, NULL)) as Subject5,MAX(IF(SubjectNumber = 5, NULL, NULL)) as Grade5,MAX(IF(SubjectNumber = 5, Units, NULL)) as Units5,MAX(IF(SubjectNumber = 6, Subject, NULL)) as Subject6,MAX(IF(SubjectNumber = 6, NULL, NULL)) as Grade6,MAX(IF(SubjectNumber = 6, Units, NULL)) as Units6,MAX(IF(SubjectNumber = 7, Subject, NULL)) as Subject7,MAX(IF(SubjectNumber = 7, NULL, NULL)) as Grade7,MAX(IF(SubjectNumber = 7, Units, NULL)) as Units7,MAX(IF(SubjectNumber = 8, Subject, NULL)) as Subject8,MAX(IF(SubjectNumber = 8, NULL, NULL)) as Grade8,MAX(IF(SubjectNumber = 8, Units, NULL)) as Units8,MAX(IF(SubjectNumber = 9, Subject, NULL)) as Subject9,MAX(IF(SubjectNumber = 9, NULL, NULL)) as Grade9,MAX(IF(SubjectNumber = 9, Units, NULL)) as Units9,MAX(IF(SubjectNumber = 10, Subject, NULL)) as Subject10,MAX(IF(SubjectNumber = 10, NULL, NULL)) as Grade10,MAX(IF(SubjectNumber = 10, Units, NULL)) as Units10,COUNT(Subject) as TotalSubject,SUM(Units) as TotalUnits, sg_course_id, s_fn,s_mn,s_ln,course_major from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', CONCAT(s_ln, ', ', s_fn, ' ', LEFT(s_mn, 1),'.') as 'Student',(s_gender) AS 'Sex',(course_code) AS 'Course',(sg_yearlevel) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(subject_units) AS 'Units',Format(sg_grade, 1) AS 'Grade', (sg_grade_status) AS 'GradeStatus', sg_course_id, s_fn,s_mn,s_ln,course_major FROM tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' and sg_course_id = " & courseId & " and YearLevel like '%" & cbEPlistYearLevel.Text & "%' group by ID order by Student"
                                End If
                            ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                                If cbEPlistYearLevel.Text = "All" Then
                                    sql = "SELECT ID, Student, Sex, Course, LEFT(YearLevel,1), Birthdate,MAX(IF(SubjectNumber = 1, Subject, NULL)) as Subject1,MAX(IF(SubjectNumber = 1, Grade, NULL)) as Grade1,MAX(IF(SubjectNumber = 1, Units, NULL)) as Units1,MAX(IF(SubjectNumber = 2, Subject, NULL)) as Subject2,MAX(IF(SubjectNumber = 2, Grade, NULL)) as Grade2,MAX(IF(SubjectNumber = 2, Units, NULL)) as Units2,MAX(IF(SubjectNumber = 3, Subject, NULL)) as Subject3,MAX(IF(SubjectNumber = 3, Grade, NULL)) as Grade3,MAX(IF(SubjectNumber = 3, Units, NULL)) as Units3,MAX(IF(SubjectNumber = 4, Subject, NULL)) as Subject4,MAX(IF(SubjectNumber = 4, Grade, NULL)) as Grade4,MAX(IF(SubjectNumber = 4, Units, NULL)) as Units4,MAX(IF(SubjectNumber = 5, Subject, NULL)) as Subject5,MAX(IF(SubjectNumber = 5, Grade, NULL)) as Grade5,MAX(IF(SubjectNumber = 5, Units, NULL)) as Units5,MAX(IF(SubjectNumber = 6, Subject, NULL)) as Subject6,MAX(IF(SubjectNumber = 6, Grade, NULL)) as Grade6,MAX(IF(SubjectNumber = 6, Units, NULL)) as Units6,MAX(IF(SubjectNumber = 7, Subject, NULL)) as Subject7,MAX(IF(SubjectNumber = 7, Grade, NULL)) as Grade7,MAX(IF(SubjectNumber = 7, Units, NULL)) as Units7,MAX(IF(SubjectNumber = 8, Subject, NULL)) as Subject8,MAX(IF(SubjectNumber = 8, Grade, NULL)) as Grade8,MAX(IF(SubjectNumber = 8, Units, NULL)) as Units8,MAX(IF(SubjectNumber = 9, Subject, NULL)) as Subject9,MAX(IF(SubjectNumber = 9, Grade, NULL)) as Grade9,MAX(IF(SubjectNumber = 9, Units, NULL)) as Units9,MAX(IF(SubjectNumber = 10, Subject, NULL)) as Subject10,MAX(IF(SubjectNumber = 10, Grade, NULL)) as Grade10,MAX(IF(SubjectNumber = 10, Units, NULL)) as Units10,COUNT(Subject) as TotalSubject,SUM(Units) as TotalUnits, sg_course_id, s_fn,s_mn,s_ln,course_major from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', CONCAT(s_ln, ', ', s_fn, ' ', LEFT(s_mn, 1),'.') as 'Student',(s_gender) AS 'Sex',(course_code) AS 'Course',(sg_yearlevel) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(sg_credits) AS 'Units',IF(sg_grade > 0, ROUND(sg_grade,1),sg_grade) AS 'Grade', (sg_grade_status) AS 'GradeStatus', sg_course_id, s_fn,s_mn,s_ln,course_major FROM tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' and sg_course_id = " & courseId & " group by ID order by Student"
                                Else
                                    sql = "SELECT ID, Student, Sex, Course, LEFT(YearLevel,1), Birthdate,MAX(IF(SubjectNumber = 1, Subject, NULL)) as Subject1,MAX(IF(SubjectNumber = 1, Grade, NULL)) as Grade1,MAX(IF(SubjectNumber = 1, Units, NULL)) as Units1,MAX(IF(SubjectNumber = 2, Subject, NULL)) as Subject2,MAX(IF(SubjectNumber = 2, Grade, NULL)) as Grade2,MAX(IF(SubjectNumber = 2, Units, NULL)) as Units2,MAX(IF(SubjectNumber = 3, Subject, NULL)) as Subject3,MAX(IF(SubjectNumber = 3, Grade, NULL)) as Grade3,MAX(IF(SubjectNumber = 3, Units, NULL)) as Units3,MAX(IF(SubjectNumber = 4, Subject, NULL)) as Subject4,MAX(IF(SubjectNumber = 4, Grade, NULL)) as Grade4,MAX(IF(SubjectNumber = 4, Units, NULL)) as Units4,MAX(IF(SubjectNumber = 5, Subject, NULL)) as Subject5,MAX(IF(SubjectNumber = 5, Grade, NULL)) as Grade5,MAX(IF(SubjectNumber = 5, Units, NULL)) as Units5,MAX(IF(SubjectNumber = 6, Subject, NULL)) as Subject6,MAX(IF(SubjectNumber = 6, Grade, NULL)) as Grade6,MAX(IF(SubjectNumber = 6, Units, NULL)) as Units6,MAX(IF(SubjectNumber = 7, Subject, NULL)) as Subject7,MAX(IF(SubjectNumber = 7, Grade, NULL)) as Grade7,MAX(IF(SubjectNumber = 7, Units, NULL)) as Units7,MAX(IF(SubjectNumber = 8, Subject, NULL)) as Subject8,MAX(IF(SubjectNumber = 8, Grade, NULL)) as Grade8,MAX(IF(SubjectNumber = 8, Units, NULL)) as Units8,MAX(IF(SubjectNumber = 9, Subject, NULL)) as Subject9,MAX(IF(SubjectNumber = 9, Grade, NULL)) as Grade9,MAX(IF(SubjectNumber = 9, Units, NULL)) as Units9,MAX(IF(SubjectNumber = 10, Subject, NULL)) as Subject10,MAX(IF(SubjectNumber = 10, Grade, NULL)) as Grade10,MAX(IF(SubjectNumber = 10, Units, NULL)) as Units10,COUNT(Subject) as TotalSubject,SUM(Units) as TotalUnits, sg_course_id, s_fn,s_mn,s_ln,course_major from (SELECT ROW_NUMBER() OVER (PARTITION BY sg_student_id, sg_period_id ORDER BY sg_student_id, subject_code asc) AS SubjectNumber, (sg_student_id) as 'ID', CONCAT(s_ln, ', ', s_fn, ' ', LEFT(s_mn, 1),'.') as 'Student',(s_gender) AS 'Sex',(course_code) AS 'Course',(sg_yearlevel) AS 'YearLevel',date_format(s_dob, '%m/%d/%Y') AS 'Birthdate',(sg_period_id) AS 'AcademicID',CONCAT(period_name,' - ',period_semester) AS 'Academic Year',(subject_code) AS 'Subject',(sg_credits) AS 'Units',IF(sg_grade > 0, ROUND(sg_grade,1),sg_grade) AS 'Grade', (sg_grade_status) AS 'GradeStatus', sg_course_id, s_fn,s_mn,s_ln,course_major FROM tbl_students_grades LEFT JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no LEFT JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id LEFT JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id  LEFT JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id where sg_grade_status = 'Enrolled' order by subject_code) as promolist2 where AcademicID = " & CInt(cbAcademicYear.SelectedValue) & " and GradeStatus = 'Enrolled' and sg_course_id = " & courseId & " and YearLevel like '%" & cbEPlistYearLevel.Text & "%' group by ID order by Student"
                                End If
                            End If
                            Dim dbcommand As New MySqlCommand(sql, cn)
                            adt.SelectCommand = dbcommand
                            dtable = New DataTable
                            adt.Fill(dtable)
                            dg_report.DataSource = dtable
                            adt.Dispose()
                            dbcommand.Dispose()

                            dt.Columns.Clear()
                            dt.Rows.Clear()
                            With dt
                                .Columns.Add("ID")
                                .Columns.Add("Student")
                                .Columns.Add("Sex")
                                .Columns.Add("Course")
                                .Columns.Add("YearLevel")
                                .Columns.Add("Birthdate")
                                .Columns.Add("Subject1")
                                .Columns.Add("Grade1")
                                .Columns.Add("Units1")
                                .Columns.Add("Subject2")
                                .Columns.Add("Grade2")
                                .Columns.Add("Units2")
                                .Columns.Add("Subject3")
                                .Columns.Add("Grade3")
                                .Columns.Add("Units3")
                                .Columns.Add("Subject4")
                                .Columns.Add("Grade4")
                                .Columns.Add("Units4")
                                .Columns.Add("Subject5")
                                .Columns.Add("Grade5")
                                .Columns.Add("Units5")
                                .Columns.Add("Subject6")
                                .Columns.Add("Grade6")
                                .Columns.Add("Units6")
                                .Columns.Add("Subject7")
                                .Columns.Add("Grade7")
                                .Columns.Add("Units7")
                                .Columns.Add("Subject8")
                                .Columns.Add("Grade8")
                                .Columns.Add("Units8")
                                .Columns.Add("Subject9")
                                .Columns.Add("Grade9")
                                .Columns.Add("Units9")
                                .Columns.Add("Subject10")
                                .Columns.Add("Grade10")
                                .Columns.Add("Units10")
                                .Columns.Add("TotalSubject")
                                .Columns.Add("TotalUnits")
                                .Columns.Add("stud_first_name")
                                .Columns.Add("stud_middle_name")
                                .Columns.Add("stud_last_name")
                                .Columns.Add("course_major")
                            End With
                            For Each dr As DataGridViewRow In dg_report.Rows
                                dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value, dr.Cells(11).Value, dr.Cells(12).Value, dr.Cells(13).Value, dr.Cells(14).Value, dr.Cells(15).Value, dr.Cells(16).Value, dr.Cells(17).Value, dr.Cells(18).Value, dr.Cells(19).Value, dr.Cells(20).Value, dr.Cells(21).Value, dr.Cells(22).Value, dr.Cells(23).Value, dr.Cells(24).Value, dr.Cells(25).Value, dr.Cells(26).Value, dr.Cells(27).Value, dr.Cells(28).Value, dr.Cells(29).Value, dr.Cells(30).Value, dr.Cells(31).Value, dr.Cells(32).Value, dr.Cells(33).Value, dr.Cells(34).Value, dr.Cells(35).Value, dr.Cells(36).Value, dr.Cells(37).Value, dr.Cells(39).Value, dr.Cells(40).Value, dr.Cells(41).Value, dr.Cells(42).Value)
                            Next
                            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                            'If frmMain.formTitle.Text = "Generate Enrollment List" Then
                            '    rptdoc = New EnrollmentListing
                            '    rptdoc.SetDataSource(dt)
                            '    rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                            '    rptdoc.SetParameterValue("promo_president_admin", cbPresident.Text)
                            '    rptdoc.SetParameterValue("promo_prepared_by", cbRegistrar.Text)
                            '    rptdoc.SetParameterValue("course_grade_strand", "Course")
                            '    rptdoc.SetParameterValue("year_grade", "Year Level")
                            '    ReportViewer.ReportSource = rptdoc
                            'ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                            rptdoc = New Promotional
                            rptdoc.SetDataSource(dt)
                            rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                            rptdoc.SetParameterValue("promo_president_admin", cbPresident.Text)
                            rptdoc.SetParameterValue("promo_prepared_by", cbRegistrar.Text)
                            rptdoc.SetParameterValue("course_grade_strand", "Course")
                            rptdoc.SetParameterValue("year_grade", "Year Level")
                            If frmMain.formTitle.Text = "Generate Enrollment List" Then
                                rptdoc.SetParameterValue("reportName", "ENROLLMENT")
                            ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
                                rptdoc.SetParameterValue("reportName", "PROMOTIONAL")
                            End If
                            ReportViewer.ReportSource = rptdoc
                            'End If
                            dg_report.DataSource = Nothing
                            ReportViewer.Select()
                            ReportGenerated = True
                        End If
                    End If
                End If
            End If
        ElseIf frmMain.formTitle.Text = "Generate Enrolled Student List" Then
            If CInt(cbAcademicYear.SelectedValue) <= 0 Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Academic Year.", vbCritical)
                cbAcademicYear.Select()
            Else
                Try
                    If CheckBox11.Checked = True Then
                        load_datagrid("select `t1`.`Class` AS `Class`,`t1`.`Period` AS `Period`,`t1`.`Code` AS `Code`,`t1`.`Subject` AS `Subject`,`t1`.`Population` AS `Population`,ifnull(count(`t2`.`sg_class_id`),0) AS `Enrolled` from (`cfcissmsdb`.`classschedulelibrary` `t1` left join `cfcissmsdb`.`tbl_students_grades` `t2` on(`t1`.`class_schedule_id` = `t2`.`sg_class_id`)) WHERE `t1`.`P_ID` = " & CInt(cbAcademicYear.SelectedValue) & " group by `t1`.`class_schedule_id`", dg_report)
                        Dim dt As New DataTable
                        With dt
                            .Columns.Add("class")
                            .Columns.Add("period")
                            .Columns.Add("code")
                            .Columns.Add("subject")
                            .Columns.Add("population")
                            .Columns.Add("enrolled")
                        End With

                        For Each dr As DataGridViewRow In dg_report.Rows
                            dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value)
                        Next

                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New EnrolledPerClassSchedule

                        rptdoc.SetDataSource(dt)
                        'rptdoc.SetParameterValue("schoolyear", cmb_period.Text)
                        rptdoc.SetParameterValue("preparedby", str_name)
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing

                    ElseIf CheckBox12.Checked = True Then

                        Dim sql As String = ""
                        If cbSearchbyDate.Checked = True Then
                            sql = "SELECT t1.sg_student_id as SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, CONCAT(course_code,' - ', course_name), DATE_FORMAT(EnrollDate, '%M %d, %Y'), s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) as Student, eperiod_id as Period, eenrolledby_datetime as EnrollDate from tbl_enrollment where eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & ") t4 ON t1.sg_student_id = t4.Student and t1.sg_period_id = t4.Period where t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and t1.sg_grade_status = 'Enrolled' and t4.EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' GROUP BY t1.sg_student_id order by course_code asc"
                        ElseIf cbSearchbyAcad.Checked = True Then
                            sql = "SELECT t1.sg_student_id as SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, CONCAT(course_code,' - ', course_name), DATE_FORMAT(EnrollDate, '%M %d, %Y'), s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) as Student, eperiod_id as Period, eenrolledby_datetime as EnrollDate from tbl_enrollment where eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & ") t4 ON t1.sg_student_id = t4.Student and t1.sg_period_id = t4.Period where t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and t1.sg_grade_status = 'Enrolled' GROUP BY t1.sg_student_id order by course_code asc"
                        End If

                        load_datagrid(sql, dg_report)

                        Dim dt As New DataTable
                        With dt
                            .Columns.Add("estudent_id")
                            .Columns.Add("s_fn")
                            .Columns.Add("s_mn")
                            .Columns.Add("s_ln")
                            .Columns.Add("s_gender")
                            .Columns.Add("s_yr_lvl")
                            .Columns.Add("course_code")
                            .Columns.Add("eenrolledby_datetime")
                            .Columns.Add("status")
                        End With

                        For Each dr As DataGridViewRow In dg_report.Rows
                            dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(8).Value)
                        Next

                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New EnrolledStudent_Short2

                        rptdoc.SetDataSource(dt)
                        rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                        rptdoc.SetParameterValue("preparedby", str_name)
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing

                    ElseIf CheckBox13.Checked = True Then
                        cn.Close()
                        cn.Open()
                        Dim sql As String = ""
                        If frmMain.systemModule.Text = "College Module" Then
                            If cbSearchbyDate.Checked = True Then
                                sql = "SELECT course_code as 'Course Code', course_name as 'Course Description', SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'New' THEN 1 ELSE 0 END) AS '1st Year New', SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '1st Year Old', SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '1st Year Returnee', SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '1st Year Transferee', SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END) AS '1st Year Irreg. New', SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '1st Year Irreg. Old', SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '1st Year Irreg. Returnee', SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '1st Year Irreg. Transferee', SUM(CASE WHEN sg_yearlevel = '1st Year' OR sg_yearlevel = '1st Year Irreg.' THEN 1 ELSE 0 END) AS '1st Year Total', SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'New' THEN 1 ELSE 0 END) AS '2nd Year New', SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '2nd Year Old', SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '2nd Year Returnee', SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '2nd Year Transferee', SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END) AS '2nd Year Irreg. New', SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '2nd Year Irreg. Old', SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '2nd Year Irreg. Returnee', SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '2nd Year Irreg. Transferee', SUM(CASE WHEN sg_yearlevel = '2nd Year' OR sg_yearlevel = '2nd Year Irreg.' THEN 1 ELSE 0 END) AS '2nd Year Total', SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'New' THEN 1 ELSE 0 END) AS '3rd Year New', SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '3rd Year Old', SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '3rd Year Returnee', SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '3rd Year Transferee', SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END) AS '3rd Year Irreg. New', SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '3rd Year Irreg. Old', SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '3rd Year Irreg. Returnee', SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '3rd Year Irreg. Transferee', SUM(CASE WHEN sg_yearlevel = '3rd Year' OR sg_yearlevel = '3rd Year Irreg.' THEN 1 ELSE 0 END) AS '3rd Year Total', SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'New' THEN 1 ELSE 0 END) AS '4th Year New', SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '4th Year Old', SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '4th Year Returnee', SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '4th Year Transferee', SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END) AS '4th Year Irreg. New', SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '4th Year Irreg. Old', SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '4th Year Irreg. Returnee', SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '4th Year Irreg. Transferee', SUM(CASE WHEN sg_yearlevel = '4th Year' OR sg_yearlevel = '4th Year Irreg.' THEN 1 ELSE 0 END) AS '4th Year Total', SUM(CASE WHEN sg_yearlevel LIKE '%Year%' THEN 1 ELSE 0 END) AS 'Overall Total' FROM (SELECT t1.sg_student_id AS SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, course_code, course_name, DATE_FORMAT(EnrollDate, '%M %d, %Y' ) AS EnrollDate, s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) AS Student, eperiod_id AS Period, eenrolledby_datetime AS EnrollDate FROM tbl_enrollment WHERE eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & " ) t4 ON t1.sg_student_id = t4.Student AND t1.sg_period_id = t4.Period WHERE t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND t1.sg_grade_status = 'Enrolled' AND t4.EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' GROUP BY t1.sg_student_id) AS sub GROUP BY course_code, course_name UNION ALL SELECT 'Overall Total', '', SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year' OR sg_yearlevel = '1st Year Irreg.' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year' OR sg_yearlevel = '2nd Year Irreg.' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year' OR sg_yearlevel = '3rd Year Irreg.' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year' OR sg_yearlevel = '4th Year Irreg.' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel LIKE '%Year%' THEN 1 ELSE 0 END) FROM (SELECT t1.sg_student_id AS SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, course_code, course_name, DATE_FORMAT(EnrollDate, '%M %d, %Y' ) AS EnrollDate, s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) AS Student, eperiod_id AS Period, eenrolledby_datetime AS EnrollDate FROM tbl_enrollment WHERE eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & " ) t4 ON t1.sg_student_id = t4.Student AND t1.sg_period_id = t4.Period WHERE t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND t1.sg_grade_status = 'Enrolled' AND t4.EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' GROUP BY t1.sg_student_id) AS sub;"
                            ElseIf cbSearchbyAcad.Checked = True Then
                                sql = "SELECT course_code as 'Course Code', course_name as 'Course Description', SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'New' THEN 1 ELSE 0 END) AS '1st Year New', SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '1st Year Old', SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '1st Year Returnee', SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '1st Year Transferee', SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END) AS '1st Year Irreg. New', SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '1st Year Irreg. Old', SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '1st Year Irreg. Returnee', SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '1st Year Irreg. Transferee', SUM(CASE WHEN sg_yearlevel = '1st Year' OR sg_yearlevel = '1st Year Irreg.' THEN 1 ELSE 0 END) AS '1st Year Total', SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'New' THEN 1 ELSE 0 END) AS '2nd Year New', SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '2nd Year Old', SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '2nd Year Returnee', SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '2nd Year Transferee', SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END) AS '2nd Year Irreg. New', SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '2nd Year Irreg. Old', SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '2nd Year Irreg. Returnee', SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '2nd Year Irreg. Transferee', SUM(CASE WHEN sg_yearlevel = '2nd Year' OR sg_yearlevel = '2nd Year Irreg.' THEN 1 ELSE 0 END) AS '2nd Year Total', SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'New' THEN 1 ELSE 0 END) AS '3rd Year New', SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '3rd Year Old', SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '3rd Year Returnee', SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '3rd Year Transferee', SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END) AS '3rd Year Irreg. New', SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '3rd Year Irreg. Old', SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '3rd Year Irreg. Returnee', SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '3rd Year Irreg. Transferee', SUM(CASE WHEN sg_yearlevel = '3rd Year' OR sg_yearlevel = '3rd Year Irreg.' THEN 1 ELSE 0 END) AS '3rd Year Total', SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'New' THEN 1 ELSE 0 END) AS '4th Year New', SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '4th Year Old', SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '4th Year Returnee', SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '4th Year Transferee', SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END) AS '4th Year Irreg. New', SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END) AS '4th Year Irreg. Old', SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END) AS '4th Year Irreg. Returnee', SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END) AS '4th Year Irreg. Transferee', SUM(CASE WHEN sg_yearlevel = '4th Year' OR sg_yearlevel = '4th Year Irreg.' THEN 1 ELSE 0 END) AS '4th Year Total', SUM(CASE WHEN sg_yearlevel LIKE '%Year%' THEN 1 ELSE 0 END) AS 'Overall Total' FROM (SELECT t1.sg_student_id AS SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, course_code, course_name, DATE_FORMAT(EnrollDate, '%M %d, %Y' ) AS EnrollDate, s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) AS Student, eperiod_id AS Period, eenrolledby_datetime AS EnrollDate FROM tbl_enrollment WHERE eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & " ) t4 ON t1.sg_student_id = t4.Student AND t1.sg_period_id = t4.Period WHERE t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND t1.sg_grade_status = 'Enrolled' GROUP BY t1.sg_student_id) AS sub GROUP BY course_code, course_name UNION ALL SELECT 'Overall Total', '', SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '1st Year' OR sg_yearlevel = '1st Year Irreg.' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '2nd Year' OR sg_yearlevel = '2nd Year Irreg.' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '3rd Year' OR sg_yearlevel = '3rd Year Irreg.' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'New' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Old' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Returnee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year Irreg.' AND s_status = 'Transferee' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel = '4th Year' OR sg_yearlevel = '4th Year Irreg.' THEN 1 ELSE 0 END), SUM(CASE WHEN sg_yearlevel LIKE '%Year%' THEN 1 ELSE 0 END) FROM (SELECT t1.sg_student_id AS SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, course_code, course_name, DATE_FORMAT(EnrollDate, '%M %d, %Y' ) AS EnrollDate, s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) AS Student, eperiod_id AS Period, eenrolledby_datetime AS EnrollDate FROM tbl_enrollment WHERE eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & " ) t4 ON t1.sg_student_id = t4.Student AND t1.sg_period_id = t4.Period WHERE t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND t1.sg_grade_status = 'Enrolled' GROUP BY t1.sg_student_id) AS sub;"
                            End If
                        Else
                            sql = "select course_code as 'GRADE/STRAND', '' as 'OVERALL',SUM(CASE WHEN t0.sg_yearlevel = 'Kinder 1' THEN 1 ELSE 0 END) AS 'KINDER 1',SUM(CASE WHEN t0.sg_yearlevel = 'Kinder 2' THEN 1 ELSE 0 END) AS 'KINDER 2',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 1' THEN 1 ELSE 0 END) AS 'GRADE 1',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 7' THEN 1 ELSE 0 END) AS 'GRADE 7',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 8' THEN 1 ELSE 0 END) AS 'GRADE 8',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 9' THEN 1 ELSE 0 END) AS 'GRADE 9',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 10' THEN 1 ELSE 0 END) AS 'GRADE 10',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 11' THEN 1 ELSE 0 END) AS 'GRADE 11',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 12' THEN 1 ELSE 0 END) AS 'GRADE 12',SUM(CASE WHEN t0.sg_yearlevel IS NULL THEN 0 ELSE 1 END) AS 'TOTAL', SUM(CASE WHEN t0.s_status = 'Old' THEN 1 ELSE 0 END) AS 'OLD', SUM(CASE WHEN t0.s_status = 'Returnee' THEN 1 ELSE 0 END) AS 'RETURNEE', SUM(CASE WHEN t0.s_status = 'Transferee' THEN 1 ELSE 0 END) AS 'TRANSFEREE', SUM(CASE WHEN t0.s_status = 'New' THEN 1 ELSE 0 END) AS 'NEW','' as 'TODAY',SUM(CASE WHEN t0.sg_yearlevel = 'Kinder 1' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'KINDER 1',SUM(CASE WHEN t0.sg_yearlevel = 'Kinder 2' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'KINDER 2',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 1' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'GRADE 1',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 7' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'GRADE 7',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 8' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'GRADE 8',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 9' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'GRADE 9',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 10' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'GRADE 10',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 11' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'GRADE 11',SUM(CASE WHEN t0.sg_yearlevel = 'Grade 12' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'GRADE 12',SUM(CASE WHEN t0.sg_yearlevel IS NOT NULL and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'TOTAL',SUM(CASE WHEN t0.s_status = 'Old' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'OLD', SUM(CASE WHEN t0.s_status = 'Returnee' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'RETURNEE', SUM(CASE WHEN t0.s_status = 'Transferee' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'TRANSFEREE', SUM(CASE WHEN t0.s_status = 'New' and EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' THEN 1 ELSE 0 END) AS 'NEW' from (SELECT t1.sg_student_id as SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, course_code, course_name, EnrollDate, s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) as Student, eperiod_id as Period, eenrolledby_datetime as EnrollDate from tbl_enrollment where eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & ") t4 ON t1.sg_student_id = t4.Student and t1.sg_period_id = t4.Period where t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and t1.sg_grade_status = 'Enrolled' GROUP BY t1.sg_student_id order by course_code asc) as t0 group by t0.course_code"
                        End If
                        load_datagrid(sql, dg_report)

                        Dim csv As String = String.Empty
                        For Each column As DataGridViewColumn In dg_report.Columns
                            csv += column.HeaderText & ","c
                        Next
                        csv += vbCr & vbLf
                        For Each row2 As DataGridViewRow In dg_report.Rows
                            For Each cell As DataGridViewCell In row2.Cells
                                csv += cell.Value.ToString().Replace(",", ";") & ","c
                            Next
                            csv += vbCr & vbLf
                        Next

                        Dim proc As New System.Diagnostics.Process()
                        Dim folderPath As String
                        If frmMain.systemModule.Text = "College Module" Then
                            folderPath = "C:\College Enrollment Summary\" & cbAcademicYear.Text & "\"
                        Else
                            folderPath = "C:\High School Enrollment Summary\" & cbAcademicYear.Text & "\"
                        End If

                        Dim d1 As String = dtFrom.Text
                        Dim dttfrom As String = d1.Replace("/", "-")
                        Dim d2 As String = dtTo.Text
                        Dim dttto As String = d2.Replace("/", "-")

                        If frmMain.systemModule.Text = "College Module" Then
                            If Not IO.Directory.Exists(folderPath) Then
                                IO.Directory.CreateDirectory(folderPath)
                                File.WriteAllText(folderPath & "College Enrollment -" & dttfrom & "-" & dttto & ".csv", csv)

                                MessageBox.Show("Enrollment successfully exported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                proc = Process.Start(folderPath & "College Enrollment -" & dttfrom & "-" & dttto & ".csv", "")
                            Else
                                File.WriteAllText(folderPath & "College Enrollment -" & dttfrom & "-" & dttto & ".csv", csv)

                                MessageBox.Show("Enrollment successfully exported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                proc = Process.Start(folderPath & "College Enrollment -" & dttfrom & "-" & dttto & ".csv", "")
                            End If
                        Else
                            If Not IO.Directory.Exists(folderPath) Then
                                IO.Directory.CreateDirectory(folderPath)
                                File.WriteAllText(folderPath & "High School Enrollment -" & dttfrom & "-" & dttto & ".csv", csv)

                                MessageBox.Show("Enrollment successfully exported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                proc = Process.Start(folderPath & "High School Enrollment -" & dttfrom & "-" & dttto & ".csv", "")
                            Else
                                File.WriteAllText(folderPath & "High School Enrollment -" & dttfrom & "-" & dttto & ".csv", csv)

                                MessageBox.Show("Enrollment successfully exported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                proc = Process.Start(folderPath & "High School Enrollment -" & dttfrom & "-" & dttto & ".csv", "")
                            End If
                        End If
                    Else
                        NextBtn()

                        Dim sql As String = ""
                        If cbAllCourse.Checked = True Then
                            If cbEPlistYearLevel.Text = "All" Then
                                sql = "SELECT t1.sg_student_id as SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, CONCAT(course_code,' - ', course_name), DATE_FORMAT(EnrollDate, '%M %d, %Y'), s_contact, s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) as Student, eperiod_id as Period, eenrolledby_datetime as EnrollDate from tbl_enrollment where eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & ") t4 ON t1.sg_student_id = t4.Student and t1.sg_period_id = t4.Period where t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and t1.sg_grade_status = 'Enrolled' and t4.EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' GROUP BY t1.sg_student_id order by course_code asc"
                            Else
                                sql = "SELECT t1.sg_student_id as SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, CONCAT(course_code,' - ', course_name), DATE_FORMAT(EnrollDate, '%M %d, %Y' ), s_contact, s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) as Student, eperiod_id as Period, eenrolledby_datetime as EnrollDate from tbl_enrollment where eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & ") t4 ON t1.sg_student_id = t4.Student and t1.sg_period_id = t4.Period where t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and t1.sg_grade_status = 'Enrolled' and t1.sg_yearlevel like '%" & cbEPlistYearLevel.Text & "%' and t4.EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' GROUP BY t1.sg_student_id order by s_ln"
                            End If
                        Else
                            If cbEPlistYearLevel.Text = "All" Then
                                sql = "SELECT t1.sg_student_id as SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, CONCAT(course_code,' - ', course_name), DATE_FORMAT(EnrollDate, '%M %d, %Y' ), s_contact, s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) as Student, eperiod_id as Period, eenrolledby_datetime as EnrollDate from tbl_enrollment where eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & ") t4 ON t1.sg_student_id = t4.Student and t1.sg_period_id = t4.Period where t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_course_id = " & courseId & " and t1.sg_grade_status = 'Enrolled' and t4.EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' GROUP BY t1.sg_student_id order by s_ln"
                            Else
                                sql = "SELECT t1.sg_student_id as SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, CONCAT(course_code,' - ', course_name), DATE_FORMAT(EnrollDate, '%M %d, %Y' ), s_contact, s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) as Student, eperiod_id as Period, eenrolledby_datetime as EnrollDate from tbl_enrollment where eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & ") t4 ON t1.sg_student_id = t4.Student and t1.sg_period_id = t4.Period where t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_course_id = " & courseId & " and t1.sg_grade_status = 'Enrolled' and t1.sg_yearlevel like '%" & cbEPlistYearLevel.Text & "%' and t4.EnrollDate BETWEEN '" & dtFrom.Text & "' AND '" & dtTo.Text & "' GROUP BY t1.sg_student_id order by s_ln"
                            End If
                        End If
                        load_datagrid(sql, dg_report)

                        Dim dt As New DataTable
                        With dt
                            .Columns.Add("estudent_id")
                            .Columns.Add("s_fn")
                            .Columns.Add("s_mn")
                            .Columns.Add("s_ln")
                            .Columns.Add("s_gender")
                            .Columns.Add("s_yr_lvl")
                            .Columns.Add("course_code")
                            .Columns.Add("eenrolledby_datetime")
                            .Columns.Add("s_contact")
                        End With

                        For Each dr As DataGridViewRow In dg_report.Rows
                            dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(9).Value)
                        Next

                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New EnrolledStudentWithStatus

                        rptdoc.SetDataSource(dt)
                        rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                        rptdoc.SetParameterValue("preparedby", str_name)
                        rptdoc.SetParameterValue("schoolname", " ")
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, vbCritical)
                    cn.Close()
                    PrevBtn()
                End Try
            End If
        ElseIf frmMain.formTitle.Text = "Generate Student Credential" Then
            Select Case cbCredentials.Text
                Case "Good Moral Character"
                    If studentId = String.Empty Then
                        ReportViewer.ReportSource = Nothing
                        MsgBox("Please select Student.", vbCritical)
                        btnSearchStudent.Select()
                    ElseIf CInt(cbAcademicYear.SelectedValue) <= 0 Then
                        ReportViewer.ReportSource = Nothing
                        MsgBox("Please select Academic Year.", vbCritical)
                        cbAcademicYear.Select()
                    Else
                        Try
                            NextBtn()
                            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                            rptdoc = New GMC

                            cn.Close()
                            Dim studentCivilStatus As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                            studentCivilStatus = cm.ExecuteScalar
                            cn.Close()

                            If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Ms")
                            ElseIf studentGender = "Male" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Mr")
                            ElseIf studentGender = "Female" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                            End If

                            If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                            ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                            Else
                                rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                            End If

                            If studentGender = "Female" Then
                                rptdoc.SetParameterValue("heshe", "she")
                            ElseIf studentGender = "Male" Then
                                rptdoc.SetParameterValue("heshe", "he")
                            End If

                            Dim fnlow As String = studentFName.ToLower
                            Dim fn As String
                            fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                            Dim lnlow As String = studentLName.ToLower
                            Dim ln As String
                            ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                            Dim mnlow As String = studentMName.ToLower
                            Dim mn As String
                            mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                            rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)

                            'If smn.Text = String.Empty Then
                            '    rptdoc.SetParameterValue("studentname", fn & " " & ln)
                            'Else
                            '    Dim middleinitial As String
                            '    middleinitial = smn.Text
                            '    Dim mi As String
                            '    mi = middleinitial.Substring(0, 1)
                            '    rptdoc.SetParameterValue("studentname", fn & " " & mi & ". " & ln)
                            'End If

                            Try
                                Dim scourselow As String = studentGradeLevelCourseName.ToLower
                                Dim scourse2 As String
                                scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                                Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                                Dim scourse As String = scourse3.Replace(" Of ", " of ")
                                rptdoc.SetParameterValue("course", scourse)
                            Catch ex As Exception
                                rptdoc.SetParameterValue("course", "")
                                MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                                ReportViewer.ReportSource = Nothing
                                cbAcademicYear.Select()
                                PrevBtn()
                                Return
                            End Try

                            Try
                                Dim syearlvl As String
                                syearlvl = studentGradeLevel
                                Dim styear As String
                                styear = syearlvl.Substring(0, 3)
                                If styear = "1st" Then
                                    rptdoc.SetParameterValue("yearlevel", "first year")
                                ElseIf styear = "2nd" Then
                                    rptdoc.SetParameterValue("yearlevel", "second year")
                                ElseIf styear = "3rd" Then
                                    rptdoc.SetParameterValue("yearlevel", "third year")
                                ElseIf styear = "4th" Then
                                    rptdoc.SetParameterValue("yearlevel", "fourth year")
                                ElseIf styear = "5th" Then
                                    rptdoc.SetParameterValue("yearlevel", "fifth year")
                                End If
                            Catch ex As Exception
                                rptdoc.SetParameterValue("yearlevel", "")
                                MsgBox("Select the academic year the student is enrolled or graduated to indicate his/her year level.", vbCritical)
                                ReportViewer.ReportSource = Nothing
                                cbAcademicYear.Select()
                                PrevBtn()
                                Return
                            End Try

                            rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                            Dim periodName As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodName = cm.ExecuteScalar
                            cn.Close()
                            rptdoc.SetParameterValue("semesteryear", periodName)
                            Dim periodSemester As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT period_semester FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodSemester = cm.ExecuteScalar
                            cn.Close()
                            Dim syear As String
                            syear = periodSemester
                            Dim semyear As String
                            semyear = syear.Substring(0, 3)
                            If semyear = "1st" Then
                                rptdoc.SetParameterValue("semester", "First Semester")
                            ElseIf semyear = "2nd" Then
                                rptdoc.SetParameterValue("semester", "Second Semester")
                            Else
                                rptdoc.SetParameterValue("semester", "Summer")
                            End If

                            If cbLast.Checked = True Then
                                rptdoc.SetParameterValue("lastcurrent", "last")
                                rptdoc.SetParameterValue("iswas", "was")
                            ElseIf cbCurrent.Checked = True Then
                                rptdoc.SetParameterValue("lastcurrent", "this")
                                rptdoc.SetParameterValue("iswas", "is")
                            End If

                            rptdoc.SetParameterValue("lastname", ln)

                            Dim odate As String
                            odate = Format(Convert.ToDateTime(DateToday), "dd")
                            If odate = "01" Or odate = "21" Or odate = "31" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "st")
                            ElseIf odate = "02" Or odate = "22" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "nd")
                            ElseIf odate = "03" Or odate = "23" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "rd")
                            ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "th")
                            End If

                            If cbRegSign.Checked = True Then
                                rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                            Else
                                rptdoc.SetParameterValue("emp_sign_pic", "0")
                            End If

                            rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))
                            ReportViewer.ReportSource = rptdoc
                            dg_report.DataSource = Nothing
                            ReportViewer.Select()
                            ReportGenerated = True
                            recordRequest()
                        Catch ex As Exception
                            MsgBox(ex.Message, vbCritical)
                            cn.Close()
                            PrevBtn()
                        End Try
                    End If
                Case "Honorable Dismissal"
                    Try
                        NextBtn()
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New HonorableDismissal
                        cn.Close()
                        Dim studentCivilStatus As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentCivilStatus = cm.ExecuteScalar
                        cn.Close()
                        If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Ms")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mr")
                        ElseIf studentGender = "Female" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                        End If


                        Dim fnlow As String = studentFName.ToLower
                        Dim fn As String
                        fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                        Dim lnlow As String = studentLName.ToLower
                        Dim ln As String
                        ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                        Dim mnlow As String = studentMName.ToLower
                        Dim mn As String
                        mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                        rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)

                        'If smn.Text = String.Empty Then
                        '    rptdoc.SetParameterValue("studentname", fn & " " & ln)
                        'Else
                        '    Dim middleinitial As String
                        '    middleinitial = smn.Text
                        '    Dim mi As String
                        '    mi = middleinitial.Substring(0, 1)
                        '    rptdoc.SetParameterValue("studentname", fn & " " & mi & ". " & ln)
                        'End If

                        rptdoc.SetParameterValue("scourse", studentGradeLevelCourseName)

                        Dim periodName As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        periodName = cm.ExecuteScalar
                        cn.Close()
                        rptdoc.SetParameterValue("semesteryear", periodName)
                        Dim periodSemester As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT period_semester FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        periodSemester = cm.ExecuteScalar
                        cn.Close()
                        Dim syear As String
                        syear = periodSemester
                        Dim semyear As String
                        semyear = syear.Substring(0, 3)
                        If semyear = "1st" Then
                            rptdoc.SetParameterValue("semester", "First Semester")
                        ElseIf semyear = "2nd" Then
                            rptdoc.SetParameterValue("semester", "Second Semester")
                        Else
                            rptdoc.SetParameterValue("semester", "Summer")
                        End If


                        If cbRecord.Checked = True Then
                            rptdoc.SetParameterValue("rcpt", txtReceipt.Text)
                            Dim docrqst_no As String = ""
                            cn.Close()
                            cn.Open()
                            cm = New MySqlCommand("Select Ifnull(Count(credrqst_id),0) as id from tbl_student_credrqst where credrqst_doc = 'Honorable Dismissal'", cn)
                            docrqst_no = cm.ExecuteScalar
                            cn.Close()
                            Try
                                rptdoc.SetParameterValue("chd", "HD-" & CInt(docrqst_no) + 1)
                            Catch ex As Exception
                                rptdoc.SetParameterValue("chd", "HD-1")
                            End Try
                        Else
                            rptdoc.SetParameterValue("rcpt", "-")
                            rptdoc.SetParameterValue("chd", "-")
                        End If

                        rptdoc.SetParameterValue("ddate", Format(Convert.ToDateTime(DateToday), "MMMM dd, yyyy"))
                        If cbRegSign.Checked = True Then
                            rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                        Else
                            rptdoc.SetParameterValue("emp_sign_pic", "0")
                        End If
                        rptdoc.SetParameterValue("school", txtSchool.Text)
                        rptdoc.SetParameterValue("schooladdress", SchoolAddress)
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                        recordRequest()
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try
                Case "Diploma"
                    Try
                        NextBtn()
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New Diploma

                        If studentMName = String.Empty Then
                            rptdoc.SetParameterValue("studentname", studentFName & " " & studentLName)
                        Else
                            rptdoc.SetParameterValue("studentname", studentFName & " " & studentMName.Substring(0, 1) & ". " & studentLName)
                        End If

                        Try
                            rptdoc.SetParameterValue("degree", studentGradeLevelCourseName.ToUpper)
                        Catch ex As Exception
                            rptdoc.SetParameterValue("degree", "")
                            MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            cbAcademicYear.Select()
                            PrevBtn()
                            Return
                        End Try

                        cn.Close()
                        Dim soNumber As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT s_so_no FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        soNumber = cm.ExecuteScalar
                        cn.Close()

                        rptdoc.SetParameterValue("sonumber", soNumber)
                        rptdoc.SetParameterValue("registrar", cbRegistrar.Text)
                        rptdoc.SetParameterValue("president", cbPresident.Text)

                        Dim gradDate As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(s_grad_date,'%M %d %Y'), '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        gradDate = cm.ExecuteScalar
                        cn.Close()

                        Dim odate As String
                        odate = Format(Convert.ToDateTime(gradDate), "dd")
                        If odate = "01" Or odate = "21" Or odate = "31" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "st")
                        ElseIf odate = "02" Or odate = "22" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "nd")
                        ElseIf odate = "03" Or odate = "23" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "rd")
                        ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "th")
                        End If

                        rptdoc.SetParameterValue("month", Format(Convert.ToDateTime(gradDate), "MMMM"))
                        rptdoc.SetParameterValue("year", Format(Convert.ToDateTime(gradDate), "yyyy"))

                        Dim textObject As TextObject = CType(rptdoc.ReportDefinition.ReportObjects("degreeText"), TextObject)
                        Dim textObject2 As FieldObject = CType(rptdoc.ReportDefinition.ReportObjects("studentname1"), FieldObject)

                        textObject.ApplyFont(New Font("Baskerville Old Face", CInt(cbFontSizeCourse.Text), FontStyle.Regular)) ' Replace "Arial" and 12 with your desired font and size
                        textObject2.ApplyFont(New Font("Arial Black", CInt(cbFontSizeStudentName.Text), FontStyle.Bold Or FontStyle.Underline)) ' Replace "Arial" and 12 with your desired font and size
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                        recordRequest()
                    Catch ex As Exception
                        MsgBox(ex.Message & " Graduated date is not set or is not in the correct Format.", vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try
                Case "NSTP Serial Number Certification"
                    Try
                        NextBtn()
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New NSTPCertificate
                        cn.Close()
                        Dim studentCivilStatus As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentCivilStatus = cm.ExecuteScalar
                        cn.Close()
                        If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Ms")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mr")
                        ElseIf studentGender = "Female" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                        End If

                        If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                        ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                        Else
                            rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                        End If

                        If studentGender = "Female" Then
                            rptdoc.SetParameterValue("heshe", "She")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("heshe", "He")
                        End If

                        If cbLast.Checked = True Then
                            rptdoc.SetParameterValue("lastcurrent", "last")
                            rptdoc.SetParameterValue("iswas", "was")
                        ElseIf cbCurrent.Checked = True Then
                            rptdoc.SetParameterValue("lastcurrent", "this")
                            rptdoc.SetParameterValue("iswas", "is")
                        End If

                        Dim fnlow As String = studentFName.ToLower
                        Dim fn As String
                        fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                        Dim lnlow As String = studentLName.ToLower
                        Dim ln As String
                        ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                        Dim mnlow As String = studentMName.ToLower
                        Dim mn As String
                        mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                        rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)

                        Try
                            Dim scourselow As String = studentGradeLevelCourseName.ToLower
                            Dim scourse2 As String
                            scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                            Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                            Dim scourse As String = scourse3.Replace(" Of ", " of ")
                            rptdoc.SetParameterValue("course", scourse)
                        Catch ex As Exception
                            rptdoc.SetParameterValue("course", "")
                            MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            cbAcademicYear.Select()
                            PrevBtn()
                            Return
                        End Try

                        Try
                            Dim syearlvl As String
                            syearlvl = studentGradeLevel
                            Dim styear As String
                            styear = syearlvl.Substring(0, 3)
                            If styear = "1st" Then
                                rptdoc.SetParameterValue("yearlevel", "first year")
                            ElseIf styear = "2nd" Then
                                rptdoc.SetParameterValue("yearlevel", "second year")
                            ElseIf styear = "3rd" Then
                                rptdoc.SetParameterValue("yearlevel", "third year")
                            ElseIf styear = "4th" Then
                                rptdoc.SetParameterValue("yearlevel", "fourth year")
                            ElseIf styear = "5th" Then
                                rptdoc.SetParameterValue("yearlevel", "fifth year")
                            End If
                        Catch ex As Exception
                            rptdoc.SetParameterValue("yearlevel", "")
                            MsgBox("Select the academic year the student is enrolled or graduated to indicate his/her year level.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            cbAcademicYear.Select()
                            PrevBtn()
                            Return
                        End Try

                        rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                        cn.Close()
                        Dim periodName As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        periodName = cm.ExecuteScalar
                        cn.Close()
                        rptdoc.SetParameterValue("semesteryear", periodName)

                        rptdoc.SetParameterValue("lastname", ln)

                        Dim odate As String
                        odate = Format(Convert.ToDateTime(DateToday), "dd")
                        If odate = "01" Or odate = "21" Or odate = "31" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "st")
                        ElseIf odate = "02" Or odate = "22" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "nd")
                        ElseIf odate = "03" Or odate = "23" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "rd")
                        ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "th")
                        End If

                        rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))

                        cn.Close()
                        Dim studentNSTP As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT s_nstp_no FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentNSTP = cm.ExecuteScalar
                        cn.Close()


                        rptdoc.SetParameterValue("nstp_serial", studentNSTP)

                        If cbRegSign.Checked = True Then
                            rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                        Else
                            rptdoc.SetParameterValue("emp_sign_pic", "0")
                        End If

                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                        recordRequest()
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try
                Case "Certification of Total Number of Units Earned"

                    cn.Close()
                    Dim totalunits As Integer = 0
                    cn.Open()
                    cm = New MySqlCommand("SELECT SUM(sg_credits) as credits from tbl_students_grades where sg_student_id = " & studentId & " and (sg_grade_status = 'Enrolled' or sg_school_id = 0)", cn)
                    totalunits = cm.ExecuteScalar
                    cn.Close()
                    If cbGrad.Checked = True Then
                        Try
                            NextBtn()
                            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                            rptdoc = New UnitsEarned_Grad

                            cn.Close()
                            Dim studentCivilStatus As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                            studentCivilStatus = cm.ExecuteScalar
                            cn.Close()
                            If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Ms")
                            ElseIf studentGender = "Male" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Mr")
                            ElseIf studentGender = "Female" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                            End If

                            If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                            ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                            Else
                                rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                            End If

                            If studentGender = "Female" Then
                                rptdoc.SetParameterValue("heshe", "she")
                            ElseIf studentGender = "Male" Then
                                rptdoc.SetParameterValue("heshe", "he")
                            End If

                            Dim fnlow As String = studentFName.ToLower
                            Dim fn As String
                            fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                            Dim lnlow As String = studentLName.ToLower
                            Dim ln As String
                            ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                            Dim mnlow As String = studentMName.ToLower
                            Dim mn As String
                            mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                            rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)

                            Try
                                Dim scourselow As String = studentGradeLevelCourseName.ToLower
                                Dim scourse2 As String
                                scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                                Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                                Dim scourse As String = scourse3.Replace(" Of ", " of ")
                                rptdoc.SetParameterValue("course", scourse)
                            Catch ex As Exception
                                rptdoc.SetParameterValue("course", "")
                                MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                                ReportViewer.ReportSource = Nothing
                                cbAcademicYear.Select()
                                PrevBtn()
                                Return
                            End Try

                            rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                            rptdoc.SetParameterValue("lastname", ln)

                            Dim odate As String
                            odate = Format(Convert.ToDateTime(DateToday), "dd")

                            If odate = "01" Or odate = "21" Or odate = "31" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "st")
                            ElseIf odate = "02" Or odate = "22" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "nd")
                            ElseIf odate = "03" Or odate = "23" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "rd")
                            ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "th")
                            End If
                            rptdoc.SetParameterValue("units", totalunits)

                            Dim gradDate As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(s_grad_date,'%M %d %Y'), '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                            gradDate = cm.ExecuteScalar
                            cn.Close()

                            Dim beginDate As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT DATE_FORMAT(s_begin_date, '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                            beginDate = cm.ExecuteScalar
                            cn.Close()

                            rptdoc.SetParameterValue("datestart", Format(Convert.ToDateTime(beginDate), "MMMM, yyyy"))
                            If cbGrad.Checked = True Then
                                rptdoc.SetParameterValue("dateend", Format(Convert.ToDateTime(gradDate), "MMMM, yyyy"))
                            Else
                                rptdoc.SetParameterValue("dateend", Format(Convert.ToDateTime(gradDate), ""))
                            End If

                            If cbRegSign.Checked = True Then
                                rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                            Else
                                rptdoc.SetParameterValue("emp_sign_pic", "0")
                            End If
                            rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))
                            ReportViewer.ReportSource = rptdoc
                            dg_report.DataSource = Nothing
                            ReportViewer.Select()
                            ReportGenerated = True
                            recordRequest()
                        Catch ex As Exception
                            MsgBox(ex.Message & " Graduated date is not set or is not in the correct Format.", vbCritical)
                            cn.Close()
                            PrevBtn()
                        End Try
                    Else
                        Try
                            NextBtn()
                            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                            rptdoc = New UnitsEarned_UnderGrad
                            cn.Close()
                            Dim studentCivilStatus As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                            studentCivilStatus = cm.ExecuteScalar
                            cn.Close()
                            If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Ms")
                            ElseIf studentGender = "Male" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Mr")
                            ElseIf studentGender = "Female" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                            End If

                            If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                            ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                            Else
                                rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                            End If

                            If studentGender = "Female" Then
                                rptdoc.SetParameterValue("heshe", "she")
                            ElseIf studentGender = "Male" Then
                                rptdoc.SetParameterValue("heshe", "he")
                            End If

                            Dim fnlow As String = studentFName.ToLower
                            Dim fn As String
                            fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                            Dim lnlow As String = studentLName.ToLower
                            Dim ln As String
                            ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                            Dim mnlow As String = studentMName.ToLower
                            Dim mn As String
                            mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                            rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)

                            Try
                                Dim syearlvl As String
                                syearlvl = studentGradeLevel
                                Dim styear As String
                                styear = syearlvl.Substring(0, 3)
                                If styear = "1st" Then
                                    rptdoc.SetParameterValue("yearlevel", "first year")
                                ElseIf styear = "2nd" Then
                                    rptdoc.SetParameterValue("yearlevel", "second year")
                                ElseIf styear = "3rd" Then
                                    rptdoc.SetParameterValue("yearlevel", "third year")
                                ElseIf styear = "4th" Then
                                    rptdoc.SetParameterValue("yearlevel", "fourth year")
                                ElseIf styear = "5th" Then
                                    rptdoc.SetParameterValue("yearlevel", "fifth year")
                                End If
                            Catch ex As Exception
                                rptdoc.SetParameterValue("yearlevel", "")
                                MsgBox("Select the academic year the student is enrolled or graduated to indicate his/her year level.", vbCritical)
                                ReportViewer.ReportSource = Nothing
                                cbAcademicYear.Select()
                                PrevBtn()
                                Return
                            End Try


                            cn.Close()
                            Dim periodSemester As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT period_semester FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodSemester = cm.ExecuteScalar
                            cn.Close()

                            Dim syear As String
                            syear = periodSemester
                            Dim semyear As String
                            semyear = syear.Substring(0, 3)
                            If semyear = "1st" Then
                                rptdoc.SetParameterValue("semester", "First Semester")
                            ElseIf semyear = "2nd" Then
                                rptdoc.SetParameterValue("semester", "Second Semester")
                            Else
                                rptdoc.SetParameterValue("semester", "Summer")
                            End If

                            If cbLast.Checked = True Then
                                rptdoc.SetParameterValue("lastcurrent", "last")
                                rptdoc.SetParameterValue("iswas", "was")
                            ElseIf cbCurrent.Checked = True Then
                                rptdoc.SetParameterValue("lastcurrent", "this")
                                rptdoc.SetParameterValue("iswas", "is")
                            End If


                            Try
                                Dim scourselow As String = studentGradeLevelCourseName.ToLower
                                Dim scourse2 As String
                                scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                                Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                                Dim scourse As String = scourse3.Replace(" Of ", " of ")
                                rptdoc.SetParameterValue("course", scourse)
                            Catch ex As Exception
                                rptdoc.SetParameterValue("course", "")
                                MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                                ReportViewer.ReportSource = Nothing
                                cbAcademicYear.Select()
                                PrevBtn()
                                Return
                            End Try

                            rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                            cn.Close()
                            Dim periodName As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodName = cm.ExecuteScalar
                            cn.Close()
                            rptdoc.SetParameterValue("semesteryear", periodName)

                            rptdoc.SetParameterValue("lastname", ln)

                            Dim odate As String
                            odate = Format(Convert.ToDateTime(DateToday), "dd")

                            If odate = "01" Or odate = "21" Or odate = "31" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "st")
                            ElseIf odate = "02" Or odate = "22" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "nd")
                            ElseIf odate = "03" Or odate = "23" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "rd")
                            ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "th")
                            End If
                            rptdoc.SetParameterValue("units", totalunits)
                            Dim gradDate As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(s_grad_date,'%M %d %Y'), '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                            gradDate = cm.ExecuteScalar
                            cn.Close()

                            Dim beginDate As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT DATE_FORMAT(s_begin_date, '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                            beginDate = cm.ExecuteScalar
                            cn.Close()

                            rptdoc.SetParameterValue("datestart", Format(Convert.ToDateTime(beginDate), "MMMM, yyyy"))
                            If cbGrad.Checked = True Then
                                rptdoc.SetParameterValue("dateend", Format(Convert.ToDateTime(gradDate), "MMMM, yyyy"))
                            Else
                                rptdoc.SetParameterValue("dateend", Format(Convert.ToDateTime(gradDate), ""))
                            End If

                            If cbRegSign.Checked = True Then
                                rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                            Else
                                rptdoc.SetParameterValue("emp_sign_pic", "0")
                            End If
                            rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))
                            ReportViewer.ReportSource = rptdoc
                            dg_report.DataSource = Nothing
                            ReportViewer.Select()
                            ReportGenerated = True
                            recordRequest()
                        Catch ex As Exception
                            MsgBox(ex.Message & " Graduated date is not set or is not in the correct Format.", vbCritical)
                            cn.Close()
                            PrevBtn()
                        End Try
                    End If
                Case "Graduation Certificate"
                    Try
                        NextBtn()
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New GraduationCertificate
                        cn.Close()
                        Dim studentCivilStatus As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentCivilStatus = cm.ExecuteScalar
                        cn.Close()
                        If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Ms")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mr")
                        ElseIf studentGender = "Female" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                        End If

                        If studentGender = "Female" Then
                            rptdoc.SetParameterValue("herhis", "her")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("herhis", "him")
                        End If

                        If studentGender = "Female" Then
                            rptdoc.SetParameterValue("heshe", "she")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("heshe", "he")
                        End If

                        Dim fnlow As String = studentFName.ToLower
                        Dim fn As String
                        fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                        Dim lnlow As String = studentLName.ToLower
                        Dim ln As String
                        ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                        Dim mnlow As String = studentMName.ToLower
                        Dim mn As String
                        mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                        rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)

                        If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                        ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                        Else
                            rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                        End If

                        Try
                            Dim scourselow As String = studentGradeLevelCourseName.ToLower
                            Dim scourse2 As String
                            scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                            Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                            Dim scourse As String = scourse3.Replace(" Of ", " of ")
                            rptdoc.SetParameterValue("course", scourse)
                        Catch ex As Exception
                            rptdoc.SetParameterValue("course", "")
                            MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            cbAcademicYear.Select()
                            PrevBtn()
                            Return
                        End Try

                        rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                        rptdoc.SetParameterValue("lastname", ln)

                        Dim odate As String
                        odate = Format(Convert.ToDateTime(DateToday), "dd")

                        If odate = "01" Or odate = "21" Or odate = "31" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "st")
                        ElseIf odate = "02" Or odate = "22" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "nd")
                        ElseIf odate = "03" Or odate = "23" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "rd")
                        ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "th")
                        End If

                        Dim gradDate As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(s_grad_date,'%M %d %Y'), '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        gradDate = cm.ExecuteScalar
                        cn.Close()

                        rptdoc.SetParameterValue("ddate", Format(Convert.ToDateTime(gradDate), "MMMM dd, yyyy"))
                        rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))
                        If cbRegSign.Checked = True Then
                            rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                        Else
                            rptdoc.SetParameterValue("emp_sign_pic", "0")
                        End If
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                        recordRequest()
                    Catch ex As Exception
                        MsgBox(ex.Message & " Graduated date is not set or is not in the correct Format.", vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try
                Case "Graduating Certificate"
                    Try
                        NextBtn()
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New GraduatingCertificate

                        cn.Close()
                        Dim studentCivilStatus As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentCivilStatus = cm.ExecuteScalar
                        cn.Close()
                        If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Ms")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mr")
                        ElseIf studentGender = "Female" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                        End If

                        If studentGender = "Female" Then
                            rptdoc.SetParameterValue("herhis", "her")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("herhis", "him")
                        End If

                        If studentGender = "Female" Then
                            rptdoc.SetParameterValue("heshe", "she")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("heshe", "he")
                        End If

                        Dim fnlow As String = studentFName.ToLower
                        Dim fn As String
                        fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                        Dim lnlow As String = studentLName.ToLower
                        Dim ln As String
                        ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                        Dim mnlow As String = studentMName.ToLower
                        Dim mn As String
                        mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                        rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)

                        Try
                            Dim scourselow As String = studentGradeLevelCourseName.ToLower
                            Dim scourse2 As String
                            scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                            Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                            Dim scourse As String = scourse3.Replace(" Of ", " of ")
                            rptdoc.SetParameterValue("course", scourse)
                        Catch ex As Exception
                            rptdoc.SetParameterValue("course", "")
                            MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            cbAcademicYear.Select()
                            PrevBtn()
                            Return
                        End Try

                        rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                        rptdoc.SetParameterValue("lastname", ln)

                        Dim odate As String
                        odate = Format(Convert.ToDateTime(DateToday), "dd")

                        If odate = "01" Or odate = "21" Or odate = "31" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "st")
                        ElseIf odate = "02" Or odate = "22" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "nd")
                        ElseIf odate = "03" Or odate = "23" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "rd")
                        ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "th")
                        End If
                        cn.Close()
                        Dim gradDate As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(s_grad_date,'%M %d %Y'), '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        gradDate = cm.ExecuteScalar
                        cn.Close()

                        rptdoc.SetParameterValue("ddate", Format(Convert.ToDateTime(gradDate), "MMMM dd, yyyy"))

                        cn.Close()
                        Dim periodName As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        periodName = cm.ExecuteScalar
                        cn.Close()

                        rptdoc.SetParameterValue("acad_year", periodName)
                        rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))
                        If cbRegSign.Checked = True Then
                            rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                        Else
                            rptdoc.SetParameterValue("emp_sign_pic", "0")
                        End If
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                        recordRequest()
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try
                Case "General Weighted Average"
                    Try
                        NextBtn()
                        cn.Close()
                        Dim avg As String = ""
                        cn.Open()
                        cm = New MySqlCommand("select COALESCE(ROUND(avg(`sg_grade`),1),0) from tbl_students_grades where `sg_student_id` = '" & studentId & "' and sg_grade_status = 'Enrolled'", cn)
                        avg = cm.ExecuteScalar
                        cn.Close()

                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New GWA

                        cn.Close()
                        Dim studentCivilStatus As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentCivilStatus = cm.ExecuteScalar
                        cn.Close()
                        If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Ms")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mr")
                        ElseIf studentGender = "Female" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                        End If

                        If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                        ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                        Else
                            rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                        End If

                        If studentGender = "Female" Then
                            rptdoc.SetParameterValue("heshe", "she")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("heshe", "he")
                        End If


                        Dim fnlow As String = studentFName.ToLower
                        Dim fn As String
                        fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                        Dim lnlow As String = studentLName.ToLower
                        Dim ln As String
                        ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                        Dim mnlow As String = studentMName.ToLower
                        Dim mn As String
                        mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                        rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)

                        Try
                            Dim scourselow As String = studentGradeLevelCourseName.ToLower
                            Dim scourse2 As String
                            scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                            Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                            Dim scourse As String = scourse3.Replace(" Of ", " of ")
                            rptdoc.SetParameterValue("course", scourse)
                        Catch ex As Exception
                            rptdoc.SetParameterValue("course", "")
                            MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            cbAcademicYear.Select()
                            PrevBtn()
                            Return
                        End Try

                        rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                        rptdoc.SetParameterValue("lastname", ln)

                        Dim odate As String
                        odate = Format(Convert.ToDateTime(DateToday), "dd")

                        If odate = "01" Or odate = "21" Or odate = "31" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "st")
                        ElseIf odate = "02" Or odate = "22" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "nd")
                        ElseIf odate = "03" Or odate = "23" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "rd")
                        ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "th")
                        End If

                        cn.Close()
                        Dim gradDate As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(s_grad_date,'%M %d %Y'), '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        gradDate = cm.ExecuteScalar
                        cn.Close()
                        rptdoc.SetParameterValue("ddate", Format(Convert.ToDateTime(gradDate), "MMMM dd, yyyy"))
                        rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))

                        rptdoc.SetParameterValue("average", avg)
                        Dim average As Decimal
                        average = avg
                        If average = 1.1 Then
                            rptdoc.SetParameterValue("percent", "94% and above")
                        ElseIf average = 1.2 Then
                            rptdoc.SetParameterValue("percent", "93%")
                        ElseIf average = 1.3 Then
                            rptdoc.SetParameterValue("percent", "92%")
                        ElseIf average = 1.4 Then
                            rptdoc.SetParameterValue("percent", "91%")
                        ElseIf average = 1.5 Then
                            rptdoc.SetParameterValue("percent", "90%")
                        ElseIf average = 1.6 Then
                            rptdoc.SetParameterValue("percent", "89%")
                        ElseIf average = 1.7 Then
                            rptdoc.SetParameterValue("percent", "88%")
                        ElseIf average = 1.8 Then
                            rptdoc.SetParameterValue("percent", "87%")
                        ElseIf average = 1.9 Then
                            rptdoc.SetParameterValue("percent", "86%")
                        ElseIf average = 2.0 Then
                            rptdoc.SetParameterValue("percent", "85%")
                        ElseIf average = 2.1 Then
                            rptdoc.SetParameterValue("percent", "84%")
                        ElseIf average = 2.2 Then
                            rptdoc.SetParameterValue("percent", "83%")
                        ElseIf average = 2.3 Then
                            rptdoc.SetParameterValue("percent", "82%")
                        ElseIf average = 2.4 Then
                            rptdoc.SetParameterValue("percent", "81%")
                        ElseIf average = 2.5 Then
                            rptdoc.SetParameterValue("percent", "80%")
                        ElseIf average = 2.6 Then
                            rptdoc.SetParameterValue("percent", "79%")
                        ElseIf average = 2.7 Then
                            rptdoc.SetParameterValue("percent", "78%")
                        ElseIf average = 2.8 Then
                            rptdoc.SetParameterValue("percent", "77%")
                        ElseIf average = 2.9 Then
                            rptdoc.SetParameterValue("percent", "76%")
                        ElseIf average = 3.0 Then
                            rptdoc.SetParameterValue("percent", "75%")
                        ElseIf average > 3.0 Then
                            rptdoc.SetParameterValue("percent", "74% and below")
                        ElseIf average < 1 Then
                            rptdoc.SetParameterValue("percent", "0%")
                        End If
                        If cbRegSign.Checked = True Then
                            rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                        Else
                            rptdoc.SetParameterValue("emp_sign_pic", "0")
                        End If
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                        recordRequest()
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try
                Case "Grade Certificate"
                    Try
                        NextBtn()
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("select * from tbl_students_grades where sg_student_id = '" & studentId & "' and sg_period_id = '" & CInt(cbAcademicYear.SelectedValue) & "' and sg_grade_status = 'Enrolled'", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            cn.Close()
                            Dim avg As String = ""
                            cn.Open()
                            cm = New MySqlCommand("select COALESCE(ROUND(avg(`sg_grade`),1),0) from tbl_students_grades where `sg_student_id` = '" & studentId & "' and sg_grade_status = 'Enrolled'", cn)
                            avg = cm.ExecuteScalar
                            cn.Close()
                            cn.Open()
                            Dim dtable As DataTable
                            Dim adt As New MySqlDataAdapter
                            Dim dbcommand As New MySqlCommand("select CONCAT(PERIOD,' / ', course_name) as 'ACADEMIC YEAR', (subject_code) as 'CODE', (subject_description) as 'DESCRIPTION', sg_credits as 'CREDIT', if(sg_grade REGEXP '^-?[0-9]+$' >  0 and sg_grade < 6 and sg_school_id = '0' , ROUND(sg_grade,1), sg_grade) as 'GRADE', (CASE WHEN FORMAT(sg_grade,1) BETWEEN 1.1 AND 3 THEN 'PASSED' WHEN sg_grade = 'D' THEN 'DROPPED' ELSE 'FAILED' END) as 'REMARKS' from tbl_students_grades, tbl_subject, period, tbl_course where tbl_students_grades.sg_subject_id = tbl_subject.subject_id and tbl_students_grades.sg_period_id = period.period_id and tbl_students_grades.sg_course_id = tbl_course.course_id and sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_grade_status = 'Enrolled'", cn)
                            adt.SelectCommand = dbcommand
                            dtable = New DataTable
                            adt.Fill(dtable)
                            dg_report.DataSource = dtable
                            adt.Dispose()
                            dbcommand.Dispose()
                            cn.Close()

                            dt.Columns.Clear()
                            dt.Rows.Clear()
                            With dt
                                .Columns.Add("tor_code")
                                .Columns.Add("tor_description")
                                .Columns.Add("tor_grades")
                                .Columns.Add("tor_credit")
                                .Columns.Add("tor_pass_fail")
                            End With

                            For Each dr As DataGridViewRow In dg_report.Rows
                                dt.Rows.Add(dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value)
                            Next
                            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                            rptdoc = New GradeCertificate
                            rptdoc.SetDataSource(dt)

                            cn.Close()
                            Dim studentCivilStatus As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                            studentCivilStatus = cm.ExecuteScalar
                            cn.Close()
                            If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Ms")
                            ElseIf studentGender = "Male" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Mr")
                            ElseIf studentGender = "Female" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                            End If

                            If studentGender = "Female" Then
                                rptdoc.SetParameterValue("heshe", "she")
                            ElseIf studentGender = "Male" Then
                                rptdoc.SetParameterValue("heshe", "he")
                            End If

                            If cbLast.Checked = True Then
                                rptdoc.SetParameterValue("lastcurrent", "last")
                                rptdoc.SetParameterValue("iswas", "was")
                            ElseIf cbCurrent.Checked = True Then
                                rptdoc.SetParameterValue("lastcurrent", "this")
                                rptdoc.SetParameterValue("iswas", "is")
                            End If

                            Dim fnlow As String = studentFName.ToLower
                            Dim fn As String
                            fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                            Dim lnlow As String = studentLName.ToLower
                            Dim ln As String
                            ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                            Dim mnlow As String = studentMName.ToLower
                            Dim mn As String
                            mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                            rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)

                            Try
                                Dim scourselow As String = studentGradeLevelCourseName.ToLower
                                Dim scourse2 As String
                                scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                                Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                                Dim scourse As String = scourse3.Replace(" Of ", " of ")
                                rptdoc.SetParameterValue("course", scourse)
                            Catch ex As Exception
                                rptdoc.SetParameterValue("course", "")
                                MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                                ReportViewer.ReportSource = Nothing
                                cbAcademicYear.Select()
                                PrevBtn()
                                Return
                            End Try

                            cn.Close()
                            Dim periodName As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodName = cm.ExecuteScalar
                            cn.Close()
                            rptdoc.SetParameterValue("semesteryear", periodName)
                            rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                            cn.Close()
                            Dim periodsemester As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT `period_semester` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodsemester = cm.ExecuteScalar
                            cn.Close()
                            Dim semyear As String
                            semyear = periodsemester.Substring(0, 3)
                            If semyear = "1st" Then
                                rptdoc.SetParameterValue("semester", "First Semester")
                            ElseIf semyear = "2nd" Then
                                rptdoc.SetParameterValue("semester", "Second Semester")
                            Else
                                rptdoc.SetParameterValue("semester", "Summer")
                            End If

                            If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                            ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                            Else
                                rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                            End If

                            rptdoc.SetParameterValue("lastname", ln)

                            Dim odate As String
                            odate = Format(Convert.ToDateTime(DateToday), "dd")

                            If odate = "01" Or odate = "21" Or odate = "31" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "st")
                            ElseIf odate = "02" Or odate = "22" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "nd")
                            ElseIf odate = "03" Or odate = "23" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "rd")
                            ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "th")
                            End If

                            rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))

                            rptdoc.SetParameterValue("average", avg)
                            Dim average As Decimal
                            average = avg
                            If average = 1.1 Then
                                rptdoc.SetParameterValue("percent", "94% and above")
                            ElseIf average = 1.2 Then
                                rptdoc.SetParameterValue("percent", "93%")
                            ElseIf average = 1.3 Then
                                rptdoc.SetParameterValue("percent", "92%")
                            ElseIf average = 1.4 Then
                                rptdoc.SetParameterValue("percent", "91%")
                            ElseIf average = 1.5 Then
                                rptdoc.SetParameterValue("percent", "90%")
                            ElseIf average = 1.6 Then
                                rptdoc.SetParameterValue("percent", "89%")
                            ElseIf average = 1.7 Then
                                rptdoc.SetParameterValue("percent", "88%")
                            ElseIf average = 1.8 Then
                                rptdoc.SetParameterValue("percent", "87%")
                            ElseIf average = 1.9 Then
                                rptdoc.SetParameterValue("percent", "86%")
                            ElseIf average = 2.0 Then
                                rptdoc.SetParameterValue("percent", "85%")
                            ElseIf average = 2.1 Then
                                rptdoc.SetParameterValue("percent", "84%")
                            ElseIf average = 2.2 Then
                                rptdoc.SetParameterValue("percent", "83%")
                            ElseIf average = 2.3 Then
                                rptdoc.SetParameterValue("percent", "82%")
                            ElseIf average = 2.4 Then
                                rptdoc.SetParameterValue("percent", "81%")
                            ElseIf average = 2.5 Then
                                rptdoc.SetParameterValue("percent", "80%")
                            ElseIf average = 2.6 Then
                                rptdoc.SetParameterValue("percent", "79%")
                            ElseIf average = 2.7 Then
                                rptdoc.SetParameterValue("percent", "78%")
                            ElseIf average = 2.8 Then
                                rptdoc.SetParameterValue("percent", "77%")
                            ElseIf average = 2.9 Then
                                rptdoc.SetParameterValue("percent", "76%")
                            ElseIf average = 3.0 Then
                                rptdoc.SetParameterValue("percent", "75%")
                            ElseIf average > 3.0 Then
                                rptdoc.SetParameterValue("percent", "74% and below")
                            ElseIf average < 1 Then
                                rptdoc.SetParameterValue("percent", "0%")
                            End If

                            Try
                                If studentGradeLevel = "Transferee" Then
                                    MessageBox.Show("Input year level as: first year, second year, third year, fourth year, fifth year.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                ElseIf studentGradeLevel.Contains("1st Year") Or studentGradeLevel.Contains("2nd Year") Or studentGradeLevel.Contains("3rd Year") Or studentGradeLevel.Contains("4th Year") Or studentGradeLevel.Contains("5th Year") Then

                                    Dim syearlvl As String
                                    syearlvl = studentGradeLevel
                                    Dim styear As String
                                    styear = syearlvl.Substring(0, 3)
                                    If styear = "1st" Then
                                        rptdoc.SetParameterValue("yearlevel", "first year")
                                    ElseIf styear = "2nd" Then
                                        rptdoc.SetParameterValue("yearlevel", "second year")
                                    ElseIf styear = "3rd" Then
                                        rptdoc.SetParameterValue("yearlevel", "third year")
                                    ElseIf styear = "4th" Then
                                        rptdoc.SetParameterValue("yearlevel", "fourth year")
                                    ElseIf styear = "5th" Then
                                        rptdoc.SetParameterValue("yearlevel", "fifth year")
                                    End If
                                Else
                                    MessageBox.Show("Student record not found on this Academic Year.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If

                            Catch ex As Exception
                                rptdoc.SetParameterValue("yearlevel", "")
                                MsgBox("Select the academic year the student is enrolled or graduated to indicate his/her year level.", vbCritical)
                                ReportViewer.ReportSource = Nothing
                                cbAcademicYear.Select()
                                PrevBtn()
                                Return
                            End Try

                            If cbRegSign.Checked = True Then
                                rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                            Else
                                rptdoc.SetParameterValue("emp_sign_pic", "0")
                            End If
                            ReportViewer.ReportSource = rptdoc
                            dg_report.DataSource = Nothing
                            ReportViewer.Select()
                            ReportGenerated = True
                            recordRequest()
                        Else
                            MsgBox("Student record not found on this Academic Year.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            PrevBtn()
                            Return
                        End If
                        dr.Close()
                        cn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try
                Case "Enrollment Certificate"
                    Try
                        NextBtn()
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("select * from tbl_students_grades where sg_student_id = '" & studentId & "' and sg_period_id = '" & CInt(cbAcademicYear.SelectedValue) & "' and sg_grade_status = 'Enrolled'", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            cn.Close()
                            Dim avg As String = ""
                            cn.Open()
                            cm = New MySqlCommand("select COALESCE(ROUND(avg(`sg_grade`),1),0) from tbl_students_grades where `sg_student_id` = '" & studentId & "' and sg_grade_status = 'Enrolled'", cn)
                            avg = cm.ExecuteScalar
                            cn.Close()
                            cn.Open()
                            Dim dtable As DataTable
                            Dim adt As New MySqlDataAdapter
                            Dim dbcommand As New MySqlCommand("Select (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (subject_units) as 'Units' from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_enrollment, tbl_students_grades, tbl_user_account where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_students_grades.sg_class_id and tbl_enrollment.estudent_id = tbl_students_grades.sg_student_id and tbl_class_schedule.csperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eenrolledby_id = tbl_user_account.ua_id and tbl_enrollment.estudent_id = '" & studentId & "' and tbl_enrollment.eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_grade_status = 'Enrolled'", cn)
                            adt.SelectCommand = dbcommand
                            dtable = New DataTable
                            adt.Fill(dtable)
                            dg_report.DataSource = dtable
                            adt.Dispose()
                            dbcommand.Dispose()
                            cn.Close()

                            dt.Columns.Clear()
                            dt.Rows.Clear()
                            With dt
                                .Columns.Add("tor_cs_class")
                                .Columns.Add("tor_code")
                                .Columns.Add("tor_description")
                                .Columns.Add("tor_cs_day")
                                .Columns.Add("tor_cs_timestart")
                                .Columns.Add("tor_cs_timeend")
                                .Columns.Add("tor_credit")
                            End With

                            For Each dr As DataGridViewRow In dg_report.Rows
                                dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value)
                            Next

                            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                            rptdoc = New EnrollmentCertificate
                            rptdoc.SetDataSource(dt)

                            cn.Close()
                            Dim studentCivilStatus As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                            studentCivilStatus = cm.ExecuteScalar
                            cn.Close()
                            If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Ms")
                            ElseIf studentGender = "Male" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Mr")
                            ElseIf studentGender = "Female" Then
                                rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                            End If

                            If studentGender = "Female" Then
                                rptdoc.SetParameterValue("heshe", "she")
                            ElseIf studentGender = "Male" Then
                                rptdoc.SetParameterValue("heshe", "he")
                            End If

                            If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                            ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                            Else
                                rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                            End If

                            If cbLast.Checked = True Then
                                rptdoc.SetParameterValue("lastcurrent", "last")
                                rptdoc.SetParameterValue("iswas", "was")
                            ElseIf cbCurrent.Checked = True Then
                                rptdoc.SetParameterValue("lastcurrent", "this")
                                rptdoc.SetParameterValue("iswas", "is")
                            End If

                            Dim fnlow As String = studentFName.ToLower
                            Dim fn As String
                            fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                            Dim lnlow As String = studentLName.ToLower
                            Dim ln As String
                            ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                            Dim mnlow As String = studentMName.ToLower
                            Dim mn As String
                            mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                            rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)

                            Try
                                Dim scourselow As String = studentGradeLevelCourseName.ToLower
                                Dim scourse2 As String
                                scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                                Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                                Dim scourse As String = scourse3.Replace(" Of ", " of ")
                                rptdoc.SetParameterValue("course", scourse)
                            Catch ex As Exception
                                rptdoc.SetParameterValue("course", "")
                                MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                                ReportViewer.ReportSource = Nothing
                                cbAcademicYear.Select()
                                PrevBtn()
                                Return
                            End Try

                            cn.Close()
                            Dim periodName As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodName = cm.ExecuteScalar
                            cn.Close()
                            rptdoc.SetParameterValue("semesteryear", periodName)
                            rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                            cn.Close()
                            Dim periodsemester As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT `period_semester` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodsemester = cm.ExecuteScalar
                            cn.Close()
                            Dim semyear As String
                            semyear = periodsemester.Substring(0, 3)
                            If semyear = "1st" Then
                                rptdoc.SetParameterValue("semester", "First Semester")
                            ElseIf semyear = "2nd" Then
                                rptdoc.SetParameterValue("semester", "Second Semester")
                            Else
                                rptdoc.SetParameterValue("semester", "Summer")
                            End If

                            rptdoc.SetParameterValue("lastname", ln)

                            Dim odate As String
                            odate = Format(Convert.ToDateTime(DateToday), "dd")

                            If odate = "01" Or odate = "21" Or odate = "31" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "st")
                            ElseIf odate = "02" Or odate = "22" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "nd")
                            ElseIf odate = "03" Or odate = "23" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "rd")
                            ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "th")
                            End If

                            rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))

                            rptdoc.SetParameterValue("average", avg)
                            Dim average As Decimal
                            average = avg
                            If average = 1.1 Then
                                rptdoc.SetParameterValue("percent", "94% and above")
                            ElseIf average = 1.2 Then
                                rptdoc.SetParameterValue("percent", "93%")
                            ElseIf average = 1.3 Then
                                rptdoc.SetParameterValue("percent", "92%")
                            ElseIf average = 1.4 Then
                                rptdoc.SetParameterValue("percent", "91%")
                            ElseIf average = 1.5 Then
                                rptdoc.SetParameterValue("percent", "90%")
                            ElseIf average = 1.6 Then
                                rptdoc.SetParameterValue("percent", "89%")
                            ElseIf average = 1.7 Then
                                rptdoc.SetParameterValue("percent", "88%")
                            ElseIf average = 1.8 Then
                                rptdoc.SetParameterValue("percent", "87%")
                            ElseIf average = 1.9 Then
                                rptdoc.SetParameterValue("percent", "86%")
                            ElseIf average = 2.0 Then
                                rptdoc.SetParameterValue("percent", "85%")
                            ElseIf average = 2.1 Then
                                rptdoc.SetParameterValue("percent", "84%")
                            ElseIf average = 2.2 Then
                                rptdoc.SetParameterValue("percent", "83%")
                            ElseIf average = 2.3 Then
                                rptdoc.SetParameterValue("percent", "82%")
                            ElseIf average = 2.4 Then
                                rptdoc.SetParameterValue("percent", "81%")
                            ElseIf average = 2.5 Then
                                rptdoc.SetParameterValue("percent", "80%")
                            ElseIf average = 2.6 Then
                                rptdoc.SetParameterValue("percent", "79%")
                            ElseIf average = 2.7 Then
                                rptdoc.SetParameterValue("percent", "78%")
                            ElseIf average = 2.8 Then
                                rptdoc.SetParameterValue("percent", "77%")
                            ElseIf average = 2.9 Then
                                rptdoc.SetParameterValue("percent", "76%")
                            ElseIf average = 3.0 Then
                                rptdoc.SetParameterValue("percent", "75%")
                            ElseIf average > 3.0 Then
                                rptdoc.SetParameterValue("percent", "74% and below")
                            ElseIf average < 1 Then
                                rptdoc.SetParameterValue("percent", "0%")
                            End If

                            Try
                                If studentGradeLevel = "Transferee" Then
                                    MessageBox.Show("Input year level as: first year, second year, third year, fourth year, fifth year.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                ElseIf studentGradeLevel.Contains("1st Year") Or studentGradeLevel.Contains("2nd Year") Or studentGradeLevel.Contains("3rd Year") Or studentGradeLevel.Contains("4th Year") Or studentGradeLevel.Contains("5th Year") Then

                                    Dim syearlvl As String
                                    syearlvl = studentGradeLevel
                                    Dim styear As String
                                    styear = syearlvl.Substring(0, 3)
                                    If styear = "1st" Then
                                        rptdoc.SetParameterValue("yearlevel", "first year")
                                    ElseIf styear = "2nd" Then
                                        rptdoc.SetParameterValue("yearlevel", "second year")
                                    ElseIf styear = "3rd" Then
                                        rptdoc.SetParameterValue("yearlevel", "third year")
                                    ElseIf styear = "4th" Then
                                        rptdoc.SetParameterValue("yearlevel", "fourth year")
                                    ElseIf styear = "5th" Then
                                        rptdoc.SetParameterValue("yearlevel", "fifth year")
                                    End If
                                Else
                                    MessageBox.Show("Student record not found on this Academic Year.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If

                            Catch ex As Exception
                                rptdoc.SetParameterValue("yearlevel", "")
                                MsgBox("Select the academic year the student is enrolled or graduated to indicate his/her year level.", vbCritical)
                                ReportViewer.ReportSource = Nothing
                                cbAcademicYear.Select()
                                PrevBtn()
                                Return
                            End Try

                            If cbRegSign.Checked = True Then
                                rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                            Else
                                rptdoc.SetParameterValue("emp_sign_pic", "0")
                            End If
                            ReportViewer.ReportSource = rptdoc
                            dg_report.DataSource = Nothing
                            ReportViewer.Select()
                            ReportGenerated = True
                            recordRequest()
                        Else
                            MsgBox("Student record not found on this Academic Year.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            PrevBtn()
                            Return
                        End If
                        dr.Close()
                        cn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try
                Case "Certificate of No Scholarship"
                    Try
                        NextBtn()
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New NoScholarshipCert
                        cn.Close()
                        Dim studentCivilStatus As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentCivilStatus = cm.ExecuteScalar
                        cn.Close()
                        If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Ms")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mr")
                        ElseIf studentGender = "Female" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                        End If

                        If studentGender = "Female" Then
                            rptdoc.SetParameterValue("heshe", "she")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("heshe", "he")
                        End If

                        If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                        ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                        Else
                            rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                        End If

                        Dim fnlow As String = studentFName.ToLower
                        Dim fn As String
                        fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                        Dim lnlow As String = studentLName.ToLower
                        Dim ln As String
                        ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                        Dim mnlow As String = studentMName.ToLower
                        Dim mn As String
                        mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)
                        rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)


                        Try
                            Dim scourselow As String = studentGradeLevelCourseName.ToLower
                            Dim scourse2 As String
                            scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                            Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                            Dim scourse As String = scourse3.Replace(" Of ", " of ")
                            rptdoc.SetParameterValue("course", scourse)
                        Catch ex As Exception
                            rptdoc.SetParameterValue("course", "")
                            MsgBox("Select the academic year corresponding to the student's graduation to indicate the completion of his/her degree or course.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            cbAcademicYear.Select()
                            PrevBtn()
                            Return
                        End Try

                        Try
                            Dim syearlvl As String
                            syearlvl = studentGradeLevel
                            Dim styear As String
                            styear = syearlvl.Substring(0, 3)
                            If styear = "1st" Then
                                rptdoc.SetParameterValue("yearlevel", "first year")
                            ElseIf styear = "2nd" Then
                                rptdoc.SetParameterValue("yearlevel", "second year")
                            ElseIf styear = "3rd" Then
                                rptdoc.SetParameterValue("yearlevel", "third year")
                            ElseIf styear = "4th" Then
                                rptdoc.SetParameterValue("yearlevel", "fourth year")
                            ElseIf styear = "5th" Then
                                rptdoc.SetParameterValue("yearlevel", "fifth year")
                            End If
                        Catch ex As Exception
                            rptdoc.SetParameterValue("yearlevel", "")
                            MsgBox("Select the academic year the student is enrolled or graduated to indicate his/her year level.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            cbAcademicYear.Select()
                            PrevBtn()
                            Return
                        End Try


                        cn.Close()
                        Dim periodName As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        periodName = cm.ExecuteScalar
                        cn.Close()
                        rptdoc.SetParameterValue("semesteryear", periodName)
                        rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                        cn.Close()
                        Dim periodsemester As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT `period_semester` FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        periodsemester = cm.ExecuteScalar
                        cn.Close()
                        Dim semyear As String
                        semyear = periodsemester.Substring(0, 3)
                        If semyear = "1st" Then
                            rptdoc.SetParameterValue("semester", "First Semester")
                        ElseIf semyear = "2nd" Then
                            rptdoc.SetParameterValue("semester", "Second Semester")
                        Else
                            rptdoc.SetParameterValue("semester", "Summer")
                        End If

                        If cbLast.Checked = True Then
                            rptdoc.SetParameterValue("lastcurrent", "last")
                            rptdoc.SetParameterValue("iswas", "was")
                        ElseIf cbCurrent.Checked = True Then
                            rptdoc.SetParameterValue("lastcurrent", "this")
                            rptdoc.SetParameterValue("iswas", "is")
                        End If

                        rptdoc.SetParameterValue("lastname", ln)

                        Dim odate As String
                        odate = Format(Convert.ToDateTime(DateToday), "dd")

                        If odate = "01" Or odate = "21" Or odate = "31" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "st")
                        ElseIf odate = "02" Or odate = "22" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "nd")
                        ElseIf odate = "03" Or odate = "23" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "rd")
                        ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "th")
                        End If
                        rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))
                        If cbRegSign.Checked = True Then
                            rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                        Else
                            rptdoc.SetParameterValue("emp_sign_pic", "0")
                        End If
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                        recordRequest()
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try
                Case "Certificate of English as Medium of Instruction"
                    Try
                        NextBtn()
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New CertEnglishMedium
                        cn.Close()
                        Dim studentCivilStatus As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentCivilStatus = cm.ExecuteScalar
                        cn.Close()
                        If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Ms")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mr")
                        ElseIf studentGender = "Female" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                        End If

                        If studentGender = "Female" Then
                            rptdoc.SetParameterValue("heshe", "she")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("heshe", "he")
                        End If

                        If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                        ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                        Else
                            rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                        End If

                        Dim fnlow As String = studentFName.ToLower
                        Dim fn As String
                        fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                        Dim lnlow As String = studentLName.ToLower
                        Dim ln As String
                        ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                        Dim mnlow As String = studentMName.ToLower
                        Dim mn As String
                        mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)
                        rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)


                        Try
                            Dim scourselow As String = studentGradeLevelCourseName.ToLower
                            Dim scourse2 As String
                            scourse2 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scourselow)
                            Dim scourse3 As String = scourse2.Replace(" In ", " in ")
                            Dim scourse As String = scourse3.Replace(" Of ", " of ")
                            rptdoc.SetParameterValue("course", scourse)
                        Catch ex As Exception
                            rptdoc.SetParameterValue("course", "")
                            MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                            ReportViewer.ReportSource = Nothing
                            cbAcademicYear.Select()
                            PrevBtn()
                            Return
                        End Try

                        rptdoc.SetParameterValue("registrar", cbRegistrar.Text)

                        rptdoc.SetParameterValue("lastname", ln)

                        Dim odate As String
                        odate = Format(Convert.ToDateTime(DateToday), "dd")

                        If odate = "01" Or odate = "21" Or odate = "31" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "st")
                        ElseIf odate = "02" Or odate = "22" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "nd")
                        ElseIf odate = "03" Or odate = "23" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "rd")
                        ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                            If odate.StartsWith("0") Then
                                rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                            Else
                                rptdoc.SetParameterValue("day", odate)
                            End If
                            rptdoc.SetParameterValue("rank", "th")
                        End If
                        rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))
                        rptdoc.SetParameterValue("datetoday", Format(Convert.ToDateTime(DateToday), "MMMM dd, yyyy"))

                        Dim gradDate As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT DATE_FORMAT(STR_TO_DATE(s_grad_date,'%M %d %Y'), '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        gradDate = cm.ExecuteScalar
                        cn.Close()

                        Dim beginDate As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT DATE_FORMAT(s_begin_date, '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        beginDate = cm.ExecuteScalar
                        cn.Close()

                        Dim birthDate As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT DATE_FORMAT(s_dob, '%Y/%m/%d') FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        beginDate = cm.ExecuteScalar
                        cn.Close()

                        rptdoc.SetParameterValue("datestarted", Format(Convert.ToDateTime(beginDate), "MMMM dd, yyyy"))
                        rptdoc.SetParameterValue("dategraduated", Format(Convert.ToDateTime(gradDate), "MMMM dd, yyyy"))
                        rptdoc.SetParameterValue("bday", Format(Convert.ToDateTime(birthDate), "MMMM dd, yyyy"))
                        If cbRegSign.Checked = True Then
                            rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                        Else
                            rptdoc.SetParameterValue("emp_sign_pic", "0")
                        End If
                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                        recordRequest()
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try
                Case "Notice of Release"
                    Try
                        NextBtn()
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New NoticeOfRelease
                        cn.Close()
                        Dim studentCivilStatus As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT s_civil_status FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                        studentCivilStatus = cm.ExecuteScalar
                        cn.Close()
                        If studentGender = "Female" AndAlso studentCivilStatus = "Single" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Ms")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mr")
                        ElseIf studentGender = "Female" Then
                            rptdoc.SetParameterValue("mrmsmrs", "Mrs")
                        End If

                        If studentGender = "Female" Then
                            rptdoc.SetParameterValue("herhis", "her")
                        ElseIf studentGender = "Male" Then
                            rptdoc.SetParameterValue("herhis", "his")
                        End If

                        Dim fnlow As String = studentFName.ToLower
                        Dim fn As String
                        fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                        Dim lnlow As String = studentLName.ToLower
                        Dim ln As String
                        ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                        Dim mnlow As String = studentMName.ToLower
                        Dim mn As String
                        mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)
                        rptdoc.SetParameterValue("studentname", fn & " " & mn & " " & ln)
                        rptdoc.SetParameterValue("lastname", ln)

                        rptdoc.SetParameterValue("ddate", Format(Convert.ToDateTime(DateToday), "MMMM dd, yyyy"))
                        If cbRegSign.Checked = True Then
                            rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                        Else
                            rptdoc.SetParameterValue("emp_sign_pic", "0")
                        End If
                        rptdoc.SetParameterValue("schoolname", txtSchool.Text)
                        rptdoc.SetParameterValue("schooladdress", SchoolAddress)
                        rptdoc.SetParameterValue("registrar", cbRegistrar.Text)
                        rptdoc.SetParameterValue("daterelease", Format(Convert.ToDateTime(dtNOR.Text), "MMMM dd, yyyy"))

                        If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                        ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                            rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                        Else
                            rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                        End If

                        ReportViewer.ReportSource = rptdoc
                        dg_report.DataSource = Nothing
                        ReportViewer.Select()
                        ReportGenerated = True
                        recordRequest()
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                        cn.Close()
                        PrevBtn()
                    End Try

                Case "Award Certificate"
                    If studentId = String.Empty Then
                        ReportViewer.ReportSource = Nothing
                        MsgBox("Please select Student.", vbCritical)
                        btnSearchStudent.Select()
                    ElseIf CInt(cbAcademicYear.SelectedValue) <= 0 Then
                        ReportViewer.ReportSource = Nothing
                        MsgBox("Please select Academic Year.", vbCritical)
                        cbAcademicYear.Select()
                    Else
                        Try
                            NextBtn()
                            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                            rptdoc = New AwardCertification

                            cn.Close()
                            Dim award As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT s_acad_awards FROM `tbl_student` WHERE `s_id_no` = '" & studentId & "'", cn)
                            award = cm.ExecuteScalar
                            cn.Close()
                            rptdoc.SetParameterValue("award", award)


                            If studentGender = "Female" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve her best")
                            ElseIf studentGender = "Male" AndAlso cbPurpose.Text = "Legal Purpose" Then
                                rptdoc.SetParameterValue("purpose", "whatever legal purpose it may serve him best")
                            Else
                                rptdoc.SetParameterValue("purpose", cbPurpose.Text)
                            End If

                            Dim fnlow As String = studentFName.ToLower
                            Dim fn As String
                            fn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fnlow)

                            Dim lnlow As String = studentLName.ToLower
                            Dim ln As String
                            ln = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lnlow)

                            Dim mnlow As String = studentMName.ToLower
                            Dim mn As String
                            mn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mnlow)

                            Dim studentName As String = fn & " " & mn & " " & ln
                            rptdoc.SetParameterValue("studentname", studentName.ToString.ToUpper)

                            Try
                                Dim syearlvl As String
                                syearlvl = studentGradeLevel

                                rptdoc.SetParameterValue("course", studentGradeLevelCourseName.ToString.ToUpper)

                                Dim periodName As String = ""
                                cn.Open()
                                cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                                periodName = cm.ExecuteScalar
                                cn.Close()
                                rptdoc.SetParameterValue("semesteryear", periodName)
                            Catch ex As Exception
                                rptdoc.SetParameterValue("course", "")
                                rptdoc.SetParameterValue("semesteryear", "")
                                MsgBox("Select the academic year the student is enrolled or graduated to indicate the degree or course completion.", vbCritical)
                                ReportViewer.ReportSource = Nothing
                                cbAcademicYear.Select()
                                PrevBtn()
                                Return
                            End Try

                            rptdoc.SetParameterValue("registrar", cbRegistrar.Text)
                            rptdoc.SetParameterValue("academicDirector", cbAcadCoor.Text)
                            rptdoc.SetParameterValue("president", cbPresident.Text)

                            Dim odate As String
                            odate = Format(Convert.ToDateTime(DateToday), "dd")
                            If odate = "01" Or odate = "21" Or odate = "31" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "st")
                            ElseIf odate = "02" Or odate = "22" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "nd")
                            ElseIf odate = "03" Or odate = "23" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "rd")
                            ElseIf odate = "04" Or odate = "05" Or odate = "06" Or odate = "07" Or odate = "08" Or odate = "09" Or odate = "10" Or odate = "11" Or odate = "12" Or odate = "13" Or odate = "14" Or odate = "15" Or odate = "16" Or odate = "17" Or odate = "18" Or odate = "19" Or odate = "20" Or odate = "24" Or odate = "25" Or odate = "26" Or odate = "27" Or odate = "28" Or odate = "29" Or odate = "30" Then
                                If odate.StartsWith("0") Then
                                    rptdoc.SetParameterValue("day", odate.Remove(0, 1))
                                Else
                                    rptdoc.SetParameterValue("day", odate)
                                End If
                                rptdoc.SetParameterValue("rank", "th")
                            End If

                            If cbRegSign.Checked = True Then
                                rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                            Else
                                rptdoc.SetParameterValue("emp_sign_pic", "0")
                            End If

                            rptdoc.SetParameterValue("monthyear", Format(Convert.ToDateTime(DateToday), "MMMM, yyyy"))
                            ReportViewer.ReportSource = rptdoc
                            dg_report.DataSource = Nothing
                            ReportViewer.Select()
                            ReportGenerated = True
                            recordRequest()
                        Catch ex As Exception
                            MsgBox(ex.Message, vbCritical)
                            cn.Close()
                            PrevBtn()
                        End Try
                    End If
            End Select


        ElseIf frmMain.formTitle.Text = "Generate NSTP Reports" Then
            If CInt(cbAcademicYear.SelectedValue) <= 0 Then
                ReportViewer.ReportSource = Nothing
                MsgBox("Please select Academic Year.", vbCritical)
                cbAcademicYear.Select()
            ElseIf cbAcademicYear.Text.Contains("2nd Semester") = False Then
                MsgBox("Academic Year Invalid for generating List Of NSTP Graduates for Searial Number. Must Select a 2nd Semester Academic Year.", vbCritical)
                cbAcademicYear.Select()
            Else
                Try
                    If cbNSTPEG.Checked = True Then
                        Try
                            cn.Close()
                            Dim periodName As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodName = cm.ExecuteScalar
                            cn.Close()

                            cn.Open()
                            Dim dtable As DataTable
                            Dim adt As New MySqlDataAdapter
                            Dim sql As String = ""
                            sql = "SELECT ROW_NUMBER() OVER (ORDER BY `Last Name` asc, `First Name` asc) AS 'No.', `Serial Number` as 'Serial No.', `Last Name` as 'Surname', `First Name`, `Middle Name`,`Main Program Name` as 'Course/Program', `Sex` as 'Gender', `Birthdate`, CONCAT(`Street/Bgry.`, ', ', `Town/City`) as 'City Address', `Province` as 'Provincial Address', `Telephone/CP Number` as 'Contact Number Telephone/Mobile', `Email Address`  FROM (select tbl_period.period_name as 'Award Year', 'NSTP/CWTS' as 'NSTP Program', '12-SOCCSKSARGEN' as 'REGION', (s_nstp_no) as 'Serial Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_ext) as 'Extension Name',  (s_mn) as 'Middle Name', DATE_FORMAT(s_dob, '%m/%d/%y') as 'Birthdate', (s_gender) as 'Sex', CONCAT(s_address, ', ', (refbrgy.brgyDesc)) as 'Street/Bgry.', (refcitymun.citymunDesc) as 'Town/City', (refprovince.provDesc) as 'Province', (sg_yearlevel) as 'Year Level', CONCAT(course_name, ' ',course_major) as 'Main Program Name', s_email as 'Email Address', s_contact as 'Telephone/CP Number' from tbl_students_grades JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id LEFT JOIN refbrgy ON tbl_student.s_address_brgy = refbrgy.brgyCode  LEFT JOIN refcitymun ON tbl_student.s_address_citymun = refcitymun.citymunCode LEFT JOIN refprovince ON tbl_student.s_address_prov = refprovince.provCode JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id where tbl_student.s_nstp_no NOT LIKE '%C-%' and (tbl_subject.subject_description like '%CWTS%' or tbl_subject.subject_description like '%ROTC%') and tbl_course.course_name not like '%CRIMINOLOGY%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' and tbl_students_grades.sg_grade between 1 and 3 order by tbl_student.s_ln asc, tbl_student.s_fn asc) as NSTP"
                            Dim dbcommand As New MySqlCommand(sql, cn)
                            adt.SelectCommand = dbcommand
                            dtable = New DataTable
                            adt.Fill(dtable)
                            dg_report.DataSource = dtable
                            adt.Dispose()
                            dbcommand.Dispose()
                            cn.Close()

                            cn.Open()
                            Dim dtable2 As DataTable
                            Dim adt2 As New MySqlDataAdapter
                            Dim sql2 As String = ""
                            sql2 = "select SUM(CASE WHEN `Sex` = 'Male' and `Main Program Name` like '%CRIMINOLOGY%' and `Award Semester` = '1st Semester' THEN 1 ELSE 0 END) AS 1stSemROTCMaleStudents,SUM(CASE WHEN `Sex` = 'Female' and `Main Program Name` like '%CRIMINOLOGY%' and `Award Semester` = '1st Semester' THEN 1 ELSE 0 END) AS 1stSemROTCFemaleStudents,SUM(CASE WHEN `Sex` = 'Male' and `Main Program Name` NOT like '%CRIMINOLOGY%' and `Award Semester` = '1st Semester' THEN 1 ELSE 0 END) AS 1stSemCWTSMaleStudents,SUM(CASE WHEN `Sex` = 'Female' and `Main Program Name` NOT like '%CRIMINOLOGY%' and `Award Semester` = '1st Semester' THEN 1 ELSE 0 END) AS 1stSemCWTSFemaleStudents,SUM(CASE WHEN `Sex` = 'Male' and `Main Program Name` like '%CRIMINOLOGY%' and `Award Semester` = '2nd Semester' THEN 1 ELSE 0 END) AS 2ndSemROTCMaleStudents,SUM(CASE WHEN `Sex` = 'Female' and `Main Program Name` like '%CRIMINOLOGY%' and `Award Semester` = '2nd Semester' THEN 1 ELSE 0 END) AS 2ndSemROTCFemaleStudents,SUM(CASE WHEN `Sex` = 'Male' and `Main Program Name` NOT like '%CRIMINOLOGY%' and `Award Semester` = '2nd Semester' THEN 1 ELSE 0 END) AS 2ndSemCWTSMaleStudents,SUM(CASE WHEN `Sex` = 'Female' and `Main Program Name` NOT like '%CRIMINOLOGY%' and `Award Semester` = '2nd Semester' THEN 1 ELSE 0 END) AS 2ndSemCWTSFemaleStudents,SUM(CASE WHEN `Sex` = 'Male' and `Main Program Name` like '%CRIMINOLOGY%' and `Award Semester` = '2nd Semester' and `Grade` BETWEEN 1 AND 3 THEN 1 ELSE 0 END) AS GradROTCMaleStudents,SUM(CASE WHEN `Sex` = 'Female' and `Main Program Name` like '%CRIMINOLOGY%' and `Award Semester` = '2nd Semester' and `Grade` BETWEEN 1 AND 3 THEN 1 ELSE 0 END) AS GradROTCFemaleStudents,SUM(CASE WHEN `Sex` = 'Male' and `Main Program Name` NOT like '%CRIMINOLOGY%' and `Award Semester` = '2nd Semester' and `Grade` BETWEEN 1 AND 3 THEN 1 ELSE 0 END) AS GradCWTSMaleStudents,SUM(CASE WHEN `Sex` = 'Female' and `Main Program Name` NOT like '%CRIMINOLOGY%' and `Award Semester` = '2nd Semester' and `Grade` BETWEEN 1 AND 3 THEN 1 ELSE 0 END) AS GradCWTSFemaleStudents from (select tbl_period.period_name as 'Award Year', tbl_period.period_semester as 'Award Semester', 'NSTP/CWTS' as 'NSTP Program', '12-SOCCSKSARGEN' as 'REGION', (s_nstp_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_ext) as 'Extension Name',  (s_mn) as 'Middle Name', DATE_FORMAT(s_dob, '%m/%d/%y') as 'Birthdate', (s_gender) as 'Sex', (sg_yearlevel) as 'Year Level', CONCAT(course_name, ' ',course_major) as 'Main Program Name', s_email as 'Email Address', s_contact as 'Telephone/CP Number', tbl_students_grades.sg_grade as 'Grade' from tbl_students_grades JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id where (tbl_subject.subject_description like '%CWTS%' or tbl_subject.subject_description like '%ROTC%') and tbl_period.period_name = '" & periodName & "' and tbl_students_grades.sg_grade_status = 'Enrolled' and tbl_student.s_nstp_no NOT LIKE '%C-%' order by tbl_student.s_ln asc, tbl_student.s_fn asc) as NSTP"
                            Dim dbcommand2 As New MySqlCommand(sql2, cn)
                            adt2.SelectCommand = dbcommand2
                            dtable2 = New DataTable
                            adt2.Fill(dtable2)
                            dg_report2.DataSource = dtable2
                            adt2.Dispose()
                            dbcommand2.Dispose()
                            cn.Close()


                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
                            Dim subfolderName As String = "Docs"
                            Dim relativePath As String = Path.Combine(subfolderName, "CHED-NSTP.xlsx")
                            Dim fullPath As String = Path.Combine(Application.StartupPath, relativePath)
                            Using package As New ExcelPackage(New FileInfo(fullPath))
                                Dim worksheet = package.Workbook.Worksheets.FirstOrDefault(Function(sheet) sheet.Name = "LIST OF NSTP GRADUATES")
                                Dim worksheet2 = package.Workbook.Worksheets.FirstOrDefault(Function(sheet) sheet.Name = "SUMMARY NUMBER ENRL. & GRAD.")
                                If worksheet IsNot Nothing Or worksheet2 IsNot Nothing Then
                                    Dim startRowToDelete As Integer = 10
                                    Dim endRowToDelete As Integer = worksheet.Dimension.End.Row
                                    If endRowToDelete >= startRowToDelete Then
                                        worksheet.DeleteRow(startRowToDelete, endRowToDelete)
                                    End If
                                    If dg_report.Rows.Count > 0 AndAlso dg_report.Rows(0).Cells.Count > 0 Or dg_report2.Rows.Count > 0 AndAlso dg_report2.Rows(0).Cells.Count > 0 Then
                                        worksheet.Cells("A3").Value = ""
                                        worksheet.Cells("A3").Value = "" & cbAcademicYear.Text.Substring(0, 1) & " Semester, Academic Year " & periodName & ""
                                        worksheet.Cells("A9:L9").Value = ""
                                        Dim lastColumn As Integer = 12
                                        Dim startRow As Integer = 9
                                        For rowIndex As Integer = 0 To dg_report.Rows.Count - 1
                                            Dim dataGridViewRow As DataGridViewRow = dg_report.Rows(rowIndex)
                                            Dim excelRowIndex As Integer = startRow + rowIndex
                                            For columnIndex As Integer = 0 To dataGridViewRow.Cells.Count - 1
                                                Dim dataGridViewCell As DataGridViewCell = dataGridViewRow.Cells(columnIndex)
                                                Dim excelColumnIndex As Integer = columnIndex + 1
                                                Dim cellValue As Object = dataGridViewCell.Value
                                                worksheet.Cells(excelRowIndex, excelColumnIndex).Value = cellValue
                                            Next
                                            worksheet.Row(excelRowIndex).Height = 31.5
                                            worksheet.Row(excelRowIndex).Style.WrapText = True
                                            For columnIndex As Integer = 0 To lastColumn - 1
                                                worksheet.Cells(excelRowIndex, columnIndex + 1).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                                worksheet.Cells(excelRowIndex, columnIndex + 1).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                                worksheet.Cells(excelRowIndex, columnIndex + 1).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                                worksheet.Cells(excelRowIndex, columnIndex + 1).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                                If columnIndex = 1 Then
                                                    worksheet.Cells(excelRowIndex, columnIndex + 1).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
                                                    worksheet.Cells(excelRowIndex, columnIndex + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)
                                                    worksheet.Cells(excelRowIndex, columnIndex + 1).Style.Font.Bold = True
                                                    worksheet.Cells(excelRowIndex, columnIndex + 1).Style.Font.Size = 14
                                                ElseIf columnIndex = 2 Then
                                                    worksheet.Cells(excelRowIndex, columnIndex + 1).Style.Font.Bold = True
                                                    worksheet.Cells(excelRowIndex, columnIndex + 1).Style.Font.Size = 14
                                                End If
                                            Next
                                        Next

                                        ' Find the last used row
                                        Dim lastUsedRow As Integer = worksheet.Dimension.End.Row

                                        ' Merge cells and apply border
                                        worksheet.Cells(lastUsedRow + 1, 1, lastUsedRow + 2, 2).Merge = True ' Merge cells A[lastUsedRow+1]:B[lastUsedRow+2]
                                        worksheet.Cells(lastUsedRow + 1, 1, lastUsedRow + 2, 2).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 1, lastUsedRow + 2, 2).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 1, lastUsedRow + 2, 2).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 1, lastUsedRow + 2, 2).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin

                                        worksheet.Cells(lastUsedRow + 1, 1, lastUsedRow + 2, 2).Value = "Sub Total:"
                                        worksheet.Cells(lastUsedRow + 1, 1, lastUsedRow + 2, 2).Style.Font.Bold = True
                                        worksheet.Cells(lastUsedRow + 1, 1, lastUsedRow + 2, 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                        worksheet.Cells(lastUsedRow + 1, 1, lastUsedRow + 2, 2).Style.HorizontalAlignment = ExcelVerticalAlignment.Distributed

                                        worksheet.Cells(lastUsedRow + 1, 4, lastUsedRow + 1, 5).Merge = True
                                        worksheet.Cells(lastUsedRow + 2, 4, lastUsedRow + 2, 5).Merge = True


                                        Dim MaleCount As String = ""
                                        cn.Open()
                                        cm = New MySqlCommand("select ifNULL(SUM(CASE WHEN `Sex` = 'Male'  THEN 1 ELSE 0 END),0) AS Students from (select tbl_period.period_name as 'Award Year', 'NSTP/CWTS' as 'NSTP Program', '12-SOCCSKSARGEN' as 'REGION', (s_nstp_no) as 'Serial Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_ext) as 'Extension Name',  (s_mn) as 'Middle Name', DATE_FORMAT(s_dob, '%m/%d/%y') as 'Birthdate', (s_gender) as 'Sex', CONCAT(s_address, ', ', (refbrgy.brgyDesc)) as 'Street/Bgry.', (refcitymun.citymunDesc) as 'Town/City', (refprovince.provDesc) as 'Province', (sg_yearlevel) as 'Year Level', CONCAT(course_name, ' ',course_major) as 'Main Program Name', s_email as 'Email Address', s_contact as 'Telephone/CP Number' from tbl_students_grades JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id LEFT JOIN refbrgy ON tbl_student.s_address_brgy = refbrgy.brgyCode  LEFT JOIN refcitymun ON tbl_student.s_address_citymun = refcitymun.citymunCode LEFT JOIN refprovince ON tbl_student.s_address_prov = refprovince.provCode JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id where tbl_student.s_nstp_no NOT LIKE '%C-%' and (tbl_subject.subject_description like '%CWTS%' or tbl_subject.subject_description like '%ROTC%') and tbl_course.course_name not like '%CRIMINOLOGY%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' and tbl_students_grades.sg_grade between 1 and 3 order by tbl_student.s_ln asc, tbl_student.s_fn asc) as NSTP", cn)
                                        MaleCount = cm.ExecuteScalar
                                        cn.Close()

                                        Dim FemaleCount As String = ""
                                        cn.Open()
                                        cm = New MySqlCommand("select ifNULL(SUM(CASE WHEN `Sex` = 'Female'  THEN 1 ELSE 0 END),0) AS Students from (select tbl_period.period_name as 'Award Year', 'NSTP/CWTS' as 'NSTP Program', '12-SOCCSKSARGEN' as 'REGION', (s_nstp_no) as 'Serial Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_ext) as 'Extension Name',  (s_mn) as 'Middle Name', DATE_FORMAT(s_dob, '%m/%d/%y') as 'Birthdate', (s_gender) as 'Sex', CONCAT(s_address, ', ', (refbrgy.brgyDesc)) as 'Street/Bgry.', (refcitymun.citymunDesc) as 'Town/City', (refprovince.provDesc) as 'Province', (sg_yearlevel) as 'Year Level', CONCAT(course_name, ' ',course_major) as 'Main Program Name', s_email as 'Email Address', s_contact as 'Telephone/CP Number' from tbl_students_grades JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id LEFT JOIN refbrgy ON tbl_student.s_address_brgy = refbrgy.brgyCode  LEFT JOIN refcitymun ON tbl_student.s_address_citymun = refcitymun.citymunCode LEFT JOIN refprovince ON tbl_student.s_address_prov = refprovince.provCode JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id where tbl_student.s_nstp_no NOT LIKE '%C-%' and (tbl_subject.subject_description like '%CWTS%' or tbl_subject.subject_description like '%ROTC%') and tbl_course.course_name not like '%CRIMINOLOGY%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' and tbl_students_grades.sg_grade between 1 and 3 order by tbl_student.s_ln asc, tbl_student.s_fn asc) as NSTP", cn)
                                        FemaleCount = cm.ExecuteScalar
                                        cn.Close()

                                        worksheet.Cells(lastUsedRow + 1, 4, lastUsedRow + 1, 5).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 4, lastUsedRow + 1, 5).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 4, lastUsedRow + 1, 5).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 4, lastUsedRow + 1, 5).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 4, lastUsedRow + 1, 5).Value = MaleCount
                                        worksheet.Cells(lastUsedRow + 1, 4, lastUsedRow + 1, 5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                        worksheet.Cells(lastUsedRow + 1, 4, lastUsedRow + 1, 5).Style.Font.Bold = True

                                        worksheet.Cells(lastUsedRow + 2, 4, lastUsedRow + 2, 5).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 2, 4, lastUsedRow + 2, 5).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 2, 4, lastUsedRow + 2, 5).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 2, 4, lastUsedRow + 2, 5).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 2, 4, lastUsedRow + 2, 5).Value = FemaleCount
                                        worksheet.Cells(lastUsedRow + 2, 4, lastUsedRow + 2, 5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                        worksheet.Cells(lastUsedRow + 2, 4, lastUsedRow + 2, 5).Style.Font.Bold = True

                                        worksheet.Cells(lastUsedRow + 1, 3).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 3).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 3).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 3).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 1, 3).Value = "Male:"
                                        worksheet.Cells(lastUsedRow + 1, 3).Style.Font.Bold = True

                                        worksheet.Cells(lastUsedRow + 2, 3).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 2, 3).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 2, 3).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 2, 3).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 2, 3).Value = "Female:"
                                        worksheet.Cells(lastUsedRow + 2, 3).Style.Font.Bold = True

                                        worksheet.Cells(lastUsedRow + 3, 3, lastUsedRow + 4, 3).Merge = True
                                        worksheet.Cells(lastUsedRow + 3, 3, lastUsedRow + 4, 3).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 3, lastUsedRow + 4, 3).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 3, lastUsedRow + 4, 3).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 3, lastUsedRow + 4, 3).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 3, lastUsedRow + 4, 3).Value = "Grand Total:"
                                        worksheet.Cells(lastUsedRow + 3, 3, lastUsedRow + 4, 3).Style.Font.Bold = True

                                        worksheet.Cells(lastUsedRow + 3, 4).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 4).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 4).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 4).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 4).Value = "Male:"
                                        worksheet.Cells(lastUsedRow + 3, 4).Style.Font.Bold = True

                                        worksheet.Cells(lastUsedRow + 4, 4).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 4, 4).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 4, 4).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 4, 4).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 4, 4).Value = "Female:"
                                        worksheet.Cells(lastUsedRow + 4, 4).Style.Font.Bold = True

                                        worksheet.Cells(lastUsedRow + 3, 5).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 5).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 5).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 5).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 3, 5).Value = MaleCount
                                        worksheet.Cells(lastUsedRow + 3, 5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                        worksheet.Cells(lastUsedRow + 3, 5).Style.Font.Bold = True
                                        worksheet.Cells(lastUsedRow + 3, 5).Style.Font.Size = 18

                                        worksheet.Cells(lastUsedRow + 4, 5).Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 4, 5).Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 4, 5).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 4, 5).Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
                                        worksheet.Cells(lastUsedRow + 4, 5).Value = FemaleCount
                                        worksheet.Cells(lastUsedRow + 4, 5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                        worksheet.Cells(lastUsedRow + 4, 5).Style.Font.Bold = True
                                        worksheet.Cells(lastUsedRow + 4, 5).Style.Font.Size = 18

                                        worksheet.Cells(lastUsedRow + 2, 6, lastUsedRow + 2, 8).Merge = True
                                        worksheet.Cells(lastUsedRow + 2, 9, lastUsedRow + 2, 11).Merge = True
                                        worksheet.Cells(lastUsedRow + 2, 6, lastUsedRow + 2, 8).Value = "Prepared by:"
                                        worksheet.Cells(lastUsedRow + 2, 9, lastUsedRow + 2, 11).Value = "Certified Correct:"
                                        worksheet.Cells(lastUsedRow + 2, 6, lastUsedRow + 2, 8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                        worksheet.Cells(lastUsedRow + 2, 9, lastUsedRow + 2, 11).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

                                        worksheet.Cells(lastUsedRow + 5, 6, lastUsedRow + 5, 8).Merge = True
                                        worksheet.Cells(lastUsedRow + 5, 9, lastUsedRow + 5, 11).Merge = True
                                        worksheet.Cells(lastUsedRow + 5, 6, lastUsedRow + 5, 8).Value = str_name.ToUpper
                                        worksheet.Cells(lastUsedRow + 5, 9, lastUsedRow + 5, 11).Value = cbRegistrar.Text.ToUpper
                                        worksheet.Cells(lastUsedRow + 5, 6, lastUsedRow + 5, 8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                        worksheet.Cells(lastUsedRow + 5, 9, lastUsedRow + 5, 11).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                        worksheet.Cells(lastUsedRow + 5, 6, lastUsedRow + 5, 8).Style.Font.Bold = True
                                        worksheet.Cells(lastUsedRow + 5, 9, lastUsedRow + 5, 11).Style.Font.Bold = True
                                        worksheet.Cells(lastUsedRow + 5, 6, lastUsedRow + 5, 8).Style.Font.Size = 16
                                        worksheet.Cells(lastUsedRow + 5, 9, lastUsedRow + 5, 11).Style.Font.Size = 16

                                        worksheet.Cells(lastUsedRow + 6, 6, lastUsedRow + 6, 8).Merge = True
                                        worksheet.Cells(lastUsedRow + 6, 9, lastUsedRow + 6, 11).Merge = True
                                        worksheet.Cells(lastUsedRow + 6, 6, lastUsedRow + 6, 8).Value = "Registrar Records In-charge"
                                        worksheet.Cells(lastUsedRow + 6, 9, lastUsedRow + 6, 11).Value = "College Registrar"
                                        worksheet.Cells(lastUsedRow + 6, 6, lastUsedRow + 6, 8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                        worksheet.Cells(lastUsedRow + 6, 9, lastUsedRow + 6, 11).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

                                        '-----------------------------------------

                                        worksheet2.Cells("A3").Value = ""

                                        worksheet2.Cells("B32").Value = ""

                                        worksheet2.Cells("B11").Value = String.Empty
                                        worksheet2.Cells("C11").Value = String.Empty
                                        worksheet2.Cells("D11").Value = String.Empty
                                        worksheet2.Cells("E11").Value = String.Empty

                                        worksheet2.Cells("H11").Value = String.Empty
                                        worksheet2.Cells("I11").Value = String.Empty
                                        worksheet2.Cells("J11").Value = String.Empty
                                        worksheet2.Cells("K11").Value = String.Empty

                                        worksheet2.Cells("T11").Value = String.Empty
                                        worksheet2.Cells("U11").Value = String.Empty
                                        worksheet2.Cells("V11").Value = String.Empty
                                        worksheet2.Cells("W11").Value = String.Empty

                                        worksheet2.Cells("A3").Value = "Academic Year " & periodName & ""
                                        worksheet2.Cells("B32").Value = cbRegistrar.Text.ToUpper

                                        worksheet2.Cells("B11").Value = dg_report2.Rows(0).Cells(0).Value
                                        worksheet2.Cells("C11").Value = dg_report2.Rows(0).Cells(1).Value
                                        worksheet2.Cells("D11").Value = dg_report2.Rows(0).Cells(2).Value
                                        worksheet2.Cells("E11").Value = dg_report2.Rows(0).Cells(3).Value

                                        worksheet2.Cells("H11").Value = dg_report2.Rows(0).Cells(4).Value
                                        worksheet2.Cells("I11").Value = dg_report2.Rows(0).Cells(5).Value
                                        worksheet2.Cells("J11").Value = dg_report2.Rows(0).Cells(6).Value
                                        worksheet2.Cells("K11").Value = dg_report2.Rows(0).Cells(7).Value

                                        worksheet2.Cells("T11").Value = dg_report2.Rows(0).Cells(8).Value
                                        worksheet2.Cells("U11").Value = dg_report2.Rows(0).Cells(9).Value
                                        worksheet2.Cells("V11").Value = dg_report2.Rows(0).Cells(10).Value
                                        worksheet2.Cells("W11").Value = dg_report2.Rows(0).Cells(11).Value

                                        package.Save()
                                        Process.Start(fullPath)
                                    Else
                                        MsgBox("No data found to be exported.", vbCritical)
                                    End If
                                Else
                                    MsgBox("Worksheet 'LIST OF NSTP GRADUATES' or 'SUMMARY NUMBER ENRL. & GRAD.' not found.", vbCritical)
                                End If
                            End Using
                        Catch ex As Exception
                            MsgBox("No data found to be exported.", vbCritical)
                        End Try
                    ElseIf cbNSTPRD.Checked = True Then
                        cn.Close()
                        cn.Open()
                        Dim dtable As DataTable
                        Dim adt As New MySqlDataAdapter
                        Dim sql As String = ""
                        sql = "SELECT ROW_NUMBER() OVER (ORDER BY `Last Name` asc, `First Name` asc) AS SEQ, `Award Year`, `NSTP Program`, `REGION`, `Serial Number`, `ID Number`, `Last Name`, `First Name`, `Extension Name`, `Middle Name`, `Birthdate`, `Sex`, `Street/Bgry.`, `Town/City`, `Province`, 'CRONASIA FOUNDATION COLLEGE, INC.' as 'HEI NAME', '11029' as 'INSTITUTIONAL CODE', 'PRIVATE' as 'TYPES OF HEIS (SUC, LUC, PRIVATE, OGS)', 'NI' as 'PROGRAM LEVEL CODE', `Main Program Name`, `Email Address`, `Telephone/CP Number` FROM (select tbl_period.period_name as 'Award Year', 'NSTP/CWTS' as 'NSTP Program', '12-SOCCSKSARGEN' as 'REGION', '' as 'Serial Number', (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_ext) as 'Extension Name',  (s_mn) as 'Middle Name', DATE_FORMAT(s_dob, '%m/%d/%y') as 'Birthdate', (s_gender) as 'Sex', CONCAT(s_address, ', ', (refbrgy.brgyDesc)) as 'Street/Bgry.', (refcitymun.citymunDesc) as 'Town/City', (refprovince.provDesc) as 'Province', (sg_yearlevel) as 'Year Level', CONCAT(course_name, ' ',course_major) as 'Main Program Name', s_email as 'Email Address', s_contact as 'Telephone/CP Number' from tbl_students_grades JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id JOIN tbl_subject ON tbl_students_grades.sg_subject_id = tbl_subject.subject_id LEFT JOIN refbrgy ON tbl_student.s_address_brgy = refbrgy.brgyCode  LEFT JOIN refcitymun ON tbl_student.s_address_citymun = refcitymun.citymunCode LEFT JOIN refprovince ON tbl_student.s_address_prov = refprovince.provCode JOIN tbl_period ON tbl_students_grades.sg_period_id = tbl_period.period_id where tbl_student.s_nstp_no NOT LIKE '%C-%' and (tbl_subject.subject_description like '%CWTS%' or tbl_subject.subject_description like '%ROTC%') and tbl_course.course_name not like '%CRIMINOLOGY%' and tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' and tbl_students_grades.sg_grade between 1 and 3 order by tbl_student.s_ln asc, tbl_student.s_fn asc) as NSTP"
                        Dim dbcommand As New MySqlCommand(sql, cn)
                        adt.SelectCommand = dbcommand
                        dtable = New DataTable
                        adt.Fill(dtable)
                        dg_report.DataSource = dtable
                        adt.Dispose()
                        dbcommand.Dispose()
                        cn.Close()

                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial
                        Dim package As ExcelPackage = New ExcelPackage()

                        Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets.Add("NSTP Report")
                        worksheet.Cells("A1").Value = "NATIONAL SERVICE TRAINING PROGRAM (NTSP) REGIONAL DATABASE" & vbCr & vbLf
                        worksheet.Cells("A2").Value = "CHED REGIONAL OFFICE 12" & vbCr & vbLf
                        Dim periodName As String = ""
                        cn.Open()
                        cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        periodName = cm.ExecuteScalar
                        cn.Close()
                        worksheet.Cells("A3").Value = "AY " & periodName & "" & vbCr & vbLf
                        worksheet.Cells("A4").Value = "" & vbCr & vbLf

                        For i As Integer = 0 To dg_report.Columns.Count - 1
                            worksheet.Cells(5, i + 1).Value = dg_report.Columns(i).HeaderText.ToUpper()
                        Next

                        For i As Integer = 0 To dg_report.Rows.Count - 1
                            For j As Integer = 0 To dg_report.Columns.Count - 1
                                worksheet.Cells(i + 6, j + 1).Value = dg_report.Rows(i).Cells(j).Value
                            Next
                        Next

                        worksheet.Cells("A1:U1").Merge = True
                        worksheet.Cells("A2:U2").Merge = True
                        worksheet.Cells("A3:U3").Merge = True
                        worksheet.Cells("A4:U4").Merge = True

                        worksheet.Cells("A1:A3").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        worksheet.Cells.AutoFitColumns()
                        worksheet.Row(5).Height = 35
                        worksheet.Cells("A5:U5").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        worksheet.Cells("A5:U5").Style.HorizontalAlignment = ExcelVerticalAlignment.Distributed
                        worksheet.Cells("A5:U5").Style.Font.Bold = True
                        worksheet.Cells("A1:A3").Style.Font.Bold = True
                        worksheet.Cells("A1").Style.Font.Size = 18
                        worksheet.Cells("A2:A3").Style.Font.Size = 14

                        For i As Integer = 1 To dg_report.Rows.Count + 4
                            If i < 5 Then
                                Continue For
                            End If

                            worksheet.Cells(i, 1, i, dg_report.Columns.Count).Style.Border.Top.Style = ExcelBorderStyle.Thin
                            worksheet.Cells(i, 1, i, dg_report.Columns.Count).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                            worksheet.Cells(i, 1, i, dg_report.Columns.Count).Style.Border.Right.Style = ExcelBorderStyle.Thin
                            worksheet.Cells(i, 1, i, dg_report.Columns.Count).Style.Border.Left.Style = ExcelBorderStyle.Thin
                        Next
                        Dim lastRow = dg_report.Rows.Count + 5
                        worksheet.Cells(lastRow, 1, lastRow, dg_report.Columns.Count).Style.Border.Top.Style = ExcelBorderStyle.Thin
                        worksheet.Cells(lastRow, 1, lastRow, dg_report.Columns.Count).Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                        worksheet.Cells(lastRow, 1, lastRow, dg_report.Columns.Count).Style.Border.Right.Style = ExcelBorderStyle.Thin
                        worksheet.Cells(lastRow, 1, lastRow, dg_report.Columns.Count).Style.Border.Left.Style = ExcelBorderStyle.Thin

                        Dim folderPath As String = "C:\NSTP Reports\" & cbAcademicYear.Text & "\"
                        If Not IO.Directory.Exists(folderPath) Then
                            IO.Directory.CreateDirectory(folderPath)
                        End If

                        Dim excelFileName As String = folderPath & "NSTP Report " & cbAcademicYear.Text & " - NSTP Regional Database.xlsx"
                        Dim fileInfo As New FileInfo(excelFileName)
                        package.SaveAs(fileInfo)

                        Process.Start(fileInfo.FullName)
                        MessageBox.Show("NSTP Report successfully generated and exported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf cbNSTPFirstPage.Checked = True Then
                        Try
                            NextBtn()
                            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                            rptdoc = New NSTPFirst
                            Dim periodName As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodName = cm.ExecuteScalar
                            cn.Close()
                            rptdoc.SetParameterValue("AcadYear", periodName)
                            rptdoc.SetParameterValue("Reg", cbRegistrar.Text.ToUpper)
                            rptdoc.SetParameterValue("user", str_name.ToUpper)
                            rptdoc.SetParameterValue("Focal", txtNSTPFocal.Text.ToUpper)
                            rptdoc.SetParameterValue("DateToday", Format(Convert.ToDateTime(DateToday), "MMMM dd, yyyy"))
                            ReportViewer.ReportSource = rptdoc
                            dg_report.DataSource = Nothing
                            ReportViewer.Select()
                            ReportGenerated = True
                        Catch ex As Exception
                            MsgBox(ex.Message, vbCritical)
                            cn.Close()
                        End Try
                    ElseIf cbNSTPCoverPage.Checked = True Then
                        Try
                            NextBtn()
                            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                            rptdoc = New NTSPCoverP
                            Dim periodName As String = ""
                            cn.Open()
                            cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                            periodName = cm.ExecuteScalar
                            cn.Close()
                            rptdoc.SetParameterValue("AcadYear", periodName)
                            ReportViewer.ReportSource = rptdoc
                            dg_report.DataSource = Nothing
                            ReportViewer.Select()
                            ReportGenerated = True
                        Catch ex As Exception
                            MsgBox(ex.Message, vbCritical)
                            cn.Close()
                        End Try
                    Else
                        MsgBox("No data to generate.", vbCritical)
                    End If
                Catch ex As Exception
                    cn.Close()
                    MsgBox(ex.Message)
                    PrevBtn()
                End Try
            End If
        ElseIf frmMain.formTitle.Text = "Generate Student Credential Transmittal" Then

            Try
                NextBtn()
                Dim dt As New DataTable
                With dt
                    .Columns.Add("s_id")
                    .Columns.Add("s_fn")
                    .Columns.Add("s_ln")
                End With
                Dim i As Integer
                For Each dr As DataGridViewRow In dgStudTransmittal.Rows
                    i += 1
                    dt.Rows.Add(i, dr.Cells(0).Value, dr.Cells(1).Value)
                Next

                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New TransmittalForm
                rptdoc.SetDataSource(dt)

                rptdoc.SetParameterValue("datetoday", Format(Convert.ToDateTime(DateToday), "MMMM dd, yyyy"))
                rptdoc.SetParameterValue("schoolname", SchoolName)
                rptdoc.SetParameterValue("schooladdress", SchoolAddress)



                Dim periodName As String = ""
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT period_name FROM `tbl_period` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                periodName = cm.ExecuteScalar
                cn.Close()
                rptdoc.SetParameterValue("schoolyear", periodName)

                If dgStudTransmittal.RowCount > 1 Then
                    rptdoc.SetParameterValue("students", "students")
                    rptdoc.SetParameterValue("credential", "credentials")
                Else
                    rptdoc.SetParameterValue("students", "student")
                    If dgStudTransmittal.CurrentRow.Cells(1).Value.ToString.Contains(",") Then
                        rptdoc.SetParameterValue("credential", "credentials")
                    Else
                        rptdoc.SetParameterValue("credential", "credential")
                    End If
                End If

                rptdoc.SetParameterValue("registrar", cbRegistrar.Text)
                If cbRegSign.Checked = True Then
                    rptdoc.SetParameterValue("emp_sign_pic", "" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg")
                Else
                    rptdoc.SetParameterValue("emp_sign_pic", "0")
                End If

                ReportViewer.ReportSource = rptdoc
                dg_report.DataSource = Nothing
                ReportViewer.Select()
                ReportGenerated = True
                recordRequest()

                dgStudTransmittal.Rows.Clear()

            Catch ex As Exception
                MsgBox(ex.Message, vbCritical)
                cn.Close()
                PrevBtn()
            End Try

        End If

        If ReportGenerated = True Then
            UserActivity("" & frmMain.formTitle.Text & "", "REPORTS")
            ReportGenerated = False
        Else
        End If
    End Sub

    Private Sub recordRequest()
        If cbRecord.Checked = True Then
            If str_role = "Administrator" Or str_role = "Registrar" Then
                Try
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("INSERT INTO `tbl_student_credrqst` (`credrqst_stud_id`, `credrqst_doc`, `credrqst_purpose`, `credrqst_date`, `credrqst_user_id`) VALUES ('" & studentId & "', 'Transcript of Records', '" & txtRemarks.Text & "', CURDATE(), " & str_userid & ")", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                Catch ex As Exception
                    cn.Close()
                    MsgBox(ex.Message, vbCritical)
                End Try
            End If
        Else
        End If
    End Sub
    Private Sub cbCredentials_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCredentials.SelectedIndexChanged
        LoadCredentialOptions()
    End Sub
    Sub LoadCredentialOptions()
        Try
            If frmMain.formTitle.Text = "Generate Student Credential" Then
                HideAllReportPanels()
                PanelAcad.Visible = True
                PanelStudent.Visible = True
                PanelCredential.Visible = True
                PanelAdmin.Visible = True
                PanelRegistrar.Visible = True
                Select Case cbCredentials.Text
                    Case "Honorable Dismissal"
                        PanelAdmin.Visible = False
                        PanelReceipt.Visible = True
                        PanelSchool.Visible = True
                    Case "Good Moral Character"
                        PanelAdmin.Visible = False
                        PanelPurpose.Visible = True
                        LastCurrentPanel.Visible = True
                        cbPurpose.Text = "Legal Purpose"
                        LastCurrentPanel.Visible = True
                    Case "Diploma"
                        PanelDiplomaFormat.Visible = True
                    Case "NSTP Serial Number Certification"
                        PanelAdmin.Visible = False
                        LastCurrentPanel.Visible = True
                        PanelPurpose.Visible = True
                    Case "Grade Certificate"
                        PanelAdmin.Visible = False
                        LastCurrentPanel.Visible = True
                        PanelPurpose.Visible = True
                    Case "Graduation Certificate"
                        PanelAdmin.Visible = False
                    Case "Certification of Total Number of Units Earned"
                        PanelAdmin.Visible = False
                        PanelEarnedUnits.Visible = True
                    Case "Transcript of Records"
                        PanelTORGraduated.Visible = True
                        PanelRFG.Visible = True
                        PanelRemarks.Visible = True
                    Case "Official Transcript of Records"
                        PanelTORGraduated.Visible = True
                        PanelRFG.Visible = True
                        PanelRemarks.Visible = True
                        OTRack.Visible = True
                    Case "Certification of Total Number of Units Earned"
                        PanelAdmin.Visible = False
                    Case "General Weighted Average"
                        PanelAdmin.Visible = False
                    Case "Enrollment Certificate"
                        PanelAdmin.Visible = False
                        LastCurrentPanel.Visible = True
                        PanelPurpose.Visible = True
                    Case "Certificate of No Scholarship"
                        PanelAdmin.Visible = False
                    Case "Graduating Certificate"
                        PanelAdmin.Visible = False
                    Case "Graduation Certificate"
                        PanelAdmin.Visible = False
                    Case "Certificate of English as Medium of Instruction"
                        PanelAdmin.Visible = False
                    Case "Notice of Release"
                        PanelAcad.Visible = False
                        PanelAdmin.Visible = False
                        PanelSchool.Visible = True
                        PanelNOR.Visible = True
                        PanelRecordReport.Visible = False
                    Case "Award Certificate"
                        PanelAcadCoor.Visible = True
                        PanelPurpose.Visible = True
                        cbPurpose.Text = "Legal Purpose"
                End Select
                PanelRecordReport.Visible = True
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub exportexcel()

        Dim sheetIndex As Integer
        Dim Ex As Object
        Dim Wb As Object
        Dim Ws As Object
        Ex = CreateObject("Excel.Application")
        Wb = Ex.workbooks.add

        Dim col, row As Integer

        Dim rawData(dg_report.Rows.Count, dg_report.Columns.Count - 1) As Object


        For col = 0 To dg_report.Columns.Count - 1
            rawData(0, col) = dg_report.Columns(col).HeaderText.ToUpper

        Next

        For col = 0 To dg_report.Columns.Count - 1
            For row = 0 To dg_report.Rows.Count - 1
                rawData(row + 1, col) = dg_report.Rows(row).Cells(col).Value

            Next
        Next

        Dim finalColLetter As String = String.Empty
        finalColLetter = ExcelColName(dg_report.Columns.Count)

        sheetIndex += 1
        Ws = Wb.Worksheets(sheetIndex)

        Dim excelRange As String = String.Format("A1:{0}{1}", finalColLetter, dg_report.Rows.Count + 1)

        Ws.Range(excelRange, Type.Missing).Value2 = rawData
        Ws = Nothing
        If frmMain.formTitle.Text = "Generate Enrollment List" Then
            Wb.SaveAs("C:\HEMIS - " & cbAcademicYear.Text & "\HEMIS - Enrollment List - " & cbAcademicYear.Text & ".xlsx", Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
            Wb.Close(True, Type.Missing, Type.Missing)
            Wb = Nothing
        ElseIf frmMain.formTitle.Text = "Generate Promotional List" Then
            Wb.SaveAs("C:\HEMIS - " & cbAcademicYear.Text & "\HEMIS - Promotional List - " & cbAcademicYear.Text & ".xlsx", Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
            Wb.Close(True, Type.Missing, Type.Missing)
            Wb = Nothing
        End If

        Ex.Quit()
        Ex = Nothing

        GC.Collect()

    End Sub
    Public Function ExcelColName(ByVal Col As Integer) As String
        If Col < 0 And Col > 256 Then
            MsgBox("Invalid Argument", MsgBoxStyle.Critical)
            Return Nothing
            Exit Function
        End If
        Dim i As Int16
        Dim r As Int16
        Dim S As String
        If Col <= 26 Then
            S = Chr(Col + 64)
        Else
            r = Col Mod 26
            i = System.Math.Floor(Col / 26)
            If r = 0 Then
                r = 26
                i = i - 1
            End If
            S = Chr(i + 64) & Chr(r + 64)
        End If
        ExcelColName = S
    End Function

    Sub ReportStudentList()
        dgStudentList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, course_name, s_course_status from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (tbl_student.s_ln like '" & txtSearch.Text & "%' or tbl_student.s_fn like '" & txtSearch.Text & "%' or tbl_student.s_mn like '" & txtSearch.Text & "%' or tbl_student.s_id_no like '" & txtSearch.Text & "%' or tbl_student.s_yr_lvl like '" & txtSearch.Text & "%') order by s_id_no asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("course_name").ToString, dr.Item("s_course_status").ToString)
        End While
        dr.Close()
        cn.Close()

        If frmMain.systemModule.Text = "College Module" Then
            dgStudentList.Columns(8).HeaderText = "Course"
        Else
            dgStudentList.Columns(8).HeaderText = "Strand/Grade"
        End If

        dgPanelPadding(dgStudentList, dgPanel)
    End Sub

    Sub AcknowledgementListRecord()
        dgAckList.Rows.Clear()
        Dim sql As String
        sql = "SELECT t1.`ref_code` as 'Code', t5.schl_name as 'School', t1.`ref_status` as 'Status', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where t1.`ref_schoold_id` = " & schoolId & " and t1.`ref_status` = 'Pending' and t1.`ref_code` LIKE '%" & txtSearch.Text & "%' group by t1.`ref_code` limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgAckList.Rows.Add(dr.Item("Code").ToString, dr.Item("School").ToString, dr.Item("Status").ToString, dr.Item("Date").ToString, dr.Item("Remarks").ToString)
        End While
        dr.Close()
        cn.Close()
        dgPanelPadding(dgAckList, dgPanel)
    End Sub



    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Select Case frmTitle.Text
            Case "Search Student"
                ReportStudentList()
            Case "Search Class Schedule"
                ReportClassSchedList()
            Case "Search Instructor"
                ReportEmployeeList()
            Case "Search Class Block/Section"
                ReportSectionList()
            Case "Search Course"
                ReportCourseList()
            Case "Search School"
                ReportSchoolList()
            Case "Search Student - Transmittal"
                AcknowledgementListRecord()
        End Select
    End Sub

    Private Sub btnCancelSearch_Click(sender As Object, e As EventArgs) Handles btnCancelSearch.Click
        SearchPanel.Visible = False
        ReportsControlsPanel.Visible = True
        Panel3.Visible = True
        frmTitle.Text = "Search"
        txtSearch.Text = String.Empty
        dgStudentList.Rows.Clear()
    End Sub

    Private Sub btnSearchClass_Click(sender As Object, e As EventArgs) Handles btnSearchClass.Click
        SearchPanel.Visible = True
        dgClassSchedList.BringToFront()
        ReportsControlsPanel.Visible = False
        Panel3.Visible = False
        frmTitle.Text = "Search Class Schedule"
        ReportClassSchedList()
    End Sub

    Private Sub btnSearchInstructor_Click(sender As Object, e As EventArgs) Handles btnSearchInstructor.Click
        SearchPanel.Visible = True
        dgClassSchedules.Rows.Clear()
        dgEmployeeList.BringToFront()
        ReportsControlsPanel.Visible = False
        Panel3.Visible = False
        frmTitle.Text = "Search Instructor"
        ReportEmployeeList()
    End Sub

    Private Sub btnSearchSection_Click(sender As Object, e As EventArgs) Handles btnSearchSection.Click
        SearchPanel.Visible = True
        dgClassSchedules.Rows.Clear()
        dgSectionList.BringToFront()
        ReportsControlsPanel.Visible = False
        Panel3.Visible = False
        frmTitle.Text = "Search Class Block/Section"
        ReportSectionList()
    End Sub

    Private Sub fetchClassdata()
        classId = dgClassSchedules.CurrentRow.Cells(1).Value
        subjectCode = dgClassSchedules.CurrentRow.Cells(3).Value
        subjectDesc = dgClassSchedules.CurrentRow.Cells(4).Value
        subjectUnits = dgClassSchedules.CurrentRow.Cells(5).Value
        subjectDay = dgClassSchedules.CurrentRow.Cells(6).Value
        subjectTstart = dgClassSchedules.CurrentRow.Cells(7).Value
        subjectTend = dgClassSchedules.CurrentRow.Cells(8).Value
        subjectRoom = dgClassSchedules.CurrentRow.Cells(9).Value
        subjectInstructor = dgClassSchedules.CurrentRow.Cells(10).Value
        subjectClass = dgClassSchedules.CurrentRow.Cells(2).Value
    End Sub
    Private Sub dgClassSchedules_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgClassSchedules.CellClick
        fetchClassdata()
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        If txtStudent.Text = String.Empty Then
        Else
            YearLevelStudentGradeLevel()
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If ReportViewer.ReportSource Is Nothing Then
                MsgBox("No report is loaded in the Report Viewer.", vbCritical)
                Return
            End If
            ReportViewer.PrintReport()
        Catch ex As Exception
            ' Handle any exceptions
            MsgBox("Error printing report: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnPrintSettings_Click(sender As Object, e As EventArgs) Handles btnPrintSettings.Click
        Try
            ' Check if the CrystalReportViewer has a ReportSource
            If ReportViewer.ReportSource Is Nothing Then
                MsgBox("No report is loaded in the Report Viewer.", vbCritical)
                Return
            End If
            ' Create a PrintDocument
            Dim printDoc As New Printing.PrintDocument()
            ' Set the ReportDocument as the PrintDocument's Document
            Dim reportDocument As ReportDocument = TryCast(ReportViewer.ReportSource, ReportDocument)
            printDoc.DocumentName = reportDocument.Database.Tables(0).Name
            ' Create a PrintDialog
            Dim printDialog As New PrintDialog()
            printDialog.Document = printDoc
            ' Show the printer setup dialog
            If printDialog.ShowDialog() = DialogResult.OK Then
                ' User clicked OK in the printer setup dialog
                ' Set the printer settings
                reportDocument.PrintOptions.PrinterName = printDoc.PrinterSettings.PrinterName
                ' Print the report
                reportDocument.PrintToPrinter(printDoc.PrinterSettings, printDoc.PrinterSettings.DefaultPageSettings, False)
            Else
                ' User clicked Cancel in the printer setup dialog
                MsgBox("Printing cancelled.", vbInformation)
            End If
        Catch ex As Exception
            ' Handle any exceptions
            MsgBox("Error printing report: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub SearchPanel_VisibleChanged(sender As Object, e As EventArgs) Handles SearchPanel.VisibleChanged
        txtSearch.Select()
    End Sub

    Private Sub btnSearchCourse_Click(sender As Object, e As EventArgs) Handles btnSearchCourse.Click
        SearchPanel.Visible = True
        dgCourseList.BringToFront()
        ReportsControlsPanel.Visible = False
        Panel3.Visible = False
        frmTitle.Text = "Search Course"
        ReportCourseList()
    End Sub
    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click
        If CheckBox2.Checked = True Or CheckBox3.Checked = True Or CheckBox4.Checked = True Then
            CheckBox1.Checked = True
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            CheckBox4.Checked = False
            PanelCourse.Visible = False
        Else
        End If
        If CheckBox1.Checked = True Or CheckBox2.Checked = True Or CheckBox3.Checked = True Or CheckBox4.Checked = True Then
            PanelCourse.Visible = False
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            PanelCourse.Visible = True
        End If
    End Sub

    Private Sub CheckBox2_Click(sender As Object, e As EventArgs) Handles CheckBox2.Click
        If CheckBox1.Checked = True Or CheckBox3.Checked = True Or CheckBox4.Checked = True Then
            CheckBox2.Checked = True
            CheckBox1.Checked = False
            CheckBox3.Checked = False
            CheckBox4.Checked = False
            PanelCourse.Visible = False
        Else
        End If
        If CheckBox1.Checked = True Or CheckBox2.Checked = True Or CheckBox3.Checked = True Or CheckBox4.Checked = True Then
            PanelCourse.Visible = False
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            PanelCourse.Visible = True
        End If
    End Sub

    Private Sub CheckBox3_Click(sender As Object, e As EventArgs) Handles CheckBox3.Click
        If CheckBox1.Checked = True Or CheckBox2.Checked = True Or CheckBox4.Checked = True Then
            CheckBox3.Checked = True
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox4.Checked = False
            PanelCourse.Visible = False
        Else
        End If
        If CheckBox1.Checked = True Or CheckBox2.Checked = True Or CheckBox3.Checked = True Or CheckBox4.Checked = True Then
            PanelCourse.Visible = False
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            PanelCourse.Visible = True
        End If
    End Sub

    Private Sub CheckBox4_Click(sender As Object, e As EventArgs) Handles CheckBox4.Click
        If CheckBox1.Checked = True Or CheckBox2.Checked = True Or CheckBox3.Checked = True Then
            CheckBox4.Checked = True
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            PanelCourse.Visible = False
        Else
        End If
        If CheckBox1.Checked = True Or CheckBox2.Checked = True Or CheckBox3.Checked = True Or CheckBox4.Checked = True Then
            PanelCourse.Visible = False
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            PanelCourse.Visible = True
        End If
    End Sub

    Private Sub cbAllCourse_CheckedChanged(sender As Object, e As EventArgs) Handles cbAllCourse.CheckedChanged
        If cbAllCourse.Checked = True Then
            txtCourse.Text = "All Course Selected"
            btnSearchCourse.Enabled = False
            PanelEnrollmentList.Enabled = False
        Else
            txtCourse.Text = String.Empty
            btnSearchCourse.Enabled = True
            PanelEnrollmentList.Enabled = True
        End If
    End Sub

    Private Sub txtRemarks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtRemarks.SelectedIndexChanged
        If txtRemarks.Text = "VALID FOR BOARD EXAMINATION PURPOSES ONLY" Or txtRemarks.Text = "VALID FOR REAL ESTATE BROKERS LICENSURE EXAMINATION PURPOSES ONLY" Then
            PanelRemarksYear.Visible = True
            PanelRemarks.Size = New Size(284, 88)
        Else
            PanelRemarksYear.Visible = False
            PanelRemarks.Size = New Size(284, 41)
        End If
        txtRemarksYear.Text = String.Empty
    End Sub

    Private Sub cbNote_CheckedChanged(sender As Object, e As EventArgs) Handles cbNote.CheckedChanged
        If cbNote.Checked = False Then
            txtNote.Text = String.Empty
        Else
            If txtStudent.Text = String.Empty Then
                MsgBox("Please select Student.", vbCritical)
                btnSearchStudent.Select()
            Else
                cn.Close()
                cn.Open()
                Dim CourseYearsNumber As String = String.Empty
                cm = New MySqlCommand("SELECT course_levels FROM `tbl_course` where course_id = " & studentCourseId & "", cn)
                CourseYearsNumber = cm.ExecuteScalar
                cn.Close()
                cn.Close()
                cn.Open()
                Dim SONNumber As String = String.Empty
                cm = New MySqlCommand("SELECT IF(s_so_no = NULL or s_so_no = '',0,s_so_no) as s_so_no FROM `tbl_student` where s_id_no = '" & studentId & "'", cn)
                SONNumber = cm.ExecuteScalar
                cn.Close()
                cn.Open()
                Dim DateGrad As String = String.Empty
                cm = New MySqlCommand("SELECT Ifnull(DATE_FORMAT(s_grad_date, '%M %d, %Y'),'-') FROM `tbl_student` where s_id_no = '" & studentId & "'", cn)
                DateGrad = cm.ExecuteScalar
                cn.Close()
                cn.Open()
                Dim SODate As String = String.Empty
                cm = New MySqlCommand("SELECT s_so_date FROM `tbl_student` where s_id_no = '" & studentId & "'", cn)
                SODate = cm.ExecuteScalar
                cn.Close()

                If CourseYearsNumber = "1ST & 2ND YEAR" AndAlso studentCourse = "ACT" Then
                    If SONNumber = String.Empty Then
                        txtNote.Text = "GRADUATED: FROM THE TWO-YEAR COURSE IN COMPUTER TECHNOLOGY LEADING TO THE DEGREE ASSOCIATE IN COMPUTER TECHNOLOGY (ACT) ON " & DateGrad.ToUpper & "."
                    Else
                        txtNote.Text = "GRADUATED: FROM THE TWO-YEAR COURSE IN COMPUTER TECHNOLOGY WITH THE DEGREE OF ASSOCIATE IN COMPUTER TECHNOLOGY (ACT) AS OF " & DateGrad.ToUpper & " AS PER SPECIAL ORDER NO." & SONNumber & " DATED " & SODate.ToUpper & " ISSUED BY THE COMMISSION ON HIGHER EDUCATION (CHED) REGION XII, KORONADAL CITY."
                    End If
                Else
                    If SONNumber = String.Empty Then
                        txtNote.Text = "GRADUATED: FROM THE FOUR-YEAR COURSE IN " & studentCourseSector.ToUpper & " LEADING TO THE DEGREE " & studentCourseDesc.ToUpper & " (" & studentCourse.ToUpper & ") ON " & DateGrad.ToUpper & "."
                    Else
                        txtNote.Text = "GRADUATED: FROM THE FOUR-YEAR COURSE IN " & studentCourseSector.ToUpper & " WITH THE DEGREE OF " & studentCourseDesc.ToUpper & " (" & studentCourse.ToUpper & ") AS OF " & DateGrad.ToUpper & " AS PER SPECIAL ORDER NO." & SONNumber & " DATED " & SODate.ToUpper & " ISSUED BY THE COMMISSION ON HIGHER EDUCATION (CHED) REGION XII, KORONADAL CITY."
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub btnSearchSchool_Click(sender As Object, e As EventArgs) Handles btnSearchSchool.Click
        SearchPanel.Visible = True
        dgSchoolList.BringToFront()
        ReportsControlsPanel.Visible = False
        Panel3.Visible = False
        frmTitle.Text = "Search School"
        ReportSchoolList()
    End Sub

    Private Sub CheckBox11_Click(sender As Object, e As EventArgs) Handles CheckBox11.Click
        If CheckBox12.Checked = True Or CheckBox13.Checked = True Then
            CheckBox11.Checked = True
            CheckBox12.Checked = False
            CheckBox13.Checked = False
        Else
        End If
        PanelGenerateAcadDate.Visible = False
    End Sub

    Private Sub CheckBox12_Click(sender As Object, e As EventArgs) Handles CheckBox12.Click
        If CheckBox11.Checked = True Or CheckBox13.Checked = True Then
            CheckBox12.Checked = True
            CheckBox11.Checked = False
            CheckBox13.Checked = False
        Else
        End If
        PanelGenerateAcadDate.Visible = True
    End Sub

    Private Sub CheckBox13_Click(sender As Object, e As EventArgs) Handles CheckBox13.Click
        If CheckBox11.Checked = True Or CheckBox12.Checked = True Then
            CheckBox13.Checked = True
            CheckBox11.Checked = False
            CheckBox12.Checked = False
        Else
        End If
        PanelGenerateAcadDate.Visible = True
    End Sub

    Private Sub cbRegSign_CheckedChanged(sender As Object, e As EventArgs) Handles cbRegSign.CheckedChanged
        If cbRegSign.Checked = True Then
            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select emp_sign_photo from tbl_employee where emp_id = " & CInt(cbRegistrar.SelectedValue) & "", cn)
                dr = cm.ExecuteReader
                While dr.Read
                    Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                    Dim array(CInt(len)) As Byte
                    dr.GetBytes(0, 0, array, 0, CInt(len))
                    Dim ms As New MemoryStream(array)
                    Dim bitmap As New System.Drawing.Bitmap(ms)
                    employee_sign.Image = bitmap
                End While
                dr.Close()
                cn.Close()
            Catch ex As Exception
                employee_sign.Image = dummy_pic.Image
                cn.Close()
                MsgBox(ex.Message)
            End Try
        Else
            employee_sign.Image = dummy_pic.Image
        End If
        RegPhoto()
    End Sub

    Sub RegPhoto()
        Try
            Dim FileSize As UInt32
            Dim mstream As New System.IO.MemoryStream()
            employee_sign.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim arrImage() As Byte = mstream.GetBuffer()
            FileSize = mstream.Length
            mstream.Close()

            Dim sPath As String = IO.Path.Combine("" & Application.StartupPath() & "\EMPLOYEEPHOTOS")
            If Not IO.Directory.Exists(sPath) Then
                IO.Directory.CreateDirectory(sPath)
                employee_sign.Image.Save("" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
            Else
                employee_sign.Image.Save("" & Application.StartupPath() & "\EMPLOYEEPHOTOS\" & cbRegistrar.Text & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cbNSTPEG_Click(sender As Object, e As EventArgs) Handles cbNSTPEG.Click
        If cbNSTPFirstPage.Checked = True Or cbNSTPRD.Checked = True Or cbNSTPCoverPage.Checked = True Then
            cbNSTPEG.Checked = True
            cbNSTPFirstPage.Checked = False
            cbNSTPRD.Checked = False
            cbNSTPCoverPage.Checked = False
        Else
        End If
    End Sub

    Private Sub cbNSTPGSN_Click(sender As Object, e As EventArgs) Handles cbNSTPFirstPage.Click
        If cbNSTPEG.Checked = True Or cbNSTPRD.Checked = True Or cbNSTPCoverPage.Checked = True Then
            cbNSTPFirstPage.Checked = True
            cbNSTPEG.Checked = False
            cbNSTPRD.Checked = False
            cbNSTPCoverPage.Checked = False
        Else
        End If
    End Sub

    Private Sub cbNSTPRD_Click(sender As Object, e As EventArgs) Handles cbNSTPRD.Click
        If cbNSTPEG.Checked = True Or cbNSTPFirstPage.Checked = True Or cbNSTPCoverPage.Checked = True Then
            cbNSTPRD.Checked = True
            cbNSTPEG.Checked = False
            cbNSTPFirstPage.Checked = False
            cbNSTPCoverPage.Checked = False
        Else
        End If
    End Sub
    Private Sub cbNSTPCoverPage_Click(sender As Object, e As EventArgs) Handles cbNSTPCoverPage.Click
        If cbNSTPEG.Checked = True Or cbNSTPFirstPage.Checked = True Or cbNSTPRD.Checked = True Then
            cbNSTPRD.Checked = False
            cbNSTPEG.Checked = False
            cbNSTPFirstPage.Checked = False
            cbNSTPCoverPage.Checked = True
        Else
        End If
    End Sub

    Private Sub cbLast_Click(sender As Object, e As EventArgs) Handles cbLast.Click
        Try
            If cbCurrent.Checked = True Then
                cbCurrent.Checked = False
                cbLast.Checked = True
            End If
            If cbLast.Checked = False And cbCurrent.Checked = False Then
                cbLast.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub cbCurrent_Click(sender As Object, e As EventArgs) Handles cbCurrent.Click
        Try
            If cbLast.Checked = True Then
                cbLast.Checked = False
                cbCurrent.Checked = True
            End If
            If cbLast.Checked = False And cbCurrent.Checked = False Then
                cbCurrent.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cbHemisCourseSelected_Click(sender As Object, e As EventArgs) Handles cbHemisCourseSelected.Click
        Try
            If cbHemisAllCourse.Checked = True Then
                cbHemisAllCourse.Checked = False
                cbHemisCourseSelected.Checked = True
            End If
            If cbHemisCourseSelected.Checked = False And cbHemisAllCourse.Checked = False Then
                cbHemisCourseSelected.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub cbHemisAllCourse_Click(sender As Object, e As EventArgs) Handles cbHemisAllCourse.Click
        Try
            If cbHemisCourseSelected.Checked = True Then
                cbHemisCourseSelected.Checked = False
                cbHemisAllCourse.Checked = True
            End If
            If cbHemisCourseSelected.Checked = False And cbHemisAllCourse.Checked = False Then
                cbHemisAllCourse.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cbGradList_Click(sender As Object, e As EventArgs) Handles cbGradList.Click
        If cbGradList.Checked = True Then
            cbGradList.Checked = True
            cbCredMonitoring.Checked = False
            cbStudentData.Checked = False
            PanelHemis2.Visible = True
            PanelHemis3.Visible = False
        End If
        If cbGradList.Checked = False And cbStudentData.Checked = False And cbCredMonitoring.Checked = False Then
            PanelHemis2.Visible = True
            PanelHemis3.Visible = False
            cbGradList.Checked = True
        End If
    End Sub
    Private Sub cbStudentData_Click(sender As Object, e As EventArgs) Handles cbStudentData.Click
        If cbStudentData.Checked = True Then
            cbGradList.Checked = False
            cbCredMonitoring.Checked = False
            cbStudentData.Checked = True
            PanelHemis2.Visible = False
            PanelHemis3.Visible = True
        End If

        If cbGradList.Checked = False And cbStudentData.Checked = False And cbCredMonitoring.Checked = False Then
            PanelHemis2.Visible = False
            PanelHemis3.Visible = True
            cbStudentData.Checked = True
        End If
    End Sub
    Private Sub cbCredMonitoring_Click(sender As Object, e As EventArgs) Handles cbCredMonitoring.Click
        If cbCredMonitoring.Checked = True Then
            cbStudentData.Checked = False
            cbGradList.Checked = False
            cbCredMonitoring.Checked = True
            PanelHemis2.Visible = False
            PanelHemis3.Visible = False
        End If
        If cbGradList.Checked = False And cbStudentData.Checked = False And cbCredMonitoring.Checked = False Then
            PanelHemis2.Visible = False
            PanelHemis3.Visible = False
            cbCredMonitoring.Checked = True
        End If
    End Sub

    Private Sub txtRemarks_TextChanged(sender As Object, e As EventArgs) Handles txtRemarks.TextChanged
        If txtRemarks.Text = "VALID FOR BOARD EXAMINATION PURPOSES ONLY" Or txtRemarks.Text = "VALID FOR REAL ESTATE BROKERS LICENSURE EXAMINATION PURPOSES ONLY" Then
            PanelRemarksYear.Visible = True
            PanelRemarksYear.Size = New Size(284, 88)
        Else
            PanelRemarksYear.Visible = False
            PanelRemarksYear.Size = New Size(284, 41)
        End If
        txtRemarksYear.Text = String.Empty
    End Sub

    Private Sub btnTransmittalStudent_Click(sender As Object, e As EventArgs) Handles btnTransmittalStudent.Click
        SearchPanel.Visible = True
        dgAckList.BringToFront()
        ReportsControlsPanel.Visible = False
        Panel3.Visible = False
        frmTitle.Text = "Search Student - Transmittal"
        AcknowledgementListRecord()
    End Sub

    Private Sub rbViewClass_Click(sender As Object, e As EventArgs)
        'If rbViewClass.Checked Then
        '    rbViewClass.Checked = False
        'Else
        '    rbViewClass.Checked = True
        'End If

    End Sub

    Private Sub rbViewClass_CheckedChanged(sender As Object, e As EventArgs) Handles rbViewClass.CheckedChanged

        If rbViewClass.Checked = True Then
            Try
                dgStudentView.Visible = True
                dgStudentView.Rows.Clear()
                Dim i As Integer
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT t2.enrollment_code, t1.sg_period_id, t1.sg_student_id as id, t4.s_ln, t4.s_fn,t4.s_mn, t4.s_gender, t5.course_code, t1.sg_yearlevel, (t3.class_schedule_id) AS 'ClassSchedule_ID', (t6.cb_code) AS 'Class', (t9.subject_code) AS 'Subject Code', (t9.subject_description) AS 'Subject Desc.', (t9.subject_units) AS 'Units', (t10.ds_code) AS 'Days', (time_start_schedule) AS 'Start Time', (t3.time_end_schedule) AS 'End Time', (t8.room_code) AS 'Room', (t7.Instructor) AS 'Instructor', tp.sp_profile_photo FROM tbl_students_grades t1 JOIN tbl_class_schedule t3 ON t1.sg_class_id = t3.class_schedule_id AND t3.csperiod_id = t1.sg_period_id JOIN tbl_student t4 ON t1.sg_student_id = t4.s_id_no JOIN tbl_course t5 ON t1.sg_course_id = t5.course_id LEFT JOIN tbl_class_block t6 ON t3.class_block_id = t6.cb_id  LEFT JOIN employee t7 ON t3.csemp_id = t7.emp_id LEFT JOIN tbl_room t8 ON t3.csroom_id = t8.room_id LEFT JOIN tbl_subject t9 ON t3.cssubject_id = t9.subject_id LEFT JOIN tbl_day_schedule t10 ON t3.days_schedule = t10.ds_id JOIN tbl_enrollment t2 ON t4.s_id_no = t2.estudent_id and t1.sg_period_id = t2.eperiod_id LEFT JOIN cfcissmsdb_sphotos.tbl_student_photos tp ON t1.sg_student_id = tp.sp_student_id WHERE t1.sg_class_id = " & classId & " and t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and t1.sg_grade_status = 'Enrolled' order by t4.s_ln", cn)
                dr = cm.ExecuteReader
                While dr.Read
                    i += 1
                    ' Check if the profile photo is null
                    Dim profilePhoto As Object = dr.Item("sp_profile_photo")
                    If IsDBNull(profilePhoto) Then
                        ' Assign the image from the PictureBox if profile photo is null
                        profilePhoto = Nothing
                    End If
                    dgStudentView.Rows.Add(i, profilePhoto, dr.Item("id").ToString, dr.Item("s_ln").ToString, dr.Item("s_fn").ToString, dr.Item("s_mn").ToString, "", dr.Item("s_gender").ToString, dr.Item("sg_yearlevel").ToString, dr.Item("course_code").ToString)
                End While
                dr.Close()

                For i = 0 To dgStudentView.Rows.Count - 1
                    Dim r As DataGridViewRow = dgStudentView.Rows(i)
                    r.Height = 100
                Next
                If dgStudentView.Rows.Count = 0 Then
                Else
                    Dim imageColumn = DirectCast(dgStudentView.Columns("Column10"), DataGridViewImageColumn)
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch
                End If
                cn.Close()
            Catch ex As FormatException
            Catch ex As Exception
            End Try
        Else
            dgStudentView.Visible = False
            dgStudentView.Rows.Clear()
        End If

    End Sub
    Sub PrintSOA()
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT * from tbl_enrollment where estudent_id = '" & studentId & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                NextBtn()
                dr.Close()
                cn.Close()
                Dim assessment As Decimal
                Dim institutionaldiscount As Decimal
                Dim downpayment As Decimal
                Dim additionaladjustment As Decimal
                Dim lessadjustment As Decimal
                Dim totalassessment As Decimal
                Dim balance As Decimal
                Dim totalpaid As Decimal
                Dim otherfees As Decimal
                Dim prelim_date As String
                Dim midterm_date As String
                Dim semifinal_date As String
                Dim final_date As String
                Dim assessmentid As Integer
                Dim oldaccount As Decimal
                Dim lackingcredentials As String
                Dim scholarname As String
                Dim petition As Decimal
                Dim petition_no As Integer
                Dim additional_fees As Decimal
                Dim non_petition_no As Integer
                Dim prcnt As Decimal
                Dim discount As Decimal

                cn.Open()
                cm = New MySqlCommand("SELECT `Additional Fee (Subject Fee/Petition)` FROM `student_assessment_total` WHERE `spab_stud_id` =  '" & studentId & "' and `spab_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                petition = CDec(cm.ExecuteScalar)
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT COUNT(class_schedule_id) as 'ClassSchedule_ID' from tbl_students_grades JOIN tbl_class_schedule ON tbl_students_grades.sg_class_id = tbl_class_schedule.class_schedule_id and tbl_students_grades.sg_period_id = tbl_class_schedule.csperiod_id WHERE cs_is_petition = 'Yes' and `sg_student_id` =  '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                petition_no = CInt(cm.ExecuteScalar)
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT COUNT(class_schedule_id) as 'ClassSchedule_ID' from tbl_students_grades JOIN tbl_class_schedule ON tbl_students_grades.sg_class_id = tbl_class_schedule.class_schedule_id and tbl_students_grades.sg_period_id = tbl_class_schedule.csperiod_id WHERE cs_amount > 0 and cs_is_petition NOT IN ('Yes') and `sg_student_id` =  '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                non_petition_no = CInt(cm.ExecuteScalar)
                cn.Close()
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT `Academic Year`, `Assessment`, `Institutional Discount`, `Discounted Assessment`, `Other Fees`, `Additional Fee (Uniforms, etc.)`, `Additional Fee (Subject Fee/Petition)`, `Additional Adjustment`, `Less Adjustment`, `Down Payment`, `Total Assessment`, (`Total Paid` + `Down Payment`) as `Paid`, `Total Balance`, `Excess`, spab_ass_id, `Discount` FROM `student_assessment_total` WHERE `spab_stud_id` =  '" & studentId & "' and `spab_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    assessment = CDec(dr.Item("Assessment").ToString)
                    institutionaldiscount = CDec(dr.Item("Institutional Discount").ToString)
                    downpayment = CDec(dr.Item("Down Payment").ToString)
                    additionaladjustment = CDec(dr.Item("Additional Adjustment").ToString)
                    lessadjustment = CDec(dr.Item("Less Adjustment").ToString)
                    totalassessment = CDec(dr.Item("Total Assessment").ToString)
                    balance = CDec(dr.Item("Total Balance").ToString)
                    totalpaid = CDec(dr.Item("Paid").ToString)
                    otherfees = CDec(dr.Item("Other Fees").ToString)
                    assessmentid = CInt(dr.Item("spab_ass_id").ToString)
                    additional_fees = CDec(dr.Item("Additional Fee (Uniforms, etc.)").ToString)
                    discount = CDec(dr.Item("discount").ToString)
                End If
                dr.Close()
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'PRELIM' AND afb_breakdown_period_date IS NOT NULL) AS PRELIM, (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'MID-TERM' AND afb_breakdown_period_date IS NOT NULL) AS MIDTERM, (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'SEMI-FINAL' AND afb_breakdown_period_date IS NOT NULL) AS 'SEMI-FINAL', (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'FINAL' AND afb_breakdown_period_date IS NOT NULL) AS FINAL;", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    prelim_date = dr.Item("PRELIM").ToString
                    midterm_date = dr.Item("MIDTERM").ToString
                    semifinal_date = dr.Item("SEMI-FINAL").ToString
                    final_date = dr.Item("FINAL").ToString
                End If
                dr.Close()
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT ifNULL(SUM(`Total Balance`),0) - ifNULL(SUM(`Excess`),0) as 'OldAccount' FROM `student_assessment_total` WHERE `spab_stud_id` = '" & studentId & "' and `spab_period_id` NOT IN(" & CInt(cbAcademicYear.SelectedValue) & ")", cn)
                oldaccount = CDec(cm.ExecuteScalar)
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT s_notes from tbl_student JOIN tbl_scholarship_status where tbl_student.s_scholarship = tbl_scholarship_status.scholar_id and s_id_no = '" & studentId & "'", cn)
                lackingcredentials = cm.ExecuteScalar
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT scholar_name from tbl_student JOIN tbl_scholarship_status where tbl_student.s_scholarship = tbl_scholarship_status.scholar_id and s_id_no = '" & studentId & "'", cn)
                scholarname = cm.ExecuteScalar
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT ROUND(aid_percentage, 2) from tbl_assessment_institutional_discount where aid_student_id = '" & studentId & "' and aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                prcnt = CDec(cm.ExecuteScalar)
                cn.Close()
                Dim prelimpercent As Decimal
                Dim midtermpercent As Decimal
                Dim semipercent As Decimal
                Dim finalpercent As Decimal

                cn.Open()
                cm = New MySqlCommand("SELECT af_prelim_percentage, af_midterm_percentage, af_semifinal_percentage, af_final_percentage from tbl_assessment_fee where af_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and af_id = " & assessmentid & "", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    prelimpercent = dr.Item("af_prelim_percentage").ToString
                    midtermpercent = dr.Item("af_midterm_percentage").ToString
                    semipercent = dr.Item("af_semifinal_percentage").ToString
                    finalpercent = dr.Item("af_final_percentage").ToString
                End If
                dr.Close()
                cn.Close()
                Dim totalaccountpaid As Decimal
                cn.Open()
                cm = New MySqlCommand("SELECT `Total Paid` as 'TotalPaid' FROM `student_assessment_total` WHERE `spab_stud_id` = '" & studentId & "' and `spab_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                totalaccountpaid = CDec(cm.ExecuteScalar)
                cn.Close()

                Dim totalcurrentassessment As Decimal
                Dim totalcurrent_assessment As Decimal
                Dim totalcurrent_balance As Decimal
                Dim total_assessment As Decimal
                Dim a As Decimal
                Dim b As Decimal
                Dim c As Decimal
                Dim d As Decimal
                Dim f As Decimal
                Dim g As Decimal
                Dim h As Decimal
                a = oldaccount
                b = assessment
                c = additionaladjustment
                d = balance
                f = totalassessment
                g = institutionaldiscount
                h = downpayment
                totalcurrent_assessment = b + c
                totalcurrent_balance = d
                total_assessment = f
                totalcurrentassessment = a + b + otherfees - g
                totalcurrent_assessment = (totalassessment + oldaccount) - h

                Dim total_prelim As Decimal
                Dim total_midterm As Decimal
                Dim total_semifinal As Decimal
                Dim total_final As Decimal
                Dim withdownpayment As Decimal
                Dim totalwithdownpayment As Decimal
                If downpayment >= 2000 Then
                    total_prelim = totalcurrent_assessment * prelimpercent
                    total_midterm = totalcurrent_assessment * midtermpercent
                    total_semifinal = totalcurrent_assessment * semipercent
                    total_final = totalcurrent_assessment * finalpercent
                ElseIf downpayment < 2000 Then
                    withdownpayment = totalcurrent_assessment - 2000 + downpayment
                    totalwithdownpayment = 2000 - downpayment
                    total_prelim = (withdownpayment * prelimpercent) + totalwithdownpayment
                    total_midterm = withdownpayment * midtermpercent
                    total_semifinal = withdownpayment * semipercent
                    total_final = withdownpayment * finalpercent
                End If

                Dim balance_prelim As Decimal
                Dim balance_midterm As Decimal
                Dim balance_semifinal As Decimal
                Dim balance_final As Decimal

                If totalaccountpaid > total_prelim Then
                    Dim subtractprelim As Decimal
                    subtractprelim = totalaccountpaid - total_prelim
                    If subtractprelim > total_midterm Then
                        Dim subtractmidterm As Decimal
                        subtractmidterm = subtractprelim - total_midterm
                        If subtractmidterm > total_semifinal Then
                            Dim subtractsemi As Decimal
                            subtractsemi = subtractmidterm - total_semifinal
                            If subtractsemi > total_final Then
                                Dim subtractfinal As Decimal
                                subtractfinal = subtractsemi - total_final
                                If subtractfinal <= total_final Then
                                    balance_prelim = "0.00"
                                    balance_midterm = "0.00"
                                    balance_semifinal = "0.00"
                                    balance_final = "0.00"
                                End If
                            Else
                                balance_prelim = "0.00"
                                balance_midterm = "0.00"
                                balance_semifinal = "0.00"
                                balance_final = total_final - subtractsemi
                            End If
                        Else
                            balance_prelim = "0.00"
                            balance_midterm = "0.00"
                            balance_semifinal = total_semifinal - subtractmidterm
                            balance_final = total_final
                        End If
                    Else
                        balance_prelim = "0.00"
                        balance_midterm = total_midterm - subtractprelim
                        balance_semifinal = total_semifinal
                        balance_final = total_final
                    End If
                Else
                    balance_prelim = total_prelim - totalaccountpaid
                    balance_midterm = total_midterm
                    balance_semifinal = total_semifinal
                    balance_final = total_final
                End If

                If frmMain.systemModule.Text = "College Module" Then
                    cn.Close()
                    cn.Open()
                    Dim studentAssessmentID As Integer = 0
                    cm = New MySqlCommand("SELECT ps_ass_id FROM `tbl_pre_cashiering` WHERE `student_id` = '" & studentId & "' and `period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                    studentAssessmentID = cm.ExecuteScalar
                    cn.Close()

                    cn.Open()
                    Dim asseessmentCategory As String = ""
                    cm = New MySqlCommand("SELECT af_gender FROM `tbl_assessment_fee` WHERE `af_id` = " & studentAssessmentID & "", cn)
                    asseessmentCategory = cm.ExecuteScalar
                    cn.Close()
                    cn.Open()

                    Dim dtable As DataTable
                    Dim sql As String

                    sql = "SELECT (ofsp_particular_id) as 'ID', (ap_particular_code) as 'Code', (ap_particular_name) as 'Particular', (ofsp_amount) as 'Amount' from tbl_assessment_ofs_particulars JOIN tbl_assessment_particulars where tbl_assessment_ofs_particulars.ofsp_particular_id = tbl_assessment_particulars.ap_id and ofsp_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and ofsp_course_id = " & courseId & " and ofsp_year_level = LEFT('" & studentGradeLevel & "', 8) and ofsp_gender ='" & asseessmentCategory & "'"

                    Dim dbcommand As New MySqlCommand(sql, cn)
                    Dim adt As New MySqlDataAdapter
                    adt.SelectCommand = dbcommand
                    dtable = New DataTable
                    adt.Fill(dtable)
                    dg_report2.DataSource = dtable
                    adt.Dispose()
                    dbcommand.Dispose()
                    cn.Close()

                    dt2.Columns.Clear()
                    dt2.Rows.Clear()
                    With dt2
                        .Columns.Add("name")
                        .Columns.Add("amount")
                    End With

                    For Each dr As DataGridViewRow In dg_report2.Rows
                        dt2.Rows.Add(dr.Cells(2).Value, dr.Cells(3).Value)
                    Next
                End If

                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                If frmMain.systemModule.Text = "College Module" Then
                    rptdoc = New StatementOfAccount6
                    rptdoc.Subreports(0).SetDataSource(dt2)
                Else
                    rptdoc = New StatementOfAccount4
                End If
                rptdoc.SetParameterValue("sname", studentName)
                rptdoc.SetParameterValue("sid", studentId)
                rptdoc.SetParameterValue("scourse_yrlvl", studentGradeLevel & " - " & studentGradeLevelCourseCode)
                rptdoc.SetParameterValue("prelim_date", Format(Convert.ToDateTime(prelim_date), "MMM yyyy"))
                rptdoc.SetParameterValue("midterm_date", Format(Convert.ToDateTime(midterm_date), "MMM yyyy"))
                rptdoc.SetParameterValue("semifinal_date", Format(Convert.ToDateTime(semifinal_date), "MMM yyyy"))
                rptdoc.SetParameterValue("final_date", Format(Convert.ToDateTime(final_date), "MMM yyyy"))
                rptdoc.SetParameterValue("prelim_balance", Format(balance_prelim, "n2"))
                rptdoc.SetParameterValue("midterm_balance", Format(balance_midterm, "n2"))
                rptdoc.SetParameterValue("semifinal_balance", Format(balance_semifinal, "n2"))
                rptdoc.SetParameterValue("final_balance", Format(balance_final, "n2"))
                rptdoc.SetParameterValue("otherdiscount", Format(discount, "n2"))
                rptdoc.SetParameterValue("oldaccounts", Format(oldaccount, "n2"))
                rptdoc.SetParameterValue("addadjustment", Format(additionaladjustment, "n2"))
                rptdoc.SetParameterValue("lessadjustment", Format(lessadjustment, "n2"))
                rptdoc.SetParameterValue("downpayment", Format(downpayment, "n2"))
                rptdoc.SetParameterValue("institutionaldiscount", Format(institutionaldiscount, "n2"))
                rptdoc.SetParameterValue("totalcurrentassessment", Format(totalcurrentassessment, "n2"))
                rptdoc.SetParameterValue("currentbalance", Format(((totalassessment + oldaccount) - totalaccountpaid) - downpayment, "n2"))
                rptdoc.SetParameterValue("president_admin", cbPresident.Text)
                rptdoc.SetParameterValue("prepared_by", str_name)
                rptdoc.SetParameterValue("currentassessment", Format(assessment, "n2"))
                rptdoc.SetParameterValue("payments", Format(totalaccountpaid, "n2"))
                rptdoc.SetParameterValue("currentperiod", cbAcademicYear.Text)
                rptdoc.SetParameterValue("lackingcredentials", lackingcredentials)
                rptdoc.SetParameterValue("otherfee", Format(otherfees, "n2"))
                rptdoc.SetParameterValue("scholarship", scholarname)
                rptdoc.SetParameterValue("discountpercent", Math.Round(prcnt * 100, 0) & "%")

                If petition > 0 Then
                    rptdoc.SetParameterValue("petitiontitle", "Add: Subject Fee: Regular -" & non_petition_no & " / Petition - " & petition_no & "")
                    rptdoc.SetParameterValue("petition_amount", Format(petition, "n2"))
                Else
                    rptdoc.SetParameterValue("petitiontitle", " ")
                    rptdoc.SetParameterValue("petition_amount", " ")
                End If

                rptdoc.SetParameterValue("additional_amount", Format(additional_fees, "n2"))
                rptdoc.SetParameterValue("date_generated", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
                ReportViewer.ReportSource = rptdoc
                dg_report2.DataSource = Nothing
                ReportViewer.Select()
                ReportGenerated = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
            PrevBtn()
        End Try
    End Sub

    Sub PrintSOAScholar()
        'Try
        cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT * from tbl_enrollment where estudent_id = '" & studentId & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                NextBtn()
                dr.Close()
                cn.Close()
                Dim assessment As Decimal
                Dim institutionaldiscount As Decimal
                Dim downpayment As Decimal
                Dim additionaladjustment As Decimal
                Dim lessadjustment As Decimal
                Dim totalassessment As Decimal
                Dim balance As Decimal
                Dim totalpaid As Decimal
                Dim otherfees As Decimal
                Dim prelim_date As String
                Dim midterm_date As String
                Dim semifinal_date As String
                Dim final_date As String
                Dim assessmentid As Integer
                Dim oldaccount As Decimal
                Dim lackingcredentials As String
                Dim scholarname As String
                Dim petition As Decimal
                Dim petition_no As Integer
                Dim additional_fees As Decimal
                Dim non_petition_no As Integer
                Dim prcnt As Decimal
                Dim discount As Decimal

                cn.Open()
                cm = New MySqlCommand("SELECT `Additional Fee (Subject Fee/Petition)` FROM `student_assessment_total` WHERE `spab_stud_id` =  '" & studentId & "' and `spab_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                petition = CDec(cm.ExecuteScalar)
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT COUNT(class_schedule_id) as 'ClassSchedule_ID' from tbl_students_grades JOIN tbl_class_schedule ON tbl_students_grades.sg_class_id = tbl_class_schedule.class_schedule_id and tbl_students_grades.sg_period_id = tbl_class_schedule.csperiod_id WHERE cs_is_petition = 'Yes' and `sg_student_id` =  '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                petition_no = CInt(cm.ExecuteScalar)
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT COUNT(class_schedule_id) as 'ClassSchedule_ID' from tbl_students_grades JOIN tbl_class_schedule ON tbl_students_grades.sg_class_id = tbl_class_schedule.class_schedule_id and tbl_students_grades.sg_period_id = tbl_class_schedule.csperiod_id WHERE cs_amount > 0 and cs_is_petition NOT IN ('Yes') and `sg_student_id` =  '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                non_petition_no = CInt(cm.ExecuteScalar)
                cn.Close()
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT `Academic Year`, `Assessment`, `Institutional Discount`, `Discounted Assessment`, `Other Fees`, `Additional Fee (Uniforms, etc.)`, `Additional Fee (Subject Fee/Petition)`, `Additional Adjustment`, `Less Adjustment`, `Down Payment`, `Total Assessment`, (`Total Paid` + `Down Payment`) as `Paid`, `Total Balance`, `Excess`, spab_ass_id, `Discount` FROM `student_assessment_total` WHERE `spab_stud_id` =  '" & studentId & "' and `spab_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    assessment = CDec(dr.Item("Assessment").ToString)
                    institutionaldiscount = CDec(dr.Item("Institutional Discount").ToString)

                'downpayment = CDec(dr.Item("Down Payment").ToString)
                downpayment = 0


                additionaladjustment = 0
                lessadjustment = 0
                    totalassessment = CDec(dr.Item("Total Assessment").ToString)
                    balance = CDec(dr.Item("Total Balance").ToString)
                    totalpaid = CDec(dr.Item("Paid").ToString)
                    otherfees = CDec(dr.Item("Other Fees").ToString)
                    assessmentid = CInt(dr.Item("spab_ass_id").ToString)
                    additional_fees = CDec(dr.Item("Additional Fee (Uniforms, etc.)").ToString)
                    discount = CDec(dr.Item("discount").ToString)
                End If
                dr.Close()
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'PRELIM' AND afb_breakdown_period_date IS NOT NULL) AS PRELIM, (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'MID-TERM' AND afb_breakdown_period_date IS NOT NULL) AS MIDTERM, (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'SEMI-FINAL' AND afb_breakdown_period_date IS NOT NULL) AS 'SEMI-FINAL', (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'FINAL' AND afb_breakdown_period_date IS NOT NULL) AS FINAL;", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    prelim_date = dr.Item("PRELIM").ToString
                    midterm_date = dr.Item("MIDTERM").ToString
                    semifinal_date = dr.Item("SEMI-FINAL").ToString
                    final_date = dr.Item("FINAL").ToString
                End If
                dr.Close()
                oldaccount = 0
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT s_notes from tbl_student JOIN tbl_scholarship_status where tbl_student.s_scholarship = tbl_scholarship_status.scholar_id and s_id_no = '" & studentId & "'", cn)
                lackingcredentials = cm.ExecuteScalar
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT scholar_name from tbl_student JOIN tbl_scholarship_status where tbl_student.s_scholarship = tbl_scholarship_status.scholar_id and s_id_no = '" & studentId & "'", cn)
                scholarname = cm.ExecuteScalar
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT ROUND(aid_percentage, 2) from tbl_assessment_institutional_discount where aid_student_id = '" & studentId & "' and aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                prcnt = CDec(cm.ExecuteScalar)
                cn.Close()
                Dim prelimpercent As Decimal
                Dim midtermpercent As Decimal
                Dim semipercent As Decimal
                Dim finalpercent As Decimal

                cn.Open()
                cm = New MySqlCommand("SELECT af_prelim_percentage, af_midterm_percentage, af_semifinal_percentage, af_final_percentage from tbl_assessment_fee where af_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and af_id = " & assessmentid & "", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    prelimpercent = dr.Item("af_prelim_percentage").ToString
                    midtermpercent = dr.Item("af_midterm_percentage").ToString
                    semipercent = dr.Item("af_semifinal_percentage").ToString
                    finalpercent = dr.Item("af_final_percentage").ToString
                End If
                dr.Close()
                cn.Close()
                Dim totalaccountpaid As Decimal
                cn.Open()
                cm = New MySqlCommand("SELECT `Total Paid` as 'TotalPaid' FROM `student_assessment_total` WHERE `spab_stud_id` = '" & studentId & "' and `spab_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            totalaccountpaid = CDec(cm.ExecuteScalar)

            totalaccountpaid = 0

            cn.Close()

            Dim totalcurrentassessment As Decimal
                Dim totalcurrent_assessment As Decimal
                Dim totalcurrent_balance As Decimal
                Dim total_assessment As Decimal
                Dim a As Decimal
                Dim b As Decimal
                Dim c As Decimal
                Dim d As Decimal
                Dim f As Decimal
                Dim g As Decimal
                Dim h As Decimal
                a = oldaccount
                b = assessment
                c = additionaladjustment
                d = balance
                f = totalassessment
                g = institutionaldiscount
                h = downpayment
                totalcurrent_assessment = b + c
                totalcurrent_balance = d
                total_assessment = f
            totalcurrentassessment = a + b + otherfees + c

            totalcurrentassessment = (assessment + otherfees + additional_fees + petition)
            totalcurrent_assessment = (assessment + otherfees + additional_fees + petition) - institutionaldiscount

            Dim total_prelim As Decimal
                Dim total_midterm As Decimal
                Dim total_semifinal As Decimal
                Dim total_final As Decimal
                Dim withdownpayment As Decimal
                Dim totalwithdownpayment As Decimal
                If downpayment >= 2000 Then
                    total_prelim = totalcurrent_assessment * prelimpercent
                    total_midterm = totalcurrent_assessment * midtermpercent
                    total_semifinal = totalcurrent_assessment * semipercent
                    total_final = totalcurrent_assessment * finalpercent
                ElseIf downpayment < 2000 Then
                    withdownpayment = totalcurrent_assessment - 2000 + downpayment
                    totalwithdownpayment = 2000 - downpayment
                    total_prelim = (withdownpayment * prelimpercent) + totalwithdownpayment
                    total_midterm = withdownpayment * midtermpercent
                    total_semifinal = withdownpayment * semipercent
                    total_final = withdownpayment * finalpercent
                End If

                Dim balance_prelim As Decimal
                Dim balance_midterm As Decimal
                Dim balance_semifinal As Decimal
                Dim balance_final As Decimal

                If totalaccountpaid > total_prelim Then
                    Dim subtractprelim As Decimal
                    subtractprelim = totalaccountpaid - total_prelim
                    If subtractprelim > total_midterm Then
                        Dim subtractmidterm As Decimal
                        subtractmidterm = subtractprelim - total_midterm
                        If subtractmidterm > total_semifinal Then
                            Dim subtractsemi As Decimal
                            subtractsemi = subtractmidterm - total_semifinal
                            If subtractsemi > total_final Then
                                Dim subtractfinal As Decimal
                                subtractfinal = subtractsemi - total_final
                                If subtractfinal <= total_final Then
                                    balance_prelim = "0.00"
                                    balance_midterm = "0.00"
                                    balance_semifinal = "0.00"
                                    balance_final = "0.00"
                                End If
                            Else
                                balance_prelim = "0.00"
                                balance_midterm = "0.00"
                                balance_semifinal = "0.00"
                                balance_final = total_final - subtractsemi
                            End If
                        Else
                            balance_prelim = "0.00"
                            balance_midterm = "0.00"
                            balance_semifinal = total_semifinal - subtractmidterm
                            balance_final = total_final
                        End If
                    Else
                        balance_prelim = "0.00"
                        balance_midterm = total_midterm - subtractprelim
                        balance_semifinal = total_semifinal
                        balance_final = total_final
                    End If
                Else
                    balance_prelim = total_prelim - totalaccountpaid
                    balance_midterm = total_midterm
                    balance_semifinal = total_semifinal
                    balance_final = total_final
                End If

            If frmMain.systemModule.Text = "College Module" Then
                cn.Close()
                cn.Open()
                Dim courseId As Integer = 0
                cm = New MySqlCommand("SELECT `sg_course_id` FROM `tbl_students_grades` WHERE `sg_student_id` = '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                courseId = cm.ExecuteScalar
                cn.Close()
                cn.Open()

                Dim dtable As DataTable
                Dim sql As String
                If studentGradeLevel.Contains("1st Year") Then
                    sql = "SELECT (ofsp_particular_id) as 'ID', (ap_particular_code) as 'Code', (ap_particular_name) as 'Particular', (ofsp_amount) as 'Amount' from tbl_assessment_ofs_particulars JOIN tbl_assessment_particulars where tbl_assessment_ofs_particulars.ofsp_particular_id = tbl_assessment_particulars.ap_id and ofsp_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and ofsp_course_id = " & courseId & " and ofsp_year_level = '1st Year' and ofsp_gender ='" & studentGender & "'"
                Else
                    sql = "SELECT (ofsp_particular_id) as 'ID', (ap_particular_code) as 'Code', (ap_particular_name) as 'Particular', (ofsp_amount) as 'Amount' from tbl_assessment_ofs_particulars JOIN tbl_assessment_particulars where tbl_assessment_ofs_particulars.ofsp_particular_id = tbl_assessment_particulars.ap_id and ofsp_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and ofsp_course_id = " & courseId & " and ofsp_year_level = LEFT('" & studentGradeLevel & "', 8) and ofsp_gender ='Both'"
                End If
                Dim dbcommand As New MySqlCommand(sql, cn)
                Dim adt As New MySqlDataAdapter
                adt.SelectCommand = dbcommand
                dtable = New DataTable
                adt.Fill(dtable)
                dg_report2.DataSource = dtable
                adt.Dispose()
                dbcommand.Dispose()
                cn.Close()

                dt2.Columns.Clear()
                dt2.Rows.Clear()
                With dt2
                    .Columns.Add("name")
                    .Columns.Add("amount")
                End With

                For Each dr As DataGridViewRow In dg_report2.Rows
                    dt2.Rows.Add(dr.Cells(2).Value, dr.Cells(3).Value)
                Next
            End If

            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New StatementOfAccount7
            rptdoc.SetParameterValue("sname", studentName)
            rptdoc.SetParameterValue("sid", studentId)
            rptdoc.SetParameterValue("scourse_yrlvl", studentGradeLevel & " - " & studentGradeLevelCourseCode)
            rptdoc.SetParameterValue("prelim_date", Format(Convert.ToDateTime(prelim_date), "MMM yyyy"))
                rptdoc.SetParameterValue("midterm_date", Format(Convert.ToDateTime(midterm_date), "MMM yyyy"))
                rptdoc.SetParameterValue("semifinal_date", Format(Convert.ToDateTime(semifinal_date), "MMM yyyy"))
                rptdoc.SetParameterValue("final_date", Format(Convert.ToDateTime(final_date), "MMM yyyy"))
                rptdoc.SetParameterValue("prelim_balance", Format(balance_prelim, "n2"))
                rptdoc.SetParameterValue("midterm_balance", Format(balance_midterm, "n2"))
                rptdoc.SetParameterValue("semifinal_balance", Format(balance_semifinal, "n2"))
                rptdoc.SetParameterValue("final_balance", Format(balance_final, "n2"))
                'rptdoc.SetParameterValue("otherdiscount", Format(discount, "n2"))
                'rptdoc.SetParameterValue("oldaccounts", Format(oldaccount, "n2"))
                'rptdoc.SetParameterValue("addadjustment", Format(additionaladjustment, "n2"))
                'rptdoc.SetParameterValue("lessadjustment", Format(lessadjustment, "n2"))
                rptdoc.SetParameterValue("downpayment", Format(downpayment, "n2"))
                rptdoc.SetParameterValue("institutionaldiscount", Format(institutionaldiscount, "n2"))
            rptdoc.SetParameterValue("totalcurrentassessment", Format(assessment - institutionaldiscount, "n2"))
            rptdoc.SetParameterValue("currentbalance", Format(((assessment - institutionaldiscount) - downpayment) - totalaccountpaid, "n2"))
            rptdoc.SetParameterValue("president_admin", "KRISTINE J. CABRERA, MBM, LPT")
            rptdoc.SetParameterValue("prepared_by", str_name)
            rptdoc.SetParameterValue("currentassessment", Format(assessment, "n2"))
            rptdoc.SetParameterValue("payments", Format(totalaccountpaid, "n2"))
                rptdoc.SetParameterValue("currentperiod", cbAcademicYear.Text)
                rptdoc.SetParameterValue("lackingcredentials", lackingcredentials)
                'rptdoc.SetParameterValue("otherfee", Format(otherfees, "n2"))
                rptdoc.SetParameterValue("scholarship", scholarname)
                rptdoc.SetParameterValue("discountpercent", Math.Round(prcnt * 100, 0) & "%")

            'If petition > 0 Then
            '    rptdoc.SetParameterValue("petitiontitle", "Add: Subject Fee: Regular -" & non_petition_no & " / Petition - " & petition_no & "")
            '    rptdoc.SetParameterValue("petition_amount", Format(petition, "n2"))
            'Else
            '    rptdoc.SetParameterValue("petitiontitle", " ")
            '    rptdoc.SetParameterValue("petition_amount", " ")
            'End If
            rptdoc.SetParameterValue("scourse", studentGradeLevelCourseName)
            'rptdoc.SetParameterValue("additional_amount", Format(additional_fees, "n2"))
            rptdoc.SetParameterValue("date_generated", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
                ReportViewer.ReportSource = rptdoc
                dg_report2.DataSource = Nothing
                ReportViewer.Select()
                ReportGenerated = True
            End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, vbCritical)
        '    cn.Close()
        '    PrevBtn()
        'End Try
    End Sub
End Class