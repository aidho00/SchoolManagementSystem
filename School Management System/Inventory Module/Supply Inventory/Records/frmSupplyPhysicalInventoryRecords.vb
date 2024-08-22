Imports MySql.Data.MySqlClient

Public Class frmSupplyPhysicalInventoryRecords
    Sub PhysicalCountRecords()
        Try

            dgPhysicalInventoryList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "SELECT `pino` as 'RecountNo' , DATE_FORMAT(`pidate`, '%m/%d/%Y') as 'RecountDate'FROM `tbl_supply_physicalinventory` where pino LIKE '%" & txtSearch.Text & "%' GROUP BY `pino` ORDER by `pidate` desc"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgPhysicalInventoryList.Rows.Add(i, dr.Item("RecountNo").ToString, dr.Item("RecountDate").ToString)
        End While
        dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgPhysicalInventoryList.Rows.Clear()

        End Try
    End Sub

    Private Sub dgPhysicalInventoryList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPhysicalInventoryList.CellContentClick
        Dim colname As String = dgPhysicalInventoryList.Columns(e.ColumnIndex).Name
        If colname = "colView" Then

        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        With frmSupplyPhysicalInventory
            ResetControls(frmSupplyPhysicalInventory)
            .PhysicalCountSupplyItemStockLevel()
            .ShowDialog()
        End With
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        PhysicalCountRecords()
    End Sub

    Private Sub frmSupplyPhysicalInventoryRecords_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub
End Class