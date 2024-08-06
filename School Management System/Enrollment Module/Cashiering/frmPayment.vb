Public Class frmPayment

    Public Shared ChequeNo As String = ""
    Public Shared ChequeBankName As Integer = ""
    Public Shared ChequeBankBranch As Integer = ""

    Public Shared StudentID As String = ""
    Public Shared CourseID As String = ""
    Public Shared YearLevel As String = ""
    Public Shared Gender As String = ""

    Public Shared StudentBalance As Decimal = 0
    Public Shared StudentAssessmentID As Integer = 0



    Private Sub frmPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
        ResetControls(Me)
    End Sub

    Private Sub frmPayment_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ResetControls(Me)
    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        SearchPanel.Visible = True
        LibraryCashieringStudentList()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LibraryCashieringStudentList()
    End Sub

    Private Sub cbView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbView.SelectedIndexChanged
        ViewDG()
    End Sub

    Private Sub ViewDG()
        If cbView.Text = "Current Account" Then
            dgCurrentAccount.Visible = True
            dgAcadAccounts.Visible = False
            dgPaymentHistory.Visible = False
            soaPanel.Visible = False
        ElseIf cbView.Text = "Academic Year Accounts" Then
            dgCurrentAccount.Visible = False
            dgAcadAccounts.Visible = True
            dgPaymentHistory.Visible = False
            soaPanel.Visible = False
        ElseIf cbView.Text = "Payment History" Then
            dgCurrentAccount.Visible = False
            dgAcadAccounts.Visible = False
            dgPaymentHistory.Visible = True
            soaPanel.Visible = False
        End If
    End Sub

    Private Sub viewSOA_Click(sender As Object, e As EventArgs) Handles viewSOA.Click
        dgCurrentAccount.Visible = False
        dgAcadAccounts.Visible = False
        dgPaymentHistory.Visible = False
        soaPanel.Visible = True
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
        If CHECK_PRECASHIERING("SELECT * FROM tbl_pre_cashiering WHERE student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and period_id = " & CInt(cbAcademicYear.SelectedValue) & "") Then Return
        StudentBalance = StudentTotalBalance("SELECT ifNULL(SUM(`Total Balance`),0) as 'OldAccount' FROM `student_assessment_total` WHERE `spab_stud_id` = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and `spab_period_id` NOT IN(" & CInt(cbAcademicYear.SelectedValue) & ")")
        If CHECK_BALANCE("SELECT period_balance_check FROM tbl_period where period_id  = " & CInt(cbAcademicYear.SelectedValue) & " and period_balance_check = 'ON'") Then Return

        txtStudent.Text = dgStudentList.CurrentRow.Cells(1).Value & " - " & dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
        txtCourse.Text = dgStudentList.CurrentRow.Cells(8).Value
        txtGenderYearLevel.Text = dgStudentList.CurrentRow.Cells(6).Value & " - " & dgStudentList.CurrentRow.Cells(7).Value

        StudentID = dgStudentList.CurrentRow.Cells(1).Value
        CourseID = dgStudentList.CurrentRow.Cells(9).Value
        YearLevel = dgStudentList.CurrentRow.Cells(7).Value
        Gender = dgStudentList.CurrentRow.Cells(6).Value

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
                    If txtAmountPaid.Text = "" Then
                        MsgBox("Please input amount paid.", MessageBoxIcon.Error)
                        Return
                        txtAmountPaid.Select()
                    ElseIf txtAmountReceived.Text = "" Then
                        MsgBox("Please input amount received.", MessageBoxIcon.Error)
                        Return
                        txtAmountReceived.Select()
                    ElseIf CDec(txtAmountPaid.Text) = 0 Then
                        MsgBox("Please input amount paid.", MessageBoxIcon.Error)
                        Return
                        txtAmountPaid.Select()
                    ElseIf CDec(txtAmountReceived.Text) = 0 Then
                        MsgBox("Please input amount received.", MessageBoxIcon.Error)
                        Return
                        txtAmountReceived.Select()
                    ElseIf txtOR.Text = String.Empty Then
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

            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to save this transaction?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dr = DialogResult.No Then
            Else
                If StudentID = String.Empty Then
                    MsgBox("Please select student.", MessageBoxIcon.Error)
                    Return
                ElseIf txtAmountReceived.Text = "" Then
                    MsgBox("Please input amount received.", MessageBoxIcon.Error)
                    Return
                    txtAmountReceived.Select()
                ElseIf CDec(txtAmountPaid.Text) = 0 Then
                    MsgBox("Please input amount paid.", MessageBoxIcon.Error)
                    Return
                    txtAmountPaid.Select()
                ElseIf CDec(txtAmountReceived.Text) = 0 Then
                    MsgBox("Please input amount received.", MessageBoxIcon.Error)
                    Return
                    txtAmountReceived.Select()
                ElseIf txtOR.Text = String.Empty Then
                    MsgBox("Please input OR Number.", MessageBoxIcon.Error)
                    Return
                    txtOR.Select()
                ElseIf txtAmountChange.Text.StartsWith("-") Then
                    MsgBox("Amount paid is greater than the amount received. Transaction invalid.", MessageBoxIcon.Error)
                    Return
                End If
                PerformPreCashieringTransaction()
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearAll()
    End Sub

    Public Sub ClearAll()
        ResetControls(Me)

        txtAmountPaid.Text = "0.00"
        txtAmountPaid.Text = "0.00"
        txtAmountChange.Text = "0.00"
        txtAcadBalance.Text = "0.00"
        txtTotalBalance.Text = "0.00"

        ChequeNo = ""
        ChequeBankName = ""
        ChequeBankBranch = ""
        StudentID = ""
        CourseID = ""
        YearLevel = ""
        Gender = ""

        StudentAssessmentID = 0
        StudentBalance = 0
    End Sub
End Class