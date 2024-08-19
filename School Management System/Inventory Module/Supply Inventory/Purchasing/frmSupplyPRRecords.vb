Imports MySql.Data.MySqlClient

Public Class frmSupplyPRRecords
    Sub PurchaseRequestList()

        dgPRList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "Select prno, prtotal, status, DATE_FORMAT(prdate, '%m/%d/%Y') as prdate, AccountName, prremarks from tbl_supply_purchaserequest pr JOIN useraccounts ua ON pr.pruser_id = ua.useraccountID where prno LIKE '%" & frmMain.txtSearch.Text & "%'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgPRList.Rows.Add(i, dr.Item("prno").ToString, dr.Item("prtotal").ToString, dr.Item("prdate").ToString, dr.Item("status").ToString, dr.Item("AccountName").ToString, dr.Item("prremarks").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub dgPRList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPRList.CellContentClick
        Dim colname As String = dgPRList.Columns(e.ColumnIndex).Name
        If colname = "colView" Then
            'Try
            frmReportViewer.Show()
                load_datagrid("SELECT t1.`itemid`,t2.description, '','', t1.`itemqty`,t1.itemprice,t1.itemtotal FROM `tbl_supply_purchaserequest_items` t1 JOIN tbl_supply_item t2 ON t1.itemid = t2.barcodeid WHERE `prno` = '" & dgPRList.CurrentRow.Cells(1).Value & "'", dg_report)
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
                    dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value)
                Next
            Dim iDate As String = dgPRList.CurrentRow.Cells(3).Value
            Dim oDate As DateTime = Convert.ToDateTime(iDate)
                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New PurchaseRequest
                rptdoc.SetDataSource(dt)
            rptdoc.SetParameterValue("requestdate", Format(Convert.ToDateTime(dgPRList.CurrentRow.Cells(3).Value), "MMMM d, yyyy"))
            rptdoc.SetParameterValue("requestno", dgPRList.CurrentRow.Cells(1).Value)
                rptdoc.SetParameterValue("requestremarks", dgPRList.CurrentRow.Cells(6).Value)

                rptdoc.SetParameterValue("preparedby", dgPRList.CurrentRow.Cells(5).Value)
                frmReportViewer.ReportViewer.ReportSource = rptdoc
            'Catch ex As Exception

            'End Try
        End If
    End Sub
End Class