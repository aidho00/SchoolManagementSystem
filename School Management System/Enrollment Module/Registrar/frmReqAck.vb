
Public Class frmReqAck

    Private Sub btnPrint_MouseDown(sender As Object, e As MouseEventArgs) Handles btnPrint.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim position As Point = Control.MousePosition
            printMenu.Show(position)
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        AcknowledgementList()
    End Sub

    Private Sub frmReqAck_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        frmReqAckReceipt.ShowDialog()
    End Sub

    Private Sub btnIndividual_Click(sender As Object, e As EventArgs) Handles btnIndividual.Click
        Date_Today()

        Try

            load_datagrid("SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),''), t1.`received_datetime`, t1.forwarded_to FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where t1.ref_id = " & dgAckList.CurrentRow.Cells(0).Value & "", dg_report)

            frmReportViewer.Show()
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New Cred_Ack_Receipt
            rptdoc.SetParameterValue("sender_company", dg_report.CurrentRow.Cells(8).Value)
            rptdoc.SetParameterValue("document_title", dg_report.CurrentRow.Cells(3).Value)
            rptdoc.SetParameterValue("number_pages", dg_report.CurrentRow.Cells(9).Value)
            rptdoc.SetParameterValue("number_attachments", dg_report.CurrentRow.Cells(10).Value)
            rptdoc.SetParameterValue("document_reference_no", dg_report.CurrentRow.Cells(4).Value)
            rptdoc.SetParameterValue("date_time_received", dg_report.CurrentRow.Cells(12).Value)
            rptdoc.SetParameterValue("forwarded_to", dg_report.CurrentRow.Cells(13).Value)
            rptdoc.SetParameterValue("registrar", dg_report.CurrentRow.Cells(11).Value)
            rptdoc.SetParameterValue("date_time", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy").ToUpper & " " & sysTime)
            rptdoc.SetParameterValue("remarks", dg_report.CurrentRow.Cells(7).Value)
            frmReportViewer.ReportViewer.ReportSource = rptdoc
            'frm_registrar_request.Enabled = True
            'Me.Close()
        Catch ex As Exception
            MessageBox.Show("Re-select record.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnBatch_Click(sender As Object, e As EventArgs) Handles btnBatch.Click
        Date_Today()

        Try

            load_datagrid("SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),''), t1.`received_datetime`, t1.forwarded_to FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where t1.rcpt_code = '" & dgAckList.CurrentRow.Cells(15).Value & "'", dg_report)

            Dim dt2 As New DataTable
            With dt2
                .Columns.Add("cb_code")
                .Columns.Add("subject_code")
                .Columns.Add("subject_description")
                .Columns.Add("ds_code")
            End With

            For Each dr2 As DataGridViewRow In dg_report.Rows
                dt2.Rows.Add(dr2.Cells(3).Value, dr2.Cells(9).Value, dr2.Cells(10).Value, dr2.Cells(4).Value)
            Next

            Dim dt3 As New DataTable
            With dt3
                .Columns.Add("cb_code")
                .Columns.Add("subject_code")
                .Columns.Add("subject_description")
                .Columns.Add("ds_code")
            End With

            For Each dr3 As DataGridViewRow In dg_report.Rows
                dt3.Rows.Add(dr3.Cells(3).Value, dr3.Cells(9).Value, dr3.Cells(10).Value, dr3.Cells(4).Value)
            Next

            frmReportViewer.Show()
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New Cred_Ack_Receipt2
            rptdoc.Subreports(0).SetDataSource(dt2)
            rptdoc.Subreports(1).SetDataSource(dt3)
            rptdoc.SetParameterValue("sender_company", dg_report.CurrentRow.Cells(8).Value)
            'rptdoc.SetParameterValue("document_title", dg_report.CurrentRow.Cells(3).Value)
            'rptdoc.SetParameterValue("number_pages", dg_report.CurrentRow.Cells(9).Value)
            'rptdoc.SetParameterValue("number_attachments", dg_report.CurrentRow.Cells(10).Value)
            'rptdoc.SetParameterValue("document_reference_no", dg_report.CurrentRow.Cells(4).Value)
            rptdoc.SetParameterValue("date_time_received", dg_report.CurrentRow.Cells(12).Value)
            rptdoc.SetParameterValue("forwarded_to", dg_report.CurrentRow.Cells(13).Value)
            rptdoc.SetParameterValue("registrar", dg_report.CurrentRow.Cells(11).Value)
            rptdoc.SetParameterValue("date_time", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy").ToUpper & " " & sysTime)
            rptdoc.SetParameterValue("remarks", dg_report.CurrentRow.Cells(7).Value)
            frmReportViewer.ReportViewer.ReportSource = rptdoc
            'frm_registrar_request.Enabled = True
            'Me.Close()
        Catch ex As Exception
            MessageBox.Show("Re-select record.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        frmReportViewer.Show()
        Dim dt As New DataTable
        With dt
            .Columns.Add("subject_code")
            .Columns.Add("subject_description")
            .Columns.Add("ds_code")
            .Columns.Add("room_code")
        End With
        For Each dr As DataGridViewRow In dgAckList.Rows
            dt.Rows.Add(dr.Cells(4).Value, dr.Cells(3).Value, dr.Cells(5).Value & " - " & dr.Cells(6).Value, dr.Cells(7).Value)
        Next
        Dim iDate As String = DateToday
        Dim oDate As DateTime = Convert.ToDateTime(iDate)
        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New Ack_Report
        rptdoc.SetDataSource(dt)
        'rptdoc.SetParameterValue("studentname", cmb_student.Text)
        frmReportViewer.ReportViewer.ReportSource = rptdoc

    End Sub

    Private Sub dgAckList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgAckList.CellContentClick
        Dim colname As String = dgAckList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then

        Else

        End If
    End Sub
End Class