Public Class frmAssessmentCategory
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(txtDesc) = True Then Return
        If CHECK_EXISTING("SELECT category_name FROM tbl_assessment_fee_category WHERE category_name = '" & txtDesc.Text & "'") = True Then Return
        query("INSERT INTO tbl_assessment_fee_category (category_name) VALUES ('" & txtDesc.Text & "')")
        UserActivity("Added an assessment category " & txtDesc.Text.Trim & "", "ASSESSMENT CATEGORY ENTRY")
        frmWait.seconds = 1
        frmWait.ShowDialog()
        MsgBox("New assessment category has been successfully added.", vbInformation, "")
        fillCombo("SELECT category_name from tbl_assessment_fee_category ", frmAssessmentSetup.cbGender, "tbl_assessment_fee_category", "category_name", "category_name")
        Me.Close()
    End Sub
End Class