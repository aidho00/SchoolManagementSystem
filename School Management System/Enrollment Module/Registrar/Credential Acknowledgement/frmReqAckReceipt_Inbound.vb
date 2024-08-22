Imports MySql.Data.MySqlClient
Public Class frmReqAckReceipt_Inbound

    Public ref_id As Integer
    Public doc_id As Integer
    Public doc_code As String
    Public doc_type As String
    Dim rcpt_code As String
    Dim datetime As String

    Public Shared StudentID As String = ""
    Public Shared StudentName As String = ""
    Public Shared CourseID As Integer = 0
    Public Shared Course As String = ""
    Public Shared YearLevel As String = ""
    Public Shared Gender As String = ""

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

    Sub docs_list()
        'dg_doc_list.Columns.Clear()
        'load_datagrid("select (doc_id) as 'ID', (doc_code) as 'Docu Code', (doc_description) as 'Docu Description' from tbl_documents order by doc_description", dg_doc_list)
        'dg_doc_list.Columns(0).Visible = False
        'dg_doc_list.Columns(1).ReadOnly = True
        'dg_doc_list.Columns(2).ReadOnly = True

        'Dim chkbox As New DataGridViewCheckBoxColumn()
        'dg_doc_list.Columns.Add(chkbox)

        'Dim num_pages As New DataGridViewTextBoxColumn()
        'dg_doc_list.Columns.Add(num_pages)

        'Dim num_attach As New DataGridViewTextBoxColumn()
        'dg_doc_list.Columns.Add(num_attach)

        ''Dim forwardedto As New DataGridViewTextBoxColumn()
        ''dg_doc_list.Columns.Add(forwardedto)

        'dg_doc_list.Columns(3).HeaderCell.Value = "Select"
        'dg_doc_list.Columns(4).HeaderCell.Value = "No. Of Pages"
        'dg_doc_list.Columns(5).HeaderCell.Value = "No. Of Attachments"

        'dg_doc_list.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        ''dg_doc_list.Columns(6).HeaderCell.Value = "Forwarded To"
    End Sub

    Private Sub frmReqAckReceipt_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        docs_list()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click, Label4.Click, Label3.Click, Label9.Click, Label8.Click, Label15.Click, Label14.Click, Label12.Click, Label10.Click, Label5.Click

    End Sub

    Private Sub dg_doc_list_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_doc_list.CellContentClick
        Dim colname As String = dg_doc_list.Columns(e.ColumnIndex).Name
        If colname = "colRemove" Then
            Try
                dg_doc_list.Rows.RemoveAt(dg_doc_list.CurrentRow.Index)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub btnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        clear()
    End Sub

    Sub clear()
        ref_id = ""
        rcpt_code = ""

        StudentID = ""
        StudentName = ""
        CourseID = 0
        Course = ""
        YearLevel = ""
        Gender = ""

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

        dg_doc_list.Rows.Clear()
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

    Private Sub AutoReferenceNumber()
        Dim yearid As String
        yearid = YearToday
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
        Dim yearid As String
        yearid = YearToday
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
            str = str.Remove(0, 8)
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


    Private Function NoneOfTheRowsAreChecked() As Boolean
        Dim checkboxColumnIndex As Integer = 3
        For Each row As DataGridViewRow In dg_doc_list.Rows
            If Convert.ToBoolean(row.Cells(checkboxColumnIndex).Value) Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(txtNoAttach) = True Then Return
        If IS_EMPTY(txtRefNo) = True Then Return
        If IS_EMPTY(txtDocTitle) = True Then Return
        If IS_EMPTY(txtForwardedTo) = True Then Return
        If IS_EMPTY(txtNoOfPages) = True Then Return
        If IS_EMPTY(txtSender) = True Then Return
        If MsgBox("Are you sure you want to save this record?.", vbYesNo + vbQuestion) = vbYes Then
            Try
                If NoneOfTheRowsAreChecked() Then
                    MsgBox("None of the documents are checked.", vbCritical)
                Else
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
                                cm = New MySqlCommand("INSERT INTO tbl_documents_reference (rcpt_code,ref_doc_id,ref_no,ref_student_id,ref_remarks,sender,pages,attachments,received_by,received_datetime,forwarded_to,received_date,received_time)VALUES('" & rcpt_code & "', " & row.Cells(0).Value & ",'" & txtRefNo.Text & "','" & StudentID & "','" & txtRemarks.Text & "','" & txtSender.Text & "'," & row.Cells(4).Value & "," & row.Cells(5).Value & ",'" & str_userid & "','" & Format(Convert.ToDateTime(dtDate.Text), "MMMM d, yyyy").ToUpper & " " & dtTime.Text & "','" & txtForwardedTo.Text & "',CURDATE(),'" & dtTime.Text & "')", cn)
                                cm.ExecuteNonQuery()
                                cn.Close()
                            End If
                        End If
                    Next
                    MessageBox.Show("Successfully recorded.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    clear()
                End If
            Catch ex As Exception
                MessageBox.Show("Failed to record." & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If IS_EMPTY(txtNoAttach) = True Then Return
        If IS_EMPTY(txtRefNo) = True Then Return
        If IS_EMPTY(txtDocTitle) = True Then Return
        If IS_EMPTY(txtForwardedTo) = True Then Return
        If IS_EMPTY(txtNoOfPages) = True Then Return
        If IS_EMPTY(txtSender) = True Then Return
        If MsgBox("Are you sure you want to update this record?.", vbYesNo + vbQuestion) = vbYes Then
            Try
                Dim iDate As String = DateToday
                Dim oDate As DateTime = Convert.ToDateTime(iDate)
                cn.Open()
                cm = New MySqlCommand("UPDATE `tbl_documents_reference` SET `ref_doc_id`=" & doc_id & ", `ref_student_id`='" & StudentID & "', `ref_remarks`='" & txtRemarks.Text & "', `sender`='" & txtSender.Text & "', `pages`=" & txtNoOfPages.Text & ", `attachments`=" & txtNoAttach.Text & ", `forwarded_to`='" & txtForwardedTo.Text & "', `received_datetime`='" & Format(Convert.ToDateTime(dtDate.Text), "MMMM d, yyyy").ToUpper & " " & dtTime.Text & "', received_date = '" & dtDate.Text & "', received_time = '" & dtTime.Value & "' WHERE `ref_id` = " & ref_id & "", cn)
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
        Try

            dgStudentList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, course_name, s_course_status from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (tbl_student.s_ln like '" & txtSearch.Text & "%' or tbl_student.s_fn like '" & txtSearch.Text & "%' or tbl_student.s_mn like '" & txtSearch.Text & "%' or tbl_student.s_id_no like '" & txtSearch.Text & "%' or tbl_student.s_yr_lvl like '" & txtSearch.Text & "%') order by s_id_no asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("course_name").ToString, dr.Item("s_course_status").ToString)
            End While
            dr.Close()
            cn.Close()

            If frmMain.systemModule.Text = "College Module" Then
                dgStudentList.Columns(8).HeaderText = "Course"
            Else
                dgStudentList.Columns(8).HeaderText = "Strand/Grade"
            End If

            dgPanelPadding(dgStudentList, dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgStudentList.Rows.Clear()

        End Try
    End Sub

    Public Sub AckDocumentList()
        Try

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
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgDocuList.Rows.Clear()

        End Try
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
                StudentName = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
                studentID = dgStudentList.CurrentRow.Cells(1).Value
                CourseID = dgStudentList.CurrentRow.Cells(9).Value
                YearLevel = dgStudentList.CurrentRow.Cells(7).Value
                Gender = dgStudentList.CurrentRow.Cells(6).Value
                txtStudent.Text = StudentName
            Case "Search Document"
                If btnUpdate.Visible = True Then
                    doc_id = dgDocuList.CurrentRow.Cells(0).Value
                    doc_code = dgDocuList.CurrentRow.Cells(1).Value
                    txtDocTitle.Text = dgDocuList.CurrentRow.Cells(2).Value
                    doc_type = dgDocuList.CurrentRow.Cells(3).Value
                    AutoReferenceNumber()
                Else
                    dg_doc_list.Rows.Add(dgDocuList.CurrentRow.Cells(0).Value, dgDocuList.CurrentRow.Cells(1).Value, dgDocuList.CurrentRow.Cells(2).Value, dgDocuList.CurrentRow.Cells(3).Value)
                End If
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
            Me.Close()
        End If
    End Sub

    Private Sub dgDocuList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgDocuList.CellContentClick

    End Sub

    Private Sub btnAddDoc_Click(sender As Object, e As EventArgs)
        frmDocumentAdd.ShowDialog()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        SearchPanel.Visible = True
        dgDocuList.BringToFront()
        Panel3.Visible = False
        frmTitle.Text = "Search Document"
        AckDocumentList()
    End Sub
End Class