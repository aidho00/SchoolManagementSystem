Imports MySql.Data.MySqlClient

Public Class frmSection
    Public Shared SectionID As Integer = 0
    Private Sub frmSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        SectionID = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(cbCourse) = True Then Return
            If IS_EMPTY(txtCode) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If CHECK_EXISTING("SELECT * FROM tbl_class_block WHERE cb_code = '" & txtCode.Text.Trim & "' and cb_year_level = '" & cbYearLevel.Text & "' and cb_course_id = " & CInt(cbCourse.SelectedValue.ToString) & "") = True Then Return
            If MsgBox("Are you sure you want to add this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("INSERT INTO tbl_class_block (cb_code, cb_description, cb_year_level, cb_course_id) values ('" & txtCode.Text.Trim & "', '" & txtName.Text.Trim & "', '" & cbYearLevel.Text & "', " & CInt(cbCourse.SelectedValue.ToString) & ")")
                    UserActivity("Added a new class section '" & txtCode.Text.Trim & "'.", "LIBRARY CLASS SECTION")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("New section has been successfully saved.", vbInformation, "")
                    LibrarySectionList()
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

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If IS_EMPTY(cbCourse) = True Then Return
            If IS_EMPTY(txtCode) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                query("update tbl_class_block set cb_code = '" & txtCode.Text.Trim & "', cb_description = '" & txtName.Text.Trim & "', cb_year_level = '" & cbYearLevel.Text & "', cb_course_id = " & CInt(cbCourse.SelectedValue.ToString) & " where cb_id = " & SectionID & "")
                UserActivity("Updated class section '" & txtCode.Text.Trim & "' details.", "LIBRARY CLASS SECTION")
                frmWait.seconds = 1
                frmWait.ShowDialog()
                MsgBox("Record has been successfully updated.", vbInformation, "")
                LibrarySectionList()
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "")
        End Try
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress, txtCode.KeyPress
        If e.KeyChar = " " AndAlso txtName.Text.EndsWith(" ") Then
            e.Handled = True
        End If
    End Sub

    Private Sub cbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCourse.SelectedIndexChanged, cbAdviser.SelectedIndexChanged
        txtCourse.Text = ComboCourseName(cbCourse)
    End Sub
End Class