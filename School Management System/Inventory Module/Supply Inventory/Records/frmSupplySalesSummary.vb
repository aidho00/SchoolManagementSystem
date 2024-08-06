Imports MySql.Data.MySqlClient

Public Class frmSupplySalesSummary

    Private Sub frmSalesSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyHoverEffectToControls(Me)
    End Sub
    Sub loadDate()
        Try
            cn.Open()
            cm = New MySqlCommand("select IFNULL(min(ddate),CURDATE()) as ddate from tbl_supply_deployed", cn)
            dr = cm.ExecuteReader
            While dr.Read
                dtFrom.Value = dr.Item("ddate")
            End While
            dr.Close()
            cn.Close()
            dtTo.Value = Now
        Catch ex As Exception
            'dtFrom.Value = Now
            dtTo.Value = Now
            cn.Close()
        End Try
    End Sub

    Private Sub frmSalesSummary_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged, dtTo.ValueChanged
        loadDailySales()
        loadMonthlySales()
        loadQuarterlySales()
        loadYearlySales()
    End Sub

    Sub loadYearlySales()
        Try
            cn.Close()
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgYearlySales.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select year(ddate) as year, sum(ditem_price) as total from tbl_supply_deployed where dstatus = 'APPROVED' and ddate between '" & sdate1 & "' and '" & sdate2 & "' group by year(ddate) order by year(ddate) desc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgYearlySales.Rows.Add(dr.Item("year").ToString, Format(CDbl(dr.Item("total").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Sub loadQuarterlySales()
        Try
            cn.Close()
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgQuarterlySales.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select year(ddate) as year, quarter(ddate) as quarter, sum(ditem_price) as total from tbl_supply_deployed where dstatus = 'APPROVED' and ddate between '" & sdate1 & "' and '" & sdate2 & "' group by year(ddate), quarter(ddate) order by year(ddate) desc, quarter(ddate) desc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgQuarterlySales.Rows.Add(dr.Item("year").ToString, dr.Item("quarter").ToString, Format(CDbl(dr.Item("total").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Sub loadMonthlySales()
        Try
            cn.Close()
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgMonthlySales.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select year(ddate) as year, DATE_FORMAT(ddate,'%M') as month, sum(ditem_price) as total from tbl_supply_deployed where dstatus = 'APPROVED' and ddate between '" & sdate1 & "' and '" & sdate2 & "' group by year(ddate), DATE_FORMAT(ddate,'%M') order by year(ddate) desc, DATE_FORMAT(ddate,'%M') desc", cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgMonthlySales.Rows.Add(dr.Item("year").ToString, dr.Item("month").ToString, Format(CDbl(dr.Item("total").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub

    Sub loadDailySales()
        Try
            cn.Close()
            Dim sdate1 As String = dtFrom.Value.ToString("yyyy-MM-dd")
            Dim sdate2 As String = dtTo.Value.ToString("yyyy-MM-dd")
            dgDailySales.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select year(ddate) as year, DATE_FORMAT(ddate,'%M') as month, DATE_FORMAT(ddate,'%d') as days, sum(ditem_price) as total from tbl_supply_deployed where dstatus = 'APPROVED' and ddate between '" & sdate1 & "' and '" & sdate2 & "' group by year(ddate), DATE_FORMAT(ddate,'%M'), DATE_FORMAT(ddate,'%d') order by year(ddate) desc, DATE_FORMAT(ddate,'%M') desc, DATE_FORMAT(ddate,'%d')", cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgDailySales.Rows.Add(dr.Item("year").ToString, dr.Item("month").ToString, dr.Item("days").ToString, Format(CDbl(dr.Item("total").ToString), "#,##0.00"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            cn.Close()
        End Try
    End Sub
End Class