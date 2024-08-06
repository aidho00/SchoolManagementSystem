Public Class frmCurriculum
    Public Shared currID As Integer = 0
    Private Sub frmCurriculum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtCurriculum) = True Then Return
            If IS_EMPTY(cbCourse) = True Then Return
            If IS_EMPTY(cbStatus) = True Then Return
            If IS_EMPTY(cbCurrent) = True Then Return
            If cbCourse.Items.Contains(cbCourse.SelectedValue) Then
            Else
                MsgBox("Invalid course selection!", vbCritical)
                Return
            End If
            If CHECK_EXISTING("SELECT * FROM tbl_curriculum WHERE curriculum_code = '" & txtCurriculum.Text.ToUpper.Trim & "'") = True Then Return
            If MsgBox("Are you sure you want to add this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("INSERT INTO tbl_course (INSERT INTO tbl_curriculum (curriculum_code, curr_course_id, notes, is_active, prepared_by_id, datetime_created, is_current, total_units) values ('" & txtCurriculum.Text.Trim & "', " & CInt(cbCourse.SelectedValue) & ", '" & txtNotes.Text.Trim & "', '" & cbStatus.Text & "', " & str_userid & ", CURDATE(), '" & cbCurrent.Text & "', " & CInt(txtUnits.Text) & ")")
                    UserActivity("Added a new curriculum '" & txtCurriculum.Text.Trim & "'.", "LIBRARY CURRICULUM")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("New curriculum has been successfully saved.", vbInformation, "")
                    CurriculumList()
                    Me.Close()
                Catch ex As Exception
                    cn.Close()
                    MsgBox(ex.Message, vbCritical)
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If IS_EMPTY(txtCurriculum) = True Then Return
            If IS_EMPTY(cbCourse) = True Then Return
            If IS_EMPTY(cbStatus) = True Then Return
            If IS_EMPTY(cbCurrent) = True Then Return

            If CHECK_EXISTING("SELECT * FROM tbl_curriculum WHERE curriculum_code = '" & txtCurriculum.Text.ToUpper.Trim & "' and curriculum_id NOT IN (" & currID & ")") = True Then Return
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("UPDATE tbl_curriculum SET curriculum_code='" & txtCurriculum.Text.Trim & "', curr_course_id=" & CInt(cbCourse.SelectedValue) & ", notes='" & txtNotes.Text.Trim & "', is_active='" & cbStatus.Text.Trim & "', is_current='" & cbCurrent.Text.Trim & "', total_units = " & CInt(txtUnits.Text) & " where curriculum_id = " & currID & "")
                    UserActivity("Updated the curriculum '" & txtCurriculum.Text.Trim & "'.", "LIBRARY CURRICULUM")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("Curriculum has been successfully updated.", vbInformation, "")
                    CurriculumList()
                    Me.Close()
                Catch ex As Exception
                    cn.Close()
                    MsgBox(ex.Message, vbCritical)
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel?", vbYesNo + vbQuestion) = vbYes Then
            If btnSave.Visible = True Then
                ResetControls(Me)
            ElseIf btnUpdate.Visible = True Then
                Me.Close()
            End If
        Else
        End If
    End Sub

    Private Sub frmCurriculum_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ResetControls(Me)
        currID = 0
    End Sub

    Private Sub txtUnits_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUnits.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
           AndAlso e.KeyChar <> ControlChars.Back Then
            'cancel keys
            e.Handled = True
        End If
    End Sub
End Class