
Public Class frmSupplyPurchaseQty

    Public PurchasingStatus As String = ""

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 46
            Case 8
            Case 13
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub frmSupplyPurchaseQty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub txtQty_KeyDown(sender As Object, e As KeyEventArgs) Handles txtQty.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Dispose()
        ElseIf e.KeyCode = Keys.Enter Then
            frmSupplyPurchaseRequest.dgPRtemList.Rows.Add(frmSupplyPurchaseRequest.dgSupplyItemList.CurrentRow.Cells(1).Value, frmSupplyPurchaseRequest.dgSupplyItemList.CurrentRow.Cells(2).Value, frmSupplyPurchaseRequest.dgSupplyItemList.CurrentRow.Cells(3).Value, frmSupplyPurchaseRequest.dgSupplyItemList.CurrentRow.Cells(4).Value, frmSupplyPurchaseRequest.dgSupplyItemList.CurrentRow.Cells(5).Value)
            Me.Dispose()
            frmSupplyPurchaseRequest.SearchPanel.Visible = False
        End If
    End Sub
End Class