Imports MySql.Data.MySqlClient

Public Class frmPaymentCollectedPerCashier

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub LoadPayments(cashierDate As String)
        Try
            dgPayments.Rows.Clear()
            Dim sql As String
            sql = "Select t2.AccountName, ifnull(sum(t1.csh_total_amount),0) as AmountCollected from tbl_cashiering t1 JOIN useraccounts t2 ON t1.csh_cashier_id = t2.useraccountID where t1.csh_date = '" & cashierDate & "' and t1.csh_ornumber REGEXP '^[0-9]' group by t1.csh_cashier_id"

            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgPayments.Rows.Add(dr.Item("AccountName").ToString, Format(CDec(dr.Item("AmountCollected").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgPayments.Rows.Clear()
        End Try
    End Sub

    Private Sub frmPaymentCollectedPerCashier_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub
End Class