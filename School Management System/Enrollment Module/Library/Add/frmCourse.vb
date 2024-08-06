Public Class frmCourse
    Public Shared CourseID As Integer = 0

#Region "Drag Form"

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles systemSign.MouseDown, Panel1.MouseDown  ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles systemSign.MouseMove, Panel1.MouseMove  ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles systemSign.MouseUp, Panel1.MouseUp   ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

#End Region

    Private Sub frmCourse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
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

    Private Sub frmCourse_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ResetControls(Me)
        CourseID = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtCode) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If CHECK_EXISTING("SELECT * FROM tbl_course WHERE course_name = '" & txtName.Text.ToUpper.Trim & "'") = True Then Return
            If MsgBox("Are you sure you want to add this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("INSERT INTO tbl_course (course_code, course_name, course_major, course_status, course_gr_number, course_date_granted, course_sector) values ('" & txtCode.Text.Trim & "', '" & txtName.Text.Trim & "', '" & txtMajor.Text.Trim & "', '" & cbStatus.Text & "', '" & txtGr.Text.Trim & "', " & dtGrant.Value & ", '" & txtSector.Text & "')")
                    UserActivity("Added a new course '" & txtCode.Text.Trim & " - " & txtName.Text.Trim & "'.", "LIBRARY COURSE")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("New course has been successfully saved.", vbInformation, "")
                    LibraryCourseList()
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
            If IS_EMPTY(txtName) = True Then Return
            If CHECK_EXISTING("SELECT * FROM tbl_course WHERE course_name = '" & txtName.Text.ToUpper.Trim & "' and course_id NOT IN(" & CourseID & ") and course_code = '" & txtCode.Text & "'") = True Then Return
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("UPDATE tbl_course set course_code = '" & txtCode.Text.Trim & "', course_name = '" & txtName.Text.Trim & "', course_major = '" & txtMajor.Text.Trim & "', course_status = '" & cbStatus.Text & "', course_gr_number = '" & txtGr.Text.Trim & "', course_date_granted = " & dtGrant.Value & ", course_sector = '" & txtSector.Text & "' where course_id = " & CourseID & "")
                    UserActivity("Updated course '" & txtCode.Text.Trim & " - " & txtName.Text.Trim & "' details.", "LIBRARY COURSE")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("Record has been successfully updated.", vbInformation, "")
                    LibraryCourseList()
                    Me.Close()
                Catch ex As Exception
                    cn.Close()
                    MsgBox(ex.Message, vbCritical)
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "")
        End Try
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = " " AndAlso txtName.Text.EndsWith(" ") Then
            e.Handled = True
        End If
    End Sub

    Private Sub cbStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbStatus.SelectedIndexChanged

    End Sub

    Private Sub cbStatus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbStatus.KeyPress
        e.Handled = True
    End Sub
End Class