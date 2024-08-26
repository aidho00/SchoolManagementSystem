Imports MySql.Data.MySqlClient

Public Class frmSupplyGRRecords
    Sub GoodsReceiptList()
        Try

            dgGRList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "Select grno, pono, grtotal, gr.status, DATE_FORMAT(grdate, '%Y/%m/%d') as grdate, AccountName, grremarks from tbl_supply_goodsreceipts gr JOIN useraccounts ua ON gr.gruser_id = ua.useraccountID where grno LIKE '%" & frmMain.txtSearch.Text & "%'"
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
            Try
                frmReportViewer.Show()
                load_datagrid("SELECT t1.`itemid`,t2.description, '','', t1.`itemqty`,t1.itemprice,t1.itemtotal FROM `tbl_supply_goodsreceipts_items` t1 JOIN tbl_supply_item t2 ON t1.itemid = t2.barcodeid WHERE `grno` = '" & dgGRList.CurrentRow.Cells(1).Value & "'", dg_report)
                Dim dt As New DataTable
                With dt
                    .Columns.Add("barcodeid")
                    .Columns.Add("description")
                    .Columns.Add("categoryname")
                    .Columns.Add("sizes")
                    .Columns.Add("dprice")
                    .Columns.Add("dqty")
                    .Columns.Add("ditemprice")
                End With
                For Each dr As DataGridViewRow In dg_report.Rows
                    dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(5).Value, dr.Cells(4).Value, dr.Cells(6).Value)
                Next
                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New GoodsReceipt
                rptdoc.SetDataSource(dt)
                rptdoc.SetParameterValue("greceiptno", dgGRList.CurrentRow.Cells(1).Value)
                rptdoc.SetParameterValue("requestdate", Format(Convert.ToDateTime(dgGRList.CurrentRow.Cells(4).Value), "MMMM d, yyyy"))
                rptdoc.SetParameterValue("orderno", dgGRList.CurrentRow.Cells(2).Value)
                rptdoc.SetParameterValue("requestremarks", dgGRList.CurrentRow.Cells(7).Value)
                rptdoc.SetParameterValue("preparedby", dgGRList.CurrentRow.Cells(6).Value)
                frmReportViewer.ReportViewer.ReportSource = rptdoc
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class