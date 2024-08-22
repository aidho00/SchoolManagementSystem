Imports MySql.Data.MySqlClient

Public Class frmSupplyGRRecords
    Sub GoodsReceiptList()
        Try

            dgGRList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "Select grno, pono, grtotal, gr.status, DATE_FORMAT(grdate, '%m/%d/%Y') as grdate, AccountName, grremarks from tbl_supply_goodsreceipts gr JOIN useraccounts ua ON gr.gruser_id = ua.useraccountID where grno LIKE '%" & frmMain.txtSearch.Text & "%'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgGRList.Rows.Add(i, dr.Item("grno").ToString, dr.Item("pono").ToString, dr.Item("grtotal").ToString, dr.Item("grdate").ToString, dr.Item("status").ToString, dr.Item("AccountName").ToString, dr.Item("grremarks").ToString)
        End While
        dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgGRList.Rows.Clear()

        End Try
    End Sub

    Private Sub dgGRList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgGRList.CellContentClick
        Dim colname As String = dgGRList.Columns(e.ColumnIndex).Name
        If colname = "colView" Then

        End If
    End Sub
End Class