Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Globalization
Imports System.Windows.Forms

Public Class frmEnrollStudent

    Public Shared StudentID As String = ""
    Public Shared StudentName As String = ""
    Public Shared CourseID As Integer = 0
    Public Shared Course As String = ""
    Public Shared YearLevel As String = ""
    Public Shared Gender As String = ""

    Public Shared StudentStatus As String = ""
    Public Shared CourseStatus As String = ""

    Public Shared StudentAssessmentID As Integer = 0

    Public Shared EnrollmentCode As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        frmTitle.Text = "Search Student"
        SearchPanel.Visible = True
        LibraryEnrollmentStudentList()
        dgStudentList.BringToFront()
        txtSearch.Select()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If frmTitle.Text = "Search Student" Then
            StudentName = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
            StudentID = dgStudentList.CurrentRow.Cells(1).Value
            CourseID = dgStudentList.CurrentRow.Cells(9).Value
            YearLevel = dgStudentList.CurrentRow.Cells(7).Value
            Gender = dgStudentList.CurrentRow.Cells(6).Value
            CourseStatus = dgStudentList.CurrentRow.Cells(11).Value
            StudentStatus = dgStudentList.CurrentRow.Cells(12).Value

            txtStudent.Text = dgStudentList.CurrentRow.Cells(1).Value & " - " & dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
            txtCourse.Text = dgStudentList.CurrentRow.Cells(8).Value & " - " & dgStudentList.CurrentRow.Cells(10).Value
            txtGenderYearLevel.Text = dgStudentList.CurrentRow.Cells(6).Value & " - " & dgStudentList.CurrentRow.Cells(7).Value

            If frmMain.systemModule.Text = "College Module" Then
                If YearLevel.Contains("1st Year") Then
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT af_id from tbl_assessment_fee where af_period_id = " & CInt(cbAcad.SelectedValue) & " and af_course_id = " & CourseID & " and af_year_level = LEFT('" & YearLevel & "', 8) and af_gender = '" & Gender & "'", cn)
                    StudentAssessmentID = cm.ExecuteScalar
                    cn.Close()
                Else
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT af_id from tbl_assessment_fee where af_period_id = " & CInt(cbAcad.SelectedValue) & " and af_course_id = " & CourseID & " and af_year_level = LEFT('" & YearLevel & "', 8) and af_gender = 'Both'", cn)
                    StudentAssessmentID = cm.ExecuteScalar
                    cn.Close()
                End If
            Else
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT af_id from tbl_assessment_fee where af_period_id = " & CInt(cbAcad.SelectedValue) & " and af_course_id = " & CourseID & " and af_year_level = '" & YearLevel & "' and af_gender = 'Both'", cn)
                StudentAssessmentID = cm.ExecuteScalar
                cn.Close()
            End If

            If frmMain.formTitle.Text = "Enroll Class Schedule" Then

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT * FROM tbl_students_grades WHERE sg_student_id = '" & StudentID & "' and sg_period_id = " & CInt(cbAcad.SelectedValue) & "", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    dr.Close()
                    cn.Close()
                    MsgBox("Student " & StudentName & " with ID Number " & StudentID & " already has grades or is enrolled for this Academic Year.'", vbCritical)
                    ClearAll()
                Else

                    dr.Close()
                    cn.Close()
                    dgStudentSched.Rows.Clear()

                    Dim sql As String
                    sql = "SELECT (class_schedule_id) as 'ID', (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (subject_units) as 'Units', (ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (room_code) as 'Room', (Instructor) as 'Instructor', population, csperiod_id from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_enrollment_subjects where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_enrollment_subjects.es_class_schedule_id and tbl_enrollment_subjects.es_student_id = '" & StudentID & "' and tbl_enrollment_subjects.es_period_id = " & CInt(cbAcad.SelectedValue) & ";"
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand(sql, cn)
                    dr = cm.ExecuteReader
                    While dr.Read
                        dgStudentSched.Rows.Add(dr.Item("ID").ToString, dr.Item("Class").ToString, dr.Item("Subject Code").ToString, dr.Item("Subject Desc.").ToString, dr.Item("Units").ToString, dr.Item("Days").ToString, dr.Item("Start Time").ToString, dr.Item("End Time").ToString, dr.Item("Room").ToString, dr.Item("Instructor").ToString)
                    End While
                    dr.Close()
                    cn.Close()

                    MsgBox("Subject schedules from the evaluation process have been successfully fetched.", vbInformation)
                End If

            ElseIf frmMain.formTitle.Text = "Update Class Schedule" Then
                dgStudentSched.Rows.Clear()
                Dim sql As String
                sql = "SELECT (class_schedule_id) as 'ID', (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (subject_units) as 'Units', (ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (room_code) as 'Room', (Instructor) as 'Instructor', population, csperiod_id from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_enrollment, tbl_students_grades where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_students_grades.sg_class_id and tbl_enrollment.estudent_id = tbl_students_grades.sg_student_id and tbl_class_schedule.csperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.estudent_id = '" & StudentID & "' and tbl_enrollment.eperiod_id = " & CInt(cbAcad.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled'"
                cn.Close()
                cn.Open()
                cm = New MySqlCommand(sql, cn)
                dr = cm.ExecuteReader
                While dr.Read
                    dgStudentSched.Rows.Add(dr.Item("ID").ToString, dr.Item("Class").ToString, dr.Item("Subject Code").ToString, dr.Item("Subject Desc.").ToString, dr.Item("Units").ToString, dr.Item("Days").ToString, dr.Item("Start Time").ToString, dr.Item("End Time").ToString, dr.Item("Room").ToString, dr.Item("Instructor").ToString)
                End While
                dr.Close()
                cn.Close()
            End If
            SearchPanel.Visible = False
        ElseIf frmTitle.Text = "Search Class Schedule" Then

            Dim population As Integer = 0
            Dim enrolled As Integer = 0
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT population from tbl_class_schedule where class_schedule_id = " & CInt(dgClassSchedList.CurrentRow.Cells(0).Value) & "", cn)
            population = cm.ExecuteScalar
            cn.Close()
            enrolled = CountEnrolled(CInt(dgClassSchedList.CurrentRow.Cells(0).Value))

            If enrolled >= population Then
                MessageBox.Show("Subject ''" & dgClassSchedList.CurrentRow.Cells(2).Value & " - " & dgClassSchedList.CurrentRow.Cells(3).Value & "'' enrolled population is already full.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else

                Dim isFound As Boolean = False

                Dim selectedClassID As Integer
                Dim selectedDay As String
                Dim selectedStartTime As String
                Dim selectedEndTime As String

                selectedClassID = CInt(dgClassSchedList.CurrentRow.Cells(0).Value)
                selectedDay = dgClassSchedList.CurrentRow.Cells(5).Value.ToString
                selectedStartTime = dgClassSchedList.CurrentRow.Cells(6).Value.ToString
                selectedEndTime = dgClassSchedList.CurrentRow.Cells(7).Value.ToString

                Dim selectedStartTimeSpan As TimeSpan = ParseTime(selectedStartTime)
                Dim selectedEndTimeSpan As TimeSpan = ParseTime(selectedEndTime)

                Dim selectedDaysArray As New List(Of String)(selectedDay.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))

                For Each row As DataGridViewRow In dgStudentSched.Rows
                    Dim classSubject As String = row.Cells(3).Value.ToString
                    Dim classID As Integer = Convert.ToInt32(row.Cells(0).Value)
                    Dim day As String = row.Cells(5).Value.ToString()
                    Dim startTime As String = row.Cells(6).Value.ToString()
                    Dim endTime As String = row.Cells(7).Value.ToString()

                    Dim startTimeSpan As TimeSpan = ParseTime(startTime)
                    Dim endTimeSpan As TimeSpan = ParseTime(endTime)

                    Dim daysArray As New List(Of String)(day.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))

                    If classID = selectedClassID Then
                        isFound = True
                        MsgBox("Same Class Schedule Exist. Conflict found with Class: " & classSubject & " on Day(s): " & day & " between Starting Time: " & startTime & " and End Time: " & endTime, vbCritical)
                        Exit For
                    ElseIf classID <> selectedClassID Then
                        Dim DaysIntersect = selectedDaysArray.Intersect(daysArray).Count() > 0
                        If DaysIntersect = True AndAlso ((selectedStartTimeSpan < endTimeSpan AndAlso selectedEndTimeSpan > startTimeSpan) OrElse (startTimeSpan < selectedEndTimeSpan AndAlso endTimeSpan > selectedStartTimeSpan)) Then
                            isFound = True
                            MsgBox("Conflict found with Class: " & classSubject & " on Day(s): " & day & " between Starting Time: " & startTime & " and End Time: " & endTime, vbCritical)
                            Exit For
                        End If
                    End If
                Next

                If isFound = True Then
                Else
                    dgStudentSched.Rows.Add(dgClassSchedList.CurrentRow.Cells(0).Value, dgClassSchedList.CurrentRow.Cells(1).Value, dgClassSchedList.CurrentRow.Cells(2).Value, dgClassSchedList.CurrentRow.Cells(3).Value, dgClassSchedList.CurrentRow.Cells(4).Value, dgClassSchedList.CurrentRow.Cells(5).Value, dgClassSchedList.CurrentRow.Cells(6).Value, dgClassSchedList.CurrentRow.Cells(7).Value, dgClassSchedList.CurrentRow.Cells(8).Value, dgClassSchedList.CurrentRow.Cells(9).Value, dgClassSchedList.CurrentRow.Cells(10).Value)
                    SearchPanel.Visible = False
                End If
            End If
        End If
    End Sub

    Function ParseTime(timeString As String) As TimeSpan
        Dim parts = timeString.Split(":"c)
        Dim hours As Integer
        If Integer.TryParse(parts(0), hours) Then
            Dim minutes As Integer
            If Integer.TryParse(parts(1), minutes) Then
                Dim amPM = parts(2).Split(" "c)
                Dim am As Boolean = amPM(1).ToUpper() = "AM"
                If am AndAlso hours = 12 Then
                    hours = 0
                ElseIf Not am AndAlso hours < 12 Then
                    hours += 12
                End If
                Return New TimeSpan(hours, minutes, 0)
            End If
        End If
        ' Return TimeSpan.Zero if parsing fails
        Return TimeSpan.Zero
    End Function

    Private Sub txtCourse_TextChanged(sender As Object, e As EventArgs) Handles txtCourse.TextChanged
        fillCombo("Select DISTINCT(class_block_id) as id, cb_code, cb_year_level, csperiod_id, cb_course_id from tbl_class_schedule, tbl_class_block where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and csperiod_id = " & CInt(cbAcad.SelectedValue) & " and cb_year_level = LEFT('" & YearLevel & "', 8) and cb_course_id = " & CourseID & " order by cb_code asc", cbSection, "tbl_class_schedule", "cb_code", "id")
    End Sub

    Private Sub cbSetSched_Click(sender As Object, e As EventArgs) Handles cbSetSched.Click
        If StudentID = String.Empty Then
            cbSetSched.Checked = False
        Else
            If cbSetSched.Checked = True Then
                If MsgBox("Are you sure you want to set schedule base on section selected?", vbYesNo + vbQuestion) = vbYes Then
                    EnrollmentSubjectListPerSection()
                    cbSetSched.Checked = True
                Else
                    cbSetSched.Checked = False
                End If
            Else
                If MsgBox("Are you sure you want to set reset schedule?", vbYesNo + vbQuestion) = vbYes Then
                    cbSetSched.Checked = False
                    dgStudentSched.Rows.Clear()
                Else
                    cbSetSched.Checked = True
                End If
            End If
        End If
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub reload_Click(sender As Object, e As EventArgs) Handles reload.Click
        If frmMain.formTitle.Text = "Enroll Class Schedule" Then
            fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", cbAcad, "tbl_period", "PERIOD", "period_id")
        ElseIf frmMain.formTitle.Text = "Update Class Schedule" Then
            fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_ad_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", cbAcad, "tbl_period", "PERIOD", "period_id")
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles btnFetchSched.Click
        EnrollmentSubjectListPerSection()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnAddSched.Click
        If txtStudent.Text = String.Empty Then
        Else
            frmTitle.Text = "Search Class Schedule"
            SearchPanel.Visible = True
            LibraryEnrollmentClassSchedList()
            dgClassSchedList.BringToFront()
            txtSearch.Select()
        End If
    End Sub

    Function CountEnrolled(ByVal classID As Integer) As Integer
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT ifnull(COUNT(sg_class_id),0) as 'Enrolled' FROM tbl_students_grades where sg_class_id = " & classID & "", cn)
        CountEnrolled = CInt(cm.ExecuteScalar)
        cn.Close()
        Return CountEnrolled
    End Function

    Private Sub dgClassSchedList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgClassSchedList.CellContentClick
        dgClassSchedList.CurrentRow.Cells(11).Value = CountEnrolled(CInt(dgClassSchedList.CurrentRow.Cells(0).Value))
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            dgStudentSched.Rows.RemoveAt(dgStudentSched.CurrentRow.Index)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgStudentSched_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgStudentSched.RowsAdded
        Dim rowCount = dgStudentSched.Rows.Count
        totalSubjects.Text = rowCount

        Dim columnIndex As Integer = 4
        Dim columnSum As Double = GetColumnSum(dgStudentSched, columnIndex)
        totalUnits.Text = columnSum
    End Sub

    Private Sub dgStudentSched_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgStudentSched.RowsRemoved
        Dim rowCount = dgStudentSched.Rows.Count
        totalSubjects.Text = rowCount

        Dim columnIndex As Integer = 4
        Dim columnSum As Double = GetColumnSum(dgStudentSched, columnIndex)
        totalUnits.Text = columnSum
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If StudentID = String.Empty Then
            MessageBox.Show("No student selected.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            cn.Close()
            cn.Open()
            Dim sql As String
            If frmMain.formTitle.Text = "Enroll Class Schedule" Then
                sql = "SELECT period_enrollment_status FROM tbl_period where period_id  = " & CInt(cbAcad.SelectedValue) & " and period_enrollment_status = 'CLOSE'"
            ElseIf frmMain.formTitle.Text = "Update Class Schedule" Then
                sql = "SELECT period_enrollment_ad_status FROM tbl_period where period_id  = " & CInt(cbAcad.SelectedValue) & " and period_enrollment_status = 'CLOSE'"
            End If
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Close()
                If frmMain.formTitle.Text = "Enroll Class Schedule" Then
                    MsgBox("Enrollment for Academic Year " & cbAcad.Text & " is close. Please contact Registrar to gain access.", vbExclamation)
                ElseIf frmMain.formTitle.Text = "Update Class Schedule" Then
                    MsgBox("Enrollment Update for Academic Year " & cbAcad.Text & " is close. Please contact Registrar to gain access.", vbExclamation)
                End If
            Else
                dr.Close()
                Dim drr As DialogResult
                drr = MessageBox.Show("Are you sure you want to save this student " & StudentName & " enrollment with the following subjects for academic year " & cbAcad.Text & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If drr = DialogResult.No Then
                    MsgBox("Enroll cancelled.", vbExclamation)
                Else
                    If dgStudentSched.RowCount = 0 Then
                        MessageBox.Show("No subjects found.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ElseIf StudentAssessmentID = 0 Then
                        MessageBox.Show("Course Assessment Invalid. There might be no assessment made for this student course and year level. Please contact cashier for course assessment.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        If frmMain.systemModule.Text = "College Module" Then
                            If dgStudentSched.RowCount > 10 Then
                                MessageBox.Show("Enrollment failed. Cannot enroll more than 10 subjects.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Else
                                enrollstudent()
                            End If
                        Else
                            If dgStudentSched.RowCount > 12 Then
                                MessageBox.Show("Enrollment failed. Cannot enroll more than 12 subjects.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Else
                                enrollstudent()
                            End If
                        End If
                    End If
                End If
            End If
            cn.Close()
        End If
    End Sub

    Sub enrollstudent()
        Dim OverlapIsFound As Boolean = False
        Dim PopulationIsFound As Boolean = False
        For Each row As DataGridViewRow In dgStudentSched.Rows
            Dim population As Integer = 0
            Dim enrolled As Integer = 0
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT population from tbl_class_schedule where class_schedule_id = " & CInt(row.Cells(0).Value) & "", cn)
            population = cm.ExecuteScalar
            cn.Close()
            enrolled = CountEnrolled(CInt(row.Cells(0).Value))

            For Each row2 As DataGridViewRow In dgStudentSched.Rows
                If frmMain.formTitle.Text = "Update Class Schedule" Then
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT sg_class_id FROM tbl_students_grades WHERE sg_student_id = '" & StudentID & "' and sg_period_id = " & CInt(cbAcad.SelectedValue) & " and sg_class_id = " & CInt(row2.Cells(0).Value) & "", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        dr.Close()
                        If enrolled = population Then
                            enrolled = enrolled - 1
                        End If
                    Else
                        dr.Close()
                    End If
                    cn.Close()
                End If

                If enrolled >= population Then
                    PopulationIsFound = True
                    MessageBox.Show("Subject ''" & row.Cells(2).Value & " - " & row.Cells(3).Value & "'' enrolled population is already full.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit For
                End If
            Next
        Next

        If (PopulationIsFound) Then

        Else
            If frmMain.formTitle.Text = "Enroll Class Schedule" Then
                AutoEnrollmentCode()
                PerformEnrollTransaction()
            ElseIf frmMain.formTitle.Text = "Update Class Schedule" Then
                PerformEnrollUpdateTransaction()
            End If

            Try
                StudentCOR()
            Catch ex As Exception
                MessageBox.Show("No Crystal Report found. Please install crystall report from application directory", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            ClearAll()
        End If

    End Sub

    Private Sub PerformEnrollTransaction()
        cn.Close()
        cn.Open()
        Dim enroll_transaction As MySqlTransaction = cn.BeginTransaction()
        Try
            Using insert_enrollment_Cmd As MySqlCommand = cn.CreateCommand()
                insert_enrollment_Cmd.Transaction = enroll_transaction
                insert_enrollment_Cmd.CommandText = "INSERT INTO tbl_enrollment (enrollment_code, estudent_id, eperiod_id, ecurrriculum_id, etotal_units, etotal_subjects, eenrolledby_id, eenrolledby_datetime, e_yearlevel, enrollment_status, ecourse_status) values (@enrollment_code, @estudent_id, @eperiod_id, @ecurrriculum_id, @etotal_units, @etotal_subjects, @eenrolledby_id, curdate(), @e_yearlevel, @enrollment_status, @ecourse_status)"
                insert_enrollment_Cmd.Parameters.AddWithValue("@enrollment_code", EnrollmentCode)
                insert_enrollment_Cmd.Parameters.AddWithValue("@estudent_id", StudentID)
                insert_enrollment_Cmd.Parameters.AddWithValue("@eperiod_id", CInt(cbAcad.SelectedValue))
                insert_enrollment_Cmd.Parameters.AddWithValue("@ecurrriculum_id", "")
                insert_enrollment_Cmd.Parameters.AddWithValue("@etotal_units", CInt(totalUnits.Text))
                insert_enrollment_Cmd.Parameters.AddWithValue("@etotal_subjects", CInt(totalSubjects.Text))
                insert_enrollment_Cmd.Parameters.AddWithValue("@eenrolledby_id", str_userid)

                insert_enrollment_Cmd.Parameters.AddWithValue("@e_yearlevel", YearLevel)
                insert_enrollment_Cmd.Parameters.AddWithValue("@enrollment_status", "Enrolled")
                insert_enrollment_Cmd.Parameters.AddWithValue("@ecourse_status", CourseStatus)
                insert_enrollment_Cmd.Parameters.AddWithValue("@ecourse_status", CourseStatus)
                insert_enrollment_Cmd.ExecuteNonQuery()
            End Using

            Dim subject_id As Integer
            Dim scholar As String
            Dim aidp As Decimal
            For Each row As DataGridViewRow In dgStudentSched.Rows
                cm = New MySqlCommand("SELECT cssubject_id from tbl_class_schedule where class_schedule_id = '" & row.Cells(0).Value & "'", cn)
                subject_id = cm.ExecuteScalar
                Using insert_subjects_Cmd As MySqlCommand = cn.CreateCommand()
                    insert_subjects_Cmd.Transaction = enroll_transaction
                    insert_subjects_Cmd.CommandText = "INSERT INTO tbl_students_grades (sg_student_id, sg_course_id, sg_class_id, sg_subject_id, sg_grade, sg_period_id, sg_yearlevel, sg_grade_status) values (@sg_student_id, @sg_course_id, @sg_class_id, @sg_subject_id, @sg_grade, @sg_period_id, @sg_yearlevel, @sg_grade_status); UPDATE `tbl_enrollment_subjects` SET `es_status` = 'Enrolled' WHERE `es_student_id` = @sg_student_id and `es_class_schedule_id` = @sg_class_id and `es_period_id` = @sg_period_id;"
                    insert_subjects_Cmd.Parameters.AddWithValue("@sg_student_id", StudentID)
                    insert_subjects_Cmd.Parameters.AddWithValue("@sg_class_id", row.Cells(0).Value)
                    insert_subjects_Cmd.Parameters.AddWithValue("@sg_course_id", CourseID)
                    insert_subjects_Cmd.Parameters.AddWithValue("@sg_subject_id", subject_id)
                    insert_subjects_Cmd.Parameters.AddWithValue("@sg_grade", "")
                    insert_subjects_Cmd.Parameters.AddWithValue("@sg_period_id", CInt(cbAcad.SelectedValue))
                    insert_subjects_Cmd.Parameters.AddWithValue("@sg_yearlevel", YearLevel)
                    insert_subjects_Cmd.Parameters.AddWithValue("@sg_grade_status", "Enrolled")
                    insert_subjects_Cmd.ExecuteNonQuery()
                End Using
            Next

            Using insert_account_Cmd As MySqlCommand = cn.CreateCommand()
                insert_account_Cmd.Transaction = enroll_transaction
                insert_account_Cmd.CommandText = "INSERT INTO tbl_student_paid_account_breakdown (spab_period_id, spab_stud_id, spab_prelim, spab_midterm, spab_semifinal, spab_final, spab_total_paid, spab_ass_id) values (@1, @2, @3, @4, @5, @6, @7, @8)"
                insert_account_Cmd.Parameters.AddWithValue("@1", CInt(cbAcad.SelectedValue))
                insert_account_Cmd.Parameters.AddWithValue("@2", StudentID)
                insert_account_Cmd.Parameters.AddWithValue("@3", "")
                insert_account_Cmd.Parameters.AddWithValue("@4", "")
                insert_account_Cmd.Parameters.AddWithValue("@5", "")
                insert_account_Cmd.Parameters.AddWithValue("@6", "")
                insert_account_Cmd.Parameters.AddWithValue("@7", "")
                insert_account_Cmd.Parameters.AddWithValue("@8", StudentAssessmentID)
                insert_account_Cmd.ExecuteNonQuery()
            End Using

            cm = New MySqlCommand("SELECT scholar_name from tbl_student join tbl_scholarship_status where tbl_student.s_scholarship = tbl_scholarship_status.scholar_id and s_id_no = '" & StudentID & "'", cn)
            scholar = cm.ExecuteScalar

            cm = New MySqlCommand("SELECT af_institutional_discount_percent from tbl_assessment_fee where af_id = " & StudentAssessmentID & "", cn)
            aidp = cm.ExecuteScalar

            If scholar.Contains("Tertiary Education Subsidy") Or scholar.Contains("EDUKAR") Then
                aidp = 0
            End If

            cm = New MySqlCommand("SELECT * FROM tbl_assessment_institutional_discount  WHERE aid_student_id  = '" & StudentID & "' AND aid_period_id  = " & CInt(cbAcad.SelectedValue) & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Close()
                Using discount_update_Cmd As MySqlCommand = cn.CreateCommand()
                    discount_update_Cmd.Transaction = enroll_transaction
                    discount_update_Cmd.CommandText = "UPDATE tbl_assessment_institutional_discount set aid_assessment_id = " & StudentAssessmentID & ", aid_percentage = @row  where aid_student_id  = '" & StudentID & "' AND aid_period_id  = " & CInt(cbAcad.SelectedValue) & ""
                    discount_update_Cmd.Parameters.AddWithValue("@row", aidp)
                    discount_update_Cmd.ExecuteNonQuery()
                End Using
            Else
                dr.Close()
                Using discount_add_Cmd As MySqlCommand = cn.CreateCommand()
                    discount_add_Cmd.Transaction = enroll_transaction
                    discount_add_Cmd.CommandText = "INSERT INTO tbl_assessment_institutional_discount (aid_student_id, aid_period_id, aid_percentage, aid_assessment_id) VALUES (@aid_student_id, @aid_period_id, @aid_percentage, @aid_assessment_id)"
                    discount_add_Cmd.Parameters.AddWithValue("@aid_student_id", StudentID)
                    discount_add_Cmd.Parameters.AddWithValue("@aid_period_id", CInt(cbAcad.SelectedValue))
                    discount_add_Cmd.Parameters.AddWithValue("@aid_percentage", aidp)
                    discount_add_Cmd.Parameters.AddWithValue("@aid_assessment_id", StudentAssessmentID)
                    discount_add_Cmd.ExecuteNonQuery()
                End Using
            End If

            enroll_transaction.Commit()

            MessageBox.Show("Student successfully enrolled.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            UserActivity("Enrolled student " & StudentID & " " & StudentName & " in Academic Year " & cbAcad.Text & ".", "ENROLLLMENT")
        Catch
            enroll_transaction.Rollback()
            MessageBox.Show("Student failed to enroll. Enrollment transaction rolled back.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        cn.Close()
    End Sub

    Private Sub PerformEnrollUpdateTransaction()
        Try
            load_datagrid("SELECT sg_class_id from tbl_students_grades where sg_period_id = " & CInt(cbAcad.SelectedValue) & " and sg_student_id = '" & StudentID & "' and sg_grade_status = 'Enrolled'", dg_prev_subj)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        cn.Close()
        cn.Open()
        Dim enroll_transaction As MySqlTransaction = cn.BeginTransaction()
        Try
            Using students_enrollment_update_cmd As MySqlCommand = cn.CreateCommand()
                students_enrollment_update_cmd.Transaction = enroll_transaction
                students_enrollment_update_cmd.CommandText = "UPDATE tbl_enrollment SET etotal_subjects=@etotal_subjects, etotal_units=@etotal_units, e_yearlevel = @e_yearlevel, eenrolledby_id=@eenrolledby_id where eperiod_id = " & CInt(cbAcad.SelectedValue) & " and estudent_id = '" & StudentID & "'"
                students_enrollment_update_cmd.Parameters.AddWithValue("@etotal_subjects", totalSubjects.Text)
                students_enrollment_update_cmd.Parameters.AddWithValue("@etotal_units", totalUnits.Text)
                students_enrollment_update_cmd.Parameters.AddWithValue("@e_yearlevel", YearLevel)
                students_enrollment_update_cmd.Parameters.AddWithValue("@eenrolledby_id", str_userid)
                students_enrollment_update_cmd.ExecuteNonQuery()
            End Using

            For Each rw1 As DataGridViewRow In dgStudentSched.Rows
                Dim IsFound As Boolean = False
                For Each rw2 As DataGridViewRow In dg_prev_subj.Rows
                    If rw1.Cells(0).Value = rw2.Cells(0).Value Then
                        IsFound = True
                    Else
                    End If
                Next
                If IsFound = True Then
                Else
                    Dim subject_id As Integer
                    cm = New MySqlCommand("SELECT cssubject_id from tbl_class_schedule where class_schedule_id = '" & rw1.Cells(0).Value & "'", cn)
                    subject_id = cm.ExecuteScalar

                    Using insert_subjects_Cmd As MySqlCommand = cn.CreateCommand()
                        insert_subjects_Cmd.Transaction = enroll_transaction
                        insert_subjects_Cmd.CommandText = "INSERT INTO tbl_students_grades (sg_student_id, sg_course_id, sg_class_id, sg_subject_id, sg_grade, sg_period_id, sg_yearlevel, sg_grade_status) values (@sg_student_id, @sg_course_id, @sg_class_id, @sg_subject_id, @sg_grade, @sg_period_id, @sg_yearlevel, @sg_grade_status)"
                        insert_subjects_Cmd.Parameters.AddWithValue("@sg_student_id", StudentID)
                        insert_subjects_Cmd.Parameters.AddWithValue("@sg_class_id", rw1.Cells(0).Value)
                        insert_subjects_Cmd.Parameters.AddWithValue("@sg_course_id", CourseID)
                        insert_subjects_Cmd.Parameters.AddWithValue("@sg_subject_id", subject_id)
                        insert_subjects_Cmd.Parameters.AddWithValue("@sg_grade", "")
                        insert_subjects_Cmd.Parameters.AddWithValue("@sg_period_id", CInt(cbAcad.SelectedValue))
                        insert_subjects_Cmd.Parameters.AddWithValue("@sg_yearlevel", YearLevel)
                        insert_subjects_Cmd.Parameters.AddWithValue("@sg_grade_status", "Enrolled")
                        insert_subjects_Cmd.ExecuteNonQuery()
                    End Using

                End If
            Next

            For Each rw1 As DataGridViewRow In dg_prev_subj.Rows
                Dim IsFound As Boolean = False
                For Each rw2 As DataGridViewRow In dgStudentSched.Rows
                    If rw1.Cells(0).Value = rw2.Cells(0).Value Then
                        IsFound = True
                    Else

                    End If
                Next
                If IsFound = True Then
                Else
                    Using student_dropped_subjects_cmd As MySqlCommand = cn.CreateCommand()
                        student_dropped_subjects_cmd.Transaction = enroll_transaction
                        student_dropped_subjects_cmd.CommandText = "insert into tbl_student_dropped_subjects (`class_id`, `period_id`, `student_id`, `dropped_by`) values (" & rw1.Cells(0).Value & ", " & CInt(cbAcad.SelectedValue) & ", '" & StudentID & "', " & str_userid & ")"
                        student_dropped_subjects_cmd.ExecuteNonQuery()
                    End Using

                    cm = New MySqlCommand("SELECT * FROM tbl_period WHERE period_enrollment_enddate >= CURDATE() AND period_id  = " & CInt(cbAcad.SelectedValue) & "", cn)
                    Dim sdr0 As MySqlDataReader = cm.ExecuteReader()
                    If (sdr0.Read() = True) Then
                        sdr0.Dispose()
                        Using students_grades_cmd As MySqlCommand = cn.CreateCommand()
                            students_grades_cmd.Transaction = enroll_transaction
                            students_grades_cmd.CommandText = "delete from tbl_students_grades where sg_student_id = " & StudentID & " and sg_period_id = " & CInt(cbAcad.SelectedValue) & " and sg_class_id = " & rw1.Cells(0).Value & ""
                            students_grades_cmd.ExecuteNonQuery()
                        End Using
                    Else
                        sdr0.Dispose()
                        Using students_grades_cmd As MySqlCommand = cn.CreateCommand()
                            students_grades_cmd.Transaction = enroll_transaction
                            students_grades_cmd.CommandText = "Update tbl_students_grades set sg_grade = 'D', sg_credits = '0' where sg_student_id = " & StudentID & " and sg_period_id = " & CInt(cbAcad.SelectedValue) & " and sg_class_id = " & rw1.Cells(0).Value & ""
                            students_grades_cmd.ExecuteNonQuery()
                        End Using
                    End If
                End If
            Next

            Using student_account_cmd As MySqlCommand = cn.CreateCommand()
                student_account_cmd.Transaction = enroll_transaction
                student_account_cmd.CommandText = "UPDATE tbl_student_paid_account_breakdown SET spab_ass_id = @spab_ass_id where spab_stud_id = @spab_stud_id and spab_period_id = @spab_period_id"
                student_account_cmd.Parameters.AddWithValue("@spab_ass_id", StudentAssessmentID)
                student_account_cmd.Parameters.AddWithValue("@spab_stud_id", StudentID)
                student_account_cmd.Parameters.AddWithValue("@spab_period_id", CInt(cbAcad.SelectedValue))
                student_account_cmd.ExecuteNonQuery()
            End Using

            Using student_precash_cmd As MySqlCommand = cn.CreateCommand()
                student_precash_cmd.Transaction = enroll_transaction
                student_precash_cmd.CommandText = "UPDATE tbl_pre_cashiering SET ps_ass_id = @ps_ass_id, ps_yrlvl = @ps_yrlvl where student_id = @student_id and period_id = @period_id"
                student_precash_cmd.Parameters.AddWithValue("@ps_ass_id", StudentAssessmentID)
                student_precash_cmd.Parameters.AddWithValue("@student_id", StudentID)
                student_precash_cmd.Parameters.AddWithValue("@period_id", CInt(cbAcad.SelectedValue))
                student_precash_cmd.Parameters.AddWithValue("@ps_yrlvl", YearLevel)
                student_precash_cmd.ExecuteNonQuery()
            End Using


            Dim s As String
            cm = New MySqlCommand("SELECT scholar_name from tbl_student join tbl_scholarship_status where tbl_student.s_scholarship = tbl_scholarship_status.scholar_id and s_id_no = '" & StudentID & "'", cn)
            s = cm.ExecuteScalar

            Dim aidp As Decimal
            cm = New MySqlCommand("SELECT af_institutional_discount_percent from tbl_assessment_fee where af_id = " & StudentAssessmentID & "", cn)
            aidp = cm.ExecuteScalar
            If s.Contains("Tertiary Education Subsidy") Or s.Contains("EDUKAR") Then
                aidp = 0
            End If

            cm = New MySqlCommand("SELECT * FROM tbl_assessment_institutional_discount  WHERE aid_student_id  = '" & StudentID & "' AND aid_period_id  = " & CInt(cbAcad.SelectedValue) & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Close()
                Using discount_update_Cmd As MySqlCommand = cn.CreateCommand()
                    discount_update_Cmd.Transaction = enroll_transaction
                    discount_update_Cmd.CommandText = "UPDATE tbl_assessment_institutional_discount set aid_assessment_id = " & StudentAssessmentID & ", aid_percentage = @row  where aid_student_id  = '" & StudentID & "' AND aid_period_id  = " & CInt(cbAcad.SelectedValue) & ""
                    discount_update_Cmd.Parameters.AddWithValue("@row", aidp)
                    discount_update_Cmd.ExecuteNonQuery()
                End Using
            Else
                dr.Close()
                Using discount_add_Cmd As MySqlCommand = cn.CreateCommand()
                    discount_add_Cmd.Transaction = enroll_transaction
                    discount_add_Cmd.CommandText = "INSERT INTO tbl_assessment_institutional_discount (aid_student_id, aid_period_id, aid_percentage, aid_assessment_id) VALUES (@aid_student_id, @aid_period_id, @aid_percentage, @aid_assessment_id)"
                    discount_add_Cmd.Parameters.AddWithValue("@aid_student_id", StudentID)
                    discount_add_Cmd.Parameters.AddWithValue("@aid_period_id", CInt(cbAcad.SelectedValue))
                    discount_add_Cmd.Parameters.AddWithValue("@aid_percentage", aidp)
                    discount_add_Cmd.Parameters.AddWithValue("@aid_assessment_id", StudentAssessmentID)
                    discount_add_Cmd.ExecuteNonQuery()
                End Using
            End If

            enroll_transaction.Commit()

            cm = New MySqlCommand("SELECT * FROM tbl_period WHERE period_enrollment_enddate >= CURDATE() AND period_id  = " & CInt(cbAcad.SelectedValue) & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Close()
            Else
                dr.Close()
                MessageBox.Show("Enrollment period has passed. Dropped subjects were not removed but marked D(Dropped) as grade.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


            MessageBox.Show("Student '" & StudentName & "' enrollment for '" & cbAcad.Text & "' successfully updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            UserActivity("Updated student " & StudentID & " " & StudentName & " enrollment in Academic Year " & cbAcad.Text & ".", "ENROLLLMENT UPDATE")

        Catch ex As Exception
        enroll_transaction.Rollback()
        MessageBox.Show("Failed to update student enrollment failed to update. Enrollment transaction rolled back.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub AutoEnrollmentCode()
        Dim yearid As String = YearToday
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT enrollment_code FROM tbl_enrollment WHERE enrollment_code LIKE '" & yearid & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(enrollment_code) as ID from tbl_enrollment", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            lastCode = lastCode.Remove(0, 4)
            EnrollmentCode = CInt(yearid & lastCode) + 1
        Else
            dr.Close()
            EnrollmentCode = yearid & "00001"
        End If
        cn.Close()
    End Sub

    Sub StudentCOR()
        ReportPanel.Visible = True

        Try
            Dim dtable As DataTable
            Dim dbcommand As New MySqlCommand("Select (class_schedule_id) As 'ID', (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (subject_units) as 'Units', if(ds_code = 'M T W TH F SAT SUN', 'DAILY', ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (room_code) as 'Room', (Instructor) as 'Instructor', DATE_FORMAT(tbl_enrollment.eenrolledby_datetime,'%M %d, %Y') as 'DateEnrolled', CONCAT(tbl_user_account.ua_first_name,' ',tbl_user_account.ua_middle_name, ' ', tbl_user_account.ua_last_name) as 'EnrolledBy', ewithdrawn_datetime from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_enrollment, tbl_students_grades, tbl_user_account where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_students_grades.sg_class_id and tbl_enrollment.estudent_id = tbl_students_grades.sg_student_id and tbl_class_schedule.csperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eenrolledby_id = tbl_user_account.ua_id and tbl_enrollment.estudent_id = '" & StudentID & "' and tbl_enrollment.eperiod_id = " & CInt(cbAcad.SelectedValue) & "  order by Days asc, STR_TO_DATE(`Start Time`,'%l:%i:%s %p') asc", cn)
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
            rptdoc.SetParameterValue("studentname", StudentName)
            rptdoc.SetParameterValue("studentcourse", txtCourse.Text)
            rptdoc.SetParameterValue("schoolyear", cbAcad.Text)
            rptdoc.SetParameterValue("studentyearlevel", YearLevel)
            rptdoc.SetParameterValue("studentidnumber", StudentID)
            rptdoc.SetParameterValue("enrolledby", dg_report.CurrentRow.Cells(11).Value)
            rptdoc.SetParameterValue("enrolleddate", dg_report.CurrentRow.Cells(10).Value)
            rptdoc.SetParameterValue("dategenerated", oDate.ToString("MMMM' 'dd', 'yyyy"))

            rptdoc.SetParameterValue("enrollment_status", " ")
            rptdoc.SetParameterValue("wdate", " ")
            rptdoc.SetParameterValue("wdate2", " ")

            ReportViewer.ReportSource = rptdoc
            dg_report.DataSource = Nothing
            ReportViewer.Select()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If frmTitle.Text = "Search Student" Then
            LibraryEnrollmentStudentList()
        ElseIf frmTitle.Text = "Search Class Schedule" Then
            LibraryEnrollmentClassSchedList()
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

    Sub ClearAll()
        dgStudentSched.Rows.Clear()
        cbSection.DataSource = Nothing
        txtCourse.Text = String.Empty
        txtGenderYearLevel.Text = String.Empty
        txtStudent.Text = String.Empty

        StudentID = ""
        StudentName = ""
        CourseID = 0
        Course = ""
        YearLevel = ""
        Gender = ""
        CourseStatus = ""
        StudentAssessmentID = 0
        EnrollmentCode = 0
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearAll()
        MsgBox("Cancelled.", vbInformation)
    End Sub

    Private Sub frmEnrollStudent_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub
End Class