Public Class frmSubject
    Public Shared SubjectID As Integer = 0
    Public Shared PRSubjectID As Integer = 0

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

    Private Sub toNormalForm()
        SearchPanel.Visible = False
        Me.Size = New Size(830, 481)
        dgSubjectList.Rows.Clear()
        txtSearch.Text = String.Empty
        centerForm(Me)
    End Sub

    Private Sub frmCourse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
    End Sub

    Sub comboSubjectGroup()
        If frmMain.systemModule.Text = "College Module" Then
            With cbGroup
                .Items.Clear()
                .Items.Add("English")
                .Items.Add("Filipino")
                .Items.Add("Math")
                .Items.Add("Social Science")
                .Items.Add("Elective Major")
                .Items.Add("Elective Minor")
                .Items.Add("Health Science")
                .Items.Add("Physical Education")
                .Items.Add("CWTS CMT")
            End With

        ElseIf frmMain.systemModule.Text = "High School Moldule" Then
            With cbGroup
                .Items.Clear()
                .Items.Add("Core")
                .Items.Add("Applied")
                .Items.Add("Specialized")
            End With
        End If
        cbGroup.SelectedIndex = 0
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            toNormalForm()
        Else
            Me.Close()
        End If
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
        SubjectID = 0
        PRSubjectID = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim SubjectDescription As String = txtName.Text.Replace("'", "''")
            If IS_EMPTY(txtCode) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If IS_EMPTY(txtUnits) = True Then Return
            If CHECK_EXISTING("SELECT * FROM tbl_subject WHERE subject_description = '" & SubjectDescription.Trim & "' and subject_code = '" & txtCode.Text.Trim & "' and subject_units = " & txtUnits.Text.Trim & " and subject_group = '" & cbGroup.Text & "'") Then Return
            If MsgBox("Are you sure you want to save this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("INSERT INTO tbl_subject (subject_code, subject_description, subject_active_status, subject_type, subject_units, subject_charge_units, subject_group, subject_prerequisite) values ('" & txtCode.Text.Trim & "', '" & SubjectDescription.Trim & "', '" & cbStatus.Text & "', '" & cbType.Text & "', '" & txtUnits.Text & "', '" & txtCunits.Text & "', '" & cbGroup.Text & "', " & PRSubjectID & ")")
                    UserActivity("Added a new subject '" & txtCode.Text.Trim & " - " & txtName.Text.Trim & "'.", "LIBRARY SUBJECT")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("New subject has been successfully saved.", vbInformation, "")
                    LibrarySubjectList()
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
            Dim SubjectDescription As String = txtName.Text.Replace("'", "''")
            If IS_EMPTY(txtCode) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If IS_EMPTY(txtUnits) = True Then Return
            If CHECK_EXISTING("SELECT * FROM tbl_subject WHERE subject_description = '" & SubjectDescription.Trim & "' and subject_code = '" & txtCode.Text.Trim & "' and subject_units = " & txtUnits.Text.Trim & " and subject_group = '" & cbGroup.Text & "' and subject_id NOT IN(" & SubjectID & ")") Then Return
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("UPDATE tbl_subject SET subject_code='" & txtCode.Text.Trim & "', subject_description='" & SubjectDescription.Trim & "', subject_active_status='" & cbStatus.Text & "', subject_type='" & cbType.Text & "', subject_units='" & txtUnits.Text.Trim & "', subject_charge_units='" & txtCunits.Text.Trim & "', subject_group='" & cbGroup.Text & "', subject_prerequisite=" & PRSubjectID & " where subject_id = " & SubjectID & "")
                    UserActivity("Updated subject '" & txtCode.Text.Trim & " - " & txtName.Text.Trim & "' details.", "LIBRARY SUBJECT")
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("Record has been successfully updated.", vbInformation, "")
                    LibrarySubjectList()
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

    Private Sub txtUnits_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUnits.KeyPress, txtCunits.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        SearchPanel.Visible = True
        LibrarySubjectListModule()
        Me.Size = New Size(1038, 682)
        centerForm(Me)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        PRSubjectID = 0
        txtPreSubject.Text = "NONE-NONE"
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        PRSubjectID = dgSubjectList.CurrentRow.Cells(1).Value
        txtPreSubject.Text = dgSubjectList.CurrentRow.Cells(2).Value & " - " & dgSubjectList.CurrentRow.Cells(3).Value
        toNormalForm()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LibrarySubjectListModule()
    End Sub
End Class