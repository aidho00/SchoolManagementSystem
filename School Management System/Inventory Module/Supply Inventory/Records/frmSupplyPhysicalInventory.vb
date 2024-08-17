Imports MySql.Data.MySqlClient

Public Class frmSupplyPhysicalInventory

    Function GetTransno() As String
        Dim yearid As String = YearToday
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT pino FROM tbl_supply_physicalinventory WHERE pino like 'STKRC" & yearid.Remove(0, 2) & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(pino) as ID from tbl_supply_physicalinventory", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            lastCode = lastCode.Remove(0, 7)
            GetTransno = "STKRC" & CInt(yearid.Remove(0, 2) & lastCode) + 1
        Else
            dr.Close()
            GetTransno = "STKRC" & yearid.Remove(0, 2) & "0001"
        End If
        cn.Close()

        Return GetTransno
    End Function

    Sub PhysicalCountSupplyItemStockLevel()
        dgSupplyItemList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "Select (BarcodeID) as 'Item ID', Description, (CategoryName) as 'Category', Sizes, (tbl_supply_inventory.Spare) as 'Stock' from tbl_supply_item JOIN tbl_supply_category ON tbl_supply_item.CategoryID = tbl_supply_category.catid JOIN tbl_supply_sizes ON tbl_supply_item.sizesid = tbl_supply_sizes.sizeid JOIN tbl_supply_inventory ON tbl_supply_item.barcodeid = tbl_supply_inventory.itembarcode JOIN tbl_supply_brand ON tbl_supply_item.brandid = tbl_supply_brand.brandid order by Description asc"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgSupplyItemList.Rows.Add(i, dr.Item("Item ID").ToString, dr.Item("Description").ToString, dr.Item("Category").ToString, dr.Item("Sizes").ToString, dr.Item("Stock").ToString, dr.Item("Stock").ToString, "0")
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs)
        If dgSupplyItemList.RowCount > 0 Then
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to regenerate the supply item list for recounting? Any changes made will be reset.", "Notice!", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dr = DialogResult.No Then
            Else
                PhysicalCountSupplyItemStockLevel()
            End If
        Else
            PhysicalCountSupplyItemStockLevel()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim dr As DialogResult
        dr = MessageBox.Show("Are you sure you want to save the physical count? Once saved, the existing stock levels will be updated based on this count.", "Notice!", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dr = DialogResult.No Then
        Else
            Dim transno As String = GetTransno()
            For Each row As DataGridViewRow In dgSupplyItemList.Rows
                query("INSERT INTO tbl_supply_physicalinventory (pino, item_barcode, currspare, sparephysicalcount, spareadjustment, userid) VALUES ('" & transno & "', '" & row.Cells(1).Value & "', " & CInt(row.Cells(5).Value) & ", " & CInt(row.Cells(6).Value) & ", '" & CInt(row.Cells(7).Value) & "', " & str_userid & ")")
                StockLedgerPhysicalRecount(row.Cells(1).Value, 0, 0, "Physical count adjustment.", "Supply Item Stock Adjustment", "Physical Count No." & transno & "", CInt(row.Cells(6).Value))
            Next
            MsgBox("The recount was successfully recorded.", vbInformation, "")
        End If
    End Sub

    Private isAlreadyValidated As Boolean = False

    Private Sub dgSupplyItemList_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles dgSupplyItemList.CellValidating
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
                    MessageBox.Show("Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                ' Reset the flag if validation passes
                isAlreadyValidated = False
            End If
        End If
    End Sub

    Private Sub frmSupplyPhysicalInventory_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler dgSupplyItemList.CellValidating, AddressOf dgSupplyItemList_CellValidating
    End Sub

    Private Sub dgSupplyItemList_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgSupplyItemList.CellEndEdit
        If dgSupplyItemList.Columns(e.ColumnIndex).Name = "Column3" Then
            dgSupplyItemList.CurrentRow.Cells(7).Value = CInt(dgSupplyItemList.CurrentRow.Cells(6).Value) - CInt(dgSupplyItemList.CurrentRow.Cells(5).Value)
        End If
    End Sub
End Class