Imports MySql.Data.MySqlClient
Public Class frmEvaluationGrade
    Private Sub frmEvaluationGrade_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnLink_Click(sender As Object, e As EventArgs) Handles btnLink.Click
        If cbGrade.Text = String.Empty Then
            MsgBox("There is no selected grade to save.", vbCritical)
        Else
            If MsgBox("Are you sure you want to save this grade to the curriculum subject?", vbYesNo + vbQuestion) = vbYes Then
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT * from tbl_students_curriculum_grades where scg_student_id = '" & frmStudentEvaluation.StudentID & "' and scg_curr_id = " & frmStudentEvaluation.currid & " and scg_subject_id = " & CInt(frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(0).Value) & "", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    query("UPDATE tbl_students_curriculum_grades SET `scg_grade` = '" & cbGrade.Text & "' WHERE scg_student_id = '" & frmStudentEvaluation.StudentID & "' and scg_curr_id = " & frmStudentEvaluation.currid & " and scg_subject_id = " & CInt(frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(0).Value) & "")
                    UserActivity("Updated the encoded grade from " & cbGrade.Text & " to the curriculum subject " & lblCurrCode.Text & " - " & lblCurrDesc.Text & " in curriculum " & frmStudentEvaluation.txtCurr.Text & ".", "STUDENT EVALUATION")
                    MsgBox("Successfully updated linked grade to the curriculum subject.", vbInformation)
                Else
                    query("Insert into tbl_students_curriculum_grades (`scg_student_id`, `scg_curr_id`, `scg_subject_id`, `scg_grade_id`, `scg_grade`) VALUES ('" & frmStudentEvaluation.StudentID & "', " & frmStudentEvaluation.currid & ", " & CInt(frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(0).Value) & ", 0, '" & cbGrade.Text & "')")
                    UserActivity("Encoded grade from " & cbGrade.Text & " to the curriculum subject " & lblCurrCode.Text & " - " & lblCurrDesc.Text & " in curriculum " & frmStudentEvaluation.txtCurr.Text & ".", "STUDENT EVALUATION")
                    MsgBox("Successfully linked grade to the curriculum subject.", vbInformation)
                End If
                dr.Close()
                cn.Close()
                frmStudentEvaluation.LoadCurriculum()
                frmStudentEvaluation.SearchPanel.Visible = False
                Me.Close()
            Else
                MsgBox("Grade encoding cancelled.", vbExclamation)
            End If
        End If
    End Sub

    Private Sub btnLinkGrade_Click(sender As Object, e As EventArgs) Handles btnLinkGrade.Click
        frmStudentEvaluation.frmTitle.Text = "Search Student Grade"
        frmStudentEvaluation.SearchPanel.Visible = True
        frmStudentEvaluation.StudentGradeList()
        frmStudentEvaluation.dgGradeList.BringToFront()
        frmStudentEvaluation.txtSearch.Select()
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If MsgBox("Are you sure you want to remove encoded grade to the curriculum subject?", vbYesNo + vbQuestion) = vbYes Then
            query("DELETE FROM `tbl_students_curriculum_grades` WHERE `scg_student_id` = '" & frmStudentEvaluation.StudentID & "' and `scg_curr_id` = " & frmStudentEvaluation.currid & " and `scg_subject_id` = " & CInt(frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(0).Value) & "")
            UserActivity("Removed encoded curriculum subject " & lblCurrCode.Text & " - " & lblCurrDesc.Text & " grade in curriculum " & frmStudentEvaluation.txtCurr.Text & ".", "STUDENT EVALUATION")
            MsgBox("Successfully removed grade to the curriculum subject.", vbInformation)
            frmStudentEvaluation.LoadCurriculum()
            frmStudentEvaluation.SearchPanel.Visible = False
            Me.Close()
        Else
            MsgBox("Grade removal cancelled.", vbExclamation)
        End If
    End Sub
End Class