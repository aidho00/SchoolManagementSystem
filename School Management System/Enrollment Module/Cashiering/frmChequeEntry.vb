Public Class frmChequeEntry
    Private Sub frmChequeEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel? All data entry will be resetted/emptied.", vbYesNo + vbQuestion) = vbYes Then
            frmCashiering.ChequeNo = ""
            frmCashiering.ChequeBankName = ""
            frmCashiering.ChequeBankBranch = ""
            Me.Close()
        Else
        End If
    End Sub

    Private Sub frmChequeEntry_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        ResetControls(Me)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(txtChequeNo) = True Then Return
        If IS_EMPTY(txtBankName) = True Then Return
        If IS_EMPTY(txtBankBranch) = True Then Return

        frmCashiering.ChequeNo = txtChequeNo.Text
        frmCashiering.ChequeBankName = txtBankName.Text
        frmCashiering.ChequeBankBranch = txtBankBranch.Text
    End Sub
End Class