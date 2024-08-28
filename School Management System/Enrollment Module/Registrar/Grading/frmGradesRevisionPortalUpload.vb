Imports MySql.Data.MySqlClient
Imports System.ComponentModel

Public Class frmGradesRevisionPortalUpload

    Dim strPath As String

    Private Sub frmGradesRevisionPortalUpload_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        btnCancel.PerformClick()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        dgStudentList.Rows.Clear()
        dgDummyList.DataSource = Nothing

        Dim con As OleDb.OleDbConnection
        Dim cmdd As New OleDb.OleDbCommand
        Dim da As New OleDb.OleDbDataAdapter
        Dim dt As New DataTable
        Try
            With OpenFileDialog1
                .Filter = "Excel files(.xls)|*.xls| Excel files(*.xlsx)|*.xlsx| All files (*.*)|*.*"
                .FilterIndex = 1
                .Title = "Import grading sheet data from Excel file"
            End With
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                strPath = OpenFileDialog1.FileName
                con = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" & strPath & " ; " & "Extended Properties=Excel 8.0;")
                con.Open()
                With cmdd
                    .Connection = con
                    .CommandText = "select * from [Sheet1$]"
                End With
                da.SelectCommand = cmdd
                da.Fill(dt)
                dgDummyList.Columns.Clear()
                dgDummyList.DataSource = dt
                con.Close()

                dgStudentList.Rows.Clear()

                Dim i As Integer
                For Each row As DataGridViewRow In dgDummyList.Rows

                    Dim grade As String
                    Try
                        Dim finalgrade = row.Cells(5).Value.ToString

                        If row.Cells(5).Value Is DBNull.Value Then
                            grade = ""
                        ElseIf finalgrade = "D" Then
                            grade = row.Cells(5).Value
                        ElseIf finalgrade >= 94 Then
                            grade = "1.1"
                        ElseIf finalgrade = 93 Then
                            grade = "1.2"
                        ElseIf finalgrade = 92 Then
                            grade = "1.3"
                        ElseIf finalgrade = 91 Then
                            grade = "1.4"
                        ElseIf finalgrade = 90 Then
                            grade = "1.5"
                        ElseIf finalgrade = 89 Then
                            grade = "1.6"
                        ElseIf finalgrade = 88 Then
                            grade = "1.7"
                        ElseIf finalgrade = 87 Then
                            grade = "1.8"
                        ElseIf finalgrade = 86 Then
                            grade = "1.9"
                        ElseIf finalgrade = 85 Then
                            grade = "2.0"
                        ElseIf finalgrade = 84 Then
                            grade = "2.1"
                        ElseIf finalgrade = 83 Then
                            grade = "2.2"
                        ElseIf finalgrade = 82 Then
                            grade = "2.3"
                        ElseIf finalgrade = 81 Then
                            grade = "2.4"
                        ElseIf finalgrade = 80 Then
                            grade = "2.5"
                        ElseIf finalgrade = 79 Then
                            grade = "2.6"
                        ElseIf finalgrade = 78 Then
                            grade = "2.7"
                        ElseIf finalgrade = 77 Then
                            grade = "2.8"
                        ElseIf finalgrade = 76 Then
                            grade = "2.9"
                        ElseIf finalgrade = 75 Then
                            grade = "3.0"
                        ElseIf finalgrade < 75 Then
                            grade = "5.0"
                        Else
                            grade = ""
                        End If
                    Catch ex As Exception
                        grade = ""
                    End Try

                    Dim curGrade As String = ""
                    Try
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT sg_grade as Name from tbl_students_grades where sg_student_id = '" & row.Cells(1).Value & "'", cn)
                        curGrade = cm.ExecuteScalar
                        cn.Close()
                    Catch ex As Exception
                        curGrade = ""
                    End Try

                    i += 1
                    dgStudentList.Rows.Add(i, row.Cells(0).Value.ToString, row.Cells(4).Value.ToString, row.Cells(8).Value.ToString, row.Cells(1).Value.ToString, "", curGrade, grade)
                    'dgStudentList.Rows.Add(i, row.Cells(0).Value.ToString, CInt(cbAcademicYear.SelectedValue), row.Cells(7).Value.ToString, row.Cells(1).Value.ToString, "-", "", "", "", "", row.Cells(10).Value.ToString, grade)
                Next
                MsgBox("Number of imported rows: " & dgStudentList.RowCount & ".", vbInformation)

                If MsgBox("Do you want to load the names of the students?", vbYesNo + vbQuestion) = vbYes Then
                    studentName.Visible = True
                    studentID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    StudentNamesStartUpdateProcess()
                Else
                    studentName.Visible = False
                    studentID.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If

                'Try
                '    For Each row As DataGridViewRow In dgStudentList.Rows
                '        cn.Close()
                '        cn.Open()
                '        cm = New MySqlCommand("SELECT CONCAT(s_ln, ', ',s_fn, ' ', s_mn) as Name from tbl_student where s_id_no = '" & row.Cells(4).Value & "'", cn)
                '        row.Cells(5).Value = cm.ExecuteScalar
                '        cn.Close()
                '    Next
                'Catch ex As Exception
                'End Try

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        dgDummyList.DataSource = Nothing
    End Sub


    Private Sub StudentNamesStartUpdateProcess()
        If Not BackgroundWorker2.IsBusy Then
            BackgroundWorker2.RunWorkerAsync(dgStudentList)
        End If
    End Sub

    Private Sub StudentNamesUpdateDatabaseForRow(row As DataGridViewRow)
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT CONCAT(s_ln, ', ',s_fn, ' ', s_mn) as Name from tbl_student where s_id_no = '" & row.Cells(4).Value & "'", cn)
        row.Cells(5).Value = cm.ExecuteScalar
        cn.Close()
    End Sub


    Private Sub BackgroundWorker2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Dim dgv As DataGridView = CType(e.Argument, DataGridView)

        For Each row As DataGridViewRow In dgv.Rows
            If BackgroundWorker2.CancellationPending Then
                e.Cancel = True
                Exit For
            End If

            StudentNamesUpdateDatabaseForRow(row)

            Dim progressPercentage As Integer = (row.Index + 1) * 100 / dgv.Rows.Count
            BackgroundWorker2.ReportProgress(progressPercentage, row.Index + 1)
        Next
    End Sub

    Private Sub BackgroundWorker2_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker2.ProgressChanged
        lblProgress.Text = $"Progress: {e.UserState}/{dgStudentList.Rows.Count}"
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        If e.Error IsNot Nothing Then
            MsgBox($"An error occurred: {e.Error.Message}", vbCritical)
        ElseIf e.Cancelled Then
            MsgBox("Showing students names was canceled.", vbExclamation)
        Else
            MsgBox("Showing students names completed successfully.", vbInformation)
        End If

        lblProgress.Text = "Progress: 0/0"
        btnImport.Enabled = True
        btnSave.Enabled = True
        cbAcademicYear.Enabled = True
        dgStudentList.AllowUserToDeleteRows = True
        loading.Visible = False

    End Sub




    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If MsgBox("Are you sure you want to upload these revised grades?", vbYesNo + vbQuestion) = vbYes Then
            dgStudentList.AllowUserToDeleteRows = False
            btnImport.Enabled = False
            btnSave.Enabled = False
            cbAcademicYear.Enabled = False
            loading.Visible = True
            StartUpdateProcess()
        Else

        End If
    End Sub

    Private Sub UpdateDatabaseForRow(row As DataGridViewRow)

        cn2.Close()
        cn2.Open()
        cm2 = New MySqlCommand("SELECT sg_grade from tbl_students_grades where sg_student_id = '" & row.Cells(4).Value & "' and sg_period_id = " & CInt(row.Cells(2).Value) & " and sg_class_id = " & CInt(row.Cells(1).Value) & " and sg_grade_status IN ('W', 'D')", cn2)
        dr2 = cm2.ExecuteReader
        dr2.Read()
        If dr2.HasRows Then
            dr2.Close()
            cn2.Close()
        Else
            cm2.Dispose()
            dr2.Close()
            cn2.Close()
            dr2.Close()
            Dim subj_id As String
            Dim credits As String
            cn2.Close()
            cn2.Open()
            cm2 = New MySqlCommand("SELECT t1.cssubject_id, t2.subject_units from tbl_class_schedule t1 JOIN tbl_subject t2 ON t1.cssubject_id = t2.subject_id where class_schedule_id = '" & row.Cells(1).Value & "'", cn2)
            dr2 = cm2.ExecuteReader
            dr2.Read()
            If dr2.HasRows Then
                subj_id = dr2.Item("cssubject_id").ToString
                credits = dr2.Item("subject_units").ToString
            Else
            End If
            dr2.Close()
            cn2.Close()
            query2("UPDATE tbl_students_grades set sg_grade = '" & row.Cells(7).Value & "', sg_credits = if('" & row.Cells(7).Value & "' = 'D' or '" & row.Cells(7).Value & "' = 5,0,'" & credits & "'), sg_grade_addedby = " & str_userid & ", sg_grade_dateadded = CURDATE() where sg_student_id = '" & row.Cells(4).Value & "' and sg_period_id = " & CInt(row.Cells(2).Value) & " and sg_class_id = " & CInt(row.Cells(1).Value) & "' and sg_grade_status = 'Enrolled'")
        End If
    End Sub

    Private Sub StartUpdateProcess()
        If Not BackgroundWorker1.IsBusy Then
            BackgroundWorker1.RunWorkerAsync(dgStudentList)
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim dgv As DataGridView = CType(e.Argument, DataGridView)

        For Each row As DataGridViewRow In dgv.Rows
            If BackgroundWorker1.CancellationPending Then
                e.Cancel = True
                Exit For
            End If

            UpdateDatabaseForRow(row)

            Dim progressPercentage As Integer = (row.Index + 1) * 100 / dgv.Rows.Count
            BackgroundWorker1.ReportProgress(progressPercentage, row.Index + 1)
        Next
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        lblProgress.Text = $"Progress: {e.UserState}/{dgStudentList.Rows.Count}"
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If e.Error IsNot Nothing Then
            MsgBox($"An error occurred: {e.Error.Message}", vbCritical)
        ElseIf e.Cancelled Then
            MsgBox("Uploading grades was canceled.", vbExclamation)
        Else
            MsgBox("Uploading grades completed successfully.", vbInformation)
        End If

        lblProgress.Text = "Progress: 0/0"
        btnImport.Enabled = True
        btnSave.Enabled = True
        cbAcademicYear.Enabled = True
        dgStudentList.AllowUserToDeleteRows = True
        loading.Visible = False
        dgStudentList.Rows.Clear()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If BackgroundWorker1.IsBusy Then
            BackgroundWorker1.CancelAsync()
            lblProgress.Text = "Progress: 0/0"
            btnImport.Enabled = True
            btnSave.Enabled = True
            cbAcademicYear.Enabled = True
            loading.Visible = False
            dgStudentList.AllowUserToDeleteRows = True
        Else
            If MsgBox("Are you sure you want to cancel uploading grades?", vbYesNo + vbQuestion) = vbYes Then
                Me.Close()
            End If
        End If
    End Sub

    Private Sub frmGradesPortalUpload_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If BackgroundWorker1.IsBusy Then
            BackgroundWorker1.CancelAsync()
            lblProgress.Text = "Progress: 0/0"
            btnImport.Enabled = True
            btnSave.Enabled = True
            cbAcademicYear.Enabled = True
            loading.Visible = False
            dgStudentList.AllowUserToDeleteRows = True
        Else

        End If
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        dgStudentList.Rows.Clear()
    End Sub

    Private Sub cbAcademicYear_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbAcademicYear.KeyPress
        e.Handled = True
    End Sub

    Private Sub dgStudentList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgStudentList.CellClick

    End Sub
End Class