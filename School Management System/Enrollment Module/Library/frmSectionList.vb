Imports MySql.Data.MySqlClient

Public Class frmSectionList
    Private Sub frmSectionList_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'LibrarySectionList()
    End Sub

    Private Sub dgSectionList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgSectionList.CellMouseEnter
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = CType(sender, DataGridView).Columns(e.ColumnIndex).Name
            If columnName = "colUpdate" Then
                CType(sender, DataGridView).Cursor = Cursors.Hand
            Else
                CType(sender, DataGridView).Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub dgSectionList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSectionList.CellContentClick
        Dim colname As String = dgSectionList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            fillCombo("select * from tbl_course", frmSection.cbCourse, "tbl_course", "course_code", "course_id")
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_class_block t1 JOIN tbl_course t2 ON t1.cb_course_id = t2.course_id where cb_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgSectionList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmSection
                    comboStudentLevel(.cbYearLevel, .lblLabel, .lblLevel)
                    .SectionID = dr.Item("cb_id").ToString
                    .txtCode.Text = dr.Item("cb_code").ToString
                    .txtName.Text = dr.Item("cb_description").ToString
                    .cbYearLevel.Text = dr.Item("cb_year_level").ToString
                    .cbCourse.Text = dr.Item("course_code").ToString
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