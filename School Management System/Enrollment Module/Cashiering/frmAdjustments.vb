Imports MySql.Data.MySqlClient

Public Class frmAdjustments
    Public Shared StudentID As String = ""
    Public Shared StudentName As String = ""
    Public Shared CourseID As Integer = 0
    Public Shared Course As String = ""
    Public Shared YearLevel As String = ""
    Public Shared Gender As String = ""
    Public Shared CourseStatus As String = ""

    Public Shared StudentAssessmentID As Integer = 0

    Dim AssessmentCourseID As Integer = 0

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
                txtAssessmentTotal.Text = Format(CDec(dr.Item("af_subtotal_amount").ToString) + CDec(dr.Item("af_other_fee").ToString), "#,##0.00")
            Else
            End If
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            txtAssessmentAmount.Text = "0.00"
            txtOtherFees.Text = "0.00"
            txtAssessmentTotal.Text = "0.00"
        End Try
    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        frmTitle.Text = "Search Student"
        SearchPanel.Visible = True
        AdjustmentStudentList()
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

            txtStudent.Text = dgStudentList.CurrentRow.Cells(1).Value & " - " & dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
            txtCourse.Text = dgStudentList.CurrentRow.Cells(8).Value & " - " & dgStudentList.CurrentRow.Cells(10).Value
            txtGenderYearLevel.Text = dgStudentList.CurrentRow.Cells(6).Value & " - " & dgStudentList.CurrentRow.Cells(7).Value
            fillCombo("Select CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_student_paid_account_breakdown JOIN tbl_period ON tbl_student_paid_account_breakdown.spab_period_id = tbl_period.period_id WHERE spab_stud_id = '" & StudentID & "' order by `period_name` desc, `period_status` asc, `period_semester` desc", cbAcademicYear, "CashieringPeriodList", "PERIOD", "period_id")

            'cn.Close()
            'cn.Open()
            'cm = New MySqlCommand("SELECT af_id from tbl_assessment_fee where af_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and af_course_id = " & CourseID & " and af_year_level = LEFT('" & YearLevel & "', 8) and af_gender = '" & Gender & "'", cn)
            'StudentAssessmentID = cm.ExecuteScalar
            'cn.Close()

            SearchPanel.Visible = False
        ElseIf frmTitle.Text = "Search Course" Then
            AssessmentCourseID = dgCourseList.CurrentRow.Cells(0).Value
            lblCourse.Text = dgCourseList.CurrentRow.Cells(1).Value & " - " & dgCourseList.CurrentRow.Cells(2).Value
            SearchPanel.Visible = False
            Assessment()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If frmTitle.Text = "Search Student" Then
            AdjustmentStudentList()
        ElseIf frmTitle.Text = "Search Course" Then
            AdjustmentCourseList()
        End If
    End Sub

    Private Sub lblAssessmentStatus_TextChanged(sender As Object, e As EventArgs) Handles lblAssessmentStatus.TextChanged
        If StudentAssessmentID = 0 Then
            lblAssessmentStatus.Text = "Invalid Assessment."
            lblAssessmentStatus.ForeColor = Color.Red
        Else
            lblAssessmentStatus.Text = "Valid Assessment."
            lblAssessmentStatus.ForeColor = Color.Green
        End If
    End Sub

    Private Sub frmAdjustments_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnSearchCourse_Click(sender As Object, e As EventArgs) Handles btnSearchCourse.Click
        frmTitle.Text = "Search Course"
        SearchPanel.Visible = True
        AdjustmentCourseList()
        dgCourseList.BringToFront()
        txtSearch.Select()
    End Sub

    Private Sub cbYearLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbYearLevel.SelectedIndexChanged
        Assessment()
    End Sub

    Private Sub cbGender_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbGender.SelectedIndexChanged
        Assessment()
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        CurrentAcount()
    End Sub

    Private Sub btnSaveChangeAssessment_Click(sender As Object, e As EventArgs) Handles btnSaveChangeAssessment.Click
        Dim dr As DialogResult
        dr = MessageBox.Show("Are you sure you want to update this student adjustment?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dr = DialogResult.No Then
        Else
            If StudentAssessmentID = 0 Then
                MsgBox("No assessment selected. Please select an assessment to proceed with setup.", vbCritical)
            Else
                query("UPDATE tbl_student_paid_account_breakdown SET spab_ass_id= " & StudentAssessmentID & " where spab_stud_id = '" & StudentID & "' and spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                query("UPDATE tbl_pre_cashiering SET ps_ass_id = " & StudentAssessmentID & " where student_id = '" & StudentID & "' and period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                query("UPDATE tbl_assessment_institutional_discount SET aid_assessment_id = " & StudentAssessmentID & " where aid_student_id = '" & StudentID & "' and aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                UserActivity("Changed student " & StudentID & " " & StudentName & " account assessment in Academic Year " & cbAcademicYear.Text & ".", "STUDENT ACCOUNT ADJUSTMENT")
                MsgBox("Student account assessment successfully changed.", vbInformation)
            End If
        End If
    End Sub

    Sub CurrentAcount()
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT `Total Assessment` from student_assessment_total where spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and spab_stud_id = '" & StudentID & "'", cn)
            txtAdjustmentTotal.Text = Format(CDec(cm.ExecuteScalar), "n2")
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT spab_add_adjusment from tbl_student_paid_account_breakdown where spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and spab_stud_id = '" & StudentID & "'", cn)
            txtAdditional.Text = Format(CDec(cm.ExecuteScalar), "n2")
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT spab_less_adjusment from tbl_student_paid_account_breakdown where spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and spab_stud_id = '" & StudentID & "'", cn)
            txtLess.Text = Format(CDec(cm.ExecuteScalar), "n2")
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT spab_remarks from tbl_student_paid_account_breakdown where spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and spab_stud_id = '" & StudentID & "'", cn)
            txtRemarks.Text = cm.ExecuteScalar
            cn.Close()
        Catch ex As Exception
            cn.Close()
        End Try
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnSaveAmountAdjust.Click
        Dim dr As DialogResult
        dr = MessageBox.Show("Are you sure you want to update this student adjustment?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dr = DialogResult.No Then
        Else
            If txtStudent.Text = String.Empty Then
            Else
                query("UPDATE tbl_student_paid_account_breakdown SET spab_add_adjusment='" & CDec(txtAdditional.Text) & "', spab_less_adjusment='" & CDec(txtLess.Text) & "', spab_remarks = 'Adjustment: " & txtRemarks.Text & "' where spab_stud_id = '" & StudentID & "' and spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                UserActivity("Adjusted student " & StudentID & " " & StudentName & " account assessment amount in Academic Year " & cbAcademicYear.Text & ".", "STUDENT ACCOUNT ADJUSTMENT")
                CurrentAcount()
                MsgBox("Student account adjustments successfully saved.", vbInformation)
            End If
        End If
    End Sub

    Private Sub txtAdditional_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAdditional.KeyPress, txtLess.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
           AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            'cancel keys
            e.Handled = True
        End If
    End Sub
End Class