Imports MySql.Data.MySqlClient

Public Class frmSupplyPhysicalInventoryRecords
    Sub PhysicalCountRecords()
        dgPhysicalInventoryList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "SELECT `pino` as 'RecountNo' , DATE_FORMAT(`pidate`, '%m/%d/%Y') as 'RecountDate'FROM `tbl_supply_physicalinventory` GROUP BY `pino` ORDER by `pidate` desc"
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
    End Sub
End Class