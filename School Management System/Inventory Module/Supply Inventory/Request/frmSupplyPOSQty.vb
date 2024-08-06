Imports MySql.Data.MySqlClient

Public Class frmSupplyPOSQty
    Dim id As String, price As Double
    Private Sub frmQty_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            frmSupplyPOS.txtItemID.Clear()
            Me.Dispose()
        ElseIf e.KeyCode = Keys.Enter Then
            Dim sdate As String = Now.ToString("yyyy-MM-dd")
            If CInt(txtQty.Text) > CInt(frmSupplyPOS.lblItemQTY.Text) Then
                MsgBox("Quantity to release is greater than current item spare. Current Item Spare: '" & CInt(frmSupplyPOS.lblItemQTY.Text) & "'", vbCritical)
                txtQty.Select()
            Else


                If frmSupplyPOS.lblLocation.Text = "Student" Then

                    Dim itemcount As Integer = 0

                    cn.Open()

                    Dim sql As String
                    If frmSupplyPOS.cs_hs.Text = "cs" Then
                        sql = "select count(*) as count from cfcissmsdb.tbl_assessment_additional where additional_stud_id = @1 and additional_period_id = @2 and additional_item_id = @3"
                    ElseIf frmSupplyPOS.cs_hs.Text = "hs" Then
                        sql = "select count(*) as count from cfcissmsdbhighschool.tbl_assessment_additional where additional_stud_id = @1 and additional_period_id = @2 and additional_item_id = @3"
                    End If

                    cm = New MySqlCommand(sql, cn)
                    With cm
                        .Parameters.AddWithValue("@1", frmSupplyPOS.stud_id.Text)
                        .Parameters.AddWithValue("@2", frmSupplyPOS.period_id.Text)
                        .Parameters.AddWithValue("@3", frmSupplyPOS.txtItemID.Text)
                    End With
                    dr = cm.ExecuteReader
                    While dr.Read
                        itemcount = itemcount + CInt(dr.Item("count").ToString)
                    End While
                    cn.Close()


                    If itemcount = 0 Then

                        cn.Open()

                        Dim sql2 As String
                        If frmSupplyPOS.cs_hs.Text = "cs" Then
                            sql2 = "insert into cfcissmsdb.tbl_assessment_additional (additional_period_id, additional_item_id, additional_stud_id, additional_amount, additional_date_added, additional_added_by, additional_qty, additional_price) values (@1,@2,@3,@4,@5,@6,@7,@8)"
                        ElseIf frmSupplyPOS.cs_hs.Text = "hs" Then
                            sql2 = "insert into cfcissmsdbhighschool.tbl_assessment_additional (additional_period_id, additional_item_id, additional_stud_id, additional_amount, additional_date_added, additional_added_by, additional_qty, additional_price) values (@1,@2,@3,@4,@5,@6,@7,@8)"
                        End If

                        cm = New MySqlCommand(sql2, cn)
                        With cm
                            .Parameters.AddWithValue("@1", frmSupplyPOS.period_id.Text)
                            .Parameters.AddWithValue("@2", frmSupplyPOS.txtItemID.Text)
                            .Parameters.AddWithValue("@3", frmSupplyPOS.stud_id.Text)
                            .Parameters.AddWithValue("@4", CDbl(frmSupplyPOS.lblItemPrice.Text) * CDbl(txtQty.Text))
                            .Parameters.AddWithValue("@5", sdate)
                            .Parameters.AddWithValue("@6", str_userid)
                            .Parameters.AddWithValue("@7", CDbl(txtQty.Text))
                            .Parameters.AddWithValue("@8", CDbl(frmSupplyPOS.lblItemPrice.Text))
                            .ExecuteNonQuery()
                        End With
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("Update tbl_supply_inventory set deployed = deployed+@deployqty, spare = spare-@deployqty where itembarcode = @itembarcode", cn)
                        With cm
                            .Parameters.AddWithValue("@deployqty", CInt(txtQty.Text))
                            .Parameters.AddWithValue("@itembarcode", frmSupplyPOS.txtItemID.Text)
                            .ExecuteNonQuery()
                        End With
                        cn.Close()
                    Else

                        cn.Open()

                        Dim sql3 As String
                        If frmSupplyPOS.cs_hs.Text = "cs" Then
                            sql3 = "update cfcissmsdb.tbl_assessment_additional set additional_qty = additional_qty+@1, additional_amount = additional_price * additional_qty where additional_stud_id = @2 and additional_item_id = @3 and additional_period_id = @4"
                        ElseIf frmSupplyPOS.cs_hs.Text = "hs" Then
                            sql3 = "update cfcissmsdbhighschool.tbl_assessment_additional set additional_qty = additional_qty+@1, additional_amount = additional_price * additional_qty where additional_stud_id = @2 and additional_item_id = @3 and additional_period_id = @4"
                        End If

                        cm = New MySqlCommand(sql3, cn)
                        With cm
                            .Parameters.AddWithValue("@1", CInt(txtQty.Text))
                            .Parameters.AddWithValue("@2", frmSupplyPOS.stud_id.Text)
                            .Parameters.AddWithValue("@3", frmSupplyPOS.txtItemID.Text)
                            .Parameters.AddWithValue("@4", CInt(frmSupplyPOS.period_id.Text))
                            .ExecuteNonQuery()
                        End With
                        cn.Close()

                        cn.Open()
                        cm = New MySqlCommand("Update tbl_supply_inventory set deployed = deployed+@deployqty, spare = spare-@deployqty where itembarcode = @itembarcode", cn)
                        With cm
                            .Parameters.AddWithValue("@deployqty", CDbl(txtQty.Text))
                            .Parameters.AddWithValue("@itembarcode", frmSupplyPOS.txtItemID.Text)
                            .ExecuteNonQuery()
                        End With

                        cn.Close()
                    End If
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
                        AutoNumber()
                        cn.Open()
                        cm = New MySqlCommand("insert into tbl_supply_deployed (dbarcode, dqty, dlocation, ddate, dprice, ditem_price, qty_requested,dstudentid, duser_id) values (@1,@2,@3,@4,@5,@6,@7,@8,@9)", cn)
                        With cm
                            .Parameters.AddWithValue("@1", frmSupplyPOS.txtItemID.Text)
                            .Parameters.AddWithValue("@2", CInt(txtQty.Text))
                            .Parameters.AddWithValue("@3", frmSupplyPOS.lblLocationNumber.Text)
                            .Parameters.AddWithValue("@4", sdate)
                            .Parameters.AddWithValue("@5", CDbl(frmSupplyPOS.lblItemPrice.Text))
                            .Parameters.AddWithValue("@6", CDbl(frmSupplyPOS.lblItemPrice.Text) * CDbl(txtQty.Text))
                            .Parameters.AddWithValue("@7", CInt(TextBox1.Text))
                            .Parameters.AddWithValue("@8", txtbox_code.Text)
                            .Parameters.AddWithValue("@9", str_userid)
                            .ExecuteNonQuery()
                        End With
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("Update tbl_supply_inventory set deployed = deployed+@deployqty, spare = spare-@deployqty where itembarcode = @itembarcode", cn)
                        With cm
                            .Parameters.AddWithValue("@deployqty", CInt(txtQty.Text))
                            .Parameters.AddWithValue("@itembarcode", frmSupplyPOS.txtItemID.Text)
                            .ExecuteNonQuery()
                        End With
                        cn.Close()
                    Else

                        cn.Open()
                        cm = New MySqlCommand("update tbl_supply_deployed set dqty = dqty+@1, qty_requested = @4, ditem_price = dprice * dqty where dlocation = @2 and dbarcode = @3 and dstatus = 'PENDING'", cn)
                        With cm
                            .Parameters.AddWithValue("@1", CInt(txtQty.Text))
                            .Parameters.AddWithValue("@2", frmSupplyPOS.lblLocationNumber.Text)
                            .Parameters.AddWithValue("@3", frmSupplyPOS.txtItemID.Text)
                            .Parameters.AddWithValue("@4", CInt(TextBox1.Text))
                            .ExecuteNonQuery()
                        End With
                        cn.Close()

                        cn.Open()
                        cm = New MySqlCommand("Update tbl_supply_inventory set deployed = deployed+@deployqty, spare = spare-@deployqty where itembarcode = @itembarcode", cn)
                        With cm
                            .Parameters.AddWithValue("@deployqty", CDbl(txtQty.Text))
                            .Parameters.AddWithValue("@itembarcode", frmSupplyPOS.txtItemID.Text)
                            .ExecuteNonQuery()
                        End With

                        cn.Close()

                    End If



                End If

                'cn.Open()
                'cm = New MySqlCommand("Update tblcart set total = price * qty where tableno = '" & frmPOS.lblTable.Text & "' and status = 'Pending'", cn)
                'cm.ExecuteNonQuery()
                'cn.Close()
                'AuditTrail("Added an order product id " & id & "; price " & Format(price, "n2") & "; order " & txtQty.Text & "; total " & Format(price * CDbl(txtQty.Text), "n2") & "; discount " & txtDiscount.Text & " for table " & SupplyPOS.lblLocation.Text & ".")
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

        If frmSupplyPOS.lblLocation.Text = "Student" Then
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

    Private Sub AutoNumber()
        Dim yearid As String = YearToday
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT dstudentid FROM tbl_supply_deployed WHERE dlocation NOT IN (0) and dstudentid like '" & yearid & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(dstudentid) as ID from tbl_supply_deployed", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            lastCode = lastCode.Remove(0, 4)
            txtbox_code.Text = CInt(yearid & lastCode) + 1
        Else
            dr.Close()
            txtbox_code.Text = yearid & "00001"
        End If
        cn.Close()
    End Sub
End Class