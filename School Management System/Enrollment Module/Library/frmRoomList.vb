Imports MySql.Data.MySqlClient

Public Class frmRoomList
    Private Sub frmSubjectList_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'LibraryRoomList()
    End Sub

    Private Sub dgRoomList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgRoomList.CellMouseEnter
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = CType(sender, DataGridView).Columns(e.ColumnIndex).Name
            If columnName = "colUpdate" Then
                CType(sender, DataGridView).Cursor = Cursors.Hand
            Else
                CType(sender, DataGridView).Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub dgRoomList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgRoomList.CellContentClick
        Dim colname As String = dgRoomList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_room where room_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgRoomList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmRoom
                    .RoomID = dr.Item("room_id").ToString
                    .txtCode.Text = dr.Item("room_code").ToString
                    .txtName.Text = dr.Item("room_description").ToString
                    .txtCapacity.Text = dr.Item("capacity").ToString
                    .btnSave.Visible = False
                    .btnUpdate.Visible = True
                    .ShowDialog()
                End With
            Else
            End If
            dr.Close()
            cn.Close()
        End If
    End Sub
End Class