Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class frmSupplyPurchaseRequest


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
        cm = New MySqlCommand("SELECT prno FROM tbl_supply_purchaserequest WHERE prno like 'PR" & yearid & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(prno) as ID from tbl_supply_purchaserequest", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            lastCode = lastCode.Remove(0, 6)
            GetTransno = "EN" & CInt(yearid & lastCode) + 1
        Else
            dr.Close()
            GetTransno = "EN" & yearid & "00001"
        End If
        cn.Close()

        Return GetTransno
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If dgPRitemList.RowCount = 0 Then
            MsgBox("There are no items in the list. Please add an item.", vbCritical)
            Return
        End If
        If IS_EMPTY(txtRemarks) = True Then Return
        Dim PRNo As String = GetTransno()
        query("INSERT INTO `tbl_supply_purchaserequest`(`prno`, `prtotal`, `prremarks`) VALUES ('" & PRNo & "'," & CDec(lblTotal.Text) & ",'" & txtRemarks.Text & "')")
        For Each row As DataGridViewRow In dgPRitemList.Rows
            query("INSERT INTO `tbl_supply_purchaserequest_items`(`prno`, `itemid`, `itemqty`, `itemprice`, `itemtotal`) VALUES ('" & PRNo & "'," & CInt(row.Cells(0).Value) & "," & CInt(row.Cells(5).Value) & "," & CInt(row.Cells(4).Value) & "," & CDec(row.Cells(6).Value) & ")")
        Next

        PurchaseRequestRPT(PRNo)
        MsgBox("Purchase Request sucessfully created.", vbInformation)

    End Sub

    Sub PurchaseRequestRPT(prno As String)
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
        For Each dr As DataGridViewRow In dgPRitemList.Rows
            dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(6).Value)
        Next
        Dim iDate As String = DateToday
        Dim oDate As DateTime = Convert.ToDateTime(iDate)
        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New Ack_Report
        rptdoc.SetDataSource(dt)
        'rptdoc.SetParameterValue("studentname", cmb_student.Text)
        rptdoc.SetParameterValue("requestdate", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
        rptdoc.SetParameterValue("requestno", prno)
        rptdoc.SetParameterValue("requestremarks", txtRemarks.Text)

        Dim input As String = str_name
        Dim cultureInfo As CultureInfo = CultureInfo.CurrentCulture
        Dim textInfo As TextInfo = cultureInfo.TextInfo
        Dim user_name As String = textInfo.ToTitleCase(input.ToLower())

        rptdoc.SetParameterValue("preparedby", user_name)
        frmReportViewer.ReportViewer.ReportSource = rptdoc
    End Sub

    Private Sub btnSearchItems_Click(sender As Object, e As EventArgs) Handles btnSearchItems.Click
        SearchPanel.Visible = True
        SupplyItemList()
    End Sub

    Sub SupplyItemList()
        dgSupplyItemList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "Select (BarcodeID) as 'Item ID', Description, (CategoryName) as 'Category', Sizes, item_price, (tbl_supply_inventory.Spare) as 'Stock' from tbl_supply_item JOIN tbl_supply_category ON tbl_supply_item.CategoryID = tbl_supply_category.catid JOIN tbl_supply_sizes ON tbl_supply_item.sizesid = tbl_supply_sizes.sizeid JOIN tbl_supply_inventory ON tbl_supply_item.barcodeid = tbl_supply_inventory.itembarcode JOIN tbl_supply_brand ON tbl_supply_item.brandid = tbl_supply_brand.brandid where tbl_supply_item.item_status = 'Available' and (BarcodeID LIKE '%" & txtSearch.Text & "%' or CategoryName LIKE '%" & txtSearch.Text & "%' or Description LIKE '%" & txtSearch.Text & "%' or Sizes LIKE '%" & txtSearch.Text & "%' or brandname LIKE '%" & txtSearch.Text & "%') order by tbl_supply_inventory.Spare asc"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgSupplyItemList.Rows.Add(i, dr.Item("Item ID").ToString, dr.Item("Description").ToString, dr.Item("Category").ToString, dr.Item("Sizes").ToString, dr.Item("item_price").ToString, dr.Item("Stock").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        SupplyItemList()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim isFound As Boolean = False
        For Each row As DataGridViewRow In dgPRitemList.Rows
            If row.Cells(0).Value = dgSupplyItemList.CurrentRow.Cells(1).Value Then
                isFound = True
                Exit For
            End If
        Next
        If isFound = False Then
            frmSupplyPurchaseQty.PurchasingStatus = "Purchase Request"
            frmSupplyPurchaseQty.ShowDialog()
        Else
            MsgBox("The item is already on the list. Please add a different item.", vbCritical)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            SearchPanel.Visible = False
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnCancelSearch_Click(sender As Object, e As EventArgs) Handles btnCancelSearch.Click
        SearchPanel.Visible = False
    End Sub

    Private Sub dgPRitemList_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgPRitemList.RowsAdded
        lblTotal.Text = Format(CDec(GetColumnSum(dgPRitemList, 6)), "#,##0.00")
    End Sub

    Private Sub dgPRitemList_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgPRitemList.RowsRemoved
        lblTotal.Text = Format(CDec(GetColumnSum(dgPRitemList, 6)), "#,##0.00")
    End Sub

    Private Sub dgPRitemList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPRitemList.CellContentClick
        Dim colname As String = dgPRitemList.Columns(e.ColumnIndex).Name
        If colname = "colRemove" Then
            dgPRitemList.Rows.Remove(dgPRitemList.CurrentRow)
        End If
    End Sub
End Class