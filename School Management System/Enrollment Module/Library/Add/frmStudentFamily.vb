Public Class frmStudentFamily
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(txtFName) = True Then Return
        If IS_EMPTY(txtMName) = True Then Return
        If IS_EMPTY(txtLName) = True Then Return
        With frmStudentInfo
            If familyName.Text = "Father's Name" Then
                .father_fname = txtFName.Text.Trim
                .father_mname = txtMName.Text.Trim
                .father_lname = txtLName.Text.Trim

                .txtFatherName.Text = .father_fname & " " & .father_mname & " " & .father_lname
            ElseIf familyName.Text = "Mother's Maiden Name" Then
                .mother_fname = txtFName.Text.Trim
                .mother_mname = txtMName.Text.Trim
                .mother_lname = txtLName.Text.Trim

                .txtMotherName.Text = .mother_fname & " " & .mother_mname & " " & .mother_lname
            End If
        End With
        Me.Dispose()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
End Class