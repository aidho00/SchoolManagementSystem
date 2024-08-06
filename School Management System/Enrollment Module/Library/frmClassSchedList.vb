Imports MySql.Data.MySqlClient

Public Class frmClassSchedList

    Private Sub frmSubjectList_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'LibraryClassSchedList()
    End Sub

    Private Sub dgClassSchedList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = CType(sender, DataGridView).Columns(e.ColumnIndex).Name
            If columnName = "colUpdate" Then
                CType(sender, DataGridView).Cursor = Cursors.Hand
            Else
                CType(sender, DataGridView).Cursor = Cursors.Default
            End If
        End If
    End Sub

    Public Sub LoadComboBoxData()
        With frmClassSched
            fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period order by `period_name` desc, `period_status` asc, `period_semester` desc", .cbAcademicYear, "tbl_period", "PERIOD", "period_id")
            fillCombo("SELECT cb_code, cb_id FROM tbl_class_block order by cb_code", .cbSection, "tbl_class_block", "cb_code", "cb_id")
            fillCombo("select CONCAT(emp_last_name,', ',emp_first_name,' ',emp_middle_name) as 'Instructor', emp_id from tbl_employee where is_active = 'Active' order by emp_last_name asc", .cbInstructor, "tbl_employee", "Instructor", "emp_id")
            fillCombo("select (curriculum_code) as 'curriculum', curriculum_id from tbl_curriculum order by curriculum", .cbCur, "tbl_curriculum", "curriculum", "curriculum_id")
            fillCombo("SELECT CONCAT(room_code,'  -  ',room_description) as 'Room', room_id FROM tbl_room order by room_code", .cbRoom, "tbl_room", "Room", "room_id")
            fillCombo("SELECT ds_code, ds_id FROM tbl_day_schedule order by ds_code", .cbDaySched, "tbl_day_schedule", "ds_code", "ds_id")
            'fillCombo("select Subject, Subject_ID from subjectspercurriculum where curr_ID = " & CInt(.cbCur.SelectedValue) & "", .cbSubject, "subjectspercurriculum", "Subject", "Subject_ID")
        End With
    End Sub

    Private Sub LoadDataComboBoxes()
        With frmClassSched
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select cb_code from tbl_class_block where cb_id = " & .ClassSectionID & "", cn)
            .cbSection.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select curriculum_code from tbl_curriculum where curriculum_id = " & .ClassCurID & "", cn)
            .cbCur.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select room from rooms where room_id = " & .ClassRoomID & "", cn)
            .cbRoom.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select ds_code from tbl_day_schedule where ds_id = " & .ClassDaySchedID & "", cn)
            .cbDaySched.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select instructor from employee where emp_id = " & .ClassInstructorID & "", cn)
            .cbInstructor.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select PERIOD from period where period_id = " & .ClassAcadID & "", cn)
            .cbAcademicYear.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select subject from subjectspercurriculum where subject_ID = " & .ClassSubjectID & "", cn)
            .cbSubject.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT ifnull(COUNT(DISTINCT sg_student_id),0) as 'Enrolled' FROM tbl_class_schedule t1 LEFT JOIN tbl_period t7 ON t1.csperiod_id = t7.period_id LEFT JOIN tbl_students_grades t8 ON t1.class_schedule_id = t8.sg_class_id AND t1.csperiod_id = t8.sg_period_id WHERE t1.class_schedule_id = " & .ClassID & " and t1.csperiod_id = " & .ClassAcadID & "", cn)
            .txtEnrolled.Text = cm.ExecuteScalar
            cn.Close()
        End With
    End Sub

    Public Sub LoadComboBoxSubjectData()
        fillCombo("select Subject, Subject_ID from subjectspercurriculum where curr_ID = " & frmClassSched.ClassCurID & "", frmClassSched.cbSubject, "subjectspercurriculum", "Subject", "Subject_ID")
    End Sub
    Private Sub dgClassSchedList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgClassSchedList.CellContentClick

        Dim colname As String = dgClassSchedList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            LoadComboBoxData()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_class_schedule where class_schedule_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgClassSchedList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmClassSched
                    .btnPrev.Visible = True
                    .btnNext.Visible = True
                    .slide1.Visible = False
                    .slide2.Visible = True
                    .slide3.Visible = False
                    '.currentSlideIndex = 0


                    .ClassID = CInt(dr.Item("class_schedule_id").ToString)
                    .ClassCurID = CInt(dr.Item("class_schedule_curriculum").ToString)
                    .ClassSubjectID = CInt(dr.Item("cssubject_id").ToString)
                    .ClassDaySchedID = CInt(dr.Item("days_schedule").ToString)
                    .start_time.Text = dr.Item("time_start_schedule").ToString
                    .end_time.Text = dr.Item("time_end_schedule").ToString
                    .ClassSectionID = CInt(dr.Item("class_block_id").ToString)
                    .ClassRoomID = CInt(dr.Item("csroom_id").ToString)
                    .ClassInstructorID = CInt(dr.Item("csemp_id").ToString)
                    .txtPopulation.Text = dr.Item("population")
                    .ClassAcadID = CInt(dr.Item("csperiod_id").ToString)
                    .cbStatus.Text = dr.Item("is_active").ToString
                    .CbPetition.Text = dr.Item("cs_is_petition").ToString
                    .txtAmount.Text = dr.Item("cs_amount").ToString
                    .cbClassStatus.Text = dr.Item("status").ToString
                    Select Case dr.Item("subject_load_status")
                        Case "0"
                            .cbLoadStatus.Text = "Normal"
                        Case "1"
                            .cbLoadStatus.Text = "Overload"
                        Case "0.5" Or "0.50"
                            .cbLoadStatus.Text = "Half Overload"
                        Case "0.2" Or "0.20"
                            .cbLoadStatus.Text = "Honorarium"
                    End Select
                    .cbPassingGrade.Text = dr.Item("passing_grade").ToString
                    .cbClassStatus.Text = dr.Item("class_status").ToString

                    Dim skipcheck As Integer = 0
                    skipcheck = dr.Item("skipconflictcheck")
                    If skipcheck = 0 Then
                        .CheckBox_skip.Checked = False
                    ElseIf skipcheck = 1 Then
                        .CheckBox_skip.Checked = True
                    End If

                    dr.Close()
                    cn.Close()
                    LoadComboBoxSubjectData()
                    LoadDataComboBoxes()
                    .btnSave.Visible = False
                    .btnUpdate.Visible = True
                    .ClickNext()
                    .ShowDialog()
                End With
            Else

            End If
        Else
            dgClassSchedList.CurrentRow.Cells(12).Value = CountEnrolled(CInt(dgClassSchedList.CurrentRow.Cells(1).Value))
        End If
    End Sub

    Private Sub dgClassSchedList_SelectionChanged(sender As Object, e As EventArgs)

    End Sub
End Class