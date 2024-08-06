Imports MySql.Data.MySqlClient

Public Class frmDaySchedList
    Private Sub frmDaySchedList_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'LibraryDaySchedList()
    End Sub

    Private Sub dgDaySchedList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgDaySchedList.CellMouseEnter
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = CType(sender, DataGridView).Columns(e.ColumnIndex).Name
            If columnName = "colUpdate" Then
                CType(sender, DataGridView).Cursor = Cursors.Hand
            Else
                CType(sender, DataGridView).Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub dgDaySchedList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgDaySchedList.CellContentClick
        Dim colname As String = dgDaySchedList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_day_schedule where ds_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgDaySchedList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmDaySched
                    .DaySchedID = dr.Item("ds_id").ToString
                    .txtCode.Text = dr.Item("ds_code").ToString
                    .txtName.Text = dr.Item("ds_description").ToString
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