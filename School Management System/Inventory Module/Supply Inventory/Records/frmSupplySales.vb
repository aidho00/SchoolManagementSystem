﻿Imports MySql.Data.MySqlClient

Public Class frmSupplySales
    Dim _total As Double
    Dim startAmount As Double

    Sub loadSales()
        Try
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            _total = 0

            dgSales.Rows.Clear()
            Dim i As Integer
            cn.Open()
            cm = New MySqlCommand("select c.dno, c.dstatus, c.dtransno, c.dbarcode, p.description, '' as categoryname, '' as sizes, c.dprice, round(c.dqty) as dqty, c.ditem_price, c.ddate as 'transDate', u.AccountName as 'cashier' from cfcissmsdb_supply.tbl_supply_deployed as c inner join cfcissmsdb_supply.tbl_supply_item as p on c.dbarcode = p.barcodeid LEFT join useraccounts u on c.druser_id = u.useraccountID where c.dstatus = 'APPROVED' and c.ddate between '" & sdate1 & "' and '" & sdate2 & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                _total += CDbl(dr.Item("ditem_price").ToString)
                dgSales.Rows.Add(i, dr.Item("dno").ToString, dr.Item("dstatus").ToString, dr.Item("dtransno").ToString, dr.Item("dbarcode").ToString, dr.Item("description").ToString, dr.Item("categoryname").ToString, dr.Item("sizes").ToString, Format(CDbl(dr.Item("dprice").ToString), "#,##0.00"), CInt(dr.Item("dqty").ToString), Format(CDbl(dr.Item("ditem_price").ToString), "#,##0.00"), dr.Item("transDate").ToString, dr.Item("cashier").ToString)
            End While
            dr.Close()
            cn.Close()

            lblTotal.Text = "REQUESTS - SALES: " & Format(_total, "₱ #,##0.00")
        Catch ex As Exception
            dr.Close()
            cn.Close()
            MsgBox(ex.Message, vbCritical)

        End Try
    End Sub

    Private Sub frmSalesDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyHoverEffectToControls(Me)
        'dtFrom.Value = Now
        'dtTo.Value = Now
    End Sub

    Private Sub frmSalesDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged, dtTo.ValueChanged
        loadSales()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        frmMain.OpenForm(frmSupplySalesSummary, "Supply Sales Summary")
        frmMain.HideAllFormsInPanelExcept(frmSupplySalesSummary)
        frmSupplySalesSummary.loadDailySales()
        frmSupplySalesSummary.loadMonthlySales()
        frmSupplySalesSummary.loadQuarterlySales()
        frmSupplySalesSummary.loadYearlySales()
        frmMain.controlsPanel.Visible = False
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        frmMain.OpenForm(frmSupplyBestSelling, "Supply Best Requested Items")
        frmMain.HideAllFormsInPanelExcept(frmSupplyBestSelling)
        frmSupplyBestSelling.loadRecords()
        frmMain.controlsPanel.Visible = False
    End Sub
End Class