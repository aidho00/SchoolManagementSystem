Imports MySql.Data.MySqlClient

Public Class frmStudentGradeEditor

    Public studentId As String = ""
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

    Dim courseId As Integer = 0
    Dim courseName As String = ""
    Dim courseCode As String = ""
    Dim courseMajor As String = ""

    Dim schoolID As Integer = 0

    Dim GradeVisibilityStatus As Integer = 0

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        SearchPanel.Visible = True
        dgStudentList.BringToFront()
        frmTitle.Text = "Search Student"
        LoadGradingStudentList()
        txtSearch.Select()
    End Sub

    Sub LoadData()
        fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_students_grades t1 JOIN tbl_period t2 ON t1.sg_period_id = t2.period_id where t1.sg_student_id = '" & studentId & "' group by t1.sg_period_id order by `period_name` desc, `period_status` ASC, `period_semester` desc", cbAcademicYear, "tbl_period", "PERIOD", "period_id")
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Select Case frmTitle.Text
            Case "Search Student"
                LoadGradingStudentList()
        End Select
    End Sub

    Sub StudentList()
        dgStudentList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, (course_name) as 'Course Desc', (course_sector) as 'Course Sector' from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (s_ln like '%" & txtSearch.Text & "%' or s_fn like '%" & txtSearch.Text & "%' or s_mn like '%" & txtSearch.Text & "%' or s_id_no like '%" & txtSearch.Text & "%' or course_code like '%" & txtSearch.Text & "%' or s_yr_lvl like '%" & txtSearch.Text & "%') order by s_id_no asc limit 250"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("Course Desc").ToString, dr.Item("Course Sector").ToString)
        End While
        dr.Close()
        cn.Close()
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
            cn.Open()
            cm = New MySqlCommand("SELECT sg_course_id FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            studentCourseId = cm.ExecuteScalar
            cn.Close()
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
                studentCourseSector = dgStudentList.CurrentRow.Cells(11).Value
                studentGender = dgStudentList.CurrentRow.Cells(6).Value

                LoadData()
                'YearLevelStudentGradeLevel()

                txtStudent.Text = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
                txtYearLevelCourse.Text = studentYearLevel & " | " & studentCourse & " - " & studentCourseDesc

            Case "Search School"
                schoolID = dgSchoolList.CurrentRow.Cells(1).Value
                txtSchool = dgSchoolList.CurrentRow.Cells(3).Value
        End Select
        SearchPanel.Visible = False
        dgStudentList.Rows.Clear()
        dgSchoolList.Rows.Clear()
        frmTitle.Text = "Search"
        txtSearch.Text = String.Empty
    End Sub

    Sub StudentGrades()
        dgStudentGrades.Rows.Clear()
        LoadStudentGrades()

        Try
            Dim rowCount = dgStudentGrades.Rows.Count
            totalSubjects.Text = rowCount
        Catch ex As Exception
            totalSubjects.Text = "0"
        End Try

        Try
            Dim columnIndex As Integer = 5 ' Assuming column index for the value to sum (modify as needed)
            Dim columnSum As Double = GetColumnSum(dgStudentGrades, columnIndex)
            totalUnits.Text = columnSum
        Catch ex As Exception
            totalUnits.Text = "-"
        End Try

        Try
            txtSchool.Text = dgStudentGrades.Rows(0).Cells(10).Value
            txtRemarks.Text = dgStudentGrades.Rows(0).Cells(9).Value
            GradeVisibilityStatus = CInt(dgStudentGrades.Rows(0).Cells(12).Value)
            GradeStatus.Text = dgStudentGrades.Rows(0).Cells(6).Value

            schoolID = dgStudentGrades.Rows(0).Cells(14).Value

            If GradeVisibilityStatus = 1 Then
                GradeVisibility.Visible = True
                GradeVisibility.Checked = False
            ElseIf GradeVisibilityStatus = 0 Then
                GradeVisibility.Visible = True
                GradeVisibility.Checked = True
            Else
                GradeVisibility.Visible = False
                GradeStatus.Text = "Status"
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Function GetColumnSum(ByVal dgv As DataGridView, ByVal colIndex As Integer) As Double
        Dim sum As Double = 0.0
        For Each row As DataGridViewRow In dgv.Rows
            ' Check if cell value is numeric before adding
            If IsNumeric(row.Cells(colIndex).Value) Then
                sum += Convert.ToDouble(row.Cells(colIndex).Value)
            End If
        Next
        Return sum
    End Function

    Private Sub btnLoadGrade_Click(sender As Object, e As EventArgs) Handles btnLoadGrade.Click
        StudentGrades()
    End Sub

    Private Sub GradeVisibility_CheckedChanged(sender As Object, e As EventArgs) Handles GradeVisibility.CheckedChanged
        If GradeVisibility.Checked = True Then
            GradeVisibility.Text = "Visible"
        Else
            GradeVisibility.Text = "Not Visible"
        End If
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        dgStudentGrades.Rows.Clear()
        GradeVisibility.Visible = False
        GradeStatus.Text = "Status"
        YearLevelStudentGradeLevel()
        txtYearLevelCourse.Text = studentGradeLevel & " | " & studentGradeLevelCourse
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim drr As DialogResult
        drr = MessageBox.Show("Are you sure you want to update student " & studentName & " grades for academic year " & cbAcademicYear.Text & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If drr = DialogResult.No Then
        Else
            If dgStudentGrades.RowCount = 0 Then
                MessageBox.Show("There are currently no grades to update!", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else

                Dim x As Integer
                If GradeVisibility.Checked = True Then
                    x = 0
                Else
                    x = 1
                End If

                If GradeStatus.Text = "Enrolled" Then
                    'For Each row As DataGridViewRow In dgStudentGrades.Rows
                    '    ' Check if the row index is less than 2 (assuming you want to start checking from the third row)
                    '    If row.Index >= 2 Then
                    '        ' Check if row 3 and row 4 have 0 value
                    '        If row.Index = 2 AndAlso Convert.ToInt32(row.Cells(0).Value) = 0 AndAlso Convert.ToInt32(dgStudentGrades.Rows(row.Index + 1).Cells(0).Value) = 0 Then
                    '            MessageBox.Show("Inputted grade of subject '" & row.Cells(2).Value & " " & row.Cells(3).Value & "' is invalid." & Environment.NewLine & "Grading System Range: 1.1(94)/1.2(93)/1.3(92)/1.4(91)/1.5(90)/1.6(89)/1.7(88)/1.8(87)/1.9(86)" & Environment.NewLine & "2.0(85)/2.1(84)/2.2(83)/2.3(82)/2.4(81)/2.5(80)/2.6(79)/2.7(78)/2.8(77)/2.9(76)" & Environment.NewLine & "3.0(75)" & Environment.NewLine & "5.0(Failed)" & Environment.NewLine & "D(Dropped)", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '            Return ' Return if conditions are met
                    '        End If

                    '        ' Check if row 3 is exactly 1
                    '        If Convert.ToInt32(row.Cells(0).Value) = 1 Then
                    '            MessageBox.Show("Inputted grade of subject '" & row.Cells(2).Value & " " & row.Cells(3).Value & "' is invalid." & Environment.NewLine & "Grading System Range: 1.1(94)/1.2(93)/1.3(92)/1.4(91)/1.5(90)/1.6(89)/1.7(88)/1.8(87)/1.9(86)" & Environment.NewLine & "2.0(85)/2.1(84)/2.2(83)/2.3(82)/2.4(81)/2.5(80)/2.6(79)/2.7(78)/2.8(77)/2.9(76)" & Environment.NewLine & "3.0(75)" & Environment.NewLine & "5.0(Failed)" & Environment.NewLine & "D(Dropped)", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '            Return ' Return if conditions are met
                    '        End If

                    '        ' Check if row 3 is greater than 5
                    '        If Convert.ToInt32(row.Cells(0).Value) > 5 Then
                    '            MessageBox.Show("Inputted grade of subject '" & row.Cells(2).Value & " " & row.Cells(3).Value & "' is invalid." & Environment.NewLine & "Grading System Range: 1.1(94)/1.2(93)/1.3(92)/1.4(91)/1.5(90)/1.6(89)/1.7(88)/1.8(87)/1.9(86)" & Environment.NewLine & "2.0(85)/2.1(84)/2.2(83)/2.3(82)/2.4(81)/2.5(80)/2.6(79)/2.7(78)/2.8(77)/2.9(76)" & Environment.NewLine & "3.0(75)" & Environment.NewLine & "5.0(Failed)" & Environment.NewLine & "D(Dropped)", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '            Return ' Return if conditions are met
                    '        End If

                    '        ' Check if row 3 is greater than 3 and less than 5
                    '        If Convert.ToInt32(row.Cells(0).Value) > 3 AndAlso Convert.ToInt32(row.Cells(0).Value) < 5 Then
                    '            MessageBox.Show("Inputted grade of subject '" & row.Cells(2).Value & " " & row.Cells(3).Value & "' is invalid." & Environment.NewLine & "Grading System Range: 1.1(94)/1.2(93)/1.3(92)/1.4(91)/1.5(90)/1.6(89)/1.7(88)/1.8(87)/1.9(86)" & Environment.NewLine & "2.0(85)/2.1(84)/2.2(83)/2.3(82)/2.4(81)/2.5(80)/2.6(79)/2.7(78)/2.8(77)/2.9(76)" & Environment.NewLine & "3.0(75)" & Environment.NewLine & "5.0(Failed)" & Environment.NewLine & "D(Dropped)", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '            Return ' Return if conditions are met
                    '        End If

                    '        ' Check if row 3 is equal to 5 and row 4 is equal to 0
                    '        If row.Index < dgStudentGrades.Rows.Count - 1 AndAlso Convert.ToInt32(row.Cells(0).Value) = 5 AndAlso Convert.ToInt32(dgStudentGrades.Rows(row.Index + 1).Cells(0).Value) = 0 Then
                    '            MessageBox.Show("Inputted grade of subject '" & row.Cells(2).Value & " " & row.Cells(3).Value & "' is invalid." & Environment.NewLine & "Grading System Range: 1.1(94)/1.2(93)/1.3(92)/1.4(91)/1.5(90)/1.6(89)/1.7(88)/1.8(87)/1.9(86)" & Environment.NewLine & "2.0(85)/2.1(84)/2.2(83)/2.3(82)/2.4(81)/2.5(80)/2.6(79)/2.7(78)/2.8(77)/2.9(76)" & Environment.NewLine & "3.0(75)" & Environment.NewLine & "5.0(Failed)" & Environment.NewLine & "D(Dropped)", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '            Return ' Return if conditions are met
                    '        End If

                    '        ' Check if row 3 is not equal to "D"
                    '        If Not row.Cells(0).Value.ToString() = "D" Then
                    '            MessageBox.Show("Inputted grade of subject '" & row.Cells(2).Value & " " & row.Cells(3).Value & "' is invalid." & Environment.NewLine & "Grading System Range: 1.1(94)/1.2(93)/1.3(92)/1.4(91)/1.5(90)/1.6(89)/1.7(88)/1.8(87)/1.9(86)" & Environment.NewLine & "2.0(85)/2.1(84)/2.2(83)/2.3(82)/2.4(81)/2.5(80)/2.6(79)/2.7(78)/2.8(77)/2.9(76)" & Environment.NewLine & "3.0(75)" & Environment.NewLine & "5.0(Failed)" & Environment.NewLine & "D(Dropped)", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '            Return ' Return if conditions are met
                    '        End If
                    '    End If
                    'Next


                    For Each row As DataGridViewRow In dgStudentGrades.Rows
                        Dim found As Boolean
                        Dim grade As String
                        Dim credit As String
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("select sg_grade, sg_credits from tbl_students_grades where sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_subject_id = '" & row.Cells(1).Value & "'", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            found = True
                            grade = dr.Item("sg_grade").ToString
                            credit = dr.Item("sg_credits").ToString
                        Else
                            found = False
                        End If
                        dr.Close()
                        cn.Close()

                        If grade = row.Cells(4).Value And credit = row.Cells(5).Value Then
                            'query("UPDATE tbl_students_grades set sg_grade_visibility = " & x & " where sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_subject_id = '" & row.Cells(1).Value & "'")
                        Else
                            query("UPDATE tbl_students_grades set sg_prev_grade = sg_grade, sg_prev_grade_addedby = sg_grade_addedby, sg_prev_grade_dateadded = sg_grade_dateadded, sg_grade = '" & row.Cells(4).Value & "' , sg_credits = '" & row.Cells(5).Value & "', sg_grade_addedby = " & str_userid & ", sg_grade_dateadded = CURDATE(), sg_grade_visibility = " & x & " where sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_subject_id = '" & row.Cells(1).Value & "'")
                            UserActivity("Updated student " & txtStudent.Text & " grade for subject " & row.Cells(2).Value & " - " & row.Cells(3).Value & " in Academic Year " & cbAcademicYear.Text & " from Grade:" & grade & " to Grade:" & row.Cells(4).Value & " and Credit:" & credit & " to Credit:" & row.Cells(5).Value & ".", "GRADE EDITING")
                        End If
                    Next
                    MsgBox("Grades successfully updated.", vbInformation)
                    clearfields()
                    LoadData()
                ElseIf GradeStatus.Text = "Credited" Then
                    Dim visibilitychange As Boolean = False
                    For Each row As DataGridViewRow In dgStudentGrades.Rows
                        Dim found As Boolean
                        Dim grade As String
                        Dim credit As String
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("select sg_grade, sg_credits from tbl_students_grades where sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_subject_id = '" & row.Cells(1).Value & "'", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            found = True
                            grade = dr.Item("sg_grade").ToString
                            credit = dr.Item("sg_credits").ToString
                        Else
                            found = False
                        End If
                        dr.Close()
                        cn.Close()

                        If grade = row.Cells(4).Value And credit = row.Cells(5).Value Then
                            query("UPDATE tbl_students_grades set sg_grade_visibility = " & x & " where sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_subject_id = '" & row.Cells(1).Value & "'")
                            visibilitychange = True
                        Else
                            query("UPDATE tbl_students_grades set sg_grade = '" & row.Cells(4).Value & "', sg_credits = '" & row.Cells(5).Value & "', sg_grade_addedby = " & str_userid & ", sg_grade_dateadded = CURDATE(), sg_grade_visibility = " & x & ", sg_grade_remarks = '" & txtRemarks.Text & "', sg_school_id = " & schoolID & " where sg_student_id = '" & studentId & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_subject_id = '" & row.Cells(1).Value & "'")
                            UserActivity("Updated student " & txtStudent.Text & " credited grade for subject " & row.Cells(2).Value & " - " & row.Cells(3).Value & " in Academic Year " & cbAcademicYear.Text & " from Grade:" & grade & " to Grade:" & row.Cells(4).Value & " and Credit:" & credit & " to Credit:" & row.Cells(5).Value & ".", "GRADE EDITING")
                        End If
                    Next
                    If visibilitychange = True Then
                        UserActivity("Updated student " & txtStudent.Text & " credited grade visiblity in Academic Year " & cbAcademicYear.Text & " to " & GradeVisibility.Text & ".", "GRADE EDITING")
                    End If
                    MsgBox("Grades successfully updated.", vbInformation)
                    clearfields()
                    LoadData()
                End If
            End If
        End If
    End Sub
    Sub clearfields()
        studentId = ""
        studentName = ""
        studentFName = ""
        studentMName = ""
        studentLName = ""
        studentEXTName = ""
        studentCourse = ""
        studentCourseDesc = ""
        studentCourseSector = ""
        studentCourseId = 0
        studentYearLevel = ""
        studentGender = ""

        studentGradeLevel = ""
        studentGradeLevelCourse = ""
        studentGradeLevelCourseName = ""
        studentGradeLevelCourseCode = ""

        courseId = 0
        courseName = ""
        courseCode = ""
        courseMajor = ""

        GradeStatus.Text = "Status"
        GradeVisibility.Checked = False

        txtStudent.Text = String.Empty
        txtYearLevelCourse.Text = String.Empty
        txtSchool.Text = String.Empty
        txtRemarks.Text = String.Empty

        dgStudentGrades.Rows.Clear()
    End Sub


    Private Sub dgStudentGrades_SelectionChanged(sender As Object, e As EventArgs) Handles dgStudentGrades.SelectionChanged
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select CONCAT(t2.ua_last_name, ', ', t2.ua_first_name,' ', t2.ua_middle_name) as NAME, DATE_FORMAT(t1.sg_grade_dateadded, '%M %d, %Y') as DATEADDED from tbl_students_grades t1 JOIN tbl_user_account t2 ON t1.sg_grade_addedby = t2.ua_id where t1.sg_student_id = '" & studentId & "' and t1.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and t1.sg_subject_id = '" & dgStudentGrades.CurrentRow.Cells(1).Value & "'", cn)
            gradeRemark.Text = "*Selected Cell - Grade uploaded/edited by: " & cm.ExecuteScalar
            cn.Close()
        Catch ex As Exception
            gradeRemark.Text = "*Selected Cell - Grade uploaded/edited by:"
        End Try
    End Sub

    Private Sub dgStudentGrades_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgStudentGrades.CellContentClick
        Dim colname As String = dgStudentGrades.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            frmGradingChanges.cbGrade.Text = dgStudentGrades.CurrentRow.Cells(4).Value
            frmGradingChanges.cbCredit.Text = dgStudentGrades.CurrentRow.Cells(5).Value
            frmGradingChanges.ShowDialog()
        ElseIf colname = "colRemove" Then
            'Try
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to remove this student '" & txtStudent.Text & "' credited grade for subject " & dgStudentGrades.CurrentRow.Cells(2).Value & " - " & dgStudentGrades.CurrentRow.Cells(3).Value & " in Academic Year " & cbAcademicYear.Text & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dr = DialogResult.Yes Then
                query("delete from tbl_students_grades where sg_id = " & CInt(dgStudentGrades.CurrentRow.Cells(13).Value) & " and sg_student_id = '" & studentId & "'")
                UserActivity("Removed student " & txtStudent.Text & " grade credited grade for subject " & dgStudentGrades.CurrentRow.Cells(2).Value & " - " & dgStudentGrades.CurrentRow.Cells(3).Value & " in Academic Year " & cbAcademicYear.Text & ".", "GRADE EDITING")
                dgStudentGrades.Rows.RemoveAt(dgStudentGrades.CurrentRow.Index)
                If dgStudentGrades.RowCount = 0 Then
                    LoadData()
                End If
                MsgBox("Grade successfully removed.", vbInformation)
            Else
                MsgBox("Grade removal cancelled.", vbExclamation)
            End If
            ''Catch ex As Exception
            ''End Try
        ElseIf colname = "colTransfer" Then
            fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_period where period_id NOT IN(" & CInt(cbAcademicYear.SelectedValue) & ") and period_id NOT IN(SELECT period_id FROM tbl_students_grades t1 JOIN tbl_period t2 ON t1.sg_period_id = t2.period_id where t1.sg_student_id = '" & studentId & "' and t1.sg_grade_status = 'Enrolled' group by t1.sg_period_id) order by `period_name` desc, `period_status` ASC, `period_semester` desc", frmGradingTransfer.cbAcademicYear, "tbl_period", "PERIOD", "period_id")
            frmGradingTransfer.cbAcademicYear.Text = cbAcademicYear.Text
            frmGradingTransfer.ShowDialog()
        End If
    End Sub

    Private Sub GradeStatus_TextChanged(sender As Object, e As EventArgs) Handles GradeStatus.TextChanged
        If GradeStatus.Text = "Enrolled" Then
            colUpdate.Visible = True
            colRemove.Visible = False
            colTransfer.Visible = False
            btnSearchSchool.Visible = False
            txtRemarks.ReadOnly = True

            cGrade.ReadOnly = True
            cCredit.ReadOnly = True
        ElseIf GradeStatus.Text = "Credited" Then
            colUpdate.Visible = False
            colRemove.Visible = True
            colTransfer.Visible = True
            btnSearchSchool.Visible = True
            txtRemarks.ReadOnly = False

            cGrade.ReadOnly = False
            cCredit.ReadOnly = False
        End If
    End Sub

    Private Sub GradeStatus_Click(sender As Object, e As EventArgs) Handles GradeStatus.Click

    End Sub

    Private Sub btnUpdateSR_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSearchSchool_Click(sender As Object, e As EventArgs) Handles btnSearchSchool.Click
        SearchPanel.Visible = True
        dgSchoolList.BringToFront()
        frmTitle.Text = "Search School"
        LoadGradingSchoolList()
        txtSearch.Select()
    End Sub

    Private Sub dgStudentGrades_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgStudentGrades.RowsAdded
        Try
            Dim rowCount = dgStudentGrades.Rows.Count
            totalSubjects.Text = rowCount
        Catch ex As Exception
            totalSubjects.Text = "0"
        End Try

        Try
            Dim columnIndex As Integer = 5 ' Assuming column index for the value to sum (modify as needed)
            Dim columnSum As Double = GetColumnSum(dgStudentGrades, columnIndex)
            totalUnits.Text = columnSum
        Catch ex As Exception
            totalUnits.Text = "-"
        End Try
    End Sub

    Private Sub dgStudentGrades_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgStudentGrades.RowsRemoved
        Try
            Dim rowCount = dgStudentGrades.Rows.Count
            totalSubjects.Text = rowCount
        Catch ex As Exception
            totalSubjects.Text = "0"
        End Try

        Try
            Dim columnIndex As Integer = 5 ' Assuming column index for the value to sum (modify as needed)
            Dim columnSum As Double = GetColumnSum(dgStudentGrades, columnIndex)
            totalUnits.Text = columnSum
        Catch ex As Exception
            totalUnits.Text = "-"
        End Try
    End Sub

    Private Sub frmStudentGradeEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub
End Class

Public Class ListItem
    Public Property Text As String
    Public Property Value As Object

    Public Sub New(ByVal text As String, ByVal value As Object)
        Me.Text = text
        Me.Value = value
    End Sub

    Public Overrides Function ToString() As String
        Return Text
    End Function
End Class