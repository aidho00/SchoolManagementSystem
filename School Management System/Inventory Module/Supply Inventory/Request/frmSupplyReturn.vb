Imports MySql.Data.MySqlClient
Public Class frmSupplyReturn
    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_returnall.Click
        Dim dr As DialogResult
        dr = MessageBox.Show("When returning items be sure that only the items to be returned are in the list. Do this by deleting the items that are not included using the delete key on the keyboard after the item row is selected." & Environment.NewLine & "Are you sure you want to return items on this request?", "Notice!", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dr = DialogResult.No Then
        Else
            cn.Close()
            cn.Open()
            Dim iDate As String = DateToday
            Dim oDate As DateTime = Convert.ToDateTime(iDate)
            For Each row As DataGridViewRow In dgCart.Rows
                Dim x As Integer
                Dim y As Integer
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT spare from tbl_supply_inventory where itembarcode = '" & dgCart.CurrentRow.Cells(0).Value & "'", cn)
                x = cm.ExecuteScalar
                cn.Close()

                y = row.Cells(4).Value

                Using cmd As New MySqlCommand("update tbl_supply_inventory set spare = spare + @1, deployed = deployed - @1 where itembarcode = @2", cn)
                    cmd.Parameters.AddWithValue("@1", row.Cells(4).Value)
                    cmd.Parameters.AddWithValue("@2", row.Cells(0).Value)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                End Using


                StockLedger(dgCart.CurrentRow.Cells(0).Value, CInt(row.Cells(5).Value), 0, "Supply Request Item Return", "Stock Return", "REQUEST ID: " & frmSupplyRecords.lblReportRequestID.Text)

                'Using cmd As New MySqlCommand("INSERT INTO tbl_supply_ledger (sl_itembarcode, sl_stockin_added, sl_stockout_deducted, sl_running_balance, sl_date, sl_remark) values (@1, @2, @3, @4, @5, @6)", dbconn)
                '    cmd.Parameters.AddWithValue("@1", row.Cells(0).Value)
                '    cmd.Parameters.AddWithValue("@2", row.Cells(4).Value)
                '    cmd.Parameters.AddWithValue("@3", "0")
                '    cmd.Parameters.AddWithValue("@4", x + y)
                '    cmd.Parameters.AddWithValue("@5", oDate)
                '    cmd.Parameters.AddWithValue("@6", "Return")
                '    cmd.ExecuteNonQuery()
                '    cmd.Dispose()
                'End Using
                Using cmd As New MySqlCommand("update tbl_supply_deployed set dstatus = 'RETURNED' where dstudentid = @1", cn)
                    cmd.Parameters.AddWithValue("@1", frmSupplyRecords.cbRequests.Text)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                End Using
                MessageBox.Show("Item(s) returned successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                cn.Close()
            Next
            cn.Close()
        End If
    End Sub

    Private Sub dgCart_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCart.CellContentClick
        Dim colname As String = dgCart.Columns(e.ColumnIndex).Name
        If colname = "colRemove" Then
            With frmSupplyReturnQTY
                .ShowDialog()
            End With
        End If
    End Sub

    Sub loadCart2()
        Dim _total As Double
        dgCart.Rows.Clear()
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("Select barcodeid, description, tbl_supply_deployed.dprice as Price, (dqty) as QTY, qty_requested as RQST, qty_returned as RTRN, tbl_supply_deployed.ditem_price as Total from tbl_supply_deployed, tbl_supply_item, tbl_supply_category where tbl_supply_deployed.dbarcode = tbl_supply_item.barcodeid AND tbl_supply_item.categoryid = tbl_supply_category.catid and tbl_supply_deployed.dstatus = 'APPROVED' and tbl_supply_deployed.dtransno = '" & frmSupplyRecords.cbRequests.Text & "'", cn)
        dr = cm.ExecuteReader()
        While dr.Read
            _total += CDbl(dr.Item("Total").ToString)
            dgCart.Rows.Add(dr.Item("barcodeid").ToString, dr.Item("description").ToString, dr.Item("Price").ToString, dr.Item("QTY").ToString, dr.Item("RQST").ToString, dr.Item("RTRN").ToString, dr.Item("Total").ToString)
        End While
        dr.Close()
        cn.Close()
        lblTotal.Text = Format(_total, "#,##0.00")
    End Sub

    Private Sub dgCart_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgCart.RowsRemoved
        lblTotal.Text = Format(dgTotal, "#,##0.00")
    End Sub

    Private Function dgTotal() As Double
        Dim tot As Double = 0
        Dim i As Integer = 0
        For i = 0 To dgCart.Rows.Count - 1
            tot = tot + Convert.ToDouble(dgCart.Rows(i).Cells(6).Value)
        Next i
        Return tot
    End Function

    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class