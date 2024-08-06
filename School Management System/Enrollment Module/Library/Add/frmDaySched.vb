Public Class frmDaySched
    Public Shared DaySchedID As Integer = 0
    Private lastChar As Char = Char.MinValue

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

    Private Sub frmDaySched_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        DaySchedID = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtCode) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If CHECK_EXISTING("SELECT * FROM tbl_day_schedule WHERE ds_code = '" & txtCode.Text.Trim & "'") = True Then Return
            If MsgBox("Are you sure you want to add this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("INSERT INTO tbl_day_schedule (ds_code, ds_description) values ('" & txtCode.Text.Trim & "', '" & txtName.Text.Trim & "')")
                    UserActivity("Added a new day schedule '" & txtCode.Text.Trim & " - " & txtName.Text.Trim & "'.", "LIBRARY DAY SCHEDULE")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("New day schedule has been successfully saved.", vbInformation, "")
                    LibraryDaySchedList()
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
            If IS_EMPTY(txtCode) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("update tbl_day_schedule set ds_code = '" & txtCode.Text.Trim & "', ds_description = '" & txtName.Text.Trim & "' where ds_id = " & DaySchedID & "")
                    UserActivity("Updated day schedule '" & txtCode.Text.Trim & " - " & txtName.Text.Trim & "' details.", "LIBRARY DAY SCHEDULE")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("Record has been successfully updated.", vbInformation, "")
                    LibraryDaySchedList()
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

        Dim allowedChars As String = "MmTtHhWwFfSsAaUuNn "

        If e.KeyChar = " " AndAlso txtCode.Text.EndsWith(" ") Then
            e.Handled = True
        End If

        If allowedChars.Contains(e.KeyChar) Then
            If lastChar = e.KeyChar Then
                e.Handled = True
            Else
                lastChar = e.KeyChar
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode_TextChanged(sender As Object, e As EventArgs) Handles txtCode.TextChanged
        If String.IsNullOrEmpty(txtCode.Text) Then
            lastChar = Char.MinValue
        End If
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = " " AndAlso txtName.Text.EndsWith(" ") Then
            e.Handled = True
        End If
    End Sub

End Class