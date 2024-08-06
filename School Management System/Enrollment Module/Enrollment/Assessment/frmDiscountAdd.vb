Imports MySql.Data.MySqlClient

Public Class frmDiscountAdd

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
           AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            'cancel keys
            e.Handled = True
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        cbDesc.Text = String.Empty
        txtAmount.Text = "0.00"
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtAmount.Text = String.Empty Then
            MsgBox("Amount cannot be empty.", vbCritical)
        Else
            If MsgBox("Are you sure you want to add this discount?", vbYesNo + vbQuestion) = vbYes Then
                query("INSERT INTO `tbl_student_discounts`(`sd_student_id`, `sd_period_id`, `sd_amount`, `sd_remarks`) VALUES ('" & frmDiscount.txtStudentID.Text & "', " & CInt(frmDiscount.cbAcademicYear.SelectedValue) & ", " & CDec(txtAmount.Text) & ", '" & cbDesc.Text & "')")
                UserActivity("Added a discount on student " & frmDiscount.txtStudentID.Text & " " & frmDiscount.txtStudentName.Text & " in Academic Year " & frmDiscount.cbAcademicYear.Text & ".", "STUDENT DISCOUNT")
                MsgBox("Successfully added a student discount.", vbInformation)
                cbDesc.Text = String.Empty
                txtAmount.Text = "0.00"
                frmDiscount.OtherDiscounts()
                Me.Dispose()
            End If
        End If
    End Sub

    Private Sub txtAmount_VisibleChanged(sender As Object, e As EventArgs) Handles txtAmount.VisibleChanged
        cbDesc.Text = String.Empty
    End Sub
End Class