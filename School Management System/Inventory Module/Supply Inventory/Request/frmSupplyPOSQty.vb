Imports MySql.Data.MySqlClient

Public Class frmSupplyPOSQty
    Dim id As String, price As Double
    Private Sub frmQty_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            frmSupplyPOS.txtItemID.Clear()
            frmSupplyPOS.lblDescription.Text = ""
            frmSupplyPOS.lblItemPrice.Text = "0.00"
            Me.Dispose()
        ElseIf e.KeyCode = Keys.Enter Then
            Dim sdate As String = Now.ToString("yyyy-MM-dd")

            If IS_EMPTY(TextBox1) = True Then Return
            If IS_EMPTY(txtQty) = True Then Return

            If CInt(txtQty.Text) > CInt(frmSupplyPOS.lblItemQTY.Text) Then
                MsgBox("Quantity to release is greater than current item stock. Current Item Stock: '" & CInt(frmSupplyPOS.lblItemQTY.Text) & "'", vbExclamation)
                txtQty.Select()
            Else

                If frmSupplyPOS.lblLocation.Text = "STUDENT" Then

                    Dim isFound As Boolean = False
                    Dim insufficientStock As Boolean = False
                    Dim rqstdValue As Integer = 0

                    For Each row As DataGridViewRow In frmSupplyPOS.dgCart.Rows
                        If row.Cells(0).Value = frmSupplyPOS.txtItemID.Text Then

                            rqstdValue = CInt(row.Cells(3).Value) + CInt(txtQty.Text)
                            If rqstdValue > CInt(frmSupplyPOS.lblItemQTY.Text) Then
                                isFound = True
                                MsgBox("Insufficient stock. Quantity to release is greater than current item stock. Current Item Stock: '" & CInt(frmSupplyPOS.lblItemQTY.Text) & "'", vbExclamation)
                                Exit For
                            Else
                                isFound = True
                                row.Cells(3).Value = CInt(row.Cells(3).Value) + CInt(txtQty.Text)
                                row.Cells(5).Value = CDec(row.Cells(2).Value) * CInt(row.Cells(3).Value)
                                frmSupplyPOS.lblTotal.Text = Format(GetColumnSum(frmSupplyPOS.dgCart, 5), "#,##0.00")
                                Exit For
                            End If
                        Else
                            isFound = False
                        End If
                    Next

                    If isFound = False Then
                        frmSupplyPOS.dgCart.Rows.Add(frmSupplyPOS.txtItemID.Text, frmSupplyPOS.lblDescription.Text, CDec(frmSupplyPOS.lblItemPrice.Text), CInt(txtQty.Text), CInt(TextBox1.Text), CDec(frmSupplyPOS.lblItemPrice.Text) * CInt(txtQty.Text))
                    End If

                    With frmSupplyPOS
                        .txtItemID.Clear()
                        .lblDescription.Text = ""
                        .lblItemPrice.Text = "0.00"
                    End With
                    Me.Dispose()

                Else

                    Dim itemcount As Integer = 0

                    cn.Open()
                    cm = New MySqlCommand("select count(*) as count from tbl_supply_deployed where dlocation = @1 and dbarcode = @2 and dstatus = 'PENDING'", cn)
                    With cm
                        .Parameters.AddWithValue("@1", frmSupplyPOS.lblLocationNumber.Text)
                        .Parameters.AddWithValue("@2", frmSupplyPOS.txtItemID.Text)
                    End With
                    dr = cm.ExecuteReader
                    While dr.Read
                        itemcount = itemcount + CInt(dr.Item("count").ToString)
                    End While
                    cn.Close()


                    If itemcount = 0 Then

                        cn.Open()
                        cm = New MySqlCommand("insert into tbl_supply_deployed (dbarcode, dqty, dlocation, ddate, dprice, ditem_price, qty_requested,dstudentid, duser_id) values (@1,@2,@3,CURDATE(),@5,@6,@7,@8,@9)", cn)
                        With cm
                            .Parameters.AddWithValue("@1", frmSupplyPOS.txtItemID.Text)
                            .Parameters.AddWithValue("@2", CInt(txtQty.Text))
                            .Parameters.AddWithValue("@3", frmSupplyPOS.lblLocationNumber.Text)

                            .Parameters.AddWithValue("@5", CDbl(frmSupplyPOS.lblItemPrice.Text))
                            .Parameters.AddWithValue("@6", CDbl(frmSupplyPOS.lblItemPrice.Text) * CDbl(txtQty.Text))
                            .Parameters.AddWithValue("@7", CInt(TextBox1.Text))
                            .Parameters.AddWithValue("@8", "")
                            .Parameters.AddWithValue("@9", str_userid)
                            .ExecuteNonQuery()
                        End With

                    Else

                        cn.Open()
                        cm = New MySqlCommand("update tbl_supply_deployed set ddate = CURDATE(), dqty = dqty+@1, qty_requested = qty_requested+@4, ditem_price = dprice * dqty where dlocation = @2 and dbarcode = @3 and dstatus = 'PENDING'", cn)
                        With cm
                            .Parameters.AddWithValue("@1", CInt(txtQty.Text))
                            .Parameters.AddWithValue("@2", frmSupplyPOS.lblLocationNumber.Text)
                            .Parameters.AddWithValue("@3", frmSupplyPOS.txtItemID.Text)
                            .Parameters.AddWithValue("@4", CInt(TextBox1.Text))
                            .ExecuteNonQuery()
                        End With
                        cn.Close()

                    End If
                End If
                With frmSupplyPOS
                    .loadCart()
                    .txtItemID.Clear()
                    .lblDescription.Text = ""
                    .lblItemPrice.Text = "0.00"
                End With
                Me.Dispose()
            End If
        End If
    End Sub

    'Public Function duplicateEntry(ByVal location_number As Integer, ByVal item_id As String) As Boolean
    '    cn.Open()
    '    cm = New MySqlCommand("select count(*) from tbl_supply_deployed where location = @1 and deployitemid = @2 and transno = '' and status = 'Pending'", cn)
    '    With cm
    '        .Parameters.AddWithValue("@1", location_number)
    '        .Parameters.AddWithValue("@2", item_id)
    '    End With
    '    Dim i As Integer = CInt(cm.ExecuteScalar)
    '    cn.Close()
    '    If i > 0 Then Return True Else Return False

    'End Function

    Private Sub frmQty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        If frmSupplyPOS.lblLocation.Text = "STUDENT" Then
            Me.Height = 108
        Else
            Me.Height = 208
        End If

    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        txtQty.Text = TextBox1.Text
    End Sub


    'Sub Addtocart(ByVal id As String, ByVal price As Double, weight As Boolean)
    '    If weight = False Then lblQty.Text = "Quantity" Else lblQty.Text = "Weight"
    '    Me.price = price
    '    Me.id = id
    'End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress, TextBox1.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 46
            Case 8
            Case 13
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        frmSupplyPOS.txtItemID.Clear()
        Me.Dispose()
    End Sub
End Class