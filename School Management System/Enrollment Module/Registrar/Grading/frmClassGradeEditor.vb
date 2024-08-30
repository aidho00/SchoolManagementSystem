Imports MySql.Data.MySqlClient

Public Class frmClassGradeEditor
    Public classID As Integer
    Public SubjectID As Integer
    Public SubjectUnits As Integer


    Private Sub btnSearchClass_Click(sender As Object, e As EventArgs) Handles btnSearchClass.Click
        SearchPanel.Visible = True
        dgStudentList.BringToFront()
        frmTitle.Text = "Search Class Schedule"
        GradingClassSchedList()
        txtSearch.Select()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        classID = CInt(dgClassSchedList.CurrentRow.Cells(1).Value)
        SubjectID = CInt(dgClassSchedList.CurrentRow.Cells(15).Value)
        SubjectUnits = CInt(dgClassSchedList.CurrentRow.Cells(5).Value)
        txtClass.Text = dgClassSchedList.CurrentRow.Cells(2).Value & " | " & dgClassSchedList.CurrentRow.Cells(3).Value & " | " & dgClassSchedList.CurrentRow.Cells(4).Value & " | " & dgClassSchedList.CurrentRow.Cells(6).Value & " - " & dgClassSchedList.CurrentRow.Cells(7).Value & " - " & dgClassSchedList.CurrentRow.Cells(8).Value & " | " & dgClassSchedList.CurrentRow.Cells(10).Value
        SearchPanel.Visible = False
        LoadClassStudentGrades()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Select Case frmTitle.Text
            Case "Search Class Schedule"
                GradingClassSchedList()
        End Select
    End Sub

    Private Sub dgStudentList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgStudentList.CellContentClick
        Dim colname As String = dgStudentList.Columns(e.ColumnIndex).Name
        If colname = "colEdit" Then
            frmGradingChanges.cbGrade.Text = dgStudentList.CurrentRow.Cells(8).Value
            frmGradingChanges.cbCredit.Text = dgStudentList.CurrentRow.Cells(9).Value
            frmGradingChanges.GradingChangeSubjectID = SubjectID
            frmGradingChanges.GradingChangeSubjectUnit = SubjectUnits
            frmGradingChanges.GradingChangeStudentID = dgStudentList.CurrentRow.Cells(1).Value
            frmGradingChanges.ShowDialog()
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        For Each row As DataGridViewRow In dgStudentList.Rows
            Dim grade As String
            Dim credit As String
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select sg_grade, sg_credits from tbl_students_grades where sg_student_id = '" & row.Cells(1).Value & "' and sg_class_id = " & classID & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                grade = dr.Item("sg_grade").ToString
                credit = dr.Item("sg_credits").ToString
            Else
            End If
            dr.Close()
            cn.Close()

            If grade = row.Cells(4).Value And credit = row.Cells(5).Value Then
            Else
                query("UPDATE tbl_students_grades set sg_prev_grade = sg_grade, sg_prev_grade_addedby = sg_grade_addedby, sg_prev_grade_dateadded = sg_grade_dateadded, sg_grade = '" & row.Cells(8).Value & "' , sg_credits = '" & row.Cells(9).Value & "', sg_grade_addedby = " & str_userid & ", sg_grade_dateadded = CURDATE() where sg_student_id = '" & row.Cells(1).Value & "' and sg_class_id = " & classID & "")
            End If
        Next
        MessageBox.Show("Grades successfully updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        clearfields()
    End Sub
    Sub clearfields()
        dgStudentList.Rows.Clear()
        txtClass.Text = String.Empty
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearfields()
    End Sub

    Private Sub frmClassGradeEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub
End Class