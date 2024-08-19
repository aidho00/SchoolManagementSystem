Imports MySql.Data.MySqlClient

Public Class frmSupplyPORecords
    Sub PurchaseRequestList()

        dgPOList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "Select prno, prtotal, status, DATE_FORMAT(prdate, '%m/%d/%Y') as prdate, AccountName, prremarks from tbl_supply_purchaserequest pr JOIN useraccounts ua ON pr.pruser_id = ua.useraccountID where prno LIKE '%" & frmMain.txtSearch.Text & "%'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgPOList.Rows.Add(i, dr.Item("prno").ToString, dr.Item("prtotal").ToString, dr.Item("prdate").ToString, dr.Item("status").ToString, dr.Item("AccountName").ToString, dr.Item("prremarks").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub
End Class