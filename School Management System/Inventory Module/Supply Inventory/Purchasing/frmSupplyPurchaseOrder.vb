Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class frmSupplyPurchaseOrder


#Region "Drag Form"

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles systemSign.MouseDown, Panel1.MouseDown  ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles systemSign.MouseMove, Panel1.MouseMove  ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles systemSign.MouseUp, Panel1.MouseUp   ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

#End Region

    Function GetTransno() As String
        Dim yearid As String = YearToday
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT pono FROM tbl_supply_purchaseorder WHERE pono like 'PO" & yearid & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(pono) as ID from tbl_supply_purchaseorder", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            lastCode = lastCode.Remove(0, 6)
            GetTransno = "PO" & CInt(yearid & lastCode) + 1
        Else
            dr.Close()
            GetTransno = "PO" & yearid & "00001"
        End If
        cn.Close()

        Return GetTransno
    End Function

    Private Sub btnSearchPR_Click(sender As Object, e As EventArgs) Handles btnSearchPR.Click
        SearchPanel.Visible = True
        PurchaseRequestList()
    End Sub

    Sub PurchaseRequestList()
        Try

            dgPRList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "Select prno, prtotal, status, DATE_FORMAT(prdate, '%m/%d/%Y') as prdate, AccountName, prremarks from tbl_supply_purchaserequest pr JOIN useraccounts ua ON pr.pruser_id = ua.useraccountID where prno LIKE '%" & frmMain.txtSearch.Text & "%' and status NOT IN ('Close')"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgPRList.Rows.Add(i, dr.Item("prno").ToString, dr.Item("prtotal").ToString, dr.Item("prdate").ToString, dr.Item("status").ToString, dr.Item("AccountName").ToString, dr.Item("prremarks").ToString)
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgPRList.Rows.Clear()

        End Try
    End Sub

    Sub PurchaseRequestItems()
        Try

            dgPOitemList.Rows.Clear()
        Dim sql As String
        sql = "SELECT t1.`itemid`,t2.description, '' as cat,'' as size, t1.`itemqty`,t1.itemprice,t1.itemtotal FROM `tbl_supply_purchaserequest_items` t1 JOIN tbl_supply_item t2 ON t1.itemid = t2.barcodeid WHERE `prno` = '" & dgPRList.CurrentRow.Cells(1).Value & "'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgPOitemList.Rows.Add(dr.Item("itemid").ToString, dr.Item("description").ToString, dr.Item("cat").ToString, dr.Item("size").ToString, dr.Item("itemprice").ToString, dr.Item("itemqty").ToString, dr.Item("itemtotal").ToString)
        End While
        dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgPOitemList.Rows.Clear()

        End Try
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        lblPRno.Text = dgPRList.CurrentRow.Cells(1).Value
        PurchaseRequestItems()
        SearchPanel.Visible = False
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        PurchaseRequestList()
    End Sub


    Private Sub dgPOitemList_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgPOitemList.RowsAdded
        lblTotal.Text = Format(CDec(GetColumnSum(dgPOitemList, 6)), "#,##0.00")
    End Sub

    Private Sub dgPOitemList_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgPOitemList.RowsRemoved
        lblTotal.Text = Format(CDec(GetColumnSum(dgPOitemList, 6)), "#,##0.00")
    End Sub

    Private Sub dgPOitemList_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgPOitemList.CellValueChanged
        lblTotal.Text = Format(CDec(GetColumnSum(dgPOitemList, 6)), "#,##0.00")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim dr As DialogResult
        dr = MessageBox.Show("Are you sure you want to save this purchase order?", "Notice!", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dr = DialogResult.No Then
        Else
            If dgPOitemList.RowCount = 0 Then
                MsgBox("There are no items in the list. Please add an item.", vbCritical)
                Return
            End If
            If lblPRno.Text = "" Or lblPRno.Text = "-" Then
                MsgBox("Please select a purchase request to create a purchase order.", vbCritical)
                Return
            End If
            If IS_EMPTY(txtRemarks) = True Then Return
            Dim PONo As String = GetTransno()
            query("INSERT INTO `tbl_supply_purchaseorder`(`pono`, `prno`, `pototal`, `poremarks`, `pouser_id`) VALUES ('" & PONo & "', '" & lblPRno.Text & "'," & CDec(lblTotal.Text) & ",'" & txtRemarks.Text & "', " & str_userid & ")")
            For Each row As DataGridViewRow In dgPOitemList.Rows
                query("INSERT INTO `tbl_supply_purchaseorder_items`(`pono`, `itemid`, `itemqty`, `itemprice`, `itemtotal`) VALUES ('" & PONo & "','" & row.Cells(0).Value & "'," & CInt(row.Cells(5).Value) & "," & CInt(row.Cells(4).Value) & "," & CDec(row.Cells(6).Value) & ")")
            Next
            query("UPDATE `tbl_supply_purchaserequest` SET `status` = 'Close' where `prno` = '" & lblPRno.Text & "'")

            frmSupplyPORecords.PurchaseOrderList()
            MsgBox("Purchase Order sucessfully created.", vbInformation)
            PurchaseOrderRPT(PONo)
            Me.Close()
        End If
    End Sub

    Sub PurchaseOrderRPT(pono As String)
        frmReportViewer.Show()
        Dim dt As New DataTable
        With dt
            .Columns.Add("barcodeid")
            .Columns.Add("description")
            .Columns.Add("categoryname")
            .Columns.Add("sizes")
            .Columns.Add("dprice")
            .Columns.Add("dqty")
            .Columns.Add("ditemprice")
        End With
        For Each dr As DataGridViewRow In dgPOitemList.Rows
            dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value)
        Next
        Dim iDate As String = DateToday
        Dim oDate As DateTime = Convert.ToDateTime(iDate)
        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New PurchaseOrder
        rptdoc.SetDataSource(dt)
        rptdoc.SetParameterValue("orderno", pono)
        rptdoc.SetParameterValue("requestdate", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
        rptdoc.SetParameterValue("requestno", lblPRno.Text)
        rptdoc.SetParameterValue("requestremarks", txtRemarks.Text)
        Dim input As String = str_name
        Dim cultureInfo As CultureInfo = cultureInfo.CurrentCulture
        Dim textInfo As TextInfo = cultureInfo.TextInfo
        Dim user_name As String = textInfo.ToTitleCase(input.ToLower())
        rptdoc.SetParameterValue("preparedby", user_name)
        frmReportViewer.ReportViewer.ReportSource = rptdoc
    End Sub
End Class