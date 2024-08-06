Public Class frmRoom
    Public Shared RoomID As Integer = 0
    Private Sub frmRoom_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        RoomID = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtCode) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If IS_EMPTY(txtCapacity) = True Then Return
            If CHECK_EXISTING("SELECT * FROM tbl_room WHERE room_code = '" & txtCode.Text.Trim & "'") = True Then Return
            If MsgBox("Are you sure you want to add this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("INSERT INTO tbl_room (room_code, room_description, is_active, capacity) values ('" & txtCode.Text.Trim & "', '" & txtName.Text.Trim & "', 'Active', '" & txtCapacity.Text.Trim & "')")
                    UserActivity("Added a new room '" & txtCode.Text.Trim & " - " & txtName.Text.Trim & "'.", "LIBRARY ROOM")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("New room has been successfully saved.", vbInformation, "")
                    LibraryRoomList()
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
            If IS_EMPTY(txtCode) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If IS_EMPTY(txtCapacity) = True Then Return
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("UPDATE tbl_room SET room_code='" & txtCode.Text.Trim & "', room_description='" & txtName.Text.Trim & "', capacity='" & txtCapacity.Text.Trim & "' where room_id = " & RoomID & "")
                    UserActivity("Updated room '" & txtCode.Text.Trim & " - " & txtName.Text.Trim & "' details.", "LIBRARY ROOM")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("Record has been successfully updated.", vbInformation, "")
                    LibraryRoomList()
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

    Private Sub txtCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCode.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Back) Then
            Return
        End If

        If e.KeyChar = " " AndAlso txtCode.Text.EndsWith(" ") Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = " " AndAlso txtCode.Text.EndsWith(" ") Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCapacity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCapacity.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If
    End Sub
End Class