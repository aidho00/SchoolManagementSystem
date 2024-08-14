Imports MySql.Data.MySqlClient

Public Class frmSupplyPOSStudID
    Dim id As String, price As Double
    Private Sub frmQty_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            frmSupplyPOS.txtItemID.Clear()
            frmSupplyPOS.lblLocation.Text = ""
            frmSupplyPOS.lblLocationNumber.Text = ""
            Me.Dispose()
        ElseIf e.KeyCode = Keys.Enter Then
            txtQty_TextChanged(sender, e)
        End If
    End Sub


    Private Sub frmQty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtStudentID.TextChanged
        If college.Checked = False And highschool.Checked = False Then
            If txtStudentID.Text = "" Then
            Else
                MessageBox.Show("Please select status of student whether he/she is College or Basic Education student. Re-scan/input Student ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtStudentID.Text = ""
            End If
        Else
            If txtStudentID.Text.Length = 7 Then
                cn.Close()
                cn.Open()
                Dim sql As String

                If college.Checked = True Then
                    sql = "SELECT s_id_no FROM cfcissmsdb.tbl_student WHERE s_id_no = " & txtStudentID.Text & ""
                ElseIf highschool.Checked = True Then
                    sql = "SELECT s_id_no FROM cfcissmsdbhighschool.tbl_student WHERE s_id_no = " & txtStudentID.Text & ""
                End If

                cm = New MySqlCommand(sql, cn)
                Dim sdr2 As MySqlDataReader = cm.ExecuteReader()
                If (sdr2.Read() = False) Then
                    MessageBox.Show("Student ID Number " & txtStudentID.Text & " is not registered.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtStudentID.Text = ""
                    cn.Close()
                    sdr2.Dispose()
                Else
                    sdr2.Dispose()
                    frmSupplyPOS.stud_id.Text = txtStudentID.Text
                    Try
                        Dim cmd3 As New MySqlCommand
                        Dim adt3 As New MySqlDataAdapter
                        Dim ds3 As New DataSet
                        cn.Close()
                        cn.Open()
                        Dim sql3 As String
                        If college.Checked = True Then
                            sql3 = "Select CONCAT(s_ln,', ',s_fn,' ',s_mn) as StudentName, s_yr_lvl, s_course_id, course_code, CONCAT(course_code,' - ',course_name) as StudentCourse, s_guardian_name, s_address, s_gender from cfcissmsdb.tbl_student, cfcissmsdb.tbl_course where cfcissmsdb.tbl_student.s_course_id = cfcissmsdb.tbl_course.course_id and cfcissmsdb.tbl_student.s_id_no = '" & txtStudentID.Text & "'"
                        ElseIf highschool.Checked = True Then
                            sql3 = "Select CONCAT(s_ln,', ',s_fn,' ',s_mn) as StudentName, s_yr_lvl, s_course_id, course_code, CONCAT(course_code,' - ',course_name) as StudentCourse, s_guardian_name, s_address, s_gender from cfcissmsdbhighschool.tbl_student, cfcissmsdbhighschool.tbl_course where cfcissmsdbhighschool.tbl_student.s_course_id = cfcissmsdbhighschool.tbl_course.course_id and cfcissmsdbhighschool.tbl_student.s_id_no = '" & txtStudentID.Text & "'"
                        End If
                        adt3 = New MySqlDataAdapter(sql3, cn)
                        cmd3 = New MySqlCommand(sql3)
                        adt3.Fill(ds3, "StudentName")

                        frmSupplyPOS.stud_name.Text = ds3.Tables("StudentName").Rows(0)(0).ToString
                        frmSupplyPOS.stud_yrcourse.Text = ds3.Tables("StudentName").Rows(0)(1).ToString & " - " & ds3.Tables("StudentName").Rows(0)(3).ToString
                        frmSupplyPOS.stud_gender.Text = ds3.Tables("StudentName").Rows(0)(7).ToString
                        cn.Close()

                        If college.Checked = True Then
                            frmSupplyPOS.cs_hs.Text = "cs"
                            fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  cfcissmsdb.tbl_period t1 JOIN cfcissmsdb.tbl_student_paid_account_breakdown t2 ON t1.period_id = t2.spab_period_id where t2.spab_stud_id = '" & txtStudentID.Text & "' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmSupplyPOS.cmb_period, "period", "PERIOD", "period_id")

                        ElseIf highschool.Checked = True Then
                            frmSupplyPOS.cs_hs.Text = "hs"
                            fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  cfcissmsdbhighschool.tbl_period t1 JOIN cfcissmsdbhighschool.tbl_student_paid_account_breakdown t2 ON t1.period_id = t2.spab_period_id where t2.spab_stud_id = '" & txtStudentID.Text & "' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmSupplyPOS.cmb_period, "period", "PERIOD", "period_id")
                        End If
                        Try
                            frmSupplyPOS.cmb_period.SelectedIndex = 0
                        Catch ex As Exception
                        End Try
                    Catch ex As Exception
                    End Try
                    txtStudentID.Text = ""
                    Me.Dispose()
                End If
            End If
        End If

    End Sub



    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStudentID.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 46
            Case 8
            Case 13
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub cb_dropped_Click(sender As Object, e As EventArgs) Handles college.Click
        Try
            If highschool.Checked = True Then
                highschool.Checked = False
                college.Checked = True
            Else
                college.Checked = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub college_CheckedChanged(sender As Object, e As EventArgs) Handles college.CheckedChanged
        txtStudentID.Select()
    End Sub

    Private Sub highschool_CheckedChanged(sender As Object, e As EventArgs) Handles highschool.CheckedChanged
        txtStudentID.Select()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        frmSupplyPOS.txtItemID.Clear()
        frmSupplyPOS.lblLocation.Text = ""
        frmSupplyPOS.lblLocationNumber.Text = ""
        Me.Dispose()
    End Sub

    Private Sub cb_failed_Click(sender As Object, e As EventArgs) Handles highschool.Click
        Try
            If college.Checked = True Then
                college.Checked = False
                highschool.Checked = True
            Else 
                highschool.Checked = True
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class