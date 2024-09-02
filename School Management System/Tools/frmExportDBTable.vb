Imports MySql.Data.MySqlClient
Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Style

Public Class frmExportDBTable
    Private Sub frmExportDBTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
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
        If frmMain.systemModule.Text = "College Module" Then
            folderPath = "C:\Exported Database Table\" & cbAcademicYear.Text & "\"
        Else
            folderPath = "C:\Exported Database Table\" & cbAcademicYear.Text & "\"
        End If

        Dim d1 As String = dtFrom.Text
        Dim dttfrom As String = d1.Replace("/", "-")
        Dim d2 As String = dtTo.Text
        Dim dttto As String = d2.Replace("/", "-")

        If frmMain.systemModule.Text = "College Module" Then
            If Not IO.Directory.Exists(folderPath) Then
                IO.Directory.CreateDirectory(folderPath)
                File.WriteAllText(folderPath & "College " & cbData.Text & " -" & dttfrom & "-" & dttto & ".csv", csv)

                MessageBox.Show("College " & cbData.Text & " successfully exported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                proc = Process.Start(folderPath & "College " & cbData.Text & " -" & dttfrom & "-" & dttto & ".csv", "")
            Else
                File.WriteAllText(folderPath & "College " & cbData.Text & " -" & dttfrom & "-" & dttto & ".csv", csv)

                MessageBox.Show("College " & cbData.Text & " Data successfully exported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                proc = Process.Start(folderPath & "" & cbData.Text & " -" & dttfrom & "-" & dttto & ".csv", "")
            End If
        Else
            If Not IO.Directory.Exists(folderPath) Then
                IO.Directory.CreateDirectory(folderPath)
                File.WriteAllText(folderPath & "HighSchool " & cbData.Text & " -" & dttfrom & "-" & dttto & ".csv", csv)

                MessageBox.Show("HighSchool " & cbData.Text & " successfully exported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                proc = Process.Start(folderPath & "HighSchool " & cbData.Text & " -" & dttfrom & "-" & dttto & ".csv", "")
            Else
                File.WriteAllText(folderPath & "HighSchool " & cbData.Text & "  -" & dttfrom & "-" & dttto & ".csv", csv)

                MessageBox.Show("HighSchool" & cbData.Text & " successfully exported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                proc = Process.Start(folderPath & "HighSchool " & cbData.Text & "  -" & dttfrom & "-" & dttto & ".csv", "")
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
End Class