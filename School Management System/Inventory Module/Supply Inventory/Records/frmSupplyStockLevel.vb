Imports MySql.Data.MySqlClient

Public Class frmSupplyStockLevel
    Sub SupplyItemStockLevel()
        Try

            dgSupplyItemList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "Select (BarcodeID) as 'Item ID', Description, (CategoryName) as 'Category', Sizes, (tbl_supply_inventory.Spare) as 'Stock' from tbl_supply_item JOIN tbl_supply_category ON tbl_supply_item.CategoryID = tbl_supply_category.catid JOIN tbl_supply_sizes ON tbl_supply_item.sizesid = tbl_supply_sizes.sizeid JOIN tbl_supply_inventory ON tbl_supply_item.barcodeid = tbl_supply_inventory.itembarcode JOIN tbl_supply_brand ON tbl_supply_item.brandid = tbl_supply_brand.brandid where tbl_supply_category.categorytype = '" & frmMain.SelectionTitle.Text & "' and tbl_supply_item.item_status = 'Available' and (BarcodeID LIKE '%" & frmMain.txtSearch.Text & "%' or CategoryName LIKE '%" & frmMain.txtSearch.Text & "%' or Description LIKE '%" & frmMain.txtSearch.Text & "%' or Sizes LIKE '%" & frmMain.txtSearch.Text & "%'  or brandname LIKE '%" & frmMain.txtSearch.Text & "%')"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgSupplyItemList.Rows.Add(i, dr.Item("Item ID").ToString, dr.Item("Description").ToString, dr.Item("Category").ToString, dr.Item("Sizes").ToString, dr.Item("Stock").ToString)
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgSupplyItemList.Rows.Clear()

        End Try
    End Sub
End Class