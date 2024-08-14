Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Public Class frmSupplyRecords
    Dim dt As DataTable

    Private Sub DeployedRecords_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub DeployedRecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
        'dtFrom.Value = Now.ToString("yyyy-MM-dd")
        'dtTo.Value = Now.ToString("yyyy-MM-dd")
        fillCombo("SELECT Locationname, locationnumber from tbl_supply_location", txtboxlocation, "tbl_supply_location", "Locationname", "locationnumber")
        'inventoryshow()

        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub inventoryshow()
        Try


        Catch ex As Exception
        End Try
    End Sub

    Private Sub deployedlist()
        Try
            Dim dt As New DataTable
            With dt
                .Columns.Add("ItemID")
                .Columns.Add("Description")
                .Columns.Add("Category")
                .Columns.Add("Price")
                .Columns.Add("Quantity")
                .Columns.Add("Total")
                .Columns.Add("Location")
                .Columns.Add("DeployDate")
            End With

            For Each dr As DataGridViewRow In Me.dgdeployrecords.Rows
                dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value)
            Next
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New DeployedReport
            With rptdoc
                .SetDataSource(dt)
                .SetParameterValue("office_location", txtboxlocation.Text)
            End With
            CrystalReportViewer.ReportSource = rptdoc
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btn_cancel_login_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If txtboxlocation.Text = "Student" Then
            lblReportRequestID.Text = "-"
            dgdeployrecords.Visible = True
            CrystalReportViewer.Visible = False
            Label4.Visible = True
            lblTotal.Visible = True

            Dim x As Integer = CInt(txtboxlocation.SelectedValue)
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")

            Dim dt As DataTable

            If ComboBox1.Text = "Both College - High School" Then
                load_datagrid("SELECT t1.additional_stud_id as 'Student ID', CONCAT(t2.s_ln,', ', t2.s_fn,' ', t2.s_mn) as 'Student Name', t4.categoryname as 'Item', t3.description as 'Description', t1.additional_qty as 'QTY', t1.additional_price as 'Price', t1.additional_amount as 'Amount', DATE_FORMAT(t1.additional_date_added, '%m-%d-%Y') as 'Date Added' from tbl_assessment_additional t1 JOIN tbl_student t2 ON t1.additional_stud_id = t2.s_id_no JOIN tbl_supply_item t3 ON t1.additional_item_id = t3.barcodeid LEFT JOIN tbl_supply_category t4 ON t3.categoryid = t4.catID where t1.additional_date_added between '" & sdate1 & "' and '" & sdate2 & "' UNION ALL SELECT t1.additional_stud_id as 'Student ID', CONCAT(t2.s_ln,', ', t2.s_fn,' ', t2.s_mn, ' - ', t2.s_yr_lvl) as 'Student Name', t4.categoryname as 'Item', t3.description as 'Description', t1.additional_qty as 'QTY', t1.additional_price as 'Price', t1.additional_amount as 'Amount', DATE_FORMAT(t1.additional_date_added, '%m-%d-%Y') as 'Date Added' from cfcissmsdbhighschool.tbl_assessment_additional t1 JOIN cfcissmsdbhighschool.tbl_student t2 ON t1.additional_stud_id = t2.s_id_no JOIN tbl_supply_item t3 ON t1.additional_item_id = t3.barcodeid LEFT JOIN tbl_supply_category t4 ON t3.categoryid = t4.catID where t1.additional_date_added between '" & sdate1 & "' and '" & sdate2 & "'", dgdeployrecords)
            ElseIf ComboBox1.Text = "College" Then
                load_datagrid("SELECT t1.additional_stud_id as 'Student ID', CONCAT(t2.s_ln,', ', t2.s_fn,' ', t2.s_mn) as 'Student Name', t4.categoryname as 'Item', t3.description as 'Description', t1.additional_qty as 'QTY', t1.additional_price as 'Price', t1.additional_amount as 'Amount', DATE_FORMAT(t1.additional_date_added, '%m-%d-%Y') as 'Date Added' from tbl_assessment_additional t1 JOIN tbl_student t2 ON t1.additional_stud_id = t2.s_id_no JOIN tbl_supply_item t3 ON t1.additional_item_id = t3.barcodeid LEFT JOIN tbl_supply_category t4 ON t3.categoryid = t4.catID where t1.additional_date_added between '" & sdate1 & "' and '" & sdate2 & "'", dgdeployrecords)
            ElseIf ComboBox1.Text = "High School" Then
                load_datagrid("SELECT t1.additional_stud_id as 'Student ID', CONCAT(t2.s_ln,', ', t2.s_fn,' ', t2.s_mn, ' - ', t2.s_yr_lvl) as 'Student Name', t4.categoryname as 'Item', t3.description as 'Description', t1.additional_qty as 'QTY', t1.additional_price as 'Price', t1.additional_amount as 'Amount', DATE_FORMAT(t1.additional_date_added, '%m-%d-%Y') as 'Date Added' from cfcissmsdbhighschool.tbl_assessment_additional t1 JOIN cfcissmsdbhighschool.tbl_student t2 ON t1.additional_stud_id = t2.s_id_no JOIN tbl_supply_item t3 ON t1.additional_item_id = t3.barcodeid LEFT JOIN tbl_supply_category t4 ON t3.categoryid = t4.catID where t1.additional_date_added between '" & sdate1 & "' and '" & sdate2 & "'", dgdeployrecords)
            End If


            Try
                If dgdeployrecords.RowCount = 0 Then
                    lblTotal.Text = "0.00"
                Else
                    lblTotal.Text = Format(total3, "n2")
                End If
            Catch ex As Exception
            End Try

        Else

            dgdeployrecords.Visible = True
            CrystalReportViewer.Visible = False
            Label4.Visible = True
            lblTotal.Visible = True

            Dim x As Integer = CInt(txtboxlocation.SelectedValue)
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")

            If cb_as.Checked = True Then
                'Try
                lblReportRequestID.Text = "-"
                load_datagrid("Select dstudentid, (barcodeid) as `Item ID`, Description, (categoryname) as Item, (Sizes) as Size, (dprice) as Price, (dqty) as QTY, (qty_requested) as RQTY, ditem_price as 'Total', (ddate) as Date from tbl_supply_deployed, tbl_supply_item, tbl_supply_category, tbl_supply_location, tbl_supply_sizes where tbl_supply_deployed.dbarcode = tbl_supply_item.barcodeid AND tbl_supply_item.categoryid = tbl_supply_category.catid and tbl_supply_deployed.dlocation = tbl_supply_location.locationnumber and tbl_supply_sizes.sizeid = tbl_supply_item.sizesid and dlocation = " & x & " and ddate between '" & sdate1 & "' and '" & sdate2 & "' and dstatus = 'APPROVED' order by Description", dgdeployrecords)

                dgdeployrecords.Columns(0).Visible = False
                dgdeployrecords.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                dgdeployrecords.Columns(3).Visible = False
                dgdeployrecords.Columns(4).Visible = False
                dgdeployrecords.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(9).Visible = False

                Try
                    If dgdeployrecords.RowCount = 0 Then
                        lblTotal.Text = "0.00"
                    Else
                        lblTotal.Text = Format(total2, "n2")
                    End If
                Catch ex As Exception
                End Try

            ElseIf cb_aps.Checked = True Then
                'Try
                lblReportRequestID.Text = "-"
                load_datagrid("Select dstudentid, (barcodeid) as `Item ID`, Description, (categoryname) as Item, (Sizes) as Size, (item_price) as Price, SUM(dqty) as QTY,  SUM(qty_requested) as RQTY,  SUM(ditem_price) as 'Total', (ddate) as Date, locationname as Location from tbl_supply_deployed, tbl_supply_item, tbl_supply_category, tbl_supply_location, tbl_supply_sizes where tbl_supply_deployed.dbarcode = tbl_supply_item.barcodeid AND tbl_supply_item.categoryid = tbl_supply_category.catid and tbl_supply_deployed.dlocation = tbl_supply_location.locationnumber and tbl_supply_sizes.sizeid = tbl_supply_item.sizesid and ddate between '" & sdate1 & "' and '" & sdate2 & "' and dstatus = 'APPROVED' group by barcodeid, dlocation order by Description", dgdeployrecords)


                dgdeployrecords.Columns(0).Visible = False
                dgdeployrecords.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                dgdeployrecords.Columns(3).Visible = False
                dgdeployrecords.Columns(4).Visible = False
                dgdeployrecords.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(9).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                Try
                    If dgdeployrecords.RowCount = 0 Then
                        lblTotal.Text = "0.00"
                    Else
                        lblTotal.Text = Format(total2, "n2")
                    End If
                Catch ex As Exception
                End Try
            Else
                lblReportRequestID.Text = cbRequests.Text


                load_datagrid("Select (barcodeid) as 'Item ID', Description, (categoryname) as Category, (Sizes) as Size, (dqty) as QTY, (qty_requested) as RQTY, (item_price) as 'Price', ditem_price as 'Sub-Total', (ddate) as Date, (dstatus) as Status from tbl_supply_deployed, tbl_supply_item, tbl_supply_category, tbl_supply_location, tbl_supply_sizes where tbl_supply_deployed.dbarcode = tbl_supply_item.barcodeid AND tbl_supply_item.categoryid = tbl_supply_category.catid and tbl_supply_deployed.dlocation = tbl_supply_location.locationnumber and tbl_supply_sizes.sizeid = tbl_supply_item.sizesid AND dstudentid = '" & cbRequests.Text & "'", dgdeployrecords)

                dgdeployrecords.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                dgdeployrecords.Columns(2).Visible = False
                dgdeployrecords.Columns(3).Visible = False
                dgdeployrecords.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgdeployrecords.Columns(8).Visible = False
                dgdeployrecords.Columns(9).Visible = False
                dgdeployrecords.Columns(2).Visible = True

                Try
                    If dgdeployrecords.RowCount = 0 Then
                        lblTotal.Text = "0.00"
                    Else
                        lblTotal.Text = Format(total1, "n2")
                    End If
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If CrystalReportViewer.ReportSource Is Nothing Then
                MsgBox("No report is loaded in the Report Viewer.", vbCritical)
                Return
            End If
            CrystalReportViewer.PrintReport()
        Catch ex As Exception
            ' Handle any exceptions
            MsgBox("Error printing report: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btn_return_Click(sender As Object, e As EventArgs) Handles btn_return.Click
        With frmSupplyReturn
            .loadCart2()
            .ShowDialog()
        End With
    End Sub


    Private Function total1() As Double
        Dim tot As Double = 0
        Dim i As Integer = 0
        For i = 0 To dgdeployrecords.Rows.Count - 1
            tot = tot + Convert.ToDouble(dgdeployrecords.Rows(i).Cells(7).Value)
        Next i
        Return tot
    End Function

    Private Function total2() As Double
        Dim tot As Double = 0
        Dim i As Integer = 0
        For i = 0 To dgdeployrecords.Rows.Count - 1
            tot = tot + Convert.ToDouble(dgdeployrecords.Rows(i).Cells(8).Value)
        Next i
        Return tot
    End Function

    Private Function total3() As Double
        Dim tot As Double = 0
        Dim i As Integer = 0
        For i = 0 To dgdeployrecords.Rows.Count - 1
            tot = tot + Convert.ToDouble(dgdeployrecords.Rows(i).Cells(6).Value)
        Next i
        Return tot
    End Function


    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If txtboxlocation.Text = "Student" Then
            If dgdeployrecords.RowCount = 0 Then
            Else
                dgdeployrecords.Visible = False
                CrystalReportViewer.Visible = True
                Label4.Visible = False
                lblTotal.Visible = False

                Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
                Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")

                Dim dt2 As New DataTable
                With dt2
                    .Columns.Add("request_id")
                    .Columns.Add("barcodeid")
                    .Columns.Add("description")
                    .Columns.Add("categoryname")
                    .Columns.Add("dqty")
                    .Columns.Add("itemprice")
                    .Columns.Add("sizes")

                End With
                For Each dvr As DataGridViewRow In dgdeployrecords.Rows
                    dt2.Rows.Add(dvr.Cells(0).Value, dvr.Cells(1).Value, dvr.Cells(2).Value, dvr.Cells(3).Value, dvr.Cells(4).Value, dvr.Cells(5).Value, dvr.Cells(7).Value)
                Next
                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New Supply_Student
                rptdoc.SetDataSource(dt2)
                'rptdoc.SetParameterValue("preparedby", frm_main_v2.txtbox_account_name.Text)

                CrystalReportViewer.ReportSource = rptdoc



            End If

        Else



            If dgdeployrecords.RowCount = 0 Then
            Else
                dgdeployrecords.Visible = False
                CrystalReportViewer.Visible = True
                Label4.Visible = False
                lblTotal.Visible = False

                Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
                Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")

                If cb_as.Checked = True Then
                    'Try
                    Dim dt2 As New DataTable
                    With dt2
                        .Columns.Add("request_id")
                        .Columns.Add("barcodeid")
                        .Columns.Add("description")
                        .Columns.Add("categoryname")
                        .Columns.Add("sizes")
                        .Columns.Add("itemprice")
                        .Columns.Add("dqty")
                        .Columns.Add("ditemprice")
                    End With
                    For Each dvr As DataGridViewRow In dgdeployrecords.Rows
                        dt2.Rows.Add(dvr.Cells(0).Value, dvr.Cells(1).Value, dvr.Cells(2).Value, dvr.Cells(3).Value, dvr.Cells(4).Value, dvr.Cells(5).Value, dvr.Cells(6).Value, dvr.Cells(8).Value)
                    Next
                    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rptdoc = New Supply_AllRequest_2
                    rptdoc.SetDataSource(dt2)
                    rptdoc.SetParameterValue("preparedby", str_name)
                    rptdoc.SetParameterValue("requestoffice", txtboxlocation.Text)
                    rptdoc.SetParameterValue("requestdate", Format(Convert.ToDateTime(dtFrom.Text), "MMMM d, yyyy") & " - " & Format(Convert.ToDateTime(dtTo.Text), "MMMM d, yyyy"))

                    CrystalReportViewer.ReportSource = rptdoc
                    'Catch ex As Exception
                    '    MessageBox.Show("No Crystal Report found. Please install crystal report from application directory.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'End Try

                ElseIf cb_aps.Checked = True Then
                    'Try

                    Dim dt2 As New DataTable
                    With dt2
                        .Columns.Add("request_id")
                        .Columns.Add("barcodeid")
                        .Columns.Add("description")
                        .Columns.Add("categoryname")
                        .Columns.Add("sizes")
                        .Columns.Add("itemprice")
                        .Columns.Add("dqty")
                        .Columns.Add("ditemprice")
                        .Columns.Add("locationname")
                    End With
                    For Each dvr As DataGridViewRow In dgdeployrecords.Rows
                        dt2.Rows.Add(dvr.Cells(0).Value, dvr.Cells(1).Value, dvr.Cells(2).Value, dvr.Cells(3).Value, dvr.Cells(4).Value, dvr.Cells(5).Value, dvr.Cells(6).Value, dvr.Cells(8).Value, dvr.Cells(10).Value)
                    Next
                    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rptdoc = New Supply_AllRequestPerOffices
                    rptdoc.SetDataSource(dt2)
                    rptdoc.SetParameterValue("preparedby", str_name)
                    rptdoc.SetParameterValue("requestoffice", "All Offices")
                    rptdoc.SetParameterValue("requestdate", Format(Convert.ToDateTime(dtFrom.Text), "MMMM d, yyyy") & " - " & Format(Convert.ToDateTime(dtTo.Text), "MMMM d, yyyy"))

                    CrystalReportViewer.ReportSource = rptdoc
                    'Catch ex As Exception
                    '    MessageBox.Show("No Crystal Report found. Please install crystal report from application directory.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'End Try
                Else
                    If lbl_status.Text = "APPROVED" Then
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
                        For Each dvr As DataGridViewRow In Me.dgdeployrecords.Rows
                            dt.Rows.Add(dvr.Cells(0).Value, dvr.Cells(1).Value, dvr.Cells(2).Value, dvr.Cells(3).Value, dvr.Cells(4).Value, dvr.Cells(6).Value, dvr.Cells(7).Value)
                        Next
                        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        rptdoc = New Supply_Request
                        rptdoc.SetDataSource(dt)
                        rptdoc.SetParameterValue("preparedby", str_name)
                        rptdoc.SetParameterValue("requestoffice", txtboxlocation.Text)
                        rptdoc.SetParameterValue("requestdate", Format(Convert.ToDateTime(dgdeployrecords.Rows(0).Cells(8).Value), "MMMM d, yyyy"))
                        rptdoc.SetParameterValue("requestid", cbRequests.Text)
                        rptdoc.SetParameterValue("requeststatus", lbl_status.Text)
                        CrystalReportViewer.ReportSource = rptdoc
                    Else
                        MessageBox.Show("The request is not yet approved. Unable to print.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged, dtTo.ValueChanged
        'dgdeployrecords.DataSource = Nothing
        Dim x As Integer = CInt(txtboxlocation.SelectedValue)
        Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
        Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
        fillCombo("select distinct(dtransno) as ID, dstatus from tbl_supply_deployed where ddate between '" & dtFrom.Text & "' and '" & dtTo.Text & "' and dlocation = " & x & " order by dstudentid desc", cbRequests, "tbl_supply_deployed", "ID", "dstatus")
    End Sub

    Private Sub cbRequests_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRequests.SelectedIndexChanged, ComboBox1.SelectedIndexChanged
        'dgdeployrecords.DataSource = Nothing
        lbl_status.Text = cbRequests.SelectedValue
        If lbl_status.Text = "APPROVED" Or lbl_status.Text = "CANCELLED" Then
            btn_return.Enabled = True
            btn_return.Visible = True
        ElseIf lbl_status.Text = "PENDING" Then
            btn_return.Enabled = False
            btn_return.Visible = False
        End If
    End Sub

    Private Sub cb_as_Click(sender As Object, e As EventArgs) Handles cb_as.Click
        Try
            If cb_aps.Checked = True Then
                cb_aps.Checked = False
                cb_as.Checked = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cb_aps_Click(sender As Object, e As EventArgs) Handles cb_aps.Click
        Try
            If cb_as.Checked = True Then
                cb_as.Checked = False
                cb_aps.Checked = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtboxlocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtboxlocation.SelectedIndexChanged
        Try
            Dim x As Integer = CInt(txtboxlocation.SelectedValue)
            fillCombo("select distinct(dstudentid) as ID, dstatus from tbl_supply_deployed where ddate between '" & dtFrom.Text & "' and '" & dtTo.Text & "' and dlocation = " & x & " order by dstudentid desc", cbRequests, "tbl_supply_deployed", "ID", "dstatus")
        Catch ex As Exception
        End Try
        If txtboxlocation.Text = "Student" Then
            Label5.Visible = True
            ComboBox1.Visible = True
            cb_as.Visible = False
            cb_aps.Visible = False
        Else
            Label5.Visible = False
            ComboBox1.Visible = False
            cb_as.Visible = True
            cb_aps.Visible = True
        End If
    End Sub

    Private Sub lbl_status_TextChanged(sender As Object, e As EventArgs) Handles lbl_status.TextChanged
        If lbl_status.Text = "APPROVED" Then
            btn_return.Visible = True
        ElseIf lbl_status.Text = "PENDING" Or lbl_status.Text = "CANCELLED" Then
            btn_return.Visible = False
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnPrinterSetup_Click(sender As Object, e As EventArgs) Handles btnPrinterSetup.Click
        Try
            ' Check if the CrystalReportViewer has a ReportSource
            If CrystalReportViewer.ReportSource Is Nothing Then
                MsgBox("No report is loaded in the Report Viewer.", vbCritical)
                Return
            End If
            ' Create a PrintDocument
            Dim printDoc As New Printing.PrintDocument()
            ' Set the ReportDocument as the PrintDocument's Document
            Dim reportDocument As ReportDocument = TryCast(CrystalReportViewer.ReportSource, ReportDocument)
            printDoc.DocumentName = reportDocument.Database.Tables(0).Name
            ' Create a PrintDialog
            Dim printDialog As New PrintDialog()
            printDialog.Document = printDoc
            ' Show the printer setup dialog
            If printDialog.ShowDialog() = DialogResult.OK Then
                ' User clicked OK in the printer setup dialog
                ' Set the printer settings
                reportDocument.PrintOptions.PrinterName = printDoc.PrinterSettings.PrinterName
                ' Print the report
                reportDocument.PrintToPrinter(printDoc.PrinterSettings, printDoc.PrinterSettings.DefaultPageSettings, False)
            Else
                ' User clicked Cancel in the printer setup dialog
                MsgBox("Printing cancelled.", vbInformation)
            End If
        Catch ex As Exception
            ' Handle any exceptions
            MsgBox("Error printing report: " & ex.Message, vbCritical)
        End Try
    End Sub
End Class