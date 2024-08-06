Imports MySql.Data.MySqlClient

Public Class frmReqAck_Outbound
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        frmReqAckReceipt_Outbound.btnUpdate.Visible = False
        frmReqAckReceipt_Outbound.btnSave.Visible = True
        frmReqAckReceipt_Outbound.ShowDialog()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        AcknowledgementList2()
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged
        AcknowledgementList2()
    End Sub

    Private Sub dtTo_ValueChanged(sender As Object, e As EventArgs) Handles dtTo.ValueChanged
        AcknowledgementList2()
    End Sub

    Private Sub cbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilter.SelectedIndexChanged
        AcknowledgementList2()
    End Sub

    Private Sub dgAckList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgAckList.CellContentClick
        Dim colname As String = dgAckList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            If dgAckList.RowCount = 0 Then
            Else
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select * from tbl_documents_reference_out where ref_code = '" & dgAckList.CurrentRow.Cells(0).Value & "'", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then

                    frmReqAckReceipt_Outbound.dCode = dgAckList.CurrentRow.Cells(0).Value
                    frmReqAckReceipt_Outbound.txtSchool.Text = dgAckList.CurrentRow.Cells(2).Value
                    frmReqAckReceipt_Outbound.schoolid = dr.Item("ref_schoold_id").ToString
                    frmReqAckReceipt_Outbound.txtRemarks.Text = dr.Item("ref_remarks").ToString
                    dr.Close()
                    cn.Close()
                    cn.Open()
                    frmReqAckReceipt_Outbound.dg_doc_list.Rows.Clear()
                    Dim i As Integer
                    Dim sql As String
                    sql = "SELECT t1.`ref_student_id` as 'StudID', t3.`StudentFullName` as 'Student',t1.`ref_doc_id` as 'ID', t2.doc_code as 'Doc Code', t2.doc_description as 'Doc Desc', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', '',t1.`ref_released_date`) as 'Released', t1.`ref_release_type` as 'Mode', t1.ref_received_by as 'Received', t1.ref_contact as 'Contact' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id WHERE t1.ref_code = '" & dgAckList.CurrentRow.Cells(0).Value & "'"
                    cm = New MySqlCommand(sql, cn)
                    dr = cm.ExecuteReader
                    While dr.Read
                        i += 1
                        frmReqAckReceipt_Outbound.dg_doc_list.Rows.Add(dr.Item("StudID").ToString, dr.Item("Student").ToString, dr.Item("ID").ToString, dr.Item("Doc Code").ToString, dr.Item("Doc Desc").ToString, dr.Item("Status").ToString, dr.Item("Released").ToString, dr.Item("Mode").ToString, dr.Item("Received").ToString, dr.Item("Contact").ToString)
                    End While
                    dr.Close()
                    cn.Close()
                    frmReqAckReceipt_Outbound.btnUpdate.Visible = True
                    frmReqAckReceipt_Outbound.btnSave.Visible = False
                    frmReqAckReceipt_Outbound.ShowDialog()
                Else
                    dr.Close()
                    cn.Close()
                End If
            End If
        End If
    End Sub

End Class