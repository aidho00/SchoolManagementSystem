Imports System.IO

Public Class frmEmployee


    Public Shared EmployeeID As Integer = 0

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

    Private Sub frmEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub frmEmployee_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ResetControls(Me)
        EmployeeID = 0
    End Sub

    Private Sub SetSlide(index As Integer)
        For Each control As Control In Panels.Controls
            If TypeOf control Is Panel Then
                Dim PaneltoHide As Panel = DirectCast(control, Panel)
                PaneltoHide.Hide()
            End If
        Next
    End Sub


    Private Sub photoSignatureSwitch_Click(sender As Object, e As EventArgs) Handles photoSignatureSwitch.Click
        If photoSignature.Text = "Photo" Then
            photoSignature.Text = "Signature"
            empSignature.Visible = True
            empPhoto.Visible = False
        ElseIf photoSignature.Text = "Signature" Then
            photoSignature.Text = "Photo"
            empSignature.Visible = False
            empPhoto.Visible = True
        End If
    End Sub

    Private Sub dtBirthdate_ValueChanged(sender As Object, e As EventArgs) Handles dtBirthdate.ValueChanged
        txtAge.Text = "Age: " & GetCurrentAge(dtBirthdate.Value) & " Years Old"
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtFirstName) = True Then Return
            If IS_EMPTY(txtLastName) = True Then Return
            If IS_EMPTY(txtAddress) = True Then Return
            If IS_EMPTY(txtContact) = True Then Return
            If IS_EMPTY(txtRequiredUnits) = True Then Return
            If IS_EMPTY(txtDesignation) = True Then Return
            If IS_EMPTY(txtBio) = True Then Return
            If CHECK_EXISTING("SELECT * FROM tbl_employee WHERE emp_first_name = '" & txtFirstName.Text.Trim & "' and emp_last_name = '" & txtLastName.Text.Trim & "' and emp_middle_name = '" & txtMiddleName.Text.Trim & "'") = True Then Return
            If MsgBox("Are you sure you want to add this record?", vbYesNo + vbQuestion) = vbYes Then
                If empPhoto.Image Is Nothing Then
                    empPhoto.Image = Dummypicture.Image
                End If
                If empSignature.Image Is Nothing Then
                    empSignature.Image = Dummysign.Image
                End If
                InsertEmployee()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "")
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If IS_EMPTY(txtFirstName) = True Then Return
            If IS_EMPTY(txtLastName) = True Then Return
            If IS_EMPTY(txtAddress) = True Then Return
            If IS_EMPTY(txtContact) = True Then Return
            If IS_EMPTY(txtRequiredUnits) = True Then Return
            If IS_EMPTY(txtDesignation) = True Then Return
            If IS_EMPTY(txtBio) = True Then Return
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                If empPhoto.Image Is Nothing Then
                    empPhoto.Image = Dummypicture.Image
                End If
                If empSignature.Image Is Nothing Then
                    empSignature.Image = Dummysign.Image
                End If
                UpdateEmployee()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "")
        End Try
    End Sub

    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        frmCamera.Show()
        frmCamera.lblCameraSubject.Text = "Employee"
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        frmCamera.Show()
        frmCamera.lblCameraSubject.Text = "Employee"
    End Sub
End Class