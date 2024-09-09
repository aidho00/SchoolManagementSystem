Imports MySql.Data.MySqlClient

Module Records
    Dim arrImage() As Byte
    Dim arrImage2() As Byte
    Dim dt As New DataTable
    Dim acadID As Integer
#Region "Dashboard"
    Public Sub loadDashboard()
        With frmMain
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT period_id as PERIOD from tbl_period where period_status = 'Active'", cn)
            activeAcademicYear = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT CONCAT(period_name) as PERIOD from tbl_period where period_status = 'Active'", cn)
            .lblAcadYear.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT CONCAT(period_semester) from tbl_period where period_status = 'Active'", cn)
            .lblSemester.Text = cm.ExecuteScalar.ToString.ToUpper
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT FORMAT(COUNT(DISTINCT sg_student_id),0) from tbl_students_grades where sg_period_id = " & activeAcademicYear & " and sg_grade_status = 'Enrolled'", cn)
            .btnEnrolledBreakdown2.Text = Format(CDbl(cm.ExecuteScalar), "#,##0")
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT COUNT(DISTINCT sg_course_id) as courses from tbl_students_grades where sg_period_id = '" & activeAcademicYear & "' and sg_grade_status = 'Enrolled'", cn)
            .lblCourses.Text = cm.ExecuteScalar
            cn.Close()
        End With
    End Sub
#End Region
#Region "Library Records"
    Sub MainSearch()
        Select Case frmMain.formTitle.Text
            Case "List Of Students"
                LibraryStudentList()
            Case "List Of Academic Years"
                LibraryAcadList()
            Case "List Of Class Schedules"
                LibraryClassSchedList()
            Case "Setup Class Schedules"
                LibraryClassSchedList()
            Case "List Of Courses"
                LibraryCourseList()
            Case "List Of Day Schedules"
                LibraryDaySchedList()
            Case "List Of Employees"
                LibraryEmployeeList()
            Case "List Of Rooms"
                LibraryRoomList()
            Case "List Of Schools"
                LibrarySchoolList()
            Case "List Of Class Sections"
                LibrarySectionList()
            Case "List Of Subjects"
                LibrarySubjectList()
            Case "List Of Curriculum"
                CurriculumList()
            Case "User Logs"
                UserLogList()
            Case "List Of User Accounts"
                LibraryUserList()
            Case "List Of Supply Items"
                frmSupplyItems.SupplyItemList()
            Case "Supply Items Stock Level"
                frmSupplyStockLevel.SupplyItemStockLevel()
            Case "List Of Purchase Requests"
                frmSupplyPRRecords.PurchaseRequestList()
            Case "List Of Purchase Orders"
                frmSupplyPORecords.PurchaseOrderList()
            Case "List Of Goods Receipt"
                frmSupplyGRRecords.GoodsReceiptList()
        End Select
    End Sub

    Sub MainAdd()
        Select Case frmMain.formTitle.Text
            Case "List Of Students"
                With frmStudentInfo
                    frmStudentList.LoadComboBoxData()
                    comboStudentLevelWithIrreg(.cbYearLevel, .lbllabel, .lbllevel)
                    ResetControls(frmStudentInfo)

                    .cbTransferMode.Text = String.Empty
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True

                    .ShowDialog()
                End With
            Case "List Of Academic Years"
                With frmAcademicYear
                    ResetControls(frmAcademicYear)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "Setup Class Schedules"
                With frmClassSched
                    frmClassSchedList.LoadComboBoxData()
                    ResetControls(frmClassSched)
                    '.btnPrev.Visible = True
                    '.btnNext.Visible = True
                    '.slide1.Visible = False
                    '.slide2.Visible = True
                    '.slide3.Visible = False
                    .ShowDialog()
                End With
            Case "List Of Courses"
                With frmCourse
                    ResetControls(frmCourse)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "List Of Day Schedules"
                With frmDaySched
                    ResetControls(frmDaySched)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "List Of Employees"
                With frmEmployee
                    ResetControls(frmEmployee)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True

                    .ShowDialog()
                End With
            Case "List Of Rooms"
                With frmRoom
                    ResetControls(frmRoom)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "List Of Schools"
                With frmSchool
                    ResetControls(frmSchool)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "List Of Class Sections"
                With frmSection
                    fillCombo("SELECT `course_id`, `course_code`, `course_name`, `course_major`, `course_levels`, `course_status`, `course_gr_number` FROM `tbl_course`", .cbCourse, "tbl_course", "course_code", "course_id")
                    comboStudentLevel(.cbYearLevel, .lblLabel, .lblLevel)
                    ResetControls(frmSection)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "List Of Subjects"
                With frmSubject
                    .comboSubjectGroup()
                    ResetControls(frmSubject)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .comboSubjectGroup()
                    .ShowDialog()
                End With
            Case "List Of Curriculum"
                With frmCurriculum
                    fillCombo("SELECT `course_id`, CONCAT(`course_code`,' - ', `course_name`) as Course, `course_major`, `course_levels`, `course_status`, `course_gr_number` FROM `tbl_course`", .cbCourse, "tbl_course", "Course", "course_id")
                    ResetControls(frmCurriculum)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "List Of User Accounts"
                With frmUser
                    ResetControls(frmUser)
                    .btnSave.Visible = True
                    .btnUpdate.Visible = False
                    .txtUsername.Enabled = True
                    .slide4.Visible = True
                    .employeePanel.AutoScroll = True
                    .cbAccountType.Enabled = True
                    .SystemModules()
                    .ShowDialog()
                End With
            Case "List Of Supply Items"
                With frmSupplyItemAdd
                    ResetControls(frmSupplyItemAdd)
                    .cbSupplyType.Enabled = True
                    .txtOpeningStock.Enabled = True
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True

                    .btnSearchBrand.Visible = True
                    .btnSearchCategory.Visible = True
                    .btnSearchSize.Visible = True

                    .CategoryID = 0
                    .SizeID = 0
                    .AutoBarCode()
                    .ShowDialog()
                End With
            Case "List Of Purchase Requests"
                With frmSupplyPurchaseRequest
                    ResetControls(frmSupplyPurchaseRequest)
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "List Of Purchase Orders"
                With frmSupplyPurchaseOrder
                    ResetControls(frmSupplyPurchaseOrder)
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "List Of Goods Receipt"
                With frmSupplyPurchaseGReceipt
                    ResetControls(frmSupplyPurchaseGReceipt)
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
        End Select
    End Sub


    Public Sub dgPanelPadding(ByVal DTG As Object, ByVal PNL As Object)
        If DTG.FirstDisplayedScrollingRowIndex <> -1 Then
            PNL.Padding = New Padding(7, 0, 0, 0)
        Else
            PNL.Padding = New Padding(7, 0, 0, 7)
        End If
    End Sub

    Public Sub UserLogList()
        Try

            frmUserLogs.dgUserLogs.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        If str_role = "Administrator" Then
            sql = "Select CONCAT(t2.ua_first_name, ' ', t2.ua_middle_name, ' ', t2.ua_last_name) as 'Name', DATE_FORMAT(t1.log_date, '%Y-%m-%d') AS 'Date', TIME_FORMAT(t1.log_time, '%h:%i %p') AS 'Time', t1.log_location as 'Location', t1.log_area as 'Area', t1.log_action as 'Action' FROM `tbl_user_logs` t1 JOIN tbl_user_account t2 ON t1.log_user_id = t2.ua_id WHERE (log_action LIKE '%" & frmMain.txtSearch.Text & "%' or  CONCAT(t2.ua_first_name, ' ', t2.ua_middle_name, ' ', t2.ua_last_name) LIKE '%" & frmMain.txtSearch.Text & "%') order by t1.log_id desc limit 500"
        Else
            sql = "Select CONCAT(t2.ua_first_name, ' ', t2.ua_middle_name, ' ', t2.ua_last_name) as 'Name', DATE_FORMAT(t1.log_date, '%Y-%m-%d') AS 'Date', TIME_FORMAT(t1.log_time, '%h:%i %p') AS 'Time', t1.log_location as 'Location', t1.log_area as 'Area', t1.log_action as 'Action' FROM `tbl_user_logs` t1 JOIN tbl_user_account t2 ON t1.log_user_id = t2.ua_id where t1.log_user_id = " & str_userid & " order by t1.log_id desc limit 500"
        End If
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmUserLogs.dgUserLogs.Rows.Add(i, dr.Item("Name").ToString, dr.Item("Date").ToString, dr.Item("Time").ToString, dr.Item("Location").ToString, dr.Item("Area").ToString, dr.Item("Action").ToString)
        End While
        dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmUserLogs.dgUserLogs.Rows.Clear()

        End Try
    End Sub

    Public Sub LibraryStudentList()
        Try

            frmStudentList.dgStudentList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            If frmMain.cbStudentStatus.Text = "Active Students" Then
                sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', s_so_no, s_notes from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where s_active_status = 'Active' and (s_ln like '%" & frmMain.txtSearch.Text & "%' or s_fn like '%" & frmMain.txtSearch.Text & "%' or s_mn like '%" & frmMain.txtSearch.Text & "%' or s_id_no like '%" & frmMain.txtSearch.Text & "%' or course_code like '%" & frmMain.txtSearch.Text & "%' or s_yr_lvl like '%" & frmMain.txtSearch.Text & "%') order by s_id_no asc limit 500"
            Else
                sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', s_so_no, s_notes from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (s_ln like '%" & frmMain.txtSearch.Text & "%' or s_fn like '%" & frmMain.txtSearch.Text & "%' or s_mn like '%" & frmMain.txtSearch.Text & "%' or s_id_no like '%" & frmMain.txtSearch.Text & "%' or course_code like '%" & frmMain.txtSearch.Text & "%' or s_yr_lvl like '%" & frmMain.txtSearch.Text & "%') order by s_id_no asc limit 500"
            End If
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmStudentList.dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("s_so_no").ToString, dr.Item("s_notes").ToString, EditButton, "Edit", ViewButton, "View")
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmStudentList.dgStudentList, frmStudentList.dgPanel)

            If str_role = "Administrator" Or str_role = "Registrar" Then
                frmStudentList.dgStudentList.Columns(9).Visible = True
                frmStudentList.dgStudentList.Columns(10).Visible = True
            Else
                frmStudentList.dgStudentList.Columns(9).Visible = False
                frmStudentList.dgStudentList.Columns(10).Visible = False
            End If

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmStudentList.dgStudentList.Rows.Clear()

        End Try
    End Sub

    Public Sub LibraryCourseList()
        Try

            frmCourseList.dgCourseList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select course_id, course_code, course_name, course_major, course_status from tbl_course where (course_code LIKE '%" & frmMain.txtSearch.Text & "%' or course_name LIKE '%" & frmMain.txtSearch.Text & "%') order by course_name asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
                frmCourseList.dgCourseList.Rows.Add(i, dr.Item("course_id").ToString, dr.Item("course_code").ToString, dr.Item("course_name").ToString, dr.Item("course_major").ToString, dr.Item("course_status").ToString, EditButton, "Edit")
            End While
        dr.Close()
        cn.Close()

        dgPanelPadding(frmCourseList.dgCourseList, frmCourseList.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmCourseList.dgCourseList.Rows.Clear()

        End Try
    End Sub

    Public Sub LibraryAcadList()
        Try

            frmAcademicYearList.dgAcadList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select period_id, (period_name) as 'Period', (period_semester) as 'Semester', (period_status) as 'Status', period_enrollment_status, period_enrollment_ad_status from tbl_period where (period_name LIKE '%" & frmMain.txtSearch.Text & "%' or period_semester LIKE '%" & frmMain.txtSearch.Text & "%') order by `period_name` desc, `period_semester` desc, `period_status` asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmAcademicYearList.dgAcadList.Rows.Add(i, dr.Item("period_id").ToString, dr.Item("Period").ToString, dr.Item("Semester").ToString, dr.Item("period_enrollment_status").ToString, dr.Item("period_enrollment_ad_status").ToString, dr.Item("Status").ToString, EditButton, "Edit")
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmAcademicYearList.dgAcadList, frmAcademicYearList.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmAcademicYearList.dgAcadList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibrarySubjectList()
        Try

            frmSubjectList.dgSubjectList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (b.subject_id) as 'ID', (b.subject_code) as 'Code', (b.subject_description) as 'Description', (b.subject_Type) as 'Type', (b.subject_group) as 'Group', (b.subject_units) as 'Units', CONCAT(a.subject_description,'-',a.subject_code) as 'Prerequisite', (b.subject_active_status) as 'Status' from tbl_subject b LEFT JOIN tbl_subject a ON a.subject_id = b.subject_prerequisite where (b.subject_code LIKE '" & frmMain.txtSearch.Text & "%' or b.subject_description LIKE '" & frmMain.txtSearch.Text & "%') order by b.subject_description asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmSubjectList.dgSubjectList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Group").ToString, dr.Item("Units").ToString, dr.Item("Prerequisite").ToString, dr.Item("Status").ToString, EditButton, "Edit")
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmSubjectList.dgSubjectList, frmSubjectList.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmSubjectList.dgSubjectList.Rows.Clear()

        End Try
    End Sub

    Public Sub LibrarySubjectListModule()
        Try

            frmSubject.dgSubjectList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (b.subject_id) as 'ID', (b.subject_code) as 'Code', (b.subject_description) as 'Description', (b.subject_Type) as 'Type', (b.subject_group) as 'Group', (b.subject_units) as 'Units', CONCAT(a.subject_description,'-',a.subject_code) as 'Prerequisite', (b.subject_active_status) as 'Status' from tbl_subject b JOIN tbl_subject a ON a.subject_id = b.subject_prerequisite where b.subject_active_status = 'Active' and (b.subject_code LIKE '" & frmSubject.txtSearch.Text & "%' or b.subject_description LIKE '" & frmSubject.txtSearch.Text & "%') order by b.subject_description asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmSubject.dgSubjectList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Group").ToString, dr.Item("Units").ToString, dr.Item("Prerequisite").ToString, dr.Item("Status").ToString)
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmSubject.dgSubjectList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibrarySectionList()
        Try

            frmSectionList.dgSectionList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (cb_id) as 'ID', (cb_code) as 'Code', (cb_description) as 'Description' from tbl_class_block where (cb_code LIKE '%" & frmMain.txtSearch.Text & "%' or cb_description LIKE '%" & frmMain.txtSearch.Text & "%') order by cb_description asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmSectionList.dgSectionList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, EditButton, "Edit")
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmSectionList.dgSectionList, frmSectionList.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmSectionList.dgSectionList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibraryDaySchedList()
        Try

            frmDaySchedList.dgDaySchedList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (ds_id) as 'ID', (ds_code) as 'Code', (ds_description) as 'Description' from tbl_day_schedule where (ds_code LIKE '%" & frmMain.txtSearch.Text & "%' or ds_description LIKE '%" & frmMain.txtSearch.Text & "%') order by ds_description asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmDaySchedList.dgDaySchedList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, EditButton, "Edit")
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmDaySchedList.dgDaySchedList, frmSectionList.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmDaySchedList.dgDaySchedList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibraryRoomList()
        Try

            frmRoomList.dgRoomList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select room_id as 'ID', (room_code) as 'Code', (room_description) as 'Description', (is_active) as 'Status', (capacity) as 'Capacity' from tbl_room where (room_code LIKE '%" & frmMain.txtSearch.Text & "%' or room_description LIKE '%" & frmMain.txtSearch.Text & "%') order by room_description asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmRoomList.dgRoomList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Capacity").ToString, dr.Item("Status").ToString, EditButton, "Edit")
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmRoomList.dgRoomList, frmSectionList.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmRoomList.dgRoomList.Rows.Clear()

        End Try
    End Sub

    Public Sub LibrarySchoolList()
        Try

            frmSchoolList.dgSchoolList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (schl_id) as 'ID', (schl_code) as 'Code', (schl_name) as 'School Name', (schl_address) as 'School Address' from tbl_schools where (schl_code LIKE '%" & frmMain.txtSearch.Text & "%' or schl_name LIKE '%" & frmMain.txtSearch.Text & "%') order by schl_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmSchoolList.dgSchoolList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("School Name").ToString, dr.Item("School Address").ToString, EditButton, "Edit")
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmSchoolList.dgSchoolList, frmSchoolList.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmSchoolList.dgSchoolList.Rows.Clear()

        End Try
    End Sub

    Public Sub LibraryEmployeeList()
        Try

            frmEmployeeList.dgEmployeeList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select emp_id, (emp_code) as 'BIO No.', (emp_Last_name) as 'Last Name', (emp_first_name) as 'First Name', (emp_middle_name) as 'Middle Name', (position_id) as 'Position', (emp_status) as 'Status', required_subject_load as 'Required Subject Load (Units)' from tbl_employee where (emp_code LIKE '%" & frmMain.txtSearch.Text & "%' or emp_Last_name LIKE '%" & frmMain.txtSearch.Text & "%' or emp_first_name LIKE '%" & frmMain.txtSearch.Text & "%' or emp_middle_name LIKE '%" & frmMain.txtSearch.Text & "%') order by emp_Last_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmEmployeeList.dgEmployeeList.Rows.Add(i, dr.Item("emp_id").ToString, dr.Item("BIO No.").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, "", dr.Item("Status").ToString, dr.Item("Required Subject Load (Units)").ToString, EditButton, "Edit")
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmEmployeeList.dgEmployeeList, frmEmployeeList.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmEmployeeList.dgEmployeeList.Rows.Clear()

        End Try
    End Sub

    Public Sub LibraryClassSchedList()
        Try

            frmClassSchedList.dgClassSchedList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells
            frmClassSchedList.dgClassSchedList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "SELECT t1.`class_schedule_id`, t1.`cb_code`, t1.`subject_code`, t1.`subject_description`, t1.`subject_units`, t1.`ds_code`, t1.`time_start_schedule`, t1.`time_end_schedule`, t1.`room_code`, t1.`Instructor`, t1.`population`, t1.`csperiod_id`, t1.`is_active` FROM `classschedulelist` t1 where t1.csperiod_id = " & acadID & " and (t1.cb_code LIKE '%" & frmMain.txtSearch.Text & "%' or t1.subject_code LIKE '%" & frmMain.txtSearch.Text & "%' or t1.subject_description LIKE '%" & frmMain.txtSearch.Text & "%' or t1.Instructor LIKE '%" & frmMain.txtSearch.Text & "%') limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmClassSchedList.dgClassSchedList.Rows.Add(i, dr.Item("class_schedule_id").ToString, dr.Item("cb_code").ToString, dr.Item("subject_code").ToString, dr.Item("subject_description").ToString, dr.Item("subject_units").ToString, dr.Item("ds_code").ToString, dr.Item("time_start_schedule").ToString, dr.Item("time_end_schedule").ToString, dr.Item("room_code").ToString, dr.Item("Instructor").ToString, dr.Item("population").ToString, "👁", dr.Item("csperiod_id").ToString, dr.Item("is_active").ToString, EditButton, "Edit")
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmClassSchedList.dgClassSchedList, frmClassSchedList.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmClassSchedList.dgClassSchedList.Rows.Clear()

        End Try
    End Sub

    Public Sub LibraryUserList()
        Try

            frmUserList.dgUserList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "SELECT `ua_id` as 'ID', `ua_user_name` as 'Username', CONCAT(`ua_first_name`, ' ',`ua_middle_name`, ' ', `ua_last_name`) as 'Name', `ua_account_type` as 'Role' FROM `tbl_user_account` where CONCAT(`ua_first_name`, ' ',`ua_middle_name`, ' ', `ua_last_name`) LIKE '%" & frmMain.txtSearch.Text & "%' order by Name asc"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmUserList.dgUserList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Username").ToString, dr.Item("Name").ToString, dr.Item("Role").ToString, EditButton, "Edit")
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmUserList.dgUserList, frmUserList.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmUserList.dgUserList.Rows.Clear()
        End Try
    End Sub

    Public Sub GradingClassSchedList()
        Try

            frmClassGradeEditor.dgClassSchedList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.None
            frmClassGradeEditor.dgClassSchedList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "SELECT t1.`class_schedule_id`, t1.`cb_code`, t1.`subject_code`, t1.`subject_description`, t1.`subject_units`, t1.`ds_code`, t1.`time_start_schedule`, t1.`time_end_schedule`, t1.`room_code`, t1.`Instructor`, t1.`population`, t1.`csperiod_id`, t1.`is_active`, t1.subject_id FROM `classschedulelist` t1 where t1.csperiod_id = " & CInt(frmClassGradeEditor.cbAcademicYear.SelectedValue) & " and (t1.cb_code LIKE '%" & frmClassGradeEditor.txtSearch.Text & "%' or t1.subject_code LIKE '%" & frmClassGradeEditor.txtSearch.Text & "%' or t1.subject_description LIKE '%" & frmClassGradeEditor.txtSearch.Text & "%' or t1.Instructor LIKE '%" & frmClassGradeEditor.txtSearch.Text & "%') limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmClassGradeEditor.dgClassSchedList.Rows.Add(i, dr.Item("class_schedule_id").ToString, dr.Item("cb_code").ToString, dr.Item("subject_code").ToString, dr.Item("subject_description").ToString, dr.Item("subject_units").ToString, dr.Item("ds_code").ToString, dr.Item("time_start_schedule").ToString, dr.Item("time_end_schedule").ToString, dr.Item("room_code").ToString, dr.Item("Instructor").ToString, dr.Item("population").ToString, "👁", dr.Item("csperiod_id").ToString, dr.Item("is_active").ToString, dr.Item("subject_id").ToString)
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmClassGradeEditor.dgClassSchedList.Rows.Clear()
        End Try
    End Sub

    Function CountEnrolled(ByVal classID As Integer) As Integer
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT ifnull(COUNT(sg_class_id),0) as 'Enrolled' FROM tbl_students_grades where sg_class_id = " & classID & "", cn)
        CountEnrolled = CInt(cm.ExecuteScalar)
        cn.Close()
        Return CountEnrolled
    End Function

    Sub classSchedAcademicYear()
        frmMain.cmbAcad.Items.Clear()
        Dim sql As String
        sql = "SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM tbl_class_schedule t1 JOIN tbl_period t2 ON t1.csperiod_id = t2.period_id group by t1.csperiod_id order by `period_name` desc, `period_status` ASC, `period_semester` desc"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            frmMain.cmbAcad.Items.Add(dr("PERIOD").ToString())
        End While
        dr.Close()
        cn.Close()
    End Sub

    Sub academicYearID()
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT period_id as PERIOD from tbl_period where CONCAT(period_name,'-',period_semester) = '" & frmMain.cmbAcad.Text & "'", cn)
        acadID = cm.ExecuteScalar
        cn.Close()
    End Sub

    Sub ShowPhotoEmployee(ByVal empID As Integer)
        'Try
        cn.Close()
        cn.Open()
        Dim sql = "select emp_photo from tbl_employee where emp_id = " & empID & ""
        da = New MySqlDataAdapter(sql, cn)
        cm = New MySqlCommand(sql)
        da.Fill(ds, "emp_photo")
        arrImage = ds.Tables("emp_photo").Rows(0)(0)

        Dim mstream As New System.IO.MemoryStream(arrImage)
        frmEmployee.empPhoto.Image = Image.FromStream(mstream)

        ds = New DataSet
        cn.Close()
        'Catch ex As Exception
        'End Try
    End Sub

#End Region
#Region "Class Schedule"

    Sub ClassScheduleSearch()
        Select Case frmClassSched.frmTitle.Text
            Case "Search Section"
                LibraryClassSectionList()
            Case "Search Curriculum"
                LibraryClassCurList()
            Case "Search Subject"
                LibraryClassSubjectList()
            Case "Search Day Schedule"
                LibraryClassDaySchedList()
            Case "Search Instructor"
                LibraryClassEmployeeList()
            Case "Search Room"
                LibraryClassRoomList()
            Case "Search Academic Year"
                LibraryClassAcadList()
        End Select
    End Sub

    Public Sub LibraryClassSectionList()
        Try
            Dim CurrCourseID As Integer = 0
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT curr_course_id from tbl_curriculum where curriculum_id = " & CInt(frmClassSched.ClassCurID) & "", cn)
            CurrCourseID = CInt(cm.ExecuteScalar)
            cn.Close()

            frmClassSched.dgSectionList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (cb_id) as 'ID', (cb_code) as 'Code', (cb_description) as 'Description' from tbl_class_block where cb_course_id = " & CurrCourseID & " and (cb_code LIKE '%" & frmClassSched.txtSearch.Text & "%' or cb_description LIKE '%" & frmClassSched.txtSearch.Text & "%') order by cb_description asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmClassSched.dgSectionList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmClassSched.dgSectionList, frmClassSched.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmClassSched.dgSectionList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibraryClassEmployeeList()
        Try


            frmClassSched.dgEmployeeList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select emp_id, (emp_code) as 'BIO No.', (emp_Last_name) as 'Last Name', (emp_first_name) as 'First Name', (emp_middle_name) as 'Middle Name', (position_id) as 'Position', (emp_status) as 'Status', required_subject_load as 'Required Subject Load (Units)' from tbl_employee where (emp_code LIKE '%" & frmClassSched.txtSearch.Text & "%' or emp_Last_name LIKE '%" & frmClassSched.txtSearch.Text & "%' or emp_first_name LIKE '%" & frmClassSched.txtSearch.Text & "%' or emp_middle_name LIKE '%" & frmClassSched.txtSearch.Text & "%') order by emp_Last_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmClassSched.dgEmployeeList.Rows.Add(i, dr.Item("emp_id").ToString, dr.Item("BIO No.").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, "", dr.Item("Status").ToString, dr.Item("Required Subject Load (Units)").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmClassSched.dgEmployeeList, frmClassSched.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmClassSched.dgEmployeeList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibraryClassDaySchedList()
        Try


            frmClassSched.dgDaySchedList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (ds_id) as 'ID', (ds_code) as 'Code', (ds_description) as 'Description' from tbl_day_schedule where (ds_code LIKE '%" & frmClassSched.txtSearch.Text & "%' or ds_description LIKE '%" & frmClassSched.txtSearch.Text & "%') order by ds_description asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmClassSched.dgDaySchedList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmClassSched.dgDaySchedList, frmClassSched.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmClassSched.dgDaySchedList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibraryClassRoomList()
        Try


            frmClassSched.dgRoomList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select room_id as 'ID', (room_code) as 'Code', (room_description) as 'Description', (is_active) as 'Status', (capacity) as 'Capacity' from tbl_room where (room_code LIKE '%" & frmClassSched.txtSearch.Text & "%' or room_description LIKE '%" & frmClassSched.txtSearch.Text & "%') order by room_description asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmClassSched.dgRoomList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Capacity").ToString, dr.Item("Status").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmClassSched.dgRoomList, frmClassSched.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmClassSched.dgRoomList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibraryClassAcadList()
        Try


            frmClassSched.dgAcadList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select period_id, (period_name) as 'Period', (period_semester) as 'Semester', (period_status) as 'Status', period_enrollment_status, period_enrollment_ad_status from tbl_period where (period_name LIKE '%" & frmClassSched.txtSearch.Text & "%' or period_semester LIKE '%" & frmClassSched.txtSearch.Text & "%') order by `period_name` desc, `period_semester` desc, `period_status` asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmClassSched.dgAcadList.Rows.Add(i, dr.Item("period_id").ToString, dr.Item("Period").ToString, dr.Item("Semester").ToString, dr.Item("period_enrollment_status").ToString, dr.Item("period_enrollment_ad_status").ToString, dr.Item("Status").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmClassSched.dgAcadList, frmClassSched.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmClassSched.dgAcadList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibraryClassCurList()
        Try

            frmClassSched.dgCurList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select curriculum_id, (curriculum_code) as 'Curriculum', (course_code) as 'Course', (datetime_created) as 'Created', CONCAT(ua_first_name,' ',ua_middle_name,' ',ua_last_name) as 'Created By', (is_active) as 'Status', (is_current) as 'Is Current?' from (tbl_curriculum JOIN tbl_course) JOIN tbl_user_account where tbl_curriculum.prepared_by_id = tbl_user_account.ua_id and tbl_curriculum.curr_course_id = tbl_course.course_id and curriculum_code LIKE '%" & frmClassSched.txtSearch.Text & "%' order by datetime_created asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmClassSched.dgCurList.Rows.Add(i, dr.Item("curriculum_id").ToString, dr.Item("Curriculum").ToString, dr.Item("Course").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmClassSched.dgCurList, frmClassSched.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmClassSched.dgCurList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibraryClassSubjectList()
        Try
            Dim SectionYearLevel As String
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT cb_year_level from tbl_class_block where cb_id = " & CInt(frmClassSched.ClassSectionID) & "", cn)
            SectionYearLevel = cm.ExecuteScalar
            cn.Close()

            frmClassSched.dgSubjectList.Rows.Clear()
            Dim curID As Integer = 0
            Try
                curID = CInt(frmClassSched.ClassCurID)
            Catch ex As Exception
                curID = 0
            End Try
            Dim i As Integer
            Dim sql As String
        sql = "SELECT t1.subjectID 'SubjectID', CONCAT(t2.subject_code, ' - ', t2.subject_description) as Subject, t1.subjectGroup as 'Group', t2.subject_type as 'Type', t2.subject_units as 'Units' FROM tbl_curriculum_subjects t1 JOIN tbl_subject t2 ON t1.subjectID = t2.subject_id where t1.curriculumID = " & curID & " and t1.yearLevel = '" & SectionYearLevel & "' and CONCAT(t2.subject_code, ' - ', t2.subject_description) LIKE '%" & frmClassSched.txtSearch.Text & "%' order by CONCAT(t2.subject_code, ' - ', t2.subject_description) asc limit 500"
        cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
            frmClassSched.dgSubjectList.Rows.Add(i, dr.Item("SubjectID").ToString, dr.Item("Subject").ToString, dr.Item("SubGroup").ToString, dr.Item("Type").ToString, dr.Item("Units").ToString)
        End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmClassSched.dgSubjectList, frmClassSched.dgPanel)
        Catch ex As Exception
        dr.Close()
        cn.Close()
        frmClassSched.dgSubjectList.Rows.Clear()
        End Try
    End Sub
    Public Sub AddSubjectSchedule()
        Try
            Dim isFound As Boolean = False
            Dim isFound2 As Boolean = False

            Dim DaySchedCode As String = ""
            Dim SubjectCode As String = ""
            Dim classCode As String = ""
            Dim subjectTimeStart As String = ""
            Dim subjectTimeEnd As String = ""

            With frmClassSched


                For Each row As DataGridViewRow In .dgSection.Rows

                    DaySchedCode = row.Cells(5).Value

                    Dim a As New List(Of String)(DaySchedCode.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))
                    Dim b As New List(Of String)(.cbDaySched.Text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))
                    Dim dcodedcontain = a.Intersect(b).Count() > 0

                    SubjectCode = "" & row.Cells(2).Value & " - " & row.Cells(3).Value & ""
                    subjectTimeStart = row.Cells(6).Value
                    subjectTimeEnd = row.Cells(7).Value

                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT * FROM tbl_class_schedule WHERE csperiod_id = " & .ClassAcadID & " and class_block_id = " & .ClassSectionID & " and (STR_TO_DATE('" & subjectTimeStart & "','%h:%i:%s %p') < STR_TO_DATE('" & row.Cells(7).Value & "','%h:%i:%s %p') AND STR_TO_DATE('" & subjectTimeEnd & "','%h:%i:%s %p') > STR_TO_DATE('" & row.Cells(6).Value & "','%h:%i:%s %p'))", cn)
                    Dim sdr3 As MySqlDataReader = cm.ExecuteReader()
                    'If .cbSubject.Text.Contains("PE 1 -") Or .cbSubject.Text.Contains("PE 2 -") Or .cbSubject.Text.Contains("PE 3 -") Or .cbSubject.Text.Contains("PE 4 -") Or .cbSubject.Text.Contains("NSTP") Or .cbSubject.Text.Contains("PE1 -") Or .cbSubject.Text.Contains("PE2 -") Or .cbSubject.Text.Contains("PE3 -") Or .cbSubject.Text.Contains("PE4 -") Then
                    '    sdr3.Dispose()
                    '    isFound = False
                    '    isFound2 = False
                    'ElseIf .cbSubject.Text.Contains("PATHFit") Then
                    '    sdr3.Dispose()
                    '    isFound = False
                    '    isFound2 = False
                    'Else
                    If .cbSubject.Text = SubjectCode Then
                        sdr3.Dispose()
                        isFound = True
                        MessageBox.Show("Subject " & .cbSubject.Text & " is conflict with subject " & SubjectCode & ". Subject " & .cbSubject.Text & " already existed.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ElseIf .start_time.Text = row.Cells(6).Value And .end_time.Text = row.Cells(7).Value And .cbDaySched.Text = row.Cells(5).Value Then
                        sdr3.Dispose()
                        isFound = True
                        MessageBox.Show("Time schedule " & .start_time.Text & " - " & .end_time.Text & " - " & .cbDaySched.Text & " already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit For
                    ElseIf (sdr3.Read() = True) And dcodedcontain = True Then
                        isFound2 = True
                        MessageBox.Show("Subject " & .cbSubject.Text & " is conflict with subject " & SubjectCode & ".", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        sdr3.Dispose()
                        Exit For
                    End If
                    cn.Close()
                Next

                Dim isFound_ins As Boolean = False
                Dim isFound_ins2 As Boolean = False

                For Each row As DataGridViewRow In .dgInstructor.Rows

                    DaySchedCode = row.Cells(5).Value
                    Dim a As New List(Of String)(DaySchedCode.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))

                    Dim b As New List(Of String)(.cbDaySched.Text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))

                    Dim dcodedcontain = a.Intersect(b).Count() > 0

                    SubjectCode = "" & row.Cells(2).Value & " - " & row.Cells(3).Value & ""

                    subjectTimeStart = row.Cells(6).Value
                    subjectTimeEnd = row.Cells(7).Value

                    cn.Open()
                    cn.Close()
                    cm = New MySqlCommand("SELECT * FROM tbl_class_schedule WHERE csperiod_id = " & CInt(.cbAcademicYear.SelectedValue) & " and csemp_id = " & CInt(.ClassInstructorID) & " and (STR_TO_DATE('" & subjectTimeStart & "','%h:%i:%s %p') < STR_TO_DATE('" & row.Cells(7).Value & "','%h:%i:%s %p') AND STR_TO_DATE('" & subjectTimeEnd & "','%h:%i:%s %p') > STR_TO_DATE('" & row.Cells(6).Value & "','%h:%i:%s %p'))", cn)
                    Dim sdr3 As MySqlDataReader = cm.ExecuteReader()

                    'If .cbSubject.Text.Contains("PE 1") Or .cbSubject.Text.Contains("PE 2") Or .cbSubject.Text.Contains("PE 3") Or .cbSubject.Text.Contains("PE 4") Or .cbSubject.Text.Contains("NSTP") Then
                    '    sdr3.Dispose()
                    '    isFound_ins = False
                    '    isFound_ins2 = False
                    'ElseIf .cbSubject.Text.Contains("PATHFit") Or .cbSubject.Text.Contains("PATHFIT") Then
                    '    sdr3.Dispose()
                    '    isFound_ins = False
                    '    isFound_ins2 = False
                    'Else
                    If .CheckBox_skip.Checked = True Then
                        sdr3.Dispose()
                        isFound = False
                        isFound2 = False
                    ElseIf .cbClassStatus.Text = "Merged" Then
                        sdr3.Dispose()
                        isFound_ins = False
                        isFound_ins2 = False
                    ElseIf .cbInstructor.Text = String.Empty Or .ClassInstructorID = 0 Then
                        isFound_ins = False
                        isFound_ins2 = False
                        sdr3.Dispose()
                    ElseIf .start_time.Text = row.Cells(6).Value And .end_time.Text = row.Cells(7).Value And .cbDaySched.Text = row.Cells(5).Value Then
                        sdr3.Dispose()
                        isFound_ins = True
                        MessageBox.Show("Time schedule " & .start_time.Text & " - " & .end_time.Text & " - " & .cbDaySched.Text & " already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit For
                    ElseIf (sdr3.Read() = True) And dcodedcontain = True Then
                        isFound_ins2 = True
                        MessageBox.Show("Subject " & .cbSubject.Text & " is conflict with instructor load in subject " & SubjectCode & ".", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        sdr3.Dispose()
                        Exit For
                    End If
                    cn.Close()
                Next


                If (isFound) Then

                ElseIf (isFound2) Then

                ElseIf (isFound_ins) Then

                ElseIf (isFound_ins2) Then

                Else
                    cn.Open()
                    cn.Close()
                    classCode = .cbSection.Text + " - " + .cbAcademicYear.Text

                    Dim Date_Today As DateTime = Convert.ToDateTime(DateToday)

                    Dim str As String
                    str = "INSERT INTO tbl_class_schedule (class_schedule_code, class_schedule_curriculum, cssubject_id, days_schedule, time_start_schedule, time_end_schedule, class_block_id, csroom_id, csemp_id, population, csperiod_id, is_active, date_created, exam_link, cs_is_petition, cs_amount, subject_load_status, class_adviser, passing_grade, class_status, skipconflictcheck) values (@class_schedule_code, @class_schedule_curriculum, @cssubject_id, @days_schedule, @time_start_schedule, @time_end_schedule, @class_block_id, @csroom_id, @csemp_id, @population, @csperiod_id, @is_active, @date_created, @exam_link, @is_petition, @amount, @subject_load_status, @class_adviser, @passing_grade, @class_status, @skipconflictcheck)"
                    cm = New MySqlCommand(str, cn)
                    cm.Parameters.AddWithValue("@class_schedule_code", classCode)
                    cm.Parameters.AddWithValue("@class_schedule_curriculum", CInt(.ClassCurID))
                    cm.Parameters.AddWithValue("@cssubject_id", CInt(.ClassSubjectID))
                    cm.Parameters.AddWithValue("@days_schedule", CInt(.ClassDaySchedID))
                    cm.Parameters.AddWithValue("@time_start_schedule", .start_time.Text.ToUpper)
                    cm.Parameters.AddWithValue("@time_end_schedule", .end_time.Text.ToUpper)
                    cm.Parameters.AddWithValue("@class_block_id", CInt(.ClassSectionID))
                    cm.Parameters.AddWithValue("@csroom_id", CInt(.ClassRoomID))
                    cm.Parameters.AddWithValue("@csemp_id", CInt(.ClassInstructorID))
                    cm.Parameters.AddWithValue("@population", .txtPopulation.Text)
                    cm.Parameters.AddWithValue("@csperiod_id", CInt(.cbAcademicYear.SelectedValue))
                    cm.Parameters.AddWithValue("@is_active", .cbStatus.Text)
                    cm.Parameters.AddWithValue("@date_created", Date_Today)
                    cm.Parameters.AddWithValue("@exam_link", "")
                    cm.Parameters.AddWithValue("@is_petition", .CbPetition.Text)

                    Dim x As Double
                    Try
                        x = .txtAmount.Text
                    Catch ex As Exception
                        x = 0
                    End Try
                    cm.Parameters.AddWithValue("@amount", x)

                    If .cbLoadStatus.Text = "Normal" Then
                        cm.Parameters.AddWithValue("@subject_load_status", 0)
                    ElseIf .cbLoadStatus.Text = "Overload" Then
                        cm.Parameters.AddWithValue("@subject_load_status", 1)
                    ElseIf .cbLoadStatus.Text = "Half Overload" Then
                        cm.Parameters.AddWithValue("@subject_load_status", 0.5)
                    ElseIf .cbLoadStatus.Text = "Honorarium" Then
                        cm.Parameters.AddWithValue("@subject_load_status", 0.2)
                    End If

                    cm.Parameters.AddWithValue("@class_adviser", 0)
                    cm.Parameters.AddWithValue("@passing_grade", .cbPassingGrade.Text)
                    cm.Parameters.AddWithValue("@class_status", .cbClassStatus.Text)
                    Dim skipcheck As Integer
                    If .CheckBox_skip.Checked = False Then
                        skipcheck = 0
                    ElseIf .CheckBox_skip.Checked = True Then
                        skipcheck = 1
                    End If
                    cm.Parameters.AddWithValue("@skipconflictcheck", skipcheck)
                    cm.ExecuteNonQuery()
                    cm.Dispose()
                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    MsgBox("New class schedule has been successfully saved.", vbInformation, "")
                    UserActivity("Added a class schedule to Academic Year " & .cbAcademicYear.Text & " for " & .cbSection.Text & " with subject " & .cbSubject.Text & ".", "LIBRARY CLASS SCHEDULE")
                    cn.Close()
                    SubjectListPerSection()
                    SubjectListPerInstructor()
                    .Close()
                End If
            End With
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Public Sub UpdateSubjectSchedule()
        Dim isFound As Boolean = False
        Dim isFound2 As Boolean = False

        Dim DaySchedCode As String = ""
        Dim SubjectCode As String = ""
        Dim classCode As String = ""
        Dim subjectTimeStart As String = ""
        Dim subjectTimeEnd As String = ""

        With frmClassSched
            For Each row As DataGridViewRow In .dgSection.Rows
                DaySchedCode = row.Cells(5).Value
                Dim a As New List(Of String)(DaySchedCode.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))
                Dim b As New List(Of String)(.cbDaySched.Text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))
                Dim dcodedcontain = a.Intersect(b).Count() > 0
                SubjectCode = "" & row.Cells(2).Value & " - " & row.Cells(3).Value & ""
                subjectTimeStart = row.Cells(6).Value
                subjectTimeEnd = row.Cells(7).Value

                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT * FROM tbl_class_schedule WHERE csperiod_id = " & .ClassAcadID & " and class_block_id = " & .ClassSectionID & " and (STR_TO_DATE('" & subjectTimeStart & "','%h:%i:%s %p') < STR_TO_DATE('" & row.Cells(7).Value & "','%h:%i:%s %p') AND STR_TO_DATE('" & subjectTimeEnd & "','%h:%i:%s %p') > STR_TO_DATE('" & row.Cells(6).Value & "','%h:%i:%s %p'))", cn)
                Dim sdr3 As MySqlDataReader = cm.ExecuteReader()
                'If .cbSubject.Text.Contains("PE 1 -") Or .cbSubject.Text.Contains("PE 2 -") Or .cbSubject.Text.Contains("PE 3 -") Or .cbSubject.Text.Contains("PE 4 -") Or .cbSubject.Text.Contains("NSTP") Or .cbSubject.Text.Contains("PE1 -") Or .cbSubject.Text.Contains("PE2 -") Or .cbSubject.Text.Contains("PE3 -") Or .cbSubject.Text.Contains("PE4 -") Then
                '    sdr3.Dispose()
                '    isFound = False
                '    isFound2 = False
                'ElseIf .cbSubject.Text.Contains("PATHFit") Then
                '    sdr3.Dispose()
                '    isFound = False
                '    isFound2 = False
                'Else
                If .cbSubject.Text = SubjectCode Then
                    sdr3.Dispose()
                    isFound = True
                    MessageBox.Show("Subject " & .cbSubject.Text & " is conflict with subject " & SubjectCode & ". Subject " & .cbSubject.Text & " already existed.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ElseIf .start_time.Text = row.Cells(6).Value And .end_time.Text = row.Cells(7).Value And .cbDaySched.Text = row.Cells(5).Value Then
                    sdr3.Dispose()
                    isFound = True
                    MessageBox.Show("Time schedule " & .start_time.Text & " - " & .end_time.Text & " - " & .cbDaySched.Text & " already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit For
                ElseIf (sdr3.Read() = True) And dcodedcontain = True Then
                    isFound2 = True
                    MessageBox.Show("Subject " & .cbSubject.Text & " is conflict with subject " & SubjectCode & ".", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    sdr3.Dispose()
                    Exit For
                End If
                cn.Close()
            Next

            Dim isFound_ins As Boolean = False
            Dim isFound_ins2 As Boolean = False

            For Each row As DataGridViewRow In .dgInstructor.Rows

                DaySchedCode = row.Cells(5).Value
                Dim a As New List(Of String)(DaySchedCode.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))

                Dim b As New List(Of String)(.cbDaySched.Text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries))

                Dim dcodedcontain = a.Intersect(b).Count() > 0

                SubjectCode = "" & row.Cells(2).Value & " - " & row.Cells(3).Value & ""

                subjectTimeStart = row.Cells(6).Value
                subjectTimeEnd = row.Cells(7).Value

                cn.Open()
                cn.Close()
                cm = New MySqlCommand("SELECT * FROM tbl_class_schedule WHERE csperiod_id = " & CInt(.cbAcademicYear.SelectedValue) & " and csemp_id = " & CInt(.ClassInstructorID) & " and (STR_TO_DATE('" & subjectTimeStart & "','%h:%i:%s %p') < STR_TO_DATE('" & row.Cells(7).Value & "','%h:%i:%s %p') AND STR_TO_DATE('" & subjectTimeEnd & "','%h:%i:%s %p') > STR_TO_DATE('" & row.Cells(6).Value & "','%h:%i:%s %p'))", cn)
                Dim sdr3 As MySqlDataReader = cm.ExecuteReader()

                'If .cbSubject.Text.Contains("PE 1") Or .cbSubject.Text.Contains("PE 2") Or .cbSubject.Text.Contains("PE 3") Or .cbSubject.Text.Contains("PE 4") Or .cbSubject.Text.Contains("NSTP") Then
                '    sdr3.Dispose()
                '    isFound_ins = False
                '    isFound_ins2 = False
                'ElseIf .cbSubject.Text.Contains("PATHFit") Or .cbSubject.Text.Contains("PATHFIT") Then
                '    sdr3.Dispose()
                '    isFound_ins = False
                '    isFound_ins2 = False
                If .CheckBox_skip.Checked = True Then
                    sdr3.Dispose()
                    isFound = False
                    isFound2 = False
                ElseIf .cbClassStatus.Text = "Merged" Then
                    sdr3.Dispose()
                    isFound_ins = False
                    isFound_ins2 = False
                ElseIf .cbInstructor.Text = String.Empty Or .ClassInstructorID = 0 Then
                    isFound_ins = False
                    isFound_ins2 = False
                    sdr3.Dispose()
                ElseIf .start_time.Text = row.Cells(6).Value And .end_time.Text = row.Cells(7).Value And .cbDaySched.Text = row.Cells(5).Value Then
                    sdr3.Dispose()
                    isFound_ins = True
                    MessageBox.Show("Time schedule " & .start_time.Text & " - " & .end_time.Text & " - " & .cbDaySched.Text & " already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit For
                ElseIf (sdr3.Read() = True) And dcodedcontain = True Then
                    isFound_ins2 = True
                    MessageBox.Show("Subject " & .cbSubject.Text & " is conflict with instructor load in subject " & SubjectCode & ".", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    sdr3.Dispose()
                    Exit For
                End If
                cn.Close()
            Next

            If (isFound) Then
            ElseIf (isFound2) Then
            ElseIf (isFound_ins) Then
            ElseIf (isFound_ins2) Then
            Else
                UpdateClassSchedule()
            End If
        End With
    End Sub

    Public Sub UpdateClassSchedule()
        cn.Close()
        cn.Open()
        Dim update_subjectschedule_transaction As MySqlTransaction = cn.BeginTransaction()
        Try


            With frmClassSched
                Dim classCode As String = .cbSection.Text + " - " + .cbAcademicYear.Text

                Using update_subjectschedule_Cmd As MySqlCommand = cn.CreateCommand()
                    update_subjectschedule_Cmd.Transaction = update_subjectschedule_transaction
                    update_subjectschedule_Cmd.CommandText = "UPDATE tbl_class_schedule SET class_schedule_code=@class_schedule_code, class_schedule_curriculum=@class_schedule_curriculum, cssubject_id=@cssubject_id, days_schedule=@days_schedule, time_start_schedule=@time_start_schedule, time_end_schedule=@time_end_schedule, class_block_id=@class_block_id, csroom_id=@csroom_id, csemp_id=@csemp_id, population=@population, csperiod_id=@csperiod_id, is_active=@is_active, exam_link=@exam_link, cs_is_petition=@is_petition, cs_amount=@amount, subject_load_status=@load_status, class_adviser=@class_adviser, passing_grade=@passing_grade, class_status=@class_status, skipconflictcheck=@skipconflictcheck where class_schedule_id = " & .ClassID & ""
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@class_schedule_code", classCode)
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@class_schedule_curriculum", CInt(.ClassCurID))
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@cssubject_id", CInt(.ClassSubjectID))
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@days_schedule", CInt(.ClassDaySchedID))
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@time_start_schedule", .start_time.Text.ToUpper)
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@time_end_schedule", .end_time.Text.ToUpper)
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@class_block_id", CInt(.ClassSectionID))
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@csroom_id", CInt(.ClassRoomID))
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@csemp_id", CInt(.ClassInstructorID))
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@population", .txtPopulation.Text)
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@csperiod_id", CInt(.cbAcademicYear.SelectedValue))
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@is_active", CInt(.cbStatus.SelectedValue))
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@exam_link", "")
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@is_petition", CInt(.CbPetition.SelectedValue))

                    Dim x As Double
                    x = .txtAmount.Text
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@amount", x)
                    If .cbLoadStatus.Text = "Normal" Then
                        update_subjectschedule_Cmd.Parameters.AddWithValue("@load_status", 0)
                    ElseIf .cbLoadStatus.Text = "Overload" Then
                        update_subjectschedule_Cmd.Parameters.AddWithValue("@load_status", 1)
                    ElseIf .cbLoadStatus.Text = "Half Overload" Then
                        update_subjectschedule_Cmd.Parameters.AddWithValue("@load_status", 0.5)
                    ElseIf .cbLoadStatus.Text = "Honorarium" Then
                        update_subjectschedule_Cmd.Parameters.AddWithValue("@load_status", 0.2)
                    End If

                    update_subjectschedule_Cmd.Parameters.AddWithValue("@class_adviser", .CbPetition.Text)
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@passing_grade", .cbPassingGrade.Text)
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@class_status", .cbClassStatus.Text)

                    Dim skipcheck As Integer
                    If .CheckBox_skip.Checked = False Then
                        skipcheck = 0
                    ElseIf .CheckBox_skip.Checked = True Then
                        skipcheck = 1
                    End If
                    update_subjectschedule_Cmd.Parameters.AddWithValue("@skipconflictcheck", skipcheck)
                    update_subjectschedule_Cmd.ExecuteNonQuery()
                End Using

                If .ClassPreviousSubjectID = CInt(.ClassSubjectID) Then
                Else
                    Using update_studentgrades_Cmd As MySqlCommand = cn.CreateCommand()
                        update_studentgrades_Cmd.Transaction = update_subjectschedule_transaction
                        update_studentgrades_Cmd.CommandText = "Update tbl_students_grades set sg_subject_id = @1 where sg_class_id = @2 and sg_period_id = @3"
                        update_studentgrades_Cmd.Parameters.AddWithValue("@1", CInt(.ClassSubjectID))
                        update_studentgrades_Cmd.Parameters.AddWithValue("@2", .ClassID)
                        update_studentgrades_Cmd.Parameters.AddWithValue("@3", CInt(.cbAcademicYear.SelectedValue))
                        update_studentgrades_Cmd.ExecuteNonQuery()
                    End Using
                End If

                update_subjectschedule_transaction.Commit()
                frmWait.seconds = 1
                frmWait.ShowDialog()
                cm = New MySqlCommand("SELECT * FROM tbl_class_schedule WHERE csperiod_id = " & CInt(.cbAcademicYear.SelectedValue) & " and class_schedule_id = " & .ClassID & " and cssubject_id = " & CInt(.ClassSubjectID) & "", cn)
                Dim sdr As MySqlDataReader = cm.ExecuteReader()
                If (sdr.Read() = True) Then
                    sdr.Dispose()
                    Dim dr2 As DialogResult
                    dr2 = MessageBox.Show("Class schedule successfully updated! Do you want to continue updating class schedule using this form??", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If dr2 = DialogResult.No Then
                        .Close()
                    Else

                    End If
                Else
                    sdr.Dispose()
                    Dim dr2 As DialogResult
                    dr2 = MessageBox.Show("Class schedule successfully updated! Enrolled students successfully transferred. Do you want to continue updating class schedule using this form??", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If dr2 = DialogResult.No Then
                        .Close()
                    Else

                    End If
                End If
                cn.Close()
                UserActivity("Updated a class schedule in Academic Year " & .cbAcademicYear.Text & " for " & .cbSection.Text & " with subject " & .cbSubject.Text & " and Class ID:" & .ClassID & ".", "LIBRARY CLASS SCHEDULE")
                .Close()
            End With
        Catch ex As Exception
            update_subjectschedule_transaction.Rollback()
            MessageBox.Show("Class schedule update failed!", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cn.Close()
        End Try
    End Sub

    Public Sub SubjectListPerSection()
        Try

            frmClassSchedList.dgClassSchedList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "SELECT `tbl_class_schedule`.`class_schedule_id` AS `ID`, `tbl_class_block`.`cb_code` as 'Class', `period`.`PERIOD` as 'Period', CONCAT(`tbl_class_block`.`cb_code`, ' - ', `period`.`PERIOD`) AS `Code`, CONCAT(`tbl_subject`.`subject_code`, ' - ', `tbl_subject`.`subject_description` ) AS `Subject`, `tbl_subject`.`subject_units` AS `Units`, if(`tbl_day_schedule`.`ds_code` = 'M T W TH F SAT SUN', 'DAILY', `tbl_day_schedule`.`ds_code`) AS `Day Schedule`, `tbl_class_schedule`.`time_start_schedule` AS `Time Start`, `tbl_class_schedule`.`time_end_schedule` as `Time End`, `tbl_room`.`room_code` AS `Room`, CONCAT( `tbl_employee`.`emp_last_name`, ', ', `tbl_employee`.`emp_first_name`, ' ', `tbl_employee`.`emp_middle_name` ) AS `Instructor`, `tbl_class_schedule`.`population` AS `Population`, period_id, if(tbl_class_schedule.class_status = 'Merged', CONCAT(tbl_class_schedule.is_active,' - ',tbl_class_schedule.class_status), tbl_class_schedule.is_active) as 'Status' FROM (((((`tbl_class_schedule`JOIN `tbl_class_block`)JOIN `tbl_subject`)JOIN `tbl_day_schedule`)JOIN `tbl_room`)JOIN `tbl_employee`)JOIN `period` WHERE `tbl_class_schedule`.`class_block_id` = `tbl_class_block`.`cb_id` AND `tbl_class_schedule`.`cssubject_id` = `tbl_subject`.`subject_id` AND `tbl_class_schedule`.`days_schedule` = `tbl_day_schedule`.`ds_id` AND `tbl_class_schedule`.`csroom_id` = `tbl_room`.`room_id` AND `tbl_class_schedule`.`csemp_id` = `tbl_employee`.`emp_id` and `tbl_class_schedule`.`csperiod_id` = `period`.`period_id` and period_id = " & acadID & " and cb_id = " & CInt(frmClassSched.ClassSectionID) & " and `tbl_class_schedule`.`class_schedule_id` NOT IN (" & frmClassSched.ClassID & ") group by `class_schedule_id` limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmClassSchedList.dgClassSchedList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Class").ToString, dr.Item("Code").ToString, dr.Item("Subject").ToString, dr.Item("Units").ToString, dr.Item("Day Schedule").ToString, dr.Item("Time Start").ToString, dr.Item("Time End").ToString, dr.Item("Room").ToString, dr.Item("Instructor").ToString, dr.Item("Population").ToString, "", dr.Item("period_id").ToString, dr.Item("status").ToString)
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmClassSchedList.dgClassSchedList.Rows.Clear()
        End Try
    End Sub

    Public Sub SubjectListPerInstructor()
        Try
            frmClassSchedList.dgClassSchedList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "SELECT `tbl_class_schedule`.`class_schedule_id` AS `ID`, `tbl_class_block`.`cb_code` as 'Class', `period`.`PERIOD` as 'Period', CONCAT(`tbl_class_block`.`cb_code`, ' - ', `period`.`PERIOD`) AS `Code`, CONCAT(`tbl_subject`.`subject_code`, ' - ', `tbl_subject`.`subject_description` ) AS `Subject`, `tbl_subject`.`subject_units` AS `Units`, if(`tbl_day_schedule`.`ds_code` = 'M T W TH F SAT SUN', 'DAILY', `tbl_day_schedule`.`ds_code`) AS `Day Schedule`, `tbl_class_schedule`.`time_start_schedule` AS `Time Start`, `tbl_class_schedule`.`time_end_schedule` as `Time End`, `tbl_room`.`room_code` AS `Room`, CONCAT( `tbl_employee`.`emp_last_name`, ', ', `tbl_employee`.`emp_first_name`, ' ', `tbl_employee`.`emp_middle_name` ) AS `Instructor`, `tbl_class_schedule`.`population` AS `Population`, period_id, if(tbl_class_schedule.class_status = 'Merged', CONCAT(tbl_class_schedule.is_active,' - ',tbl_class_schedule.class_status), tbl_class_schedule.is_active) as 'Status' FROM (((((`tbl_class_schedule`JOIN `tbl_class_block`)JOIN `tbl_subject`)JOIN `tbl_day_schedule`)JOIN `tbl_room`)JOIN `tbl_employee`)JOIN `period` WHERE `tbl_class_schedule`.`class_block_id` = `tbl_class_block`.`cb_id` AND `tbl_class_schedule`.`cssubject_id` = `tbl_subject`.`subject_id` AND `tbl_class_schedule`.`days_schedule` = `tbl_day_schedule`.`ds_id` AND `tbl_class_schedule`.`csroom_id` = `tbl_room`.`room_id` AND `tbl_class_schedule`.`csemp_id` = `tbl_employee`.`emp_id` and `tbl_class_schedule`.`csperiod_id` = `period`.`period_id` and period_id = " & acadID & " and csemp_id = " & CInt(frmClassSched.ClassInstructorID) & " and `tbl_class_schedule`.`class_schedule_id` NOT IN (" & frmClassSched.ClassID & ") group by `class_schedule_id` limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmClassSchedList.dgClassSchedList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Class").ToString, dr.Item("Code").ToString, dr.Item("Subject").ToString, dr.Item("Units").ToString, dr.Item("Day Schedule").ToString, dr.Item("Time Start").ToString, dr.Item("Time End").ToString, dr.Item("Room").ToString, dr.Item("Instructor").ToString, dr.Item("Population").ToString, "", dr.Item("period_id").ToString, dr.Item("status").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmClassSchedList.dgClassSchedList.Rows.Clear()
        End Try
    End Sub

#End Region
#Region "Student"
    Public Sub AutoIDNumber()
        With frmStudentInfo
            .StudentID = YearToday.Remove(0, 2)
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT s_id_no FROM tbl_student WHERE s_id_no like '" & .StudentID & "%'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT MAX(s_id_no) as ID from tbl_student", cn)
                .StudentIDLastNumber = cm.ExecuteScalar
                cn.Close()
                Dim str As Integer = CInt(.StudentIDLastNumber)
                .NewStudentID = str + 1
                cn.Close()
            Else
                .NewStudentID = .StudentID + "00001"
                cn.Close()
            End If
            dr.Close()
            cn.Close()
        End With

    End Sub

    Public Sub LibraryStudentCourseList()
        Try


            frmStudentInfo.dgCourseList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select course_id, course_code, course_name, course_major, course_status from tbl_course where (course_code LIKE '%" & frmStudentInfo.txtSearch.Text & "%' or course_name LIKE '%" & frmStudentInfo.txtSearch.Text & "%') order by course_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmStudentInfo.dgCourseList.Rows.Add(i, dr.Item("course_id").ToString, dr.Item("course_code").ToString, dr.Item("course_name").ToString, dr.Item("course_major").ToString, dr.Item("course_status").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmStudentInfo.dgCourseList, frmStudentInfo.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmStudentInfo.dgCourseList.Rows.Clear()
        End Try
    End Sub

    Public Sub LibraryStudentReligionList()
        frmStudentInfo.dgReligionList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select * from tbl_religion where r_name LIKE '%" & frmStudentInfo.txtSearch.Text & "%' order by r_name asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmStudentInfo.dgReligionList.Rows.Add(i, dr.Item("r_id").ToString, dr.Item("r_name").ToString)
        End While
        dr.Close()
        cn.Close()

        dgPanelPadding(frmStudentInfo.dgReligionList, frmStudentInfo.dgPanel)
    End Sub

    Public Sub LibraryStudentDisabilityList()
        frmStudentInfo.dgDisabilityList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select * from tbl_pwd where pwd_name LIKE '%" & frmStudentInfo.txtSearch.Text & "%' order by pwd_name asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmStudentInfo.dgDisabilityList.Rows.Add(i, dr.Item("pwd_id").ToString, dr.Item("pwd_name").ToString)
        End While
        dr.Close()
        cn.Close()

        dgPanelPadding(frmStudentInfo.dgDisabilityList, frmStudentInfo.dgPanel)
    End Sub

    Public Sub LibraryStudentScholarshipList()
        Try

            frmStudentInfo.dgScholarshipList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select * from tbl_scholarship_status where scholar_name LIKE '%" & frmStudentInfo.txtSearch.Text & "%' order by scholar_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmStudentInfo.dgScholarshipList.Rows.Add(i, dr.Item("scholar_id").ToString, dr.Item("scholar_name").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmStudentInfo.dgScholarshipList, frmStudentInfo.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmStudentInfo.dgScholarshipList.Rows.Clear()

        End Try
    End Sub

    Public Sub LibraryStudentSchoolList()
        Try

            frmStudentInfo.dgSchoolList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select * from tbl_schools where (schl_code LIKE '%" & frmStudentInfo.txtSearch.Text & "%' or schl_name LIKE '%" & frmStudentInfo.txtSearch.Text & "%') order by schl_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmStudentInfo.dgSchoolList.Rows.Add(i, dr.Item("schl_id").ToString, dr.Item("schl_code").ToString, dr.Item("schl_name").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmStudentInfo.dgSchoolList, frmStudentInfo.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmStudentInfo.dgSchoolList.Rows.Clear()

        End Try
    End Sub

    Sub StudentModuleSearch()
        Select Case frmStudentInfo.frmTitle.Text
            Case "Search Religion"
                LibraryStudentReligionList()
            Case "Search Disability"
                LibraryStudentDisabilityList()
            Case "Search Course"
                LibraryStudentCourseList()
            Case "Search Scholarship"
                LibraryStudentScholarshipList()
            Case "Search Last School Attended"
                LibraryStudentSchoolList()
            Case "Search Transferred School"
                LibraryStudentSchoolList()
            Case "Search Primary School"
                LibraryStudentSchoolList()
            Case "Search Junior High School"
                LibraryStudentSchoolList()
            Case "Search Senior High School"
                LibraryStudentSchoolList()
            Case "Search College School"
                LibraryStudentSchoolList()
            Case "Search Masters School"
                LibraryStudentSchoolList()
            Case "Search Doctorate School"
                LibraryStudentSchoolList()
        End Select
    End Sub

    Sub StudentModuleAdd()
        Select Case frmStudentInfo.frmTitle.Text
            Case "Search Religion"

            Case "Search Disability"

            Case "Search Course"
                With frmCourse
                    ResetControls(frmCourse)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "Search Scholarship"

            Case "Search Last School Attended"
                With frmSchool
                    ResetControls(frmSchool)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "Search Transferred School"
                With frmSchool
                    ResetControls(frmSchool)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "Search Primary School"
                With frmSchool
                    ResetControls(frmSchool)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "Search Junior High School"
                With frmSchool
                    ResetControls(frmSchool)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "Search Senior High School"
                With frmSchool
                    ResetControls(frmSchool)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "Search College School"
                With frmSchool
                    ResetControls(frmSchool)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "Search Masters School"
                With frmSchool
                    ResetControls(frmSchool)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
            Case "Search Doctorate School"
                With frmSchool
                    ResetControls(frmSchool)
                    .btnUpdate.Visible = False
                    .btnSave.Visible = True
                    .ShowDialog()
                End With
        End Select
    End Sub
#End Region
#Region "Cashiering"
    Public Sub GenerateSOA()
        If frmCashiering.StudentID = String.Empty Then
            frmCashiering.ReportViewer.ReportSource = Nothing
            MsgBox("Please select Student.", vbCritical)
            frmCashiering.btnSearchStudent.Select()
        ElseIf CInt(frmCashiering.cbAcademicYear.SelectedValue) <= 0 Then
            frmCashiering.ReportViewer.ReportSource = Nothing
            MsgBox("Please select Academic Year.", vbCritical)
            frmCashiering.cbAcademicYear.Select()
        Else


            Dim studentGradeLevel As String = ""
            Dim studentGradeLevelCourse As String = ""
            Dim studentGradeLevelCourseName As String = ""
            Dim studentGradeLevelCourseCode As String = ""
            Dim studentGradeLevelCourseID As Integer = 0

            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT `sg_yearlevel` FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & frmCashiering.StudentID & "' and `sg_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                studentGradeLevel = cm.ExecuteScalar
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT CONCAT(`course_code`,' - ',`course_name`) FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & frmCashiering.StudentID & "' and `sg_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                studentGradeLevelCourse = cm.ExecuteScalar
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT `course_name` FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & frmCashiering.StudentID & "' and `sg_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                studentGradeLevelCourseName = cm.ExecuteScalar
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT `course_code` FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & frmCashiering.StudentID & "' and `sg_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                studentGradeLevelCourseCode = cm.ExecuteScalar
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT `course_id` FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & frmCashiering.StudentID & "' and `sg_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                studentGradeLevelCourseID = cm.ExecuteScalar
                cn.Close()
                'cn.Open()
                'cm = New MySqlCommand("SELECT sg_course_id FROM `tbl_students_grades` t1 JOIN tbl_course t2 ON t1.sg_course_id = t2.course_id WHERE `sg_student_id` = '" & studentId & "' and `sg_period_id` = " & CInt(cbAcademicYear.SelectedValue) & "", cn)
                'studentCourseId = cm.ExecuteScalar
                'cn.Close()
                'cn.Open()
            Catch ex As Exception

            End Try


            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("SELECT * from tbl_enrollment where estudent_id = '" & frmCashiering.StudentID & "' and eperiod_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then

                    dr.Close()
                    cn.Close()
                    Dim assessment As Double
                    Dim institutionaldiscount As Double
                    Dim downpayment As Double
                    Dim additionaladjustment As Double
                    Dim lessadjustment As Double
                    Dim totalassessment As Double
                    Dim balance As Double
                    Dim totalpaid As Double
                    Dim otherfees As Double
                    Dim prelim_date As String
                    Dim midterm_date As String
                    Dim semifinal_date As String
                    Dim final_date As String
                    Dim assessmentid As Integer
                    Dim oldaccount As Double
                    Dim lackingcredentials As String
                    Dim scholarname As String
                    Dim petition As Double
                    Dim petition_no As Integer
                    Dim additional_fees As Double
                    Dim non_petition_no As Integer
                    Dim prcnt As Decimal

                    cn.Open()
                    cm = New MySqlCommand("SELECT `Additional Fee (Subject Fee/Petition)` FROM `student_assessment_total` WHERE `spab_stud_id` =  '" & frmCashiering.StudentID & "' and `spab_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                    petition = cm.ExecuteScalar
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT COUNT(class_schedule_id) as 'ClassSchedule_ID' from tbl_students_grades JOIN tbl_class_schedule ON tbl_students_grades.sg_class_id = tbl_class_schedule.class_schedule_id and tbl_students_grades.sg_period_id = tbl_class_schedule.csperiod_id WHERE cs_is_petition = 'Yes' and `sg_student_id` =  '" & frmCashiering.StudentID & "' and `sg_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                    petition_no = cm.ExecuteScalar
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT COUNT(class_schedule_id) as 'ClassSchedule_ID' from tbl_students_grades JOIN tbl_class_schedule ON tbl_students_grades.sg_class_id = tbl_class_schedule.class_schedule_id and tbl_students_grades.sg_period_id = tbl_class_schedule.csperiod_id WHERE cs_amount > 0 and cs_is_petition NOT IN ('Yes') and `sg_student_id` =  '" & frmCashiering.StudentID & "' and `sg_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                    non_petition_no = cm.ExecuteScalar
                    cn.Close()
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT `Academic Year`, `Assessment`, `Institutional Discount`, `Discounted Assessment`, `Other Fees`, `Additional Fee (Uniforms, etc.)`, `Additional Fee (Subject Fee/Petition)`, `Additional Adjustment`, `Less Adjustment`, `Down Payment`, `Total Assessment`, (`Total Paid` + `Down Payment`) as `Paid`, `Total Balance`, `Excess`, spab_ass_id FROM `student_assessment_total` WHERE `spab_stud_id` =  '" & frmCashiering.StudentID & "' and `spab_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        assessment = dr.Item("Assessment").ToString
                        institutionaldiscount = dr.Item("Institutional Discount").ToString
                        downpayment = dr.Item("Down Payment").ToString
                        additionaladjustment = dr.Item("Additional Adjustment").ToString
                        lessadjustment = dr.Item("Less Adjustment").ToString
                        totalassessment = dr.Item("Total Assessment").ToString
                        balance = dr.Item("Total Balance").ToString
                        totalpaid = dr.Item("Paid").ToString
                        otherfees = dr.Item("Other Fees").ToString
                        assessmentid = dr.Item("spab_ass_id").ToString
                        additional_fees = dr.Item("Additional Fee (Uniforms, etc.)").ToString
                    End If
                    dr.Close()
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'PRELIM' AND afb_breakdown_period_date IS NOT NULL) AS PRELIM, (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'MID-TERM' AND afb_breakdown_period_date IS NOT NULL) AS MIDTERM, (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'SEMI-FINAL' AND afb_breakdown_period_date IS NOT NULL) AS 'SEMI-FINAL', (SELECT DISTINCT afb_breakdown_period_date FROM tbl_assessment_fee_breakdown WHERE afb_period_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & " AND afb_breakdown_period = 'FINAL' AND afb_breakdown_period_date IS NOT NULL) AS FINAL;", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        prelim_date = dr.Item("PRELIM").ToString
                        midterm_date = dr.Item("MIDTERM").ToString
                        semifinal_date = dr.Item("SEMI-FINAL").ToString
                        final_date = dr.Item("FINAL").ToString
                    End If
                    dr.Close()
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT ifNULL(SUM(`Total Balance`),0) - ifNULL(SUM(`Excess`),0) as 'OldAccount' FROM `student_assessment_total` WHERE `spab_stud_id` = '" & frmCashiering.StudentID & "' and `spab_period_id` NOT IN(" & CInt(frmCashiering.cbAcademicYear.SelectedValue) & ")", cn)
                    oldaccount = cm.ExecuteScalar
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT s_notes from tbl_student JOIN tbl_scholarship_status where tbl_student.s_scholarship = tbl_scholarship_status.scholar_id and s_id_no = '" & frmCashiering.StudentID & "'", cn)
                    lackingcredentials = cm.ExecuteScalar
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT scholar_name from tbl_student JOIN tbl_scholarship_status where tbl_student.s_scholarship = tbl_scholarship_status.scholar_id and s_id_no = '" & frmCashiering.StudentID & "'", cn)
                    scholarname = cm.ExecuteScalar
                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT ROUND(aid_percentage, 2) from tbl_assessment_institutional_discount where aid_student_id = '" & frmCashiering.StudentID & "' and aid_period_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                    prcnt = cm.ExecuteScalar
                    cn.Close()
                    Dim prelimpercent As Decimal
                    Dim midtermpercent As Decimal
                    Dim semipercent As Decimal
                    Dim finalpercent As Decimal

                    cn.Open()
                    cm = New MySqlCommand("SELECT af_prelim_percentage, af_midterm_percentage, af_semifinal_percentage, af_final_percentage from tbl_assessment_fee where af_period_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & " and af_id = " & assessmentid & "", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        prelimpercent = dr.Item("af_prelim_percentage").ToString
                        midtermpercent = dr.Item("af_midterm_percentage").ToString
                        semipercent = dr.Item("af_semifinal_percentage").ToString
                        finalpercent = dr.Item("af_final_percentage").ToString
                    End If
                    dr.Close()
                    cn.Close()
                    Dim totalaccountpaid As Decimal
                    cn.Open()
                    cm = New MySqlCommand("SELECT `Total Paid` as 'TotalPaid' FROM `student_assessment_total` WHERE `spab_stud_id` = '" & frmCashiering.StudentID & "' and `spab_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                    totalaccountpaid = cm.ExecuteScalar
                    cn.Close()

                    Dim totalcurrentassessment As Decimal
                    Dim totalcurrent_assessment As Decimal
                    Dim totalcurrent_balance As Decimal
                    Dim total_assessment As Decimal
                    Dim a As Decimal
                    Dim b As Decimal
                    Dim c As Decimal
                    Dim d As Decimal
                    Dim f As Decimal
                    Dim g As Decimal
                    Dim h As Decimal
                    a = oldaccount
                    b = assessment
                    c = additionaladjustment
                    d = balance
                    f = totalassessment
                    g = institutionaldiscount
                    h = downpayment
                    totalcurrent_assessment = b + c
                    totalcurrent_balance = d
                    total_assessment = f
                    totalcurrentassessment = a + b + otherfees - g
                    totalcurrent_assessment = (totalassessment + oldaccount) - h

                    Dim total_prelim As Double
                    Dim total_midterm As Double
                    Dim total_semifinal As Double
                    Dim total_final As Double
                    Dim withdownpayment As Double
                    Dim totalwithdownpayment As Double
                    If downpayment >= 2000 Then
                        total_prelim = totalcurrent_assessment * prelimpercent
                        total_midterm = totalcurrent_assessment * midtermpercent
                        total_semifinal = totalcurrent_assessment * semipercent
                        total_final = totalcurrent_assessment * finalpercent
                    ElseIf downpayment < 2000 Then
                        withdownpayment = totalcurrent_assessment - 2000 + downpayment
                        totalwithdownpayment = 2000 - downpayment
                        total_prelim = (withdownpayment * prelimpercent) + totalwithdownpayment
                        total_midterm = withdownpayment * midtermpercent
                        total_semifinal = withdownpayment * semipercent
                        total_final = withdownpayment * finalpercent
                    End If

                    Dim balance_prelim As Decimal
                    Dim balance_midterm As Decimal
                    Dim balance_semifinal As Decimal
                    Dim balance_final As Decimal

                    If totalaccountpaid > total_prelim Then
                        Dim subtractprelim As Decimal
                        subtractprelim = totalaccountpaid - total_prelim
                        If subtractprelim > total_midterm Then
                            Dim subtractmidterm As Decimal
                            subtractmidterm = subtractprelim - total_midterm
                            If subtractmidterm > total_semifinal Then
                                Dim subtractsemi As Decimal
                                subtractsemi = subtractmidterm - total_semifinal
                                If subtractsemi > total_final Then
                                    Dim subtractfinal As Decimal
                                    subtractfinal = subtractsemi - total_final
                                    If subtractfinal <= total_final Then
                                        balance_prelim = "0.00"
                                        balance_midterm = "0.00"
                                        balance_semifinal = "0.00"
                                        balance_final = "0.00"
                                    End If
                                Else
                                    balance_prelim = "0.00"
                                    balance_midterm = "0.00"
                                    balance_semifinal = "0.00"
                                    balance_final = total_final - subtractsemi
                                End If
                            Else
                                balance_prelim = "0.00"
                                balance_midterm = "0.00"
                                balance_semifinal = total_semifinal - subtractmidterm
                                balance_final = total_final
                            End If
                        Else
                            balance_prelim = "0.00"
                            balance_midterm = total_midterm - subtractprelim
                            balance_semifinal = total_semifinal
                            balance_final = total_final
                        End If
                    Else
                        balance_prelim = total_prelim - totalaccountpaid
                        balance_midterm = total_midterm
                        balance_semifinal = total_semifinal
                        balance_final = total_final
                    End If

                    If frmMain.systemModule.Text = "College Module" Then

                        cn.Open()
                        Dim studentAssessmentID As Integer = 0
                        cm = New MySqlCommand("SELECT ps_ass_id FROM `tbl_pre_cashiering` WHERE `student_id` = '" & frmCashiering.StudentID & "' and `period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & "", cn)
                        studentAssessmentID = cm.ExecuteScalar
                        cn.Close()
                        cn.Open()

                        cn.Open()
                        Dim asseessmentCategory As String = ""
                        cm = New MySqlCommand("SELECT af_gender FROM `tbl_assessment_fee` WHERE `af_id` = " & studentAssessmentID & "", cn)
                        asseessmentCategory = cm.ExecuteScalar
                        cn.Close()
                        cn.Open()

                        Dim dtable As DataTable
                        Dim sql As String

                        sql = "SELECT (ofsp_particular_id) as 'ID', (ap_particular_code) as 'Code', (ap_particular_name) as 'Particular', (ofsp_amount) as 'Amount' from tbl_assessment_ofs_particulars JOIN tbl_assessment_particulars where tbl_assessment_ofs_particulars.ofsp_particular_id = tbl_assessment_particulars.ap_id and ofsp_period_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & " and ofsp_course_id = " & studentGradeLevelCourseID & " and ofsp_year_level = LEFT('" & studentGradeLevel & "', 8) and ofsp_gender ='" & asseessmentCategory & "'"

                        Dim dbcommand As New MySqlCommand(sql, cn)
                        Dim adt As New MySqlDataAdapter
                        adt.SelectCommand = dbcommand
                        dtable = New DataTable
                        adt.Fill(dtable)
                        frmCashiering.dg_report2.DataSource = dtable
                        adt.Dispose()
                        dbcommand.Dispose()
                        cn.Close()

                        dt2.Columns.Clear()
                        dt2.Rows.Clear()
                        With dt2
                            .Columns.Add("name")
                            .Columns.Add("amount")
                        End With

                        For Each dr As DataGridViewRow In frmCashiering.dg_report2.Rows
                            dt2.Rows.Add(dr.Cells(2).Value, dr.Cells(3).Value)
                        Next
                    End If

                    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    If frmMain.systemModule.Text = "College Module" Then
                        rptdoc = New StatementOfAccount6
                        rptdoc.Subreports(0).SetDataSource(dt2)
                    Else
                        rptdoc = New StatementOfAccount4
                    End If
                    rptdoc.SetParameterValue("sname", frmCashiering.StudentName)
                    rptdoc.SetParameterValue("sid", frmCashiering.StudentID)
                    rptdoc.SetParameterValue("scourse_yrlvl", studentGradeLevel & " - " & studentGradeLevelCourseCode)
                    rptdoc.SetParameterValue("prelim_date", Format(Convert.ToDateTime(prelim_date), "MMM yyyy"))
                    rptdoc.SetParameterValue("midterm_date", Format(Convert.ToDateTime(midterm_date), "MMM yyyy"))
                    rptdoc.SetParameterValue("semifinal_date", Format(Convert.ToDateTime(semifinal_date), "MMM yyyy"))
                    rptdoc.SetParameterValue("final_date", Format(Convert.ToDateTime(final_date), "MMM yyyy"))
                    rptdoc.SetParameterValue("prelim_balance", Format(balance_prelim, "n2"))
                    rptdoc.SetParameterValue("midterm_balance", Format(balance_midterm, "n2"))
                    rptdoc.SetParameterValue("semifinal_balance", Format(balance_semifinal, "n2"))
                    rptdoc.SetParameterValue("final_balance", Format(balance_final, "n2"))
                    rptdoc.SetParameterValue("oldaccounts", Format(oldaccount, "n2"))
                    rptdoc.SetParameterValue("addadjustment", Format(additionaladjustment, "n2"))
                    rptdoc.SetParameterValue("lessadjustment", Format(lessadjustment, "n2"))
                    rptdoc.SetParameterValue("downpayment", Format(downpayment, "n2"))
                    rptdoc.SetParameterValue("institutionaldiscount", Format(institutionaldiscount, "n2"))
                    rptdoc.SetParameterValue("totalcurrentassessment", Format(totalcurrentassessment, "n2"))
                    rptdoc.SetParameterValue("currentbalance", Format(((totalassessment + oldaccount) - totalaccountpaid) - downpayment, "n2"))
                    rptdoc.SetParameterValue("president_admin", "")
                    rptdoc.SetParameterValue("prepared_by", str_name)
                    rptdoc.SetParameterValue("currentassessment", Format(assessment, "n2"))
                    rptdoc.SetParameterValue("payments", Format(totalaccountpaid, "n2"))
                    rptdoc.SetParameterValue("currentperiod", frmCashiering.cbAcademicYear.Text)
                    rptdoc.SetParameterValue("lackingcredentials", lackingcredentials)
                    rptdoc.SetParameterValue("otherfee", Format(otherfees, "n2"))
                    rptdoc.SetParameterValue("scholarship", scholarname)
                    rptdoc.SetParameterValue("discountpercent", Math.Round(prcnt * 100, 0) & "%")

                    If petition > 0 Then
                        rptdoc.SetParameterValue("petitiontitle", "Add: Subject Fee: Regular -" & non_petition_no & " / Petition - " & petition_no & "")
                        rptdoc.SetParameterValue("petition_amount", Format(petition, "n2"))
                    Else
                        rptdoc.SetParameterValue("petitiontitle", " ")
                        rptdoc.SetParameterValue("petition_amount", " ")
                    End If

                    rptdoc.SetParameterValue("additional_amount", Format(additional_fees, "n2"))
                    rptdoc.SetParameterValue("date_generated", Format(Convert.ToDateTime(DateToday), "MMMM d, yyyy"))
                    frmCashiering.ReportViewer.ReportSource = rptdoc
                    frmCashiering.dg_report2.DataSource = Nothing
                    frmCashiering.ReportViewer.Select()
                End If
            Catch ex As Exception
                MsgBox(ex.Message, vbCritical)
                cn.Close()
            End Try
        End If
    End Sub

    Public Function CHECK_PRECASHIERING(ByVal sql As String) As Boolean
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            CHECK_PRECASHIERING = True
            MsgBox("Warning: Student already pre-cashiered.", vbExclamation)
        Else
            CHECK_PRECASHIERING = False
        End If
        dr.Close()
        cn.Close()
        Return CHECK_PRECASHIERING
    End Function

    Function StudentTotalBalance(ByVal sql As String) As Decimal
        Dim count As Decimal
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        count = CInt(cm.ExecuteScalar)
        cn.Close()
        Return count
    End Function

    Public Function CHECK_BALANCE(ByVal sql As String) As Boolean
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            CHECK_BALANCE = False
        Else
            CHECK_BALANCE = True
            CHECK_BALANCE_ALL("SELECT * FROM tbl_student_enroll_permit where stud_id = '" & frmCashiering.dgStudentList.CurrentRow.Cells(1).Value & "' and period_id  = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & " and status = 1")
        End If
        dr.Close()
        cn.Close()
        Return CHECK_BALANCE
    End Function

    Public Function CHECK_BALANCE_ALL(ByVal sql As String) As Boolean
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            CHECK_BALANCE_ALL = False
        Else
            If frmCashiering.StudentBalance >= 1 Then
                CHECK_BALANCE_ALL = False
            Else
                CHECK_BALANCE_ALL = True
                MsgBox("Warning: Selected student " & frmCashiering.txtStudent.Text & " still has outstanding balance of '" & Format(frmCashiering.StudentBalance, "#,###.00") & "'. Unable to proceed.", vbExclamation)
            End If
        End If
        dr.Close()
        cn.Close()
        Return CHECK_BALANCE_ALL
    End Function

    Public Sub CashieringLoadCurrentAccount()
        Try
            frmCashiering.dgCurrentAccount.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "SELECT `Academic Year`, FORMAT(`Assessment`,2) as Assessment, FORMAT(`Institutional Discount`,2) as 'Institutional Discount', FORMAT(`Discounted Assessment`,2) as 'Discounted Assessment', FORMAT(`Other Fees`,2) as 'Other Fees', FORMAT(`Additional Fee (Uniforms, etc.)`,2) as 'Additional Fee (Uniforms, etc.)', FORMAT(`Additional Fee (Subject Fee/Petition)`,2) as 'Additional Fee (Subject Fee/Petition)', FORMAT(`Additional Adjustment`,2) as 'Additional Adjustment', FORMAT(`Less Adjustment`,2) as 'Less Adjustment', FORMAT(`Down Payment`,2) as 'Down Payment', FORMAT(`Total Assessment`,2) as 'Total Assessment', FORMAT(`Total Paid`,2) as 'Total Paid', FORMAT(`Total Balance`,2) as 'Total Balance', FORMAT(`Excess`,2) as 'Excess' FROM `student_assessment_total` WHERE `spab_stud_id` =  '" & frmCashiering.StudentID & "' and `spab_period_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & ""
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmCashiering.dgCurrentAccount.Rows.Add(i, dr.Item("Academic Year").ToString, dr.Item("Assessment").ToString, dr.Item("Institutional Discount").ToString, dr.Item("Discounted Assessment").ToString, dr.Item("Other Fees").ToString, dr.Item("Additional Fee (Uniforms, etc.)").ToString, dr.Item("Additional Fee (Subject Fee/Petition)").ToString, dr.Item("Additional Adjustment").ToString, dr.Item("Less Adjustment").ToString, dr.Item("Down Payment").ToString, dr.Item("Total Assessment").ToString, dr.Item("Total Paid").ToString, dr.Item("Total Balance").ToString, dr.Item("Excess").ToString)
            End While
            dr.Close()
            cn.Close()

            Try
                Dim dbcommand As String
                If frmMain.systemModule.Text = "College Module" Then
                    dbcommand = "Select categoryname, description, cfcissmsdb.tbl_assessment_additional.additional_price as Price, (cfcissmsdb.tbl_assessment_additional.additional_qty) as QTY, cfcissmsdb.tbl_assessment_additional.additional_price as Total, '' as 'Additional', '' as 'Additional', '' as 'Additional', '' as 'Additional', '' as 'Additional', '' as 'Additional', '' as 'Additional', '' as 'Additional' from cfcissmsdb.tbl_assessment_additional, cfcissmsdb.tbl_supply_item, cfcissmsdb.tbl_supply_category where cfcissmsdb.tbl_assessment_additional.additional_item_id = cfcissmsdb.tbl_supply_item.barcodeid AND cfcissmsdb.tbl_supply_item.categoryid = cfcissmsdb.tbl_supply_category.catid and cfcissmsdb.tbl_assessment_additional.additional_period_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & " and cfcissmsdb.tbl_assessment_additional.additional_stud_id = '" & frmCashiering.StudentID & "'"
                ElseIf frmMain.systemModule.Text = "Basic Education Module" Then
                    dbcommand = "Select categoryname, description, cfcissmsdbhighschool.tbl_assessment_additional.additional_price as Price, (cfcissmsdbhighschool.tbl_assessment_additional.additional_qty) as QTY, cfcissmsdbhighschool.tbl_assessment_additional.additional_price as Total, '' as 'Additional', '' as 'Additional', '' as 'Additional', '' as 'Additional', '' as 'Additional', '' as 'Additional', '' as 'Additional', '' as 'Additional' from cfcissmsdbhighschool.tbl_assessment_additional, cfcissmsdb.tbl_supply_item, cfcissmsdb.tbl_supply_category where cfcissmsdbhighschool.tbl_assessment_additional.additional_item_id = cfcissmsdb.tbl_supply_item.barcodeid AND cfcissmsdb.tbl_supply_item.categoryid = cfcissmsdb.tbl_supply_category.catid and cfcissmsdbhighschool.tbl_assessment_additional.additional_period_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & " and cfcissmsdbhighschool.tbl_assessment_additional.additional_stud_id = '" & frmCashiering.StudentID & "'"
                End If
                adptr = New MySqlDataAdapter(dbcommand, cn)
                dt = New DataTable
                adptr.Fill(dt)
                frmCashiering.dgadditional.DataSource = dt
                If frmCashiering.dgadditional.RowCount = 0 Then
                Else
                    Try
                        Dim dttt As DataTable = frmCashiering.dgCurrentAccount.DataSource
                        Dim MyNewRow As DataRow
                        MyNewRow = dttt.NewRow
                        MyNewRow("Assessment") = "Additional"
                        dttt.Rows.Add(MyNewRow)
                        dttt.AcceptChanges()
                        dttt.Dispose()
                    Catch ex As Exception
                    End Try
                    Try
                        Dim dttt As DataTable = frmCashiering.dgCurrentAccount.DataSource
                        Dim MyNewRow As DataRow
                        MyNewRow = dttt.NewRow
                        MyNewRow("Assessment") = "Category"
                        MyNewRow("Institutional Discount") = "Description"
                        MyNewRow("Discounted Assessment") = "Price"
                        MyNewRow("Other Fees") = "Quantity"
                        MyNewRow("Additional Fee (Uniforms, etc.)") = "Total"
                        dttt.Rows.Add(MyNewRow)
                        dttt.AcceptChanges()
                        dttt.Dispose()
                    Catch ex As Exception
                    End Try
                    For Each row As DataGridViewRow In frmCashiering.dgadditional.Rows
                        Try
                            Dim dttt As DataTable = frmCashiering.dgCurrentAccount.DataSource
                            Dim MyNewRow As DataRow
                            MyNewRow = dttt.NewRow
                            MyNewRow("Assessment") = row.Cells(0).Value
                            MyNewRow("Institutional Discount") = row.Cells(1).Value
                            MyNewRow("Discounted Assessment") = row.Cells(2).Value
                            MyNewRow("Other Fees") = row.Cells(3).Value
                            MyNewRow("Additional Fee (Uniforms, etc.)") = row.Cells(4).Value
                            dttt.Rows.Add(MyNewRow)
                            dttt.AcceptChanges()
                            dttt.Dispose()
                        Catch ex As Exception
                        End Try
                    Next
                    frmCashiering.dgadditional.DataSource = Nothing
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Public Sub CashieringLoadCurrentAccountPerAcademicYear()
        Try
            frmCashiering.dgAcadAccounts.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "SELECT `Academic Year`, FORMAT(`Assessment`,2) as Assessment, FORMAT(`Institutional Discount`,2) as 'Institutional Discount', FORMAT(`Discounted Assessment`,2) as 'Discounted Assessment', FORMAT(`Other Fees`,2) as 'Other Fees', FORMAT(`Additional Fee (Uniforms, etc.)`,2) as 'Additional Fee (Uniforms, etc.)', FORMAT(`Additional Fee (Subject Fee/Petition)`,2) as 'Additional Fee (Subject Fee/Petition)', FORMAT(`Additional Adjustment`,2) as 'Additional Adjustment', FORMAT(`Less Adjustment`,2) as 'Less Adjustment', FORMAT(`Down Payment`,2) as 'Down Payment', FORMAT(`Total Assessment`,2) as 'Total Assessment', FORMAT(`Total Paid`,2) as 'Total Paid', FORMAT(`Total Balance`,2) as 'Total Balance', FORMAT(`Excess`,2) as 'Excess' FROM `student_assessment_total` WHERE `spab_stud_id` = '" & frmCashiering.StudentID & "'"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmCashiering.dgAcadAccounts.Rows.Add(i, dr.Item("Academic Year").ToString, dr.Item("Assessment").ToString, dr.Item("Institutional Discount").ToString, dr.Item("Discounted Assessment").ToString, dr.Item("Other Fees").ToString, dr.Item("Additional Fee (Uniforms, etc.)").ToString, dr.Item("Additional Fee (Subject Fee/Petition)").ToString, dr.Item("Additional Adjustment").ToString, dr.Item("Less Adjustment").ToString, dr.Item("Down Payment").ToString, dr.Item("Total Assessment").ToString, dr.Item("Total Paid").ToString, dr.Item("Total Balance").ToString, dr.Item("Excess").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmCashiering.dgAcadAccounts.Rows.Clear()
        End Try
    End Sub

    Public Sub CashieringPaymentHistory()
        Try
            frmCashiering.dgPaymentHistory.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "Select (Period) as 'Academic Year', (csh_ornumber) as 'OR Number', Format(csh_total_amount, 2) as 'Amount Total', Format(csh_amount_received,2) as 'Amount Received', Format(csh_amount_change, 2) as 'Amount Change', (accountname) as 'Cashier', DATE_FORMAT(csh_date, '%M %d, %Y') as 'Date', (csh_notes) as 'Notes' from ((tbl_cashiering JOIN tbl_student) JOIN useraccounts) JOIN period where tbl_cashiering.csh_stud_id = tbl_student.s_id_no and tbl_cashiering.csh_cashier_id = useraccounts.useraccountID and tbl_cashiering.csh_period_id = period.period_id and csh_stud_id = '" & frmCashiering.StudentID & "' order by csh_date desc"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmCashiering.dgPaymentHistory.Rows.Add(i, dr.Item("Academic Year").ToString, dr.Item("OR Number").ToString, dr.Item("Amount Total").ToString, dr.Item("Amount Received").ToString, dr.Item("Cashier").ToString, dr.Item("Date").ToString, dr.Item("Notes").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmCashiering.dgPaymentHistory.Rows.Clear()
        End Try
    End Sub

    Public Sub PaymentMonitoring()
        Try
            frmPaymentMonitoring.dgPayments.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "Select (csh_stud_id) as 'Student ID', CONCAT(s_ln,', ',s_fn,' ',s_mn) as 'Student Name',(csh_ornumber) as 'OR Number', (csh_total_amount) as 'Amount Paid', (csh_amount_received) as 'Amount Received', (csh_amount_change) as 'Amount Change', (accountname) as 'Cashier', DATE_FORMAT( csh_date, '%Y/%m/%d' ) as 'Date', (csh_notes) as 'Notes', (Period) as 'Academic Year', (csh_type) as 'Type', csh_id, csh_period_id, (select pre_cash_id from tbl_pre_cashiering where ornumber = tbl_cashiering.csh_ornumber ORDER BY `Notes` ASC limit 1) as pre_cash_id from tbl_cashiering LEFT JOIN tbl_student ON tbl_cashiering.csh_stud_id = tbl_student.s_id_no LEFT JOIN useraccounts ON tbl_cashiering.csh_cashier_id = useraccounts.useraccountID LEFT JOIN period ON tbl_cashiering.csh_period_id = period.period_id WHERE (csh_stud_id like '% " & frmPaymentMonitoring.txtSearch.Text & " %' or csh_ornumber like '% " & frmPaymentMonitoring.txtSearch.Text & " %' or CONCAT(s_ln,', ',s_fn,' ',s_mn) like '% " & frmPaymentMonitoring.txtSearch.Text & " %') order by csh_id desc limit 200"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmPaymentMonitoring.dgPayments.Rows.Add(i, dr.Item("Academic Year").ToString, dr.Item("Student ID").ToString, dr.Item("Student Name").ToString, dr.Item("OR Number").ToString, dr.Item("Amount Paid").ToString, dr.Item("Amount Received").ToString, dr.Item("Cashier").ToString, dr.Item("Date").ToString, dr.Item("Type").ToString, dr.Item("Notes").ToString, dr.Item("csh_id").ToString, dr.Item("csh_period_id").ToString, dr.Item("pre_cash_id").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmPaymentMonitoring.dgPayments.Rows.Clear()
        End Try
    End Sub


    Public Sub LibraryCashieringStudentList()
        Try

            frmCashiering.dgStudentList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            If frmMain.formTitle.Text = "Pre-Cashiering" Then
                sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, course_name from tbl_enrollment_subjects JOIN tbl_student ON tbl_enrollment_subjects.es_student_id = tbl_student.s_id_no JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where tbl_enrollment_subjects.es_period_id = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & " and tbl_enrollment_subjects.es_student_id NOT IN (SELECT `estudent_id` FROM `tbl_enrollment` WHERE `eperiod_id` = " & CInt(frmCashiering.cbAcademicYear.SelectedValue) & ") and (tbl_student.s_ln like '" & frmCashiering.txtSearch.Text & "%' or tbl_student.s_fn like '" & frmCashiering.txtSearch.Text & "%' or tbl_student.s_mn like '" & frmCashiering.txtSearch.Text & "%' or tbl_student.s_id_no like '" & frmCashiering.txtSearch.Text & "%' or tbl_student.s_yr_lvl like '" & frmCashiering.txtSearch.Text & "%') group by tbl_enrollment_subjects.es_student_id order by tbl_student.s_id_no asc limit 500"
            Else
                sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, course_name from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (s_ln like '" & frmCashiering.txtSearch.Text & "%' or s_fn like '" & frmCashiering.txtSearch.Text & "%' or s_mn like '" & frmCashiering.txtSearch.Text & "%' or s_id_no like '" & frmCashiering.txtSearch.Text & "%' or course_code like '" & frmCashiering.txtSearch.Text & "%' or s_yr_lvl like '" & frmCashiering.txtSearch.Text & "%') order by s_id_no asc limit 500"
            End If
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmCashiering.dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("course_name").ToString)
            End While
            dr.Close()
            cn.Close()

            If frmMain.systemModule.Text = "College Module" Then
                frmCashiering.dgStudentList.Columns(8).HeaderText = "Course"
            Else
                frmCashiering.dgStudentList.Columns(8).HeaderText = "Strand/Grade"
            End If

            dgPanelPadding(frmCashiering.dgStudentList, frmCashiering.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmCashiering.dgStudentList.Rows.Clear()
        End Try
    End Sub

    Public Sub PerformCashieringTransaction()
        With frmCashiering
            Dim iDate As String = DateToday
            Dim oDate As DateTime = Convert.ToDateTime(iDate)

            Dim cashiering_transaction As MySqlTransaction = cn.BeginTransaction()
            Try
                cn.Open()
                If frmCashiering.cbPaymentMethod.Text = "CHEQUE" Then
                    Using cashiering_cheque_Cmd As MySqlCommand = cn.CreateCommand()
                        cashiering_cheque_Cmd.Transaction = cashiering_transaction
                        cashiering_cheque_Cmd.CommandText = "INSERT INTO tbl_cashiering_cheque (chq_ornumber, chq_no, chq_bankname, chq_bankbranch, chq_date) values (@1, @2, @3, @4, @5)"

                        cashiering_cheque_Cmd.Parameters.AddWithValue("@1", .txtOR.Text)
                        cashiering_cheque_Cmd.Parameters.AddWithValue("@2", .ChequeNo)
                        cashiering_cheque_Cmd.Parameters.AddWithValue("@3", .ChequeBankName)
                        cashiering_cheque_Cmd.Parameters.AddWithValue("@4", .ChequeBankBranch)
                        cashiering_cheque_Cmd.Parameters.AddWithValue("@5", oDate)
                        cashiering_cheque_Cmd.ExecuteNonQuery()
                    End Using
                Else

                End If

                Using cashiering_Cmd As MySqlCommand = cn.CreateCommand()
                    cashiering_Cmd.Transaction = cashiering_transaction
                    cashiering_Cmd.CommandText = "INSERT INTO tbl_cashiering (csh_ornumber, csh_period_id, csh_stud_id, csh_total_amount, csh_amount_received, csh_amount_change, csh_date, csh_notes, csh_cashier_id, csh_type, amount_balance) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)"
                    cashiering_Cmd.Parameters.AddWithValue("@1", .txtOR.Text)
                    cashiering_Cmd.Parameters.AddWithValue("@2", CInt(.cbAcademicYear.SelectedValue))
                    cashiering_Cmd.Parameters.AddWithValue("@3", .StudentID)
                    cashiering_Cmd.Parameters.AddWithValue("@4", CDec(.txtAmountPaid.Text))
                    cashiering_Cmd.Parameters.AddWithValue("@5", CDec(.txtAmountReceived.Text))
                    cashiering_Cmd.Parameters.AddWithValue("@6", CDec(.txtAmountChange.Text))
                    cashiering_Cmd.Parameters.AddWithValue("@7", oDate)
                    cashiering_Cmd.Parameters.AddWithValue("@8", "Payment For " & .cbAcademicYear.Text & ".")
                    cashiering_Cmd.Parameters.AddWithValue("@9", str_userid)
                    cashiering_Cmd.Parameters.AddWithValue("@10", .cbPaymentMethod)
                    cashiering_Cmd.Parameters.AddWithValue("@11", CDec(.txtAcadBalance.Text) - CDec(.txtAmountPaid.Text))
                    cashiering_Cmd.ExecuteNonQuery()
                End Using

                cashiering_transaction.Commit()

                MessageBox.Show("Transaction successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                UserActivity("Settled student " & .txtStudent.Text & " payment for Academic Year " & .cbAcademicYear.Text & " with an OR no.:" & .txtOR.Text & " and amount:" & Format(CDec(.txtAmountPaid.Text), "#,##0.00") & "", "CASHIERING")
                .ClearAll()
            Catch ex As Exception
                cashiering_transaction.Rollback()
                MessageBox.Show("Transaction failed. Transaction rolled back.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                cn.Close()
            End Try
        End With

    End Sub


    Public Sub PerformPreCashieringTransaction()
        With frmCashiering
            cn.Close()
            cn.Open()
            Dim iDate As String = DateToday
            Dim oDate As DateTime = Convert.ToDateTime(iDate)

            Dim cashiering_transaction As MySqlTransaction = cn.BeginTransaction()
            Try
                'cn.Open()
                If .cbPaymentMethod.Text = "CHEQUE" Then
                    Using cashiering_cheque_Cmd As MySqlCommand = cn.CreateCommand()
                        cashiering_cheque_Cmd.Transaction = cashiering_transaction
                        cashiering_cheque_Cmd.CommandText = "INSERT INTO tbl_cashiering_cheque (chq_ornumber, chq_no, chq_bankname, chq_bankbranch, chq_date) values (@1, @2, @3, @4, @5)"

                        cashiering_cheque_Cmd.Parameters.AddWithValue("@1", .txtOR.Text)
                        cashiering_cheque_Cmd.Parameters.AddWithValue("@2", .ChequeNo)
                        cashiering_cheque_Cmd.Parameters.AddWithValue("@3", .ChequeBankName)
                        cashiering_cheque_Cmd.Parameters.AddWithValue("@4", .ChequeBankBranch)
                        cashiering_cheque_Cmd.Parameters.AddWithValue("@5", oDate)
                        cashiering_cheque_Cmd.ExecuteNonQuery()
                    End Using
                Else
                End If

                Using precashiering_Cmd As MySqlCommand = cn.CreateCommand()
                    precashiering_Cmd.Transaction = cashiering_transaction
                    precashiering_Cmd.CommandText = "INSERT INTO tbl_pre_cashiering (ornumber, student_id, pre_cashier_notes, approved_by_id, approved_by_id_datetime, amount_paid, amount_received, amount_change, period_id, ps_course_id, ps_yrlvl, ps_ass_id, pre_type) values (@ornumber, @student_id, @pre_cashier_notes, @approved_by_id, @approved_by_id_datetime, @amount_paid, @amount_received, @amount_change, @period_id, @ps_course_id, @ps_yrlvl, @ps_ass_id, @pre_type)"
                    precashiering_Cmd.Parameters.AddWithValue("@ornumber", .txtOR.Text)
                    precashiering_Cmd.Parameters.AddWithValue("@student_id", .StudentID)
                    precashiering_Cmd.Parameters.AddWithValue("@pre_cashier_notes", "Down payment For " & .cbAcademicYear.Text & ".")
                    precashiering_Cmd.Parameters.AddWithValue("@approved_by_id", str_userid)
                    precashiering_Cmd.Parameters.AddWithValue("@approved_by_id_datetime", oDate)
                    precashiering_Cmd.Parameters.AddWithValue("@amount_paid", CDec(.txtAmountPaid.Text))
                    precashiering_Cmd.Parameters.AddWithValue("@amount_received", CDec(.txtAmountReceived.Text))
                    precashiering_Cmd.Parameters.AddWithValue("@amount_change", CDec(.txtAmountChange.Text))
                    precashiering_Cmd.Parameters.AddWithValue("@period_id", CInt(.cbAcademicYear.SelectedValue))
                    precashiering_Cmd.Parameters.AddWithValue("@ps_course_id", .CourseID)
                    precashiering_Cmd.Parameters.AddWithValue("@ps_yrlvl", .YearLevel)
                    precashiering_Cmd.Parameters.AddWithValue("@ps_ass_id", .StudentAssessmentID)
                    precashiering_Cmd.Parameters.AddWithValue("@pre_type", .cbPaymentMethod.Text)
                    precashiering_Cmd.ExecuteNonQuery()
                End Using

                Using cashiering_Cmd As MySqlCommand = cn.CreateCommand()
                    cashiering_Cmd.Transaction = cashiering_transaction
                    cashiering_Cmd.CommandText = "INSERT INTO tbl_cashiering (csh_ornumber, csh_period_id, csh_stud_id, csh_total_amount, csh_amount_received, csh_amount_change, csh_date, csh_notes, csh_cashier_id, csh_type, amount_balance) values (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)"
                    cashiering_Cmd.Parameters.AddWithValue("@1", .txtOR.Text)
                    cashiering_Cmd.Parameters.AddWithValue("@2", CInt(.cbAcademicYear.SelectedValue))
                    cashiering_Cmd.Parameters.AddWithValue("@3", .StudentID)
                    cashiering_Cmd.Parameters.AddWithValue("@4", CDec(.txtAmountPaid.Text))
                    cashiering_Cmd.Parameters.AddWithValue("@5", CDec(.txtAmountReceived.Text))
                    cashiering_Cmd.Parameters.AddWithValue("@6", CDec(.txtAmountChange.Text))
                    cashiering_Cmd.Parameters.AddWithValue("@7", oDate)
                    cashiering_Cmd.Parameters.AddWithValue("@8", "Down payment For " & .cbAcademicYear.Text & ".")
                    cashiering_Cmd.Parameters.AddWithValue("@9", str_userid)
                    cashiering_Cmd.Parameters.AddWithValue("@10", .cbPaymentMethod.Text)
                    cashiering_Cmd.Parameters.AddWithValue("@11", CDec(.txtAcadBalance.Text) - CDec(.txtAmountPaid.Text))
                    cashiering_Cmd.ExecuteNonQuery()
                End Using

                cashiering_transaction.Commit()

                MessageBox.Show("Transaction successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                UserActivity("Settled student " & .txtStudent.Text & " down payment for Academic Year " & .cbAcademicYear.Text & " with an OR no.:" & .txtOR.Text & " and amount:" & Format(CDec(.txtAmountPaid.Text), "#,##0.00") & "", "CASHIERING")
                .ClearAll()
            Catch ex As Exception
                cashiering_transaction.Rollback()
                MessageBox.Show("Transaction failed. Transaction rolled back.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finally
                cn.Close()
            End Try

        End With

    End Sub

    Public Sub PaymentMonitoringStudentList()
        Try
            frmPaymentMonitoringUpdate.dgStudentList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course' from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (s_ln like '%" & frmPaymentMonitoringUpdate.txtSearch.Text & "%' or s_fn like '%" & frmPaymentMonitoringUpdate.txtSearch.Text & "%' or s_mn like '%" & frmPaymentMonitoringUpdate.txtSearch.Text & "%' or s_id_no like '%" & frmPaymentMonitoringUpdate.txtSearch.Text & "%' or course_code like '%" & frmPaymentMonitoringUpdate.txtSearch.Text & "%' or s_yr_lvl like '%" & frmPaymentMonitoringUpdate.txtSearch.Text & "%') order by s_id_no asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmPaymentMonitoringUpdate.dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString)
            End While
            dr.Close()
            cn.Close()

            If frmMain.systemModule.Text = "College Module" Then
                frmPaymentMonitoringUpdate.dgStudentList.Columns(8).HeaderText = "Course"
            Else
                frmPaymentMonitoringUpdate.dgStudentList.Columns(8).HeaderText = "Strand/Grade"
            End If

            dgPanelPadding(frmPaymentMonitoringUpdate.dgStudentList, frmPaymentMonitoringUpdate.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmPaymentMonitoringUpdate.dgStudentList.Rows.Clear()
        End Try
    End Sub


    Public Sub AdjustmentStudentList()
        Try
            frmAdjustments.dgStudentList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, course_name, s_course_status from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (tbl_student.s_ln like '" & frmAdjustments.txtSearch.Text & "%' or tbl_student.s_fn like '" & frmAdjustments.txtSearch.Text & "%' or tbl_student.s_mn like '" & frmAdjustments.txtSearch.Text & "%' or tbl_student.s_id_no like '" & frmAdjustments.txtSearch.Text & "%' or tbl_student.s_yr_lvl like '" & frmAdjustments.txtSearch.Text & "%') order by s_id_no asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmAdjustments.dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("course_name").ToString, dr.Item("s_course_status").ToString)
            End While
            dr.Close()
            cn.Close()

            If frmMain.systemModule.Text = "College Module" Then
                frmAdjustments.dgStudentList.Columns(8).HeaderText = "Course"
            Else
                frmAdjustments.dgStudentList.Columns(8).HeaderText = "Strand/Grade"
            End If

            dgPanelPadding(frmAdjustments.dgStudentList, frmAdjustments.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmAdjustments.dgStudentList.Rows.Clear()
        End Try
    End Sub

    Public Sub AdjustmentCourseList()
        Try
            frmAdjustments.dgCourseList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select course_id, course_code, course_name, course_major, course_status from tbl_course where (course_code LIKE '%" & frmAdjustments.txtSearch.Text & "%' or course_name LIKE '%" & frmAdjustments.txtSearch.Text & "%') order by course_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                frmAdjustments.dgCourseList.Rows.Add(dr.Item("course_id").ToString, dr.Item("course_code").ToString, dr.Item("course_name").ToString, dr.Item("course_major").ToString, dr.Item("course_status").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmAdjustments.dgCourseList, frmAdjustments.dgPanel)
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmAdjustments.dgCourseList.Rows.Clear()
        End Try
    End Sub

#End Region
#Region "Registrar"
    Public Sub AcknowledgementList()
        With frmReqAck_Inbound
            Try
                .dgAckList.Rows.Clear()
                Dim sql As String
                If .CheckBox1.Checked = True Then
                    If .txtSearch.Text = String.Empty Then
                        sql = "SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),'') as user, t1.`received_datetime`, t1.forwarded_to, ifnull(t1.received_date,'') as 'Date Received', t1.`rcpt_code` as 'Receipt Code' FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where received_date between '" & .dtFrom.Text & "' and  '" & .dtTo.Text & "' limit 500"
                    ElseIf .cbFilter.Text = "Document" Then
                        sql = "SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),'') as user, t1.`received_datetime`, t1.forwarded_to, ifnull(t1.received_date,'') as 'Date Received', t1.`rcpt_code` as 'Receipt Code' FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where (t2.doc_code LIKE '%" & .txtSearch.Text & "%' or t2.doc_description LIKE '%" & .txtSearch.Text & "%') and received_date between '" & .dtFrom.Text & "' and  '" & .dtTo.Text & "' limit 500"
                    ElseIf .cbFilter.Text = "Reference Number" Then
                        sql = "SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),'') as user, t1.`received_datetime`, t1.forwarded_to, ifnull(t1.received_date,'') as 'Date Received', t1.`rcpt_code` as 'Receipt Code' FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where t1.`ref_no` LIKE '%" & .txtSearch.Text & "%' and received_date between '" & .dtFrom.Text & "' and  '" & .dtTo.Text & "' limit 500"
                    ElseIf .cbFilter.Text = "Student" Then
                        sql = "SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),'') as user, t1.`received_datetime`, t1.forwarded_to, ifnull(t1.received_date,'') as 'Date Received', t1.`rcpt_code` as 'Receipt Code' FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where (t1.`ref_student_id` LIKE '%" & .txtSearch.Text & "%' or StudentFullName LIKE '%" & .txtSearch.Text & "%') and received_date between '" & .dtFrom.Text & "' and  '" & .dtTo.Text & "' limit 500"
                    ElseIf .cbFilter.Text = "Sender" Then
                        sql = "SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),'') as user, t1.`received_datetime`, t1.forwarded_to, ifnull(t1.received_date,'') as 'Date Received', t1.`rcpt_code` as 'Receipt Code' FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where t1.`sender` LIKE '%" & .txtSearch.Text & "%' and received_date between '" & .dtFrom.Text & "' and  '" & .dtTo.Text & "' limit 500"
                    End If
                Else
                    If .txtSearch.Text = String.Empty Then
                        sql = "SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),'') as user, t1.`received_datetime`, t1.forwarded_to, ifnull(t1.received_date,'') as 'Date Received', t1.`rcpt_code` as 'Receipt Code' FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id limit 500"
                    ElseIf .cbFilter.Text = "Document" Then
                        sql = "SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),'') as user, t1.`received_datetime`, t1.forwarded_to, ifnull(t1.received_date,'') as 'Date Received', t1.`rcpt_code` as 'Receipt Code' FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where (t2.doc_code LIKE '%" & .txtSearch.Text & "%' or t2.doc_description LIKE '%" & .txtSearch.Text & "%') limit 500"
                    ElseIf .cbFilter.Text = "Reference Number" Then
                        sql = "SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),'') as user, t1.`received_datetime`, t1.forwarded_to, ifnull(t1.received_date,'') as 'Date Received', t1.`rcpt_code` as 'Receipt Code' FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where t1.`ref_no` LIKE '%" & .txtSearch.Text & "%' limit 500"
                    ElseIf .cbFilter.Text = "Student" Then
                        sql = "SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),'') as user, t1.`received_datetime`, t1.forwarded_to, ifnull(t1.received_date,'') as 'Date Received', t1.`rcpt_code` as 'Receipt Code' FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where (t1.`ref_student_id` LIKE '%" & .txtSearch.Text & "%' or StudentFullName LIKE '%" & .txtSearch.Text & "%') limit 500"
                    ElseIf .cbFilter.Text = "Sender" Then
                        sql = "SELECT t1.`ref_id`, t1.`ref_doc_id`, t2.doc_code as 'Docu. Code', t2.doc_description as 'Docu. Description', t1.`ref_no` as 'Reference No.', t1.`ref_student_id` as 'Student ID', StudentFullName as 'Student', t1.`ref_remarks` as 'Remarks', t1.`sender` as 'Sender', t1.`pages`, t1.`attachments`, ifnull(CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name),'') as user, t1.`received_datetime`, t1.forwarded_to, ifnull(t1.received_date,'') as 'Date Received', t1.`rcpt_code` as 'Receipt Code' FROM `tbl_documents_reference` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.received_by = t4.ua_id where t1.`sender` LIKE '%" & .txtSearch.Text & "%' limit 500"
                    End If
                End If
                cn.Close()
                cn.Open()
                cm = New MySqlCommand(sql, cn)
                dr = cm.ExecuteReader
                While dr.Read
                    .dgAckList.Rows.Add(dr.Item("ref_id").ToString, dr.Item("ref_doc_id").ToString, dr.Item("Docu. Code").ToString, dr.Item("Docu. Description").ToString, dr.Item("Reference No.").ToString, dr.Item("Student ID").ToString, dr.Item("Student").ToString, dr.Item("Remarks").ToString, dr.Item("Sender").ToString, dr.Item("pages").ToString, dr.Item("attachments").ToString, dr.Item("user").ToString, dr.Item("received_datetime").ToString, dr.Item("forwarded_to").ToString, dr.Item("Date Received").ToString, dr.Item("Receipt Code").ToString)
                End While
                dr.Close()
                cn.Close()
                dgPanelPadding(.dgAckList, .dgPanel)

            Catch ex As Exception
                dr.Close()
                cn.Close()
                .dgAckList.Rows.Clear()
            End Try
        End With
    End Sub

    Public Sub AcknowledgementList2()
        With frmReqAck_Outbound
            Try

                .dgAckList.Rows.Clear()
                Dim sql As String
                If .CheckBox1.Checked = True Then
                    If .txtSearch.Text = String.Empty Then
                        sql = "SELECT t1.`ref_code` as 'Code', t2.doc_description as 'Credential', t5.schl_name as 'School', StudentFullName as 'Student', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', ' - ',DATE_FORMAT(t1.`ref_released_date`,'%c/%e/%Y')) as 'Release Date', CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name) as 'Created', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where t1.ref_date between '" & .dtFrom.Text & "' and  '" & .dtTo.Text & "' limit 500"
                    ElseIf .cbFilter.Text = "Credential" Then
                        sql = "SELECT t1.`ref_code` as 'Code', t2.doc_description as 'Credential', t5.schl_name as 'School', StudentFullName as 'Student', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', ' - ',DATE_FORMAT(t1.`ref_released_date`,'%c/%e/%Y')) as 'Release Date', CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name) as 'Created', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where t2.doc_description LIKE '%" & .txtSearch.Text & "%' and t1.ref_date between '" & .dtFrom.Text & "' and  '" & .dtTo.Text & "' limit 500"
                    ElseIf .cbFilter.Text = "Code" Then
                        sql = "SELECT t1.`ref_code` as 'Code', t2.doc_description as 'Credential', t5.schl_name as 'School', StudentFullName as 'Student', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', ' - ',DATE_FORMAT(t1.`ref_released_date`,'%c/%e/%Y')) as 'Release Date', CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name) as 'Created', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where t1.`ref_code` LIKE '%" & .txtSearch.Text & "%' and t1.ref_date between '" & .dtFrom.Text & "' and  '" & .dtTo.Text & "' limit 500"
                    ElseIf .cbFilter.Text = "Student" Then
                        sql = "SELECT t1.`ref_code` as 'Code', t2.doc_description as 'Credential', t5.schl_name as 'School', StudentFullName as 'Student', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', ' - ',DATE_FORMAT(t1.`ref_released_date`,'%c/%e/%Y')) as 'Release Date', CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name) as 'Created', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where (t1.`ref_student_id` LIKE '%" & .txtSearch.Text & "%' or StudentFullName LIKE '%" & .txtSearch.Text & "%') and t1.ref_date between '" & .dtFrom.Text & "' and  '" & .dtTo.Text & "' limit 500"
                    ElseIf .cbFilter.Text = "School" Then
                        sql = "SELECT t1.`ref_code` as 'Code', t2.doc_description as 'Credential', t5.schl_name as 'School', StudentFullName as 'Student', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', ' - ',DATE_FORMAT(t1.`ref_released_date`,'%c/%e/%Y')) as 'Release Date', CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name) as 'Created', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where t5.`schl_name` LIKE '%" & .txtSearch.Text & "%' and t1.ref_date between '" & .dtFrom.Text & "' and  '" & .dtTo.Text & "' limit 500"
                    End If
                Else
                    If .txtSearch.Text = String.Empty Then
                        sql = "SELECT t1.`ref_code` as 'Code', t2.doc_description as 'Credential', t5.schl_name as 'School', StudentFullName as 'Student', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', ' - ',DATE_FORMAT(t1.`ref_released_date`,'%c/%e/%Y')) as 'Release Date', CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name) as 'Created', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id limit 500"
                    ElseIf .cbFilter.Text = "Credential" Then
                        sql = "SELECT t1.`ref_code` as 'Code', t2.doc_description as 'Credential', t5.schl_name as 'School', StudentFullName as 'Student', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', ' - ',DATE_FORMAT(t1.`ref_released_date`,'%c/%e/%Y')) as 'Release Date', CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name) as 'Created', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where t2.doc_description LIKE '%" & .txtSearch.Text & "%' limit 500"
                    ElseIf .cbFilter.Text = "Code" Then
                        sql = "SELECT t1.`ref_code` as 'Code', t2.doc_description as 'Credential', t5.schl_name as 'School', StudentFullName as 'Student', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', ' - ',DATE_FORMAT(t1.`ref_released_date`,'%c/%e/%Y')) as 'Release Date', CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name) as 'Created', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where t1.`ref_code` LIKE '%" & .txtSearch.Text & "%' limit 500"
                    ElseIf .cbFilter.Text = "Student" Then
                        sql = "SELECT t1.`ref_code` as 'Code', t2.doc_description as 'Credential', t5.schl_name as 'School', StudentFullName as 'Student', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', ' - ',DATE_FORMAT(t1.`ref_released_date`,'%c/%e/%Y')) as 'Release Date', CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name) as 'Created', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where (t1.`ref_student_id` LIKE '%" & .txtSearch.Text & "%' or StudentFullName LIKE '%" & .txtSearch.Text & "%') limit 500"
                    ElseIf .cbFilter.Text = "School" Then
                        sql = "SELECT t1.`ref_code` as 'Code', t2.doc_description as 'Credential', t5.schl_name as 'School', StudentFullName as 'Student', t1.`ref_status` as 'Status', if(t1.`ref_released_date` = '0000-00-00', ' - ',DATE_FORMAT(t1.`ref_released_date`,'%c/%e/%Y')) as 'Release Date', CONCAT(t4.ua_first_name, ' ', LEFT(t4.ua_middle_name,1), '. ', t4.ua_last_name) as 'Created', DATE_FORMAT(t1.`ref_date`,'%c/%e/%Y') as 'Date', t1.`ref_remarks` as 'Remarks' FROM `tbl_documents_reference_out` t1 JOIN tbl_documents t2 ON t1.ref_doc_id = t2.doc_id LEFT JOIN students t3 ON t1.ref_student_id = t3.StudentID LEFT JOIN tbl_user_account t4 ON t1.ref_user = t4.ua_id LEFT JOIN tbl_schools t5 ON t1.ref_schoold_id = t5.schl_id where t5.`schl_name` LIKE '%" & .txtSearch.Text & "%' limit 500"
                    End If
                End If
                cn.Close()
                cn.Open()
                cm = New MySqlCommand(sql, cn)
                dr = cm.ExecuteReader
                While dr.Read
                    .dgAckList.Rows.Add(dr.Item("Code").ToString, dr.Item("Credential").ToString, dr.Item("School").ToString, dr.Item("Student").ToString, dr.Item("Status").ToString, dr.Item("Release Date").ToString, dr.Item("Created").ToString, dr.Item("Date").ToString, dr.Item("Remarks").ToString)
                End While
                dr.Close()
                cn.Close()
                dgPanelPadding(.dgAckList, .dgPanel)

                For Each row As DataGridViewRow In .dgAckList.Rows
                    If row.Cells(3).Value = "Pending" Then
                        row.DefaultCellStyle.ForeColor = Color.Red
                        row.DefaultCellStyle.SelectionForeColor = Color.Red
                    Else

                    End If
                Next

            Catch ex As Exception
                dr.Close()
                cn.Close()
                .dgAckList.Rows.Clear()
            End Try
        End With
    End Sub

    Public Sub LoadGradingStudentList()
        Try

            frmStudentGradeEditor.dgStudentList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, course_name, course_sector from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (s_ln like '%" & frmStudentGradeEditor.txtSearch.Text & "%' or s_fn like '%" & frmStudentGradeEditor.txtSearch.Text & "%' or s_mn like '%" & frmStudentGradeEditor.txtSearch.Text & "%' or s_id_no like '%" & frmStudentGradeEditor.txtSearch.Text & "%' or course_code like '%" & frmStudentGradeEditor.txtSearch.Text & "%' or s_yr_lvl like '%" & frmStudentGradeEditor.txtSearch.Text & "%') order by s_id_no asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmStudentGradeEditor.dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("course_name").ToString, dr.Item("course_sector").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmStudentGradeEditor.dgStudentList, frmStudentGradeEditor.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmStudentGradeEditor.dgStudentList.Rows.Clear()
        End Try
    End Sub

    Public Sub LoadGradingSchoolList()
        Try

            frmStudentGradeEditor.dgSchoolList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select * from tbl_schools where (schl_code LIKE '%" & frmStudentGradeEditor.txtSearch.Text & "%' or schl_name LIKE '%" & frmStudentGradeEditor.txtSearch.Text & "%') order by schl_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmStudentGradeEditor.dgSchoolList.Rows.Add(i, dr.Item("schl_id").ToString, dr.Item("schl_code").ToString, dr.Item("schl_name").ToString)
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmStudentGradeEditor.dgSchoolList.Rows.Clear()

        End Try
    End Sub


    Public Sub LoadGradeCreditingStudentList()
        Try

            cn.Close()
            cn.Open()
            frmCreditGrade.dgStudentList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, (course_name) as 'Course Desc', (course_sector) as 'Course Sector' from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (s_ln like '%" & frmCreditGrade.txtSearch.Text & "%' or s_fn like '%" & frmCreditGrade.txtSearch.Text & "%' or s_mn like '%" & frmCreditGrade.txtSearch.Text & "%' or s_id_no like '%" & frmCreditGrade.txtSearch.Text & "%') order by s_id_no asc limit 250"
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmCreditGrade.dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("Course Desc").ToString, dr.Item("Course Sector").ToString)
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmCreditGrade.dgStudentList.Rows.Clear()

        End Try
    End Sub

    Public Sub LoadGradeCreditingSchoolList()
        Try

            frmCreditGrade.dgSchoolList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select * from tbl_schools where (schl_code LIKE '%" & frmCreditGrade.txtSearch.Text & "%' or schl_name LIKE '%" & frmCreditGrade.txtSearch.Text & "%') order by schl_name asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmCreditGrade.dgSchoolList.Rows.Add(i, dr.Item("schl_id").ToString, dr.Item("schl_code").ToString, dr.Item("schl_name").ToString, dr.Item("schl_address").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmCreditGrade.dgSchoolList.Rows.Clear()

        End Try
    End Sub

    Public Sub LoadGradeCreditingCourseList()
        Try

            frmCreditGrade.dgCourseList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select course_id, course_code, course_name, course_major, course_status from tbl_course where (course_code LIKE '%" & frmCreditGrade.txtSearch.Text & "%' or course_name LIKE '%" & frmCreditGrade.txtSearch.Text & "%') order by course_name asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmCreditGrade.dgCourseList.Rows.Add(i, dr.Item("course_id").ToString, dr.Item("course_code").ToString, dr.Item("course_name").ToString, dr.Item("course_major").ToString, dr.Item("course_status").ToString)
        End While
        dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmCreditGrade.dgCourseList.Rows.Clear()

        End Try
    End Sub

    Public Sub LoadGradeCreditingSubjectList()
        Try

            frmCreditGrade.dgSubjectList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (b.subject_id) as 'ID', (b.subject_code) as 'Code', (b.subject_description) as 'Description', (b.subject_Type) as 'Type', (b.subject_group) as 'Group', (b.subject_units) as 'Units', CONCAT(a.subject_description,'-',a.subject_code) as 'Prerequisite', (b.subject_active_status) as 'Status' from tbl_subject b LEFT JOIN tbl_subject a ON a.subject_id = b.subject_prerequisite where (b.subject_code LIKE '" & frmCreditGrade.txtSearch.Text & "%' or b.subject_description LIKE '" & frmCreditGrade.txtSearch.Text & "%') order by b.subject_description asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmCreditGrade.dgSubjectList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString, dr.Item("Type").ToString, dr.Item("Group").ToString, dr.Item("Units").ToString, dr.Item("Prerequisite").ToString, dr.Item("Status").ToString)
            End While
            dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmCreditGrade.dgSubjectList.Rows.Clear()
        End Try
    End Sub


    Public Sub LoadStudentGrades()
        Try

            cn2.Close()
            cn2.Open()
            Dim i As Integer
            Dim sql As String
            sql = "select subject_id, (subject_code) as 'CODE', (subject_description) as 'DESCRIPTION', (sg_grade) as 'GRADES', (sg_credits) as 'CREDIT', (sg_grade_status) as 'STATUS', (sg_course_id) as 'Course_id', CONCAT(course_code, ' - ', course_name) as 'Course', sg_grade_remarks, schl_name, sg_yearlevel, sg_grade_visibility, sg_id, sg_school_id, subject_units from tbl_students_grades, tbl_subject, period, tbl_schools, tbl_course where tbl_students_grades.sg_subject_id = tbl_subject.subject_id and tbl_students_grades.sg_period_id = period.period_id and tbl_students_grades.sg_course_id = tbl_course.course_id and tbl_students_grades.sg_school_id = tbl_schools.schl_id and sg_student_id = '" & frmStudentGradeEditor.studentId & "' and sg_period_id = " & CInt(frmStudentGradeEditor.cbAcademicYear.SelectedValue) & " and sg_grade_status NOT IN ('Pending')"
            cm2 = New MySqlCommand(sql, cn2)
            dr2 = cm2.ExecuteReader
            While dr2.Read
                i += 1
                frmStudentGradeEditor.dgStudentGrades.Rows.Add(i, dr2.Item("subject_id").ToString, dr2.Item("CODE").ToString, dr2.Item("DESCRIPTION").ToString, dr2.Item("GRADES").ToString, dr2.Item("CREDIT").ToString, dr2.Item("STATUS").ToString, dr2.Item("Course_id").ToString, dr2.Item("Course").ToString, dr2.Item("sg_grade_remarks").ToString, dr2.Item("schl_name").ToString, dr2.Item("sg_yearlevel").ToString, dr2.Item("sg_grade_visibility").ToString, dr2.Item("sg_id").ToString, dr2.Item("subject_units").ToString)
            End While
            dr2.Close()
            cn2.Close()

        Catch ex As Exception
            dr2.Close()
            cn2.Close()
            frmStudentGradeEditor.dgStudentGrades.Rows.Clear()
        End Try
    End Sub

    Public Sub LoadClassStudentGrades()
        Try

            frmClassGradeEditor.dgStudentList.Rows.Clear()
            cn2.Close()
            cn2.Open()
            Dim i As Integer
            Dim sql As String
            sql = "SELECT t1.sg_student_id, CONCAT(t2.s_ln, ', ', t2.s_fn, ' ', t2.s_mn) as Name, `sg_grade_prelim`, `sg_grade_midterm`, `sg_grade_semi`, `sg_grade_final`, `sg_grade_avg`, `sg_grade`, `sg_credits` FROM `tbl_students_grades` t1 JOIN tbl_student t2 on t1.sg_student_id = t2.s_id_no WHERE t1.sg_class_id = " & CInt(frmClassGradeEditor.classID) & " order by t2.s_ln asc, t2.s_fn asc"
            cm2 = New MySqlCommand(sql, cn2)
            dr2 = cm2.ExecuteReader
            While dr2.Read
                i += 1
                frmClassGradeEditor.dgStudentList.Rows.Add(i, dr2.Item("sg_student_id").ToString, dr2.Item("Name").ToString, dr2.Item("sg_grade_prelim").ToString, dr2.Item("sg_grade_midterm").ToString, dr2.Item("sg_grade_semi").ToString, dr2.Item("sg_grade_final").ToString, dr2.Item("sg_grade_avg").ToString, dr2.Item("sg_grade").ToString, dr2.Item("sg_credits").ToString)
            End While
            dr2.Close()
            cn2.Close()

        Catch ex As Exception
            dr2.Close()
            cn2.Close()
            frmClassGradeEditor.dgStudentList.Rows.Clear()
        End Try
    End Sub
#End Region
#Region "Enrollment"
    Public Sub CurriculumList()
        Try

            frmCurriculumList.dgCurrList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select curriculum_id, (curriculum_code) as 'Curriculum', (course_code) as 'Course', (course_name) as 'CourseDescription', (is_active) as 'Status' from tbl_curriculum JOIN tbl_course JOIN tbl_user_account where tbl_curriculum.prepared_by_id = tbl_user_account.ua_id and tbl_curriculum.curr_course_id = tbl_course.course_id and curriculum_code LIKE '%" & frmMain.txtSearch.Text & "%'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmCurriculumList.dgCurrList.Rows.Add(i, dr.Item("curriculum_id").ToString, dr.Item("Curriculum").ToString, dr.Item("Course").ToString, dr.Item("CourseDescription").ToString, dr.Item("Status").ToString)
        End While
        dr.Close()
            cn.Close()

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmCurriculumList.dgCurrList.Rows.Clear()

        End Try
    End Sub

    Public Sub LibraryEnrollmentStudentList()
        Try

            frmEnrollStudent.dgStudentList.Rows.Clear()
            Dim i As Integer
            Dim sql As String

            If frmMain.formTitle.Text = "Enroll Class Schedule" Then
                sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, course_name, s_course_status, s_status from tbl_pre_cashiering JOIN tbl_student ON tbl_pre_cashiering.student_id = tbl_student.s_id_no JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where tbl_pre_cashiering.period_id = " & CInt(frmEnrollStudent.cbAcad.SelectedValue) & " and tbl_pre_cashiering.student_id NOT IN (SELECT `estudent_id` FROM `tbl_enrollment` WHERE `eperiod_id` = " & CInt(frmEnrollStudent.cbAcad.SelectedValue) & ") and (tbl_student.s_ln like '" & frmEnrollStudent.txtSearch.Text & "%' or tbl_student.s_fn like '" & frmEnrollStudent.txtSearch.Text & "%' or tbl_student.s_mn like '" & frmEnrollStudent.txtSearch.Text & "%' or tbl_student.s_id_no like '" & frmEnrollStudent.txtSearch.Text & "%' or tbl_student.s_yr_lvl like '" & frmEnrollStudent.txtSearch.Text & "%') order by s_id_no asc limit 500"
            ElseIf frmMain.formTitle.Text = "Update Class Schedule" Then
                sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, course_name, s_course_status, s_status from tbl_enrollment JOIN tbl_student ON tbl_enrollment.estudent_id = tbl_student.s_id_no JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where tbl_enrollment.eperiod_id = " & CInt(frmEnrollStudent.cbAcad.SelectedValue) & " and (tbl_student.s_ln like '" & frmEnrollStudent.txtSearch.Text & "%' or tbl_student.s_fn like '" & frmEnrollStudent.txtSearch.Text & "%' or tbl_student.s_mn like '" & frmEnrollStudent.txtSearch.Text & "%' or tbl_student.s_id_no like '" & frmEnrollStudent.txtSearch.Text & "%' or tbl_student.s_yr_lvl like '" & frmEnrollStudent.txtSearch.Text & "%') order by s_id_no asc limit 500"
            End If

            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmEnrollStudent.dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("course_name").ToString, dr.Item("s_course_status").ToString, dr.Item("s_status").ToString)
            End While
            dr.Close()
            cn.Close()

            If frmMain.systemModule.Text = "College Module" Then
                frmEnrollStudent.dgStudentList.Columns(8).HeaderText = "Course"
            Else
                frmEnrollStudent.dgStudentList.Columns(8).HeaderText = "Strand/Grade"
            End If

            dgPanelPadding(frmEnrollStudent.dgStudentList, frmEnrollStudent.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmEnrollStudent.dgStudentList.Rows.Clear()
        End Try
    End Sub

    Public Sub EnrollmentSubjectListPerSection()
        Try


            frmEnrollStudent.dgStudentSched.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "SELECT `tbl_class_schedule`.`class_schedule_id` AS `ID`, `tbl_class_block`.`cb_code` as 'Class', `period`.`PERIOD` as 'Period', CONCAT(`tbl_class_block`.`cb_code`, ' - ', `period`.`PERIOD`) AS `Code`, CONCAT(`tbl_subject`.`subject_code`, ' - ', `tbl_subject`.`subject_description` ) AS `Subject`, `tbl_subject`.`subject_units` AS `Units`, if(`tbl_day_schedule`.`ds_code` = 'M T W TH F SAT SUN', 'DAILY', `tbl_day_schedule`.`ds_code`) AS `Day Schedule`, `tbl_class_schedule`.`time_start_schedule` AS `Time Start`, `tbl_class_schedule`.`time_end_schedule` as `Time End`, `tbl_room`.`room_code` AS `Room`, CONCAT( `tbl_employee`.`emp_last_name`, ', ', `tbl_employee`.`emp_first_name`, ' ', `tbl_employee`.`emp_middle_name` ) AS `Instructor`, `tbl_class_schedule`.`population` AS `Population`, period_id, if(tbl_class_schedule.class_status = 'Merged', CONCAT(tbl_class_schedule.is_active,' - ',tbl_class_schedule.class_status), tbl_class_schedule.is_active) as 'Status' FROM (((((`tbl_class_schedule`JOIN `tbl_class_block`)JOIN `tbl_subject`)JOIN `tbl_day_schedule`)JOIN `tbl_room`)JOIN `tbl_employee`)JOIN `period` WHERE `tbl_class_schedule`.`class_block_id` = `tbl_class_block`.`cb_id` AND `tbl_class_schedule`.`cssubject_id` = `tbl_subject`.`subject_id` AND `tbl_class_schedule`.`days_schedule` = `tbl_day_schedule`.`ds_id` AND `tbl_class_schedule`.`csroom_id` = `tbl_room`.`room_id` AND `tbl_class_schedule`.`csemp_id` = `tbl_employee`.`emp_id` and `tbl_class_schedule`.`csperiod_id` = `period`.`period_id` and period_id = " & CInt(frmEnrollStudent.cbAcad.SelectedValue) & " and cb_id = " & CInt(frmEnrollStudent.cbSection.SelectedValue) & ""
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read

                frmEnrollStudent.dgStudentSched.Rows.Add(dr.Item("ID").ToString, dr.Item("Class").ToString, dr.Item("Code").ToString, dr.Item("Subject").ToString, dr.Item("Units").ToString, dr.Item("Day Schedule").ToString, dr.Item("Time Start").ToString, dr.Item("Time End").ToString, dr.Item("Room").ToString, dr.Item("Instructor").ToString, dr.Item("Population").ToString, "", dr.Item("period_id").ToString, dr.Item("status").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmEnrollStudent.dgStudentSched.Rows.Clear()
        End Try
    End Sub

    Public Sub LibraryEnrollmentClassSchedList()
        Try

            frmEnrollStudent.dgClassSchedList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.None
            frmEnrollStudent.dgClassSchedList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "SELECT t1.`class_schedule_id`, t1.`cb_code`, t1.`subject_code`, t1.`subject_description`, t1.`subject_units`, t1.`ds_code`, t1.`time_start_schedule`, t1.`time_end_schedule`, t1.`room_code`, t1.`Instructor`, t1.`population`, t1.`csperiod_id`, t1.`is_active` FROM `classschedulelist` t1 where t1.csperiod_id = " & CInt(frmEnrollStudent.cbAcad.SelectedValue) & " and t1.`is_active` NOT IN ('Inactive') and (t1.cb_code LIKE '%" & frmMain.txtSearch.Text & "%' or t1.subject_code LIKE '%" & frmEnrollStudent.txtSearch.Text & "%' or t1.subject_description LIKE '%" & frmEnrollStudent.txtSearch.Text & "%' or t1.Instructor LIKE '%" & frmEnrollStudent.txtSearch.Text & "%') limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                frmEnrollStudent.dgClassSchedList.Rows.Add(dr.Item("class_schedule_id").ToString, dr.Item("cb_code").ToString, dr.Item("subject_code").ToString, dr.Item("subject_description").ToString, dr.Item("subject_units").ToString, dr.Item("ds_code").ToString, dr.Item("time_start_schedule").ToString, dr.Item("time_end_schedule").ToString, dr.Item("room_code").ToString, dr.Item("Instructor").ToString, dr.Item("population").ToString, "👁", dr.Item("csperiod_id").ToString, dr.Item("is_active").ToString)
            End While
            dr.Close()
            cn.Close()

            dgPanelPadding(frmEnrollStudent.dgClassSchedList, frmEnrollStudent.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmEnrollStudent.dgClassSchedList.Rows.Clear()

        End Try
    End Sub

    Public Sub LibraryEnrollmentPermitStudentList()
        Try

            frmEnrollmentPermit.dgStudentList.Rows.Clear()
            Dim i As Integer
            Dim sql As String
            sql = "select (s_id_no) as 'ID Number', (s_ln) as 'Last Name', (s_fn) as 'First Name',  (s_mn) as 'Middle Name',  (s_ext) as 'Suffix', (s_gender) as 'Gender', (s_yr_lvl) as 'Year Level', (course_code) as 'Course', course_id, course_name, s_course_status from tbl_student JOIN tbl_course ON tbl_student.s_course_id = tbl_course.course_id where (tbl_student.s_ln like '" & frmEnrollmentPermit.txtSearch.Text & "%' or tbl_student.s_fn like '" & frmEnrollmentPermit.txtSearch.Text & "%' or tbl_student.s_mn like '" & frmEnrollmentPermit.txtSearch.Text & "%' or tbl_student.s_id_no like '" & frmEnrollmentPermit.txtSearch.Text & "%' or tbl_student.s_yr_lvl like '" & frmEnrollmentPermit.txtSearch.Text & "%') order by s_id_no asc limit 500"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                frmEnrollmentPermit.dgStudentList.Rows.Add(i, dr.Item("ID Number").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, dr.Item("Suffix").ToString, dr.Item("Gender").ToString, dr.Item("Year Level").ToString, dr.Item("Course").ToString, dr.Item("course_id").ToString, dr.Item("course_name").ToString, dr.Item("s_course_status").ToString)
            End While
            dr.Close()
            cn.Close()

            If frmMain.systemModule.Text = "College Module" Then
                frmEnrollmentPermit.dgStudentList.Columns(8).HeaderText = "Course"
            Else
                frmEnrollmentPermit.dgStudentList.Columns(8).HeaderText = "Strand/Grade"
            End If

            dgPanelPadding(frmEnrollmentPermit.dgStudentList, frmEnrollmentPermit.dgPanel)

        Catch ex As Exception
            dr.Close()
            cn.Close()
            frmEnrollmentPermit.dgStudentList.Rows.Clear()

        End Try
    End Sub


#End Region


End Module
