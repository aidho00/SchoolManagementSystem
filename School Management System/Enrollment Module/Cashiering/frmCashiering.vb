Imports MySql.Data.MySqlClient

Public Class frmCashiering

    Public Shared ChequeNo As String = ""
    Public Shared ChequeBankName As String = ""
    Public Shared ChequeBankBranch As String = ""

    Public Shared StudentID As String = ""
    Public Shared StudentName As String = ""
    Public Shared CourseID As Integer = 0
    Public Shared Course As String = ""
    Public Shared YearLevel As String = ""
    Public Shared Gender As String = ""

    Dim studentGradeLevel As String = ""
    Dim studentGradeLevelCourse As String = ""
    Dim studentGradeLevelCourseName As String = ""
    Dim studentGradeLevelCourseCode As String = ""

    Public Shared StudentBalance As Decimal = 0
    Public Shared StudentAssessmentID As Integer = 0



    Private Sub frmCashiering_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
        ClearAll()
    End Sub

    Private Sub frmCashiering_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ResetControls(Me)
    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        SearchPanel.Visible = True
        LibraryCashieringStudentList()
        txtSearch.Select()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LibraryCashieringStudentList()
    End Sub

    Private Sub cbView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbView.SelectedIndexChanged
        If frmMain.formTitle.Text = "Cashiering" Then
            ViewDG()
        ElseIf frmMain.formTitle.Text = "Pre-Cashiering" Then
            StudentAssessmentID = CInt(cbView.SelectedValue)
            MsgBox("Assessment ID: " & CInt(cbView.SelectedValue) & "")

        End If
    End Sub

    Private Sub ViewDG()
        If cbView.Text = "Current Account" Then
            dgCurrentAccount.Visible = True
            dgAcadAccounts.Visible = False
            dgPaymentHistory.Visible = False
            soaPanel.Visible = False
            CashieringLoadCurrentAccount()
        ElseIf cbView.Text = "Academic Year Accounts" Then
            dgCurrentAccount.Visible = False
            dgAcadAccounts.Visible = True
            dgPaymentHistory.Visible = False
            soaPanel.Visible = False
            CashieringLoadCurrentAccountPerAcademicYear()
        ElseIf cbView.Text = "Payment History" Then
            dgCurrentAccount.Visible = False
            dgAcadAccounts.Visible = False
            dgPaymentHistory.Visible = True
            soaPanel.Visible = False
            CashieringPaymentHistory()
        End If
        Panel8.Visible = True
    End Sub

    Private Sub viewSOA_Click(sender As Object, e As EventArgs) Handles viewSOA.Click
        dgCurrentAccount.Visible = False
        dgAcadAccounts.Visible = False
        dgPaymentHistory.Visible = False
        soaPanel.Visible = True
        Panel8.Visible = False
        GenerateSOA()
    End Sub



    Private Sub btnCloseSOA_Click(sender As Object, e As EventArgs) Handles btnCloseSOA.Click
        ViewDG()
    End Sub

    Private Sub txtTotalBalance_KeyDown(sender As Object, e As KeyEventArgs)
        e.Handled = True
    End Sub

    Private Sub cbPaymentMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPaymentMethod.SelectedIndexChanged
        If cbPaymentMethod.Text = "CHEQUE" Then
            frmChequeEntry.ShowDialog()
            viewChequeEntry.Visible = True
        Else
            viewChequeEntry.Visible = False
        End If
    End Sub

    Private Sub viewChequeEntry_Click(sender As Object, e As EventArgs) Handles viewChequeEntry.Click
        With frmChequeEntry
            .txtChequeNo.Text = frmCashiering.ChequeNo
            .txtBankName.Text = frmCashiering.ChequeBankName
            .txtBankBranch.Text = frmCashiering.ChequeBankBranch
            .ShowDialog()
        End With
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        'If CHECK_PRECASHIERING("SELECT * FROM tbl_pre_cashiering WHERE student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and period_id = " & CInt(cbAcademicYear.SelectedValue) & "") Then Return
        'StudentBalance = StudentTotalBalance("SELECT ifNULL(SUM(`Total Balance`),0) as 'OldAccount' FROM `student_assessment_total` WHERE `spab_stud_id` = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and `spab_period_id` NOT IN(" & CInt(cbAcademicYear.SelectedValue) & ")")
        'If CHECK_BALANCE("SELECT period_balance_check FROM tbl_period where period_id  = " & CInt(cbAcademicYear.SelectedValue) & " and period_balance_check = 'ON'") Then Return

        txtStudent.Text = dgStudentList.CurrentRow.Cells(1).Value & " - " & dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
        txtCourse.Text = dgStudentList.CurrentRow.Cells(8).Value & " - " & dgStudentList.CurrentRow.Cells(10).Value
        txtGenderYearLevel.Text = dgStudentList.CurrentRow.Cells(6).Value & " - " & dgStudentList.CurrentRow.Cells(7).Value

        StudentName = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
        StudentID = dgStudentList.CurrentRow.Cells(1).Value
        CourseID = dgStudentList.CurrentRow.Cells(9).Value
        YearLevel = dgStudentList.CurrentRow.Cells(7).Value
        Gender = dgStudentList.CurrentRow.Cells(6).Value

        If frmMain.formTitle.Text = "Cashiering" Then
            fillCombo("Select CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_student_paid_account_breakdown JOIN tbl_period ON tbl_student_paid_account_breakdown.spab_period_id = tbl_period.period_id WHERE spab_stud_id = '" & StudentID & "' order by `period_name` desc, `period_status` asc, `period_semester` desc", cbAcademicYear, "CashieringPeriodList", "PERIOD", "period_id")
            Try
                txtAcadBalance.Text = Format(StudentTotalBalance("Select IfNULL(`Total Balance`,0) from student_assessment_total where spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and spab_stud_id = '" & StudentID & "'"), "n2")
            Catch ex As Exception
                txtAcadBalance.Text = "0.00"
            End Try
        ElseIf frmMain.formTitle.Text = "Pre-Cashiering" Then
            AssessmentID()
        End If

        Try
            txtTotalBalance.Text = Format(StudentTotalBalance("Select SUM(IfNULL(`Total Balance`,0)) from student_assessment_total where spab_stud_id = '" & StudentID & "'"), "n2")
        Catch ex As Exception
            txtTotalBalance.Text = "0.00"
        End Try

        SearchPanel.Visible = False
        cbView.SelectedIndex = 0

        dgCurrentAccount.Visible = True
        dgAcadAccounts.Visible = True
        dgPaymentHistory.Visible = True
        soaPanel.Visible = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If frmMain.formTitle.Text = "Cashiering" Then

            If StudentID = String.Empty Then
                MsgBox("Please select student.", MessageBoxIcon.Error)
            Else
                Dim dr As DialogResult
                dr = MessageBox.Show("Are you sure you want to save this transaction?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If dr = DialogResult.No Then
                Else
                    If txtOR.Text = String.Empty Then
                        MsgBox("Please input OR Number.", MessageBoxIcon.Error)
                        Return
                        txtOR.Select()
                    ElseIf CDec(txtAmountPaid.Text) > CDec(txtAcadBalance.Text) Then
                        MsgBox("Please input amount less than or equal to amount due.", MessageBoxIcon.Error)
                        Return
                        txtAmountPaid.Select()
                    ElseIf CDec(txtAcadBalance.Text) = 0 Then
                        MsgBox("Student does not have any balance in this academic year.", MessageBoxIcon.Error)
                        Return
                    End If
                    If cbNDP.Checked = True Then
                    Else
                        If CDec(txtAmountPaid.Text) = 0 Then
                            MsgBox("Please input amount paid.", MessageBoxIcon.Error)
                            Return
                            txtAmountPaid.Select()
                        ElseIf CDec(txtAmountReceived.Text) = 0 Then
                            MsgBox("Please input amount received.", MessageBoxIcon.Error)
                            Return
                            txtAmountReceived.Select()
                        End If
                    End If

                    If cbPaymentMethod.Text = "CHEQUE" Then
                        If ChequeNo = String.Empty Then
                            MsgBox("Please input cheque no.", "", MessageBoxIcon.Error)
                            Return
                        ElseIf ChequeBankName = String.Empty Then
                            MsgBox("Please input bank name.", "", MessageBoxIcon.Error)
                            Return
                        ElseIf ChequeBankBranch = String.Empty Then
                            MsgBox("Please input bank branch.", "", MessageBoxIcon.Error)
                            Return
                        Else
                        End If
                    End If
                    PerformCashieringTransaction()
                End If
            End If

        ElseIf frmMain.formTitle.Text = "Pre-Cashiering" Then
            If StudentID = String.Empty Then
                MsgBox("Please select student.", MessageBoxIcon.Error)
            Else
                Dim dr As DialogResult
                dr = MessageBox.Show("Are you sure you want to save this transaction?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If dr = DialogResult.No Then
                Else
                    If txtOR.Text = String.Empty Then
                        MsgBox("Please input OR Number.", MessageBoxIcon.Error)
                        Return
                        txtOR.Select()
                        'ElseIf CDec(txtAmountPaid.Text) > CDec(txtAcadBalance.Text) Then
                        '    MsgBox("Please input amount less than or equal to amount due.", MessageBoxIcon.Error)
                        '    Return
                        '    txtAmountPaid.Select()
                        'ElseIf CDec(txtAcadBalance.Text) = 0 Then
                        '    MsgBox("Student does not have any balance in this academic year.", MessageBoxIcon.Error)
                        '    Return
                    End If

                    If CountRecords("SELECT count(*) FROM tbl_pre_cashiering WHERE student_id = '" & StudentID & "' and period_id = " & CInt(cbAcademicYear.SelectedValue) & "") > 0 Then
                        MsgBox("Student '" & txtStudent.Text & "' with ID Number '" & StudentID & "' is already pre cashiered in academic year " & cbAcademicYear.Text & ".", MessageBoxIcon.Error)
                        Return
                    End If

                    If cbNDP.Checked = True Then
                    Else
                        If CDec(txtAmountPaid.Text) = 0 Then
                            MsgBox("Please input amount paid.", MessageBoxIcon.Error)
                            Return
                            txtAmountPaid.Select()
                        ElseIf CDec(txtAmountReceived.Text) = 0 Then
                            MsgBox("Please input amount received.", MessageBoxIcon.Error)
                            Return
                            txtAmountReceived.Select()
                        End If
                    End If

                    If cbPaymentMethod.Text = "CHEQUE" Then
                        If ChequeNo = String.Empty Then
                            MsgBox("Please input cheque no.", "", MessageBoxIcon.Error)
                            Return
                        ElseIf ChequeBankName = String.Empty Then
                            MsgBox("Please input bank name.", "", MessageBoxIcon.Error)
                            Return
                        ElseIf ChequeBankBranch = String.Empty Then
                            MsgBox("Please input bank branch.", "", MessageBoxIcon.Error)
                            Return
                        Else
                        End If
                    End If
                    PerformPreCashieringTransaction()
                End If
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearAll()
    End Sub

    Sub ClearAll()
        ResetControls(Me)

        txtAmountPaid.Text = "0.00"
        txtAmountReceived.Text = "0.00"
        txtAmountChange.Text = "0.00"
        txtAcadBalance.Text = "0.00"
        txtTotalBalance.Text = "0.00"

        ChequeNo = ""
        ChequeBankName = ""
        ChequeBankBranch = ""
        StudentID = ""
        CourseID = 0
        YearLevel = ""
        Gender = ""

        txtStudent.Text = ""
        txtCourse.Text = ""
        txtGenderYearLevel.Text = ""

        cbAcademicYear.DataSource = Nothing

        dgCurrentAccount.Rows.Clear()
        dgAcadAccounts.Rows.Clear()
        dgPaymentHistory.Rows.Clear()

        StudentAssessmentID = 0
        StudentBalance = 0

        cbNDP.Checked = False

        SearchPanel.Visible = False
        txtSearch.Text = String.Empty
    End Sub

    Private Sub cbNDP_CheckedChanged(sender As Object, e As EventArgs) Handles cbNDP.CheckedChanged
        If cbNDP.Checked = True Then
            txtAmountPaid.Text = "0.00"
            txtAmountReceived.Text = "0.00"
            txtAmountChange.Text = "0.00"

            txtAmountPaid.ReadOnly = True
            txtAmountReceived.ReadOnly = True

            txtOR.Text = "NDP"
        Else
            txtAmountPaid.ReadOnly = False
            txtAmountReceived.ReadOnly = False

            txtOR.Text = ""
        End If
    End Sub

    Private Sub txtTotalBalance_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalBalance.KeyPress, cbAcademicYear.KeyPress
        e.Handled = True
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        Try
            If frmMain.formTitle.Text = "Cashiering" Then
                txtAcadBalance.Text = Format(StudentTotalBalance("Select IfNULL(`Total Balance`,0) from student_assessment_total where spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and spab_stud_id = '" & StudentID & "'"), "n2")
            Else
                AssessmentID()
            End If
        Catch ex As Exception
            cn.Close()
        End Try
    End Sub

    Sub AssessmentID()
        'If frmMain.systemModule.Text = "College Module" Then
        '    If YearLevel.Contains("1st Year") Then
        '        cn.Close()
        '        cn.Open()
        '        cm = New MySqlCommand("SELECT af_id from tbl_assessment_fee where af_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and af_course_id = " & CourseID & " and af_year_level = LEFT('" & YearLevel & "', 8) and af_gender = '" & Gender & "'", cn)
        '        StudentAssessmentID = cm.ExecuteScalar
        '        cn.Close()
        '    Else
        '        cn.Close()
        '        cn.Open()
        '        cm = New MySqlCommand("SELECT af_id from tbl_assessment_fee where af_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and af_course_id = " & CourseID & " and af_year_level = LEFT('" & YearLevel & "', 8) and af_gender = 'Both'", cn)
        '        StudentAssessmentID = cm.ExecuteScalar
        '        cn.Close()
        '    End If
        'Else
        '    cn.Close()
        '    cn.Open()
        '    cm = New MySqlCommand("SELECT af_id from tbl_assessment_fee where af_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and af_course_id = " & CourseID & " and af_year_level = '" & YearLevel & "' and af_gender = 'Both'", cn)
        '    StudentAssessmentID = cm.ExecuteScalar
        '    cn.Close()
        'End If

        fillCombo("SELECT distinct(af_gender) as af_gender, af_id from tbl_assessment_fee where af_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and af_course_id = " & CourseID & " and af_year_level = LEFT('" & YearLevel & "', 8)", cbView, "tbl_assessment_fee", "af_gender", "af_id")

        cbView.SelectedIndex = 0
        MsgBox("Assessment ID: " & CInt(cbView.SelectedValue) & "")
    End Sub


    Private Sub txtAmountReceived_TextChanged(sender As Object, e As EventArgs) Handles txtAmountReceived.TextChanged
        txtAmountPaid.Text = txtAmountReceived.Text
        calculateAmountChange()
    End Sub

    Sub calculateAmountChange()
        Try
            txtAmountChange.Text = Format(CDec(txtAmountReceived.Text) - CDec(txtAmountPaid.Text), "n2")
        Catch ex As Exception
            txtAmountChange.Text = "0.00"
        End Try
    End Sub

    Private Sub txtAmountReceived_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmountReceived.KeyPress, txtAmountPaid.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
           AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            'cancel keys
            e.Handled = True
        End If
    End Sub

    Private Sub txtAmountPaid_TextChanged(sender As Object, e As EventArgs) Handles txtAmountPaid.TextChanged
        calculateAmountChange()
    End Sub
End Class