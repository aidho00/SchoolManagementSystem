Imports MySql.Data.MySqlClient

Public Class frmSupplyItems

    Sub SupplyItemList()
        Try

            dgSupplyItemList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "Select (BarcodeID) as 'Barcode', Description, (CategoryName) as 'Category', brandname as 'Brand', Sizes, (item_price) as 'Price', cfcissmsdb_supply.tbl_supply_category.catid as CATID, cfcissmsdb_supply.tbl_supply_brand.brandid as 'BRANDID', cfcissmsdb_supply.tbl_supply_sizes.sizeid as SIZEID, cfcissmsdb_supply.tbl_supply_item.item_open_stock as OpenStock, cfcissmsdb_supply.tbl_supply_item.item_reorder_point as ReOrderPoint, cfcissmsdb_supply.tbl_supply_category.categorytype as SupplyType, item_status from cfcissmsdb_supply.tbl_supply_item JOIN cfcissmsdb_supply.tbl_supply_category ON cfcissmsdb_supply.tbl_supply_item.CategoryID = cfcissmsdb_supply.tbl_supply_category.catid JOIN cfcissmsdb_supply.tbl_supply_sizes ON cfcissmsdb_supply.tbl_supply_item.sizesid = cfcissmsdb_supply.tbl_supply_sizes.sizeid JOIN cfcissmsdb_supply.tbl_supply_brand ON cfcissmsdb_supply.tbl_supply_item.brandid = cfcissmsdb_supply.tbl_supply_brand.brandid where cfcissmsdb_supply.tbl_supply_category.categorytype = '" & frmMain.SelectionTitle.Text & "' and (BarcodeID LIKE '%" & frmMain.txtSearch.Text & "%' or CategoryName LIKE '%" & frmMain.txtSearch.Text & "%' or Description LIKE '%" & frmMain.txtSearch.Text & "%' or Sizes LIKE '%" & frmMain.txtSearch.Text & "%' or brandname LIKE '%" & frmMain.txtSearch.Text & "%')"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgSupplyItemList.Rows.Add(i, dr.Item("Barcode").ToString, dr.Item("Description").ToString, dr.Item("Category").ToString, dr.Item("Brand").ToString, dr.Item("Sizes").ToString, dr.Item("Price").ToString, dr.Item("CATID").ToString, dr.Item("BRANDID").ToString, dr.Item("SIZEID").ToString, dr.Item("OpenStock").ToString, dr.Item("ReOrderPoint").ToString, dr.Item("SupplyType").ToString, dr.Item("item_status").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgSupplyItemList.Rows.Clear()

        End Try
    End Sub

    Private Sub dgSupplyItemList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSupplyItemList.CellContentClick
        Dim colname As String = dgSupplyItemList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            ResetControls(frmSupplyItemAdd)
            frmSupplyItemAdd.CategoryID = 0
            frmSupplyItemAdd.SizeID = 0

            frmSupplyItemAdd.cbSupplyType.Text = dgSupplyItemList.Rows(e.RowIndex).Cells(12).Value.ToString
            frmSupplyItemAdd.cbSupplyStatus.Text = dgSupplyItemList.Rows(e.RowIndex).Cells(13).Value.ToString

            frmSupplyItemAdd.cbSupplyCategory.Text = dgSupplyItemList.Rows(e.RowIndex).Cells(3).Value.ToString
            frmSupplyItemAdd.cbSupplyBrand.Text = dgSupplyItemList.Rows(e.RowIndex).Cells(4).Value.ToString
            frmSupplyItemAdd.cbSupplySize.Text = dgSupplyItemList.Rows(e.RowIndex).Cells(5).Value.ToString
            frmSupplyItemAdd.txtPrice.Text = dgSupplyItemList.Rows(e.RowIndex).Cells(6).Value.ToString
            frmSupplyItemAdd.CategoryID = CInt(dgSupplyItemList.Rows(e.RowIndex).Cells(7).Value)
            frmSupplyItemAdd.BrandID = CInt(dgSupplyItemList.Rows(e.RowIndex).Cells(8).Value)
            frmSupplyItemAdd.SizeID = CInt(dgSupplyItemList.Rows(e.RowIndex).Cells(9).Value)
            frmSupplyItemAdd.txtOpeningStock.Text = dgSupplyItemList.Rows(e.RowIndex).Cells(10).Value.ToString
            frmSupplyItemAdd.txtReOrderPoint.Text = dgSupplyItemList.Rows(e.RowIndex).Cells(11).Value.ToString

            frmSupplyItemAdd.barcodeID.Text = dgSupplyItemList.Rows(e.RowIndex).Cells(1).Value.ToString
            frmSupplyItemAdd.txtSupplyDesc.Text = dgSupplyItemList.Rows(e.RowIndex).Cells(2).Value.ToString

            frmSupplyItemAdd.cbSupplyType.Enabled = False
            frmSupplyItemAdd.txtOpeningStock.Enabled = False
            frmSupplyItemAdd.btnUpdate.Visible = True
            frmSupplyItemAdd.btnSave.Visible = False

            frmSupplyItemAdd.btnSearchBrand.Visible = False
            frmSupplyItemAdd.btnSearchCategory.Visible = False
            frmSupplyItemAdd.btnSearchSize.Visible = False
            frmSupplyItemAdd.ShowDialog()
        End If
    End Sub
End Class