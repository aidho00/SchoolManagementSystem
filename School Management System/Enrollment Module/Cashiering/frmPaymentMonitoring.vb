Imports MySql.Data.MySqlClient
Public Class frmPaymentMonitoring
    Public PaymentCashieringID As Integer = 0
    Public PaymentPreCashieringID As Integer = 0
    Private Sub frmPaymentMonitoring_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        cbFilter.SelectedIndex = 0
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadRecords()
    End Sub

    Sub LoadRecords()
        dgPayments.Rows.Clear()
        Dim sql As String
        If cbFilter.Text = "OR Number" Then
            sql = "Select (csh_stud_id) as 'Student ID', StudentFullName as 'Student Name',(csh_ornumber) as 'OR Number', (csh_total_amount) as 'Amount Paid', (csh_amount_received) as 'Amount Received', (csh_amount_change) as 'Amount Change', (accountname) as 'Cashier', DATE_FORMAT( csh_date, '%Y/%m/%d' ) as 'Date', (csh_notes) as 'Notes', (Period) as 'Academic Year', (csh_type) as 'Type', csh_id, csh_period_id, IFNULL(IF(csh_notes LIKE '%Down payment%', (select pre_cash_id from tbl_pre_cashiering where period_id = tbl_cashiering.csh_period_id and student_id = tbl_cashiering.csh_stud_id limit 1),0),0) as pre_cash_id from tbl_cashiering LEFT JOIN students ON tbl_cashiering.csh_stud_id = students.StudentID LEFT JOIN useraccounts ON tbl_cashiering.csh_cashier_id = useraccounts.useraccountID LEFT JOIN period ON tbl_cashiering.csh_period_id = period.period_id WHERE csh_ornumber like '%" & txtSearch.Text & "%' order by csh_id desc limit 50"
        ElseIf cbFilter.Text = "Student ID" Then
            sql = "Select (csh_stud_id) as 'Student ID', StudentFullName as 'Student Name',(csh_ornumber) as 'OR Number', (csh_total_amount) as 'Amount Paid', (csh_amount_received) as 'Amount Received', (csh_amount_change) as 'Amount Change', (accountname) as 'Cashier', DATE_FORMAT( csh_date, '%Y/%m/%d' ) as 'Date', (csh_notes) as 'Notes', (Period) as 'Academic Year', (csh_type) as 'Type', csh_id, csh_period_id, IFNULL(IF(csh_notes LIKE '%Down payment%', (select pre_cash_id from tbl_pre_cashiering where period_id = tbl_cashiering.csh_period_id and student_id = tbl_cashiering.csh_stud_id limit 1),0),0) as pre_cash_id from tbl_cashiering LEFT JOIN students ON tbl_cashiering.csh_stud_id = students.StudentID LEFT JOIN useraccounts ON tbl_cashiering.csh_cashier_id = useraccounts.useraccountID LEFT JOIN period ON tbl_cashiering.csh_period_id = period.period_id WHERE csh_stud_id like '%" & txtSearch.Text & "%' order by csh_id desc limit 50"
        ElseIf cbFilter.Text = "Cashier" Then
            sql = "Select (csh_stud_id) as 'Student ID', StudentFullName as 'Student Name',(csh_ornumber) as 'OR Number', (csh_total_amount) as 'Amount Paid', (csh_amount_received) as 'Amount Received', (csh_amount_change) as 'Amount Change', (accountname) as 'Cashier', DATE_FORMAT( csh_date, '%Y/%m/%d' ) as 'Date', (csh_notes) as 'Notes', (Period) as 'Academic Year', (csh_type) as 'Type', csh_id, csh_period_id, IFNULL(IF(csh_notes LIKE '%Down payment%', (select pre_cash_id from tbl_pre_cashiering where period_id = tbl_cashiering.csh_period_id and student_id = tbl_cashiering.csh_stud_id limit 1),0),0) as pre_cash_id from tbl_cashiering LEFT JOIN students ON tbl_cashiering.csh_stud_id = students.StudentID LEFT JOIN useraccounts ON tbl_cashiering.csh_cashier_id = useraccounts.useraccountID LEFT JOIN period ON tbl_cashiering.csh_period_id = period.period_id WHERE accountname like '%" & txtSearch.Text & "%' order by csh_id desc limit 50"
        ElseIf txtSearch.Text = String.Empty Then
            sql = "Select (csh_stud_id) as 'Student ID', StudentFullName as 'Student Name',(csh_ornumber) as 'OR Number', (csh_total_amount) as 'Amount Paid', (csh_amount_received) as 'Amount Received', (csh_amount_change) as 'Amount Change', (accountname) as 'Cashier', DATE_FORMAT( csh_date, '%Y/%m/%d' ) as 'Date', (csh_notes) as 'Notes', (Period) as 'Academic Year', (csh_type) as 'Type', csh_id, csh_period_id, IFNULL(IF(csh_notes LIKE '%Down payment%', (select pre_cash_id from tbl_pre_cashiering where period_id = tbl_cashiering.csh_period_id and student_id = tbl_cashiering.csh_stud_id limit 1),0),0) as pre_cash_id from tbl_cashiering LEFT JOIN students ON tbl_cashiering.csh_stud_id = students.StudentID LEFT JOIN useraccounts ON tbl_cashiering.csh_cashier_id = useraccounts.useraccountID LEFT JOIN period ON tbl_cashiering.csh_period_id = period.period_id order by csh_id desc limit 50"
        End If
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgPayments.Rows.Add(dr.Item("Academic Year").ToString, dr.Item("Student ID").ToString, dr.Item("Student Name").ToString, dr.Item("OR Number").ToString, dr.Item("Amount Paid").ToString, dr.Item("Amount Received").ToString, dr.Item("Cashier").ToString, dr.Item("Date").ToString, dr.Item("Type").ToString, dr.Item("Notes").ToString, dr.Item("csh_id").ToString, dr.Item("csh_period_id").ToString, dr.Item("pre_cash_id").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged, dtTo.ValueChanged
        If cbFilter.Text = "Date" Then
            dgPayments.Rows.Clear()
            Dim sql As String
            sql = "Select (csh_stud_id) As 'Student ID', CONCAT(s_ln,', ',s_fn,' ',s_mn) as 'Student Name',(csh_ornumber) as 'OR Number', (csh_total_amount) as 'Amount Paid', (csh_amount_received) as 'Amount Received', (csh_amount_change) as 'Amount Change', (accountname) as 'Cashier', DATE_FORMAT(csh_date, '%Y/%m/%d' ) as 'Date', (csh_notes) as 'Notes', (Period) as 'Academic Year', (csh_type) as 'Type', csh_id, csh_period_id, ifNULL((select pre_cash_id from tbl_pre_cashiering where ornumber = tbl_cashiering.csh_ornumber limit 1),0) as pre_cash_id from tbl_cashiering LEFT JOIN tbl_student ON tbl_cashiering.csh_stud_id = tbl_student.s_id_no LEFT JOIN useraccounts ON tbl_cashiering.csh_cashier_id = useraccounts.useraccountID LEFT JOIN period ON tbl_cashiering.csh_period_id = period.period_id WHERE csh_date between '" & dtFrom.Text & "' and '" & dtTo.Text & "' order by csh_id desc"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgPayments.Rows.Add(dr.Item("Academic Year").ToString, dr.Item("Student ID").ToString, dr.Item("Student Name").ToString, dr.Item("OR Number").ToString, dr.Item("Amount Paid").ToString, dr.Item("Amount Received").ToString, dr.Item("Cashier").ToString, dr.Item("Date").ToString, dr.Item("Type").ToString, dr.Item("Notes").ToString, dr.Item("csh_id").ToString, dr.Item("csh_period_id").ToString, dr.Item("pre_cash_id").ToString)
            End While
            dr.Close()
            cn.Close()
        Else
        End If
    End Sub

    Private Sub cbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilter.SelectedIndexChanged
        If cbFilter.Text = "Date" Then
            PanelDate.Visible = True
        Else
            PanelDate.Visible = False
        End If
    End Sub

    Private Sub dgPayments_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPayments.CellContentClick
        Dim colname As String = dgPayments.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            With frmPaymentMonitoringUpdate
                fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_class_schedule t1 JOIN tbl_period t2 ON t1.csperiod_id = t2.period_id group by t1.csperiod_id order by `period_name` desc, `period_status` ASC, `period_semester` desc", .cbAcademicYear, "tbl_period", "PERIOD", "period_id")
                fillCombo("SELECT concat(ua_first_name,' ', ua_middle_name ,' ',ua_last_name) AS AccountName, ua_id FROM  tbl_user_account where ua_account_type = 'Cashier' order by ua_last_name", .cbCashier, "tbl_user_account", "AccountName", "ua_id")

                .PaymentCashieringID = dgPayments.CurrentRow.Cells(10).Value
                .PaymentPreCashieringID = dgPayments.CurrentRow.Cells(12).Value

                If .PaymentPreCashieringID = 0 Then
                    .lblPaymentStatus.Text = "Payment"
                Else
                    .lblPaymentStatus.Text = "Down Payment"
                End If

                .txt1.Text = dgPayments.CurrentRow.Cells(1).Value
                .txt2.Text = dgPayments.CurrentRow.Cells(2).Value
                .txt3.Text = dgPayments.CurrentRow.Cells(3).Value
                .txt4.Text = dgPayments.CurrentRow.Cells(4).Value
                .txt5.Text = dgPayments.CurrentRow.Cells(5).Value
                .txt6.Text = dgPayments.CurrentRow.Cells(0).Value
                .txt7.Text = dgPayments.CurrentRow.Cells(6).Value
                .txt8.Text = dgPayments.CurrentRow.Cells(9).Value

                .StudentID.Text = dgPayments.CurrentRow.Cells(1).Value
                .StudentName.Text = dgPayments.CurrentRow.Cells(2).Value
                .txtOR.Text = dgPayments.CurrentRow.Cells(3).Value
                .txtAmountReceived.Text = dgPayments.CurrentRow.Cells(4).Value
                .txtAmountPaid.Text = dgPayments.CurrentRow.Cells(5).Value
                .cbAcademicYear.Text = dgPayments.CurrentRow.Cells(0).Value
                .cbCashier.Text = dgPayments.CurrentRow.Cells(6).Value
                .txtNotes.Text = dgPayments.CurrentRow.Cells(9).Value
                .ShowDialog()
            End With
        ElseIf colname = "colRemove" Then
            PanelReason.Visible = True
            Panel1.Enabled = False
            dgPayments.Enabled = False

            CenterPanelInsideForm(PanelReason, Me)
        End If
    End Sub

    Private Sub CenterPanelInsideForm(panel As Panel, form As Form)
        Dim x As Integer = (form.ClientSize.Width - panel.Width) \ 2
        Dim y As Integer = (form.ClientSize.Height - panel.Height) \ 2
        panel.Location = New Point(x, y)
    End Sub

    Private Sub btnUpdate2_Click(sender As Object, e As EventArgs) Handles btnUpdate2.Click
        If txtReason.Text = String.Empty Then
            MessageBox.Show("Please input a reason for cancelling payment.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to cancel this Payment?.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If dr = DialogResult.No Then

            Else
                Dim dr2 As DialogResult
                dr2 = MessageBox.Show("Are you REALLY sure you want to cancel this Payment?.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                If dr2 = DialogResult.No Then

                Else
                    cn.Close()
                    cn.Open()

                    PaymentCashieringID = dgPayments.CurrentRow.Cells(10).Value
                    PaymentPreCashieringID = dgPayments.CurrentRow.Cells(12).Value

                    Dim cancelling_transaction As MySqlTransaction = cn.BeginTransaction()
                    Try
                        Using cancelling_Cmd As MySqlCommand = cn.CreateCommand()
                            cancelling_Cmd.Transaction = cancelling_transaction
                            cancelling_Cmd.CommandText = "INSERT INTO tbl_cancelled_cashiering (`ccsh_ornumber`, `ccsh_period_id`, `ccsh_stud_id`, `ccsh_total_amount`, `ccsh_amount_received`, `ccsh_amount_change`, `amount_balance`, `ccsh_type`, `ccsh_date`, `ccsh_notes`, `ccsh_cashier_id`, `ccsh_status`, `ccsh_date_cancelled`, `ccsh_cancelled_by`) SELECT `csh_ornumber`, `csh_period_id`, `csh_stud_id`, `csh_total_amount`, `csh_amount_received`, `csh_amount_change`, `amount_balance`, `csh_type`, `csh_date`, `csh_notes`, `csh_cashier_id`, `csh_status`, CURDATE(), '" & str_userid & "' FROM tbl_cashiering where csh_id = " & PaymentCashieringID & ""
                            cancelling_Cmd.ExecuteNonQuery()
                        End Using

                        If PaymentPreCashieringID = "0" Then
                            Using cancelling2_Cmd As MySqlCommand = cn.CreateCommand()
                                cancelling2_Cmd.Transaction = cancelling_transaction
                                cancelling2_Cmd.CommandText = "DELETE FROM tbl_cashiering WHERE csh_id = " & PaymentCashieringID & ""
                                cancelling2_Cmd.ExecuteNonQuery()
                            End Using
                        Else
                            Using cancelling2_Cmd As MySqlCommand = cn.CreateCommand()
                                cancelling2_Cmd.Transaction = cancelling_transaction
                                cancelling2_Cmd.CommandText = "DELETE FROM tbl_pre_cashiering WHERE pre_cash_id = " & PaymentPreCashieringID & ""
                                cancelling2_Cmd.ExecuteNonQuery()
                            End Using

                            Using cancelling3_Cmd As MySqlCommand = cn.CreateCommand()
                                cancelling3_Cmd.Transaction = cancelling_transaction
                                cancelling3_Cmd.CommandText = "DELETE FROM tbl_cashiering WHERE csh_id = " & PaymentCashieringID & ""
                                cancelling3_Cmd.ExecuteNonQuery()
                            End Using
                        End If

                        cancelling_transaction.Commit()

                        MessageBox.Show("Cancelling payment successfull.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        UserActivity("Cancelled payment of student " & dgPayments.CurrentRow.Cells(1).Value & " payment with OR no. " & dgPayments.CurrentRow.Cells(3).Value & ". With a reason of '" & txtReason.Text & "'.", "PAYMENT UPDATE")
                        LoadRecords()
                        Panel1.Enabled = True
                        dgPayments.Enabled = True
                        PanelReason.Visible = False
                    Catch ex As Exception
                        cn.Close()
                        cancelling_transaction.Rollback()
                        MessageBox.Show("Cancelling payment failed. Process rolled back.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Panel1.Enabled = True
                        dgPayments.Enabled = True
                        PanelReason.Visible = False
                    End Try

                    'Try
                    '    PaymentCashieringID = dgPayments.CurrentRow.Cells(10).Value
                    '    PaymentPreCashieringID = dgPayments.CurrentRow.Cells(12).Value

                    '    If PaymentPreCashieringID = 0 Then
                    '        query("Delete from tbl_cashiering where csh_id = " & PaymentCashieringID & "")
                    '        MessageBox.Show("Student OR successfully updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        LoadRecords()
                    '    Else
                    '        query("Delete from tbl_pre_cashiering where pre_cash_id = " & PaymentPreCashieringID & "")
                    '        query("Delete from tbl_cashiering where csh_id = " & PaymentCashieringID & "")
                    '        MessageBox.Show("Student OR successfully updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        LoadRecords()
                    '    End If
                    '    Me.Dispose()
                    'Catch ex As Exception
                    '    MsgBox(ex.Message, vbCritical)
                    'End Try
                End If
            End If
        End If
    End Sub

    Private Sub closeReasonPanel_Click(sender As Object, e As EventArgs) Handles closeReasonPanel.Click
        Panel1.Enabled = True
        dgPayments.Enabled = True
        PanelReason.Visible = False
    End Sub
End Class