Imports MySql.Data.MySqlClient

Public Class frmSupplyPurchaseRequest

    Function GetTransno() As String
        Dim yearid As String = YearToday
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT prno FROM tbl_supply_purchaserequest WHERE prno like 'PR" & yearid & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(prno) as ID from tbl_supply_purchaserequest", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            lastCode = lastCode.Remove(0, 6)
            GetTransno = "EN" & CInt(yearid & lastCode) + 1
        Else
            dr.Close()
            GetTransno = "EN" & yearid & "00001"
        End If
        cn.Close()

        Return GetTransno
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub

    Private Sub btnSearchItems_Click(sender As Object, e As EventArgs) Handles btnSearchItems.Click
        SearchPanel.Visible = True
        SupplyItemList()
    End Sub

    Sub SupplyItemList()
        dgSupplyItemList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "Select (BarcodeID) as 'Barcode', Description, (CategoryName) as 'Category', Sizes, (item_price) as 'Price', tbl_supply_category.catid as CATID, tbl_supply_sizes.sizeid as SIZEID, tbl_supply_item.item_open_stock as OpenStock, tbl_supply_item.item_reorder_point as ReOrderPoint, tbl_supply_category.categorytype as SupplyType from tbl_supply_item JOIN tbl_supply_category ON tbl_supply_item.CategoryID = tbl_supply_category.catid JOIN tbl_supply_sizes ON tbl_supply_item.sizesid = tbl_supply_sizes.sizeid where (BarcodeID LIKE '%" & txtSearch.Text & "%' or CategoryName LIKE '%" & txtSearch.Text & "%' or Description LIKE '%" & txtSearch.Text & "%' or Sizes LIKE '%" & txtSearch.Text & "%')"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgSupplyItemList.Rows.Add(i, dr.Item("Barcode").ToString, dr.Item("Description").ToString, dr.Item("Category").ToString, dr.Item("Sizes").ToString, dr.Item("Price").ToString, dr.Item("CATID").ToString, dr.Item("SIZEID").ToString, dr.Item("OpenStock").ToString, dr.Item("ReOrderPoint").ToString, dr.Item("SupplyType").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        SupplyItemList()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        frmSupplyPurchaseQty.PurchasingStatus = "Purchase Request"
        frmSupplyPurchaseQty.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            SearchPanel.Visible = False
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnCancelSearch_Click(sender As Object, e As EventArgs) Handles btnCancelSearch.Click
        SearchPanel.Visible = False
    End Sub
End Class