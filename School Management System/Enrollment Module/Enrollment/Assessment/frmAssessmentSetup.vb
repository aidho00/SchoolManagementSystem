Imports MySql.Data.MySqlClient

Public Class frmAssessmentSetup
    Public Shared AssessmentID As Integer = 0
    Public Shared AssessmentPeriodID As Integer = 0
    Public Shared AssessmentCourseID As Integer = 0
    Private Sub frmAssessmentSetup_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub txtInstitutionalDiscount_TextChanged(sender As Object, e As EventArgs) Handles txtInstitutionalDiscount.TextChanged
        Try
            If txtInstitutionalDiscount.Text = String.Empty Then
                txtInstitutionalDiscount.Text = "0.00"
            Else
            End If

            Dim y As Decimal = CDec(amount_TuitionFee.Text)
            Dim prcnt As Decimal = CDec(txtInstitutionalDiscount.Text)
            amount_Discount.Text = Format(CDec(y * prcnt), "n2")
            lblDiscount.Text = "  - Institutional Tuition Fee Discount (" & Math.Round(CDec(txtInstitutionalDiscount.Text) * 100) & "%)"
        Catch ex As Exception
            txtInstitutionalDiscount.Text = "0.00"
            lblDiscount.Text = "  - Institutional Tuition Fee Discount (%)"
        End Try
    End Sub

    Private Sub txtInstitutionalDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInstitutionalDiscount.KeyPress, txtPerentagePrelim.KeyPress, txtPercentageMidterm.KeyPress, txtPercentageSemi.KeyPress, txtPercentageFinal.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
          AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            'cancel keys
            e.Handled = True
        End If
    End Sub

    Sub ParticularsList()
        dgParticulars.Rows.Clear()
        Dim sql As String
        sql = "select (ap_id) as 'ID', (ap_particular_code) as 'Code', (ap_particular_name) as 'Particular' from tbl_assessment_particulars WHERE ap_particular_name LIKE '%" & txtSearch.Text & "%' order by ap_particular_name asc"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgParticulars.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Particular").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub btnTFparticularADD_Click(sender As Object, e As EventArgs) Handles btnTFparticularADD.Click
        SearchPanel.Visible = True
        frmTitle.Text = "Search Tuition Fee Particular"
        ParticularsList()
        dgParticulars.BringToFront()
    End Sub

    Private Sub btnOFparticularADD_Click(sender As Object, e As EventArgs) Handles btnOFparticularADD.Click
        SearchPanel.Visible = True
        frmTitle.Text = "Search Other Fees Particular"
        ParticularsList()
        dgParticulars.BringToFront()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ParticularsList()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If dgParticulars.RowCount = 0 Then
        Else
            frmParticularAmountAdd.lblParticular.Text = dgParticulars.CurrentRow.Cells(1).Value & " - " & dgParticulars.CurrentRow.Cells(2).Value
            frmParticularAmountAdd.txtAmount.Text = "0.00"
            frmParticularAmountAdd.ShowDialog()
        End If
    End Sub

    Private Sub dgTuition_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgTuition.RowsAdded
        amount_TuitionFee.Text = Format(CDec(GetColumnSum(dgTuition, 3)), "n2")
    End Sub

    Private Sub dgTuition_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgTuition.RowsRemoved
        amount_TuitionFee.Text = Format(CDec(GetColumnSum(dgTuition, 3)), "n2")
    End Sub

    Private Sub dgOtherFees_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgOtherFees.RowsAdded
        amount_OtherFees.Text = Format(CDec(GetColumnSum(dgOtherFees, 3)), "n2")
    End Sub

    Private Sub dgOtherFees_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgOtherFees.RowsRemoved
        amount_OtherFees.Text = Format(CDec(GetColumnSum(dgOtherFees, 3)), "n2")
    End Sub

    Sub calculateAmount()
        Try
            Dim y As Decimal = CDec(amount_TuitionFee.Text)
            Dim prcnt As Decimal = CDec(txtInstitutionalDiscount.Text)
            Dim dscnt As Decimal = Format(CDec(y * prcnt), "n2")
            amount_Discount.Text = Format(CDec(y * prcnt), "n2")

            Dim tf As Decimal = CDec(amount_TuitionFee.Text)
            Dim ofs As Decimal = CDec(amount_OtherFees.Text)
            amount_Total.Text = Format(CDec((tf - dscnt) + ofs), "n2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub amount_TuitionFee_TextChanged(sender As Object, e As EventArgs) Handles amount_TuitionFee.TextChanged, amount_OtherFees.TextChanged, amount_Discount.TextChanged
        calculateAmount()
    End Sub

    Private Sub lblCourse_TextChanged(sender As Object, e As EventArgs) Handles lblCourse.TextChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub dgTuition_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgTuition.CellContentClick
        Dim colname As String = dgTuition.Columns(e.ColumnIndex).Name
        If colname = "colRemove1" Then
            dgTuition.Rows.Remove(dgTuition.CurrentRow)
        End If
    End Sub
    Private Sub dgOtherFees_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgOtherFees.CellContentClick
        Dim colname As String = dgOtherFees.Columns(e.ColumnIndex).Name
        If colname = "colRemove2" Then
            dgOtherFees.Rows.Remove(dgOtherFees.CurrentRow)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
        frmMain.OpenForm(frmAssessment, "Course Assessment")
        frmMain.HideAllFormsInPanelExcept(frmAssessment)
        frmMain.controlsPanel.Visible = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cbGender.Text = "" Then
            MsgBox("Please select an assessment category.", vbCritical)
            Return
        End If

        cn.Close()
        cn.Open()
        'Try

        Dim prcnttotal As Decimal = 1.0
        If dgTuition.RowCount = 0 Then
            MsgBox("No particulars found to create assessment. Please add particulars.", vbCritical)
        ElseIf CDec(txtPerentagePrelim.Text) >= 1 AndAlso CDec(txtPercentageMidterm.Text) >= 1 AndAlso CDec(txtPercentageSemi.Text) >= 1 AndAlso CDec(txtPercentageFinal.Text) >= 1 Then
            MsgBox("Value cannot be greater than 1.", vbCritical)
        ElseIf prcnttotal > 1 Then
            MsgBox("Total value is greater than 100 percent.", vbCritical)
        ElseIf prcnttotal < 1 Then
            MsgBox("Total value is less than 100 percent.", vbCritical)
        ElseIf CDec(txtInstitutionalDiscount.Text) > 1 Then
            MsgBox("Institutional Discount Percentage cannot be greater than 1(100%).", vbCritical)
        Else
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to create this assessment for '" & lblCourse.Text & " ' - ' " & lblAcademicYear.Text & "' with a payment breakdown of PRELIM: " & Math.Round(CDec(txtPerentagePrelim.Text) * 100) & "%, MIDTERM: " & Math.Round(CDec(txtPercentageMidterm.Text) * 100) & "%, SEMI-FINAL: " & Math.Round(CDec(txtPercentageSemi.Text) * 100) & "%, FINAL: " & Math.Round(CDec(txtPercentageFinal.Text) * 100) & "% and INSTITUTIONAL DISCOUNT of " & Math.Round(CDec(txtInstitutionalDiscount.Text) * 100) & "%?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dr = DialogResult.No Then
            Else
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select * from tbl_assessment_fee where af_period_id = " & AssessmentPeriodID & " and af_course_id = " & AssessmentCourseID & " and af_year_level = '" & cbYearLevel.Text & "' and af_gender = '" & cbGender.Text & "'", cn)
                Dim sdr As MySqlDataReader = cm.ExecuteReader()
                If (sdr.Read() = True) Then
                    MsgBox("Assessment for Academic year " & lblAcademicYear.Text & " with Course: " & lblCourse.Text & ", Year Level:" & cbYearLevel.Text & " and Category: " & cbGender.Text & " has already created.", vbCritical)
                    sdr.Dispose()
                    cn.Close()
                Else
                    sdr.Dispose()
                    cn.Close()

                    query("INSERT INTO tbl_assessment_fee (af_period_id, af_course_id, af_year_level, af_total_amount, af_date_created, af_subtotal_amount, af_gender, af_prelim_percentage, af_midterm_percentage, af_semifinal_percentage, af_final_percentage, af_other_fee, af_institutional_discount_percent) values (" & AssessmentPeriodID & ", " & AssessmentCourseID & ", '" & cbYearLevel.Text & "', " & CDec(amount_TuitionFee.Text) + CDec(amount_OtherFees.Text) & ", CURDATE(), " & CDec(amount_TuitionFee.Text) & ", '" & cbGender.Text & "', " & CDec(txtPerentagePrelim.Text) & ", " & CDec(txtPercentageSemi.Text) & ", " & CDec(txtPercentageSemi.Text) & ", " & CDec(txtPercentageFinal.Text) & ", " & CDec(amount_OtherFees.Text) & ", " & CDec(txtInstitutionalDiscount.Text) & ")")

                    For Each row As DataGridViewRow In dgTuition.Rows
                        query("INSERT INTO tbl_assessment_fee_particulars (afp_particular_id, afp_period_id, afp_course_id, afp_year_level, afp_amount, afp_gender) values ('" & row.Cells(0).Value & "', " & AssessmentPeriodID & ", " & AssessmentCourseID & ", '" & cbYearLevel.Text & "', " & CDec(row.Cells(3).Value) & ", '" & cbGender.Text & "')")
                    Next

                    For Each row As DataGridViewRow In dgOtherFees.Rows
                        query("INSERT INTO tbl_assessment_ofs_particulars (`ofsp_particular_id`, `ofsp_period_id`, `ofsp_course_id`, `ofsp_year_level`, `ofsp_amount`, `ofsp_gender`) values ('" & row.Cells(0).Value & "', " & AssessmentPeriodID & ", " & AssessmentCourseID & ", '" & cbYearLevel.Text & "', " & CDec(row.Cells(3).Value) & ", '" & cbGender.Text & "')")
                    Next

                    UserActivity("Added a course assessment on course " & lblCourse.Text & " for category " & cbGender.Text & " " & cbYearLevel.Text & " in Academic Year " & lblAcademicYear.Text & ".", "ASSESSMENT SETUP")

                    MsgBox("Assessment successfully created.", vbInformation)
                    frmAssessment.CourseAssessmentList()

                    Me.Close()
                    frmMain.OpenForm(frmAssessment, "Course Assessment")
                    frmMain.HideAllFormsInPanelExcept(frmAssessment)
                    frmMain.controlsPanel.Visible = False

                End If
            End If
        End If
        'Catch ex As Exception
        'End Try
        cn.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If cbGender.Text = "" Then
            MsgBox("Please select an assessment category.", vbCritical)
            Return
        End If

        cn.Close()
        cn.Open()
        'Try

        Dim prcnttotal As Decimal = 1.0
        If dgTuition.RowCount = 0 Then
            MsgBox("No particulars found to create assessment. Please add particulars.", vbCritical)
        ElseIf CDec(txtPerentagePrelim.Text) >= 1 AndAlso CDec(txtPercentageMidterm.Text) >= 1 AndAlso CDec(txtPercentageSemi.Text) >= 1 AndAlso CDec(txtPercentageFinal.Text) >= 1 Then
            MsgBox("Value cannot be greater than 1.", vbCritical)
        ElseIf prcnttotal > 1 Then
            MsgBox("Total value is greater than 100 percent.", vbCritical)
        ElseIf prcnttotal < 1 Then
            MsgBox("Total value is less than 100 percent.", vbCritical)
        ElseIf CDec(txtInstitutionalDiscount.Text) > 1 Then
            MsgBox("Institutional Discount Percentage cannot be greater than 1(100%).", vbCritical)
        Else
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to update this assessment for '" & lblCourse.Text & " ' - ' " & lblAcademicYear.Text & "' with a payment breakdown of PRELIM: " & CDec(txtPerentagePrelim.Text) * 100 & "%, MIDTERM: " & CDec(txtPercentageMidterm.Text) * 100 & "%, SEMI-FINAL: " & CDec(txtPercentageSemi.Text) * 100 & "%, FINAL: " & CDec(txtPercentageFinal.Text) * 100 & "% and INSTITUTIONAL DISCOUNT of " & CDec(txtInstitutionalDiscount.Text) * 100 & "%?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dr = DialogResult.No Then
            Else
                query("delete from tbl_assessment_fee_particulars where afp_period_id = " & AssessmentPeriodID & " and afp_course_id = " & AssessmentCourseID & " and afp_year_level = '" & cbYearLevel.Text & "' and afp_gender = '" & cbGender.Text & "'")
                query("delete from tbl_assessment_ofs_particulars where ofsp_period_id = " & AssessmentPeriodID & " and ofsp_course_id = " & AssessmentCourseID & " and ofsp_year_level = '" & cbYearLevel.Text & "' and ofsp_gender = '" & cbGender.Text & "'")

                query("UPDATE tbl_assessment_fee SET af_total_amount = " & CDec(amount_TuitionFee.Text) + CDec(amount_OtherFees.Text) & ", af_other_fee = " & CDec(amount_OtherFees.Text) & ", af_subtotal_amount = " & CDec(amount_TuitionFee.Text) & ", af_prelim_percentage = " & CDec(txtPercentageMidterm.Text) & ", af_midterm_percentage = " & CDec(txtPercentageMidterm.Text) & ", af_semifinal_percentage = " & CDec(txtPercentageSemi.Text) & ", af_final_percentage = " & CDec(txtPercentageFinal.Text) & ", af_institutional_discount_percent = " & CDec(txtInstitutionalDiscount.Text) & " where af_id = " & AssessmentID & "")

                For Each row As DataGridViewRow In dgTuition.Rows
                    query("INSERT INTO tbl_assessment_fee_particulars (afp_particular_id, afp_period_id, afp_course_id, afp_year_level, afp_amount, afp_gender) values ('" & row.Cells(0).Value & "', " & AssessmentPeriodID & ", " & AssessmentCourseID & ", '" & cbYearLevel.Text & "', " & CDec(row.Cells(3).Value) & ", '" & cbGender.Text & "')")
                Next

                For Each row As DataGridViewRow In dgOtherFees.Rows
                    query("INSERT INTO tbl_assessment_ofs_particulars (`ofsp_particular_id`, `ofsp_period_id`, `ofsp_course_id`, `ofsp_year_level`, `ofsp_amount`, `ofsp_gender`) values ('" & row.Cells(0).Value & "', " & AssessmentPeriodID & ", " & AssessmentCourseID & ", '" & cbYearLevel.Text & "', " & CDec(row.Cells(3).Value) & ", '" & cbGender.Text & "')")
                Next

                UserActivity("Updated a course assessment on course " & lblCourse.Text & " for  gender " & cbGender.Text & " " & cbYearLevel.Text & " in Academic Year " & lblAcademicYear.Text & ".", "ASSESSMENT SETUP")

                MsgBox("Assessment successfully updated.", vbInformation)
                frmAssessment.CourseAssessmentList()

                Me.Close()
                frmMain.OpenForm(frmAssessment, "Course Assessment")
                frmMain.HideAllFormsInPanelExcept(frmAssessment)
                frmMain.controlsPanel.Visible = False

            End If
        End If
        'Catch ex As Exception
        'End Try
        cn.Close()
    End Sub

    Private Sub btnAddAssessment_Click(sender As Object, e As EventArgs) Handles btnAddAssessment.Click
        frmAssessmentCategory.ShowDialog()
    End Sub
End Class