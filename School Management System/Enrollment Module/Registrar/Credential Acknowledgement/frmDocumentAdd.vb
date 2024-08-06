Imports MySql.Data.MySqlClient

Public Class frmDocumentAdd
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmDocumentAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If IS_EMPTY(txtCode) = True Then Return
        If IS_EMPTY(txtDesc) = True Then Return
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT * FROM tbl_documents WHERE (doc_code = '" & txtCode.Text & "' or doc_description = '" & txtDesc.Text & "')", cn)
        Dim sdr As MySqlDataReader = cm.ExecuteReader()
        If (sdr.Read() = True) Then
            MsgBox("Document with code: '" & txtCode.Text & "' or description: '" & txtDesc.Text & "' already exists.", vbCritical)
            sdr.Dispose()
            cn.Close()
        Else
            sdr.Dispose()
            cn.Close()
            query("INSERT INTO tbl_documents (doc_code, doc_description, doc_type) values ('" & txtCode.Text & "', '" & txtDesc.Text & "', " & If(cbDocStat.Checked = True, 1, 0) & ")")
            MsgBox("Document successfully added. The list will be refreshed. Please reselect the documents.", vbCritical)
            frmReqAckReceipt_Inbound.docs_list()

            txtCode.Text = String.Empty
            txtDesc.Text = String.Empty
            Me.Close()
        End If
    End Sub
End Class