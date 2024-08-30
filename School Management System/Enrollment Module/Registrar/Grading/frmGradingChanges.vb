Imports MySql.Data.MySqlClient

Public Class frmGradingChanges

    Public GradingChangeStudentID As String = ""
    Public GradingChangeSubjectID As Integer = 0
    Public GradingChangeSubjectUnit As Integer = 0
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
        Dim CurrID As Integer
        Dim GradeCutOff As String
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("select sc_curr_id from tbl_students_curriculum where sc_student_id = '" & GradingChangeStudentID & "' and sc_status = 'Ongoing'", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            CurrID = str_userid = CInt(dr.Item("sc_curr_id").ToString)

            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT passingGrade from tbl_curriculum_subjects where curriculumID = " & CurrID & " and subjectID = " & GradingChangeSubjectID & "", cn)
            GradeCutOff = cm.ExecuteScalar
            cn.Close()
        Else
            dr.Close()
            cn.Close()
        End If

        If cbGrade.Text = "D" Or cbGrade.Text = "W" Or cbGrade.Text = "5.0" Then
            cbCredit.Text = "0"
        Else
            If CDbl(cbGrade.Text) <= CDbl(GradeCutOff) Then
                cbCredit.Text = GradingChangeSubjectUnit
            Else
                cbCredit.Text = "0"
            End If
        End If
    End Sub
End Class