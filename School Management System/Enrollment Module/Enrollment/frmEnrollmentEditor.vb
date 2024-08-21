Imports MySql.Data.MySqlClient

Public Class frmEnrollmentEditor

    Dim studentGradeLevel As String = ""
    Dim studentGradeLevelCourse As String = ""
    Dim studentGradeLevelCourseName As String = ""
    Dim studentGradeLevelCourseCode As String = ""

    Private Sub YearLevelStudentGradeLevel()
        Try

            'cn.Open()
            'cm = New MySqlCommand("SELECT sg_course_id FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
            'studentCourseId = cm.ExecuteScalar
            'cn.Close()
            'cn.Open()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LibraryWithdrawEnrollmentStudentList()
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        dgStudentList.Rows.Clear()
    End Sub

    Sub LibraryWithdrawEnrollmentStudentList()
        dgStudentList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select DISTINCT(s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (sg_yearlevel) as 'Year Level', (course_code) as 'Course', course_id, course_name, s_course_status from tbl_students_grades JOIN tbl_student ON tbl_students_grades.sg_student_id = tbl_student.s_id_no JOIN tbl_course ON tbl_students_grades.sg_course_id = tbl_course.course_id where tbl_students_grades.sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & " and tbl_students_grades.sg_grade_status = 'Enrolled' and (tbl_student.s_ln like '" & txtSearch.Text & "%' or tbl_student.s_fn like '" & txtSearch.Text & "%' or tbl_student.s_mn like '" & txtSearch.Text & "%' or tbl_student.s_id_no like '" & txtSearch.Text & "%' or tbl_student.s_yr_lvl like '" & txtSearch.Text & "%') order by s_id_no asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("course_name").ToString, dr.Item("s_course_status").ToString)
        End While
        dr.Close()
        cn.Close()

        If frmMain.systemModule.Text = "College Module" Then
            dgStudentList.Columns(8).HeaderText = "Course"
        Else
            dgStudentList.Columns(8).HeaderText = "Strand/Grade"
        End If

        dgPanelPadding(dgStudentList, dgPanel)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If frmMain.systemModule.Text = "High School Module" Then
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to WITHDRAW student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & " ENROLLMENT for academic year " & cbAcademicYear.Text & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If dr = DialogResult.No Then
            Else
                Dim dr2 As DialogResult
                dr2 = MessageBox.Show("Are you REALLY SURE you want to WITHDRAW student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If dr2 = DialogResult.No Then
                Else

                    Try
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT `sg_yearlevel` FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        studentGradeLevel = cm.ExecuteScalar
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT CONCAT(`course_code`,' - ',`course_name`) FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        studentGradeLevelCourse = cm.ExecuteScalar
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT `course_name` FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        studentGradeLevelCourseName = cm.ExecuteScalar
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT `course_code` FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                        studentGradeLevelCourseCode = cm.ExecuteScalar
                        cn.Close()
                    Catch ex As Exception

                    End Try




                    dg_list.DataSource = Nothing
                    load_datagrid("SELECT * from tbl_students_grades where sg_student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    Dim iDate As String = DateToday
                    Dim oDate As DateTime = Convert.ToDateTime(iDate)
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_students_grades (`sg_student_id`, `sg_course_id`, `sg_period_id`, `sg_class_id`, `sg_subject_id`, `sg_grade`, `sg_credits`, `sg_school_id`, `sg_sequence`, `sg_yearlevel`, `sg_grade_status`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.Parameters.AddWithValue("@5", row.Cells(5).Value)
                            cmd.Parameters.AddWithValue("@6", row.Cells(6).Value)
                            cmd.Parameters.AddWithValue("@7", row.Cells(7).Value)
                            cmd.Parameters.AddWithValue("@8", row.Cells(8).Value)
                            cmd.Parameters.AddWithValue("@9", row.Cells(9).Value)
                            cmd.Parameters.AddWithValue("@10", row.Cells(10).Value)
                            cmd.Parameters.AddWithValue("@11", "Withdrawn")
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    query("delete from tbl_students_grades where sg_student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_enrollment where estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_enrollment (`enrollment_code`, `estudent_id`, `eperiod_id`, `ecurrriculum_id`, `etotal_units`, `etotal_subjects`, `eenrolledby_id`, `eenrolledby_datetime`, `e_yearlevel`, `ecourse_id`, `enrollment_status`, `e_withdrawn_by`, `e_withdrawn_date`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.Parameters.AddWithValue("@5", row.Cells(5).Value)
                            cmd.Parameters.AddWithValue("@6", row.Cells(6).Value)
                            cmd.Parameters.AddWithValue("@7", row.Cells(7).Value)
                            cmd.Parameters.AddWithValue("@8", row.Cells(8).Value)
                            cmd.Parameters.AddWithValue("@9", row.Cells(9).Value)
                            cmd.Parameters.AddWithValue("@10", row.Cells(10).Value)
                            cmd.Parameters.AddWithValue("@11", "Withdrawn")
                            cmd.Parameters.AddWithValue("@12", str_userid)
                            cmd.Parameters.AddWithValue("@13", oDate)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    query("delete from tbl_enrollment where estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_student_paid_account_breakdown where spab_stud_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_student_paid_account_breakdown (`spab_period_id`, `spab_stud_id`, `spab_prelim`, `spab_midterm`, `spab_semifinal`, `spab_final`, `spab_add_adjusment`, `spab_less_adjusment`, `spab_total_paid`, `spab_ass_id`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.Parameters.AddWithValue("@5", row.Cells(5).Value)
                            cmd.Parameters.AddWithValue("@6", row.Cells(6).Value)
                            cmd.Parameters.AddWithValue("@7", row.Cells(7).Value)
                            cmd.Parameters.AddWithValue("@8", row.Cells(8).Value)
                            cmd.Parameters.AddWithValue("@9", row.Cells(9).Value)
                            cmd.Parameters.AddWithValue("@10", row.Cells(10).Value)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    query("delete from tbl_student_paid_account_breakdown where spab_stud_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_pre_cashiering where student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And period_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_pre_cashiering (`ornumber`, `student_id`, `pre_cashier_notes`, `approved_by_id`, `approved_by_id_datetime`, `amount_paid`, `amount_received`, `amount_change`, `period_id`, `ps_course_id`, `ps_yrlvl`, `ps_ass_id`, `pre_type`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.Parameters.AddWithValue("@5", row.Cells(5).Value)
                            cmd.Parameters.AddWithValue("@6", row.Cells(6).Value)
                            cmd.Parameters.AddWithValue("@7", row.Cells(7).Value)
                            cmd.Parameters.AddWithValue("@8", row.Cells(8).Value)
                            cmd.Parameters.AddWithValue("@9", row.Cells(9).Value)
                            cmd.Parameters.AddWithValue("@10", row.Cells(10).Value)
                            cmd.Parameters.AddWithValue("@11", row.Cells(11).Value)
                            cmd.Parameters.AddWithValue("@12", row.Cells(12).Value)
                            cmd.Parameters.AddWithValue("@13", row.Cells(13).Value)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    query("delete from tbl_pre_cashiering where student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_cashiering where csh_stud_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And csh_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_cashiering (`csh_ornumber`, `csh_period_id`, `csh_stud_id`, `csh_total_amount`, `csh_amount_received`, `csh_amount_change`, `csh_type`, `csh_date`, `csh_notes`, `csh_cashier_id`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.Parameters.AddWithValue("@5", row.Cells(5).Value)
                            cmd.Parameters.AddWithValue("@6", row.Cells(6).Value)
                            cmd.Parameters.AddWithValue("@7", row.Cells(8).Value)
                            cmd.Parameters.AddWithValue("@8", row.Cells(9).Value)
                            cmd.Parameters.AddWithValue("@9", row.Cells(10).Value)
                            cmd.Parameters.AddWithValue("@10", row.Cells(11).Value)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    query("delete from tbl_cashiering where csh_stud_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And csh_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_assessment_institutional_discount where aid_student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_assessment_institutional_discount (`aid_student_id`, `aid_period_id`, `aid_percentage`, `aid_assessment_id`) values (@1, @2, @3, @4)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    query("delete from tbl_assessment_institutional_discount where aid_student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    StudentCOR()
                    MessageBox.Show("Student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & " with ID Number '" & dgStudentList.CurrentRow.Cells(1).Value & "' enrollment for " & cbAcademicYear.Text & " has been successfully dropped.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    UserActivity("Withdrawn student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & "  enrollment in Academic Year " & cbAcademicYear.Text & ".", "ENROLLLMENT WITHDRAW")
                End If
            End If
        ElseIf frmMain.systemModule.Text = "College Module" Then
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to WITHDRAW student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & " ENROLLMENT for academic year " & cbAcademicYear.Text & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If dr = DialogResult.No Then
            Else
                Dim dr2 As DialogResult
                dr2 = MessageBox.Show("Are you REALLY SURE you want to WITHDRAW student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If dr2 = DialogResult.No Then
                Else

                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_students_grades where sg_student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    Dim iDate As String = DateToday
                    Dim oDate As DateTime = Convert.ToDateTime(iDate)
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_students_grades (`sg_student_id`, `sg_course_id`, `sg_period_id`, `sg_class_id`, `sg_subject_id`, `sg_grade`, `sg_credits`, `sg_school_id`, `sg_sequence`, `sg_yearlevel`, `sg_grade_status`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.Parameters.AddWithValue("@5", row.Cells(5).Value)
                            cmd.Parameters.AddWithValue("@6", row.Cells(6).Value)
                            cmd.Parameters.AddWithValue("@7", row.Cells(7).Value)
                            cmd.Parameters.AddWithValue("@8", row.Cells(8).Value)
                            cmd.Parameters.AddWithValue("@9", row.Cells(14).Value)
                            cmd.Parameters.AddWithValue("@10", row.Cells(15).Value)
                            cmd.Parameters.AddWithValue("@11", "Withdrawn")
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_enrollment where estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_enrollment (`enrollment_code`, `estudent_id`, `eperiod_id`, `ecurrriculum_id`, `etotal_units`, `etotal_subjects`, `eenrolledby_id`, `eenrolledby_datetime`, `e_yearlevel`, `ecourse_id`, `enrollment_status`, `e_withdrawn_by`, `e_withdrawn_date`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.Parameters.AddWithValue("@5", row.Cells(5).Value)
                            cmd.Parameters.AddWithValue("@6", row.Cells(6).Value)
                            cmd.Parameters.AddWithValue("@7", row.Cells(7).Value)
                            cmd.Parameters.AddWithValue("@8", row.Cells(8).Value)
                            cmd.Parameters.AddWithValue("@9", row.Cells(9).Value)
                            cmd.Parameters.AddWithValue("@10", row.Cells(10).Value)
                            cmd.Parameters.AddWithValue("@11", "Withdrawn")
                            cmd.Parameters.AddWithValue("@12", str_userid)
                            cmd.Parameters.AddWithValue("@13", oDate)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT * FROM tbl_period WHERE period_enrollment_enddate >= CURDATE() AND period_id  = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                    Dim sdr0 As MySqlDataReader = cm.ExecuteReader()
                    If (sdr0.Read() = True) Then
                        sdr0.Close()
                        cn.Close()
                        query("delete from tbl_students_grades where sg_student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                        query("delete from tbl_enrollment where estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    Else
                        sdr0.Close()
                        cn.Close()
                        query("Update tbl_students_grades set sg_grade = 'W', sg_credits = '0' where sg_student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                        query("update tbl_enrollment set enrollment_status = 'Withdrawn', ewithdrawn_by = " & str_userid & ", ewithdrawn_datetime = NOW() where estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    End If


                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_student_paid_account_breakdown where spab_stud_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_student_paid_account_breakdown (`spab_period_id`, `spab_stud_id`, `spab_prelim`, `spab_midterm`, `spab_semifinal`, `spab_final`, `spab_add_adjusment`, `spab_less_adjusment`, `spab_total_paid`, `spab_ass_id`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.Parameters.AddWithValue("@5", row.Cells(5).Value)
                            cmd.Parameters.AddWithValue("@6", row.Cells(6).Value)
                            cmd.Parameters.AddWithValue("@7", row.Cells(7).Value)
                            cmd.Parameters.AddWithValue("@8", row.Cells(8).Value)
                            cmd.Parameters.AddWithValue("@9", row.Cells(9).Value)
                            cmd.Parameters.AddWithValue("@10", row.Cells(10).Value)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    query("delete from tbl_student_paid_account_breakdown where spab_stud_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_pre_cashiering where student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And period_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_pre_cashiering (`ornumber`, `student_id`, `pre_cashier_notes`, `approved_by_id`, `approved_by_id_datetime`, `amount_paid`, `amount_received`, `amount_change`, `period_id`, `ps_course_id`, `ps_yrlvl`, `ps_ass_id`, `pre_type`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.Parameters.AddWithValue("@5", row.Cells(5).Value)
                            cmd.Parameters.AddWithValue("@6", row.Cells(6).Value)
                            cmd.Parameters.AddWithValue("@7", row.Cells(7).Value)
                            cmd.Parameters.AddWithValue("@8", row.Cells(8).Value)
                            cmd.Parameters.AddWithValue("@9", row.Cells(9).Value)
                            cmd.Parameters.AddWithValue("@10", row.Cells(10).Value)
                            cmd.Parameters.AddWithValue("@11", row.Cells(11).Value)
                            cmd.Parameters.AddWithValue("@12", row.Cells(12).Value)
                            cmd.Parameters.AddWithValue("@13", row.Cells(13).Value)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    query("delete from tbl_pre_cashiering where student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_cashiering where csh_stud_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And csh_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_cashiering (`csh_ornumber`, `csh_period_id`, `csh_stud_id`, `csh_total_amount`, `csh_amount_received`, `csh_amount_change`, `csh_type`, `csh_date`, `csh_notes`, `csh_cashier_id`) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.Parameters.AddWithValue("@5", row.Cells(5).Value)
                            cmd.Parameters.AddWithValue("@6", row.Cells(6).Value)
                            cmd.Parameters.AddWithValue("@7", row.Cells(8).Value)
                            cmd.Parameters.AddWithValue("@8", row.Cells(9).Value)
                            cmd.Parameters.AddWithValue("@9", row.Cells(10).Value)
                            cmd.Parameters.AddWithValue("@10", row.Cells(11).Value)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    query("delete from tbl_cashiering where csh_stud_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And csh_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    dg_list.DataSource = Nothing
                    load_datagrid("Select * from tbl_assessment_institutional_discount where aid_student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_list)

                    cn.Close()
                    cn.Open()
                    For Each row As DataGridViewRow In dg_list.Rows
                        Using cmd As New MySqlCommand("INSERT INTO tbl_withdraw_assessment_institutional_discount (`aid_student_id`, `aid_period_id`, `aid_percentage`, `aid_assessment_id`) values (@1, @2, @3, @4)", cn)
                            cmd.Parameters.AddWithValue("@1", row.Cells(1).Value)
                            cmd.Parameters.AddWithValue("@2", row.Cells(2).Value)
                            cmd.Parameters.AddWithValue("@3", row.Cells(3).Value)
                            cmd.Parameters.AddWithValue("@4", row.Cells(4).Value)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End Using
                    Next
                    cn.Close()
                    cn.Open()

                    query("delete from tbl_assessment_institutional_discount where aid_student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' And aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    StudentCOR()
                    MessageBox.Show("Student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(2).Value & " with ID Number '" & dgStudentList.CurrentRow.Cells(1).Value & "' enrollment for " & cbAcademicYear.Text & " has been successfully dropped.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    UserActivity("Withdrawn student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(2).Value & "  enrollment in Academic Year " & cbAcademicYear.Text & ".", "ENROLLLMENT WITHDRAW")
                End If
            End If
        End If
    End Sub


    Sub StudentCOR()
        ReportPanel.Visible = True
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT * from tbl_enrollment where estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
        Dim sdr2 As MySqlDataReader = cm.ExecuteReader()
        If (sdr2.Read() = True) Then
            sdr2.Dispose()
            cn.Close()
            cn.Open()
            Try
                load_datagrid("Select (class_schedule_id) As 'ID', (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (subject_units) as 'Units', if(ds_code = 'M T W TH F SAT SUN', 'DAILY', ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (room_code) as 'Room', (Instructor) as 'Instructor', DATE_FORMAT(tbl_enrollment.eenrolledby_datetime,'%M %d, %Y') as 'DateEnrolled', CONCAT(tbl_user_account.ua_first_name,' ',tbl_user_account.ua_middle_name, ' ', tbl_user_account.ua_last_name) as 'EnrolledBy', ewithdrawn_datetime from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_enrollment, tbl_students_grades, tbl_user_account where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_students_grades.sg_class_id and tbl_enrollment.estudent_id = tbl_students_grades.sg_student_id and tbl_class_schedule.csperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eperiod_id = tbl_students_grades.sg_period_id and tbl_enrollment.eenrolledby_id = tbl_user_account.ua_id and tbl_enrollment.estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and tbl_enrollment.eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "  order by Days asc, STR_TO_DATE(`Start Time`,'%l:%i:%s %p') asc", dg_report)
                Dim dt As New DataTable
                With dt
                    .Columns.Add("cb_code")
                    .Columns.Add("subject_code")
                    .Columns.Add("subject_description")
                    .Columns.Add("ds_code")
                    .Columns.Add("time_start_schedule")
                    .Columns.Add("time_end_schedule")
                    .Columns.Add("subject_units")
                    .Columns.Add("room_code")
                    .Columns.Add("instructor")
                    .Columns.Add("eenrolledby_datetime")
                End With
                For Each dr As DataGridViewRow In dg_report.Rows
                    dt.Rows.Add(dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(4).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value)
                Next
                Dim iDate As String = DateToday
                Dim oDate As DateTime = Convert.ToDateTime(iDate)
                Dim iDate2 As String
                Dim oDate2 As DateTime

                Try
                    iDate2 = dg_report.Rows(0).Cells(12).Value.ToString()
                    oDate2 = Convert.ToDateTime(iDate2)
                Catch ex As Exception

                End Try

                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New Enrollment_Student_COR
                rptdoc.SetDataSource(dt)
                rptdoc.SetParameterValue("studentname", dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & " " & dgStudentList.CurrentRow.Cells(2).Value)
                rptdoc.SetParameterValue("studentcourse", dgStudentList.CurrentRow.Cells(10).Value)
                rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                rptdoc.SetParameterValue("studentyearlevel", dgStudentList.CurrentRow.Cells(7).Value)
                rptdoc.SetParameterValue("studentidnumber", dgStudentList.CurrentRow.Cells(1).Value)
                rptdoc.SetParameterValue("enrolledby", dg_report.CurrentRow.Cells(11).Value)
                rptdoc.SetParameterValue("enrolleddate", dg_report.CurrentRow.Cells(10).Value)
                rptdoc.SetParameterValue("dategenerated", oDate.ToString("MMMM' 'dd', 'yyyy"))
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT * from tbl_enrollment where estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & " and enrollment_status = 'Withdrawn'", cn)
                Dim sdr3 As MySqlDataReader = cm.ExecuteReader()
                If (sdr3.Read() = True) Then
                    rptdoc.SetParameterValue("enrollment_status", "WITHDRAWN")
                    rptdoc.SetParameterValue("wdate", "Date Withdrawn:")
                    rptdoc.SetParameterValue("wdate2", oDate2.ToString("MMMM' 'dd', 'yyyy"))
                Else
                    rptdoc.SetParameterValue("enrollment_status", " ")
                    rptdoc.SetParameterValue("wdate", " ")
                    rptdoc.SetParameterValue("wdate2", " ")
                End If
                cn.Close()

                ReportViewer.ReportSource = rptdoc
                dg_report.DataSource = Nothing
                ReportViewer.Select()
            Catch ex As Exception
                cn.Close()
            End Try
            cn.Close()
        Else
            sdr2.Dispose()
            cn.Close()
            cn.Open()
            Try

                cm = New MySqlCommand("SELECT * from tbl_withdraw_enrollment where estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                Dim sdr3 As MySqlDataReader = cm.ExecuteReader()
                If (sdr3.Read() = True) Then
                    sdr2.Dispose()
                    cn.Close()

                    load_datagrid("Select (class_schedule_id) As 'ID', (cb_code) as 'Class', (subject_code) as 'Subject Code', (subject_description) as 'Subject Desc.', (subject_units) as 'Units', (ds_code) as 'Days', (time_start_schedule) as 'Start Time', (time_end_schedule) as 'End Time', (room_code) as 'Room', (Instructor) as 'Instructor', DATE_FORMAT(tbl_withdraw_enrollment.eenrolledby_datetime,'%M %d, %Y') as 'DateEnrolled', CONCAT(tbl_user_account.ua_first_name,' ',tbl_user_account.ua_middle_name, ' ', tbl_user_account.ua_last_name) as 'EnrolledBy', e_withdrawn_date from tbl_class_schedule, tbl_class_block, tbl_subject, tbl_day_schedule, tbl_room, employee, tbl_withdraw_enrollment, tbl_withdraw_students_grades, tbl_user_account where tbl_class_schedule.class_block_id = tbl_class_block.cb_id and tbl_class_schedule.cssubject_id = tbl_subject.subject_id and tbl_class_schedule.days_schedule = tbl_day_schedule.ds_id and tbl_class_schedule.csroom_id = tbl_room.room_id and tbl_class_schedule.csemp_id = employee.emp_id and tbl_class_schedule.class_schedule_id = tbl_withdraw_students_grades.sg_class_id and tbl_withdraw_enrollment.estudent_id = tbl_withdraw_students_grades.sg_student_id and tbl_class_schedule.csperiod_id = tbl_withdraw_students_grades.sg_period_id and tbl_withdraw_enrollment.eperiod_id = tbl_withdraw_students_grades.sg_period_id and tbl_withdraw_enrollment.eenrolledby_id = tbl_user_account.ua_id and tbl_withdraw_enrollment.estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and tbl_withdraw_enrollment.eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "", dg_report)
                    Dim dt As New DataTable
                    With dt
                        .Columns.Add("cb_code")
                        .Columns.Add("subject_code")
                        .Columns.Add("subject_description")
                        .Columns.Add("ds_code")
                        .Columns.Add("time_start_schedule")
                        .Columns.Add("time_end_schedule")
                        .Columns.Add("subject_units")
                        .Columns.Add("room_code")
                        .Columns.Add("instructor")
                        .Columns.Add("eenrolledby_datetime")
                    End With
                    For Each dr As DataGridViewRow In dg_report.Rows
                        dt.Rows.Add(dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(4).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value)
                    Next
                    Dim iDate As String = DateToday
                    Dim oDate As DateTime = Convert.ToDateTime(iDate)

                    Dim iDate2 As String = dg_report.Rows(0).Cells(12).Value.ToString
                    Dim oDate2 As DateTime = Convert.ToDateTime(iDate2)
                    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rptdoc = New Enrollment_Student_COR
                    rptdoc.SetDataSource(dt)
                    rptdoc.SetParameterValue("studentname", dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & " " & dgStudentList.CurrentRow.Cells(2).Value)
                    rptdoc.SetParameterValue("studentcourse", dgStudentList.CurrentRow.Cells(10).Value)
                    rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                    rptdoc.SetParameterValue("studentyearlevel", dgStudentList.CurrentRow.Cells(7).Value)
                    rptdoc.SetParameterValue("studentidnumber", dgStudentList.CurrentRow.Cells(1).Value)
                    rptdoc.SetParameterValue("enrolledby", dg_report.CurrentRow.Cells(11).Value)
                    rptdoc.SetParameterValue("enrolleddate", dg_report.CurrentRow.Cells(10).Value)
                    rptdoc.SetParameterValue("dategenerated", oDate.ToString("MMMM' 'dd', 'yyyy"))
                    rptdoc.SetParameterValue("enrollment_status", "WITHDRAWN")
                    rptdoc.SetParameterValue("wdate", "Date Withdrawn:")
                    rptdoc.SetParameterValue("wdate2", oDate2.ToString("MMMM' 'dd', 'yyyy"))
                    ReportViewer.ReportSource = rptdoc
                    dg_report.DataSource = Nothing
                    ReportViewer.Select()
                Else
                    ReportViewer.ReportSource = Nothing
                    MessageBox.Show("Student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & " with ID Number " & dgStudentList.CurrentRow.Cells(1).Value & " is not enrolled in Academic Year " & cbAcademicYear.Text & ".", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception
                cn.Open()
            End Try
            cn.Open()
        End If
    End Sub

    Private Sub frmEnrollmentEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If frmMain.systemModule.Text = "High School Module" Then
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to RESET student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & " ENROLLMENT for academic year " & cbAcademicYear.Text & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If dr = DialogResult.No Then
            Else
                Dim dr2 As DialogResult
                dr2 = MessageBox.Show("Are you REALLY SURE you want to RESET student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If dr2 = DialogResult.No Then
                Else

                    query("delete from tbl_students_grades where sg_student_id = " & dgStudentList.CurrentRow.Cells(1).Value & " And sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    query("delete from tbl_enrollment where estudent_id = " & dgStudentList.CurrentRow.Cells(1).Value & " And eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    query("delete from tbl_student_paid_account_breakdown where spab_stud_id = " & dgStudentList.CurrentRow.Cells(1).Value & " And spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    query("delete from tbl_assessment_institutional_discount where aid_student_id = " & dgStudentList.CurrentRow.Cells(1).Value & " And aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    MessageBox.Show("Student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & " with ID Number '" & dgStudentList.CurrentRow.Cells(1).Value & "' enrollment for " & cbAcademicYear.Text & " has been successfully reset.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    UserActivity("Reset student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & "  enrollment in Academic Year " & cbAcademicYear.Text & ".", "ENROLLLMENT RESET")
                End If
            End If
        ElseIf frmMain.systemModule.Text = "College Module" Then
            Dim dr As DialogResult
            dr = MessageBox.Show("Are you sure you want to RESET student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & " ENROLLMENT for academic year " & cbAcademicYear.Text & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If dr = DialogResult.No Then
            Else
                Dim dr2 As DialogResult
                dr2 = MessageBox.Show("Are you REALLY SURE you want to RESET student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If dr2 = DialogResult.No Then
                Else

                    query("delete from tbl_students_grades where sg_student_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and sg_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    query("delete from tbl_enrollment where estudent_id = '" & dgStudentList.CurrentRow.Cells(1).Value & "' and eperiod_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    query("delete from tbl_student_paid_account_breakdown where spab_stud_id = " & dgStudentList.CurrentRow.Cells(1).Value & " And spab_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")
                    query("delete from tbl_assessment_institutional_discount where aid_student_id = " & dgStudentList.CurrentRow.Cells(1).Value & " And aid_period_id = " & CInt(cbAcademicYear.SelectedValue) & "")

                    MessageBox.Show("Student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(2).Value & " with ID Number '" & dgStudentList.CurrentRow.Cells(1).Value & "' enrollment for " & cbAcademicYear.Text & " has been successfully reset.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    UserActivity("Reset student " & dgStudentList.CurrentRow.Cells(2).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(2).Value & "  enrollment in Academic Year " & cbAcademicYear.Text & ".", "ENROLLLMENT RESET")
                End If
            End If
        End If
    End Sub
End Class