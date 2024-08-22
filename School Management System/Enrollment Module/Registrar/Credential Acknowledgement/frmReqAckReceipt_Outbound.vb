Imports MySql.Data.MySqlClient

Public Class frmReqAckReceipt_Outbound
    Public Shared schoolid As Integer = 0
    Public Shared schoolname As String = ""
    Public Shared schooladdress As String = ""

    Public Shared StudentID As String = ""
    Public Shared StudentName As String = ""
    Public Shared CourseID As Integer = 0
    Public Shared Course As String = ""
    Public Shared YearLevel As String = ""
    Public Shared Gender As String = ""

    Public Shared dCode As String = ""


    Dim doc_id As Integer = 0
    Dim doc_code As String = ""
    Dim doc_desc As String = ""

    Private dateTimePicker As New DateTimePicker()

    Private Sub frmReqAckReceipt_Outbound_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'docs_list()
    End Sub

    Private Sub txtStudent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStudent.KeyPress, txtSchool.KeyPress, txtDocDesc.KeyPress
        e.Handled = True
    End Sub

    Private Sub AutoCode()
        Dim yearid As String
        yearid = YearToday
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT ref_code FROM tbl_documents_reference_out WHERE ref_code like 'AOUT" & yearid & "%'", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(ref_code) as code from tbl_documents_reference_out", cn)
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
            dCode = "AOUT" & r
        Else
            dCode = "AOUT" + yearid + "00001"
        End If
        cn.Close()
    End Sub

    Sub docs_list()
        Try


            dg_doc_list.Rows.Clear()
            Dim sql As String
            sql = "select (doc_id) as 'ID', (doc_code) as 'Docu Code', (doc_description) as 'Docu Description' from tbl_documents order by doc_description"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                frmStudentGradeEditor.dgSchoolList.Rows.Add("", dr.Item("ID").ToString, dr.Item("Docu Code").ToString, dr.Item("Docu Description").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dg_doc_list.Rows.Clear()
        End Try
    End Sub

    Private Sub btnSearchStudent_Click(sender As Object, e As EventArgs) Handles btnSearchStudent.Click
        frmTitle.Text = "Search Student"
        SearchPanel.Visible = True
        AckStudentList()
        dgStudentList.BringToFront()
        txtSearch.Select()
        btnAddDoc.Visible = False
        lblAddDoc.Visible = False
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

    Public Sub AckSchoolList()
        Try

            dgSchoolList.Rows.Clear()
            Dim sql As String
        sql = "select (schl_id) as 'ID', (schl_code) as 'Code', (schl_name) as 'School Name', (schl_address) as 'School Address' from tbl_schools where (schl_code LIKE '%" & txtSearch.Text & "%' or schl_name LIKE '%" & txtSearch.Text & "%') order by schl_name asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgSchoolList.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("School Name").ToString, dr.Item("School Address").ToString)
        End While
        dr.Close()
        cn.Close()

        Catch ex As Exception
        dr.Close()
            cn.Close()
            dgSchoolList.Rows.Clear()

        End Try
    End Sub

    Private Sub btnSearchSchool_Click(sender As Object, e As EventArgs) Handles btnSearchSchool.Click
        frmTitle.Text = "Search School"
        SearchPanel.Visible = True
            AckSchoolList()
            dgSchoolList.BringToFront()
            txtSearch.Select()

    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If frmTitle.Text = "Search Student" Then
            StudentName = dgStudentList.CurrentRow.Cells(2).Value & " " & dgStudentList.CurrentRow.Cells(5).Value & ", " & dgStudentList.CurrentRow.Cells(3).Value & " " & dgStudentList.CurrentRow.Cells(4).Value
            StudentID = dgStudentList.CurrentRow.Cells(1).Value
            CourseID = dgStudentList.CurrentRow.Cells(9).Value
            YearLevel = dgStudentList.CurrentRow.Cells(7).Value
            Gender = dgStudentList.CurrentRow.Cells(6).Value
            txtStudent.Text = StudentName
        ElseIf frmTitle.Text = "Search School" Then
            schoolid = dgSchoolList.CurrentRow.Cells(0).Value
            schoolname = dgSchoolList.CurrentRow.Cells(2).Value
            schooladdress = dgSchoolList.CurrentRow.Cells(3).Value
            txtSchool.Text = schoolname
        ElseIf frmTitle.Text = "Search Credential" Then
            doc_id = dgDocuList.CurrentRow.Cells(0).Value
            doc_code = dgDocuList.CurrentRow.Cells(1).Value
            doc_desc = dgDocuList.CurrentRow.Cells(2).Value
            txtDocDesc.Text = doc_desc
        End If
        SearchPanel.Visible = False
        txtSearch.Text = ""
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
        If dg_doc_list.Rows.Count = 0 Then
            MsgBox("No record to saved.", vbCritical)
        Else
            If MsgBox("Are you sure you want to save this record?.", vbYesNo + vbQuestion) = vbYes Then
                Try
                    AutoCode()
                    For Each row As DataGridViewRow In dg_doc_list.Rows
                        query("INSERT INTO `tbl_documents_reference_out`(`ref_code`, `ref_student_id`, `ref_release_type`, `ref_schoold_id`, `ref_received_by`, `ref_contact`, `ref_doc_id`, `ref_remarks`, `ref_released_date`, `ref_status`, `ref_user`) VALUES ('" & dCode & "', '" & row.Cells(0).Value & "', '" & row.Cells(7).Value & "', " & schoolid & ", '" & row.Cells(8).Value & "', '" & row.Cells(9).Value & "', " & row.Cells(2).Value & ", '" & txtRemarks.Text & "','" & row.Cells(6).Value & "', '" & row.Cells(5).Value & "', " & str_userid & ")")
                    Next
                    MsgBox("Record successfully saved.", vbInformation)
                    clearall()
                    AcknowledgementList2()
                    Me.Close()
                Catch ex As Exception
                    MsgBox("Failed to save record." & ex.Message, vbCritical)
                End Try
            End If
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If dg_doc_list.Rows.Count = 0 Then
            MsgBox("No record to saved.", vbCritical)
        Else
            If MsgBox("Are you sure you want to update this record?.", vbYesNo + vbQuestion) = vbYes Then
                Try
                    query("UPDATE `tbl_documents_reference_out` SET `ref_schoold_id` = " & schoolid & ", `ref_remarks` = '" & txtRemarks.Text & "' WHERE ref_code = '" & dCode & "'")
                    For Each row As DataGridViewRow In dg_doc_list.Rows
                        'Dim oDate As DateTime = Convert.ToDateTime(row.Cells(4).Value)
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("select * from tbl_documents_reference_out where ref_code = '" & dCode & "' and ref_student_id = '" & row.Cells(0).Value & "' and ref_doc_id = '" & row.Cells(2).Value & "'", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            dr.Close()
                            cn.Close()
                            query("UPDATE `tbl_documents_reference_out` SET `ref_release_type`='" & row.Cells(7).Value & "',`ref_received_by`='" & row.Cells(8).Value & "',`ref_contact`='" & row.Cells(9).Value & "',`ref_released_date`='" & row.Cells(6).Value & "',`ref_status`='" & row.Cells(5).Value & "', `ref_user` = " & str_userid & " WHERE ref_code = '" & dCode & "' and ref_student_id = '" & row.Cells(0).Value & "' and ref_doc_id = '" & row.Cells(2).Value & "'")
                        Else
                            dr.Close()
                            cn.Close()
                            query("INSERT INTO `tbl_documents_reference_out`(`ref_code`, `ref_student_id`, `ref_release_type`, `ref_schoold_id`, `ref_received_by`, `ref_contact`, `ref_doc_id`, `ref_remarks`, `ref_released_date`, `ref_status`, `ref_user`) VALUES ('" & dCode & "', '" & row.Cells(0).Value & "', '" & row.Cells(7).Value & "', " & schoolid & ", '" & row.Cells(8).Value & "', '" & row.Cells(9).Value & "', " & row.Cells(2).Value & ", '" & txtRemarks.Text & "','" & row.Cells(6).Value & "', '" & row.Cells(5).Value & "', " & str_userid & ")")
                        End If
                    Next
                    MsgBox("Record successfully updated.", vbInformation)
                    clearall()
                    AcknowledgementList2()
                    Me.Close()
                Catch ex As Exception
                    MsgBox("Failed to update record." & ex.Message, vbCritical)
                End Try
            End If
        End If
    End Sub

    Private Sub btnSearchDocument_Click(sender As Object, e As EventArgs) Handles btnSearchDocument.Click
        If txtStudent.Text = String.Empty Then
        Else
            SearchPanel.Visible = True
            dgDocuList.BringToFront()
            frmTitle.Text = "Search Credential"
            AckDocumentList()
            btnAddDoc.Visible = True
            lblAddDoc.Visible = True
        End If
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

    Private Sub dg_doc_list_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dg_doc_list.CellEnter
        'Try
        If e.ColumnIndex = dg_doc_list.Columns("DateColumn").Index Then
            dateTimePicker.Format = DateTimePickerFormat.Custom
            dateTimePicker.CustomFormat = "yyyy/MM/dd"
            dateTimePicker.Size = New Size(100, 20)
            dateTimePicker.Location = dg_doc_list.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True).Location

            ' Get the cell value
            Dim cellValue As Object = dg_doc_list(e.ColumnIndex, e.RowIndex).Value
            Dim dateValue As DateTime

            ' Try to parse the cell value as DateTime
            If cellValue IsNot Nothing AndAlso DateTime.TryParse(cellValue.ToString(), dateValue) Then
                dateTimePicker.Value = dateValue
            Else
                ' If the value cannot be parsed, use the current date
                dateTimePicker.Value = DateTime.Now
            End If

            dateTimePicker.Visible = True
            dg_doc_list.Controls.Add(dateTimePicker)
            AddHandler dateTimePicker.ValueChanged, AddressOf DateTimePicker_ValueChanged
        End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub DateTimePicker_ValueChanged(sender As Object, e As EventArgs)
        Dim currentCell As DataGridViewCell = dg_doc_list.CurrentCell
        If currentCell IsNot Nothing AndAlso currentCell.ColumnIndex = dg_doc_list.Columns("DateColumn").Index Then
            If dg_doc_list.CurrentRow.Cells(3).Value = "Released" Then
                currentCell.Value = dateTimePicker.Text
                dg_doc_list.EndEdit()
            Else
                dg_doc_list.EndEdit()
            End If
        End If
    End Sub

    Private Sub dg_doc_list_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dg_doc_list.CellLeave
        If dg_doc_list.Columns(e.ColumnIndex).Name = "DateColumn" Then
            dateTimePicker.Visible = False
            RemoveHandler dateTimePicker.ValueChanged, AddressOf DateTimePicker_ValueChanged
        End If
    End Sub

    Private Sub dg_doc_list_Leave(sender As Object, e As EventArgs) Handles dg_doc_list.Leave
        dateTimePicker.Visible = False
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If frmTitle.Text = "Search Student" Then
            AckStudentList()
        ElseIf frmTitle.Text = "Search School" Then
            AckSchoolList()
        ElseIf frmTitle.Text = "Search Credential" Then
            AckDocumentList()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            SearchPanel.Visible = False
            dg_doc_list.Rows.Clear()
            txtSearch.Text = String.Empty
        Else
            Me.Close()
        End If
    End Sub

    Sub clearall()
        schoolid = 0
        schoolname = ""
        schooladdress = ""

        StudentID = ""
        StudentName = ""
        CourseID = 0
        Course = ""
        YearLevel = ""
        Gender = ""

        dCode = ""

        dg_doc_list.Rows.Clear()
        txtStudent.Text = ""
        txtSchool.Text = ""
        txtRemarks.Text = ""
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearall()
        Me.Close()
    End Sub

    Private Sub dg_doc_list_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_doc_list.CellContentClick
        Dim colname As String = dg_doc_list.Columns(e.ColumnIndex).Name
        If colname = "colRemove" Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_documents_reference_out where ref_code = '" & dCode & "' and ref_student_id = '" & StudentID & "' and ref_doc_id = '" & dg_doc_list.CurrentRow.Cells(0).Value & "'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Close()
                cn.Close()
                If MsgBox("Are you sure you want to remove this record?", vbYesNo + vbQuestion) = vbYes Then
                    query("DELETE FROM `tbl_documents_reference_out` WHERE ref_code = '" & dCode & "' and ref_student_id = '" & StudentID & "' and ref_doc_id = '" & dg_doc_list.CurrentRow.Cells(0).Value & "'")
                    dg_doc_list.Rows.RemoveAt(dg_doc_list.CurrentRow.Index)
                Else
                    MsgBox("Removing record cancelled.", vbInformation)
                End If
            Else
                dr.Close()
                cn.Close()
                dg_doc_list.Rows.RemoveAt(dg_doc_list.CurrentRow.Index)
            End If
            AcknowledgementList2()
        End If
    End Sub

    Private Sub dg_doc_list_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dg_doc_list.CellEndEdit
        If dg_doc_list.CurrentRow.Cells(3).Value = "Pending" Then
            dg_doc_list.CurrentRow.Cells(4).Value = String.Empty
        End If
    End Sub

    Private Sub btnAddToList_Click(sender As Object, e As EventArgs) Handles btnAddToList.Click
        If IS_EMPTY(txtStudent) = True Then Return
        If IS_EMPTY(txtDocDesc) = True Then Return
        dg_doc_list.Rows.Add(StudentID, StudentName, doc_id, doc_code, doc_desc, "Pending")
        MsgBox("Added to the list.", vbInformation)
        txtDocDesc.Text = String.Empty
        txtStudent.Text = String.Empty
        AddPanel.Visible = False
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If IS_EMPTY(txtSchool) = True Then Return
        AddPanel.Visible = True
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        AddPanel.Visible = False
        txtStudent.Text = String.Empty
        txtDocDesc.Text = String.Empty
    End Sub
End Class