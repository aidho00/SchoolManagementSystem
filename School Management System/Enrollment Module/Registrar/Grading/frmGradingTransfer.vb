Imports MySql.Data.MySqlClient

Public Class frmGradingTransfer

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

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles systemSign.MouseUp, Panel1.MouseUp  ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

#End Region

    Private Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click
        Try
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to transfer student '" & frmStudentGradeEditor.txtStudent.Text & "' grade in " & frmStudentGradeEditor.dgStudentGrades.CurrentRow.Cells(2).Value & " " & frmStudentGradeEditor.dgStudentGrades.CurrentRow.Cells(3).Value & " from academic year '" & frmStudentGradeEditor.cbAcademicYear.Text & "' to academic year '" & cbAcademicYear.Text & "'?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dr = DialogResult.No Then
                Me.Dispose()
            Else
                If cbAllSubject.Checked = True Then
                    For Each row As DataGridViewRow In frmStudentGradeEditor.dgStudentGrades.Rows
                        query("UPDATE tbl_students_grades set sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " where sg_id = " & CInt(row.Cells(13).Value) & "")
                    Next
                    UserActivity("Transferred all student '" & frmStudentGradeEditor.txtStudent.Text & "' grade in academic year '" & frmStudentGradeEditor.cbAcademicYear.Text & "' to academic year '" & cbAcademicYear.Text & "'.", "GRADE EDITING")

                Else
                    query("UPDATE tbl_students_grades set sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " where sg_id = " & CInt(frmStudentGradeEditor.dgStudentGrades.CurrentRow.Cells(13).Value) & "")
                    UserActivity("Transferred a student '" & frmStudentGradeEditor.txtStudent.Text & "' grade in " & frmStudentGradeEditor.dgStudentGrades.CurrentRow.Cells(2).Value & " " & frmStudentGradeEditor.dgStudentGrades.CurrentRow.Cells(3).Value & " from academic year '" & frmStudentGradeEditor.cbAcademicYear.Text & "' to academic year '" & cbAcademicYear.Text & "'.", "GRADE CREDITING")

                End If
                MessageBox.Show("Grades successfully transferred.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                frmStudentGradeEditor.btnLoadGrade.PerformClick()
                Me.Dispose()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
        MsgBox("Transfer cancelled.", vbInformation)
    End Sub
End Class