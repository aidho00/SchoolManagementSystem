Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmSupplyPOS
    Dim btnCategory As New Button
    Dim productPic As New PictureBox

    Dim _action As String
    Dim _filter As String = ""

    Private Sub frmPOS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)

        ' Get the working area of the primary screen (excluding taskbar)
        Dim workingArea As Rectangle = Screen.PrimaryScreen.WorkingArea

        ' Set the form's size and location to fit the working area
        Me.Size = New Size(workingArea.Width, workingArea.Height)
        Me.Location = New Point(workingArea.X, workingArea.Y)
    End Sub

    Private Sub frmPOS_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            If btnNewOrder.Enabled = True Then
                btnNewOrder_Click(sender, e)
            End If
        ElseIf e.KeyCode = Keys.F2 Then
            If btnSettle.Enabled = True Then
                btnSettle_Click(sender, e)
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            If MsgBox("Are you sure you want to exit?", vbYesNo + vbQuestion) = vbYes Then
                query("Update tbl_supply_location set status = 'False' where locationname = '" & lblLocation.Text & "'")
                lblLocationNumber.Text = "0"
                txtItemID.Clear()
                lblTotal.Text = "0.00"
                lblLocation.Text = ""
                lblTransno.Text = ""
                stud_id.Text = ""
                stud_name.Text = ""
                stud_yrcourse.Text = ""
                'stud_id.Visible = False
                dgCart.Rows.Clear()

                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnNewOrder_Click(sender As Object, e As EventArgs) Handles btnNewOrder.Click
        With frmSupplyPOSLocation
            .loadTable()
            .ShowDialog()
        End With
        loadCart()
    End Sub

    Sub loadCart()
        Dim _total As Double
        If lblLocation.Text = "STUDENT" Then

            dgCart.Rows.Clear()
            Try


                cn.Close()
                cn.Open()

                Dim sql As String
                If cs_hs.Text = "cs" Then
                    cm = New MySqlCommand("Select barcodeid, description, cfcissmsdb.tbl_assessment_additional.additional_price as Price, (cfcissmsdb.tbl_assessment_additional.additional_qty) as QTY, (cfcissmsdb.tbl_assessment_additional.additional_qty) as RQTY, cfcissmsdb.tbl_assessment_additional.additional_amount as Total, ifnull(cfcissmsdb.tbl_assessment_additional.additional_transno, '') as TransNo from cfcissmsdb.tbl_assessment_additional, cfcissmsdb.tbl_supply_item, cfcissmsdb.tbl_supply_category where cfcissmsdb.tbl_assessment_additional.additional_item_id = cfcissmsdb.tbl_supply_item.barcodeid AND cfcissmsdb.tbl_supply_item.categoryid = cfcissmsdb.tbl_supply_category.catid and cfcissmsdb.tbl_assessment_additional.additional_period_id = @1 and cfcissmsdb.tbl_assessment_additional.additional_stud_id = @2", cn)
                    cm.Parameters.AddWithValue("@1", CInt(cmb_period.SelectedValue))
                    cm.Parameters.AddWithValue("@2", stud_id.Text)
                    dr = cm.ExecuteReader()
                    While dr.Read
                        lblTransno.Text = dr.Item("TransNo").ToString
                        _total += CDbl(dr.Item("Total").ToString)
                        dgCart.Rows.Add(dr.Item("barcodeid").ToString, dr.Item("description").ToString, dr.Item("Price").ToString, dr.Item("QTY").ToString, dr.Item("RQTY").ToString, dr.Item("Total").ToString)
                    End While
                    dr.Close()
                    cn.Close()
                ElseIf cs_hs.Text = "hs" Then
                    cm = New MySqlCommand("Select barcodeid, description, cfcissmsdbhighschool.tbl_assessment_additional.additional_price as Price, (cfcissmsdbhighschool.tbl_assessment_additional.additional_qty) as QTY, (cfcissmsdbhighschool.tbl_assessment_additional.additional_qty) as RQTY, cfcissmsdbhighschool.tbl_assessment_additional.additional_amount as Total, ifnull(cfcissmsdbhighschool.tbl_assessment_additional.additional_transno, '') as TransNo from cfcissmsdbhighschool.tbl_assessment_additional, cfcissmsdb.tbl_supply_item, cfcissmsdb.tbl_supply_category where cfcissmsdbhighschool.tbl_assessment_additional.additional_item_id = cfcissmsdb.tbl_supply_item.barcodeid AND cfcissmsdb.tbl_supply_item.categoryid = cfcissmsdb.tbl_supply_category.catid and cfcissmsdbhighschool.tbl_assessment_additional.additional_period_id = @1 and cfcissmsdbhighschool.tbl_assessment_additional.additional_stud_id = @2", cn)
                    cm.Parameters.AddWithValue("@1", CInt(cmb_period.SelectedValue))
                    cm.Parameters.AddWithValue("@2", stud_id.Text)
                    dr = cm.ExecuteReader()
                    While dr.Read
                        lblTransno.Text = dr.Item("TransNo").ToString
                        _total += CDbl(dr.Item("Total").ToString)
                        dgCart.Rows.Add(dr.Item("barcodeid").ToString, dr.Item("description").ToString, dr.Item("Price").ToString, dr.Item("QTY").ToString, dr.Item("RQTY").ToString, dr.Item("Total").ToString)
                    End While
                    dr.Close()
                    cn.Close()
                End If
            Catch ex As Exception
            End Try
        Else

            dgCart.Rows.Clear()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("Select barcodeid, description, tbl_supply_deployed.dprice As Price, (dqty) As QTY, qty_requested As RQTY, tbl_supply_deployed.ditem_price As Total from tbl_supply_deployed, tbl_supply_item, tbl_supply_category where tbl_supply_deployed.dbarcode = tbl_supply_item.barcodeid And tbl_supply_item.categoryid = tbl_supply_category.catid And tbl_supply_deployed.dstatus = 'PENDING' and tbl_supply_deployed.dlocation = @1", cn)
            cm.Parameters.AddWithValue("@1", lblLocationNumber.Text)
            dr = cm.ExecuteReader()
            While dr.Read
                _total += CDbl(dr.Item("Total").ToString)
                dgCart.Rows.Add(dr.Item("barcodeid").ToString, dr.Item("description").ToString, dr.Item("Price").ToString, dr.Item("QTY").ToString, dr.Item("RQTY").ToString, dr.Item("Total").ToString)
            End While
            dr.Close()
            cn.Close()

        End If
        lblTotal.Text = Format(_total, "#,##0.00")
        If dgCart.Rows.Count = 0 Then
            btnSettle.Enabled = False
        Else
            btnSettle.Enabled = True
        End If
    End Sub

    Function GetTransno() As String
        Dim yearid As String = YearToday
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT dstudentid FROM tbl_supply_deployed WHERE dstudentid like 'SI-RQST" & yearid & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(dtransno) as ID from tbl_supply_deployed", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            lastCode = lastCode.Remove(0, 11)
            GetTransno = "SI-RQST" & CInt(yearid & lastCode) + 1
        Else
            dr.Close()
            GetTransno = "SI-RQST" & yearid & "00001"
        End If
        cn.Close()

        Return GetTransno
    End Function

    Private Sub AutoNumber()

    End Sub

    Private Sub dgCart_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCart.CellContentClick
        Dim colname As String = dgCart.Columns(e.ColumnIndex).Name
        If colname = "colRemove" Then

            Try
                If MsgBox("Cancel this item?", vbYesNo + vbQuestion) = vbYes Then
                    If lblLocation.Text = "STUDENT" Then
                        If str_role <> "Administrator" Then
                            MsgBox("Your are not authorized to cancel item(s)!", vbCritical)
                        Else
                            dgCart.Rows.Remove(dgCart.CurrentRow)
                        End If
                    Else
                        cn.Open()
                        cm = New MySqlCommand("Update tbl_supply_deployed set dstatus = 'CANCELLED' where dbarcode = @1 and dlocation = @2 and dstatus = 'PENDING'", cn)
                        With cm
                            .Parameters.AddWithValue("@1", dgCart.Rows(e.RowIndex).Cells(0).Value)
                            .Parameters.AddWithValue("@2", lblLocationNumber.Text)
                        End With
                        result = cm.ExecuteNonQuery
                        If result = 0 Then
                            MsgBox("Item has failed to cancel!", vbCritical)
                            cm.Dispose()
                            cn.Close()
                        Else
                            MsgBox("Item has been successfully cancelled!", vbInformation)
                            cm.Dispose()
                            cn.Close()
                            loadCart()
                        End If
                    End If

                Else
                End If
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub btnSettle_Click(sender As Object, e As EventArgs) Handles btnSettle.Click
        'With frmSettle
        '    .txtTotal.Text = lblTotal.Text
        '    .ShowDialog()
        'End With
        If dgCart.RowCount = 0 Then
            MsgBox("Transaction Failed! There are no items on the list.", vbCritical)
            Return
        End If
        If MsgBox("Are you sure you want to settle/aprrove this request?", vbYesNo + vbQuestion) = vbYes Then
            Try

                If lblLocation.Text = "STUDENT" Then

                    Dim studentstatus As String = ""
                    If cs_hs.Text = "cs" Then
                        studentstatus = "college"
                    ElseIf cs_hs.Text = "hs" Then
                        studentstatus = "highschool"
                    End If
                    lblTransno.Text = GetTransno()
                    For Each row As DataGridViewRow In dgCart.Rows
                        cn.Close()
                        cn.Open()
                        Dim sql2 As String
                        If cs_hs.Text = "cs" Then
                            studentstatus = "college"
                            sql2 = "insert into cfcissmsdb.tbl_assessment_additional (additional_period_id, additional_item_id, additional_stud_id, additional_amount, additional_date_added, additional_added_by, additional_qty, additional_price, additional_transno) values (@1,@2,@3,@4,CURDATE(),@6,@7,@8,@9)"
                        ElseIf cs_hs.Text = "hs" Then
                            studentstatus = "highschool"
                            sql2 = "insert into cfcissmsdbhighschool.tbl_assessment_additional (additional_period_id, additional_item_id, additional_stud_id, additional_amount, additional_date_added, additional_added_by, additional_qty, additional_price, additional_transno) values (@1,@2,@3,@4,CURDATE(),@6,@7,@8,@9)"
                        End If

                        cm = New MySqlCommand(sql2, cn)
                        With cm
                            .Parameters.AddWithValue("@1", CInt(cmb_period.SelectedValue))
                            .Parameters.AddWithValue("@2", row.Cells(0).Value)
                            .Parameters.AddWithValue("@3", stud_id.Text)
                            .Parameters.AddWithValue("@4", CDec(row.Cells(5).Value))

                            .Parameters.AddWithValue("@6", str_userid)
                            .Parameters.AddWithValue("@7", CInt(row.Cells(3).Value))
                            .Parameters.AddWithValue("@8", CInt(row.Cells(2).Value))
                            .Parameters.AddWithValue("@9", lblTransno.Text)
                            .ExecuteNonQuery()
                        End With
                        cn.Close()

                        cn.Open()
                        cm = New MySqlCommand("insert into tbl_supply_deployed (dbarcode, dqty, dlocation, ddate, dprice, ditem_price, qty_requested,dstudentid, druser_id, dperiodid, dtransno, dstatus) values (@1,@2,@3,CURDATE(),@5,@6,@7,@8,@9,@10,@11,'APPROVED')", cn)
                        With cm
                            .Parameters.AddWithValue("@1", row.Cells(0).Value)
                            .Parameters.AddWithValue("@2", CInt(row.Cells(3).Value))
                            .Parameters.AddWithValue("@3", lblLocationNumber.Text)

                            .Parameters.AddWithValue("@5", CDec(row.Cells(2).Value))
                            .Parameters.AddWithValue("@6", CDec(row.Cells(2).Value) * CInt(row.Cells(3).Value))
                            .Parameters.AddWithValue("@7", CInt(row.Cells(4).Value))
                            If cs_hs.Text = "cs" Then
                                .Parameters.AddWithValue("@8", "CS - " & stud_id.Text)
                            ElseIf cs_hs.Text = "hs" Then
                                .Parameters.AddWithValue("@8", "HS - " & stud_id.Text)
                            End If
                            .Parameters.AddWithValue("@9", str_userid)
                            .Parameters.AddWithValue("@10", CInt(cmb_period.SelectedValue))
                            .Parameters.AddWithValue("@11", lblTransno.Text)
                            .ExecuteNonQuery()
                        End With
                        StockLedger(row.Cells(0).Value, 0, CInt(row.Cells(3).Value), "Issued to " & studentstatus & " student.", "Student Item Release", "Item Release No." & lblTransno.Text & ". Student ID: " & stud_id.Text & ".")
                    Next

                    Try
                        Dim dt As New DataTable
                        With dt
                            .Columns.Add("barcodeid")
                            .Columns.Add("description")
                            .Columns.Add("size")
                            .Columns.Add("spare")
                            .Columns.Add("item_price")
                        End With
                        For Each dvr As DataGridViewRow In dgCart.Rows
                            dt.Rows.Add(dvr.Cells(0).Value, dvr.Cells(1).Value, dvr.Cells(2).Value, dvr.Cells(3).Value, dvr.Cells(5).Value)
                        Next
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New Supply_Inventory_Stub
                        rptdoc.SetDataSource(dt)
                        rptdoc.SetParameterValue("preparedby", str_name)
                        rptdoc.SetParameterValue("student_id", stud_id.Text)
                        rptdoc.SetParameterValue("idate", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
                        rptdoc.SetParameterValue("student_name", stud_name.Text)
                        frmReportViewer.ReportViewer.ReportSource = rptdoc
                        frmReportViewer.ShowDialog()
                    Catch ex As Exception
                        MessageBox.Show("No Crystal Report found. Please install crystal report from application directory.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Try

                    lblLocationNumber.Text = "0"
                    txtItemID.Clear()
                    lblTotal.Text = "0.00"
                    lblLocation.Text = ""
                    lblTransno.Text = ""
                    dgCart.Rows.Clear()

                    stud_gender.Text = ""
                    stud_id.Text = ""
                    stud_name.Text = ""
                    stud_yrcourse.Text = ""
                    cmb_period.DataSource = Nothing
                    student_info_panel.Visible = False

                    MsgBox("Items have been issued to the student successfully!", vbInformation)

                Else

                    lblTransno.Text = GetTransno()

                    For Each row As DataGridViewRow In dgCart.Rows
                        query("Update tbl_supply_deployed set dtransno = '" & lblTransno.Text & "' , dstatus = 'APPROVED', druser_id = '" & str_userid & "', drdate = CURDATE() where dlocation = '" & lblLocationNumber.Text & "' and dbarcode = '" & row.Cells(0).Value & "' and dstatus = 'PENDING'")
                        StockLedger(row.Cells(0).Value, 0, CInt(row.Cells(3).Value), "Issued to " & lblLocation.Text & ".", "Office Item Release", "Item Release No." & lblTransno.Text & "")
                    Next
                    Try
                        Dim dt As New DataTable
                        With dt
                            .Columns.Add("barcodeid")
                            .Columns.Add("description")
                            .Columns.Add("categoryname")
                            .Columns.Add("sizes")
                            .Columns.Add("dqty")
                            .Columns.Add("dprice")
                            .Columns.Add("ditemprice")
                        End With
                        For Each dvr As DataGridViewRow In dgCart.Rows
                            dt.Rows.Add(dvr.Cells(0).Value, dvr.Cells(1).Value, "", "", dvr.Cells(3).Value, dvr.Cells(2).Value, dvr.Cells(5).Value)
                        Next
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New Supply_Request
                        rptdoc.SetDataSource(dt)
                        rptdoc.SetParameterValue("preparedby", str_name)
                        rptdoc.SetParameterValue("requestoffice", lblLocation.Text)
                        rptdoc.SetParameterValue("requestdate", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
                        rptdoc.SetParameterValue("requestid", lblTransno.Text)
                        rptdoc.SetParameterValue("requeststatus", "APPROVED")
                        frmReportViewer.ReportViewer.ReportSource = rptdoc
                        frmReportViewer.ShowDialog()
                    Catch ex As Exception
                        MessageBox.Show("No Crystal Report found. Please install crystal report from application directory.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Try
                    lblLocation.Text = String.Empty
                    lblTransno.Text = String.Empty
                    loadCart()

                    MsgBox("Request successfully settled/approved!", vbInformation)
                End If

                'Me.Dispose()
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Private Sub btnCancelOrder_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtItemID_TextChanged(sender As Object, e As EventArgs) Handles txtItemID.TextChanged
        Panel4.Visible = False
        If lblLocation.Text = String.Empty And txtItemID.Text = String.Empty Then

        ElseIf lblLocation.Text = String.Empty Then
            MsgBox("Please start new request first to select requester!", vbCritical)
            txtItemID.Clear()
        Else
            If txtItemID.Text.Length = 16 Then
                If lblLocation.Text = String.Empty And txtItemID.Text = String.Empty Then
                    MsgBox("Please start new request first to select requester!", vbCritical)
                    txtItemID.Clear()
                ElseIf lblLocation.Text = String.Empty Then
                    MsgBox("Please start new request first to select requester!", vbCritical)
                    txtItemID.Clear()
                Else
                    cn.Open()
                    cm = New MySqlCommand("select Description,(Spare) as SQTY, item_price from tbl_supply_inventory, tbl_supply_item, tbl_supply_category where tbl_supply_inventory.itembarcode = tbl_supply_item.barcodeid AND tbl_supply_item.categoryid = tbl_supply_category.catid AND barcodeid = '" & txtItemID.Text & "'", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        lblDescription.Text = dr.Item("Description").ToString
                        lblItemPrice.Text = dr.Item("item_price").ToString
                        lblItemQTY.Text = dr.Item("SQTY").ToString
                    End If
                    dr.Close()
                    cn.Close()
                    If CInt(lblItemQTY.Text) = 0 Then
                        MsgBox("Item is out of stock!", vbExclamation)
                        txtItemID.Text = String.Empty
                        lblDescription.Text = ""
                        lblItemPrice.Text = ""
                        lblItemQTY.Text = "0"
                    Else
                        With frmSupplyPOSQty
                            .ShowDialog()
                        End With
                    End If
                End If
            Else
            End If
        End If
    End Sub

    Private Sub lblSearch_Click(sender As Object, e As EventArgs) Handles lblSearch.Click
        If lblLocation.Text = String.Empty Then
            MsgBox("Please start new request first to select requester!", vbCritical)
        Else
            loaditems()
            Panel4.Visible = True
            FlowLayoutPanel1.Size = New Size(725, 44)
        End If
    End Sub

    Sub loaditems()
        Try
            dgItemList.Rows.Clear()
            cn.Open()
            Dim sql As String = ""
            If lblLocation.Text = "STUDENT" Then
                sql = "Select barcodeid, description, (categoryname) as category, item_price from tbl_supply_item t1 JOIN tbl_supply_category t2 ON t1.categoryid = t2.catID where t2.categorytype  = 'School Consumable' and CONCAT(t1.description,t2.categoryname) like '%" & txtSearch.Text & "%'"
            Else
                sql = "Select barcodeid, description, (categoryname) as category, item_price from tbl_supply_item t1 JOIN tbl_supply_category t2 ON t1.categoryid = t2.catID where t2.categorytype  = 'Office Supply' and CONCAT(t1.description,t2.categoryname) like '%" & txtSearch.Text & "%'"
            End If
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader()
            While dr.Read
                dgItemList.Rows.Add(dr.Item("barcodeid").ToString, dr.Item("description").ToString, dr.Item("category").ToString, dr.Item("item_price").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgItemList.CellDoubleClick
        If lblLocation.Text = String.Empty And txtItemID.Text = String.Empty Then
            MsgBox("Please start new request first and select requester!", vbCritical)
            txtItemID.Clear()
            Panel4.Visible = False
        ElseIf lblLocation.Text = String.Empty Then
            MsgBox("Please start new request first and select requester!", vbCritical)
            txtItemID.Clear()
            Panel4.Visible = False
        Else
            txtItemID.Text = dgItemList.CurrentRow.Cells(0).Value.ToString
            Panel4.Visible = False
            FlowLayoutPanel1.Size = New Size(725, 89)
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        loaditems()
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs)
        Panel4.Visible = False
    End Sub

    Private Sub lblLocation_TextChanged(sender As Object, e As EventArgs) Handles lblLocation.TextChanged
        If lblLocation.Text = "STUDENT" Then
            student_info_panel.Visible = True
            frmSupplyPOSStudID.ShowDialog()
        Else
            student_info_panel.Visible = False
        End If
    End Sub

    Private Sub PeriodList()
        Try
            If cs_hs.Text = "cs" Then
                fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  cfcissmsdb.tbl_period t1 JOIN cfcissmsdb.tbl_student_paid_account_breakdown t2 ON t1.period_id = t2.spab_period_id where t2.spab_stud_id = '" & stud_id.Text & "' order by  `period_name` desc, `period_semester` desc, `period_status` asc", cmb_period, "period", "PERIOD", "period_id")
            ElseIf cs_hs.Text = "hs" Then
                fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  cfcissmsdbhighschool.tbl_period t1 JOIN cfcissmsdbhighschool.tbl_student_paid_account_breakdown t2 ON t1.period_id = t2.spab_period_id where t2.spab_stud_id = '" & stud_id.Text & "' order by  `period_name` desc, `period_semester` desc, `period_status` asc", cmb_period, "period", "PERIOD", "period_id")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub stud_id_TextChanged(sender As Object, e As EventArgs) Handles stud_id.TextChanged
        PeriodList()
        loadCart()
    End Sub

    Private Sub cmb_period_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_period.SelectedIndexChanged
        loadCart()
    End Sub

    Private Sub cmb_period_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_period.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure you want to exit?", vbYesNo + vbQuestion) = vbYes Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("Update tbl_supply_location set status = 'False' where locationname = '" & lblLocation.Text & "'", cn)
            cm.ExecuteNonQuery()
            cn.Close()

            lblLocationNumber.Text = "0"
            txtItemID.Clear()
            lblTotal.Text = "0.00"
            lblLocation.Text = ""
            lblTransno.Text = ""
            dgCart.Rows.Clear()

            Me.Close()
        End If
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If lblLocation.Text = String.Empty And lblLocationNumber.Text = String.Empty And txtItemID.Text = String.Empty Then
            MsgBox("Please start new request first and select location!", vbCritical)
            txtItemID.Clear()
            Panel4.Visible = False
        ElseIf lblLocation.Text = String.Empty And lblLocationNumber.Text = String.Empty Then
            MsgBox("Please start new request first and select location!", vbCritical)
            txtItemID.Clear()
            Panel4.Visible = False
        Else
            txtItemID.Text = dgItemList.CurrentRow.Cells(0).Value.ToString
            Panel4.Visible = False
            FlowLayoutPanel1.Size = New Size(725, 89)
        End If
    End Sub

    Private Sub dgCart_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgCart.RowsAdded
        lblTotal.Text = Format(GetColumnSum(dgCart, 5), "#,##0.00")
    End Sub

    Private Sub dgCart_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgCart.RowsRemoved
        lblTotal.Text = Format(GetColumnSum(dgCart, 5), "#,##0.00")
    End Sub
End Class
