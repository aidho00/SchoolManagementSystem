Imports MySql.Data.MySqlClient

Public Class frmDiscount

    Dim studentAssessmentID As Integer = 0

    Private Sub CenterForm()
        Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height
        Dim formWidth As Integer = Me.Width
        Dim formHeight As Integer = Me.Height

        Dim newX As Integer = (screenWidth - formWidth) / 2
        Dim newY As Integer = (screenHeight - formHeight) / 2

        Me.Location = New Point(newX, newY)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            SearchPanel.Visible = False
            Me.Size = New Size(672, 525)
            CenterForm()
        Else
            Me.Close()
            CenterForm()
        End If
    End Sub

    Sub DiscountStudentList()
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
        SearchPanel.Visible = True
        DiscountStudentList()
        Me.Size = New Size(998, 600)
        CenterForm()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        SearchPanel.Visible = False
        Me.Size = New Size(672, 525)
        CenterForm()
        txtStudentID.Text = dgStudentList.CurrentRow.Cells(1).Value.ToString
        txtStudentName.Text = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
        txtYearLevelCourse.Text = dgStudentList.CurrentRow.Cells(7).Value & " - " & dgStudentList.CurrentRow.Cells(8).Value & " - " & dgStudentList.CurrentRow.Cells(10).Value
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT spab_ass_id FROM tbl_student_paid_account_breakdown  WHERE spab_stud_id  = '" & txtStudentID.Text & "' and spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            studentAssessmentID = cm.ExecuteScalar
            cn.Close()
        Catch ex As Exception
            cn.Close()
            studentAssessmentID = 0
        End Try
    End Sub

    Sub OtherDiscounts()
        Try


            dgDiscounts.Rows.Clear()
            Dim sql As String
            sql = "SELECT t1.sd_id as 'ID', t1.sd_amount as 'Amount', t1.sd_remarks as 'Remark' FROM tbl_student_discounts t1 JOIN period t2 ON t1.sd_period_id = t2.period_id WHERE t1.sd_student_id = '" & txtStudentID.Text & "' and t1.sd_period_id = " & CInt(cbAcademicYear.SelectedValue) & ""
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgDiscounts.Rows.Add(dr.Item("ID").ToString, dr.Item("Remark").ToString, dr.Item("Amount").ToString)
            End While
            dr.Close()
            cn.Close()

            Dim ATotal As Decimal = 0

            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT SUM(t1.sd_amount) as 'Amount' FROM tbl_student_discounts t1 JOIN period t2 ON t1.sd_period_id = t2.period_id WHERE t1.sd_student_id = '" & txtStudentID.Text & "' and t1.sd_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                ATotal = cm.ExecuteScalar
                cn.Close()
                lblTotal.Text = Format(ATotal, "n2")
            Catch ex As Exception
                cn.Close()
                lblTotal.Text = "0.00"
            End Try
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgDiscounts.Rows.Clear()
            lblTotal.Text = "0.00"
        End Try
    End Sub

    Sub InstitutionalDiscountPercentage()
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT CASE WHEN COUNT(aid_percentage) > 0 THEN aid_percentage ELSE '0.0000000000000000' END AS 'Percent' from tbl_assessment_institutional_discount where aid_student_id = '" & txtStudentID.Text & "' and aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            txtPercentage.Text = cm.ExecuteScalar
            cn.Close()
        Catch ex As Exception
            cn.Close()
        End Try
    End Sub

    Private Sub txtStudentID_TextChanged(sender As Object, e As EventArgs) Handles txtStudentID.TextChanged
        fillCombo("Select CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_student_paid_account_breakdown JOIN tbl_period ON tbl_student_paid_account_breakdown.spab_period_id = tbl_period.period_id WHERE spab_stud_id = '" & txtStudentID.Text & "' order by `period_name` desc, `period_status` asc, `period_semester` desc", cbAcademicYear, "CashieringPeriodList", "PERIOD", "period_id")
        InstitutionalDiscountPercentage()
        OtherDiscounts()
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        OtherDiscounts()
    End Sub

    Private Sub btnSaveChangeAssessment_Click(sender As Object, e As EventArgs) Handles btnSaveInstitutionalDiscount.Click
        If MsgBox("Are you sure you want to save this grade to the curriculum subject?", vbYesNo + vbQuestion) = vbYes Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT * FROM tbl_assessment_institutional_discount where aid_student_id = '" & txtStudentID.Text & "' and aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            Dim sdr2 As MySqlDataReader = cm.ExecuteReader()
            If (sdr2.Read() = True) Then
                query("update tbl_assessment_institutional_discount set aid_percentage = " & CDec(txtPercentage.Text) & " where aid_student_id = '" & txtStudentID.Text & "' and aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                UserActivity("Update student " & txtStudentID.Text & " " & txtStudentName.Text & " institutional discount in Academic Year " & cbAcademicYear.Text & ".", "STUDENT DISCOUNT")
                MsgBox("Successfully added student institutional discount.", vbInformation)
            Else
                query("Insert into tbl_assessment_institutional_discount (`aid_student_id`, `aid_period_id`, `aid_percentage`, `aid_assessment_id`) values ('" & txtStudentID.Text & "', " & CInt(cbAcademicYear.SelectedValue) & ", " & CDec(txtPercentage.Text) & ", " & studentAssessmentID & ")")
                UserActivity("Added student " & txtStudentID.Text & " " & txtStudentName.Text & " institutional discount in Academic Year " & cbAcademicYear.Text & ".", "STUDENT DISCOUNT")
                MsgBox("Successfully update student institutional discount.", vbInformation)
            End If
            sdr2.Close()
            cn.Close()
        Else

        End If
    End Sub

    Private Sub btnAddDiscount_Click(sender As Object, e As EventArgs) Handles btnAddDiscount.Click
        fillCombo("Select sd_remarks FROM tbl_student_discounts group by sd_remarks", frmDiscountAdd.cbDesc, "Discounts", "sd_remarks", "sd_remarks")
        frmDiscountAdd.ShowDialog()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        DiscountStudentList()
    End Sub

    Private Sub txtPercentage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPercentage.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
           AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            'cancel keys
            e.Handled = True
        End If
    End Sub

    Private Sub frmDiscount_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub dgDiscounts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgDiscounts.CellContentClick
        Dim colname As String = dgDiscounts.Columns(e.ColumnIndex).Name
        If colname = "colRemove" Then
            If MsgBox("Are you sure you want to remove this student discount?", vbYesNo + vbQuestion) = vbYes Then
                query("DELETE FROM tbl_student_discounts WHERE sd_id = " & CInt(dgDiscounts.CurrentRow.Cells(0).Value) & "")
                UserActivity("Cancelled " & txtStudentID.Text & " " & txtStudentName.Text & "  " & dgDiscounts.CurrentRow.Cells(2).Value & " - " & dgDiscounts.CurrentRow.Cells(1).Value & " discount in Academic Year " & cbAcademicYear.Text & ".", "STUDENT DISCOUNT")
                OtherDiscounts()
                MsgBox("Successfully cancelled student discount.", vbInformation)
            End If
        End If
    End Sub

    Private Sub btnDiscountBulk_Click(sender As Object, e As EventArgs) Handles btnDiscountBulk.Click
        ResetControls(frmSetupInstitutionalDiscount)
        frmSetupInstitutionalDiscount.txtPercentage.Text = "0.0000000000000000"
        frmSetupInstitutionalDiscount.lblCourse.Text = ""
        frmSetupInstitutionalDiscount.ShowDialog()
    End Sub
End Class