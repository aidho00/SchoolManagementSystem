Public Class frmParticularAmountAdd
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtAmount.Text = String.Empty Then
            MsgBox("Amount cannot be empty.", vbCritical)
        Else
            If frmAssessmentSetup.frmTitle.Text = "Search Tuition Fee Particular" Then
                frmAssessmentSetup.dgTuition.Rows.Add(frmAssessmentSetup.dgParticulars.CurrentRow.Cells(0).Value, frmAssessmentSetup.dgParticulars.CurrentRow.Cells(1).Value, frmAssessmentSetup.dgParticulars.CurrentRow.Cells(2).Value, Format(CDec(txtAmount.Text), "#,##0.00"))
            ElseIf frmAssessmentSetup.frmTitle.Text = "Search Other Fees Particular" Then
                frmAssessmentSetup.dgOtherFees.Rows.Add(frmAssessmentSetup.dgParticulars.CurrentRow.Cells(0).Value, frmAssessmentSetup.dgParticulars.CurrentRow.Cells(1).Value, frmAssessmentSetup.dgParticulars.CurrentRow.Cells(2).Value, Format(CDec(txtAmount.Text), "#,##0.00"))
            End If
            lblParticular.Text = "-"
            txtAmount.Text = "0.00"
            Me.Dispose()
        End If
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
           AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            'cancel keys
            e.Handled = True
        End If
    End Sub
End Class