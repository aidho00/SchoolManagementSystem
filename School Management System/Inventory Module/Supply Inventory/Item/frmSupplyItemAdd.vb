Imports MySql.Data.MySqlClient
Imports IDAutomation.Windows.Forms.LinearBarCode

Public Class frmSupplyItemAdd

    Dim Barcode As String = ""

    Public CategoryID As Integer = 0
    Public SizeID As Integer = 0

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

    Private Sub frmSupplyItemAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)

    End Sub

    Private Sub barcodeID_TextChanged(sender As Object, e As EventArgs) Handles barcodeID.TextChanged
        Dim NewBarcode As IDAutomation.Windows.Forms.LinearBarCode.Barcode = New Barcode()
        NewBarcode.DataToEncode = barcodeID.Text.ToString() 'Input of textbox to generate barcode 
        NewBarcode.SymbologyID = Symbologies.Code39
        NewBarcode.Code128Set = Code128CharacterSets.A
        NewBarcode.RotationAngle = RotationAngles.Zero_Degrees
        NewBarcode.RefreshImage()
        NewBarcode.Resolution = Resolutions.Screen
        NewBarcode.ResolutionCustomDPI = 100
        NewBarcode.RefreshImage()
        NewBarcode.Resolution = Resolutions.Printer
        barcodeIMG.Image = NewBarcode.BMPPicture
    End Sub


    Sub AutoBarCode()
        Dim yearid As String = YearToday
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT barcodeid FROM tbl_supply_item WHERE barcodeid like '" & yearid & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(barcodeid) as Barcode from tbl_supply_item", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            If lastCode.ToString.Length = 13 Then
                Barcode = yearid & "0000001"
            Else
                lastCode = lastCode.Remove(0, 4)
                Barcode = CInt(yearid & lastCode) + 1
            End If
        Else
            dr.Close()
            Barcode = yearid & "0000001"
        End If
        cn.Close()
        barcodeID.Text = Barcode
    End Sub

    Private Sub txtPrice_TextChanged(sender As Object, e As EventArgs) Handles txtPrice.TextChanged

    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
          AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            'cancel keys
            e.Handled = True
        End If
    End Sub

    Private Sub txtReOrderPoint_TextChanged(sender As Object, e As EventArgs) Handles txtReOrderPoint.TextChanged

    End Sub

    Private Sub txtReOrderPoint_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReOrderPoint.KeyPress, txtOpeningStock.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
          AndAlso e.KeyChar <> ControlChars.Back Then
            'cancel keys
            e.Handled = True
        End If
    End Sub

    Sub SupplyCategoryList()
        dgSupplyCategory.Rows.Clear()
        Dim sql As String
        sql = "select (catid) as 'ID', (categoryname) as 'Desc' from tbl_supply_category where categoryname like '%" & txtSearch.Text & "%' and categorytype = '" & cbSupplyType.Text & "'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgSupplyCategory.Rows.Add(dr.Item("ID").ToString, dr.Item("Desc").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub


    Private Sub btnSearchCategory_Click(sender As Object, e As EventArgs) Handles btnSearchCategory.Click
        If cbSupplyType.Text = String.Empty Then
        Else
            SearchPanel.Visible = True
            dgSupplyCategory.BringToFront()
            frmTitle.Text = "Search Category"
            SupplyCategoryList()
        End If
    End Sub

    Sub SupplyCategorySizeList()
        dgSupplySize.Rows.Clear()
        Dim sql As String
        sql = "select (sizeid) as 'ID', (sizes) as 'Desc' from tbl_supply_sizes where sizes like '%" & txtSearch.Text & "%' and category_id = " & CategoryID & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgSupplySize.Rows.Add(dr.Item("ID").ToString, dr.Item("Desc").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub btnSearchSize_Click(sender As Object, e As EventArgs) Handles btnSearchSize.Click
        If cbSupplyCategory.Text = String.Empty Then
        Else
            SearchPanel.Visible = True
            dgSupplySize.BringToFront()
            frmTitle.Text = "Search Size"
            SupplyCategorySizeList()
        End If
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If frmTitle.Text = "Search Category" Then
            CategoryID = dgSupplyCategory.CurrentRow.Cells(0).Value
            cbSupplyCategory.Text = dgSupplyCategory.CurrentRow.Cells(1).Value
        ElseIf frmTitle.Text = "Search Size" Then
            SizeID = dgSupplySize.CurrentRow.Cells(0).Value
            cbSupplySize.Text = dgSupplySize.CurrentRow.Cells(1).Value
        End If
        SearchPanel.Visible = False
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If frmTitle.Text = "Search Category" Then
            ResetControls(frmSupplyCategoryAdd)
            frmSupplyCategoryAdd.ShowDialog()
        ElseIf frmTitle.Text = "Search Size" Then
            ResetControls(frmSupplySizeAdd)
            frmSupplySizeAdd.ShowDialog()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            SearchPanel.Visible = False
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(cbSupplyType) = True Then Return
        If IS_EMPTY(cbSupplyCategory) = True Then Return
        If IS_EMPTY(cbSupplySize) = True Then Return
        If IS_EMPTY(txtSupplyDesc) = True Then Return
        If IS_EMPTY(txtOpeningStock) = True Then Return
        If IS_EMPTY(txtReOrderPoint) = True Then Return
        If IS_EMPTY(txtPrice) = True Then Return
        If CHECK_EXISTING("SELECT * FROM tbl_supply_item WHERE supply_type = '" & cbSupplyType.Text & "' and categoryid = " & CategoryID & " and sizesid = " & SizeID & "") = True Then Return
        AutoBarCode()
        query("INSERT INTO `tbl_supply_item`(`barcodeid`, `description`, `categoryid`, `sizesid`, `item_price`, `item_status`, `item_open_stock`, `item_reorder_point`) VALUES ('" & barcodeID.Text & "', '" & txtSupplyDesc.Text & "', " & CategoryID & ", " & SizeID & ", " & CDec(txtPrice.Text) & ", '" & cbSupplyStatus.Text & "', " & CInt(txtOpeningStock.Text) & " , " & CInt(txtReOrderPoint.Text) & "")
        query("INSERT INTO `tbl_supply_inventory`(`itembarcode`, `Spare`, `Deployed`, `Defect`) VALUES ('" & barcodeID.Text & "', " & CInt(txtOpeningStock.Text) & ", 0, 0)")

        'Stock Ledger
        StockLedger(barcodeID.Text, CInt(txtOpeningStock.Text), 0, "-", "Opening Stock", "-")

        UserActivity("Added a(n) " & cbSupplyType.Text & " supply item " & txtSupplyDesc.Text.Trim & " - " & cbSupplyCategory.Text.Trim & " " & cbSupplySize.Text.Trim & ". Barcode: " & barcodeID.Text & "", "SUPPLY ITEM ENTRY")
        frmWait.seconds = 1
        frmWait.ShowDialog()
        MsgBox("New supply item has been successfully added.", vbInformation, "")
        frmSupplyItems.SupplyItemList()
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If IS_EMPTY(cbSupplyType) = True Then Return
        If IS_EMPTY(cbSupplyCategory) = True Then Return
        If IS_EMPTY(cbSupplySize) = True Then Return
        If IS_EMPTY(txtSupplyDesc) = True Then Return
        If IS_EMPTY(txtOpeningStock) = True Then Return
        If IS_EMPTY(txtReOrderPoint) = True Then Return
        If IS_EMPTY(txtPrice) = True Then Return
        If CHECK_EXISTING("SELECT * FROM tbl_supply_item WHERE supply_type = '" & cbSupplyType.Text & "' and categoryid = " & CategoryID & " and sizesid = " & SizeID & " and barcodeid NOT IN ('" & barcodeID.Text & "')") = True Then Return
        query("UPDATE `tbl_supply_item` set `description` = '" & txtSupplyDesc.Text & "', `categoryid` = " & CategoryID & ", `sizesid` = " & SizeID & ", `item_price` = " & CDec(txtPrice.Text) & ", `item_status` = '" & cbSupplyStatus.Text & "', `item_reorder_point` = " & CInt(txtReOrderPoint.Text) & " WHERE barcodeid = '" & barcodeID.Text & "'")
        UserActivity("Updated a(n) " & cbSupplyType.Text & " item " & txtSupplyDesc.Text.Trim & " - " & cbSupplyCategory.Text.Trim & " " & cbSupplySize.Text.Trim & ". Barcode: " & barcodeID.Text & "", "SUPPLY ITEM UPDATE")
        frmWait.seconds = 1
        frmWait.ShowDialog()
        MsgBox("Supply item has been successfully updated.", vbInformation, "")
        frmSupplyItems.SupplyItemList()
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cbSupplyCategory_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbSupplyCategory.KeyPress, cbSupplySize.KeyPress
        e.Handled = True
    End Sub

    Private Sub cbSupplyCategory_TextChanged(sender As Object, e As EventArgs) Handles cbSupplyCategory.TextChanged
        cbSupplySize.Text = String.Empty
        SizeID = 0

        txtSupplyDesc.Text = String.Empty
    End Sub

    Private Sub btnCancelSearch_Click(sender As Object, e As EventArgs) Handles btnCancelSearch.Click
        SearchPanel.Visible = False
    End Sub
End Class