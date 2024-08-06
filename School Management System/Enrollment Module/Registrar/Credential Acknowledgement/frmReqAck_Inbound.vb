
Public Class frmReqAck_Inbound
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
        frmReqAckReceipt_Inbound.PanelDocuDetails.Visible = False
        frmReqAckReceipt_Inbound.Size = New Size(993, 619)
        frmReqAckReceipt_Inbound.btnSave.Visible = True
        frmReqAckReceipt_Inbound.btnUpdate.Visible = False
        frmReqAckReceipt_Inbound.ShowDialog()
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
            load_datagrid("SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),''), t1.`received_date`, t1.forwarded_to, t1.`received_time` FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where t1.ref_id = " & dgAckList.CurrentRow.Cells(0).Value & "", dg_report)
            frmReqAckReceipt_Inbound.btnSave.Visible = False
            frmReqAckReceipt_Inbound.btnUpdate.Visible = True
            frmReqAckReceipt_Inbound.txtSender.Text = dg_report.CurrentRow.Cells(8).Value
            frmReqAckReceipt_Inbound.doc_id = dg_report.CurrentRow.Cells(1).Value
            Try
                load_data("SELECT doc_code, doc_description, doc_type from tbl_documents where doc_id = " & frmReqAckReceipt_Inbound.doc_id & "", "tbl_documents")
                frmReqAckReceipt_Inbound.doc_code = ds.Tables("tbl_documents").Rows(0)(0).ToString
                frmReqAckReceipt_Inbound.txtDocTitle.Text = ds.Tables("tbl_documents").Rows(0)(1).ToString
                frmReqAckReceipt_Inbound.doc_type = ds.Tables("tbl_documents").Rows(0)(2).ToString
                ds = New DataSet
            Catch ex As Exception
            End Try
            frmReqAckReceipt_Inbound.txtNoOfPages.Text = dg_report.CurrentRow.Cells(9).Value
            frmReqAckReceipt_Inbound.txtNoAttach.Text = dg_report.CurrentRow.Cells(10).Value
            frmReqAckReceipt_Inbound.txtRefNo.Text = dg_report.CurrentRow.Cells(4).Value
            If dg_report.CurrentRow.Cells(12).Value Is DBNull.Value Then
            Else
                frmReqAckReceipt_Inbound.dtDate.Text = dg_report.CurrentRow.Cells(12).Value
            End If
            frmReqAckReceipt_Inbound.txtForwardedTo.Text = dg_report.CurrentRow.Cells(13).Value
            frmReqAckReceipt_Inbound.StudentID = dg_report.CurrentRow.Cells(5).Value
            frmReqAckReceipt_Inbound.txtRemarks.Text = dg_report.CurrentRow.Cells(7).Value
            If dg_report.CurrentRow.Cells(14).Value Is DBNull.Value Then
            Else
                frmReqAckReceipt_Inbound.dtTime.Text = dg_report.CurrentRow.Cells(14).Value
            End If
            Try
                load_data("SELECT StudentFullName from students where StudentID = '" & frmReqAckReceipt_Inbound.StudentID & "'", "students")
                frmReqAckReceipt_Inbound.txtStudent.Text = ds.Tables("students").Rows(0)(0).ToString
                ds = New DataSet
            Catch ex As Exception
            End Try
            frmReqAckReceipt_Inbound.PanelDocuDetails.Visible = True
            frmReqAckReceipt_Inbound.Size = New Size(993, 334)
            frmReqAckReceipt_Inbound.ShowDialog()
        Else

        End If
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged
        AcknowledgementList()
    End Sub

    Private Sub dtTo_ValueChanged(sender As Object, e As EventArgs) Handles dtTo.ValueChanged
        AcknowledgementList()
    End Sub

    Private Sub cbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilter.SelectedIndexChanged
        AcknowledgementList()
    End Sub
End Class