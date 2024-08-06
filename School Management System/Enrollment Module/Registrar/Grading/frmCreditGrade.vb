Imports MySql.Data.MySqlClient

Public Class frmCreditGrade

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

    Dim courseId As Integer = 0
    Dim courseName As String = ""
    Dim courseCode As String = ""
    Dim courseMajor As String = ""

    Dim schoolID As Integer = 0


    Private Sub btnSearchSchool_Click(sender As Object, e As EventArgs) Handles btnSearchSchool.Click
        If txtStudent.Text = String.Empty Then
        Else
            SearchPanel.Visible = True
            dgSchoolList.BringToFront()
            frmTitle.Text = "Search School"
            LoadGradeCreditingSchoolList()
            txtSearch.Select()
        End If
    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        SearchPanel.Visible = True
        dgStudentList.BringToFront()
        frmTitle.Text = "Search Student"
        LoadGradeCreditingStudentList()
        txtSearch.Select()
    End Sub

    Private Sub btnSearchCourse_Click(sender As Object, e As EventArgs) Handles btnSearchCourse.Click
        If txtStudent.Text = String.Empty Then
        Else
            SearchPanel.Visible = True
            dgCourseList.BringToFront()
            frmTitle.Text = "Search Course"
            LoadGradeCreditingCourseList()
            txtSearch.Select()
        End If
    End Sub


    Private Sub btnSearchSubject_Click(sender As Object, e As EventArgs) Handles btnSearchSubject.Click
        If txtStudent.Text = String.Empty Then
        Else
            SearchPanel.Visible = True
            dgSubjectList.BringToFront()
            frmTitle.Text = "Search Subject"
            LoadGradeCreditingSubjectList()
            txtSearch.Select()
        End If
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

                studentGender = dgStudentList.CurrentRow.Cells(6).Value

                LoadData()

                txtStudent.Text = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value

            Case "Search School"
                schoolID = dgSchoolList.CurrentRow.Cells(1).Value
                txtSchool.Text = dgSchoolList.CurrentRow.Cells(3).Value

            Case "Search Course"
                courseId = dgCourseList.CurrentRow.Cells(1).Value
                courseCode = dgCourseList.CurrentRow.Cells(2).Value
                courseName = dgCourseList.CurrentRow.Cells(3).Value
                courseMajor = dgCourseList.CurrentRow.Cells(4).Value

                txtCourse.Text = courseName & " - " & courseMajor

            Case "Search Subject"
                dgStudentSubjects.Rows.Add(dgSubjectList.CurrentRow.Cells(1).Value, dgSubjectList.CurrentRow.Cells(2).Value, dgSubjectList.CurrentRow.Cells(3).Value)
        End Select
        SearchPanel.Visible = False
        dgStudentList.Rows.Clear()
        dgSchoolList.Rows.Clear()
        dgCourseList.Rows.Clear()
        frmTitle.Text = "Search"
        txtSearch.Text = String.Empty
    End Sub

    Sub LoadData()
        fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period WHERE period_id NOT IN (Select eperiod_id from tbl_enrollment where estudent_id = '" & studentId & "') order by  `period_name` desc, `period_semester` desc, `period_status` asc", cbAcademicYear, "tbl_period", "PERIOD", "period_id")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim drr As DialogResult
        drr = MessageBox.Show("Are you sure you want to save/credit student " & studentName & " grades for academic year " & cbAcademicYear.Text & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If drr = DialogResult.No Then
        Else
            If txtStudent.Text = String.Empty Then
                MsgBox("Please select a student.", vbCritical)
            ElseIf txtSchool.Text = String.Empty Then
                MsgBox("Please select a school.", vbCritical)
            ElseIf txtCourse.Text = String.Empty Then
                MsgBox("Please select a course.", vbCritical)
            ElseIf dgStudentSubjects.Rows.Count = 0 Then
                MsgBox("Please add a subject.", vbCritical)
            Else
                For Each row As DataGridViewRow In dgStudentSubjects.Rows
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT * FROM tbl_students_grades WHERE sg_student_id = '" & studentId & "' AND sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and sg_subject_id = " & CInt(row.Cells(0).Value) & "", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        dr.Close()
                        MsgBox("Grade for subject " & row.Cells(1).Value & " - " & row.Cells(2).Value & " in academic year " & cbAcademicYear.Text & " already exists.", vbCritical)
                        cn.Close()
                        Exit For
                    Else
                        dr.Close()
                        query("Insert into tbl_students_grades (sg_student_id, sg_course_id, sg_period_id, sg_subject_id, sg_grade, sg_credits, sg_school_id, sg_grade_status, sg_yearlevel, sg_grade_addedby, sg_grade_dateadded, sg_grade_remarks) values ('" & studentId & "', '" & courseId & "', " & CInt(cbAcademicYear.SelectedValue) & ", '" & row.Cells(0).Value & "', '" & row.Cells(3).Value & "', '" & row.Cells(4).Value & "', '" & schoolID & "', 'Credited', 'Transferee', " & str_userid & ", CURDATE(), '" & txtRemarks.Text & "')")
                        UserActivity("Credited subject(s) to student " & txtStudent.Text & ".", "GRADE CREDITING")
                        MessageBox.Show("Grades successfully added/credited.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        dgStudentSubjects.Rows.Clear()
                    End If

                Next

            End If
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Select Case frmTitle.Text
            Case "Search Student"
                LoadGradeCreditingStudentList()
            Case "Search Course"
                LoadGradeCreditingCourseList()
            Case "Search Subject"
                LoadGradeCreditingSubjectList()
            Case "Search School"
                LoadGradeCreditingSchoolList()
        End Select
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        Try


            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT * FROM tbl_students_grades WHERE sg_student_id = '" & studentId & "' AND sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT sg_school_id from tbl_students_grades WHERE sg_student_id = '" & studentId & "' AND sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                schoolID = cm.ExecuteScalar
                cn.Close()

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT schl_name from tbl_schools WHERE schl_id = " & schoolID & "", cn)
                txtSchool.Text = cm.ExecuteScalar
                cn.Close()

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT sg_course_id from tbl_students_grades WHERE sg_student_id = '" & studentId & "' AND sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                courseId = cm.ExecuteScalar
                cn.Close()

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT course_code from tbl_course WHERE course_id = " & courseId & "", cn)
                courseCode = cm.ExecuteScalar
                cn.Close()

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT course_name from tbl_course WHERE course_id = " & courseId & "", cn)
                courseName = cm.ExecuteScalar
                cn.Close()

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT course_major from tbl_course WHERE course_id = " & courseId & "", cn)
                courseMajor = cm.ExecuteScalar
                cn.Close()

                txtCourse.Text = courseName & " - " & courseMajor

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT sg_grade_remarks from tbl_students_grades WHERE sg_student_id = '" & studentId & "' AND sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                txtRemarks.Text = cm.ExecuteScalar
                cn.Close()
            Else
                dr.Close()
                cn.Close()
            End If
        Catch ex As Exception

        End Try
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

        courseId = 0
        courseName = ""
        courseCode = ""
        courseMajor = ""


        txtStudent.Text = String.Empty
        txtCourse.Text = String.Empty
        txtSchool.Text = String.Empty
        txtRemarks.Text = String.Empty

        dgStudentSubjects.Rows.Clear()
    End Sub


    Private Sub dgStudentSubjects_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgStudentSubjects.RowsAdded
        Try
            Dim rowCount = dgStudentSubjects.Rows.Count
            totalSubjects.Text = rowCount
        Catch ex As Exception
            totalSubjects.Text = "0"
        End Try

        Try
            Dim columnIndex As Integer = 4 ' Assuming column index for the value to sum (modify as needed)
            Dim columnSum As Double = GetColumnSum(dgStudentSubjects, columnIndex)
            totalUnits.Text = columnSum
        Catch ex As Exception
            totalUnits.Text = "-"
        End Try
    End Sub

    Private Sub dgStudentGrades_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgStudentSubjects.RowsRemoved
        Try
            Dim rowCount = dgStudentSubjects.Rows.Count
            totalSubjects.Text = rowCount
        Catch ex As Exception
            totalSubjects.Text = "0"
        End Try

        Try
            Dim columnIndex As Integer = 4 ' Assuming column index for the value to sum (modify as needed)
            Dim columnSum As Double = GetColumnSum(dgStudentSubjects, columnIndex)
            totalUnits.Text = columnSum
        Catch ex As Exception
            totalUnits.Text = "-"
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

    Private Sub frmCreditGrade_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub

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

    Private Sub dgStudentSubjects_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgStudentSubjects.CellContentClick
        Dim colname As String = dgStudentSubjects.Columns(e.ColumnIndex).Name
        If colname = "colRemove" Then
            dgStudentSubjects.Rows.RemoveAt(dgStudentSubjects.CurrentRow.Index)
        End If
    End Sub
End Class