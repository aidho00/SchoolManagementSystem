Imports MySql.Data.MySqlClient

Public Class frmSupplyItemLedger


    Sub SupplyItemList()
        Try

            dgSupplyItemList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "SELECT (BarcodeID) AS 'Barcode', Description, (CategoryName) AS 'Category', Sizes, (item_price) AS 'Price', tbl_supply_category.catid AS CATID, tbl_supply_sizes.sizeid AS SIZEID, tbl_supply_item.item_open_stock AS OpenStock, tbl_supply_item.item_reorder_point AS ReOrderPoint, tbl_supply_category.categorytype AS SupplyType FROM tbl_supply_item JOIN tbl_supply_category ON tbl_supply_item.CategoryID = tbl_supply_category.catid JOIN tbl_supply_sizes ON tbl_supply_item.sizesid = tbl_supply_sizes.sizeid JOIN tbl_supply_brand ON tbl_supply_item.brandid = tbl_supply_brand.brandid WHERE (BarcodeID LIKE '%" & txtSearch.Text & "%' OR CategoryName LIKE '%" & txtSearch.Text & "%' OR Description LIKE '%" & txtSearch.Text & "%' OR Sizes LIKE '%" & txtSearch.Text & "%' OR brandname LIKE '%" & txtSearch.Text & "%')"
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

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgSupplyItemList.Rows.Clear()

        End Try
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
        lblDescription.Text = dgSupplyItemList.CurrentRow.Cells(2).Value
        SupplyLedger()
        SearchPanel.Visible = False
    End Sub

    Sub SupplyLedger()
        Try
            dgLedger.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "Select sl_itembarcode, sl_transaction_type, sl_reference_no, sl_remark, DATE_FORMAT(sl_date, '%m/%d/%Y') as sl_date, sl_stockin_added, sl_stockout_deducted, sl_running_balance from tbl_supply_ledger where sl_itembarcode = '" & lblBarcode.Text & "'"
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

        Catch ex As exception
            dr.Close()
            cn.Close()
            dgLedger.Rows.Clear()
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        SupplyItemList()
    End Sub
End Class