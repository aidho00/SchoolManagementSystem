Imports MySql.Data.MySqlClient

Public Class frmAssessment
    Dim SelectedCourseID As Integer = 0
    Private Sub frmAssessment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Sub AssessmentCourseList()
        Try

            dgCourseList.Rows.Clear()
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

            AdjustDataGridViewColumns()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgCourseList.Rows.Clear()
        End Try
    End Sub

    Private Sub AdjustDataGridViewColumns()
        ' Calculate the total width of all columns
        Dim totalColumnWidth As Integer = 0

        For Each column As DataGridViewColumn In dgCourseList.Columns
            totalColumnWidth += column.Width
        Next

        ' Compare total column width with the panel's width
        If dgCourseList.Size.Width < Panel1.Size.Width Then
            ' If total column width is less than the panel's width, set AutoSizeColumnsMode to Fill
            dgCourseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Else
            ' If total column width is greater than or equal to the panel's width, set AutoSizeColumnsMode to AllCells
            dgCourseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        End If
    End Sub

    Sub CourseAssessmentList()
        Try
            dgCourseAssessments.Rows.Clear()
            Dim sql As String
            sql = "select af_id, (af_year_level) as 'Year Level', (af_gender) as 'Gender', Format(af_subtotal_amount,2) as 'Amount', (af_other_fee) as 'Other Fees', CONCAT(ROUND(af_institutional_discount_percent * 100),'%') as 'Institutional Discount' from period JOIN tbl_assessment_fee where period.period_id = tbl_assessment_fee.af_period_id and af_course_id = " & SelectedCourseID & " and af_period_id = " & CInt(cbAcademicYear.SelectedValue) & ""
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgCourseAssessments.Rows.Add(dr.Item("af_id").ToString, dr.Item("Year Level").ToString, dr.Item("Gender").ToString, dr.Item("Amount").ToString, dr.Item("Other Fees").ToString, dr.Item("Institutional Discount").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgCourseAssessments.Rows.Clear()
        End Try
    End Sub

    Sub CourseAssessmentBreakDownList()
        Try
            dgAssessmentBreakdown.Rows.Clear()
            Dim sql As String
            sql = "SELECT (ap_particular_code) as 'Code', (ap_particular_name) as 'Particular', format(afp_amount,2) as 'Amount' from tbl_assessment_fee_particulars JOIN tbl_assessment_particulars where tbl_assessment_fee_particulars.afp_particular_id = tbl_assessment_particulars.ap_id and afp_course_id = " & SelectedCourseID & " and afp_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and afp_year_level = '" & dgCourseAssessments.CurrentRow.Cells(1).Value & "' and afp_gender = '" & dgCourseAssessments.CurrentRow.Cells(2).Value & "'"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgAssessmentBreakdown.Rows.Add(dr.Item("Code").ToString, dr.Item("Particular").ToString, dr.Item("Amount").ToString)
            End While
            dr.Close()
            cn.Close()

            dgAssessmentBreakdown.Rows.Add("", "", "")
            dgAssessmentBreakdown.Rows.Add("", "Other Fees", "")

            Dim sql2 As String
            sql2 = "SELECT (ap_particular_code) as 'Code', (ap_particular_name) as 'Particular', (ofsp_amount) as 'Amount' from tbl_assessment_ofs_particulars JOIN tbl_assessment_particulars where tbl_assessment_ofs_particulars.ofsp_particular_id = tbl_assessment_particulars.ap_id and ofsp_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and ofsp_course_id = " & SelectedCourseID & " and ofsp_year_level = '" & dgCourseAssessments.CurrentRow.Cells(1).Value & "' and ofsp_gender ='" & dgCourseAssessments.CurrentRow.Cells(2).Value & "'"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql2, cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgAssessmentBreakdown.Rows.Add(dr.Item("Code").ToString, dr.Item("Particular").ToString, dr.Item("Amount").ToString)
            End While
            dr.Close()
            cn.Close()

            dgAssessmentBreakdown.Rows.Add("", "", Format(CDec(dgCourseAssessments.CurrentRow.Cells(4).Value), "n2"))
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgAssessmentBreakdown.Rows.Clear()
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        AssessmentCourseList()
    End Sub

    Private Sub dgCourseList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCourseList.CellClick
        SelectedCourseID = CInt(dgCourseList.CurrentRow.Cells(0).Value)
        txtSelectedCourse.Text = dgCourseList.CurrentRow.Cells(2).Value

        fillCombo("Select CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_assessment_fee JOIN tbl_period ON tbl_assessment_fee.af_period_id = tbl_period.period_id WHERE (af_course_id = " & SelectedCourseID & " or period_enrollment_status = 'OPEN') group by period_id order by `period_name` desc, `period_status` asc, `period_semester` desc", cbAcademicYear, "CashieringPeriodList", "PERIOD", "period_id")
        SchedDate()
        CourseAssessmentList()
        CourseAssessmentBreakDownList()
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        SchedDate()
        CourseAssessmentList()
        CourseAssessmentBreakDownList()
    End Sub

    Sub SchedDate()
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT DATE_FORMAT(afb_breakdown_period_date, '%M %d, %Y') as SchedDate from tbl_assessment_fee_breakdown where afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'PRELIM'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                lblPrelim.Text = dr.Item("SchedDate").ToString
            Else
                lblPrelim.Text = "-"
            End If
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
        End Try
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT DATE_FORMAT(afb_breakdown_period_date, '%M %d, %Y') as SchedDate from tbl_assessment_fee_breakdown where afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'MID-TERM'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                lblMidterm.Text = dr.Item("SchedDate").ToString
            Else
                lblMidterm.Text = "-"
            End If
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
        End Try
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT DATE_FORMAT(afb_breakdown_period_date, '%M %d, %Y') as SchedDate from tbl_assessment_fee_breakdown where afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'SEMI-FINAL'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                lblSemi.Text = dr.Item("SchedDate").ToString
            Else
                lblSemi.Text = "-"
            End If
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
        End Try
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT DATE_FORMAT(afb_breakdown_period_date, '%M %d, %Y') as SchedDate from tbl_assessment_fee_breakdown where afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'FINAL'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                lblFinal.Text = dr.Item("SchedDate").ToString
            Else
                lblFinal.Text = "-"
            End If
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
        End Try
    End Sub

    Private Sub btnAddAssessment_Click(sender As Object, e As EventArgs) Handles btnAddAssessment.Click
        If cbAcademicYear.Text = String.Empty Then
        Else
            frmAssessmentSetup.AssessmentCourseID = CInt(dgCourseList.CurrentRow.Cells(0).Value)
            frmAssessmentSetup.AssessmentPeriodID = CInt(cbAcademicYear.SelectedValue)
            frmAssessmentSetup.lblAcademicYear.Text = cbAcademicYear.Text
            frmAssessmentSetup.lblCourse.Text = txtSelectedCourse.Text
            frmAssessmentSetup.btnAdd.Visible = True
            frmAssessmentSetup.btnUpdate.Visible = False
            fillCombo("SELECT category_name from tbl_assessment_fee_category where category_status = 'Active'", frmAssessmentSetup.cbGender, "tbl_assessment_fee_category", "category_name", "category_name")
            frmMain.OpenForm(frmAssessmentSetup, "Course Assessment Setup")
            frmMain.HideAllFormsInPanelExcept(frmAssessmentSetup)
            frmMain.controlsPanel.Visible = False
        End If
    End Sub

    Private Sub dgCourseAssessments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCourseAssessments.CellClick
        Dim colname As String = dgCourseAssessments.Columns(e.ColumnIndex).Name
        If colname = "colEdit" Then
            frmMain.OpenForm(frmAssessmentSetup, "Course Assessment Setup")
            frmMain.HideAllFormsInPanelExcept(frmAssessmentSetup)
            frmMain.controlsPanel.Visible = False
            Try
                With frmAssessmentSetup
                    .btnAdd.Visible = False
                    .btnUpdate.Visible = True

                    fillCombo("SELECT category_name from tbl_assessment_fee_category where category_status = 'Active'", frmAssessmentSetup.cbGender, "tbl_assessment_fee_category", "category_name", "category_name")

                    .AssessmentID = CInt(dgCourseAssessments.CurrentRow.Cells(0).Value)
                    .AssessmentPeriodID = CInt(cbAcademicYear.SelectedValue)
                    .AssessmentCourseID = CInt(dgCourseList.CurrentRow.Cells(0).Value)

                    .lblCourse.Text = txtSelectedCourse.Text
                    .lblAcademicYear.Text = cbAcademicYear.Text
                    .cbGender.Text = dgCourseAssessments.CurrentRow.Cells(2).Value
                    .cbYearLevel.Text = dgCourseAssessments.CurrentRow.Cells(1).Value

                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT af_institutional_discount_percent, af_prelim_percentage, af_midterm_percentage, af_semifinal_percentage, af_final_percentage from tbl_assessment_fee where af_id = '" & dgCourseAssessments.CurrentRow.Cells(0).Value & "'", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        .txtInstitutionalDiscount.Text = dr.Item("af_institutional_discount_percent").ToString
                        .txtPerentagePrelim.Text = dr.Item("af_prelim_percentage").ToString
                        .txtPercentageMidterm.Text = dr.Item("af_midterm_percentage").ToString
                        .txtPercentageSemi.Text = dr.Item("af_semifinal_percentage").ToString
                        .txtPercentageFinal.Text = dr.Item("af_final_percentage").ToString
                    Else
                    End If
                    dr.Close()
                    cn.Close()

                    frmAssessmentSetup.dgTuition.Rows.Clear()
                    Dim sql As String
                    sql = "SELECT (afp_particular_id) as 'ID', (ap_particular_code) as 'Code', (ap_particular_name) as 'Particular', (afp_amount) as 'Amount' from tbl_assessment_fee_particulars JOIN tbl_assessment_particulars where tbl_assessment_fee_particulars.afp_particular_id = tbl_assessment_particulars.ap_id and afp_period_id  = " & CInt(cbAcademicYear.SelectedValue) & " and afp_course_id = " & CInt(dgCourseList.CurrentRow.Cells(0).Value) & " and afp_year_level = '" & dgCourseAssessments.CurrentRow.Cells(1).Value & "' and afp_gender ='" & dgCourseAssessments.CurrentRow.Cells(2).Value & "'"
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand(sql, cn)
                    dr = cm.ExecuteReader
                    While dr.Read
                        frmAssessmentSetup.dgTuition.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Particular").ToString, dr.Item("Amount").ToString)
                    End While
                    dr.Close()
                    cn.Close()

                    frmAssessmentSetup.dgOtherFees.Rows.Clear()
                    Dim sql2 As String
                    sql2 = "SELECT (ofsp_particular_id) as 'ID', (ap_particular_code) as 'Code', (ap_particular_name) as 'Particular', (ofsp_amount) as 'Amount' from tbl_assessment_ofs_particulars JOIN tbl_assessment_particulars where tbl_assessment_ofs_particulars.ofsp_particular_id = tbl_assessment_particulars.ap_id and ofsp_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and ofsp_course_id = " & CInt(dgCourseList.CurrentRow.Cells(0).Value) & " and ofsp_year_level = '" & dgCourseAssessments.CurrentRow.Cells(1).Value & "' and ofsp_gender ='" & dgCourseAssessments.CurrentRow.Cells(2).Value & "'"
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand(sql2, cn)
                    dr = cm.ExecuteReader
                    While dr.Read
                        frmAssessmentSetup.dgOtherFees.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Particular").ToString, dr.Item("Amount").ToString)
                    End While
                    dr.Close()
                    cn.Close()

                End With
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        ElseIf colname = "colRemove" Then

        Else
            CourseAssessmentBreakDownList()
        End If
    End Sub

    Private Sub btnAddExamSched_Click(sender As Object, e As EventArgs) Handles btnAddExamSched.Click
        If cbAcademicYear.Text = String.Empty Then
        Else
            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT afb_breakdown_period_date as SchedDate from tbl_assessment_fee_breakdown where afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'PRELIM'", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    frmExamSched.dtPrelim.Value = dr.Item("SchedDate")
                Else

                End If
                dr.Close()
                cn.Close()
            Catch ex As Exception
                dr.Close()
                cn.Close()
            End Try
            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT afb_breakdown_period_date as SchedDate from tbl_assessment_fee_breakdown where afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'MID-TERM'", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    frmExamSched.dtMidterm.Value = dr.Item("SchedDate").ToString
                Else

                End If
                dr.Close()
                cn.Close()
            Catch ex As Exception
                dr.Close()
                cn.Close()
            End Try
            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT afb_breakdown_period_date as SchedDate from tbl_assessment_fee_breakdown where afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'SEMI-FINAL'", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    frmExamSched.dtSemi.Value = dr.Item("SchedDate").ToString
                Else

                End If
                dr.Close()
                cn.Close()
            Catch ex As Exception
                dr.Close()
                cn.Close()
            End Try
            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT afb_breakdown_period_date as SchedDate from tbl_assessment_fee_breakdown where afb_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'FINAL'", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    frmExamSched.dtFinal.Value = dr.Item("SchedDate").ToString
                Else

                End If
                dr.Close()
                cn.Close()
            Catch ex As Exception
                dr.Close()
                cn.Close()
            End Try
            frmExamSched.ShowDialog()
        End If
    End Sub
End Class