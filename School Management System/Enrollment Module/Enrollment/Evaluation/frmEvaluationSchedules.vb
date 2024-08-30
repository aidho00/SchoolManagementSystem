Imports MySql.Data.MySqlClient

Public Class frmEvaluationSchedules


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


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Function ParseTime(timeString As String) As TimeSpan
        Dim parts = timeString.Split(":"c)
        Dim hours As Integer
        If Integer.TryParse(parts(0), hours) Then
            Dim minutes As Integer
            If Integer.TryParse(parts(1), minutes) Then
                Dim amPM = parts(2).Split(" "c)
                Dim am As Boolean = amPM(1).ToUpper() = "AM"
                If am AndAlso hours = 12 Then
                    hours = 0
                ElseIf Not am AndAlso hours < 12 Then
                    hours += 12
                End If
                Return New TimeSpan(hours, minutes, 0)
            End If
        End If
        ' Return TimeSpan.Zero if parsing fails
        Return TimeSpan.Zero
    End Function


    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If CInt(frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(7).Value) = 0 Then

            AddSubjectToList()

        Else

            Dim PRFound As Boolean = False
            Dim PRSubject As String = ""

            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT concat(subject_code, ' - ', subject_description) from tbl_subject where subject_id = " & CInt(frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(7).Value) & "", cn)
            PRSubject = cm.ExecuteScalar
            cn.Close()

            Dim dataGridViews As DataGridView() = {frmStudentEvaluation.dg1Y1S, frmStudentEvaluation.dg1Y2S, frmStudentEvaluation.dg1YS, frmStudentEvaluation.dg2Y1S, frmStudentEvaluation.dg2Y2S, frmStudentEvaluation.dg2YS, frmStudentEvaluation.dg3Y1S, frmStudentEvaluation.dg3Y2S, frmStudentEvaluation.dg3YS, frmStudentEvaluation.dg4Y1S, frmStudentEvaluation.dg4Y2S, frmStudentEvaluation.dg4YS}
            For Each dgv As DataGridView In dataGridViews
                For Each row As DataGridViewRow In dgv.Rows
                    If frmStudentEvaluation.activeDataGridView.CurrentRow.Cells(7).Value = row.Cells(0).Value Then
                        If row.Cells(4).Value = row.Cells(11).Value Then
                            PRFound = True
                            Exit For
                        End If
                    End If
                Next
                If PRFound = True Then
                    Exit For
                Else
                End If
            Next

            If PRFound = True Then
                AddSubjectToList()
            Else
                MsgBox("The student have not yet completed the prerequisite for this subject. The student need to complete " & PRSubject & " before enrolling in this subject.", vbCritical)
            End If
        End If
        Me.Close()
    End Sub

    Sub AddSubjectToList()
        cn.Close()
        cn.Open()
        Dim sql As String
        sql = "SELECT period_enrollment_status FROM tbl_period where period_id  = " & CInt(frmEvaluationSubjects.cbAcademicYear.SelectedValue) & " and period_enrollment_status = 'CLOSE'"

        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            MsgBox("Enrollment for Academic Year " & frmEvaluationSubjects.cbAcademicYear.Text & " is close. Please contact Registrar to gain access.", vbExclamation)
        Else
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT * FROM tbl_students_grades WHERE sg_student_id = '" & frmStudentEvaluation.StudentID & "' and sg_period_id = " & CInt(frmEvaluationSubjects.cbAcademicYear.SelectedValue) & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Close()
                cn.Close()
                MsgBox("Student " & frmStudentEvaluation.StudentName & " with ID Number " & frmStudentEvaluation.StudentID & " already has grades or is enrolled for this Academic Year.'", vbCritical)
            Else

                dr.Close()
                cn.Close()
                Dim population As Integer = 0
                Dim enrolled As Integer = 0
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT population from tbl_class_schedule where class_schedule_id = " & CInt(dgClassSchedList.CurrentRow.Cells(0).Value) & "", cn)
                population = cm.ExecuteScalar
                cn.Close()
                enrolled = CountEnrolled(CInt(dgClassSchedList.CurrentRow.Cells(0).Value))

                If enrolled >= population Then
                    MessageBox.Show("Subject ''" & dgClassSchedList.CurrentRow.Cells(1).Value & " - " & dgClassSchedList.CurrentRow.Cells(2).Value & "'' enrolled population is already full.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else

                    Dim isFound As Boolean = False

                    Dim selectedClassID As Integer
                    Dim selectedDay As String
                    Dim selectedStartTime As String
                    Dim selectedEndTime As String

                    selectedClassID = CInt(dgClassSchedList.CurrentRow.Cells(0).Value)
                    selectedDay = dgClassSchedList.CurrentRow.Cells(5).Value.ToString
                    selectedStartTime = dgClassSchedList.CurrentRow.Cells(6).Value.ToString
                    selectedEndTime = dgClassSchedList.CurrentRow.Cells(7).Value.ToString

                    Dim selectedStartTimeSpan As TimeSpan = ParseTime(selectedStartTime)
                    Dim selectedEndTimeSpan As TimeSpan = ParseTime(selectedEndTime)

                    Dim selectedDaysArray As New List(Of String)(selectedDay.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))

                    For Each row As DataGridViewRow In frmEvaluationSubjects.dgClassSchedList.Rows
                        Dim classSubject As String = row.Cells(2).Value.ToString & " - " & row.Cells(3).Value.ToString
                        Dim classID As Integer = CInt(row.Cells(0).Value)
                        Dim day As String = row.Cells(5).Value.ToString()
                        Dim startTime As String = row.Cells(6).Value.ToString()
                        Dim endTime As String = row.Cells(7).Value.ToString()

                        Dim startTimeSpan As TimeSpan = ParseTime(startTime)
                        Dim endTimeSpan As TimeSpan = ParseTime(endTime)

                        Dim daysArray As New List(Of String)(day.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))

                        If classID = selectedClassID Then
                            isFound = True
                            MsgBox("Same Class Schedule Exist. Conflict found with Class: " & classSubject & " on Day(s): " & day & " between Starting Time: " & startTime & " and End Time: " & endTime, vbCritical)
                            Exit For
                        ElseIf classID <> selectedClassID Then
                            Dim DaysIntersect = selectedDaysArray.Intersect(daysArray).Count() > 0
                            If DaysIntersect = True AndAlso ((selectedStartTimeSpan < endTimeSpan AndAlso selectedEndTimeSpan > startTimeSpan) OrElse (startTimeSpan < selectedEndTimeSpan AndAlso endTimeSpan > selectedStartTimeSpan)) Then
                                isFound = True
                                MsgBox("Conflict found with Class: " & classSubject & " on Day(s): " & day & " between Starting Time: " & startTime & " and End Time: " & endTime, vbCritical)
                                Exit For
                            End If
                        End If
                    Next

                    If isFound = True Then
                    Else
                        With dgClassSchedList
                            frmEvaluationSubjects.dgClassSchedList.Rows.Add(.CurrentRow.Cells(0).Value, .CurrentRow.Cells(1).Value, .CurrentRow.Cells(2).Value, .CurrentRow.Cells(3).Value, .CurrentRow.Cells(4).Value, .CurrentRow.Cells(5).Value, .CurrentRow.Cells(6).Value, .CurrentRow.Cells(7).Value, .CurrentRow.Cells(8).Value, .CurrentRow.Cells(9).Value, .CurrentRow.Cells(10).Value, .CurrentRow.Cells(11).Value, .CurrentRow.Cells(12).Value)
                        End With
                        MsgBox("The student is eligible to enroll in this subject. The subject schedule have been added to the list for enrollment.", vbInformation)
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub dgClassSchedList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgClassSchedList.CellContentClick
        dgClassSchedList.CurrentRow.Cells(11).Value = CountEnrolled(CInt(dgClassSchedList.CurrentRow.Cells(0).Value))
    End Sub

    Private Sub frmEvaluationSchedules_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub
End Class