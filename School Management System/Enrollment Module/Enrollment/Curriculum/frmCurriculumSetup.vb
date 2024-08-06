Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms

Public Class frmCurriculumSetup
    Public currID As Integer = 0
    Private Sub frmCurriculumSetup_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        If frmMain.systemModule.Text = "College Module" Then
            cbGroup.Items.Clear()
            cbGroup.Items.Add("English")
            cbGroup.Items.Add("Filipino")
            cbGroup.Items.Add("Math")
            cbGroup.Items.Add("Social Science")
            cbGroup.Items.Add("Elective Major")
            cbGroup.Items.Add("Elective Minor")
            cbGroup.Items.Add("Health Science")
            cbGroup.Items.Add("Physical Education")
            cbGroup.Items.Add("CWTS CMT")

            cbGroup.Items.Add("General Education Courses")
            cbGroup.Items.Add("Core Courses")
            cbGroup.Items.Add("Professional Courses")
            cbGroup.Items.Add("Professional Electives and Math Requirements")
            cbGroup.Items.Add("Cognates")
            cbGroup.Items.Add("Physical Education")
            cbGroup.Items.Add("NSTP")
            cbGroup.Items.Add("School Requirements")
        Else
            cbGroup.Items.Clear()
            cbGroup.Items.Add("Core")
            cbGroup.Items.Add("Applied")
            cbGroup.Items.Add("Specialized")
        End If

        If frmMain.systemModule.Text = "College Module" Then
            cbPassingGrade.Items.Clear()
            cbPassingGrade.Items.Add("3.0")
            cbPassingGrade.Items.Add("2.9")
            cbPassingGrade.Items.Add("2.8")
            cbPassingGrade.Items.Add("2.7")
            cbPassingGrade.Items.Add("2.6")
            cbPassingGrade.Items.Add("2.5")
            cbPassingGrade.Items.Add("2.4")
            cbPassingGrade.Items.Add("2.3")
            cbPassingGrade.Items.Add("2.2")
            cbPassingGrade.Items.Add("2.1")
            cbPassingGrade.Items.Add("2.0")
            cbPassingGrade.Items.Add("1.9")
            cbPassingGrade.Items.Add("1.8")
            cbPassingGrade.Items.Add("1.7")
            cbPassingGrade.Items.Add("1.6")
            cbPassingGrade.Items.Add("1.5")
        Else
            cbPassingGrade.Items.Clear()
            cbPassingGrade.Items.Add("75")
            cbPassingGrade.Items.Add("76")
            cbPassingGrade.Items.Add("77")
            cbPassingGrade.Items.Add("78")
            cbPassingGrade.Items.Add("79")
            cbPassingGrade.Items.Add("80")
            cbPassingGrade.Items.Add("81")
            cbPassingGrade.Items.Add("82")
            cbPassingGrade.Items.Add("83")
            cbPassingGrade.Items.Add("84")
            cbPassingGrade.Items.Add("85")
            cbPassingGrade.Items.Add("86")
            cbPassingGrade.Items.Add("87")
            cbPassingGrade.Items.Add("88")
            cbPassingGrade.Items.Add("89")
            cbPassingGrade.Items.Add("90")
        End If
    End Sub
    Sub LibraryCurriculumSubjectList()
        dgSubjectList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_group) as 'Group', (subject_units) as 'Units', '' as 'Prerequisite', (subject_active_status) as 'Status' from tbl_subject where (subject_code LIKE '" & txtSearch.Text & "%' or subject_description LIKE '" & txtSearch.Text & "%') order by subject_description asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgSubjectList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Group").ToString, dr.Item("Units").ToString, dr.Item("Prerequisite").ToString, dr.Item("Status").ToString)
        End While
        dr.Close()
        cn.Close()

        dgPanelPadding(dgSubjectList, dgPanel)
    End Sub

    Sub PreRequisiteCurriculumSubjectList()
        dgSubjectList.Rows.Clear()

        Dim i As Integer
        'i += 1
        'dgSubjectList.Rows.Add(i, "1st Year Standing", "1st Year Standing", "", "", "", "", "", "", "")
        'i += 1
        'dgSubjectList.Rows.Add(i, "2nd Year Standing", "2nd Year Standing", "", "", "", "", "", "", "")
        'i += 1
        'dgSubjectList.Rows.Add(i, "3rd Year Standing", "3rd Year Standing", "", "", "", "", "", "", "")
        'i += 1
        'dgSubjectList.Rows.Add(i, "4th Year Standing", "4th Year Standing", "", "", "", "", "", "", "")
        Dim sql As String
        sql = "select (t2.subject_id) as 'ID', (t2.subject_code) as 'Code', (t2.subject_description) as 'Description', (t2.subject_Type) as 'Type', (t1.subjectGroup) as 'Group', (t2.subject_units) as 'Units', '' as 'Prerequisite', (t2.subject_active_status) as 'Status', t1.id as subcurrid from tbl_curriculum_subjects t1 JOIN tbl_subject t2 ON t1.subjectID = t2.subject_id where (t2.subject_code LIKE '" & txtSearch.Text & "%' or t2.subject_description LIKE '" & txtSearch.Text & "%') and t1.id not in (" & CInt(dgCurriculumSubjects.CurrentRow.Cells(8).Value) & ") order by t2.subject_description"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            dgSubjectList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Group").ToString, dr.Item("Units").ToString, dr.Item("Prerequisite").ToString, dr.Item("Status").ToString, dr.Item("subcurrid").ToString)
        End While
        dr.Close()
        cn.Close()
        dgPanelPadding(dgSubjectList, dgPanel)
    End Sub

    Sub CurrSubjectList()
        dgCurriculumSubjects.Rows.Clear()
        Dim sql As String
        sql = "select (subject_id) as 'ID', (subject_code) as 'Code', (subject_description) as 'Description', (subject_Type) as 'Type', (subject_units) as 'Units', `subjectGroup`, `passingGrade`, `subjectIDpreq`, id as recordID from tbl_curriculum_subjects JOIN tbl_subject ON tbl_curriculum_subjects.subjectID = tbl_subject.subject_id where `yearLevel`= '" & cbYearLevel.Text & "' and `semester` = '" & cbSemester.Text & "' and `curriculumID` = " & currID & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgCurriculumSubjects.Rows.Add(dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString, dr.Item("subjectGroup").ToString, dr.Item("passingGrade").ToString, dr.Item("subjectIDpreq").ToString, dr.Item("recordID").ToString)
        End While
        dr.Close()
        cn.Close()

        dgPanelPadding(dgCurriculumSubjects, dgPanel)

        If dgCurriculumSubjects.RowCount = 0 Then
        Else
            'Try
            For Each row As DataGridViewRow In dgCurriculumSubjects.Rows
                    If row.Cells(7).Value.ToString = "0" Then
                    Else
                        cn.Close()
                        cn.Open()
                    cm = New MySqlCommand("SELECT subject_code from tbl_subject where subject_id = " & row.Cells(7).Value & "", cn)
                    row.Cells(10).Value = cm.ExecuteScalar
                        cn.Close()
                    End If
                    row.Cells(11).Value = "-"
                Next
            'Catch ex As Exception
            '    cn.Close()
            '    MsgBox(ex.Message, vbCritical)
            'End Try
        End If
    End Sub

    Private Sub btnAddSubj_Click(sender As Object, e As EventArgs) Handles btnAddSubj.Click
        SearchPanel.Visible = True
        frmTitle.Text = "Search Subject"
        LibraryCurriculumSubjectList()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If frmTitle.Text = "Search Subject" Then

            Dim found As Boolean = False
            For Each row As DataGridViewRow In dgCurriculumSubjects.Rows
                If row.Cells(0).Value = dgSubjectList.CurrentRow.Cells(1).Value Or row.Cells(2).Value = dgSubjectList.CurrentRow.Cells(3).Value Then
                    found = True
                Else
                    found = False
                End If
            Next
            If found = True Then
                MsgBox("Subject " & dgSubjectList.CurrentRow.Cells(2).Value & " " & dgSubjectList.CurrentRow.Cells(3).Value & " has already been added to the list.", vbCritical)
                SearchPanel.Visible = False
            Else
                dgCurriculumSubjects.Rows.Add(dgSubjectList.CurrentRow.Cells(1).Value, dgSubjectList.CurrentRow.Cells(2).Value, dgSubjectList.CurrentRow.Cells(3).Value, dgSubjectList.CurrentRow.Cells(4).Value, dgSubjectList.CurrentRow.Cells(6).Value)

                Dim lastRowIndex As Integer = dgCurriculumSubjects.Rows.Count - 1
                Dim imageFromPictureBox As Image = pic1.Image
                dgCurriculumSubjects.Rows(lastRowIndex).Cells(13).Value = imageFromPictureBox

                SearchPanel.Visible = False
            End If
        ElseIf frmTitle.Text = "Search Subject Replacement" Then

            Dim found As Boolean = False
            For Each row As DataGridViewRow In dgCurriculumSubjects.Rows
                If row.Cells(0).Value = dgSubjectList.CurrentRow.Cells(1).Value Then
                    found = True
                Else
                    found = False
                End If
            Next
            If found = True Then
                MsgBox("Subject " & dgSubjectList.CurrentRow.Cells(2).Value & " " & dgSubjectList.CurrentRow.Cells(3).Value & " has already been added to the list.", vbCritical)
                SearchPanel.Visible = False
            Else
                dgCurriculumSubjects.CurrentRow.Cells(0).Value = dgSubjectList.CurrentRow.Cells(1).Value
                dgCurriculumSubjects.CurrentRow.Cells(1).Value = dgSubjectList.CurrentRow.Cells(2).Value
                dgCurriculumSubjects.CurrentRow.Cells(2).Value = dgSubjectList.CurrentRow.Cells(3).Value
                dgCurriculumSubjects.CurrentRow.Cells(3).Value = dgSubjectList.CurrentRow.Cells(4).Value
                dgCurriculumSubjects.CurrentRow.Cells(4).Value = dgSubjectList.CurrentRow.Cells(6).Value

                Dim pictureBoxImage As Bitmap = CType(pic1.Image, Bitmap)
                Dim dataGridViewImage As Bitmap = CType(dgCurriculumSubjects.CurrentRow.Cells(13).Value, Bitmap)

                If CompareImages(pictureBoxImage, dataGridViewImage) Then
                Else
                    Dim imageFromPictureBox As Image = pic1.Image
                    dgCurriculumSubjects.CurrentRow.Cells(14).Value = imageFromPictureBox
                End If

                SearchPanel.Visible = False
            End If
        ElseIf frmTitle.Text = "Search Pre-requisite Subject" Then
            dgCurriculumSubjects.CurrentRow.Cells(7).Value = dgSubjectList.CurrentRow.Cells(1).Value
            dgCurriculumSubjects.CurrentRow.Cells(10).Value = dgSubjectList.CurrentRow.Cells(2).Value
            SearchPanel.Visible = False
        End If
    End Sub

    Private Sub dgCurriculumSubjects_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCurriculumSubjects.CellContentClick
        Dim colname As String = dgCurriculumSubjects.Columns(e.ColumnIndex).Name
        If colname = "colAddPreRequisite" Then
            Dim pictureBoxImage As Bitmap = CType(pic1.Image, Bitmap)
            Dim dataGridViewImage As Bitmap = CType(dgCurriculumSubjects.CurrentRow.Cells(13).Value, Bitmap)
            Dim dataGridViewImage2 As Bitmap = CType(dgCurriculumSubjects.CurrentRow.Cells(14).Value, Bitmap)

            If CompareImages(pictureBoxImage, dataGridViewImage) Or CompareImages(pictureBoxImage, dataGridViewImage2) Then
                PreRequisiteCurriculumSubjectList()
                SearchPanel.Visible = True
                frmTitle.Text = "Search Pre-requisite Subject"
            End If

        ElseIf colname = "subjectAdd" Then
            If dgCurriculumSubjects.CurrentRow.Cells(12).Value = "-" Then
            Else
                If dgCurriculumSubjects.CurrentRow.Cells(5).Value = String.Empty Then
                    MsgBox("Please select subject group.", vbCritical)
                ElseIf dgCurriculumSubjects.CurrentRow.Cells(6).Value = String.Empty Then
                    MsgBox("Please select passing grade.", vbCritical)
                Else
                    Dim pictureBoxImage As Bitmap = CType(pic1.Image, Bitmap)
                    Dim dataGridViewImage As Bitmap = CType(dgCurriculumSubjects.CurrentRow.Cells(13).Value, Bitmap)

                    If CompareImages(pictureBoxImage, dataGridViewImage) Then
                        If MsgBox("Are you sure you want to add this subject to the curriculum?", vbYesNo + vbQuestion) = vbYes Then
                            Dim subjectIDpreq As Integer = If(dgCurriculumSubjects.CurrentRow.Cells(7).Value = String.Empty, 0, CInt(dgCurriculumSubjects.CurrentRow.Cells(7).Value))
                            query("INSERT INTO tbl_curriculum_subjects(`curriculumID`, `subjectID`, `subjectGroup`, `passingGrade`, `yearLevel`, `semester`, `subjectIDpreq`) VALUES (" & currID & ", " & CInt(dgCurriculumSubjects.CurrentRow.Cells(0).Value) & ", '" & dgCurriculumSubjects.CurrentRow.Cells(5).Value & "', " & dgCurriculumSubjects.CurrentRow.Cells(6).Value & ", '" & cbYearLevel.Text & "', '" & cbSemester.Text & "', " & subjectIDpreq & ")")

                            cn.Close()
                            cn.Open()
                            cm = New MySqlCommand("SELECT id from tbl_curriculum_subjects where subjectID = " & dgCurriculumSubjects.CurrentRow.Cells(0).Value & " and yearLevel = '" & cbYearLevel.Text & "' and semester = '" & cbSemester.Text & "'", cn)
                            dgCurriculumSubjects.CurrentRow.Cells(8).Value = cm.ExecuteScalar
                            cn.Close()

                            Dim imageFromPictureBox As Image = pic2.Image
                            dgCurriculumSubjects.CurrentRow.Cells(13).Value = imageFromPictureBox

                            MsgBox("Subject successfully added to the curriculum.", vbInformation)
                            UserActivity("Added subject " & dgCurriculumSubjects.CurrentRow.Cells(2).Value & " - " & dgCurriculumSubjects.CurrentRow.Cells(3).Value & " to the curriculum " & txtCurriculum.Text & ".", "CURRICULUM SETUP")
                        Else
                            MsgBox("Adding subject to the curriculum was cancelled.", vbInformation)
                        End If
                    End If
                End If
            End If
        ElseIf colname = "subjectChange" Then
            Dim pictureBoxImage As Bitmap = CType(pic1.Image, Bitmap)
            Dim dataGridViewImage As Bitmap = CType(dgCurriculumSubjects.CurrentRow.Cells(14).Value, Bitmap)

            If CompareImages(pictureBoxImage, dataGridViewImage) Then
                If MsgBox("Are you sure you want to update this curriculum subject?", vbYesNo + vbQuestion) = vbYes Then

                    Dim subjectIDpreq As Integer = If(dgCurriculumSubjects.CurrentRow.Cells(7).Value = String.Empty, 0, CInt(dgCurriculumSubjects.CurrentRow.Cells(7).Value))
                    query("UPDATE tbl_curriculum_subjects SET `subjectID` = " & CInt(dgCurriculumSubjects.CurrentRow.Cells(0).Value) & ", `subjectGroup` = '" & dgCurriculumSubjects.CurrentRow.Cells(5).Value & "', `passingGrade` = '" & dgCurriculumSubjects.CurrentRow.Cells(6).Value & "', `yearLevel` = '" & cbYearLevel.Text & "', `semester` = '" & cbSemester.Text & "', `subjectIDpreq` = " & subjectIDpreq & " WHERE id = " & CInt(dgCurriculumSubjects.CurrentRow.Cells(8).Value) & "")

                    Dim imageFromPictureBox As Image = pic3.Image
                    dgCurriculumSubjects.CurrentRow.Cells(14).Value = imageFromPictureBox

                    MsgBox("Successfully updated the subject curriculum.", vbInformation)
                    UserActivity("Updated a subject " & dgCurriculumSubjects.CurrentRow.Cells(2).Value & " - " & dgCurriculumSubjects.CurrentRow.Cells(3).Value & " from the curriculum " & txtCurriculum.Text & ".", "CURRICULUM SETUP")
                Else
                    MsgBox("Updating curriculum subject was cancelled.", vbInformation)
                    Dim imageFromPictureBox As Image = pic3.Image
                    dgCurriculumSubjects.CurrentRow.Cells(14).Value = imageFromPictureBox
                    Try
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("SELECT t1.subjectID, t2.subject_code, t2.subject_description, t2.subject_type, t2.subject_units, t1.subjectGroup, t1.passingGrade FROM `tbl_curriculum_subjects` t1 JOIN tbl_subject t2 ON t1.subjectID = t2.subject_id WHERE id = " & CInt(dgCurriculumSubjects.CurrentRow.Cells(8).Value) & "", cn)
                        dr = cm.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            dgCurriculumSubjects.CurrentRow.Cells(0).Value = CInt(dr.Item("subjectID").ToString)
                            dgCurriculumSubjects.CurrentRow.Cells(1).Value = dr.Item("subject_code").ToString
                            dgCurriculumSubjects.CurrentRow.Cells(2).Value = dr.Item("subject_description").ToString
                            dgCurriculumSubjects.CurrentRow.Cells(3).Value = dr.Item("subject_type").ToString
                            dgCurriculumSubjects.CurrentRow.Cells(4).Value = dr.Item("subject_units").ToString
                            dgCurriculumSubjects.CurrentRow.Cells(5).Value = dr.Item("subjectGroup").ToString
                            dgCurriculumSubjects.CurrentRow.Cells(6).Value = dr.Item("passingGrade").ToString
                        Else
                        End If
                        dr.Close()
                        cn.Close()
                    Catch ex As Exception
                    End Try
                End If
            Else
                If MsgBox("Do you want to replace the subject?", vbYesNo + vbQuestion) = vbYes Then
                    SearchPanel.Visible = True
                    frmTitle.Text = "Search Subject Replacement"
                    LibraryCurriculumSubjectList()
                Else
                    Dim imageFromPictureBox As Image = pic1.Image
                    dgCurriculumSubjects.CurrentRow.Cells(14).Value = imageFromPictureBox
                    MsgBox("You may now apply other changes.", vbInformation)
                End If
            End If
        ElseIf colname = "subjectRemove" Then
            Dim pictureBoxImage As Bitmap = CType(pic1.Image, Bitmap)
            Dim dataGridViewImage As Bitmap = CType(dgCurriculumSubjects.CurrentRow.Cells(13).Value, Bitmap)
            Dim mssg As String = ""
            If CompareImages(pictureBoxImage, dataGridViewImage) Then
                mssg = "Are you sure you want to remove this curriculum subject?"
            Else
                mssg = "Are you sure you want to remove this curriculum subject from the record?"
            End If
            If MsgBox(mssg, vbYesNo + vbQuestion) = vbYes Then
                If CompareImages(pictureBoxImage, dataGridViewImage) Then
                    dgCurriculumSubjects.Rows.RemoveAt(dgCurriculumSubjects.CurrentRow.Index)
                    MsgBox("Successfully removed the curriculum subject.", vbInformation)
                Else
                    query("DELETE FROM `tbl_curriculum_subjects` WHERE id = " & CInt(dgCurriculumSubjects.CurrentRow.Cells(8).Value) & "")
                    UserActivity("Removed subject " & dgCurriculumSubjects.CurrentRow.Cells(2).Value & " - " & dgCurriculumSubjects.CurrentRow.Cells(3).Value & " from the curriculum " & txtCurriculum.Text & ".", "CURRICULUM SETUP")
                    dgCurriculumSubjects.Rows.RemoveAt(dgCurriculumSubjects.CurrentRow.Index)
                    MsgBox("Successfully removed the curriculum subject.", vbInformation)
                End If
            Else
                MsgBox("Removing subject to the curriculum was cancelled.", vbExclamation)
            End If
        End If
    End Sub

    ' Function to compare two images
    Private Function CompareImages(image1 As Bitmap, image2 As Bitmap) As Boolean
        If image1.Width <> image2.Width OrElse image1.Height <> image2.Height Then
            Return False
        End If

        For x As Integer = 0 To image1.Width - 1
            For y As Integer = 0 To image1.Height - 1
                If image1.GetPixel(x, y) <> image2.GetPixel(x, y) Then
                    Return False
                End If
            Next
        Next

        Return True
    End Function


    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If frmTitle.Text = "Search Pre-requisite Subject" Then
            PreRequisiteCurriculumSubjectList()
        Else
            LibraryCurriculumSubjectList()
        End If
    End Sub

    Private Sub cbYearLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbYearLevel.SelectedIndexChanged
        CurrSubjectList()
    End Sub

    Private Sub cbSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSemester.SelectedIndexChanged
        CurrSubjectList()
    End Sub

    Private Sub dgCurriculumSubjects_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgCurriculumSubjects.CellEnter
        Try
            Dim pictureBoxImage1 As Bitmap = CType(pic1.Image, Bitmap)
            Dim dataGridViewImage1 As Bitmap = CType(dgCurriculumSubjects.CurrentRow.Cells(14).Value, Bitmap)
            Dim dataGridViewImage2 As Bitmap = CType(dgCurriculumSubjects.CurrentRow.Cells(13).Value, Bitmap)
            If CompareImages(pictureBoxImage1, dataGridViewImage1) Or CompareImages(pictureBoxImage1, dataGridViewImage2) Then
                cbGroup.ReadOnly = False
                cbPassingGrade.ReadOnly = False
            Else
                cbGroup.ReadOnly = True
                cbPassingGrade.ReadOnly = True
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If MsgBox("This will change all changes made, adding/updating, are you sure you want to continue?", vbYesNo + vbQuestion) = vbYes Then

            Dim pictureBoxImage As Bitmap = CType(pic1.Image, Bitmap)
            Dim FoundAdd As Boolean = False
            Dim FoundUpdate As Boolean = False
            For Each row As DataGridViewRow In dgCurriculumSubjects.Rows
                Dim dataGridViewImage As Bitmap = CType(row.Cells(13).Value, Bitmap)

                If CompareImages(pictureBoxImage, dataGridViewImage) Then
                    Dim subjectIDpreq As Integer = If(row.Cells(7).Value = String.Empty, 0, CInt(row.Cells(7).Value))
                    query("INSERT INTO tbl_curriculum_subjects(`curriculumID`, `subjectID`, `subjectGroup`, `passingGrade`, `yearLevel`, `semester`, `subjectIDpreq`) VALUES (" & currID & ", " & CInt(row.Cells(0).Value) & ", '" & row.Cells(5).Value & "', " & row.Cells(6).Value & ", '" & cbYearLevel.Text & "', '" & cbSemester.Text & "', " & subjectIDpreq & ")")

                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT id from tbl_curriculum_subjects where subjectID = " & row.Cells(0).Value & " and yearLevel = '" & cbYearLevel.Text & "' and semester = '" & cbSemester.Text & "'", cn)
                    row.Cells(8).Value = cm.ExecuteScalar
                    cn.Close()

                    Dim imageFromPictureBox As Image = pic2.Image
                    row.Cells(13).Value = imageFromPictureBox

                    UserActivity("Added subject " & row.Cells(2).Value & " - " & row.Cells(3).Value & " to the curriculum " & txtCurriculum.Text & ".", "CURRICULUM SETUP")
                    FoundAdd = True
                Else

                End If
            Next
            For Each row As DataGridViewRow In dgCurriculumSubjects.Rows
                Dim dataGridViewImage2 As Bitmap = CType(row.Cells(14).Value, Bitmap)

                If CompareImages(pictureBoxImage, dataGridViewImage2) Then

                    Dim subjectIDpreq As Integer = If(row.Cells(7).Value = String.Empty, 0, CInt(row.Cells(7).Value))
                    query("UPDATE tbl_curriculum_subjects SET `subjectID` = " & CInt(row.Cells(0).Value) & ", `subjectGroup` = '" & row.Cells(5).Value & "', `passingGrade` = '" & row.Cells(6).Value & "', `yearLevel` = '" & cbYearLevel.Text & "', `semester` = '" & cbSemester.Text & "', `subjectIDpreq` = " & subjectIDpreq & " WHERE id = " & CInt(row.Cells(8).Value) & "")

                    Dim imageFromPictureBox As Image = pic3.Image
                    row.Cells(14).Value = imageFromPictureBox

                    UserActivity("Updated a subject " & row.Cells(2).Value & " - " & row.Cells(3).Value & " from the curriculum " & txtCurriculum.Text & ".", "CURRICULUM SETUP")
                    FoundUpdate = True
                Else

                End If
            Next
            If FoundAdd = True Then
                MsgBox("There no suject(s) added. Subject(s) successfully added to the curriculum.", vbInformation)
            End If
            If FoundAdd = True Then
                MsgBox("There no suject(s) updated. Successfully updated the subject(s) curriculum.", vbInformation)
            End If
            If FoundAdd = False And FoundUpdate = False Then
                MsgBox("There no suject(s) added or updated.", vbInformation)
            End If
        Else
            MsgBox("Saving changes to the curriculum was cancelled.", vbInformation)
        End If
    End Sub

    Private Sub reload_Click(sender As Object, e As EventArgs) Handles reload.Click
        Try
            Dim pictureBoxImage As Bitmap = CType(pic1.Image, Bitmap)
            Dim dataGridViewImage As Bitmap = CType(dgCurriculumSubjects.CurrentRow.Cells(13).Value, Bitmap)
            Dim dataGridViewImage2 As Bitmap = CType(dgCurriculumSubjects.CurrentRow.Cells(14).Value, Bitmap)
            Dim found As Boolean = False

            For Each row As DataGridViewRow In dgCurriculumSubjects.Rows
                If CompareImages(pictureBoxImage, dataGridViewImage) Or CompareImages(pictureBoxImage, dataGridViewImage2) Then
                    found = True
                Else
                    found = False
                End If
            Next
            If found = True Then
                If MsgBox("There are still unsave changes. Are you sure you want to continue to reload subject list?", vbYesNo + vbQuestion) = vbYes Then
                    CurrSubjectList()
                Else

                End If
            Else

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnRemoveP_Click(sender As Object, e As EventArgs) Handles btnRemoveP.Click
        Try
            dgCurriculumSubjects.CurrentRow.Cells(7).Value = "0"
            dgCurriculumSubjects.CurrentRow.Cells(10).Value = ""
            SearchPanel.Visible = False
        Catch ex As Exception
            SearchPanel.Visible = False
        End Try
    End Sub
End Class