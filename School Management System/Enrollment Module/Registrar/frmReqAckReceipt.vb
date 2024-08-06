Imports MySql.Data.MySqlClient
Public Class frmReqAckReceipt

    Public ref_id As Integer
    Public doc_id As Integer
    Public doc_code As String
    Public doc_type As String
    Dim rcpt_code As String
    Dim datetime As String
    Dim studentID As String

    Sub docs_list()
        dg_doc_list.Columns.Clear()
        load_datagrid("select (doc_id) as 'ID', (doc_code) as 'Docu Code', (doc_description) as 'Docu Description' from tbl_documents order by doc_description", dg_doc_list)
        dg_doc_list.Columns(0).Visible = False
        dg_doc_list.Columns(1).ReadOnly = True
        dg_doc_list.Columns(2).ReadOnly = True

        Dim chkbox As New DataGridViewCheckBoxColumn()
        dg_doc_list.Columns.Add(chkbox)

        Dim num_pages As New DataGridViewTextBoxColumn()
        dg_doc_list.Columns.Add(num_pages)

        Dim num_attach As New DataGridViewTextBoxColumn()
        dg_doc_list.Columns.Add(num_attach)

        'Dim forwardedto As New DataGridViewTextBoxColumn()
        'dg_doc_list.Columns.Add(forwardedto)

        dg_doc_list.Columns(3).HeaderCell.Value = "Select"
        dg_doc_list.Columns(4).HeaderCell.Value = "No. Of Pages"
        dg_doc_list.Columns(5).HeaderCell.Value = "No. Of Attachments"

        dg_doc_list.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        'dg_doc_list.Columns(6).HeaderCell.Value = "Forwarded To"
    End Sub

    Private Sub frmReqAckReceipt_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        docs_list()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click, Label6.Click, Label4.Click, Label3.Click, Label9.Click, Label8.Click, Label15.Click, Label14.Click, Label12.Click, Label10.Click

    End Sub

    Private Sub dg_doc_list_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_doc_list.CellContentClick
        If dg_doc_list.CurrentRow.Cells(3).Value = True Then
            dg_doc_list.CurrentRow.Cells(3).Value = False
        Else
            dg_doc_list.CurrentRow.Cells(3).Value = True
        End If
    End Sub

    Private Sub btnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        doc_code = String.Empty
        doc_id = 0
        doc_type = String.Empty
        txtNoAttach.Text = String.Empty
        'txtbox_date_time.Text = ""
        txtRefNo.Text = String.Empty
        txtDocTitle.Text = String.Empty
        txtForwardedTo.Text = String.Empty
        txtNoOfPages.Text = String.Empty
        txtRemarks.Text = String.Empty
        txtSender.Text = String.Empty
        txtStudent.Text = String.Empty
        docs_list()
    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If Char.IsDigit(CChar(CStr(e.KeyChar))) = False Then e.Handled = True
    End Sub

    Private Sub TextBox1_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If Char.IsDigit(CChar(CStr(e.KeyChar))) = False Then e.Handled = False
    End Sub

    Private Sub dg_doc_list_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dg_doc_list.EditingControlShowing
        If dg_doc_list.CurrentCell.ColumnIndex = 4 Then
            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        ElseIf dg_doc_list.CurrentCell.ColumnIndex = 5 Then
            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        Else
            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox1_keyPress
        End If
    End Sub

    Private Sub txtNoAttach_TextChanged(sender As Object, e As EventArgs) Handles txtNoAttach.TextChanged

    End Sub

    Private Sub txtNoAttach_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoAttach.KeyPress, txtNoOfPages.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") Then
            'cancel keys
            e.Handled = True
        End If
    End Sub

    Private Sub txtDocTitle_TextChanged(sender As Object, e As EventArgs) Handles txtDocTitle.TextChanged

    End Sub

    Private Sub txtDocTitle_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDocTitle.KeyPress, txtRefNo.KeyPress
        e.Handled = True
    End Sub

    Private Sub cbCompleted_CheckedChanged(sender As Object, e As EventArgs) Handles cbCompleted.CheckedChanged
        If cbCompleted.Checked = True Then
            txtRemarks.Text = "COMPLETED"
        Else
            txtRemarks.Text = String.Empty
        End If
    End Sub

    Private Sub cbRegistrar_CheckedChanged(sender As Object, e As EventArgs) Handles cbRegistrar.CheckedChanged
        If cbRegistrar.Checked = True Then
            txtForwardedTo.Text = "OFFICE OF THE COLLEGE REGISTRAR"
        Else
            txtForwardedTo.Text = String.Empty
        End If
    End Sub


    Private Sub AutoReferenceNumber()
        Dim s As String = DateToday
        Dim t As String = s.Substring(s.Length - 2, 1)
        Dim v As String = s.Substring(s.Length - 3, 1)
        Dim u As String = s.Substring(s.Length - 4, 1)
        Dim yearid As String
        yearid = u & v & t & s.Substring(s.Length - 1, 1)
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT ref_no FROM tbl_documents_reference WHERE ref_doc_id = " & doc_id & " and ref_no like '" & doc_code & "" & yearid & "%'", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(ref_no) as DocReference from tbl_documents_reference WHERE ref_doc_id = " & doc_id & "", cn)
            Dim code_last_no As String = cm.ExecuteScalar
            dr.Close()
            cn.Close()
            ds = New DataSet
            Dim yr As String = yearid
            Dim str As String = code_last_no
            str = str.Remove(0, doc_code.Length + yearid.Length)
            Dim n As String
            Dim m As Long
            Dim r As Long
            n = yr + str
            m = n
            r = m + 1
            txtRefNo.Text = doc_code & r
        Else
            txtRefNo.Text = doc_code + yearid + "00001"
        End If
        cn.Close()
    End Sub

    Private Sub AutoReceiptNumber()
        Dim s As String = DateToday
        Dim t As String = s.Substring(s.Length - 2, 1)
        Dim v As String = s.Substring(s.Length - 3, 1)
        Dim u As String = s.Substring(s.Length - 4, 1)
        Dim yearid As String
        yearid = u & v & t & s.Substring(s.Length - 1, 1)
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT rcpt_code FROM tbl_documents_reference WHERE rcpt_code like 'DOCS" & yearid & "%'", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(rcpt_code) as DocsReceipt from tbl_documents_reference", cn)
            Dim code_last_no As String = cm.ExecuteScalar
            dr.Close()
            cn.Close()
            ds = New DataSet
            Dim yr As String = yearid
            Dim str As String = code_last_no
            str = str.Remove(0, doc_code.Length + yearid.Length)
            Dim n As String
            Dim m As Long
            Dim r As Long
            n = yr + str
            m = n
            r = m + 1
            rcpt_code = "DOCS" & r
        Else
            rcpt_code = "DOCS" + yearid + "00001"
        End If
        cn.Close()
    End Sub

    Private Sub btnRefReload_Click(sender As Object, e As EventArgs) Handles btnRefReload.Click
        AutoReferenceNumber()
    End Sub

    Private Sub btnDTReload_Click(sender As Object, e As EventArgs) Handles btnDTReload.Click
        datetime = Format(Convert.ToDateTime(dtDate.Text), "MMMM d, yyyy").ToUpper & " " & dtTime.Text
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        doc_code = String.Empty
        doc_id = 0
        doc_type = String.Empty
        txtNoAttach.Text = String.Empty
        'txtbox_date_time.Text = ""
        txtRefNo.Text = String.Empty
        txtDocTitle.Text = String.Empty
        txtForwardedTo.Text = String.Empty
        txtNoOfPages.Text = String.Empty
        txtRemarks.Text = String.Empty
        txtSender.Text = String.Empty
        txtStudent.Text = String.Empty
        docs_list()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtSender.Text = "" Then
            MsgBox("Please input a sender.", vbCritical)
        Else
            Try
                AutoReceiptNumber()
                For Each row As DataGridViewRow In dg_doc_list.Rows
                    'Dim isSelected As Boolean = Convert.ToBoolean()
                    If row.Cells(3).Value = True Then
                        If row.Cells(4).Value = "" Then
                            MsgBox("Insert document number of pages! or input 0.")
                        ElseIf row.Cells(5).Value = "" Then
                            MsgBox("Insert document number of attachments! or input 0.")
                            Return
                        Else
                            AutoReferenceNumber()
                            datetime = Format(Convert.ToDateTime(dtDate.Text), "MMMM d, yyyy").ToUpper & " " & dtTime.Text
                            Dim iDate As String = DateToday
                            Dim oDate As DateTime = Convert.ToDateTime(iDate)
                            cn.Close()
                            cn.Open()
                            cm = New MySqlCommand("INSERT INTO tbl_documents_reference (rcpt_code,ref_doc_id,ref_no,ref_student_id,ref_remarks,sender,pages,attachments,received_by,received_datetime,forwarded_to,received_date,received_time)VALUES('" & rcpt_code & "', " & row.Cells(0).Value & ",'" & txtRefNo.Text & "','" & studentID & "','" & txtRemarks.Text & "','" & txtSender.Text & "'," & row.Cells(4).Value & "," & row.Cells(5).Value & ",'" & str_userid & "','" & Format(Convert.ToDateTime(dtDate.Text), "MMMM d, yyyy").ToUpper & " " & dtTime.Text & "','" & txtForwardedTo.Text & "',CURDATE(),'" & dtTime.Text & "')", cn)
                            cm.ExecuteNonQuery()
                            cn.Close()
                        End If
                    End If
                Next
                MessageBox.Show("Successfully recorded.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Catch ex As Exception
                MessageBox.Show("Failed to record." & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If txtNoAttach.Text = "" Or txtRefNo.Text = "" Or txtDocTitle.Text = "" Or txtForwardedTo.Text = "" Or txtNoOfPages.Text = "" Or txtSender.Text = "" Then
            MsgBox("Empty Fields Found!")
        Else
            Try
                Dim iDate As String = DateToday
                Dim oDate As DateTime = Convert.ToDateTime(iDate)
                cn.Open()
                cm = New MySqlCommand("UPDATE `tbl_documents_reference` SET `ref_doc_id`=" & doc_id & ", `ref_student_id`='" & studentID & "', `ref_remarks`='" & txtRemarks.Text & "', `sender`='" & txtSender.Text & "', `pages`=" & txtNoOfPages.Text & ", `attachments`=" & txtNoAttach.Text & ", `forwarded_to`='" & txtForwardedTo.Text & "', `received_datetime`='" & Format(Convert.ToDateTime(dtDate.Text), "MMMM d, yyyy").ToUpper & " " & dtTime.Text & "', received_date = '" & dtDate.Text & "', received_time = '" & dtTime.Value & "' WHERE `ref_id` = " & ref_id & "", cn)
                cm.ExecuteNonQuery()
                cn.Close()
                MessageBox.Show("Successfully updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Catch ex As Exception
                MessageBox.Show("Failed to update record.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        SearchPanel.Visible = True
        dgStudentList.BringToFront()
        Panel3.Visible = False
        frmTitle.Text = "Search Student"
        AckStudentList()
    End Sub

    Public Sub AckStudentList()
        dgStudentList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, (course_name) as 'Course Desc', (course_sector) as 'Course Sector' from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (s_ln like '%" & txtSearch.Text & "%' or s_fn like '%" & txtSearch.Text & "%' or s_mn like '%" & txtSearch.Text & "%' or s_id_no like '%" & txtSearch.Text & "%' or course_code like '%" & txtSearch.Text & "%' or s_yr_lvl like '%" & txtSearch.Text & "%') order by s_id_no asc limit 250"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("Course Desc").ToString, dr.Item("Course Sector").ToString)
        End While
        dr.Close()
        cn.Close()

        If frmMain.systemModule.Text = "College Module" Then
            dgStudentList.Columns(8).HeaderText = "Course"
        Else
            dgStudentList.Columns(8).HeaderText = "Strand/Grade"
        End If
    End Sub

    Public Sub AckDocumentList()
        dgDocuList.Rows.Clear()
        Dim sql As String
        sql = "select (doc_id) as 'ID', (doc_code) as 'Docu Code', (doc_description) as 'Docu Description', doc_type from tbl_documents where (doc_code like '%" & txtSearch.Text & "%' or doc_description like '%" & txtSearch.Text & "%') order by doc_description"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgDocuList.Rows.Add(dr.Item("ID").ToString, dr.Item("Docu Code").ToString, dr.Item("Docu Description").ToString, dr.Item("doc_type").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        AckStudentList()
    End Sub

    Private Sub btnSearchDoc_Click(sender As Object, e As EventArgs) Handles btnSearchDoc.Click
        SearchPanel.Visible = True
        dgDocuList.BringToFront()
        Panel3.Visible = False
        frmTitle.Text = "Search Document"
        AckDocumentList()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Select Case frmTitle.Text
            Case "Search Student"
                studentID = dgStudentList.CurrentRow.Cells(1).Value
                txtStudent.Text = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
            Case "Search Document"
                doc_id = dgDocuList.CurrentRow.Cells(0).Value
                doc_code = dgDocuList.CurrentRow.Cells(1).Value
                txtDocTitle.Text = dgDocuList.CurrentRow.Cells(2).Value
                doc_type = dgDocuList.CurrentRow.Cells(3).Value
                AutoReferenceNumber()
        End Select
        SearchPanel.Visible = False
        Panel3.Visible = True
        dgStudentList.Rows.Clear()
        dgDocuList.Rows.Clear()
        frmTitle.Text = "Search"
        txtSearch.Text = String.Empty
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            SearchPanel.Visible = False
            Panel3.Visible = True
            dgStudentList.Rows.Clear()
            dgDocuList.Rows.Clear()
            frmTitle.Text = "Search"
            txtSearch.Text = String.Empty
        Else
            Me.Dispose()
        End If
    End Sub
End Class