Imports MySql.Data.MySqlClient

Public Class frmSupplyPurchaseOrder
    Private Sub btnSearchPR_Click(sender As Object, e As EventArgs) Handles btnSearchPR.Click
        SearchPanel.Visible = True
        PurchaseRequestList()
    End Sub

    Sub PurchaseRequestList()
        dgPRList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "Select prno, prtotal, status, DATE_FORMAT(prdate, '%m/%d/%Y') as prdate, AccountName, prremarks from tbl_supply_purchaserequest pr JOIN useraccounts ua ON pr.pruser_id = ua.useraccountID where prno LIKE '%" & frmMain.txtSearch.Text & "%'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgPRList.Rows.Add(i, dr.Item("prno").ToString, dr.Item("prtotal").ToString, dr.Item("prdate").ToString, dr.Item("status").ToString, dr.Item("AccountName").ToString, dr.Item("prremarks").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Sub PurchaseRequestItems()
        dgPOitemList.Rows.Clear()
        Dim sql As String
        sql = "SELECT t1.`itemid`,t2.description, '' as cat,'' as size, t1.`itemqty`,t1.itemprice,t1.itemtotal FROM `tbl_supply_purchaserequest_items` t1 JOIN tbl_supply_item t2 ON t1.itemid = t2.barcodeid WHERE `prno` = '" & dgPRList.CurrentRow.Cells(1).Value & "'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgPOitemList.Rows.Add(dr.Item("itemid").ToString, dr.Item("description").ToString, dr.Item("cat").ToString, dr.Item("size").ToString, dr.Item("itemqty").ToString, dr.Item("itemprice").ToString, dr.Item("itemtotal").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        lblPRno.Text = dgPRList.CurrentRow.Cells(1).Value
        PurchaseRequestItems()
        SearchPanel.Visible = False
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        PurchaseRequestList()
    End Sub
End Class