Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmMain

    Public acadID As Integer

    Private hoverPanel As FlowLayoutPanel = Nothing

    Dim GraphSelectedCourse As String = ""
    Dim GraphSelectedYear As String = ""
#Region "Drag Form"

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles systemSign.MouseDown  ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles systemSign.MouseMove  ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles systemSign.MouseUp  ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

#End Region


    Public Sub loadDashboard()
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT period_id as PERIOD from tbl_period where period_status = 'Active'", cn)
        activeAcademicYear = CInt(cm.ExecuteScalar)
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT CONCAT(period_name) as PERIOD from tbl_period where period_status = 'Active'", cn)
        lblAcadYear.Text = cm.ExecuteScalar
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT CONCAT(period_semester) from tbl_period where period_status = 'Active'", cn)
        lblSemester.Text = cm.ExecuteScalar.ToString.ToUpper
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT FORMAT(COUNT(DISTINCT sg_student_id),0) from tbl_students_grades where sg_period_id = " & activeAcademicYear & " and sg_grade_status = 'Enrolled'", cn)
        btnEnrolledBreakdown2.Text = Format(CDbl(cm.ExecuteScalar), "#,##0")
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT COUNT(DISTINCT sg_course_id) as courses from tbl_students_grades where sg_period_id = '" & activeAcademicYear & "' and sg_grade_status = 'Enrolled'", cn)
        lblCourses.Text = cm.ExecuteScalar
        cn.Close()

        cn.Open()
        cm = New MySqlCommand("Select ifnull(sum(csh_total_amount),0) from tbl_cashiering where csh_date = CURDATE() and csh_ornumber REGEXP '^[0-9]'", cn)
        lblTotalCollected.Text = Format(CDbl(cm.ExecuteScalar), "#,##0.00")
        cn.Close()
    End Sub



    Public Sub OpenForm(frm As Form, ByVal title As String)
        formPanels.Visible = True
        formTitle.Text = title
        txtSearch.Text = String.Empty
        If frm.IsHandleCreated Then
            frm.BringToFront()
        Else
            frm.TopLevel = False
            formPanel.Controls.Add(frm)
            frm.BringToFront()
            frm.Show()
        End If
    End Sub

    Public Sub HideAllFormsInPanelExcept(formToShow As Form)
        For Each control As Control In formPanel.Controls
            If TypeOf control Is Form Then
                Dim formToHide As Form = DirectCast(control, Form)
                formToHide.Hide()
            End If
        Next
        formToShow.Show()
        If formTitle.Text = "Supply Items Stock Level" Then
            Label3.Visible = False
            btnAdd.Visible = False
        Else
            Label3.Visible = True
            btnAdd.Visible = True
        End If
    End Sub


    Sub UserPhoto()
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select ua_photo from tbl_user_account where ua_id = " & str_userid & "", cn)
            dr = cm.ExecuteReader
            While dr.Read
                Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                Dim array(CInt(len)) As Byte
                dr.GetBytes(0, 0, array, 0, CInt(len))
                Dim ms As New MemoryStream(array)
                Dim bitmap As New System.Drawing.Bitmap(ms)
                User_Photo.Image = bitmap
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            User_Photo.Image = dummypic.Image
        End Try
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetFormIcon(Me)

        ' Get the working area of the primary screen (excluding taskbar)
        Dim workingArea As Rectangle = Screen.PrimaryScreen.WorkingArea

        ' Set the form's size and location to fit the working area
        Me.Size = New Size(workingArea.Width, workingArea.Height)
        Me.Location = New Point(workingArea.X, workingArea.Y)

        lblUser.Text = str_user
        lblRole.Text = str_role
        User_Name.Text = str_name
        UserPhoto()

        'lblUsername.Text = str_name
        appVersion(applicationVersion)
        Date_Today()
        Timer1.Start()
        ApplyHoverEffectToControls(Me)
        loadDashboard()
    End Sub

    Private Sub HideSubbuttons()
        'Panels
        enrollmentPanel.Visible = False
        libraryPanel.Visible = False
        cashieringPanel.Visible = False
        reportsPanel.Visible = False
        registrarPanel.Visible = False
        databasePanel.Visible = False
        supplyPanel.Visible = False
        'Buttons
        btnEnrollment.Visible = False
        btnLibrary.Visible = False
        btnCashiering.Visible = False
        btnReports.Visible = False
        btnRegistrar.Visible = False
        btnDatabase.Visible = False
        btnSupply.Visible = False
    End Sub

    Private Sub ShowMainButtons()
        'Buttons
        btnEnrollment.Visible = True
        btnLibrary.Visible = True
        btnCashiering.Visible = True
        btnReports.Visible = True
        btnRegistrar.Visible = True
        btnDatabase.Visible = True
        btnSupply.Visible = True
    End Sub

    Private Sub ShowSubButtons(submenuPanel As Panel, btn As Button)
        If submenuPanel.Visible = False Then
            HideSubbuttons()
            submenuPanel.Visible = True
            btn.Visible = True
        Else
            submenuPanel.Visible = False
            ShowMainButtons()
        End If
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        loadDashboard()
        formPanels.Visible = False
        HideSubbuttons()
        ShowMainButtons()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Date_Time()
        Timer1.Stop()
    End Sub

    Private Sub btnEnrollment_Click(sender As Object, e As EventArgs) Handles btnEnrollment.Click
        ShowSubButtons(enrollmentPanel, btnEnrollment)
    End Sub

    Private Sub btnLibrary_Click(sender As Object, e As EventArgs) Handles btnLibrary.Click
        ShowSubButtons(libraryPanel, btnLibrary)
    End Sub

    Private Sub btnStudentList_Click(sender As Object, e As EventArgs) Handles btnStudentList.Click
        If Application.OpenForms.OfType(Of frmStudentList)().Any() Then
        Else
            LibraryStudentList()
        End If
        OpenForm(frmStudentList, "List Of Students")
        HideAllFormsInPanelExcept(frmStudentList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnCourseList_Click(sender As Object, e As EventArgs) Handles btnCourseList.Click
        If Application.OpenForms.OfType(Of frmCourseList)().Any() Then
        Else
            LibraryCourseList()
        End If
        OpenForm(frmCourseList, If(systemModule.Text = "College Module", "List Of Courses", "List Of Grade/Strands"))
        HideAllFormsInPanelExcept(frmCourseList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnAcamedicYearList_Click(sender As Object, e As EventArgs) Handles btnAcamedicYearList.Click
        If Application.OpenForms.OfType(Of frmAcademicYearList)().Any() Then
        Else
            LibraryAcadList()
        End If
        OpenForm(frmAcademicYearList, "List Of Academic Years")
        HideAllFormsInPanelExcept(frmAcademicYearList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnSubjectList_Click(sender As Object, e As EventArgs) Handles btnSubjectList.Click
        If Application.OpenForms.OfType(Of frmSubjectList)().Any() Then
        Else
            LibrarySubjectList()
        End If
        OpenForm(frmSubjectList, "List Of Subjects")
        HideAllFormsInPanelExcept(frmSubjectList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnClassSectionList_Click(sender As Object, e As EventArgs) Handles btnClassSectionList.Click
        If Application.OpenForms.OfType(Of frmSectionList)().Any() Then
        Else
            LibrarySectionList()
        End If
        OpenForm(frmSectionList, "List Of Class Sections")
        HideAllFormsInPanelExcept(frmSectionList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnDaySchedList_Click(sender As Object, e As EventArgs) Handles btnDaySchedList.Click
        If Application.OpenForms.OfType(Of frmDaySchedList)().Any() Then
        Else
            LibraryDaySchedList()
        End If
        OpenForm(frmDaySchedList, "List Of Day Schedules")
        HideAllFormsInPanelExcept(frmDaySchedList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnRoomList_Click(sender As Object, e As EventArgs) Handles btnRoomList.Click
        If Application.OpenForms.OfType(Of frmRoomList)().Any() Then
        Else
            LibraryRoomList()
        End If
        OpenForm(frmRoomList, "List Of Rooms")
        HideAllFormsInPanelExcept(frmRoomList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnSchoolList_Click(sender As Object, e As EventArgs) Handles btnSchoolList.Click
        If Application.OpenForms.OfType(Of frmSchoolList)().Any() Then
        Else
            LibrarySchoolList()
        End If
        OpenForm(frmSchoolList, "List Of Schools")
        HideAllFormsInPanelExcept(frmSchoolList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnEmployeeList_Click(sender As Object, e As EventArgs) Handles btnEmployeeList.Click
        If Application.OpenForms.OfType(Of frmEmployeeList)().Any() Then
        Else
            LibraryEmployeeList()
        End If
        OpenForm(frmEmployeeList, "List Of Employees")
        HideAllFormsInPanelExcept(frmEmployeeList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnClassSchedList_Click(sender As Object, e As EventArgs) Handles btnClassSchedList.Click
        If Application.OpenForms.OfType(Of frmClassSchedList)().Any() Then
        Else
            LibraryClassSchedList()
        End If
        OpenForm(frmClassSchedList, "List Of Class Schedules")
        HideAllFormsInPanelExcept(frmClassSchedList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

#Region "List Of Class Schedules"
    Private Sub ClassAcad_MouseDown(sender As Object, e As MouseEventArgs) Handles ComboClick.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim position As Point = Control.MousePosition
            ComboBoxMenu.Show(position)
        End If
    End Sub
    Private Sub txtSearch_MouseHover(sender As Object, e As EventArgs) Handles txtSearch.MouseHover
        SelectionTitle.BackColor = Color.FromName("Control")
    End Sub

    Private Sub txtSearch_MouseLeave(sender As Object, e As EventArgs) Handles txtSearch.MouseLeave
        SelectionTitle.BackColor = Color.White
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If formTitle.Text = "Cashiering" Then
            If frmCashiering.SearchPanel.Visible = True Then
                frmCashiering.SearchPanel.Visible = False
            Else
                If MsgBox("Are you sure you want to exit cashiering?", vbYesNo + vbQuestion) = vbYes Then
                    formPanels.Visible = False
                Else
                End If
            End If
        ElseIf formTitle.Text = "Pre-Cashiering" Then
            If frmCashiering.SearchPanel.Visible = True Then
                frmCashiering.SearchPanel.Visible = False
            Else
                If MsgBox("Are you sure you want to exit pre-cashiering?", vbYesNo + vbQuestion) = vbYes Then
                    formPanels.Visible = False
                Else
                End If
            End If
        ElseIf formTitle.Text.Contains("Generate") Then
            If frmReports.SearchPanel.Visible = True Then
                frmReports.SearchPanel.Visible = False
                frmReports.ReportsControlsPanel.Visible = True
                frmReports.Panel3.Visible = True
                frmReports.frmTitle.Text = "Search"
                frmReports.txtSearch.Text = String.Empty
                frmReports.dgStudentList.Rows.Clear()
            ElseIf frmReports.ReportViewer.ReportSource IsNot Nothing AndAlso frmReports.btnNext.Visible = False Then
                frmReports.PrevBtn()
            Else
                formPanels.Visible = False
            End If
        ElseIf formTitle.Text = "Curriculum Setup" Then
            If frmCurriculumSetup.SearchPanel.Visible = True Then
                frmCurriculumSetup.SearchPanel.Visible = False
            Else
                Try
                    Dim pictureBoxImage As Bitmap = CType(frmCurriculumSetup.pic1.Image, Bitmap)
                    Dim dataGridViewImage As Bitmap = CType(frmCurriculumSetup.dgCurriculumSubjects.CurrentRow.Cells(13).Value, Bitmap)
                    Dim dataGridViewImage2 As Bitmap = CType(frmCurriculumSetup.dgCurriculumSubjects.CurrentRow.Cells(14).Value, Bitmap)
                    Dim found As Boolean = False

                    For Each row As DataGridViewRow In frmCurriculumSetup.dgCurriculumSubjects.Rows
                        If CompareImages(pictureBoxImage, dataGridViewImage) Or CompareImages(pictureBoxImage, dataGridViewImage2) Then
                            found = True
                        Else
                            found = False
                        End If
                    Next
                    If found = True Then
                        If MsgBox("There are still unsave changes. Are you sure you want to exit?", vbYesNo + vbQuestion) = vbYes Then
                            frmCurriculumSetup.Dispose()
                            OpenForm(frmCurriculumList, "List Of Curriculum")
                            HideAllFormsInPanelExcept(frmCurriculumList)
                            controlsPanel.Visible = True
                        Else

                        End If
                    Else
                        frmCurriculumSetup.Dispose()
                        OpenForm(frmCurriculumList, "List Of Curriculum")
                        HideAllFormsInPanelExcept(frmCurriculumList)
                        controlsPanel.Visible = True
                    End If

                Catch ex As Exception
                    frmCurriculumSetup.Dispose()
                    OpenForm(frmCurriculumList, "List Of Curriculum")
                    HideAllFormsInPanelExcept(frmCurriculumList)
                    controlsPanel.Visible = True
                End Try
            End If
        ElseIf formTitle.Text = "Student Evaluation" Then
            If frmStudentEvaluation.SearchPanel.Visible = True Then
                frmStudentEvaluation.SearchPanel.Visible = False
            Else
                If MsgBox("Are you sure you want to exit student evaluation?", vbYesNo + vbQuestion) = vbYes Then
                    formPanels.Visible = False
                Else
                End If
            End If
        ElseIf formTitle.Text = "Edit Student Grades" Then
            If frmStudentGradeEditor.SearchPanel.Visible = True Then
                frmStudentGradeEditor.SearchPanel.Visible = False
            Else
                If MsgBox("Are you sure you want to exit editing student grade?", vbYesNo + vbQuestion) = vbYes Then
                    formPanels.Visible = False
                    frmStudentGradeEditor.dgStudentGrades.Rows.Clear()
                    frmStudentGradeEditor.txtRemarks.Text = String.Empty
                    frmStudentGradeEditor.txtSchool.Text = String.Empty
                    frmStudentGradeEditor.txtStudent.Text = String.Empty
                    frmStudentGradeEditor.txtYearLevelCourse.Text = String.Empty
                    frmStudentGradeEditor.GradeStatus.Text = "Status"
                    frmStudentGradeEditor.GradeVisibility.Text = "Not Visible"
                    frmStudentGradeEditor.totalSubjects.Text = "0"
                    frmStudentGradeEditor.totalUnits.Text = "0"
                    frmStudentGradeEditor.gradeRemark.Text = "*Selected Cell - Grade uploaded/edited by:"
                Else
                End If
            End If
        ElseIf formTitle.Text = "Edit Class Grades" Then
            If frmClassGradeEditor.SearchPanel.Visible = True Then
                frmClassGradeEditor.SearchPanel.Visible = False
            Else
                If MsgBox("Are you sure you want to exit student evaluation?", vbYesNo + vbQuestion) = vbYes Then
                    formPanels.Visible = False
                    frmClassGradeEditor.dgStudentList.Rows.Clear()
                    frmClassGradeEditor.txtClass.Text = String.Empty
                Else
                End If
            End If
        ElseIf formTitle.Text = "Credit Student Grades" Then
            If frmCreditGrade.SearchPanel.Visible = True Then
                frmCreditGrade.SearchPanel.Visible = False
            Else
                If MsgBox("Are you sure you want to exit student evaluation?", vbYesNo + vbQuestion) = vbYes Then
                    formPanels.Visible = False
                Else
                End If
            End If
        ElseIf formTitle.Text = "Enroll Class Schedule" Or formTitle.Text = "Update Class Schedule" Then
            If frmEnrollStudent.SearchPanel.Visible = True Then
                frmEnrollStudent.SearchPanel.Visible = False
            ElseIf frmEnrollStudent.ReportPanel.Visible = True Then
                frmEnrollStudent.ReportPanel.Visible = False
            Else
                If MsgBox("Are you sure you want to exit?", vbYesNo + vbQuestion) = vbYes Then
                    formPanels.Visible = False
                Else
                End If
            End If
        ElseIf formTitle.Text = "Student Account Adjustment" Then
            If frmAdjustments.SearchPanel.Visible = True Then
                frmAdjustments.SearchPanel.Visible = False
            Else
                If MsgBox("Are you sure you want to exit?", vbYesNo + vbQuestion) = vbYes Then
                    formPanels.Visible = False
                Else
                End If
            End If
        ElseIf formTitle.Text = "Course Assessment Setup" Then
            If frmAssessmentSetup.SearchPanel.Visible = True Then
                frmAssessmentSetup.SearchPanel.Visible = False
            Else
                If MsgBox("Are you sure you want to exit?", vbYesNo + vbQuestion) = vbYes Then
                    frmAssessmentSetup.Close()
                    OpenForm(frmAssessment, "Course Assessment")
                    HideAllFormsInPanelExcept(frmAssessment)
                    controlsPanel.Visible = False
                Else
                End If
            End If
        ElseIf formTitle.Text = "Supply Item Ledger" Then
            If frmSupplyItemLedger.SearchPanel.Visible = True Then
                frmSupplyItemLedger.SearchPanel.Visible = False
            Else
                formPanels.Visible = False
            End If
        Else
            formPanels.Visible = False
        End If
    End Sub

    Private Sub cmbAcad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAcad.SelectedIndexChanged
        academicYearID()
        SelectionTitle.Text = cmbAcad.Text
        MainSearch()
    End Sub
#End Region

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        MainSearch()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        MainAdd()

        Select Case formTitle.Text
            Case "List Of Students"
                With frmStudentInfo
                    .studentPhoto.Image = .studentDummypicture.Image
                    .studentSignature.Image = .studentDummysign.Image
                End With
            Case "List Of Employees"
                With frmEmployee
                    .empPhoto.Image = .Dummypicture.Image
                    .empSignature.Image = .Dummysign.Image
                End With
            Case "List Of Courses"
                With frmCourse
                    .Label1.Text = "Course Module"
                End With
            Case "List Of Grade/Strands"
                With frmCourse
                    .Label1.Text = "Strand/Grade Module"
                End With
        End Select
    End Sub

    Private Sub formTitle_TextChanged(sender As Object, e As EventArgs) Handles formTitle.TextChanged
        If formTitle.Text = "List Of Class Schedules" Then
            SelectionTitle.Visible = True
            ComboClick.Text = "  ▼  "
            ComboClick.Enabled = True
            classSchedAcademicYear()
            cmbAcad.Visible = True
            cmbSupplyType.Visible = False
            cmbAcad.SelectedIndex = 0
            academicYearID()
        ElseIf formTitle.Text = "List Of Supply Items" Then
            SelectionTitle.Visible = True
            ComboClick.Text = "  ▼  "
            ComboClick.Enabled = True
            frmSupplyItems.SupplyItemList()
            cmbAcad.Visible = False
            cmbSupplyType.Visible = True
            cmbSupplyType.SelectedIndex = 0
        ElseIf formTitle.Text = "Supply Items Stock Level" Then
            SelectionTitle.Visible = True
            ComboClick.Text = "  ▼  "
            ComboClick.Enabled = True
            frmSupplyStockLevel.SupplyItemStockLevel()
            cmbAcad.Visible = False
            cmbSupplyType.Visible = True
            cmbSupplyType.SelectedIndex = 0
        Else
            SelectionTitle.Visible = False
            ComboClick.Text = "     "
            ComboClick.Enabled = False
            PanelCredReq.Visible = False
            frmReports.dgStudentView.Visible = False
            frmReports.rbViewClass.Checked = False
        End If
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MsgBox("Are you sure you want To logout?", vbYesNo + vbQuestion) = vbYes Then
            UserActivity("Logged-out.", "LOGOUT")
            Me.Close()
            frmLogin.Show()
        Else
        End If
    End Sub

    Private Sub btnUploadPortal_Click(sender As Object, e As EventArgs) Handles btnUploadPortal.Click
        fillCombo("Select CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_class_schedule t1 JOIN tbl_period t2 ON t1.csperiod_id = t2.period_id group by t1.csperiod_id order by `period_name` desc, `period_status` ASC, `period_semester` desc", frmGradesPortalUpload.cbAcademicYear, "tbl_period", "PERIOD", "period_id")
        'OpenForm(frmGradesPortalUpload, "Upload Grades from Portal")
        'HideAllFormsInPanelExcept(frmGradesPortalUpload)
        'controlsPanel.Visible = False
        frmGradesPortalUpload.Show()
    End Sub

    Private Sub btnCashieringPayment_Click(sender As Object, e As EventArgs) Handles btnCashieringPayment.Click
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("select * from tbl_cash_denomination where cd_cashier_id = " & str_userid & " and cd_date = CURDATE()", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            MsgBox("Cashiering is closed for today for this user as the cash has been counted and denominated.", vbExclamation)
        Else
            OpenForm(frmCashiering, "Cashiering")
            HideAllFormsInPanelExcept(frmCashiering)
            controlsPanel.Visible = False
            frmCashiering.Panel8.Visible = True
            frmCashiering.Panel9.Visible = True
            frmCashiering.cbNDP.Visible = False
            frmCashiering.viewSOA.Visible = True
            frmCashiering.txtAcadBalance.Visible = True
            frmCashiering.Label17.Visible = True
            frmCashiering.ClearAll()
            frmCashiering.btnSearchStudent.Select()
        End If
        dr.Close()
        cn.Close()
    End Sub

    Private Sub btnCashiering_Click(sender As Object, e As EventArgs) Handles btnCashiering.Click
        ShowSubButtons(cashieringPanel, btnCashiering)
    End Sub

    Private Sub btnPreCashieringPayment_Click(sender As Object, e As EventArgs) Handles btnPreCashieringPayment.Click
        OpenForm(frmCashiering, "Pre-Cashiering")
        HideAllFormsInPanelExcept(frmCashiering)
        controlsPanel.Visible = False
        frmCashiering.Panel8.Visible = False
        frmCashiering.Panel9.Visible = False
        frmCashiering.cbNDP.Visible = True
        frmCashiering.viewSOA.Visible = False
        frmCashiering.txtAcadBalance.Visible = False
        frmCashiering.Label17.Visible = False
        frmCashiering.ClearAll()
        fillCombo("Select CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_period WHERE period_enrollment_status = 'OPEN' order by `period_name` desc, `period_status` asc, `period_semester` desc", frmCashiering.cbAcademicYear, "CashieringPeriodList", "PERIOD", "period_id")

        frmCashiering.btnSearchStudent.Select()
    End Sub

    Private Sub btnEnroll_Click(sender As Object, e As EventArgs) Handles btnEnroll.Click
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT * FROM tbl_period WHERE period_enrollment_status = 'OPEN'", cn)
        Dim sdr0 As MySqlDataReader = cm.ExecuteReader()
        If (sdr0.Read() = True) Then
            If str_role = "Administrator" Or str_role = "Registrar" Then
                frmEnrollStudent.updateSchedAdmin.Visible = False
                frmEnrollStudent.btnOldSched.Visible = False
                OpenForm(frmEnrollStudent, "Enroll Class Schedule")
                HideAllFormsInPanelExcept(frmEnrollStudent)
                controlsPanel.Visible = False
                fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmEnrollStudent.cbAcad, "tbl_period", "PERIOD", "period_id")
            Else
                MsgBox("There are no semester OPEN for enrollment. Please contact registrar office.", vbExclamation)
            End If

        Else
            frmEnrollStudent.updateSchedAdmin.Visible = False
            frmEnrollStudent.btnOldSched.Visible = False
            OpenForm(frmEnrollStudent, "Enroll Class Schedule")
            HideAllFormsInPanelExcept(frmEnrollStudent)
            controlsPanel.Visible = False
            fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmEnrollStudent.cbAcad, "tbl_period", "PERIOD", "period_id")
        End If
        cn.Close()
    End Sub

    Private Sub btnAddDrop_Click(sender As Object, e As EventArgs) Handles btnAddDrop.Click

        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT * FROM tbl_period WHERE CURDATE() >= period_enrollment_enddate AND period_status = 'Active'", cn)
        Dim sdr0 As MySqlDataReader = cm.ExecuteReader()
        If (sdr0.Read() = True) Then
            If str_role = "Administrator" Or str_role = "Registrar" Then
                If str_role = "Administrator" Then
                    frmEnrollStudent.updateSchedAdmin.Visible = True
                Else
                    frmEnrollStudent.updateSchedAdmin.Visible = False
                End If
                'frmEnrollStudent.btnOldSched.Visible = True
                OpenForm(frmEnrollStudent, "Update Class Schedule")
                HideAllFormsInPanelExcept(frmEnrollStudent)
                controlsPanel.Visible = False
                fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_ad_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmEnrollStudent.cbAcad, "tbl_period", "PERIOD", "period_id")
            Else
                MsgBox("Enrollment for the current active period is already finished! Please contact registrar office.", vbExclamation)
            End If
        Else
            If str_role = "Administrator" Then
                frmEnrollStudent.updateSchedAdmin.Visible = True
            Else
                frmEnrollStudent.updateSchedAdmin.Visible = False
            End If

            'frmEnrollStudent.btnOldSched.Visible = True
            OpenForm(frmEnrollStudent, "Update Class Schedule")
            HideAllFormsInPanelExcept(frmEnrollStudent)
            controlsPanel.Visible = False
            fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_ad_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmEnrollStudent.cbAcad, "tbl_period", "PERIOD", "period_id")
        End If
        cn.Close()
    End Sub

    Private Sub btnPaymentMonitoring_Click(sender As Object, e As EventArgs) Handles btnPaymentMonitoring.Click
        frmPaymentMonitoring.LoadRecords()
        OpenForm(frmPaymentMonitoring, "Payment Monitoring")
        HideAllFormsInPanelExcept(frmPaymentMonitoring)
        controlsPanel.Visible = False

        If str_role = "Administrator" Then
            frmPaymentMonitoring.colRemove.Visible = True
            frmPaymentMonitoring.colUpdate.Visible = True
        Else
            frmPaymentMonitoring.colRemove.Visible = False
            frmPaymentMonitoring.colUpdate.Visible = False
        End If
    End Sub

    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        ShowSubButtons(reportsPanel, btnReports)
    End Sub

    Private Sub btnEnrollmentForm_Click(sender As Object, e As EventArgs) Handles btnEnrollmentForm.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelAcad.Visible = True
        frmReports.PanelStudent.Visible = True
        OpenForm(frmReports, "Generate Certificate Of Registration")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnClassMasterList_Click(sender As Object, e As EventArgs) Handles btnClassMasterList.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelAcad.Visible = True
        frmReports.PanelClassSched2.Visible = True

        frmReports.cbClass.Checked = False
        frmReports.cbInstructor.Checked = False
        frmReports.cbSection.Checked = False

        OpenForm(frmReports, "Generate Class Master List")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnAPS_Click(sender As Object, e As EventArgs) Handles btnAPS.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelAcad.Visible = True
        frmReports.PanelStudent.Visible = True
        frmReports.PanelAPS.Visible = True
        OpenForm(frmReports, "Generate Academic Performance Slip")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnSubjectLoad_Click(sender As Object, e As EventArgs) Handles btnSubjectLoad.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelAcad.Visible = True
        frmReports.PanelInstructor.Visible = True
        frmReports.PanelIns2.Visible = True
        frmReports.PanelAdmin.Visible = True
        frmReports.PanelAcadCoor.Visible = True
        OpenForm(frmReports, "Generate Instructor Subject Load")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnSOA_Click(sender As Object, e As EventArgs) Handles btnSOA.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelAcad.Visible = True
        frmReports.PanelStudent.Visible = True
        frmReports.PanelAdmin.Visible = True
        OpenForm(frmReports, "Generate Statement Of Account")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnTOR_Click(sender As Object, e As EventArgs) Handles btnTOR.Click
        CallTOR()
    End Sub
    Sub CallTOR()
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelStudent.Visible = True
        frmReports.PanelRegistrar.Visible = True
        frmReports.PanelRecordReport.Visible = True
        frmReports.PanelTORGraduated.Visible = True
        frmReports.PanelAdmin.Visible = True
        frmReports.PanelRFG.Visible = True
        frmReports.PanelRemarks.Visible = True
        OpenForm(frmReports, "Generate Transcript Of Records")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnForm9_Click(sender As Object, e As EventArgs) Handles btnForm9.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelStudent.Visible = True
        frmReports.PanelRegistrar.Visible = True
        frmReports.PanelAdmin.Visible = True
        frmReports.PanelRFG.Visible = True
        frmReports.PanelRemarks.Visible = True
        OpenForm(frmReports, "Generate Form 9")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnEnrollList_Click(sender As Object, e As EventArgs) Handles btnEnrollList.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelAcad.Visible = True
        frmReports.PanelEnrollmentList.Visible = True
        frmReports.PanelAdmin.Visible = True
        frmReports.PanelRegistrar.Visible = True
        frmReports.PanelCourse.Visible = True
        OpenForm(frmReports, "Generate Enrollment List")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnPromoList_Click(sender As Object, e As EventArgs) Handles btnPromoList.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelAcad.Visible = True
        frmReports.PanelEnrollmentList.Visible = True
        frmReports.PanelAdmin.Visible = True
        frmReports.PanelRegistrar.Visible = True
        frmReports.PanelCourse.Visible = True
        OpenForm(frmReports, "Generate Promotional List")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnEnrolled_Click(sender As Object, e As EventArgs) Handles btnEnrolled.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelAcad.Visible = True
        frmReports.PanelCourse.Visible = True
        frmReports.PanelEnrolledStudent.Visible = True
        frmReports.PanelDate.Visible = True
        OpenForm(frmReports, "Generate Enrolled Student List")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        ShowSubButtons(registrarPanel, btnRegistrar)
    End Sub

    Private Sub btnGrading_Click(sender As Object, e As EventArgs) Handles btnGrading.Click
        If gradingPanel.Visible = False Then
            gradingPanel.Visible = True
        Else
            gradingPanel.Visible = False
        End If
    End Sub

    Private Sub btnNSTP_Click(sender As Object, e As EventArgs) Handles btnNSTP.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        'frmReports.PanelStudent.Visible = True
        frmReports.PanelAcad.Visible = True
        frmReports.PanelNSTP.Visible = True
        frmReports.PanelRegistrar.Visible = True
        frmReports.PanelNSTPFocal.Visible = True
        frmReports.LoadCredentialOptions()
        OpenForm(frmReports, "Generate NSTP Reports")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnHEMIS_Click(sender As Object, e As EventArgs) Handles btnOthers.Click
        'ResetControls(frmReports)
        'frmReports.PrevBtn()
        'frmReports.HideAllReportPanels()
        'frmReports.PanelStudent.Visible = True
        'frmReports.PanelAcad.Visible = True
        'frmReports.PanelCourse.Visible = True
        'frmReports.PanelHemis.Visible = True
        'frmReports.LoadCredentialOptions()
        'OpenForm(frmReports, "Generate Other Reports")
        'HideAllFormsInPanelExcept(frmReports)
        'controlsPanel.Visible = False
        'frmReports.LoadData()
        If PanelOther.Visible = False Then
            PanelOther.Visible = True
            PanelCredReq.Visible = False
            PanelMonitoring.Visible = False
        Else
            PanelOther.Visible = False
            PanelCredReq.Visible = False
            PanelMonitoring.Visible = False
        End If
    End Sub

    Private Sub btnCredentials_Click(sender As Object, e As EventArgs) Handles btnCredentials.Click
        If PanelCredReq.Visible = False Then
            PanelCredReq.Visible = True
            PanelOther.Visible = False
            PanelMonitoring.Visible = False
        Else
            PanelOther.Visible = False
            PanelCredReq.Visible = False
            PanelMonitoring.Visible = False
        End If

    End Sub

    Private Sub btnCredentials_DoubleClick(sender As Object, e As EventArgs) Handles btnCredentials.DoubleClick

    End Sub

    Private Sub btnMonitoring_Click(sender As Object, e As EventArgs) Handles btnMonitoring.Click
        If PanelMonitoring.Visible = False Then
            PanelMonitoring.Visible = True
            PanelOther.Visible = False
            PanelCredReq.Visible = False
        Else
            PanelOther.Visible = False
            PanelCredReq.Visible = False
            PanelMonitoring.Visible = False
        End If
    End Sub

    Private Sub btnESetup_Click(sender As Object, e As EventArgs) Handles btnESetup.Click
        If PanelEnrollmentSetup.Visible = False Then
            PanelEnrollmentSetup.Visible = True
        Else
            PanelEnrollmentSetup.Visible = False
        End If
    End Sub

    Private Sub btnClassSched_Click(sender As Object, e As EventArgs) Handles btnClassSched.Click
        If Application.OpenForms.OfType(Of frmClassSchedList)().Any() Then
        Else
            LibraryClassSchedList()
        End If
        OpenForm(frmClassSchedList, "List Of Class Schedules")
        HideAllFormsInPanelExcept(frmClassSchedList)
        controlsPanel.Visible = True
    End Sub

    Private Sub btnDatabase_Click(sender As Object, e As EventArgs) Handles btnDatabase.Click
        ShowSubButtons(databasePanel, btnDatabase)
    End Sub

    Private Sub btnCredReq_Click(sender As Object, e As EventArgs) Handles btnCredReq.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelAcad.Visible = True
        frmReports.PanelStudent.Visible = True
        frmReports.PanelCredential.Visible = True
        frmReports.PanelAdmin.Visible = True
        frmReports.PanelRegistrar.Visible = True
        frmReports.PanelRecordReport.Visible = True
        frmReports.LoadCredentialOptions()
        OpenForm(frmReports, "Generate Student Credential")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnAck_Click(sender As Object, e As EventArgs) Handles btnAck.Click
        If Application.OpenForms.OfType(Of frmReqAck_Inbound)().Any() Then
        Else
            AcknowledgementList()
        End If
        OpenForm(frmReqAck_Inbound, "Credential Acknowledgement - Inbound")
        HideAllFormsInPanelExcept(frmReqAck_Inbound)
        controlsPanel.Visible = False
        frmReqAck_Inbound.cbFilter.SelectedIndex = 0
    End Sub

    Private Sub btnCurSetup_Click(sender As Object, e As EventArgs) Handles btnCurSetup.Click
        If Application.OpenForms.OfType(Of frmCurriculumList)().Any() Then
        Else
            CurriculumList()
        End If
        OpenForm(frmCurriculumList, "List Of Curriculum")
        HideAllFormsInPanelExcept(frmCurriculumList)
        controlsPanel.Visible = True
    End Sub

    Private Sub btnSevaluation_Click(sender As Object, e As EventArgs) Handles btnSevaluation.Click
        OpenForm(frmStudentEvaluation, "Student Evaluation")
        HideAllFormsInPanelExcept(frmStudentEvaluation)
        controlsPanel.Visible = False
    End Sub

    Private Sub btnTOR2_Click(sender As Object, e As EventArgs) Handles btnTOR2.Click
        CallTOR()
    End Sub

    Private Sub btnClassGradeEditor_Click(sender As Object, e As EventArgs) Handles btnClassGradeEditor.Click
        fillCombo("Select CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_class_schedule t1 JOIN tbl_period t2 ON t1.csperiod_id = t2.period_id group by t1.csperiod_id order by `period_name` desc, `period_status` ASC, `period_semester` desc", frmClassGradeEditor.cbAcademicYear, "tbl_period", "PERIOD", "period_id")
        OpenForm(frmClassGradeEditor, "Edit Class Grades")
        HideAllFormsInPanelExcept(frmClassGradeEditor)
        controlsPanel.Visible = False
    End Sub

    Private Sub btnStudentGradeEditor_Click(sender As Object, e As EventArgs) Handles btnStudentGradeEditor.Click
        'fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period order by `period_name` desc, `period_semester` desc, `period_status` asc", frmStudentGradeEditor.cbAcademicYear, "tbl_period", "PERIOD", "period_id")
        OpenForm(frmStudentGradeEditor, "Edit Student Grades")
        HideAllFormsInPanelExcept(frmStudentGradeEditor)
        controlsPanel.Visible = False
        If str_role = "Administrator" Or str_role = "Registrar" Then
            frmStudentGradeEditor.GradeOptionsFlowLayoutPanel.Visible = True
        Else
            frmStudentGradeEditor.GradeOptionsFlowLayoutPanel.Visible = False
        End If
    End Sub

    Private Sub btnGradeCrediting_Click(sender As Object, e As EventArgs) Handles btnGradeCrediting.Click
        'fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period order by `period_name` desc, `period_semester` desc, `period_status` asc", frmCreditGrade.cbAcademicYear, "tbl_period", "PERIOD", "period_id")
        OpenForm(frmCreditGrade, "Credit Student Grades")
        HideAllFormsInPanelExcept(frmCreditGrade)
        controlsPanel.Visible = False
    End Sub

    Private Sub btnOTR_Click(sender As Object, e As EventArgs) Handles btnOTR.Click
        CallOTR()
    End Sub
    Sub CallOTR()
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.HideAllReportPanels()
        frmReports.PanelStudent.Visible = True
        frmReports.PanelRegistrar.Visible = True
        frmReports.PanelRecordReport.Visible = True
        frmReports.PanelTORGraduated.Visible = True
        frmReports.PanelAdmin.Visible = True
        frmReports.PanelRFG.Visible = True
        frmReports.PanelRemarks.Visible = True
        frmReports.OTRack.Visible = True
        OpenForm(frmReports, "Generate Official Transcript Of Records")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub btnTransmittal_Click(sender As Object, e As EventArgs) Handles btnTransmittal.Click
        ResetControls(frmReports)
        frmReports.PrevBtn()
        frmReports.LoadCredentialOptions()
        frmReports.HideAllReportPanels()
        frmReports.PanelAcad.Visible = True
        'frmReports.PanelCredential.Visible = True
        frmReports.PanelSchool.Visible = True
        frmReports.TransmittalPanel.Visible = True
        frmReports.PanelRegistrar.Visible = True
        frmReports.PanelRecordReport.Visible = True

        OpenForm(frmReports, "Generate Student Credential Transmittal")
        HideAllFormsInPanelExcept(frmReports)
        controlsPanel.Visible = False
        frmReports.LoadData()
    End Sub

    Private Sub cbUserLogs_Click(sender As Object, e As EventArgs) Handles cbUserLogs.Click
        'If Application.OpenForms.OfType(Of frmUserLogs)().Any() Then
        'Else
        UserLogList()
        'End If
        OpenForm(frmUserLogs, "User Logs")
        HideAllFormsInPanelExcept(frmUserLogs)
        controlsPanel.Visible = True
        btnAdd.Visible = False
    End Sub

    Private Sub btnCashDenomination_Click(sender As Object, e As EventArgs) Handles btnCashDenomination.Click
        frmCashDenomination.LoadRecords()
        OpenForm(frmCashDenomination, "Cash Denomination")
        HideAllFormsInPanelExcept(frmCashDenomination)
        controlsPanel.Visible = False
    End Sub

    Private Sub btnEnrollEditor_Click(sender As Object, e As EventArgs) Handles btnEnrollEditor.Click
        OpenForm(frmEnrollmentEditor, "Withdraw Enrolled Schedule (List of Enrolled)")
        HideAllFormsInPanelExcept(frmEnrollmentEditor)
        controlsPanel.Visible = False
        fillCombo("SELECT period_id, CONCAT(period_name,'-',period_semester) as 'PERIOD' FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmEnrollmentEditor.cbAcademicYear, "tbl_period", "PERIOD", "period_id")
        frmEnrollmentEditor.LibraryWithdrawEnrollmentStudentList()
    End Sub

    Private Sub btnEnrollPermit_Click(sender As Object, e As EventArgs) Handles btnEnrollPermit.Click
        fillCombo("SELECT period_id, CONCAT(period_name,'-',period_semester) as 'PERIOD' FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmEnrollmentPermit.cbAcademicYear, "tbl_period", "PERIOD", "period_id")
        frmEnrollmentPermit.ShowDialog()
    End Sub

    Private Sub btnAccountAdjustment_Click(sender As Object, e As EventArgs) Handles btnAccountAdjustment.Click
        OpenForm(frmAdjustments, "Student Account Adjustment")
        HideAllFormsInPanelExcept(frmAdjustments)
        controlsPanel.Visible = False
    End Sub

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

    Private Sub btnAssessmentSetup_Click(sender As Object, e As EventArgs) Handles btnAssessmentSetup.Click
        If PanelAssessment.Visible = True Then
            PanelAssessment.Visible = False
        Else
            PanelAssessment.Visible = True
        End If
    End Sub

    Private Sub btnAssessment_Click(sender As Object, e As EventArgs) Handles btnAssessment.Click
        If Application.OpenForms.OfType(Of frmAssessment)().Any() Then
        Else
            frmAssessment.AssessmentCourseList()
        End If
        OpenForm(frmAssessment, "Course Assessment")
        HideAllFormsInPanelExcept(frmAssessment)
        controlsPanel.Visible = False
    End Sub

    Private Sub btnDiscount_Click(sender As Object, e As EventArgs) Handles btnDiscount.Click
        frmDiscount.ShowDialog()
    End Sub

    Private Sub hideMenu_Click(sender As Object, e As EventArgs) Handles hideMenu.Click
        PanelMenu.Size = New Size(40, 682)
        hideMenu.Visible = False
        showMenu.Visible = True

        'For Each control As Control In formPanel.Controls
        '    If TypeOf control Is Form Then
        '        Dim formsToExpand As Form = DirectCast(control, Form)
        '        formsToExpand.TopLevel = False ' Ensure it is a child form
        '        formsToExpand.FormBorderStyle = FormBorderStyle.None ' Remove form borders
        '        formsToExpand.Dock = DockStyle.Fill ' Maximize the form inside the panel
        '        formsToExpand.Show() ' Ensure the form is shown
        '    End If
        'Next
    End Sub

    Private Sub showMenu_Click(sender As Object, e As EventArgs) Handles showMenu.Click
        PanelMenu.Size = New Size(240, 682)
        showMenu.Visible = False
        hideMenu.Visible = True

        'For Each control As Control In formPanel.Controls
        '    If TypeOf control Is Form Then
        '        Dim formsToExpand As Form = DirectCast(control, Form)
        '        formsToExpand.TopLevel = False ' Ensure it is a child form
        '        formsToExpand.FormBorderStyle = FormBorderStyle.None ' Remove form borders
        '        formsToExpand.Dock = DockStyle.Fill ' Maximize the form inside the panel
        '        formsToExpand.Show() ' Ensure the form is shown
        '    End If
        'Next
    End Sub

    Private Sub btnAck2_Click(sender As Object, e As EventArgs) Handles btnAck2.Click
        If Application.OpenForms.OfType(Of frmReqAck_Outbound)().Any() Then
        Else
            AcknowledgementList2()
        End If
        OpenForm(frmReqAck_Outbound, "Credential Acknowledgement - Outbound")
        HideAllFormsInPanelExcept(frmReqAck_Outbound)
        controlsPanel.Visible = False
        frmReqAck_Outbound.cbFilter.SelectedIndex = 0
    End Sub

    Private Sub btnEnrolledBreakdown2_Click(sender As Object, e As EventArgs) Handles btnEnrolledBreakdown2.Click
        dashboard.Visible = False
        lblDashboardDetailsTitle.Text = "Enrolled Students Academic Year " & lblAcadYear.Text & " - " & lblSemester.Text & ""
        PanelEnrollmentDetails.BringToFront()
        CreateOverAllEnrolledBarGraph()
        GraphSelectedCourse = ""
        GraphSelectedYear = ""
    End Sub

    Private Sub btnCloseDashboardDetails_Click(sender As Object, e As EventArgs) Handles btnCloseDashboardDetails.Click
        If EnrollmentSubGraph.Visible = True Then
            EnrollmentSubGraph.Visible = False
        Else
            dashboard.Visible = True
            lblDashboardDetailsTitle.Text = "Enrolled Students Academic Year " & lblAcadYear.Text & " - " & lblSemester.Text & ""
        End If
    End Sub

    Private Sub lblTotalCollected_Click(sender As Object, e As EventArgs) Handles lblTotalCollected.Click
        dashboard.Visible = False
        lblDashboardDetailsTitle.Text = "Payments Collected Today"
        PanelPaymentDetails.BringToFront()
    End Sub

    Private Sub BtnUserAccounts_Click(sender As Object, e As EventArgs) Handles btnUserAccounts.Click
        If Application.OpenForms.OfType(Of frmUserList)().Any() Then
        Else
            LibraryUserList()
        End If
        OpenForm(frmUserList, "List Of User Accounts")
        HideAllFormsInPanelExcept(frmUserList)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub










    ' Event handler for valueLabel click
    Private Sub ValueLabel_Click(sender As Object, e As EventArgs)
        Dim valueLabel As Label = CType(sender, Label)
        Dim rotatedLabel As RotatedLabel = CType(valueLabel.Tag, RotatedLabel)

        If rotatedLabel IsNot Nothing Then
            lblDashboardDetailsTitle.Text = "Enrolled Students Academic Year " & lblAcadYear.Text & " - " & lblSemester.Text & " ( " & rotatedLabel.Text & ")"
            CreateYearLevelAllEnrolledBarGraph(rotatedLabel.Text)
            GraphSelectedCourse = rotatedLabel.Text
        End If
    End Sub

    ' Event handler for rotatedLabel click
    Private Sub RotatedLabel_Click(sender As Object, e As EventArgs)
        Dim rotatedLabel As RotatedLabel = CType(sender, RotatedLabel)
        lblDashboardDetailsTitle.Text = "Enrolled Students Academic Year " & lblAcadYear.Text & " - " & lblSemester.Text & " ( " & rotatedLabel.Text & ")"
        CreateYearLevelAllEnrolledBarGraph(rotatedLabel.Text)
    End Sub



    Function GetDataOverAll() As DataTable
        Dim dt As New DataTable
        Try
            Using cmd As New MySqlCommand("select DISTINCT(t1.course_code) as Course, ifNULL(t100.Total,0) as Students, t1.course_id as CourseID from (SELECT t1.sg_student_id as SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, s_status, course_code, course_major, course_id FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id where t1.sg_period_id = 188 and t1.sg_grade_status = 'Enrolled' GROUP BY t1.sg_student_id) t1 LEFT JOIN (select COUNT(SCount) as 'Total', course_id from (SELECT t1.sg_student_id as SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, s_status, course_code, course_major, course_id FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id where t1.sg_period_id = " & activeAcademicYear & " and t1.sg_grade_status = 'Enrolled' GROUP BY t1.sg_student_id) t1 group by course_id) t100 ON t1.course_id = t100.course_id", cn)
                cn.Close()
                cn.Open()
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error accessing the database: " & ex.Message)
        End Try

        Return dt
    End Function
    Sub CreateOverAllEnrolledBarGraph()
        EnrollmentBarGraph.Controls.Clear()

        Dim dt As DataTable = GetDataOverAll()

        If dt.Rows.Count = 0 Then
            MessageBox.Show("No data available to display the bar graph.")
            Return
        End If

        Dim maxValue As Integer
        Try
            maxValue = dt.AsEnumerable().Max(Function(row) Convert.ToInt32(row.Field(Of Long)("Students")))
        Catch ex As Exception
            MessageBox.Show("Error calculating max value: " & ex.Message)
            Return
        End Try

        Dim panelWidth As Integer = EnrollmentBarGraph.Width
        Dim panelHeight As Integer = EnrollmentBarGraph.Height
        Dim barSpacing As Integer = 10
        Dim barWidth As Integer = (panelWidth - ((dt.Rows.Count + 1) * barSpacing)) / dt.Rows.Count

        For Each row As DataRow In dt.Rows
            Dim barPanel As New Panel
            Dim barHeight As Integer = If(CInt(Convert.ToInt32(row.Field(Of Long)("Students"))) >= 15, CInt((Convert.ToInt32(row.Field(Of Long)("Students")) / maxValue) * panelHeight), 20)
            barPanel.Width = barWidth
            barPanel.Height = barHeight
            barPanel.BackColor = Color.FromArgb(15, 101, 208)
            barPanel.Location = New Point(barSpacing + (barWidth + barSpacing) * dt.Rows.IndexOf(row), panelHeight - barHeight)
            EnrollmentBarGraph.Controls.Add(barPanel)

            ' Create and add label for the number value inside the bar panel
            Dim valueLabel As New Label
            valueLabel.Text = Convert.ToInt32(row.Field(Of Long)("Students")).ToString()
            valueLabel.Font = New Font("Century Gothic", 8, FontStyle.Regular)
            valueLabel.ForeColor = Color.White
            valueLabel.AutoSize = False
            valueLabel.Dock = DockStyle.Top
            valueLabel.TextAlign = ContentAlignment.MiddleCenter
            valueLabel.Cursor = Cursors.Hand

            ' Temporary size and background color for debugging
            valueLabel.BackColor = Color.Red ' Set a distinct color for visibility
            'valueLabel.Size = New Size(50, 20) ' Adjust size for testing
            ' Position the value label inside the bar panel
            valueLabel.Location = New Point((barWidth - valueLabel.Width) / 2, barHeight - valueLabel.Height - 2) ' Adjusted position with 2 pixels padding
            AddHandler valueLabel.Click, AddressOf ValueLabel_Click
            barPanel.Controls.Add(valueLabel)
            valueLabel.BringToFront()

            Dim rotatedLabel As New RotatedLabel
            rotatedLabel.Text = row.Field(Of String)("Course")
            rotatedLabel.Font = New Font("Century Gothic", 8, FontStyle.Regular)
            rotatedLabel.ForeColor = Color.Black
            rotatedLabel.Cursor = Cursors.Hand
            rotatedLabel.Angle = -90 ' Adjust the angle as needed
            ' Set a size for the control that will fit the rotated text
            rotatedLabel.Size = New Size(100, 50) ' Adjust size as needed
            If panelHeight - barHeight - 10 >= rotatedLabel.Height Then
                ' Enough space above the bar panel
                rotatedLabel.Location = New Point(barPanel.Location.X + (barWidth - rotatedLabel.Width) / 2, panelHeight - barHeight - rotatedLabel.Height - 5) ' Center above the bar panel
            Else
                ' Not enough space, place label inside the bar panel
                rotatedLabel.Location = New Point(barPanel.Location.X + barWidth / 2 - rotatedLabel.Width / 2, barPanel.Location.Y + 30) ' Adjust this value to set the label at the top
                rotatedLabel.BackColor = Color.FromArgb(15, 101, 208)
                rotatedLabel.ForeColor = Color.White
            End If
            EnrollmentBarGraph.Controls.Add(rotatedLabel)
            rotatedLabel.BringToFront()
            valueLabel.Tag = rotatedLabel
            AddHandler rotatedLabel.Click, AddressOf RotatedLabel_Click
        Next
    End Sub




    Function GetDataYearLevel(ByVal Course As String) As DataTable
        Dim dt As New DataTable
        Try
            Using cmd As New MySqlCommand("select (tt1.sg_yearlevel) as 'Year Level', ifNULL(SUM(tt1.SCount),0) as Students from (SELECT count(DISTINCT t1.sg_student_id) as SCount, sg_yearlevel FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) as Student, eperiod_id as Period, eenrolledby_datetime as EnrollDate from tbl_enrollment where eperiod_id = " & activeAcademicYear & ") t4 ON t1.sg_student_id = t4.Student and t1.sg_period_id = t4.Period where t1.sg_period_id = " & activeAcademicYear & " and t1.sg_grade_status = 'Enrolled'  and t3.course_code = '" & Course & "' GROUP BY t1.sg_course_id, t1.sg_yearlevel) tt1 group by tt1.sg_yearlevel", cn)
                cn.Close()
                cn.Open()
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error accessing the database: " & ex.Message)
        End Try

        Return dt
    End Function
    Sub CreateYearLevelAllEnrolledBarGraph(ByVal Course As String)
        EnrollmentBarGraph.Controls.Clear()

        Dim dt As DataTable = GetDataYearLevel(Course)

        If dt.Rows.Count = 0 Then
            dashboard.Visible = True
            Return
        End If

        Dim maxValue As Integer = 1 ' Default to 1 to avoid divide-by-zero error
        Try
            ' Check for DBNull values and convert to Integer safely
            maxValue = dt.AsEnumerable().Where(Function(row) Not IsDBNull(row.Field(Of Object)("Students"))).
            Max(Function(row) Convert.ToInt32(row.Field(Of Object)("Students")))
        Catch ex As Exception
            MessageBox.Show("Error calculating max value: " & ex.Message)
            Return
        End Try


        Dim panelWidth As Integer = EnrollmentBarGraph.Width
        Dim panelHeight As Integer = EnrollmentBarGraph.Height
        Dim barSpacing As Integer = 10
        Dim barWidth As Integer = (panelWidth - ((dt.Rows.Count + 1) * barSpacing)) / dt.Rows.Count

        For Each row As DataRow In dt.Rows
            Dim barPanel As New Panel

            ' Safely get the value and handle DBNull
            Dim students As Integer = If(IsDBNull(row("Students")), 0, Convert.ToInt32(row("Students")))


            Dim barHeight As Integer = If(students >= 15, CInt((students / maxValue) * panelHeight), 20)

            If dt.Rows.Count = 1 Then
                ' Set a fixed width for a single bar
                barWidth = 300 ' Set the fixed width for a single bar (adjust as needed)
                barPanel.Location = New Point((panelWidth - barWidth) / 2, panelHeight - barHeight)
            ElseIf dt.Rows.Count = 2 Then
                barWidth = 200
                barPanel.Location = New Point((panelWidth - barWidth) / 2, panelHeight - barHeight)
            Else
                barPanel.Width = barWidth
                barPanel.Location = New Point(barSpacing + (barWidth + barSpacing) * dt.Rows.IndexOf(row), panelHeight - barHeight)
            End If

            barPanel.Width = barWidth
            barPanel.Height = barHeight
            barPanel.BackColor = Color.FromArgb(15, 101, 208)
            EnrollmentBarGraph.Controls.Add(barPanel)

            ' Create and add label for the number value inside the bar panel
            Dim valueLabel As New Label
            valueLabel.Text = students.ToString()
            'valueLabel.Text = Convert.ToInt32(row.Field(Of Long)("Students")).ToString()
            valueLabel.Font = New Font("Century Gothic", 8, FontStyle.Regular)
            valueLabel.ForeColor = Color.White
            valueLabel.AutoSize = False
            valueLabel.Dock = DockStyle.Top
            valueLabel.TextAlign = ContentAlignment.MiddleCenter
            valueLabel.Cursor = Cursors.Hand

            ' Temporary size and background color for debugging
            valueLabel.BackColor = Color.Red ' Set a distinct color for visibility
            'valueLabel.Size = New Size(50, 20) ' Adjust size for testing
            ' Position the value label inside the bar panel
            valueLabel.Location = New Point((barWidth - valueLabel.Width) / 2, barHeight - valueLabel.Height - 2) ' Adjusted position with 2 pixels padding
            AddHandler valueLabel.Click, AddressOf ValueLabel_Click
            barPanel.Controls.Add(valueLabel)
            valueLabel.BringToFront()


            ' Add hover event to display additional data
            AddHandler valueLabel.MouseEnter, Sub(sender As Object, e As EventArgs)
                                                  ShowDataPanel(valueLabel)
                                              End Sub
            AddHandler valueLabel.MouseLeave, Sub(sender As Object, e As EventArgs)
                                                  HideDataPanel()
                                              End Sub



            Dim rotatedLabel As New RotatedLabel
            rotatedLabel.Text = row.Field(Of String)("Year Level")
            rotatedLabel.Font = New Font("Century Gothic", 8, FontStyle.Regular)
            rotatedLabel.ForeColor = Color.Black
            rotatedLabel.Cursor = Cursors.Hand
            rotatedLabel.Angle = 0 ' Adjust the angle as needed
            ' Set a size for the control that will fit the rotated text
            rotatedLabel.Size = New Size(100, 50) ' Adjust size as needed
            If panelHeight - barHeight - 10 >= rotatedLabel.Height Then
                ' Enough space above the bar panel
                rotatedLabel.Location = New Point(barPanel.Location.X + (barWidth - rotatedLabel.Width) / 2, panelHeight - barHeight - rotatedLabel.Height - 5) ' Center above the bar panel
            Else
                ' Not enough space, place label inside the bar panel
                rotatedLabel.Location = New Point(barPanel.Location.X + barWidth / 2 - rotatedLabel.Width / 2, barPanel.Location.Y + 30) ' Adjust this value to set the label at the top
                rotatedLabel.BackColor = Color.FromArgb(15, 101, 208)
                rotatedLabel.ForeColor = Color.White
            End If
            EnrollmentBarGraph.Controls.Add(rotatedLabel)
            rotatedLabel.BringToFront()
            valueLabel.Tag = rotatedLabel
            AddHandler rotatedLabel.Click, AddressOf RotatedLabel_Click
        Next
    End Sub

    Function GetDataStudentsEnrolled() As DataTable
        Dim dt As New DataTable
        Try
            Using cmd As New MySqlCommand("Select count(tt1.SCount)as Student, tt1.s_status as 'Status' From (SELECT t1.sg_student_id as SCount, s_fn, s_mn, s_ln, s_gender, sg_yearlevel, course_code, course_name, DATE_FORMAT(EnrollDate, '%M %d, %Y'), s_status FROM tbl_students_grades t1 LEFT JOIN tbl_student t2 ON t1.sg_student_id = t2.s_id_no LEFT JOIN tbl_course t3 ON t1.sg_course_id = t3.course_id LEFT JOIN (SELECT DISTINCT(estudent_id) as Student, eperiod_id as Period, eenrolledby_datetime as EnrollDate from tbl_enrollment where eperiod_id = " & activeAcademicYear & ") t4 ON t1.sg_student_id = t4.Student and t1.sg_period_id = t4.Period where t1.sg_period_id = " & activeAcademicYear & " and t1.sg_grade_status = 'Enrolled' GROUP BY t1.sg_student_id order by course_code asc) tt1 WHERE tt1.course_code = '" & GraphSelectedCourse & "' and tt1.sg_yearlevel = '" & GraphSelectedYear & "' group by s_status", cn)
                cn.Close()
                cn.Open()
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error accessing the database: " & ex.Message)
        End Try

        Return dt
    End Function

    Private Sub ShowDataPanel(valueLabel As Label)

        Dim rotatedLabel As RotatedLabel = CType(valueLabel.Tag, RotatedLabel)
        GraphSelectedYear = rotatedLabel.Text

        If hoverPanel IsNot Nothing Then
            PanelEnrollmentDetails.Controls.Remove(hoverPanel)
            hoverPanel.Dispose()
        End If

        hoverPanel = New FlowLayoutPanel
        hoverPanel.BorderStyle = BorderStyle.FixedSingle
        hoverPanel.BackColor = Color.LightYellow
        hoverPanel.Padding = New Padding(5)
        hoverPanel.WrapContents = False
        hoverPanel.FlowDirection = FlowDirection.TopDown
        hoverPanel.AutoSize = True

        ' Set hoverPanel width to match barPanel width
        hoverPanel.Width = valueLabel.Parent.Width

        ' Fetch the additional data
        Dim additionalDataTable As DataTable = GetDataStudentsEnrolled()

        ' Create and add labels for displaying additional data
        For Each additionalDataRow As DataRow In additionalDataTable.Rows
            Dim dataLabel As New Label
            dataLabel.Text = String.Format("{0}: {1}", additionalDataRow("Status"), additionalDataRow("Student"))
            dataLabel.AutoSize = True
            hoverPanel.Controls.Add(dataLabel)
        Next

        ' Adjust the hoverPanel height based on content



        ' Position the hoverPanel relative to the valueLabel
        Dim position As Point = valueLabel.PointToScreen(Point.Empty)
        position = PanelEnrollmentDetails.PointToClient(position)

        ' Calculate available space
        Dim spaceAbove As Integer = position.Y
        Dim spaceBelow As Integer = PanelEnrollmentDetails.Height - (position.Y + valueLabel.Height)

        If hoverPanel.Height <= spaceAbove Then
            ' Place above the valueLabel
            hoverPanel.Location = New Point(position.X, position.Y - hoverPanel.Height)
        ElseIf hoverPanel.Height <= spaceBelow Then
            ' Place below the valueLabel
            hoverPanel.Location = New Point(position.X, position.Y + valueLabel.Height)
        Else
            ' If there's not enough space either above or below, place it where there's more space
            If spaceBelow > spaceAbove Then
                hoverPanel.Location = New Point(position.X, position.Y + valueLabel.Height)
            Else
                hoverPanel.Location = New Point(position.X, position.Y - hoverPanel.Height)
            End If
        End If

        PanelEnrollmentDetails.Controls.Add(hoverPanel)
        hoverPanel.BringToFront()
    End Sub

    Private Sub HideDataPanel()
        If hoverPanel IsNot Nothing Then
            PanelEnrollmentDetails.Controls.Remove(hoverPanel)
            hoverPanel.Dispose()
            hoverPanel = Nothing
        End If
    End Sub

    Private Sub User_Photo_Click(sender As Object, e As EventArgs) Handles User_Photo.Click
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_user_account where ua_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", str_userid)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmUser
                    .AccountUserID = str_userid
                    .txtFirstName.Text = dr.Item("ua_first_name").ToString
                    .txtMiddleName.Text = dr.Item("ua_middle_name").ToString
                    .txtLastName.Text = dr.Item("ua_last_name").ToString
                    .txtAddress.Text = dr.Item("ua_address").ToString
                    .txtContact.Text = dr.Item("ua_contact").ToString
                    .cbAccountType.Text = dr.Item("ua_account_type").ToString
                    .txtUsername.Text = dr.Item("ua_user_name").ToString
                    .txtPassword.Text = dr.Item("ua_password").ToString
                    Try
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("select ua_photo from tbl_user_account where ua_id = @1", cn)
                        With cm
                            .Parameters.AddWithValue("@1", str_userid)
                        End With
                        dr = cm.ExecuteReader
                        While dr.Read
                            Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                            Dim array(CInt(len)) As Byte
                            dr.GetBytes(0, 0, array, 0, CInt(len))
                            Dim ms As New MemoryStream(array)
                            Dim bitmap As New System.Drawing.Bitmap(ms)
                            .userPhoto.Image = bitmap
                        End While
                        dr.Close()
                        cn.Close()
                    Catch ex As Exception
                        .userPhoto.Image = .Dummypicture.Image
                    End Try
                    .btnSave.Visible = False
                    .btnUpdate.Visible = True
                    .txtUsername.Enabled = False
                    .SystemModules()
                    .ShowDialog()
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub btnSupply_Click(sender As Object, e As EventArgs) Handles btnSupply.Click
        ShowSubButtons(supplyPanel, btnSupply)
    End Sub

    Private Sub btnSupplyInventory_Click(sender As Object, e As EventArgs) Handles btnSupplyInventory.Click
        If panelInventory.Visible = True Then
            panelInventory.Visible = False
        Else
            panelInventory.Visible = True
        End If
    End Sub

    Private Sub btnItems_Click(sender As Object, e As EventArgs) Handles btnItems.Click
        If Application.OpenForms.OfType(Of frmSupplyItems)().Any() Then
        Else
            frmSupplyItems.SupplyItemList()
        End If
        OpenForm(frmSupplyItems, "List Of Supply Items")
        HideAllFormsInPanelExcept(frmSupplyItems)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnPurchasing_Click(sender As Object, e As EventArgs) Handles btnPurchasing.Click
        If PanelPurchasing.Visible = True Then
            PanelPurchasing.Visible = False
        Else
            PanelPurchasing.Visible = True
        End If
    End Sub

    Private Sub btnSupplyRecords_Click(sender As Object, e As EventArgs) Handles btnSupplyRecords.Click
        If PanelRecords.Visible = True Then
            PanelRecords.Visible = False
        Else
            PanelRecords.Visible = True
        End If
    End Sub

    Private Sub btnRequests_Click(sender As Object, e As EventArgs) Handles btnRequests.Click
        OpenForm(frmSupplyRecords, "Supply Requests Record")
        HideAllFormsInPanelExcept(frmSupplyRecords)
        controlsPanel.Visible = False
    End Sub

    Private Sub btnBestRequested_Click(sender As Object, e As EventArgs) Handles btnBestRequested.Click
        OpenForm(frmSupplyBestSelling, "Supply Best Requested Items")
        HideAllFormsInPanelExcept(frmSupplyBestSelling)
        frmSupplyBestSelling.loadRecords()
        controlsPanel.Visible = False
    End Sub

    Private Sub btnSupplySales_Click(sender As Object, e As EventArgs) Handles btnSupplySales.Click
        OpenForm(frmSupplySales, "Supply Sales")
        HideAllFormsInPanelExcept(frmSupplySales)
        frmSupplySales.loadSales()
        controlsPanel.Visible = False
    End Sub

    Private Sub btnItemRequest_Click(sender As Object, e As EventArgs) Handles btnItemRequest.Click
        frmSupplyPOS.Show()
    End Sub

    Private Sub btnStockLevel_Click(sender As Object, e As EventArgs) Handles btnStockLevel.Click
        If Application.OpenForms.OfType(Of frmSupplyStockLevel)().Any() Then
        Else
            frmSupplyStockLevel.SupplyItemStockLevel()
        End If
        OpenForm(frmSupplyStockLevel, "Supply Items Stock Level")
        HideAllFormsInPanelExcept(frmSupplyStockLevel)
        controlsPanel.Visible = True
    End Sub

    Private Sub cmbSupplyType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplyType.SelectedIndexChanged
        SelectionTitle.Text = cmbSupplyType.Text
        MainSearch()
    End Sub

    Private Sub btnItemLedger_Click(sender As Object, e As EventArgs) Handles btnItemLedger.Click
        OpenForm(frmSupplyItemLedger, "Supply Item Ledger")
        HideAllFormsInPanelExcept(frmSupplyItemLedger)
        controlsPanel.Visible = False
    End Sub

    Private Sub btnPR_Click(sender As Object, e As EventArgs) Handles btnPR.Click
        If Application.OpenForms.OfType(Of frmSupplyPRRecords)().Any() Then
        Else
            frmSupplyPRRecords.PurchaseRequestList()
        End If
        OpenForm(frmSupplyPRRecords, "List Of Purchase Requests")
        HideAllFormsInPanelExcept(frmSupplyPRRecords)
        controlsPanel.Visible = True
        btnAdd.Visible = True
    End Sub

    Private Sub btnPRD_Click(sender As Object, e As EventArgs) Handles btnPRD.Click

    End Sub

    Private Sub btnStockRecount_Click(sender As Object, e As EventArgs) Handles btnStockRecount.Click
        OpenForm(frmSupplyPhysicalInventory, "Supply Items Stock Recounting")
        HideAllFormsInPanelExcept(frmSupplyPhysicalInventory)
        controlsPanel.Visible = False
    End Sub
End Class

Public Class RotatedLabel
    Inherits Control

    Private _text As String
    Private _angle As Single

    Public Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
            Invalidate()
        End Set
    End Property

    Public Property Angle As Single
        Get
            Return _angle
        End Get
        Set(value As Single)
            _angle = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim font As New Font("Century Gothic", 8, FontStyle.Regular)
        Dim textSize As SizeF = g.MeasureString(_text, font)

        ' Calculate the rotated bounding box
        Dim rotatedSize As SizeF
        Dim halfWidth As Single = textSize.Width / 2
        Dim halfHeight As Single = textSize.Height / 2

        Dim angleRad As Single = _angle * Math.PI / 180
        Dim cosAngle As Single = Math.Cos(angleRad)
        Dim sinAngle As Single = Math.Sin(angleRad)

        Dim width As Single = Math.Abs(cosAngle * textSize.Width) + Math.Abs(sinAngle * textSize.Height)
        Dim height As Single = Math.Abs(sinAngle * textSize.Width) + Math.Abs(cosAngle * textSize.Height)

        rotatedSize = New SizeF(width, height)

        ' Rotate text
        g.TranslateTransform(Me.Width / 2, Me.Height / 2)
        g.RotateTransform(_angle)
        g.TranslateTransform(-Me.Width / 2, -Me.Height / 2)

        Dim textBrush As New SolidBrush(ForeColor)
        g.DrawString(_text, font, textBrush, (Me.Width - textSize.Width) / 2, (Me.Height - textSize.Height) / 2)
    End Sub

    Protected Overrides Sub OnLayout(e As LayoutEventArgs)
        MyBase.OnLayout(e)

        ' Adjust the control's size to fit the rotated text
        Dim font As New Font("Century Gothic", 8, FontStyle.Regular)
        Dim textSize As SizeF

        Using g As Graphics = Me.CreateGraphics()
            textSize = g.MeasureString(_text, font)
        End Using

        ' Calculate the rotated bounding box
        Dim halfWidth As Single = textSize.Width / 2
        Dim halfHeight As Single = textSize.Height / 2

        Dim angleRad As Single = _angle * Math.PI / 180
        Dim cosAngle As Single = Math.Cos(angleRad)
        Dim sinAngle As Single = Math.Sin(angleRad)

        Dim width As Single = Math.Abs(cosAngle * textSize.Width) + Math.Abs(sinAngle * textSize.Height)
        Dim height As Single = Math.Abs(sinAngle * textSize.Width) + Math.Abs(cosAngle * textSize.Height)

        Me.Size = New Size(CInt(width), CInt(height))
    End Sub
End Class
