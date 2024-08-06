Imports CrystalDecisions.CrystalReports.Engine

Public Class frmReportViewer
    Public Shared StudentID As String = ""
    Public Shared recordStatus As String = ""

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

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles systemSign.MouseMove, Panel1.MouseMove   ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles systemSign.MouseUp, Panel1.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

#End Region


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmReportViewer_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If ReportViewer.ReportSource Is Nothing Then
                MsgBox("No report is loaded in the Report Viewer.", vbCritical)
                Return
            End If
            ReportViewer.PrintReport()
        Catch ex As Exception
            ' Handle any exceptions
            MsgBox("Error printing report: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnPrintSettings_Click(sender As Object, e As EventArgs) Handles btnPrintSettings.Click
        Try
            ' Check if the CrystalReportViewer has a ReportSource
            If ReportViewer.ReportSource Is Nothing Then
                MsgBox("No report is loaded in the Report Viewer.", vbCritical)
                Return
            End If
            ' Create a PrintDocument
            Dim printDoc As New Printing.PrintDocument()
            ' Set the ReportDocument as the PrintDocument's Document
            Dim reportDocument As ReportDocument = TryCast(ReportViewer.ReportSource, ReportDocument)
            printDoc.DocumentName = reportDocument.Database.Tables(0).Name
            ' Create a PrintDialog
            Dim printDialog As New PrintDialog()
            printDialog.Document = printDoc
            ' Show the printer setup dialog
            If printDialog.ShowDialog() = DialogResult.OK Then
                ' User clicked OK in the printer setup dialog
                ' Set the printer settings
                reportDocument.PrintOptions.PrinterName = printDoc.PrinterSettings.PrinterName
                ' Print the report
                reportDocument.PrintToPrinter(printDoc.PrinterSettings, printDoc.PrinterSettings.DefaultPageSettings, False)
            Else
                ' User clicked Cancel in the printer setup dialog
                MsgBox("Printing cancelled.", vbInformation)
            End If
        Catch ex As Exception
            ' Handle any exceptions
            MsgBox("Error printing report: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub cbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAcademicYear.SelectedIndexChanged
        If frmTitle.Text = "Pre-Enrollment Form" Then
            Try
                Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                rptdoc = New Enrollment_Student_FORM
                rptdoc.SetParameterValue("studentname", frmStudentInfo.txtFname.Text & " " & frmStudentInfo.txtMname.Text & " " & frmStudentInfo.txtLname.Text)
                rptdoc.SetParameterValue("studentcourse", frmStudentInfo.txtCourse.Text)
                rptdoc.SetParameterValue("schoolyear", cbAcademicYear.Text)
                rptdoc.SetParameterValue("studentyearlevel", frmStudentInfo.cbYearLevel.Text)
                rptdoc.SetParameterValue("studentidnumber", frmStudentInfo.OldStudentID)
                If recordStatus = "ADD" Then
                    Dim firstChar = frmStudentInfo.txtFname.Text.Substring(0, 2)
                    Dim lastname = frmStudentInfo.txtLname.Text.Replace(" ", "")
                    rptdoc.SetParameterValue("gsuite", firstChar.ToString.ToLower & "." & lastname.ToString.ToLower & "@cfci.ph.education | Very_secret123")
                Else
                    rptdoc.SetParameterValue("gsuite", "")
                End If
                ReportViewer.ReportSource = rptdoc
            Catch ex As Exception

            End Try
        Else

        End If
    End Sub

    Private Sub frmReportViewer_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If frmTitle.Text = "Pre-Enrollment Form" Then
            frmStudentInfo.Close()
        End If
    End Sub

    Private Sub frmTitle_TextChanged(sender As Object, e As EventArgs) Handles frmTitle.TextChanged
        If frmTitle.Text = "Pre-Enrollment Form" Then
            AcadPanel.Visible = True
        Else
            AcadPanel.Visible = False
        End If
    End Sub
End Class