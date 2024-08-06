Imports MySql.Data.MySqlClient

Public Class frmSubjectList
    Private Sub frmSubjectList_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'LibrarySubjectList()
    End Sub

    Private Sub dgSubjectList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgSubjectList.CellMouseEnter
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = CType(sender, DataGridView).Columns(e.ColumnIndex).Name
            If columnName = "colUpdate" Then
                CType(sender, DataGridView).Cursor = Cursors.Hand
            Else
                CType(sender, DataGridView).Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub dgSubjectList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSubjectList.CellContentClick
        Dim colname As String = dgSubjectList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select (b.subject_id) as 'ID', (b.subject_code) as 'Code', (b.subject_description) as 'Description', (b.subject_Type) as 'Type', (b.subject_group) as 'Group', (b.subject_units) as 'Units', (b.subject_charge_units) as 'CUnits', CONCAT(a.subject_description,'-',a.subject_code) as 'Prerequisite', a.subject_id as 'PrerequisiteID', (b.subject_active_status) as 'Status' from tbl_subject b LEFT JOIN tbl_subject a ON a.subject_id = b.subject_prerequisite where b.subject_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgSubjectList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmSubject
                    .comboSubjectGroup()
                    .SubjectID = dr.Item("ID").ToString
                    .PRSubjectID = dr.Item("PrerequisiteID").ToString
                    .txtCode.Text = dr.Item("Code").ToString
                    .txtName.Text = dr.Item("Description").ToString
                    .txtUnits.Text = dr.Item("Units").ToString
                    .txtCunits.Text = dr.Item("CUnits").ToString
                    .txtPreSubject.Text = dr.Item("Prerequisite").ToString
                    .cbStatus.Text = dr.Item("Status").ToString
                    .cbGroup.Text = dr.Item("Group").ToString
                    .cbType.Text = dr.Item("Type").ToString
                    .btnSave.Visible = False
                    .btnUpdate.Visible = True
                    .txtCode.ReadOnly = True
                    .ShowDialog()
                End With
            Else
            End If
            dr.Close()
            cn.Close()
        End If
    End Sub
End Class