Imports MySql.Data.MySqlClient

Public Class frmLinkGrade
    Private Sub frmLinkGrade_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnLink_Click(sender As Object, e As EventArgs) Handles btnLink.Click
        If MsgBox("Are you sure you want to link this grade to the curriculum subject?", vbYesNo + vbQuestion) = vbYes Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT * from tbl_students_curriculum_grades where scg_student_id = '" & frmStudentEvaluation.StudentID & "' and scg_curr_id = " & frmStudentEvaluation.currid & " and scg_subject_id = " & CInt(frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(0).Value) & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                query("UPDATE tbl_students_curriculum_grades SET `scg_grade_id` = " & CInt(frmStudentEvaluation.dgGradeList.CurrentRow.Cells(0).Value) & ", `scg_grade` = '" & cbGrade.Text & "' WHERE scg_student_id = '" & frmStudentEvaluation.StudentID & "' and scg_curr_id = " & frmStudentEvaluation.currid & " and scg_subject_id = " & CInt(frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(0).Value) & "")
                UserActivity("Updated linked grade from " & lblAcadStatus.Text & " with subject " & lblCode.Text & " - " & lblDesc.Text & " to the curriculum subject " & lblCurrCode.Text & " - " & lblCurrDesc.Text & " in curriculum " & frmStudentEvaluation.txtCurr.Text & ".", "STUDENT EVALUATION")
                MsgBox("Successfully updated linked grade to the curriculum subject.", vbInformation)
            Else
                query("Insert into tbl_students_curriculum_grades (`scg_student_id`, `scg_curr_id`, `scg_subject_id`, `scg_grade_id`, `scg_grade`) VALUES ('" & frmStudentEvaluation.StudentID & "', " & frmStudentEvaluation.currid & ", " & CInt(frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(0).Value) & ", " & CInt(frmStudentEvaluation.dgGradeList.CurrentRow.Cells(0).Value) & ", '" & cbGrade.Text & "')")
                UserActivity("Linked grade from " & lblAcadStatus.Text & " with subject " & lblCode.Text & " - " & lblDesc.Text & " to the curriculum subject " & lblCurrCode.Text & " - " & lblCurrDesc.Text & " in curriculum " & frmStudentEvaluation.txtCurr.Text & ".", "STUDENT EVALUATION")
                MsgBox("Successfully linked grade to the curriculum subject.", vbInformation)
            End If
            dr.Close()
            cn.Close()
            frmStudentEvaluation.LoadCurriculum()
            frmStudentEvaluation.SearchPanel.Visible = False
            Me.Close()
        End If
    End Sub
End Class