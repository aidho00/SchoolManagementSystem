Imports MySql.Data.MySqlClient

Public Class frmSetupInstitutionalDiscount

    Dim StudentAssessmentID As Integer = 0

    Dim AssessmentCourseID As Integer = 0

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

    Sub Assessment()
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT af_id from tbl_assessment_fee where af_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and af_course_id = " & AssessmentCourseID & " and af_year_level = '" & cbYearLevel.Text & "' and af_gender = '" & cbGender.Text & "'", cn)
            StudentAssessmentID = cm.ExecuteScalar
            cn.Close()
        Catch ex As Exception
            cn.Close()
            StudentAssessmentID = 0
        End Try
        If StudentAssessmentID = 0 Then
            lblAssessmentStatus.Text = "Invalid Assessment."
            lblAssessmentStatus.ForeColor = Color.Red
        Else
            lblAssessmentStatus.Text = "Valid Assessment."
            lblAssessmentStatus.ForeColor = Color.Green
        End If
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT af_id, af_subtotal_amount, af_other_fee FROM tbl_assessment_fee where af_id = " & StudentAssessmentID & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtAssessmentAmount.Text = Format(CDec(dr.Item("af_subtotal_amount").ToString), "#,##0.00")
                txtOtherFees.Text = Format(CDec(dr.Item("af_other_fee").ToString), "#,##0.00")
            Else
            End If
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            txtAssessmentAmount.Text = "0.00"
            txtOtherFees.Text = "0.00"
        End Try
    End Sub

    Private Sub lblAssessmentStatus_TextChanged(sender As Object, e As EventArgs) Handles lblAssessmentStatus.TextChanged

    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        frmTitle.Text = "Search Student"
        SearchPanel.Visible = True
        DiscountStudentList()
        dgStudentList.BringToFront()
        txtSearch.Select()
    End Sub

    Sub DiscountStudentList()
        Try
            dgStudentList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, course_name, s_course_status from tbl_enrollment LEFT JOIN tbl_student ON tbl_enrollment.estudent_id = tbl_student.s_id_no LEFT JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where tbl_enrollment.eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & " and (tbl_student.s_ln like '" & txtSearch.Text & "%' or tbl_student.s_fn like '" & txtSearch.Text & "%' or tbl_student.s_mn like '" & txtSearch.Text & "%' or tbl_student.s_id_no like '" & txtSearch.Text & "%' or tbl_student.s_yr_lvl like '" & txtSearch.Text & "%' or tbl_course.course_code like '" & txtSearch.Text & "%') order by s_id_no asc limit 500"
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

            dgPanelPadding(dgStudentList, dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgStudentList.Rows.Clear()

        End Try
    End Sub

    Private Sub frmSetupInstitutionalDiscount_Load(sender As Object, e As EventArgs) Handles Me.Load
        fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", cbAcademicYear, "tbl_period", "PERIOD", "period_id")
    End Sub

    Private Sub reload_Click(sender As Object, e As EventArgs) Handles reload.Click
        fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", cbAcademicYear, "tbl_period", "PERIOD", "period_id")
    End Sub

    Private Sub btnSearchCourse_Click(sender As Object, e As EventArgs) Handles btnSearchCourse.Click
        frmTitle.Text = "Search Course"
        SearchPanel.Visible = True
        DiscountCourseList()
        dgCourseList.BringToFront()
        txtSearch.Select()
    End Sub

    Sub DiscountCourseList()
        Try
            dgCourseList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select course_id, course_code, course_name, course_major, course_status from tbl_course where (course_code LIKE '%" & txtSearch.Text & "%' or course_name LIKE '%" & txtSearch.Text & "%') order by course_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgCourseList.Rows.Add(dr.Item("course_id").ToString, dr.Item("course_code").ToString, dr.Item("course_name").ToString, dr.Item("course_major").ToString, dr.Item("course_status").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(dgCourseList, dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgCourseList.Rows.Clear()
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If frmTitle.Text = "Search Student" Then
            DiscountStudentList()
        ElseIf frmTitle.Text = "Search Course" Then
            DiscountCourseList()
        End If
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If frmTitle.Text = "Search Student" Then
            dgStudentListSetup.Rows.Add(dgStudentList.CurrentRow.Cells(1).Value, dgStudentList.CurrentRow.Cells(2).Value, dgStudentList.CurrentRow.Cells(3).Value, dgStudentList.CurrentRow.Cells(4).Value, dgStudentList.CurrentRow.Cells(5).Value, dgStudentList.CurrentRow.Cells(6).Value, dgStudentList.CurrentRow.Cells(7).Value, dgStudentList.CurrentRow.Cells(8).Value, dgStudentList.CurrentRow.Cells(11).Value)
            SearchPanel.Visible = False
        ElseIf frmTitle.Text = "Search Course" Then
            AssessmentCourseID = dgCourseList.CurrentRow.Cells(0).Value
            lblCourse.Text = dgCourseList.CurrentRow.Cells(1).Value & " - " & dgCourseList.CurrentRow.Cells(2).Value
            cbYearLevel.SelectedIndex = 0
            fillCombo("SELECT distinct(af_gender) as af_gender, af_id from tbl_assessment_fee where af_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and af_course_id = " & AssessmentCourseID & " and af_year_level = LEFT('" & cbYearLevel.Text & "', 8)", cbGender, "tbl_assessment_fee", "af_gender", "af_id")
            cbGender.SelectedIndex = 0
            SearchPanel.Visible = False
            Assessment()
        End If
        txtSearch.Text = ""
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            SearchPanel.Visible = False
        Else
            Me.Close()
        End If
    End Sub

    Private Sub cbYearLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbYearLevel.SelectedIndexChanged
        Assessment()
    End Sub

    Private Sub cbGender_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbGender.SelectedIndexChanged
        Assessment()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If StudentAssessmentID = 0 Then
            MsgBox("No assessment selected. Please select an assessment to proceed with setup.", vbCritical)
            Return
        End If
        If MsgBox("Are you sure you want to save these assessment settings?", vbYesNo + vbQuestion) = vbYes Then
            'Institutional Discount Setup
            For Each row As DataGridViewRow In dgStudentListSetup.Rows
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT * FROM tbl_assessment_institutional_discount where aid_student_id = '" & row.Cells(0).Value & "' and aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                Dim sdr2 As MySqlDataReader = cm.ExecuteReader()
                If (sdr2.Read() = True) Then
                    query("update tbl_assessment_institutional_discount set aid_percentage = " & CDec(txtPercentage.Text) & " where aid_student_id = '" & row.Cells(0).Value & "' and aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    UserActivity("Updated student " & row.Cells(0).Value & " " & row.Cells(4).Value & ", " & row.Cells(2).Value & " " & row.Cells(3).Value & " institutional discount in Academic Year " & cbAcademicYear.Text & ".", "STUDENT DISCOUNT")
                Else
                    query("Insert into tbl_assessment_institutional_discount (`aid_student_id`, `aid_period_id`, `aid_percentage`, `aid_assessment_id`) values ('" & row.Cells(0).Value & "', " & CInt(cbAcademicYear.SelectedValue) & ", " & CDec(txtPercentage.Text) & ", " & StudentAssessmentID & ")")
                    UserActivity("Added student " & row.Cells(0).Value & " " & row.Cells(4).Value & ", " & row.Cells(2).Value & " " & row.Cells(3).Value & " institutional discount in Academic Year " & cbAcademicYear.Text & ".", "STUDENT DISCOUNT")
                End If
                sdr2.Close()
                cn.Close()
            Next

            'Assessment Setup
            If CheckBoxAssessment.Checked = True Then
                For Each row As DataGridViewRow In dgStudentListSetup.Rows
                    query("UPDATE tbl_student_paid_account_breakdown SET spab_ass_id= " & StudentAssessmentID & " where spab_stud_id = '" & row.Cells(0).Value & "' and spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    query("UPDATE tbl_pre_cashiering SET ps_ass_id = " & StudentAssessmentID & " where student_id = '" & row.Cells(0).Value & "' and period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    query("UPDATE tbl_assessment_institutional_discount SET aid_assessment_id = " & StudentAssessmentID & " where aid_student_id = '" & row.Cells(0).Value & "' and aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    UserActivity("Changed student " & row.Cells(0).Value & " " & row.Cells(4).Value & ", " & row.Cells(2).Value & " " & row.Cells(3).Value & " account assessment in Academic Year " & cbAcademicYear.Text & ".", "STUDENT ACCOUNT ADJUSTMENT")
                Next
            Else

            End If
            MsgBox("Settings successfully saved.", vbInformation)
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtPercentage_TextChanged(sender As Object, e As EventArgs) Handles txtPercentage.TextChanged

    End Sub

    Private Sub dgStudentListSetup_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgStudentListSetup.CellContentClick
        Dim colname As String = dgStudentListSetup.Columns(e.ColumnIndex).Name
        If colname = "colRemove" Then
            dgStudentListSetup.Rows.RemoveAt(dgStudentListSetup.CurrentRow.Index)
        End If
    End Sub

    Private Sub dgStudentListSetup_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgStudentListSetup.RowsAdded
        DGrowCount.Text = dgStudentListSetup.RowCount
    End Sub

    Private Sub dgStudentListSetup_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgStudentListSetup.RowsRemoved
        DGrowCount.Text = dgStudentListSetup.RowCount
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim strPath As String

        dgStudentListSetup.Rows.Clear()
        dgDummyList.DataSource = Nothing

        Dim con As OleDb.OleDbConnection
        Dim cmdd As New OleDb.OleDbCommand
        Dim da As New OleDb.OleDbDataAdapter
        Dim dt As New DataTable
        Try
            With OpenFileDialog1
                .Filter = "Excel files(.xls)|*.xls| Excel files(*.xlsx)|*.xlsx| All files (*.*)|*.*"
                .FilterIndex = 1
                .Title = "Import grading sheet data from Excel file"
            End With
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                strPath = OpenFileDialog1.FileName
                con = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" & strPath & " ; " & "Extended Properties=Excel 8.0;")
                con.Open()
                With cmdd
                    .Connection = con
                    .CommandText = "select * from [Sheet1$]"
                End With
                da.SelectCommand = cmdd
                da.Fill(dt)
                dgDummyList.Columns.Clear()
                dgDummyList.DataSource = dt
                con.Close()

                dgStudentListSetup.Rows.Clear()

                For Each row As DataGridViewRow In dgDummyList.Rows
                    dgStudentListSetup.Rows.Add(row.Cells(0).Value.ToString, row.Cells(1).Value.ToString, row.Cells(2).Value.ToString, row.Cells(3).Value.ToString, row.Cells(4).Value.ToString, row.Cells(5).Value.ToString, row.Cells(6).Value.ToString, row.Cells(7).Value.ToString, row.Cells(8).Value.ToString)
                Next
                MsgBox("Number of imported rows: " & dgStudentListSetup.RowCount & ".", vbInformation)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        dgDummyList.DataSource = Nothing
    End Sub
End Class