Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class frmSupplyPurchaseGReceipt


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

    Private isAlreadyValidated As Boolean = False


    Private Sub dgGRitemList_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles dgGRitemList.CellValidating
        Dim dataGridView As DataGridView = DirectCast(sender, DataGridView)
        ' Check if the column being edited is the numeric column
        If dataGridView.Columns(e.ColumnIndex).Name = "Column3" Then
            Dim newValue As String = e.FormattedValue.ToString()

            ' Check if the input is numeric
            If Not IsNumeric(newValue) Then
                ' Cancel the event
                e.Cancel = True
                ' Show the error message only if it hasn't been shown yet
                If Not isAlreadyValidated Then
                    isAlreadyValidated = True
                    MessageBox.Show("Please enter a valid quantity.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                ' Reset the flag if validation passes
                isAlreadyValidated = False
            End If
        ElseIf dataGridView.Columns(e.ColumnIndex).Name = "Column2" Then
            Dim currencyPattern As String = "^\d+(\.\d{1,2})?$"

            Dim newValue As String = e.FormattedValue.ToString()

            ' Check if the input matches the currency pattern
            If Not System.Text.RegularExpressions.Regex.IsMatch(newValue, currencyPattern) Then
                ' Cancel the event
                e.Cancel = True
                ' Show the error message only if it hasn't been shown yet
                If Not isAlreadyValidated Then
                    isAlreadyValidated = True
                    MessageBox.Show("Please enter a valid amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                ' Reset the flag if validation passes
                isAlreadyValidated = False
            End If
        End If
    End Sub

    Private Sub dgGRitemList_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgGRitemList.CellEndEdit

        'Dim remaining As Integer = 0
        'cn.Close()
        'cn.Open()
        'cm = New MySqlCommand("SELECT (`itemqty` - `receivedqty`) as 'Remaining' FROM `tbl_supply_purchaseorder_items` WHERE itemid = '" & row.Cells(0).Value & "' and pono = '" & lblPOno.Text & "'", cn)
        'remaining = cm.ExecuteScalar
        'cn.Close()

        If dgGRitemList.Columns(e.ColumnIndex).Name = "Column2" Or dgGRitemList.Columns(e.ColumnIndex).Name = "Column3" Then
            dgGRitemList.CurrentRow.Cells(6).Value = CDec(dgGRitemList.CurrentRow.Cells(4).Value) * CInt(dgGRitemList.CurrentRow.Cells(5).Value)
        End If

    End Sub



    Private Sub dgGRitemList_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgGRitemList.RowsAdded
        lblTotal.Text = Format(CDec(GetColumnSum(dgGRitemList, 6)), "#,##0.00")
    End Sub

    Private Sub dgGRitemList_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgGRitemList.RowsRemoved
        lblTotal.Text = Format(CDec(GetColumnSum(dgGRitemList, 6)), "#,##0.00")
    End Sub

    Private Sub dgGRitemList_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgGRitemList.CellValueChanged
        lblTotal.Text = Format(CDec(GetColumnSum(dgGRitemList, 6)), "#,##0.00")
    End Sub


    Function GetTransno() As String
        Dim yearid As String = YearToday
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT grno FROM tbl_supply_goodsreceipts WHERE grno like 'GR" & yearid & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(grno) as ID from tbl_supply_goodsreceipts", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            lastCode = lastCode.Remove(0, 6)
            GetTransno = "GR" & CInt(yearid & lastCode) + 1
        Else
            dr.Close()
            GetTransno = "GR" & yearid & "00001"
        End If
        cn.Close()

        Return GetTransno
    End Function

    Sub PurchaseOrderList()
        Try

            dgPOList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "Select pono, prno, pototal, po.status, DATE_FORMAT(podate, '%m/%d/%Y') as podate, AccountName, poremarks from tbl_supply_purchaseorder po JOIN useraccounts ua ON po.pouser_id = ua.useraccountID where prno LIKE '%" & frmMain.txtSearch.Text & "%'"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgPOList.Rows.Add(i, dr.Item("pono").ToString, dr.Item("prno").ToString, dr.Item("pototal").ToString, dr.Item("podate").ToString, dr.Item("status").ToString, dr.Item("AccountName").ToString, dr.Item("poremarks").ToString)
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgPOList.Rows.Clear()

        End Try
    End Sub

    Sub PurchaseOrderItems()
        Try

            dgGRitemList.Rows.Clear()
            Dim sql As String
            sql = "SELECT t1.`itemid`,t2.description, '' as cat,'' as size, t1.`itemqty`,t1.itemprice,t1.itemtotal FROM `tbl_supply_purchaseorder_items` t1 JOIN tbl_supply_item t2 ON t1.itemid = t2.barcodeid WHERE `pono` = '" & dgPOList.CurrentRow.Cells(1).Value & "'"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgGRitemList.Rows.Add(dr.Item("itemid").ToString, dr.Item("description").ToString, dr.Item("cat").ToString, dr.Item("size").ToString, dr.Item("itemprice").ToString, 0, "0.00")
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgGRitemList.Rows.Clear()

        End Try
    End Sub


    Private Sub btnSearchPO_Click(sender As Object, e As EventArgs) Handles btnSearchPO.Click
        SearchPanel.Visible = True
        PurchaseOrderList()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        PurchaseOrderList()
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim dr As DialogResult
        dr = MessageBox.Show("Are you sure you want to save this goods receipt?", "Notice!", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dr = DialogResult.No Then
        Else
            If dgGRitemList.RowCount = 0 Then
                MsgBox("There are no items in the list. Please add an item.", vbCritical)
                Return
            End If
            If lblPOno.Text = "" Or lblPOno.Text = "-" Then
                MsgBox("Please select a purchase order to create goods receipt.", vbCritical)
                Return
            End If
            If IS_EMPTY(txtRemarks) = True Then Return
            Dim GRNo As String = GetTransno()
            query("INSERT INTO `tbl_supply_goodsreceipts`(`grno`, `pono`, `pototal`, `grremarks`, `gruser_id`, grinvoice) VALUES ('" & GRNo & "', '" & lblPOno.Text & "'," & CDec(lblTotal.Text) & ",'" & txtRemarks.Text & "', " & str_userid & ", '" & txtInvoices.Text & "')")

            For Each row As DataGridViewRow In dgGRitemList.Rows
                query("INSERT INTO `tbl_supply_purchaseorder_items`(`grno`, `itemid`, `itemqty`, `itemprice`, `itemtotal`) VALUES ('" & GRNo & "','" & row.Cells(0).Value & "'," & CInt(row.Cells(5).Value) & "," & CInt(row.Cells(4).Value) & "," & CDec(row.Cells(6).Value) & ")")

                query("UPDATE tbl_supply_purchaseorder_items SET receivedqty = receivedqty + " & CInt(row.Cells(5).Value) & " WHERE itemid = '" & row.Cells(0).Value & "' and pono = '" & lblPOno.Text & "'")

            Next
            frmSupplyGRRecords.GoodsReceiptList()
            MsgBox("Goods Receipt sucessfully created.", vbInformation)
            GoodsReceiptRPT(GRNo)
            Me.Close()
        End If
    End Sub

    Sub GoodsReceiptRPT(grno As String)
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
        For Each dr As DataGridViewRow In dgGRitemList.Rows
            dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value)
        Next
        Dim iDate As String = DateToday
        Dim oDate As DateTime = Convert.ToDateTime(iDate)
        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New PurchaseOrder
        rptdoc.SetDataSource(dt)
        rptdoc.SetParameterValue("orderno", lblPOno.Text)
        rptdoc.SetParameterValue("requestdate", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
        rptdoc.SetParameterValue("greceiptno", grno)
        rptdoc.SetParameterValue("requestremarks", txtRemarks.Text)
        Dim input As String = str_name
        Dim cultureInfo As CultureInfo = CultureInfo.CurrentCulture
        Dim textInfo As TextInfo = cultureInfo.TextInfo
        Dim user_name As String = textInfo.ToTitleCase(input.ToLower())
        rptdoc.SetParameterValue("preparedby", user_name)
        frmReportViewer.ReportViewer.ReportSource = rptdoc
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        lblPOno.Text = dgPOList.CurrentRow.Cells(1).Value
        PurchaseOrderItems()
        SearchPanel.Visible = False
    End Sub
End Class
