Imports MySql.Data.MySqlClient

Public Class frmEnrollmentPermit

    Private Sub CenterForm()
        Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height
        Dim formWidth As Integer = Me.Width
        Dim formHeight As Integer = Me.Height

        Dim newX As Integer = (screenWidth - formWidth) / 2
        Dim newY As Integer = (screenHeight - formHeight) / 2

        Me.Location = New Point(newX, newY)
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            SearchPanel.Visible = False
            Me.Size = New Size(672, 312)
            CenterForm()
        Else
            Me.Close()
            CenterForm()
        End If
    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        SearchPanel.Visible = True
        LibraryEnrollmentPermitStudentList()
        Me.Size = New Size(998, 600)
        CenterForm()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        SearchPanel.Visible = False
        Me.Size = New Size(672, 312)
        CenterForm()
        txtStudentID.Text = dgStudentList.CurrentRow.Cells(1).Value.ToString
        txtStudentName.Text = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
        txtYearLevelCourse.Text = dgStudentList.CurrentRow.Cells(7).Value & " - " & dgStudentList.CurrentRow.Cells(8).Value & " - " & dgStudentList.CurrentRow.Cells(10).Value
    End Sub

    Private Sub txtStudentID_Click(sender As Object, e As EventArgs) Handles txtStudentID.Click

    End Sub

    Private Sub txtStudentID_TextChanged(sender As Object, e As EventArgs) Handles txtStudentID.TextChanged
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("select * from tbl_student_enroll_permit where stud_id = @1 and period_id = @2", cn)
        With cm
            .Parameters.AddWithValue("@1", txtStudentID.Text)
            .Parameters.AddWithValue("@2", CInt(cbAcademicYear.SelectedValue))
        End With
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Dim x As Integer
            x = CInt(dr.Item("status").ToString)
            If x = 1 Then
                cbStatus.Text = "Permitted"
            Else
                cbStatus.Text = "Not Permitted"
            End If
        Else
            cbStatus.Text = "Not Permitted"
        End If
        dr.Close()
        cn.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim x As Integer
        If cbStatus.Text = "Permitted" Then
            x = 1
        Else
            x = 0
        End If
        cm = New MySqlCommand("SELECT * FROM tbl_student_enroll_permit where stud_id = '" & txtStudentID.Text & "' and period_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
        Dim sdr2 As MySqlDataReader = cm.ExecuteReader()
        If (sdr2.Read() = True) Then
            query("update tbl_student_enroll_permit set status = " & x & " where stud_id = '" & txtStudentID.Text & "' and period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
            If cbStatus.Text = "Permitted" Then
                MsgBox("Permission updated. Bypassing of student balance checking has been permitted.", vbInformation)
                UserActivity("Permitted student " & txtStudentID.Text & " " & txtStudentName.Text & " balance checking bypass in Academic Year " & cbAcademicYear.Text & ".", "ENROLLLMENT BALANCE CHECKING")
            Else
                UserActivity("Rejected student " & txtStudentID.Text & " " & txtStudentName.Text & " balance checking bypass in Academic Year " & cbAcademicYear.Text & ".", "ENROLLLMENT BALANCE CHECKING")
                MsgBox("Permission updated. Bypassing of student balance checking has been rejected.", vbInformation)
            End If
        Else
            query("Insert into tbl_student_enroll_permit (`stud_id`, `period_id`, `status`) values ('" & txtStudentID.Text & "', " & CInt(cbAcademicYear.SelectedValue) & ", " & x & ")")
            If cbStatus.Text = "Permitted" Then
                UserActivity("Permitted student " & txtStudentID.Text & " " & txtStudentName.Text & " balance checking bypass in Academic Year " & cbAcademicYear.Text & ".", "ENROLLLMENT BALANCE CHECKING")
                MsgBox("Permission saved. Bypassing of student balance checking has been permitted.", vbInformation)
            Else
                UserActivity("Rejected student " & txtStudentID.Text & " " & txtStudentName.Text & " balance checking bypass in Academic Year " & cbAcademicYear.Text & ".", "ENROLLLMENT BALANCE CHECKING")
                MsgBox("Permission saved. Bypassing of student balance checking has been rejected.", vbInformation)
            End If
        End If
        txtStudentID.Text = "ID"
        txtStudentName.Text = "Name"
        txtYearLevelCourse.Text = "Year Level - Course"
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtStudentID.Text = "ID"
        txtStudentName.Text = "Name"
        txtYearLevelCourse.Text = "Year Level - Course"
    End Sub

    Private Sub frmEnrollmentPermit_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
    End Sub
End Class