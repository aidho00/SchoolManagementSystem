Imports MySql.Data.MySqlClient

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


    Private Sub dgGRitemList_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgGRitemList.RowsAdded
        lblTotal.Text = Format(CDec(GetColumnSum(dgGRitemList, 6)), "#,##0.00")
    End Sub

    Private Sub dgGRitemList_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgGRitemList.RowsRemoved
        lblTotal.Text = Format(CDec(GetColumnSum(dgGRitemList, 6)), "#,##0.00")
    End Sub

    Private Sub dgGRitemList_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgGRitemList.CellValueChanged
        lblTotal.Text = Format(CDec(GetColumnSum(dgGRitemList, 6)), "#,##0.00")
    End Sub

End Class