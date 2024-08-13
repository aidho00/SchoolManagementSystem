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
        If lblLocation.Text = "Student" Then

            dgCart.Rows.Clear()
            cn.Close()
            cn.Open()

            Dim sql As String
            If cs_hs.Text = "cs" Then
                cm = New MySqlCommand("Select barcodeid, description, cfcissmsdb.tbl_assessment_additional.additional_price as Price, (cfcissmsdb.tbl_assessment_additional.additional_qty) as QTY, (cfcissmsdb.tbl_assessment_additional.additional_qty) as RQTY, cfcissmsdb.tbl_assessment_additional.additional_amount as Total from cfcissmsdb.tbl_assessment_additional, cfcissmsdb.tbl_supply_item, cfcissmsdb.tbl_supply_category where cfcissmsdb.tbl_assessment_additional.additional_item_id = cfcissmsdb.tbl_supply_item.barcodeid AND cfcissmsdb.tbl_supply_item.categoryid = cfcissmsdb.tbl_supply_category.catid and cfcissmsdb.tbl_assessment_additional.additional_period_id = @1 and cfcissmsdb.tbl_assessment_additional.additional_stud_id = @2", cn)
                cm.Parameters.AddWithValue("@1", CInt(cmb_period.SelectedValue))
                cm.Parameters.AddWithValue("@2", stud_id.Text)
                dr = cm.ExecuteReader()
                While dr.Read
                    _total += CDbl(dr.Item("Total").ToString)
                    dgCart.Rows.Add(dr.Item("barcodeid").ToString, dr.Item("description").ToString, dr.Item("Price").ToString, dr.Item("QTY").ToString, dr.Item("RQTY").ToString, dr.Item("Total").ToString)
                End While
                dr.Close()
                cn.Close()
            ElseIf cs_hs.Text = "hs" Then
                cm = New MySqlCommand("Select barcodeid, description, cfcissmsdbhighschool.tbl_assessment_additional.additional_price as Price, (cfcissmsdbhighschool.tbl_assessment_additional.additional_qty) as QTY, (cfcissmsdbhighschool.tbl_assessment_additional.additional_qty) as RQTY, cfcissmsdbhighschool.tbl_assessment_additional.additional_amount as Total from cfcissmsdbhighschool.tbl_assessment_additional, cfcissmsdb.tbl_supply_item, cfcissmsdb.tbl_supply_category where cfcissmsdbhighschool.tbl_assessment_additional.additional_item_id = cfcissmsdb.tbl_supply_item.barcodeid AND cfcissmsdb.tbl_supply_item.categoryid = cfcissmsdb.tbl_supply_category.catid and cfcissmsdbhighschool.tbl_assessment_additional.additional_period_id = @1 and cfcissmsdbhighschool.tbl_assessment_additional.additional_stud_id = @2", cn)
                cm.Parameters.AddWithValue("@1", CInt(cmb_period.SelectedValue))
                cm.Parameters.AddWithValue("@2", stud_id.Text)
                dr = cm.ExecuteReader()
                While dr.Read
                    _total += CDbl(dr.Item("Total").ToString)
                    dgCart.Rows.Add(dr.Item("barcodeid").ToString, dr.Item("description").ToString, dr.Item("Price").ToString, dr.Item("QTY").ToString, dr.Item("RQTY").ToString, dr.Item("Total").ToString)
                End While
                dr.Close()
                cn.Close()
            End If



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
        cm = New MySqlCommand("SELECT dstudentid FROM tbl_supply_deployed WHERE dstudentid like '" & yearid & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(dstudentid) as ID from tbl_supply_deployed", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            lastCode = lastCode.Remove(0, 4)
            GetTransno = CInt(yearid & lastCode) + 1
        Else
            dr.Close()
            GetTransno = yearid & "00001"
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
                    If lblLocation.Text = "Student" Then
                        If str_role <> "Administrator" Then
                            MsgBox("Your are not authorized to cancel items(s)!", vbCritical)
                        Else
                            If cs_hs.Text = "cs" Then
                                cn.Open()
                                cm = New MySqlCommand("delete from cfcissmsdb.tbl_assessment_additional where additional_item_id = @1 and additional_stud_id = @2 and additional_period_id = @3", cn)
                                With cm
                                    .Parameters.AddWithValue("@1", dgCart.Rows(e.RowIndex).Cells(0).Value)
                                    .Parameters.AddWithValue("@2", stud_id.Text)
                                    .Parameters.AddWithValue("@3", CInt(cmb_period.SelectedValue))
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
                                    'AuditTrail("Voided an order with description " & dgCart.Rows(e.RowIndex).Cells(1).Value & "; price " & dgCart.Rows(e.RowIndex).Cells(2).Value & "; order " & dgCart.Rows(e.RowIndex).Cells(3).Value & "; total " & dgCart.Rows(e.RowIndex).Cells(4).Value & " from table " & lblLocation.Text & ".")
                                    loadCart()
                                End If
                            ElseIf cs_hs.Text = "hs" Then
                                cn.Open()
                                cm = New MySqlCommand("delete from cfcissmsdbhighschool.tbl_assessment_additional where additional_item_id = @1 and additional_stud_id = @2 and additional_period_id = @3", cn)
                                With cm
                                    .Parameters.AddWithValue("@1", dgCart.Rows(e.RowIndex).Cells(0).Value)
                                    .Parameters.AddWithValue("@2", stud_id.Text)
                                    .Parameters.AddWithValue("@3", CInt(cmb_period.SelectedValue))
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
                                    'AuditTrail("Voided an order with description " & dgCart.Rows(e.RowIndex).Cells(1).Value & "; price " & dgCart.Rows(e.RowIndex).Cells(2).Value & "; order " & dgCart.Rows(e.RowIndex).Cells(3).Value & "; total " & dgCart.Rows(e.RowIndex).Cells(4).Value & " from table " & lblLocation.Text & ".")
                                    loadCart()
                                End If
                            End If
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
                            'AuditTrail("Voided an order with description " & dgCart.Rows(e.RowIndex).Cells(1).Value & "; price " & dgCart.Rows(e.RowIndex).Cells(2).Value & "; order " & dgCart.Rows(e.RowIndex).Cells(3).Value & "; total " & dgCart.Rows(e.RowIndex).Cells(4).Value & " from table " & lblLocation.Text & ".")
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
        If MsgBox("Are you sure you want to settle/aprrove this request?", vbYesNo + vbQuestion) = vbYes Then
            Try

                If lblLocation.Text = "Student" Then
                    MsgBox("Student request/additional has already been Settled/Approved!", vbInformation)
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

                    lblLocation.Text = String.Empty
                    lblTransno.Text = String.Empty
                    loadCart()
                    stud_id.Text = ""
                    stud_name.Text = ""
                    stud_yrcourse.Text = ""
                Else
                    lblTransno.Text = GetTransno()

                    Dim sdate As String = Now.ToString("yyyy-MM-dd")
                    Dim stime As String = Now.ToString("hh:mm:ss")

                    cn.Open()
                    cm = New MySqlCommand("Update tbl_supply_deployed set dstudentid = @1 , dstatus = 'APPROVED', duser_id = '" & str_userid & "' where dlocation = '" & lblLocationNumber.Text & "' and dstatus = 'PENDING'", cn)
                    cm.Parameters.AddWithValue("@1", lblTransno.Text)
                    cm.ExecuteNonQuery()
                    cn.Close()

                    For Each row As DataGridViewRow In dgCart.Rows
                        StockLedger(row.Cells(0).Value, 0, CInt(row.Cells(3).Value), "Issued to " & lblLocation.Text & ".", "Office Item Release", "Item Release No." & lblTransno.Text & "")
                    Next


                        cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("Update tbl_supply_location set status = 'False' where locationname = '" & lblLocation.Text & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()

                    lblLocation.Text = String.Empty
                    lblTransno.Text = String.Empty
                    loadCart()
                    'stud_id.Visible = False
                    MsgBox("Request successfully Settled/Approved!", vbInformation)
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
        If lblLocation.Text = String.Empty And txtItemID.Text = String.Empty Then

        ElseIf lblLocation.Text = String.Empty Then
            MsgBox("Please start new request first to select location!", vbCritical)
            txtItemID.Clear()
        Else
            If txtItemID.Text.Length = 13 Then
                If lblLocation.Text = String.Empty And txtItemID.Text = String.Empty Then
                    MsgBox("Please start new request first to select location!", vbCritical)
                    txtItemID.Clear()
                ElseIf lblLocation.Text = String.Empty Then
                    MsgBox("Please start new request first to select location!", vbCritical)
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
                    With frmSupplyPOSQty
                        .ShowDialog()
                    End With
                End If
            Else
            End If
        End If
    End Sub

    Private Sub lblSearch_Click(sender As Object, e As EventArgs) Handles lblSearch.Click
        loaditems()
        Panel4.Visible = True
        FlowLayoutPanel1.Size = New Size(725, 44)
    End Sub

    Sub loaditems()
        Try
            dgItemList.Rows.Clear()
            cn.Open()
            Dim sql As String = ""
            If lblLocation.Text = "Student" Then
                sql = "Select barcodeid, description, (categoryname) as category, item_price from tbl_supply_item t1 JOIN tbl_supply_category t2 ON t1.categoryid = t2.catID where t2.supply_type  = 'School Consumable' and CONCAT(t1.description,t2.categoryname) like '%" & txtSearch.Text & "%'"
            Else
                sql = "Select barcodeid, description, (categoryname) as category, item_price from tbl_supply_item t1 JOIN tbl_supply_category t2 ON t1.categoryid = t2.catID where t2.supply_type  = 'Office Supply' and CONCAT(t1.description,t2.categoryname) like '%" & txtSearch.Text & "%'"
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
            MsgBox("Please start new request first and select location!", vbCritical)
            txtItemID.Clear()
            Panel4.Visible = False
        ElseIf lblLocation.Text = String.Empty Then
            MsgBox("Please start new request first and select location!", vbCritical)
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

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Panel4.Visible = False
    End Sub

    Private Sub lblLocation_TextChanged(sender As Object, e As EventArgs) Handles lblLocation.TextChanged
        If lblLocation.Text = "Student" Then
            student_info_panel.Visible = True
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
        If lblLocation.Text = String.Empty And txtItemID.Text = String.Empty Then
            MsgBox("Please start new request first and select location!", vbCritical)
            txtItemID.Clear()
            Panel4.Visible = False
        ElseIf lblLocation.Text = String.Empty Then
            MsgBox("Please start new request first and select location!", vbCritical)
            txtItemID.Clear()
            Panel4.Visible = False
        Else
            txtItemID.Text = dgItemList.CurrentRow.Cells(0).Value.ToString
            Panel4.Visible = False
            FlowLayoutPanel1.Size = New Size(725, 89)
        End If
    End Sub
End Class
