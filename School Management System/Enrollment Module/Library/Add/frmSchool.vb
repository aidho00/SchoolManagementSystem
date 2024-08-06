Public Class frmSchool
    Public Shared SchoolID As Integer = 0
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
        SchoolID = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtCode) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If CHECK_EXISTING("SELECT * FROM tbl_schools WHERE schl_name = '" & txtName.Text.Trim & "'") = True Then Return
            If MsgBox("Are you sure you want to add this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("INSERT INTO tbl_schools (schl_code, schl_name, schl_address, schl_official_id) values ('" & txtCode.Text.Trim & "', '" & txtName.Text.Trim & "', '" & txtAddress.Text.Trim & "', '" & txtID.Text.Trim & "')")
                    UserActivity("Added a new school '" & txtCode.Text.Trim & " - " & txtName.Text.Trim & "'.", "LIBRARY SCHOOL")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("New school has been successfully saved.", vbInformation, "")
                    LibrarySchoolList()
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
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("update tbl_schools set schl_code = '" & txtCode.Text.Trim & "', schl_name = '" & txtName.Text.Trim & "', schl_address = '" & txtAddress.Text.Trim & "', schl_official_id = '" & txtID.Text.Trim & "' where schl_id = " & SchoolID & "")
                    UserActivity("Updated school '" & txtCode.Text.Trim & " - " & txtName.Text.Trim & "' details.", "LIBRARY SCHOOL")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("Record has been successfully updated.", vbInformation, "")
                    LibrarySchoolList()
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

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress, txtAddress.KeyPress, txtID.KeyPress
        If e.KeyChar = " " AndAlso txtCode.Text.EndsWith(" ") Then
            e.Handled = True
        End If
    End Sub
End Class