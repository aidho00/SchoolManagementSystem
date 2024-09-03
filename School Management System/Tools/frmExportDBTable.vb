Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Runtime.InteropServices

Public Class frmExportDBTable
    ' Windows API functions
    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As Integer) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function

    Private Const SW_RESTORE As Integer = 9

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim drr As DialogResult
        drr = MessageBox.Show("Are you sure you want export this data?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If drr = DialogResult.No Then
        Else
            If cbData.Text = "" Then
                MsgBox("Please select a data to export.", vbCritical)
                Return
            End If
            cn.Close()
            cn.Open()
            Dim sql As String = ""
            Select Case cbData.Text
                Case "Student Assessment"
                    sql = "SELECT * FROM `tbl_student_paid_account_breakdown` WHERE `spab_period_id` = " & CInt(cbAcademicYear.SelectedValue) & ""
                Case "Student Institutional Discount"
                    sql = "SELECT * FROM `tbl_assessment_institutional_discount` WHERE `aid_period_id` = " & CInt(cbAcademicYear.SelectedValue) & ""
                Case "Student Downpayments"
                    sql = "SELECT * FROM `tbl_pre_cashiering` WHERE `period_id` = " & CInt(cbAcademicYear.SelectedValue) & " and approved_by_id_datetime BETWEEN '" & dtFrom.Text & "' and '" & dtTo.Text & "'"
                Case "Student Payments"
                    sql = "SELECT * FROM `tbl_cashiering` WHERE `csh_period_id` = " & CInt(cbAcademicYear.SelectedValue) & " and csh_date BETWEEN '" & dtFrom.Text & "' and '" & dtTo.Text & "'"
                Case "Student Enrollment"
                    sql = "SELECT * FROM `tbl_enrollment` WHERE `eperiod_id` = " & CInt(cbAcademicYear.SelectedValue) & ""
                Case "Student Information"
                    sql = "SELECT * FROM `tbl_student`"
                Case "Student Grades"
                    sql = "SELECT * FROM `tbl_students_grades` WHERE `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & ""
            End Select

            load_datagrid(sql, dg_report)

            Dim csv As String = String.Empty
            For Each column As DataGridViewColumn In dg_report.Columns
                csv += column.HeaderText & ","c
            Next
            csv += vbCr & vbLf
            For Each row2 As DataGridViewRow In dg_report.Rows
                For Each cell As DataGridViewCell In row2.Cells
                    csv += cell.Value.ToString().Replace(",", ";") & ","c
                Next
                csv += vbCr & vbLf
            Next

            Dim proc As New System.Diagnostics.Process()
            Dim folderPath As String
            Dim SavedPath As String
            folderPath = "C:\Exported Database Table\" & cbAcademicYear.Text & "\"
            SavedPath = "C:\Exported Database Table\" & cbAcademicYear.Text & "\"


                Dim d1 As String = dtFrom.Text
            Dim dttfrom As String = d1.Replace("/", "-")
            Dim d2 As String = dtTo.Text
            Dim dttto As String = d2.Replace("/", "-")

            Dim DataFilter As String = ""
            If cbData.Text = "Student Downpayments" Or cbData.Text = "Student Payments" Then
                DataFilter = "" & cbAcademicYear.Text & "-" & dttfrom & "-" & dttto & ""
            Else
                DataFilter = "" & cbAcademicYear.Text & ""
            End If

            If frmMain.systemModule.Text = "College Module" Then
                If Not IO.Directory.Exists(folderPath) Then
                    IO.Directory.CreateDirectory(folderPath)
                    File.WriteAllText(folderPath & "College " & cbData.Text & "-" & DataFilter & ".csv", csv)

                    MessageBox.Show("College " & cbData.Text & " Data successfully exported. The file has been successfully saved in the following location:" & Environment.NewLine & SavedPath, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    File.WriteAllText(folderPath & "College " & cbData.Text & "-" & DataFilter & ".csv", csv)

                    MessageBox.Show("College " & cbData.Text & " Data successfully exported. The file has been successfully saved in the following location:" & Environment.NewLine & SavedPath, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                UserActivity("Exported College " & cbData.Text & " Data-" & DataFilter & ".", "DATABASE TABLE EXPORT")

            Else
                If Not IO.Directory.Exists(folderPath) Then
                    IO.Directory.CreateDirectory(folderPath)
                    File.WriteAllText(folderPath & "HighSchool " & cbData.Text & "-" & DataFilter & ".csv", csv)

                    MessageBox.Show("HighSchool " & cbData.Text & " Data successfully exported. The file has been successfully saved in the following location:" & Environment.NewLine & SavedPath, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    File.WriteAllText(folderPath & "HighSchool " & cbData.Text & "-" & DataFilter & ".csv", csv)

                    MessageBox.Show("HighSchool" & cbData.Text & " Data successfully exported. The file has been successfully saved in the following location:" & Environment.NewLine & SavedPath, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                UserActivity("Exported Highschool " & cbData.Text & " Data" & DataFilter & ".", "DATABASE TABLE EXPORT")

            End If

            ' Open the folder after export
            If Directory.Exists(folderPath) Then
                ' Try to find an existing Explorer window with this folder
                Dim hWnd As IntPtr = FindWindow("CabinetWClass", Nothing)

                If hWnd <> IntPtr.Zero Then
                    ' Bring the window to the foreground
                    ShowWindow(hWnd, SW_RESTORE) ' Restore if minimized
                    SetForegroundWindow(hWnd)
                Else
                    ' If not found, open a new Explorer window
                    Process.Start("explorer.exe", SavedPath)
                End If
            Else
                MessageBox.Show("The specified folder path does not exist.")
            End If
        End If
    End Sub

    Private Sub cbData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbData.SelectedIndexChanged
        If cbData.Text = "Student Downpayments" Or cbData.Text = "Student Payments" Then
            Me.Size = New Size(309, 333)
        Else
            Me.Size = New Size(309, 238)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class