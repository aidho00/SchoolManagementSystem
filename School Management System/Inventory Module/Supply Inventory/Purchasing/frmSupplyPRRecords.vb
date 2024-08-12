Imports MySql.Data.MySqlClient

Public Class frmSupplyPRRecords
    Sub PurchaseRequestList()
        dgPRList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "Select prno, prtotal, status, DATE_FORMAT(prdate, '%m/%d/%Y') as prdate, status from tbl_supply_purchaserequest where prno = '" & frmMain.txtSearch.Text & "' "
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgPRList.Rows.Add(i, dr.Item("prno").ToString, dr.Item("prtotal").ToString, dr.Item("prdate").ToString, dr.Item("status").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub
End Class