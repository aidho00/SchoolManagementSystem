Imports MySql.Data.MySqlClient
Public Class frmCourseList
    Private Sub frmStudentList_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'LibraryCourseList()
    End Sub

    Private Sub dgCourseList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgCourseList.CellMouseEnter
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = CType(sender, DataGridView).Columns(e.ColumnIndex).Name
            If columnName = "colUpdate" Then
                CType(sender, DataGridView).Cursor = Cursors.Hand
            Else
                CType(sender, DataGridView).Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub dgCourseList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCourseList.CellContentClick
        Dim colname As String = dgCourseList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_course where course_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgCourseList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmCourse
                    .CourseID = dr.Item("course_id").ToString
                    .cbStatus.Text = dr.Item("course_status").ToString
                    .txtCode.Text = dr.Item("course_code").ToString
                    .txtName.Text = dr.Item("course_name").ToString
                    .txtMajor.Text = dr.Item("course_major").ToString
                    .txtGr.Text = dr.Item("course_gr_number").ToString
                    Try
                        .dtGrant.Value = dr.Item("course_date_granted")
                    Catch ex As Exception
                    End Try
                    .txtSector.Text = dr.Item("course_sector").ToString
                    .txtCode.Enabled = False
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