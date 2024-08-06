Public Class frmStudentInfo
    Public Shared slidePanels As New List(Of Panel)()
    Public Shared currentSlideIndex As Integer = 0

    Public Shared OldStudentID As String = ""
    Public Shared NewStudentID As String = ""

    Public Shared StudentIDLastNumber As String = ""
    Public Shared StudentID As String = ""
    Public Shared StudentStatus As String = ""

    Public Shared PWDID As Integer = 0
    Public Shared RelegionID As Integer = 0
    Public Shared CourseID As Integer = 0
    Public Shared PeriodID As Integer = 0
    Public Shared ScholarshipID As Integer = 0
    Public Shared GraduatedPeriodID As Integer = 0

    Public Shared PrimaryID As Integer = 0
    Public Shared JuniorHighID As Integer = 0
    Public Shared SeniorHighID As Integer = 0
    Public Shared CollegeID As Integer = 0
    Public Shared MastersID As Integer = 0
    Public Shared DoctorateID As Integer = 0
    Public Shared LastSchoolID As Integer = 0
    Public Shared TransferredSchoolID As Integer = 0

    Public Shared cbHScardValue As Integer = 0
    Public Shared cbF137Value As Integer = 0
    Public Shared cbBirthCertValue As Integer = 0
    Public Shared cbMarriageCertValue As Integer = 0
    Public Shared cbGoodMoralValue As Integer = 0
    Public Shared cbNCAEValue As Integer = 0
    Public Shared cbAlsValue As Integer = 0
    Public Shared cbHDValue As Integer = 0
    Public Shared cbTORValue As Integer = 0
    Public Shared cbOTRValue As Integer = 0

    Public Shared cbTransferredValue As Integer = 0

    Public Shared address_prov_code As String = "0"
    Public Shared address_citymun_code As String = "0"
    Public Shared address_brgy_code As String = "0"

    Public Shared address_street As String = ""

    Public Shared address_prov As String = ""
    Public Shared address_citymun As String = ""
    Public Shared address_brgy As String = ""

    Public Shared mother_fname As String = ""
    Public Shared mother_mname As String = ""
    Public Shared mother_lname As String = ""

    Public Shared father_fname As String = ""
    Public Shared father_mname As String = ""
    Public Shared father_lname As String = ""

    Public Shared guardian_fname As String = ""
    Public Shared guardian_mname As String = ""
    Public Shared guardian_lname As String = ""
#Region "Drag Form"

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown  ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove  ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp   ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

#End Region

    Private Sub toNormalForm()
        SearchPanel.Visible = False
        dgCourseList.Rows.Clear()
        dgReligionList.Rows.Clear()
        dgDisabilityList.Rows.Clear()
        dgSchoolList.Rows.Clear()
        txtSearch.Text = String.Empty
    End Sub
    Private Sub frmStudent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            toNormalForm()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel?", vbYesNo + vbQuestion) = vbYes Then
            If btnSave.Visible = True Then
                ResetControls(Me)
            ElseIf btnUpdate.Visible = True Then
                Me.Close()
            End If
        Else
        End If
    End Sub

    Private Sub frmStudent_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ResetControls(Me)

        OldStudentID = ""
        NewStudentID = ""

        StudentIDLastNumber = ""
        StudentID = ""
        StudentStatus = ""

        PWDID = 0
        RelegionID = 0
        CourseID = 0
        PeriodID = 0
        ScholarshipID = 0
        GraduatedPeriodID = 0

        PrimaryID = 0
        JuniorHighID = 0
        SeniorHighID = 0
        CollegeID = 0
        MastersID = 0
        DoctorateID = 0
        LastSchoolID = 0
        TransferredSchoolID = 0

        cbHScardValue = 0
        cbF137Value = 0
        cbBirthCertValue = 0
        cbMarriageCertValue = 0
        cbGoodMoralValue = 0
        cbNCAEValue = 0
        cbAlsValue = 0
        cbHDValue = 0
        cbTORValue = 0
        cbOTRValue = 0

        cbTransferredValue = 0
        currentSlideIndex = 0
    End Sub

    'Private Sub SetSlide(index As Integer)
    '    For Each control As Control In Panels.Controls
    '        If TypeOf control Is Panel Then
    '            Dim PaneltoHide As Panel = DirectCast(control, Panel)
    '            PaneltoHide.Hide()
    '        End If
    '    Next
    '    slidePanels(index).Show()
    'End Sub


    Private Sub photoSignatureSwitch_Click(sender As Object, e As EventArgs) Handles photoSignatureSwitch.Click
        If photoSignature.Text = "Photo" Then
            photoSignature.Text = "Signature"
            studentSignature.Visible = True
            studentPhoto.Visible = False
        ElseIf photoSignature.Text = "Signature" Then
            photoSignature.Text = "Photo"
            studentSignature.Visible = False
            studentPhoto.Visible = True
        End If
    End Sub

    Private Sub dtBirthdate_ValueChanged(sender As Object, e As EventArgs) Handles dtBirthdate.ValueChanged
        txtAge.Text = "Age: " & GetCurrentAge(dtBirthdate.Value) & " Years Old"
    End Sub

    Private Sub btnSearchReligion_Click(sender As Object, e As EventArgs) Handles btnSearchReligion.Click, btnSearchDisability.Click
        frmTitle.Text = "Search Religion"
        SearchPanel.Visible = True
        LibraryStudentReligionList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgReligionList)
    End Sub

    Private Sub btnSearchDisability_Click(sender As Object, e As EventArgs) Handles btnSearchDisability.Click
        frmTitle.Text = "Search Disability"
        SearchPanel.Visible = True
        LibraryStudentDisabilityList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgDisabilityList)
    End Sub
    Private Sub btnSearchScholarship_Click(sender As Object, e As EventArgs) Handles btnSearchScholarship.Click
        frmTitle.Text = "Search Scholarship"
        SearchPanel.Visible = True
        LibraryStudentScholarshipList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgScholarshipList)
    End Sub

    Private Sub btnSchoolTransferred_Click(sender As Object, e As EventArgs) Handles btnSchoolTransferred.Click
        frmTitle.Text = "Search Transferred School"
        SearchPanel.Visible = True
        LibraryStudentSchoolList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgSchoolList)
    End Sub

    Private Sub btnLastSchool_Click(sender As Object, e As EventArgs) Handles btnLastSchool.Click
        frmTitle.Text = "Search Last School Attended"
        SearchPanel.Visible = True
        LibraryStudentSchoolList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgSchoolList)
    End Sub

    Private Sub btnPrimary_Click(sender As Object, e As EventArgs) Handles btnPrimary.Click
        frmTitle.Text = "Search Primary School"
        SearchPanel.Visible = True
        LibraryStudentSchoolList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgSchoolList)
    End Sub

    Private Sub btnJuniorHigh_Click(sender As Object, e As EventArgs) Handles btnJuniorHigh.Click
        frmTitle.Text = "Search Junior High School"
        SearchPanel.Visible = True
        LibraryStudentSchoolList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgSchoolList)
    End Sub

    Private Sub btnSeniorHigh_Click(sender As Object, e As EventArgs) Handles btnSeniorHigh.Click
        frmTitle.Text = "Search Senior High School"
        SearchPanel.Visible = True
        LibraryStudentSchoolList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgSchoolList)
    End Sub

    Private Sub btnCollege_Click(sender As Object, e As EventArgs) Handles btnCollege.Click
        frmTitle.Text = "Search College School"
        SearchPanel.Visible = True
        LibraryStudentSchoolList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgSchoolList)
    End Sub

    Private Sub btnMasters_Click(sender As Object, e As EventArgs) Handles btnMasters.Click
        frmTitle.Text = "Search Masters School"
        SearchPanel.Visible = True
        LibraryStudentSchoolList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgSchoolList)
    End Sub

    Private Sub btnDoctorate_Click(sender As Object, e As EventArgs) Handles btnDoctorate.Click
        frmTitle.Text = "Search Doctorate School"
        SearchPanel.Visible = True
        LibraryStudentSchoolList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgSchoolList)
    End Sub

    Private Sub btnSearchCourse_Click(sender As Object, e As EventArgs) Handles btnSearchCourse.Click
        frmTitle.Text = "Search Course"
        SearchPanel.Visible = True
        LibraryStudentCourseList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgCourseList)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Select Case frmTitle.Text
            Case "Search Religion"
                RelegionID = dgReligionList.CurrentRow.Cells(1).Value
                cbReligion.Text = dgReligionList.CurrentRow.Cells(2).Value
                toNormalForm()
            Case "Search Disability"
                PWDID = dgDisabilityList.CurrentRow.Cells(1).Value
                cbDisability.Text = dgDisabilityList.CurrentRow.Cells(2).Value
                toNormalForm()
            Case "Search Course"
                CourseID = dgCourseList.CurrentRow.Cells(1).Value
                cbCourse.Text = dgReligionList.CurrentRow.Cells(2).Value
                txtCourse.Text = dgReligionList.CurrentRow.Cells(3).Value
                toNormalForm()
            Case "Search Scholarship"
                ScholarshipID = dgScholarshipList.CurrentRow.Cells(1).Value
                cbScholarship.Text = dgDisabilityList.CurrentRow.Cells(2).Value
                toNormalForm()
            Case "Search Last School Attended"
                LastSchoolID = dgSchoolList.CurrentRow.Cells(1).Value
                txtLastSchool.Text = dgDisabilityList.CurrentRow.Cells(3).Value
                toNormalForm()
            Case "Search Transferred School"
                TransferredSchoolID = dgSchoolList.CurrentRow.Cells(1).Value
                txtSchoolTransferred.Text = dgDisabilityList.CurrentRow.Cells(3).Value
                toNormalForm()
            Case "Search Primary School"
                PrimaryID = dgSchoolList.CurrentRow.Cells(1).Value
                txtPrimary.Text = dgDisabilityList.CurrentRow.Cells(3).Value
                toNormalForm()
            Case "Search Junior High School"
                JuniorHighID = dgSchoolList.CurrentRow.Cells(1).Value
                txtJuniorHigh.Text = dgDisabilityList.CurrentRow.Cells(3).Value
                toNormalForm()
            Case "Search Senior High School"
                SeniorHighID = dgSchoolList.CurrentRow.Cells(1).Value
                txtSeniorHigh.Text = dgSchoolList.CurrentRow.Cells(3).Value
                toNormalForm()
            Case "Search College School"
                CollegeID = dgSchoolList.CurrentRow.Cells(1).Value
                txtCollege.Text = dgDisabilityList.CurrentRow.Cells(3).Value
                toNormalForm()
            Case "Search Masters School"
                MastersID = dgSchoolList.CurrentRow.Cells(1).Value
                txtMasters.Text = dgDisabilityList.CurrentRow.Cells(3).Value
                toNormalForm()
            Case "Search Doctorate School"
                DoctorateID = dgSchoolList.CurrentRow.Cells(1).Value
                txtDoctorate.Text = dgDisabilityList.CurrentRow.Cells(3).Value
                toNormalForm()
        End Select
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        StudentModuleSearch()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        StudentModuleAdd()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If PWDID = 0 Or cbDisability.SelectedValue Is Nothing Then
                MsgBox("Invalid Disability.", vbCritical)
                Return
            End If
            If RelegionID = 0 Or cbReligion.SelectedValue Is Nothing Then
                MsgBox("Invalid Religion.", vbCritical)
                Return
            End If
            If ScholarshipID = 0 Or cbScholarship.SelectedValue Is Nothing Then
                MsgBox("Invalid Scholarship.", vbCritical)
                Return
            End If
            If CourseID = 0 Or cbCourse.SelectedValue Is Nothing Then
                MsgBox("Invalid Course.", vbCritical)
                Return
            End If
            If IS_EMPTY(txtFname) = True Then Return
            If IS_EMPTY(txtMname) = True Then Return
            If IS_EMPTY(txtLname) = True Then Return
            If IS_EMPTY(txtLRN) = True Then Return
            If IS_EMPTY(txtContact) = True Then Return
            If IS_EMPTY(txtAddress) = True Then Return
            If IS_EMPTY(txtMname) = True Then Return
            If IS_EMPTY(txtFname) = True Then Return
            If IS_EMPTY(txtGName) = True Then Return
            If IS_EMPTY(txtGAddress) = True Then Return
            If IS_EMPTY(txtGContact) = True Then Return
            If IS_EMPTY(txtPrimary) = True Then Return
            If CHECK_EXISTING("SELECT s_id_no FROM tbl_student WHERE s_fn = '" & txtFname.Text.Trim & "' and s_ln = '" & txtLname.Text.Trim & "' and s_mn = '" & txtMname.Text.Trim & "' and s_ext = '" & txtSuffix.Text.Trim & "'") Then Return
            If MsgBox("Are you sure you want to save this record?", vbYesNo + vbQuestion) = vbYes Then
                If studentPhoto.Image Is Nothing Then
                    studentPhoto.Image = studentDummypicture.Image
                End If
                If studentSignature.Image Is Nothing Then
                    studentSignature.Image = studentDummysign.Image
                End If
                InsertStudent()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "")
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If PWDID = 0 Or cbDisability.SelectedValue Is Nothing Then
                MsgBox("Invalid Disability.", vbCritical)
                Return
            End If
            If RelegionID = 0 Or cbReligion.SelectedValue Is Nothing Then
                MsgBox("Invalid Religion.", vbCritical)
                Return
            End If
            If ScholarshipID = 0 Or cbScholarship.SelectedValue Is Nothing Then
                MsgBox("Invalid Scholarship.", vbCritical)
                Return
            End If
            If CourseID = 0 Or cbCourse.SelectedValue Is Nothing Then
                MsgBox("Invalid Course.", vbCritical)
                Return
            End If
            If IS_EMPTY(txtFname) = True Then Return
            If IS_EMPTY(txtMname) = True Then Return
            If IS_EMPTY(txtLname) = True Then Return
            If IS_EMPTY(txtLRN) = True Then Return
            If IS_EMPTY(txtContact) = True Then Return
            If IS_EMPTY(txtAddress) = True Then Return
            If IS_EMPTY(txtFname) = True Then Return
            If IS_EMPTY(txtGName) = True Then Return
            If IS_EMPTY(txtGName) = True Then Return
            If IS_EMPTY(txtGAddress) = True Then Return
            If IS_EMPTY(txtGContact) = True Then Return
            If IS_EMPTY(txtPrimary) = True Then Return
            If CHECK_EXISTING("SELECT s_id_no FROM tbl_student WHERE s_fn = '" & txtFname.Text.Trim & "' and s_ln = '" & txtLname.Text.Trim & "' and s_mn = '" & txtMname.Text.Trim & "' and s_ext = '" & txtSuffix.Text.Trim & "' and s_id_no NOT IN (" & OldStudentID & ")") Then Return
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                If studentPhoto.Image Is Nothing Then
                    studentPhoto.Image = studentDummypicture.Image
                End If
                If studentSignature.Image Is Nothing Then
                    studentSignature.Image = studentDummysign.Image
                End If
                UpdateStudent()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "")
        End Try
    End Sub

    Private Sub cbTransferMode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbTransferMode.KeyPress
        e.Handled = True
    End Sub

    Private Sub cbTransferred_Click(sender As Object, e As EventArgs) Handles cbTransferred.Click
        If cbTransferred.Checked = True Then
            transferredPanel.Visible = True
        Else
            transferredPanel.Visible = False
        End If
    End Sub

    Private Sub cbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCourse.SelectedIndexChanged
        txtCourse.Text = ComboCourseName(cbCourse)
    End Sub

    Private Sub btnAddress_Click(sender As Object, e As EventArgs) Handles btnAddress.Click
        With frmStudentAddress
            fillCombo("SELECT `provDesc`, `provCode` FROM `refprovince` order by provDesc asc", .cbProv, "refprovince", "provDesc", "provCode")
            .cbProv.SelectedIndex = 0
            .txtStreet.Text = address_street
            .cbProv.Text = address_prov
            .cbCity.Text = address_citymun
            .cbBrgy.Text = address_brgy
            .ShowDialog()
        End With
    End Sub

    Private Sub btnFather_Click(sender As Object, e As EventArgs) Handles btnFather.Click
        frmStudentFamily.familyName.Text = "Father's Name"
        frmStudentFamily.txtFName.Text = father_fname
        frmStudentFamily.txtMName.Text = father_mname
        frmStudentFamily.txtLName.Text = father_lname
        frmStudentFamily.ShowDialog()
    End Sub

    Private Sub btnMother_Click(sender As Object, e As EventArgs) Handles btnMother.Click
        frmStudentFamily.familyName.Text = "Mother's Maiden Name"
        frmStudentFamily.txtFName.Text = mother_fname
        frmStudentFamily.txtMName.Text = mother_mname
        frmStudentFamily.txtLName.Text = mother_lname
        frmStudentFamily.ShowDialog()
    End Sub

    Private Sub cbCourseStatus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbCourseStatus.KeyPress
        e.Handled = True
    End Sub

    Private Sub cbMiddleName_CheckedChanged(sender As Object, e As EventArgs) Handles cbMiddleName.CheckedChanged
        If cbMiddleName.Checked = True Then
            txtMname.Enabled = False
        Else
            txtMname.Enabled = True
        End If
    End Sub

    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        frmCamera.Show()
        frmCamera.lblCameraSubject.Text = "Student"
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        frmCamera.Show()
        frmCamera.lblCameraSubject.Text = "Student"
    End Sub
End Class