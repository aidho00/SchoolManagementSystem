Imports MySql.Data.MySqlClient

Public Class frmSupplyReturnQTY
    Private Sub frmQty_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            'frmDefectivePOS.txtItemID.Clear()
            Me.Dispose()
        ElseIf e.KeyCode = Keys.Enter Then
            If CInt(txtQty.Text) > CInt(frmSupplyReturn.dgCart.CurrentRow.Cells(3).Value) Or CInt(txtQty.Text) = 0 Then
                MsgBox("Invalid item quantity to return!",)
                Return
            End If
            Dim x As Integer
            x = CInt(frmSupplyReturn.dgCart.CurrentRow.Cells(3).Value) - CInt(txtQty.Text)
            If MsgBox("Are you sure you want to return this item(s)?", vbYesNo + vbQuestion) = vbYes Then
                If x = 0 Then
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("Update cfcissmsdb_supply.tbl_supply_deployed set dqty = @1, qty_returned = @2, dstatus = 'CANCELLED', ditem_price = dprice * @1 where dbarcode = @3 and dtransno = @4", cn)
                    With cm
                        .Parameters.AddWithValue("@1", x)
                        .Parameters.AddWithValue("@2", CInt(txtQty.Text))
                        .Parameters.AddWithValue("@3", frmSupplyReturn.dgCart.CurrentRow.Cells(0).Value)
                        .Parameters.AddWithValue("@4", frmSupplyRecords.cbRequests.Text)
                        .ExecuteNonQuery()
                        .Dispose()
                    End With
                    cn.Close()

                    cn.Open()
                    cm = New MySqlCommand("update cfcissmsdb_supply.tbl_supply_inventory set spare = spare + @1, deployed = deployed - @2 where itembarcode = @3", cn)
                    With cm
                        .Parameters.AddWithValue("@1", CInt(txtQty.Text))
                        .Parameters.AddWithValue("@2", x)
                        .Parameters.AddWithValue("@3", frmSupplyReturn.dgCart.CurrentRow.Cells(0).Value)
                        .ExecuteNonQuery()
                        .Dispose()
                    End With
                    cn.Close()

                    If frmSupplyRecords.txtboxlocation.Text = "STUDENT" Then
                        cn.Open()
                        cm = New MySqlCommand("update tbl_assessment_additional set additional_qty = 0, additional_amount = 0 where additional_item_id = @1 and additional_transno = @2", cn)
                        With cm
                            .Parameters.AddWithValue("@1", frmSupplyReturn.dgCart.CurrentRow.Cells(0).Value)
                            .Parameters.AddWithValue("@2", frmSupplyRecords.cbRequests.Text)
                            .ExecuteNonQuery()
                            .Dispose()
                        End With
                        cn.Close()
                    Else
                    End If

                    StockLedger(frmSupplyReturn.dgCart.CurrentRow.Cells(0).Value, CInt(txtQty.Text), 0, "Supply Request Item Return", "Stock Return", "REQUEST ID: " & frmSupplyRecords.cbRequests.Text)

                    MsgBox("Item(s) has been successfully returned!", vbInformation)

                    With frmSupplyReturn
                        .loadCart2()
                    End With
                    Me.Dispose()
                Else
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("Update cfcissmsdb_supply.tbl_supply_deployed set dqty = @1, qty_returned = @2, ditem_price = dprice * @1 where dbarcode = @3 and dtransno = @4", cn)
                    With cm
                        .Parameters.AddWithValue("@1", x)
                        .Parameters.AddWithValue("@2", CInt(txtQty.Text))
                        .Parameters.AddWithValue("@3", frmSupplyReturn.dgCart.CurrentRow.Cells(0).Value)
                        .Parameters.AddWithValue("@4", frmSupplyRecords.cbRequests.Text)
                        .ExecuteNonQuery()
                        .Dispose()
                    End With
                    cn.Close()

                    cn.Open()
                    cm = New MySqlCommand("update cfcissmsdb_supply.tbl_supply_inventory set spare = spare + @1, deployed = deployed - @2 where itembarcode = @3", cn)
                    With cm
                        .Parameters.AddWithValue("@1", CInt(txtQty.Text))
                        .Parameters.AddWithValue("@2", x)
                        .Parameters.AddWithValue("@3", frmSupplyReturn.dgCart.CurrentRow.Cells(0).Value)
                        .ExecuteNonQuery()
                        .Dispose()
                    End With
                    cn.Close()

                    If frmSupplyRecords.txtboxlocation.Text = "STUDENT" Then
                        cn.Open()
                        cm = New MySqlCommand("update cfcissmsdb_supply.tbl_assessment_additional set additional_qty = @3, additional_amount = additional_price * @3  where additional_item_id = @1 and additional_transno = @2", cn)
                        With cm
                            .Parameters.AddWithValue("@1", frmSupplyReturn.dgCart.CurrentRow.Cells(0).Value)
                            .Parameters.AddWithValue("@2", frmSupplyRecords.cbRequests.Text)
                            .Parameters.AddWithValue("@3", x)
                            .ExecuteNonQuery()
                            .Dispose()
                        End With
                        cn.Close()
                    Else
                    End If

                    StockLedger(frmSupplyReturn.dgCart.CurrentRow.Cells(0).Value, CInt(txtQty.Text), 0, "Supply Requested Item Return", "Stock Return", "Request ID: " & frmSupplyRecords.cbRequests.Text & "")

                    MsgBox("Item(s) has been successfully returned!", vbInformation)

                    With frmSupplyReturn
                        .loadCart2()
                    End With
                    Me.Dispose()
                End If
            End If
        Else
        End If
    End Sub

    Private Sub frmQty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 46
            Case 8
            Case 13
            Case Else
                e.Handled = True
        End Select
    End Sub
End Class