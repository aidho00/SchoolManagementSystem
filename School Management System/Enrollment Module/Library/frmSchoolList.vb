Imports MySql.Data.MySqlClient

Public Class frmSchoolList
    Private Sub frmSchoolList_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'LibrarySchoolList()
    End Sub

    Private Sub dgSchoolList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgSchoolList.CellMouseEnter
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = CType(sender, DataGridView).Columns(e.ColumnIndex).Name
            If columnName = "colUpdate" Then
                CType(sender, DataGridView).Cursor = Cursors.Hand
            Else
                CType(sender, DataGridView).Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub dgSchoolList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSchoolList.CellContentClick
        Dim colname As String = dgSchoolList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_schools where schl_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgSchoolList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmSchool
                    .SchoolID = dr.Item("schl_id").ToString
                    .txtCode.Text = dr.Item("schl_code").ToString
                    .txtName.Text = dr.Item("schl_name").ToString
                    .txtAddress.Text = dr.Item("schl_address").ToString
                    .txtID.Text = dr.Item("schl_official_id").ToString
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