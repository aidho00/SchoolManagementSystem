Imports MySql.Data.MySqlClient
Public Class frmCurriculumList
    Private Sub dgCurrList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCurrList.CellContentClick
        Dim colname As String = dgCurrList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            fillCombo("SELECT `course_id`, CONCAT(`course_code`,' - ', `course_name`) as Course, `course_major`, `course_levels`, `course_status`, `course_gr_number` FROM `tbl_course`", frmCurriculum.cbCourse, "tbl_course", "Course", "course_id")
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_curriculum JOIN tbl_course ON tbl_curriculum.curr_course_id = tbl_course.course_id where tbl_curriculum.curriculum_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgCurrList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmCurriculum
                    .currID = dr.Item("curriculum_id").ToString
                    .txtCurriculum.Text = dr.Item("curriculum_code").ToString
                    .cbCourse.Text = dr.Item("course_code").ToString & " - " & dr.Item("course_name").ToString
                    .cbStatus.Text = dr.Item("is_active").ToString
                    .cbCurrent.Text = dr.Item("is_current").ToString
                    .txtNotes.Text = dr.Item("notes").ToString
                    .txtUnits.Text = dr.Item("total_units").ToString
                    .btnSave.Visible = False
                    .btnUpdate.Visible = True
                    .ShowDialog()
                End With
            Else
            End If
            dr.Close()
            cn.Close()
        ElseIf colname = "colSetup" Then
            'frmEnrollStudent.updateSchedAdmin.Visible = False
            frmMain.OpenForm(frmCurriculumSetup, "Curriculum Setup")
            frmMain.HideAllFormsInPanelExcept(frmCurriculumSetup)
            frmMain.controlsPanel.Visible = False
            With frmCurriculumSetup
                .cbYearLevel.SelectedIndex = 0
                .cbSemester.SelectedIndex = 0
                .currID = dgCurrList.Rows(e.RowIndex).Cells(1).Value.ToString
                .txtCurriculum.Text = dgCurrList.Rows(e.RowIndex).Cells(2).Value.ToString
                .txtCourse.Text = dgCurrList.Rows(e.RowIndex).Cells(3).Value.ToString & " - " & dgCurrList.Rows(e.RowIndex).Cells(4).Value.ToString
                .CurrSubjectList()
            End With

        End If
    End Sub

    Private Sub frmCurriculumList_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub
End Class