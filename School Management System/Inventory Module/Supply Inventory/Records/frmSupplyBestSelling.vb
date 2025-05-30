﻿Imports MySql.Data.MySqlClient

Public Class frmSupplyBestSelling
    Private Sub frmBestSelling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyHoverEffectToControls(Me)
        'dtFrom.Value = Now
        'dtTo.Value = Now
    End Sub

    Private Sub frmBestSelling_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Sub loadRecords()
        Try
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgBestSelling.Rows.Clear()
            Dim i As Integer
            cn.Open()
            cm = New MySqlCommand("Select distinct c.dbarcode, p.description, '' as categoryname, '' as sizes, c.dprice, ifnull(round(sum(c.dqty)),0) as qty, ifnull(sum(c.ditem_price),0) as total from cfcissmsdb_supply.tbl_supply_deployed as c inner join cfcissmsdb_supply.tbl_supply_item as p on c.dbarcode = p.barcodeid where c.dstatus = 'APPROVED' and c.ddate between '" & sdate1 & "' and '" & sdate2 & "' group by c.dbarcode, c.dprice order by qty desc, total desc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgBestSelling.Rows.Add(i, dr.Item("dbarcode").ToString, dr.Item("description").ToString, dr.Item("categoryname").ToString, dr.Item("sizes").ToString, Format(CDbl(dr.Item("dprice").ToString), "#,##0.00"), CInt(dr.Item("qty").ToString), Format(CDbl(dr.Item("total").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged, dtTo.ValueChanged
        loadRecords()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub
End Class