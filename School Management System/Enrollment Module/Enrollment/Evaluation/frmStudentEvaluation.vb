Imports MySql.Data.MySqlClient

Public Class frmStudentEvaluation

    Public Shared StudentID As String = ""
    Public Shared StudentName As String = ""
    Public Shared CourseID As Integer = 0
    Public Shared Course As String = ""
    Public Shared YearLevel As String = ""
    Public Shared Gender As String = ""
    Public Shared CourseStatus As String = ""

    Public Shared currid As Integer = 0

    Public Shared activeDataGridView As DataGridView

    ' Function to compare two images
    Private Function CompareImages(image1 As Bitmap, image2 As Bitmap) As Boolean
        If image1.Width <> image2.Width OrElse image1.Height <> image2.Height Then
            Return False
        End If

        For x As Integer = 0 To image1.Width - 1
            For y As Integer = 0 To image1.Height - 1
                If image1.GetPixel(x, y) <> image2.GetPixel(x, y) Then
                    Return False
                End If
            Next
        Next

        Return True
    End Function

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If frmTitle.Text = "Search Curriculum" Then
            currid = dgCurrList.CurrentRow.Cells(1).Value
            txtCurr.Text = dgCurrList.CurrentRow.Cells(2).Value
            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select * from tbl_students_curriculum where sc_curr_id = " & currid & " and sc_student_id = '" & StudentID & "'", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    btnAssignCurriculum.Visible = False
                Else
                    btnAssignCurriculum.Visible = True
                End If
                dr.Close()
                cn.Close()
            Catch ex As Exception
                dr.Close()
                cn.Close()
                MsgBox(ex.Message)
            End Try
            SearchPanel.Visible = False
        ElseIf frmTitle.Text = "Search Student" Then

            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT * FROM tbl_students_grades WHERE sg_student_id = '" & StudentID & "' and sg_period_id = " & CInt(frmEvaluationSubjects.cbAcademicYear.SelectedValue) & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Close()
                cn.Close()
                MsgBox("Student " & StudentName & " with ID Number " & StudentID & " already has grades or is enrolled for this Academic Year " & frmEvaluationSubjects.cbAcademicYear.Text & ".'", vbCritical)
            Else
                dr.Close()
                cn.Close()

                StudentName = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
                StudentID = dgStudentList.CurrentRow.Cells(1).Value
                CourseID = dgStudentList.CurrentRow.Cells(9).Value
                YearLevel = dgStudentList.CurrentRow.Cells(7).Value
                Gender = dgStudentList.CurrentRow.Cells(6).Value
                CourseStatus = dgStudentList.CurrentRow.Cells(11).Value
                txtStudent.Text = dgStudentList.CurrentRow.Cells(1).Value & " - " & dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
                txtCourse.Text = dgStudentList.CurrentRow.Cells(8).Value & " - " & dgStudentList.CurrentRow.Cells(10).Value
                txtGenderYearLevel.Text = dgStudentList.CurrentRow.Cells(6).Value & " - " & dgStudentList.CurrentRow.Cells(7).Value
                StudentAssignedCurrList()
                dg1Y1S.Rows.Clear()
                dg1Y2S.Rows.Clear()
                dg1YS.Rows.Clear()

                dg2Y1S.Rows.Clear()
                dg2Y2S.Rows.Clear()
                dg2YS.Rows.Clear()

                dg3Y1S.Rows.Clear()
                dg3Y2S.Rows.Clear()
                dg3YS.Rows.Clear()

                dg4Y1S.Rows.Clear()
                dg4Y2S.Rows.Clear()
                dg4YS.Rows.Clear()
                SearchPanel.Visible = False
            End If
        ElseIf frmTitle.Text = "Search Student Grade" Then
            frmLinkGrade.btnLink.Visible = True
            frmLinkGrade.Label1.Text = "Confirm Link Grade"
            frmLinkGrade.Label2.Text = "Link To:"
            frmLinkGrade.cbGrade.Visible = True
            frmLinkGrade.lblCode.Text = dgGradeList.CurrentRow.Cells(3).Value
            frmLinkGrade.lblDesc.Text = dgGradeList.CurrentRow.Cells(4).Value
            frmLinkGrade.lblSchool.Text = dgGradeList.CurrentRow.Cells(1).Value
            frmLinkGrade.lblAcadStatus.Text = dgGradeList.CurrentRow.Cells(2).Value & " - " & dgGradeList.CurrentRow.Cells(6).Value
            frmLinkGrade.cbGrade.Enabled = True
            frmLinkGrade.cbGrade.Text = dgGradeList.CurrentRow.Cells(5).Value

            frmLinkGrade.lblCurrCode.Text = frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(1).Value
            frmLinkGrade.lblCurrDesc.Text = frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(2).Value
            frmLinkGrade.ShowDialog()
            Me.Close()
        End If
        txtSearch.Text = String.Empty
    End Sub

    Sub StudentAssignedCurrList()
        Try
            Dim imageFromPictureBox As Image = pic4.Image
            dgStudentCurrList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "Select curriculum_id, (curriculum_code) as 'Curriculum', (course_code) as 'Course', (course_name) as 'CourseDescription', (total_units) as 'Units', (sc_total_units) as 'Earned', (sc_status) as 'Status' from tbl_students_curriculum JOIN tbl_curriculum ON tbl_students_curriculum.sc_curr_id = tbl_curriculum.curriculum_id JOIN tbl_course ON tbl_curriculum.curr_course_id = tbl_course.course_id JOIN tbl_user_account ON tbl_students_curriculum.sc_assigned_by = tbl_user_account.ua_id WHERE sc_student_id = '" & StudentID & "'"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgStudentCurrList.Rows.Add(imageFromPictureBox, dr.Item("curriculum_id").ToString, dr.Item("Curriculum").ToString, dr.Item("Course").ToString, dr.Item("CourseDescription").ToString, dr.Item("Units").ToString, dr.Item("Earned").ToString, dr.Item("Status").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgStudentCurrList.Rows.Clear()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs)
        With frmStudentInfo
            frmStudentList.LoadComboBoxData()
            comboStudentLevelWithIrreg(.cbYearLevel, .lbllabel, .lbllevel)
            ResetControls(frmStudentInfo)

            .cbTransferMode.Text = String.Empty
            .btnUpdate.Visible = False
            .btnSave.Visible = True

            .ShowDialog()
        End With
    End Sub

    Public Sub CurriculumStudentList()
        Try

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

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgStudentList.Rows.Clear()

        End Try
    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        frmTitle.Text = "Search Student"
        SearchPanel.Visible = True
        CurriculumStudentList()
        dgStudentList.BringToFront()
        txtSearch.Select()
    End Sub

    Private Sub frmStudentEvaluation_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)

        AddHandler dg1Y1S.CellMouseClick, AddressOf DataGridView_CellMouseClick
        AddHandler dg1Y2S.CellMouseClick, AddressOf DataGridView_CellMouseClick
        AddHandler dg1YS.CellMouseClick, AddressOf DataGridView_CellMouseClick

        AddHandler dg2Y1S.CellMouseClick, AddressOf DataGridView_CellMouseClick
        AddHandler dg2Y2S.CellMouseClick, AddressOf DataGridView_CellMouseClick
        AddHandler dg2YS.CellMouseClick, AddressOf DataGridView_CellMouseClick

        AddHandler dg3Y1S.CellMouseClick, AddressOf DataGridView_CellMouseClick
        AddHandler dg3Y2S.CellMouseClick, AddressOf DataGridView_CellMouseClick
        AddHandler dg3YS.CellMouseClick, AddressOf DataGridView_CellMouseClick

        AddHandler dg4Y1S.CellMouseClick, AddressOf DataGridView_CellMouseClick
        AddHandler dg4Y2S.CellMouseClick, AddressOf DataGridView_CellMouseClick
        AddHandler dg4YS.CellMouseClick, AddressOf DataGridView_CellMouseClick
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If frmTitle.Text = "Search Curriculum" Then
            StudentCurriculumList()
        ElseIf frmTitle.Text = "Search Student" Then
            CurriculumStudentList()
        ElseIf frmTitle.Text = "Search Student Grade" Then
            StudentGradeList()
        End If
    End Sub

    Private Sub btnSearchCurr_Click(sender As Object, e As EventArgs) Handles btnSearchCurr.Click
        If txtStudent.Text = String.Empty Then
        Else
            frmTitle.Text = "Search Curriculum"
            SearchPanel.Visible = True
            StudentCurriculumList()
            dgCurrList.BringToFront()
            txtSearch.Select()
        End If
    End Sub

    Sub StudentCurriculumList()
        Try

            dgCurrList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select curriculum_id, (curriculum_code) as 'Curriculum', (course_code) as 'Course', (course_name) as 'CourseDescription', (is_active) as 'Status' from tbl_curriculum JOIN tbl_course JOIN tbl_user_account where tbl_curriculum.prepared_by_id = tbl_user_account.ua_id and tbl_curriculum.curr_course_id = tbl_course.course_id and curriculum_code LIKE '%" & txtSearch.Text & "%'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgCurrList.Rows.Add(i, dr.Item("curriculum_id").ToString, dr.Item("Curriculum").ToString, dr.Item("Course").ToString, dr.Item("CourseDescription").ToString, dr.Item("Status").ToString)
        End While
        dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgCurrList.Rows.Clear()

        End Try
    End Sub

    Private Sub AutoSizeDataGridView(ByVal DTG As Object)
        Dim totalHeight As Integer = 0

        ' Calculate the total height required by the rows
        For Each row As DataGridViewRow In DTG.Rows
            totalHeight += row.Height
        Next

        ' Adding the height of the column headers if they are visible
        If DTG.ColumnHeadersVisible Then
            totalHeight += DTG.ColumnHeadersHeight
        End If

        ' Adding some extra space for the border
        totalHeight += DTG.Margin.Top + DTG.Margin.Bottom

        ' Set the height of the DataGridView
        DTG.Height = totalHeight
    End Sub

    Private Sub AutoSizePanel(ByVal PNL As Object, ByVal DTG As Object)
        ' Adjust the size of the panel to fit the DataGridView
        PNL.Height = DTG.Height + PNL.Padding.Top + PNL.Padding.Bottom
    End Sub

    '-----------------------------------------------

    Sub SubjectList_1Y1S()
        dg1Y1S.Rows.Clear()
        Dim yearlevel As String = "1st Year"
        Dim semester As String = "1st Semester"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg1Y1S.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg1Y1S)
            AutoSizePanel(Panel_1Y1S, dg1Y1S)
        End While
        dr.Close()
        cn.Close()

        If dg1Y1S.RowCount = 0 Then
            Panel_1Y1S.Visible = False
        Else
            Panel_1Y1S.Visible = True
            Try
                For Each row As DataGridViewRow In dg1Y1S.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Sub SubjectList_1Y2S()
        dg1Y2S.Rows.Clear()
        Dim yearlevel As String = "1st Year"
        Dim semester As String = "2nd Semester"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg1Y2S.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg1Y2S)
            AutoSizePanel(Panel_1Y2S, dg1Y2S)
        End While
        dr.Close()
        cn.Close()

        If dg1Y2S.RowCount = 0 Then
            Panel_1Y2S.Visible = False
        Else
            Panel_1Y2S.Visible = True
            Try
                For Each row As DataGridViewRow In dg1Y2S.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Sub SubjectList_1YS()
        dg1YS.Rows.Clear()
        Dim yearlevel As String = "1st Year"
        Dim semester As String = "Summer"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg1YS.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg1YS)
            AutoSizePanel(Panel_1YS, dg1YS)
        End While
        dr.Close()
        cn.Close()

        If dg1YS.RowCount = 0 Then
            Panel_1YS.Visible = False
        Else
            Panel_1YS.Visible = True
            Try
                For Each row As DataGridViewRow In dg1YS.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    '-----------------------------------------------

    Sub SubjectList_2Y1S()
        dg2Y1S.Rows.Clear()
        Dim yearlevel As String = "2nd Year"
        Dim semester As String = "1st Semester"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg2Y1S.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg2Y1S)
            AutoSizePanel(Panel_2Y1S, dg2Y1S)
        End While
        dr.Close()
        cn.Close()

        If dg2Y1S.RowCount = 0 Then
            Panel_2Y1S.Visible = False
        Else
            Panel_2Y1S.Visible = True
            Try
                For Each row As DataGridViewRow In dg2Y1S.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Sub SubjectList_2Y2S()
        dg2Y2S.Rows.Clear()
        Dim yearlevel As String = "2nd Year"
        Dim semester As String = "2nd Semester"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg2Y2S.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg2Y2S)
            AutoSizePanel(Panel_2Y2S, dg2Y2S)
        End While
        dr.Close()
        cn.Close()

        If dg2Y2S.RowCount = 0 Then
            Panel_2Y2S.Visible = False
        Else
            Panel_2Y2S.Visible = True
            Try
                For Each row As DataGridViewRow In dg2Y2S.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Sub SubjectList_2YS()
        dg2YS.Rows.Clear()
        Dim yearlevel As String = "2nd Year"
        Dim semester As String = "Summer"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg2YS.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg2YS)
            AutoSizePanel(Panel_2YS, dg2YS)
        End While
        dr.Close()
        cn.Close()

        If dg2YS.RowCount = 0 Then
            Panel_2YS.Visible = False
        Else
            Panel_2YS.Visible = True
            Try
                For Each row As DataGridViewRow In dg2YS.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    '-----------------------------------------------

    Sub SubjectList_3Y1S()
        dg3Y1S.Rows.Clear()
        Dim yearlevel As String = "3rd Year"
        Dim semester As String = "1st Semester"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg3Y1S.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg3Y1S)
            AutoSizePanel(Panel_3Y1S, dg3Y1S)
        End While
        dr.Close()
        cn.Close()

        If dg3Y1S.RowCount = 0 Then
            Panel_3Y1S.Visible = False
        Else
            Panel_3Y1S.Visible = True
            Try
                For Each row As DataGridViewRow In dg3Y1S.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Sub SubjectList_3Y2S()
        dg3Y2S.Rows.Clear()
        Dim yearlevel As String = "3rd Year"
        Dim semester As String = "2nd Semester"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg3Y2S.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg3Y2S)
            AutoSizePanel(Panel_3Y2S, dg3Y2S)
        End While
        dr.Close()
        cn.Close()

        If dg3Y2S.RowCount = 0 Then
            Panel_3Y2S.Visible = False
        Else
            Panel_3Y2S.Visible = True
            Try
                For Each row As DataGridViewRow In dg3Y2S.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Sub SubjectList_3YS()
        dg3YS.Rows.Clear()
        Dim yearlevel As String = "3rd Year"
        Dim semester As String = "Summer"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg3YS.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg3YS)
            AutoSizePanel(Panel_3YS, dg3YS)
        End While
        dr.Close()
        cn.Close()

        If dg3YS.RowCount = 0 Then
            Panel_3YS.Visible = False
        Else
            Panel_3YS.Visible = True
            Try
                For Each row As DataGridViewRow In dg3YS.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    '-----------------------------------------------

    Sub SubjectList_4Y1S()
        dg4Y1S.Rows.Clear()
        Dim yearlevel As String = "4th Year"
        Dim semester As String = "1st Semester"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg4Y1S.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg4Y1S)
            AutoSizePanel(Panel_4Y1S, dg4Y1S)
        End While
        dr.Close()
        cn.Close()

        If dg4Y1S.RowCount = 0 Then
            Panel_4Y1S.Visible = False
        Else
            Panel_4Y1S.Visible = True
            Try
                For Each row As DataGridViewRow In dg4Y1S.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Sub SubjectList_4Y2S()
        dg4Y2S.Rows.Clear()
        Dim yearlevel As String = "4th Year"
        Dim semester As String = "2nd Semester"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg4Y2S.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg4Y2S)
            AutoSizePanel(Panel_4Y2S, dg4Y2S)
        End While
        dr.Close()
        cn.Close()

        If dg4Y2S.RowCount = 0 Then
            Panel_4Y2S.Visible = False
        Else
            Panel_4Y2S.Visible = True
            Try
                For Each row As DataGridViewRow In dg4Y2S.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Sub SubjectList_4YS()
        dg4YS.Rows.Clear()
        Dim yearlevel As String = "3rd Year"
        Dim semester As String = "Summer"
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & yearlevel & "' and `semester` = '" & semester & "' and `curriculumID` = " & currid & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dg4YS.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
            AutoSizeDataGridView(dg4YS)
            AutoSizePanel(Panel_4YS, dg4YS)
        End While
        dr.Close()
        cn.Close()

        If dg4YS.RowCount = 0 Then
            Panel_4YS.Visible = False
        Else
            Panel_4YS.Visible = True
            Try
                For Each row As DataGridViewRow In dg4YS.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                        row.Cells(9).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                Next
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    '-----------------------------------------------

    Private Sub txtCurr_Click(sender As Object, e As EventArgs) Handles txtCurr.Click

    End Sub

    Private Sub dgStudentCurrList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgStudentCurrList.CellContentClick
        Dim colname As String = dgStudentCurrList.Columns(e.ColumnIndex).Name
        If colname = "colStatus" Then
            Dim pictureBoxImage As Bitmap = CType(pic1.Image, Bitmap)
            Dim dataGridViewImage As Bitmap = CType(dgStudentCurrList.CurrentRow.Cells(9).Value, Bitmap)
            If CompareImages(pictureBoxImage, dataGridViewImage) Then

                If MsgBox("Are you sure you want to update student curriculum record?", vbYesNo + vbQuestion) = vbYes Then
                    query("UPDATE tbl_students_curriculum SET `sc_total_units` = " & CInt(dgStudentCurrList.CurrentRow.Cells(6).Value) & ", `sc_status` = '" & If(CInt(dgStudentCurrList.CurrentRow.Cells(5).Value) >= CInt(dgStudentCurrList.CurrentRow.Cells(6).Value), "Completed", "Ongoing") & "' WHERE sc_student_id = '" & StudentID & "' and scg_curr_id = " & currid & "")
                    UserActivity("Updated student " & txtStudent.Text & " curriculum " & txtCurr.Text & " record. " & dgStudentCurrList.CurrentRow.Cells(5).Value & " units earned out of " & dgStudentCurrList.CurrentRow.Cells(6).Value & ".", "STUDENT EVALUATION")
                    Dim imageFromPictureBox As Image = pic2.Image
                    dgStudentCurrList.CurrentRow.Cells(9).Value = imageFromPictureBox
                    MsgBox("Successfully updated student " & txtStudent.Text & " curriculum " & txtCurr.Text & " record.", vbInformation)
                Else
                    MsgBox("Updating student " & txtStudent.Text & " curriculum " & txtCurr.Text & " record was cancelled.", vbInformation)
                End If

            Else
            End If
        ElseIf colname = "colRemove" Then

            If MsgBox("Are you sure you want to remove this student curriculum record?", vbYesNo + vbQuestion) = vbYes Then
                query("DELETE FROM tbl_students_curriculum WHERE sc_student_id = '" & StudentID & "' and sc_curr_id = " & currid & "")
                query("DELETE FROM tbl_students_curriculum_grades WHERE scg_student_id = '" & StudentID & "' and scg_curr_id = " & currid & "")
                UserActivity("Removed student " & txtStudent.Text & " curriculum " & txtCurr.Text & " record.", "STUDENT EVALUATION")
                StudentAssignedCurrList()
                MsgBox("Successfully removed student " & txtStudent.Text & " curriculum " & txtCurr.Text & " record.", vbInformation)
            Else
                MsgBox("Removing student " & txtStudent.Text & " curriculum " & txtCurr.Text & " record was cancelled.", vbExclamation)
            End If

        ElseIf colname = "colLoad" Then
            currid = dgStudentCurrList.CurrentRow.Cells(1).Value
            txtCurr.Text = dgStudentCurrList.CurrentRow.Cells(2).Value

            Dim imageFromPictureBox As Image = pic1.Image
            dgStudentCurrList.CurrentRow.Cells(9).Value = imageFromPictureBox

            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select * from tbl_students_curriculum where sc_curr_id = " & currid & " and sc_student_id = '" & StudentID & "'", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    btnAssignCurriculum.Visible = False
                Else
                    btnAssignCurriculum.Visible = True
                End If
                dr.Close()
                cn.Close()
            Catch ex As Exception
                dr.Close()
                cn.Close()
                MsgBox(ex.Message)
            End Try

            LoadCurriculum()

            'frmEvaluationSubjects.Show()
            'frmEvaluationSubjects.Hide()
            OpenFormEvaluation(frmEvaluationSubjects)
            btnSubjects.Visible = True
            fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmEvaluationSubjects.cbAcademicYear, "tbl_period", "PERIOD", "period_id")


            Dim sql As String
            sql = "SELECT (class_schedule_id) as 'ID', (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (subject_units) as 'Units', (ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (room_code) as 'Room', (Instructor) as 'Instructor', population, csperiod_id from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_enrollment_subjects where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_enrollment_subjects.es_class_schedule_id and tbl_enrollment_subjects.es_student_id = '" & StudentID & "' and tbl_enrollment_subjects.es_period_id = " & CInt(frmEvaluationSubjects.cbAcademicYear.SelectedValue) & ";"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                frmEvaluationSubjects.dgClassSchedList.Rows.Add(dr.Item("ID").ToString, dr.Item("Class").ToString, dr.Item("Subject Code").ToString, dr.Item("Subject Desc.").ToString, dr.Item("Units").ToString, dr.Item("Days").ToString, dr.Item("Start Time").ToString, dr.Item("End Time").ToString, dr.Item("Room").ToString, dr.Item("Instructor").ToString)
            End While
            dr.Close()
            cn.Close()
        End If
    End Sub

    Sub OpenFormEvaluation(frm As Form)
        If frm.IsHandleCreated Then
            frm.BringToFront()
        Else
            frm.TopLevel = False
            frmMain.Controls.Add(frm)
            frm.BringToFront()
            frm.Show()
            centerForm(frm)
        End If
    End Sub


    Sub LoadCurriculum()

        dg1Y1S.Columns(10).ReadOnly = True
        dg1Y2S.Columns(10).ReadOnly = True
        dg1YS.Columns(10).ReadOnly = True

        dg2Y1S.Columns(10).ReadOnly = True
        dg2Y2S.Columns(10).ReadOnly = True
        dg2YS.Columns(10).ReadOnly = True

        dg3Y1S.Columns(10).ReadOnly = True
        dg3Y2S.Columns(10).ReadOnly = True
        dg3YS.Columns(10).ReadOnly = True

        dg4Y1S.Columns(10).ReadOnly = True
        dg4Y2S.Columns(10).ReadOnly = True
        dg4YS.Columns(10).ReadOnly = True

        'First Year
        SubjectList_1Y1S()
        SubjectList_1Y2S()
        SubjectList_1YS()

        LoadGrades(dg1Y1S, earned_1Y1S)
        LoadGrades(dg1Y2S, earned_1Y2S)
        LoadGrades(dg1YS, earned_1YS)

        '2nd Year
        SubjectList_2Y1S()
        SubjectList_2Y2S()
        SubjectList_2YS()

        LoadGrades(dg2Y1S, earned_2Y1S)
        LoadGrades(dg2Y2S, earned_2Y2S)
        LoadGrades(dg2YS, earned_2YS)

        '3rd Year
        SubjectList_3Y1S()
        SubjectList_3Y2S()
        SubjectList_3YS()

        LoadGrades(dg3Y1S, earned_3Y1S)
        LoadGrades(dg3Y2S, earned_3Y2S)
        LoadGrades(dg3YS, earned_3YS)

        '4th Year
        SubjectList_4Y1S()
        SubjectList_4Y2S()
        SubjectList_4YS()

        LoadGrades(dg4Y1S, earned_4Y1S)
        LoadGrades(dg4Y2S, earned_4Y2S)
        LoadGrades(dg4YS, earned_4YS)


        Dim earned_a As Integer = CInt(earned_1Y1S.Text)
        Dim earned_b As Integer = CInt(earned_1Y2S.Text)
        Dim earned_c As Integer = CInt(earned_1YS.Text)

        Dim earned_d As Integer = CInt(earned_2Y1S.Text)
        Dim earned_e As Integer = CInt(earned_2Y2S.Text)
        Dim earned_f As Integer = CInt(earned_2YS.Text)

        Dim earned_g As Integer = CInt(earned_3Y1S.Text)
        Dim earned_h As Integer = CInt(earned_3Y2S.Text)
        Dim earned_i As Integer = CInt(earned_3YS.Text)

        Dim earned_j As Integer = CInt(earned_4Y1S.Text)
        Dim earned_k As Integer = CInt(earned_4Y2S.Text)
        Dim earned_l As Integer = CInt(earned_4YS.Text)

        dgStudentCurrList.CurrentRow.Cells(6).Value = earned_a + earned_b + earned_c + earned_d + earned_e + earned_f + earned_g + earned_h + earned_i + earned_j + earned_k + earned_l
    End Sub

    Sub LoadGrades(ByVal DTG As Object, ByVal units As Object)
        DTG.columns(12).visible = True
        DTG.Columns(12).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        DTG.columns(11).readonly = True

        Dim imageFromPictureBox As Image = pic5.Image
        Dim imageFromPictureBox2 As Image = pic6.Image
        Dim imageFromPictureBox3 As Image = pic7.Image

        Dim imageFromPictureBox4 As Image = pic8.Image
        Dim imageFromPictureBox5 As Image = pic9.Image

        Dim imageFromPictureBox6 As Image = pic10.Image
        Dim imageFromPictureBox7 As Image = pic11.Image
        For Each row As DataGridViewRow In DTG.rows
            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT t1.sg_grade as 'Grade' from tbl_students_grades t1 JOIN tbl_subject t2 ON t1.sg_subject_id = t2.subject_id where t1.sg_student_id = '" & StudentID & "' and t2.subject_description = '" & row.Cells(2).Value & "' order by CAST(t1.sg_grade AS FLOAT) asc", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    row.Cells(10).Value = dr.Item("Grade").ToString.Trim
                    If row.Cells(10).Value.ToString = "D" Or row.Cells(10).Value.ToString = "W" Or row.Cells(10).Value.ToString = "" Then
                        row.Cells(11).Value = "0"
                        row.Cells(14).Value = imageFromPictureBox6
                        row.DefaultCellStyle.ForeColor = Color.Red
                        row.DefaultCellStyle.SelectionForeColor = Color.Red
                    ElseIf CDbl(row.Cells(10).Value) <= CDbl(row.Cells(6).Value) Then
                        row.Cells(11).Value = row.Cells(4).Value
                        row.Cells(14).Value = imageFromPictureBox7
                        row.DefaultCellStyle.ForeColor = Color.Green
                        row.DefaultCellStyle.SelectionForeColor = Color.Green
                    End If
                    dr.Close()
                    cn.Close()

                    Dim sql As String
                    Dim rowCount As Integer = 0
                    sql = "Select t1.sg_grade As 'Grade' from tbl_students_grades t1 JOIN tbl_subject t2 ON t1.sg_subject_id = t2.subject_id where t1.sg_student_id = '" & StudentID & "' and t2.subject_description = '" & row.Cells(2).Value & "' order by CAST(t1.sg_grade AS FLOAT) asc"
                    cn.Close()
                        cn.Open()
                        cm = New MySqlCommand(Sql, cn)
                        dr = cm.ExecuteReader
                        While dr.Read
                            rowCount += 1
                        End While
                        dr.Close()
                        cn.Close()

                        If rowCount > 1 Then
                            row.Cells(12).Value = imageFromPictureBox
                        Else
                            row.Cells(12).Value = imageFromPictureBox2
                        End If


                        If row.Cells(10).Value = String.Empty Then
                            row.Cells(13).Value = imageFromPictureBox4
                        Else
                            row.Cells(13).Value = imageFromPictureBox5
                        End If

                    Else

                        If row.Cells(1).Value.ToString.Contains("NSTP") Or row.Cells(1).Value.ToString = "PE 1" Or row.Cells(1).Value.ToString = "PE 2" Or row.Cells(1).Value.ToString = "PE 3" Or row.Cells(1).Value.ToString = "PE 4" Then
                        dr.Close()
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT t1.sg_grade as 'Grade' from tbl_students_grades t1 JOIN tbl_subject t2 ON t1.sg_subject_id = t2.subject_id where t1.sg_student_id = '" & StudentID & "' and t2.subject_code = '" & row.Cells(1).Value & "' order by CAST(t1.sg_grade AS FLOAT) asc", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            row.Cells(10).Value = dr.Item("Grade").ToString.Trim
                            If row.Cells(10).Value.ToString = "D" Or row.Cells(10).Value.ToString = "W" Or row.Cells(10).Value.ToString = "5.0" Or row.Cells(10).Value.ToString = "" Then
                                row.Cells(11).Value = "0"
                                row.Cells(14).Value = imageFromPictureBox6
                                row.DefaultCellStyle.ForeColor = Color.Red
                                row.DefaultCellStyle.SelectionForeColor = Color.Red
                            ElseIf CDbl(row.Cells(10).Value) <= CDbl(row.Cells(6).Value) Then
                                row.Cells(11).Value = row.Cells(4).Value
                                row.Cells(14).Value = imageFromPictureBox7
                                row.DefaultCellStyle.ForeColor = Color.Green
                                row.DefaultCellStyle.SelectionForeColor = Color.Green
                            End If
                            dr.Close()
                            cn.Close()

                            Dim sql As String
                            Dim rowCount As Integer = 0
                            sql = "SELECT t1.sg_grade as 'Grade' from tbl_students_grades t1 JOIN tbl_subject t2 ON t1.sg_subject_id = t2.subject_id where t1.sg_student_id = '" & StudentID & "' and t2.subject_code = '" & row.Cells(1).Value & "' order by CAST(t1.sg_grade AS FLOAT) asc"
                            cn.Close()
                            cn.Open()
                            cm = New MySqlCommand(sql, cn)
                            dr = cm.ExecuteReader
                            While dr.Read
                                rowCount += 1
                            End While
                            dr.Close()
                            cn.Close()
                            If rowCount > 1 Then
                                row.Cells(12).Value = imageFromPictureBox
                            Else
                                row.Cells(12).Value = imageFromPictureBox2
                            End If
                            If row.Cells(10).Value = String.Empty Then
                                row.Cells(13).Value = imageFromPictureBox4
                            Else
                                row.Cells(13).Value = imageFromPictureBox5
                            End If
                        Else
                            dr.Close()
                            cn.Close()
                            row.Cells(10).Value = String.Empty
                            row.Cells(11).Value = "0"
                            row.Cells(14).Value = imageFromPictureBox6
                            row.Cells(12).Value = imageFromPictureBox3
                        End If
                    Else
                        dr.Close()
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT t1.scg_grade as 'Grade', t1.scg_grade_id as 'GradeID' from tbl_students_curriculum_grades t1 where t1.scg_student_id = '" & StudentID & "' and t1.scg_subject_id = '" & row.Cells(0).Value & "' and t1.scg_curr_id = " & currid & " order by CAST(t1.scg_grade AS FLOAT) asc", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            row.Cells(10).Value = dr.Item("Grade").ToString.Trim
                            If row.Cells(10).Value.ToString = "D" Or row.Cells(10).Value.ToString = "W" Or row.Cells(10).Value.ToString = "5.0" Or row.Cells(10).Value.ToString = "" Then
                                row.Cells(11).Value = "0"
                                row.Cells(14).Value = imageFromPictureBox6
                                row.DefaultCellStyle.ForeColor = Color.Red
                                row.DefaultCellStyle.SelectionForeColor = Color.Red
                            ElseIf CDbl(row.Cells(10).Value) <= CDbl(row.Cells(6).Value) Then
                                row.Cells(11).Value = row.Cells(4).Value
                                row.Cells(14).Value = imageFromPictureBox7
                                row.DefaultCellStyle.ForeColor = Color.Green
                                row.DefaultCellStyle.SelectionForeColor = Color.Green
                            End If

                            If CInt(dr.Item("GradeID").ToString) = 0 Then
                                row.Cells(12).Value = imageFromPictureBox2
                            Else
                                row.Cells(12).Value = imageFromPictureBox
                            End If

                            dr.Close()
                            cn.Close()
                        Else
                            dr.Close()
                            cn.Close()
                            row.Cells(10).Value = String.Empty
                            row.Cells(11).Value = "0"
                            row.Cells(14).Value = imageFromPictureBox6
                            row.Cells(12).Value = imageFromPictureBox3
                        End If
                    End If
                End If
                units.text = GetColumnSum(DTG, 11)
            Catch ex As Exception
                dr.Close()
                cn.Close()
            End Try
        Next
    End Sub

    Private Sub dgStudentCurrList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgStudentCurrList.CellClick
    End Sub

    Private Sub btnAssignCurriculum_Click(sender As Object, e As EventArgs) Handles btnAssignCurriculum.Click
        If MsgBox("Are you sure you want to assign this curriculum to student " & StudentName & "?", vbYesNo + vbQuestion) = vbYes Then
            If txtCurr.Text = String.Empty Then
                MsgBox("No Curriculum selected.", vbCritical)
            Else
                query("INSERT INTO `tbl_students_curriculum`(`sc_student_id`, `sc_curr_id`, `sc_assigned_by`, `sc_total_units`) VALUES (" & StudentID & "," & currid & "," & str_userid & ",0)")
                MsgBox("Curriculum successfully assigned to the student.", vbInformation)
                UserActivity("Assigned curriculum " & txtCurr.Text & " to student " & txtStudent.Text & ".", "STUDENT EVALUATION")
                StudentAssignedCurrList()
            End If
        End If
    End Sub

    Public Sub StudentGradeList()
        Try

            dgGradeList.Rows.Clear()
        Dim sql As String
            sql = "select tbl_students_grades.sg_id as 'ID', (schl_name) as 'SCHOOL', concat(period_name,'-',period_semester) as 'ACADEMIC YEAR', (subject_code) as 'CODE', (subject_description) as 'DESCRIPTION', if(sg_grade REGEXP '^-?[0-9]+$' >  0 and sg_grade < 6 and sg_school_id = '0' , ROUND(sg_grade,1), sg_grade)  as 'GRADES', sg_grade_status as 'STATUS' from tbl_students_grades, tbl_subject, tbl_period, tbl_schools, tbl_course where tbl_students_grades.sg_subject_id = tbl_subject.subject_id and tbl_students_grades.sg_period_id = tbl_period.period_id and tbl_students_grades.sg_course_id = tbl_course.course_id and tbl_students_grades.sg_school_id = tbl_schools.schl_id and sg_student_id = '" & StudentID & "' and sg_grade_visibility NOT IN (1) and sg_grade_status NOT IN ('Pending', 'Enrolled') and (subject_code LIKE '%" & txtSearch.Text & "%' or subject_description LIKE '%" & txtSearch.Text & "%') and period_semester NOT IN ('Review') order by period_name, period_semester, subject_code asc"
            cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgGradeList.Rows.Add(dr.Item("ID").ToString, dr.Item("SCHOOL").ToString, dr.Item("ACADEMIC YEAR").ToString, dr.Item("CODE").ToString, dr.Item("DESCRIPTION").ToString, dr.Item("GRADES").ToString, dr.Item("STATUS").ToString)
        End While
        dr.Close()
        cn.Close()

        dgPanelPadding(dgGradeList, dgPanel)

        Dim seenValues As New HashSet(Of String)()
        Dim seenValues2 As New HashSet(Of String)()
            For i As Integer = 0 To dgGradeList.Rows.Count - 1
                Dim currentValue As String = dgGradeList.Rows(i).Cells(1).Value.ToString()
                Dim currentValue2 As String = dgGradeList.Rows(i).Cells(2).Value.ToString()
                If seenValues.Contains(currentValue) Then
                    dgGradeList.Rows(i).Cells(1).Style.ForeColor = Color.White
                Else
                    seenValues.Add(currentValue)
                End If
                If seenValues2.Contains(currentValue2) Then
                    dgGradeList.Rows(i).Cells(2).Style.ForeColor = Color.White
                Else
                    seenValues2.Add(currentValue2)
                End If
            Next

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgGradeList.Rows.Clear()

        End Try
    End Sub

    Private Sub DataGridView_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        Dim dgv As DataGridView = CType(sender, DataGridView)
        activeDataGridView = dgv
        If dgv.Columns.Count = 15 Then
            If e.RowIndex >= 0 AndAlso e.ColumnIndex = 13 Then
                Dim currentRow As DataGridViewRow = dgv.Rows(e.RowIndex)
                Dim pictureBoxImage As Bitmap = CType(pic9.Image, Bitmap)
                Dim dataGridViewImage As Bitmap = CType(currentRow.Cells(13).Value, Bitmap)
                If CompareImages(pictureBoxImage, dataGridViewImage) Then

                Else
                    frmEvaluationGrade.lblCurrCode.Text = activeDataGridView.CurrentRow.Cells(1).Value
                    frmEvaluationGrade.lblCurrDesc.Text = activeDataGridView.CurrentRow.Cells(2).Value
                    frmEvaluationGrade.ShowDialog()
                End If
            ElseIf e.RowIndex >= 0 AndAlso e.ColumnIndex = 12 Then
                Dim currentRow As DataGridViewRow = dgv.Rows(e.RowIndex)
                Dim pictureBoxImage As Bitmap = CType(pic5.Image, Bitmap)
                Dim dataGridViewImage As Bitmap = CType(currentRow.Cells(12).Value, Bitmap)
                If CompareImages(pictureBoxImage, dataGridViewImage) Then
                    Dim currentRow2 As DataGridViewRow = dgv.Rows(e.RowIndex)
                    Dim pictureBoxImage2 As Bitmap = CType(pic8.Image, Bitmap)
                    Dim dataGridViewImage2 As Bitmap = CType(currentRow.Cells(13).Value, Bitmap)
                    If CompareImages(pictureBoxImage2, dataGridViewImage2) Then
                        frmLinkGrade.btnLink.Visible = False

                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT t1.scg_grade as 'Grade', t2.sg_grade_status, t3.subject_code, t3.subject_description, t4.PERIOD, t5.schl_name from tbl_students_curriculum_grades t1 JOIN tbl_students_grades t2 ON t1.scg_grade_id = t2.sg_id JOIN tbl_subject t3 ON t1.scg_subject_id = t3.subject_id JOIN period t4 ON t2.sg_period_id = t4.period_id JOIN tbl_schools t5 on t2.sg_school_id = t5.schl_id where t1.scg_subject_id = " & CInt(activeDataGridView.CurrentRow.Cells(0).Value) & " and t1.scg_curr_id = " & currid & " and t1.scg_student_id = '" & StudentID & "'", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            frmLinkGrade.Label1.Text = "Linked Grade"
                            frmLinkGrade.Label2.Text = "Linked To:"
                            frmLinkGrade.cbGrade.Visible = False
                            frmLinkGrade.lblGrade.Text = dr.Item("Grade").ToString
                            frmLinkGrade.lblAcadStatus.Text = dr.Item("sg_grade_status").ToString & " - " & dr.Item("PERIOD").ToString
                            frmLinkGrade.lblCode.Text = dr.Item("subject_code").ToString
                            frmLinkGrade.lblDesc.Text = dr.Item("subject_description").ToString
                            frmLinkGrade.lblSchool.Text = dr.Item("schl_name").ToString
                            frmLinkGrade.lblCurrCode.Text = activeDataGridView.CurrentRow.Cells(1).Value
                            frmLinkGrade.lblCurrDesc.Text = activeDataGridView.CurrentRow.Cells(2).Value
                            frmLinkGrade.cbGrade.Enabled = False
                            frmLinkGrade.ShowDialog()
                        Else

                        End If
                        dr.Close()
                        cn.Close()

                    Else

                        Try
                            frmShowMultiGrades.dgGradeList.Rows.Clear()
                            Dim i As Integer
                            Dim sql As String
                            If activeDataGridView.CurrentRow.Cells(1).Value.ToString.Contains("NSTP") Or activeDataGridView.CurrentRow.Cells(1).Value.ToString = "PE 1" Or activeDataGridView.CurrentRow.Cells(1).Value.ToString = "PE 2" Or activeDataGridView.CurrentRow.Cells(1).Value.ToString = "PE 3" Or activeDataGridView.CurrentRow.Cells(1).Value.ToString = "PE 4" Then
                                sql = "select concat(period_name,'-',period_semester) as 'ACADEMIC YEAR', if(sg_grade REGEXP '^-?[0-9]+$' >  0 and sg_grade < 6 and sg_school_id = '0' , ROUND(sg_grade,1), sg_grade)  as 'GRADES' from tbl_students_grades, tbl_subject, tbl_period, tbl_schools, tbl_course where tbl_students_grades.sg_subject_id = tbl_subject.subject_id and tbl_students_grades.sg_period_id = tbl_period.period_id and tbl_students_grades.sg_course_id = tbl_course.course_id and tbl_students_grades.sg_school_id = tbl_schools.schl_id and sg_student_id = '" & StudentID & "' and sg_grade_visibility NOT IN (1) and sg_grade_status NOT IN ('Pending') and subject_code = '" & activeDataGridView.CurrentRow.Cells(1).Value & "' and period_semester NOT IN ('Review') order by period_name, period_semester, subject_code asc"
                            Else
                                sql = "select concat(period_name,'-',period_semester) as 'ACADEMIC YEAR', if(sg_grade REGEXP '^-?[0-9]+$' >  0 and sg_grade < 6 and sg_school_id = '0' , ROUND(sg_grade,1), sg_grade)  as 'GRADES' from tbl_students_grades, tbl_subject, tbl_period, tbl_schools, tbl_course where tbl_students_grades.sg_subject_id = tbl_subject.subject_id and tbl_students_grades.sg_period_id = tbl_period.period_id and tbl_students_grades.sg_course_id = tbl_course.course_id and tbl_students_grades.sg_school_id = tbl_schools.schl_id and sg_student_id = '" & StudentID & "' and sg_grade_visibility NOT IN (1) and sg_grade_status NOT IN ('Pending') and subject_description = '" & activeDataGridView.CurrentRow.Cells(2).Value & "' and period_semester NOT IN ('Review') order by period_name, period_semester, subject_code asc"
                            End If
                            cn.Close()
                            cn.Open()
                            cm = New MySqlCommand(sql, cn)
                            dr = cm.ExecuteReader
                            While dr.Read
                                i += 1
                                frmShowMultiGrades.dgGradeList.Rows.Add(i, dr.Item("ACADEMIC YEAR").ToString, dr.Item("GRADES").ToString)
                            End While
                            dr.Close()
                            cn.Close()
                        Catch ex As Exception
                            dr.Close()
                            cn.Close()
                            MsgBox(ex.Message)
                        End Try
                        frmShowMultiGrades.lblCurrCode.Text = activeDataGridView.CurrentRow.Cells(1).Value
                        frmShowMultiGrades.lblCurrDesc.Text = activeDataGridView.CurrentRow.Cells(2).Value
                        frmShowMultiGrades.ShowDialog()
                    End If
                Else
                End If
            ElseIf e.RowIndex >= 0 AndAlso e.ColumnIndex = 14 Then
                Dim pictureBoxImage As Bitmap = CType(pic10.Image, Bitmap)
                Dim dataGridViewImage As Bitmap = CType(activeDataGridView.CurrentRow.Cells(14).Value, Bitmap)
                If CompareImages(pictureBoxImage, dataGridViewImage) Then
                    'Try
                    frmEvaluationSchedules.lblCurrCode.Text = activeDataGridView.CurrentRow.Cells(1).Value
                    frmEvaluationSchedules.lblCurrDesc.Text = activeDataGridView.CurrentRow.Cells(2).Value

                    frmEvaluationSchedules.dgClassSchedList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.None
                    frmEvaluationSchedules.dgClassSchedList.Rows.Clear()

                    Dim sql As String
                    sql = "SELECT t1.`class_schedule_id`, t1.`cb_code`, t1.`subject_code`, t1.`subject_description`, t1.`subject_units`, t1.`ds_code`, t1.`time_start_schedule`, t1.`time_end_schedule`, t1.`room_code`, t1.`Instructor`, t1.`population`, t1.`csperiod_id`, t1.`is_active` FROM `classschedulelist` t1 JOIN tbl_class_block t2 ON t1.cb_id = t2. cb_id where t1.csperiod_id = " & CInt(frmEvaluationSubjects.cbAcademicYear.SelectedValue) & " and t2.cb_course_id = " & CourseID & " and (t1.subject_code = '" & activeDataGridView.CurrentRow.Cells(1).Value & "' or t1.subject_description = '" & activeDataGridView.CurrentRow.Cells(2).Value & "') ORDER BY CASE WHEN t2.cb_course_id = " & CourseID & " THEN 0 WHEN t2.cb_year_level = '" & YearLevel & "' THEN 1 ELSE 2 END limit 500"
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand(sql, cn)
                    dr = cm.ExecuteReader
                    While dr.Read
                        frmEvaluationSchedules.dgClassSchedList.Rows.Add(dr.Item("class_schedule_id").ToString, dr.Item("cb_code").ToString, dr.Item("subject_code").ToString, dr.Item("subject_description").ToString, dr.Item("subject_units").ToString, dr.Item("ds_code").ToString, dr.Item("time_start_schedule").ToString, dr.Item("time_end_schedule").ToString, dr.Item("room_code").ToString, dr.Item("Instructor").ToString, dr.Item("population").ToString, "👁", dr.Item("csperiod_id").ToString)
                    End While
                    dr.Close()
                    cn.Close()

                    frmEvaluationSchedules.ShowDialog()
                    'Catch ex As Exception

                    'End Try
                End If
            End If
            End If
    End Sub

    Private Sub dgStudentCurrList_VisibleChanged(sender As Object, e As EventArgs) Handles dgStudentCurrList.VisibleChanged
        frmEvaluationSubjects.Close()
    End Sub

    Private Sub btnSubjects_Click(sender As Object, e As EventArgs) Handles btnSubjects.Click
        frmEvaluationSubjects.Show()
        'OpenFormEvaluation(frmEvaluationSubjects)
        ''frmEvaluationSubjects.Show()
        ''fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmEvaluationSubjects.cbAcademicYear, "tbl_period", "PERIOD", "period_id")
    End Sub

End Class