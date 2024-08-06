Public Class frmPaymentMonitoringUpdate

    Public PaymentCashieringID As Integer = 0
    Public PaymentPreCashieringID As Integer = 0
    Private Sub frmPaymentMonitoringUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub txt4_TextChanged(sender As Object, e As EventArgs) Handles txt4.TextChanged
        Try
            TextBox5.Text = Format(CDec(txt4.Text) - CDec(txt5.Text), "n2")
        Catch ex As Exception
            TextBox5.Text = "0.00"
        End Try
    End Sub

    Private Sub txt5_TextChanged(sender As Object, e As EventArgs) Handles txt5.TextChanged
        Try
            TextBox5.Text = Format(CDec(txt4.Text) - CDec(txt5.Text), "n2")
        Catch ex As Exception
            TextBox5.Text = "0.00"
        End Try
    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        SearchPanel.Visible = True
        Panel3.Visible = False
        PaymentMonitoringStudentList()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        StudentID.Text = dgStudentList.CurrentRow.Cells(1).Value
        StudentName.Text = dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & "" & dgStudentList.CurrentRow.Cells(4).Value

        SearchPanel.Visible = False
        Panel3.Visible = True
        txtSearch.Text = String.Empty
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        PaymentMonitoringStudentList()
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        Try
            If PaymentPreCashieringID = 0 Then
                txtNotes.Text = "Payment For " & cbAcademicYear.Text & "."
            Else
                txtNotes.Text = "Down Payment For " & cbAcademicYear.Text & "."
            End If
        Catch ex As Exception
            txtNotes.Text = ""
        End Try
    End Sub

    Private Sub btnUpdate2_Click(sender As Object, e As EventArgs) Handles btnUpdate2.Click
        If txtReason.Text = String.Empty Then
            MessageBox.Show("Please input a reason for the update.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to update this Payment?.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dr = DialogResult.No Then

            Else
                Dim a As Double
                Dim b As Double
                a = txtAmountPaid.Text
                b = txtAmountReceived.Text
                If CInt(cbAcademicYear.SelectedValue) = 0 Then
                    MessageBox.Show("Academic Year Invalid.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ElseIf a = 0 Then
                    MessageBox.Show("Please input amount paid.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtAmountPaid.Select()
                ElseIf b = 0 Then
                    MessageBox.Show("Please input amount received.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtAmountReceived.Select()
                ElseIf a > b Then
                    MessageBox.Show("Invalid amount. Amount to pay cannot be greater than amount received.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtAmountReceived.Select()
                Else
                    Try
                        If PaymentPreCashieringID = 0 Then
                            query("Update tbl_cashiering set csh_ornumber = '" & txtOR.Text & "', csh_total_amount = '" & txtAmountPaid.Text & "', csh_amount_received = '" & txtAmountReceived.Text & "', csh_amount_change = " & CDec(txtAmountChange.Text) & ", csh_cashier_id = " & CInt(cbCashier.SelectedValue) & ", csh_period_id = " & CInt(cbAcademicYear.SelectedValue) & ", csh_notes = '" & txtNotes.Text & "', csh_stud_id = '" & StudentID.Text & "' where csh_id = " & PaymentCashieringID & "")
                            MessageBox.Show("Student payment successfully updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            frmPaymentMonitoring.LoadRecords()
                        Else
                            query("Update tbl_pre_cashiering set ornumber = '" & txtOR.Text & "', amount_paid = " & CDec(txtAmountPaid.Text) & ", amount_received = " & CDec(txtAmountReceived.Text) & ", amount_change = " & CDec(txtAmountChange.Text) & ", approved_by_id = " & CInt(cbCashier.SelectedValue) & ", period_id = " & CInt(cbAcademicYear.SelectedValue) & ", student_id = '" & StudentID.Text & "' where pre_cash_id = " & PaymentPreCashieringID & "")
                            query("Update tbl_cashiering set csh_ornumber = '" & txtOR.Text & "', csh_total_amount = " & CDec(txtAmountPaid.Text) & ", csh_amount_received = '" & txtAmountReceived.Text & "', csh_amount_change = '" & txtAmountChange.Text & "', csh_cashier_id = " & CInt(cbCashier.SelectedValue) & ", csh_period_id = " & CInt(cbAcademicYear.SelectedValue) & ", csh_notes = '" & txtNotes.Text & "', csh_stud_id = '" & StudentID.Text & "' where csh_id = " & PaymentCashieringID & "")
                            MessageBox.Show("Student payment successfully updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            frmPaymentMonitoring.LoadRecords()
                        End If
                        UserActivity("Updated payment of student " & txt1.Text & " payment with OR no. " & txt3.Text & ", with an amount paid of " & txt5.Text & " and with an amount received of " & txt4.Text & " in academic year " & txt6.Text & " by cashier " & txt7.Text & " TO " & StudentID.Text & ", " & txtOR.Text & ", " & txtAmountPaid.Text & ", " & txtAmountReceived.Text & ", " & cbAcademicYear.Text & ", " & cbCashier.Text & ". With a reason of '" & txtReason.Text & "'", "PAYMENT UPDATE")
                        PanelReason.Visible = False
                        Me.Dispose()
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        PanelReason.Visible = True
    End Sub

    Private Sub closeReasonPanel_Click(sender As Object, e As EventArgs) Handles closeReasonPanel.Click
        PanelReason.Visible = False
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

    Private Sub txtAmountPaid_TextChanged(sender As Object, e As EventArgs) Handles txtAmountPaid.TextChanged
        calculateAmountChange()
    End Sub

    Private Sub txtAmountReceived_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmountReceived.KeyPress, txtAmountPaid.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
           AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            'cancel keys
            e.Handled = True
        End If
    End Sub
End Class