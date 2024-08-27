Imports MySql.Data.MySqlClient

Public Class frmSupplyPORecords
    Sub PurchaseOrderList()
        Try

            dgPOList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
            sql = "Select pono, prno, pototal, po.status, DATE_FORMAT(podate, '%Y/%m/%d') as podate, AccountName, poremarks from cfcissmsdb_supply.tbl_supply_purchaseorder po JOIN useraccounts ua ON po.pouser_id = ua.useraccountID where prno LIKE '%" & frmMain.txtSearch.Text & "%' order by status desc"
            cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgPOList.Rows.Add(i, dr.Item("pono").ToString, dr.Item("prno").ToString, dr.Item("pototal").ToString, dr.Item("podate").ToString, dr.Item("status").ToString, dr.Item("AccountName").ToString, dr.Item("poremarks").ToString)
        End While
        dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgPOList.Rows.Clear()

        End Try
    End Sub

    Private Sub dgPOList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPOList.CellContentClick
        Dim colname As String = dgPOList.Columns(e.ColumnIndex).Name
        If colname = "colView" Then
            Try
                frmReportViewer.Show()
                load_datagrid("SELECT t1.`itemid`,t2.description, '','', t1.`itemqty`,t1.itemprice,t1.itemtotal FROM cfcissmsdb_supply.`tbl_supply_purchaseorder_items` t1 JOIN cfcissmsdb_supply.tbl_supply_item t2 ON t1.itemid = t2.barcodeid WHERE `pono` = '" & dgPOList.CurrentRow.Cells(1).Value & "'", dg_report)
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
                Dim iDate As String = dgPOList.CurrentRow.Cells(3).Value
                Dim oDate As DateTime = Convert.ToDateTime(iDate)
                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New PurchaseOrder
                rptdoc.SetDataSource(dt)
                rptdoc.SetParameterValue("orderno", dgPOList.CurrentRow.Cells(1).Value)
                rptdoc.SetParameterValue("requestdate", Format(Convert.ToDateTime(dgPOList.CurrentRow.Cells(4).Value), "MMMM d, yyyy"))
                rptdoc.SetParameterValue("requestno", dgPOList.CurrentRow.Cells(2).Value)
                rptdoc.SetParameterValue("requestremarks", dgPOList.CurrentRow.Cells(7).Value)
                rptdoc.SetParameterValue("preparedby", dgPOList.CurrentRow.Cells(6).Value)
                frmReportViewer.ReportViewer.ReportSource = rptdoc
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class