Imports MySql.Data.MySqlClient

Public Class frmSupplyItemLedger


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

    Private Sub btnSearchItem_Click(sender As Object, e As EventArgs) Handles btnSearchItem.Click
        dgSupplyItemList.Rows.Clear()
        lblBarcode.Text = "-"
        lblDescription.Text = "-"
        SearchPanel.Visible = True
        dgSupplyItemList.BringToFront()
        frmTitle.Text = "Search Size"
        SupplyItemList()
    End Sub

    Private Sub btnCancelSearch_Click(sender As Object, e As EventArgs) Handles btnCancelSearch.Click
        SearchPanel.Visible = False
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        lblBarcode.Text = dgSupplyItemList.CurrentRow.Cells(1).Value
        lblDescription.Text = dgSupplyItemList.CurrentRow.Cells(2).Value & " " & dgSupplyItemList.CurrentRow.Cells(3).Value & " " & dgSupplyItemList.CurrentRow.Cells(4).Value
        SupplyLedger()
        SearchPanel.Visible = False
    End Sub

    Sub SupplyLedger()
        dgLedger.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "Select sl_itembarcode, sl_transaction_type, sl_reference_no, sl_remark, sl_date, sl_stockin_added, sl_stockout_deducted, sl_running_balance from tbl_supply_ledger where sl_itembarcode = '" & lblBarcode.Text & "'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgLedger.Rows.Add(i, dr.Item("sl_transaction_type").ToString, dr.Item("sl_transaction_type").ToString, dr.Item("sl_reference_no").ToString, dr.Item("sl_remark").ToString, dr.Item("sl_date").ToString, dr.Item("sl_stockin_added").ToString, dr.Item("sl_stockout_deducted").ToString, dr.Item("sl_running_balance").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub
End Class