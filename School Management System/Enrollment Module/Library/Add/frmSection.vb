Imports MySql.Data.MySqlClient

Public Class frmSection
    Dim CourseID As Integer = 0
    Public Shared SectionID As Integer = 0
    Private Sub btnSearchCourse_Click(sender As Object, e As EventArgs) Handles btnSearchCourse.Click
        frmTitle.Text = "Search Course"
        SearchPanel.Visible = True
        SectionCourseList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgCourseList)
    End Sub

    Sub SectionCourseList()
        Try
            dgCourseList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select course_id, course_code, course_name, course_major, course_status from tbl_course where (course_code LIKE '%" & txtSearch.Text & "%' or course_name LIKE '%" & txtSearch.Text & "%') order by course_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgCourseList.Rows.Add(i, dr.Item("course_id").ToString, dr.Item("course_code").ToString, dr.Item("course_name").ToString, dr.Item("course_major").ToString, dr.Item("course_status").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(dgCourseList, dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgCourseList.Rows.Clear()
        End Try
    End Sub

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

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        SectionCourseList()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        CourseID = dgCourseList.CurrentRow.Cells(1).Value
        cbCourse.Text = dgCourseList.CurrentRow.Cells(2).Value
        txtCourse.Text = dgCourseList.CurrentRow.Cells(3).Value
        SearchPanel.Visible = False
        txtSearch.Text = ""
    End Sub
End Class