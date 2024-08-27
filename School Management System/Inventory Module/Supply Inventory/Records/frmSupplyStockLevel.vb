Imports MySql.Data.MySqlClient

Public Class frmSupplyStockLevel
    Sub SupplyItemStockLevel()
        Try

            dgSupplyItemList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "Select (BarcodeID) as 'Item ID', Description, (CategoryName) as 'Category', Sizes, (tbl_supply_inventory.Spare) as 'Stock', tbl_supply_item.item_reorder_point as RPoint from cfcissmsdb_supply.tbl_supply_item JOIN cfcissmsdb_supply.tbl_supply_category ON cfcissmsdb_supply.tbl_supply_item.CategoryID = cfcissmsdb_supply.tbl_supply_category.catid JOIN cfcissmsdb_supply.tbl_supply_sizes ON cfcissmsdb_supply.tbl_supply_item.sizesid = cfcissmsdb_supply.tbl_supply_sizes.sizeid JOIN cfcissmsdb_supply.tbl_supply_inventory ON cfcissmsdb_supply.tbl_supply_item.barcodeid = cfcissmsdb_supply.tbl_supply_inventory.itembarcode JOIN cfcissmsdb_supply.tbl_supply_brand ON cfcissmsdb_supply.tbl_supply_item.brandid = cfcissmsdb_supply.tbl_supply_brand.brandid where cfcissmsdb_supply.tbl_supply_category.categorytype = '" & frmMain.SelectionTitle.Text & "' and cfcissmsdb_supply.tbl_supply_item.item_status = 'Available' and (BarcodeID LIKE '%" & frmMain.txtSearch.Text & "%' or CategoryName LIKE '%" & frmMain.txtSearch.Text & "%' or Description LIKE '%" & frmMain.txtSearch.Text & "%' or Sizes LIKE '%" & frmMain.txtSearch.Text & "%'  or brandname LIKE '%" & frmMain.txtSearch.Text & "%')"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgSupplyItemList.Rows.Add(i, dr.Item("Item ID").ToString, dr.Item("Description").ToString, dr.Item("Category").ToString, dr.Item("Sizes").ToString, dr.Item("Stock").ToString, dr.Item("RPoint").ToString)
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgSupplyItemList.Rows.Clear()

        End Try

        Try
            For Each row As DataGridViewRow In dgSupplyItemList.Rows
                Dim cellValue As Integer = CInt(row.Cells(0).Value)
                If CInt(row.Cells(5).Value) <= CInt(row.Cells(6).Value) Then
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192)
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 192, 192)
                    row.DefaultCellStyle.SelectionForeColor = Color.Black
                Else
                    row.DefaultCellStyle.BackColor = Color.White
                    row.DefaultCellStyle.SelectionBackColor = Color.LightGray
                    row.DefaultCellStyle.SelectionForeColor = Color.Black
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class