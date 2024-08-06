Imports MySql.Data.MySqlClient

Public Class frmGradingChanges

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
        If frmMain.formTitle.Text = "Edit Student Grades" Then
            frmStudentGradeEditor.dgStudentGrades.CurrentRow.Cells(4).Value = cbGrade.Text
            frmStudentGradeEditor.dgStudentGrades.CurrentRow.Cells(5).Value = cbCredit.Text
        ElseIf frmMain.formTitle.Text = "Edit Class Grades" Then
            frmClassGradeEditor.dgStudentList.CurrentRow.Cells(8).Value = cbGrade.Text
            frmClassGradeEditor.dgStudentList.CurrentRow.Cells(9).Value = cbCredit.Text
        ElseIf frmMain.formTitle.Text = "Credit Student Grades" Then

        End If
        Me.Dispose()
    End Sub

    Private Sub cbGrade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbGrade.SelectedIndexChanged
        If cbGrade.Text = "D" Or cbGrade.Text = "W" Or cbGrade.Text = "5.0" Then
            cbCredit.Text = "0"
        Else

            'cn.Close()
            'cn.Open()
            'cm = New MySqlCommand("select * from tbl_students_curriculum where sc_student_id = '" & frmStudentGradeEditor.studentId & "' and sc_status = 'On Hold'", cn)
            'dr = cm.ExecuteReader
            'dr.Read()
            'If dr.HasRows Then

            'Else
            'End If
            'dr.Close()
            'cn.Close()

            If CDbl(cbGrade.Text) <= 3 Then
                cbCredit.Text = frmStudentGradeEditor.dgStudentGrades.CurrentRow.Cells(5).Value
            End If
        End If
    End Sub
End Class