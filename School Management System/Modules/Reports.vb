Imports MySql.Data.MySqlClient
Module Reports

    Public Sub ReportClassSchedList()
        frmReports.dgClassSchedList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.None
        frmReports.dgClassSchedList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "SELECT t1.`class_schedule_id`, t1.`cb_code`, t1.`subject_code`, t1.`subject_description`, t1.`subject_units`, t1.`ds_code`, t1.`time_start_schedule`, t1.`time_end_schedule`, t1.`room_code`, t1.`Instructor`, t1.`population`, t1.`csperiod_id`, t1.`is_active` FROM `classschedulelist` t1 where t1.csperiod_id = " & CInt(frmReports.cbAcademicYear.SelectedValue) & " and (t1.cb_code LIKE '%" & frmReports.txtSearch.Text & "%' or t1.subject_code LIKE '%" & frmReports.txtSearch.Text & "%' or t1.subject_description LIKE '%" & frmReports.txtSearch.Text & "%' or t1.Instructor LIKE '%" & frmReports.txtSearch.Text & "%') limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmReports.dgClassSchedList.Rows.Add(i, dr.Item("class_schedule_id").ToString, dr.Item("cb_code").ToString, dr.Item("subject_code").ToString, dr.Item("subject_description").ToString, dr.Item("subject_units").ToString, dr.Item("ds_code").ToString, dr.Item("time_start_schedule").ToString, dr.Item("time_end_schedule").ToString, dr.Item("room_code").ToString, dr.Item("Instructor").ToString, dr.Item("population").ToString, "👁", dr.Item("csperiod_id").ToString, dr.Item("is_active").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Public Sub ReportEmployeeList()
        frmReports.dgEmployeeList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select emp_id, (emp_code) as 'BIO No.', (emp_Last_name) as 'Last Name', (emp_first_name) as 'First Name', (emp_middle_name) as 'Middle Name', (position_id) as 'Position', (emp_status) as 'Status', required_subject_load as 'Required Subject Load (Units)' from tbl_class_schedule JOIN tbl_employee ON tbl_class_schedule.csemp_id = tbl_employee.emp_id where csperiod_id = " & CInt(frmReports.cbAcademicYear.SelectedValue) & " and (emp_code LIKE '%" & frmReports.txtSearch.Text & "%' or emp_Last_name LIKE '%" & frmReports.txtSearch.Text & "%' or emp_first_name LIKE '%" & frmReports.txtSearch.Text & "%' or emp_middle_name LIKE '%" & frmReports.txtSearch.Text & "%') group by csemp_id order by emp_Last_name asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmReports.dgEmployeeList.Rows.Add(i, dr.Item("emp_id").ToString, dr.Item("BIO No.").ToString, dr.Item("Last Name").ToString, dr.Item("First Name").ToString, dr.Item("Middle Name").ToString, "", dr.Item("Status").ToString, dr.Item("Required Subject Load (Units)").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Public Sub ReportSectionList()
        frmReports.dgSectionList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select (cb_id) as 'ID', (cb_code) as 'Code', (cb_description) as 'Description' from tbl_class_block where (cb_code LIKE '%" & frmReports.txtSearch.Text & "%' or cb_description LIKE '%" & frmReports.txtSearch.Text & "%') order by cb_description asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmReports.dgSectionList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("Description").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Public Sub ReportInstructorClassSchedList(ByVal instructorId As Integer)
        frmReports.dgClassSchedules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.None
        frmReports.dgClassSchedules.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "SELECT t1.`class_schedule_id`, t1.`cb_code`, t1.`subject_code`, t1.`subject_description`, t1.`subject_units`, t1.`ds_code`, t1.`time_start_schedule`, t1.`time_end_schedule`, t1.`room_code`, t1.`Instructor`, t1.`population`, t1.`csperiod_id`, t1.`is_active` FROM `classschedulelist` t1 where t1.csperiod_id = " & CInt(frmReports.cbAcademicYear.SelectedValue) & " and t1.csemp_id = " & instructorId & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmReports.dgClassSchedules.Rows.Add(i, dr.Item("class_schedule_id").ToString, dr.Item("cb_code").ToString, dr.Item("subject_code").ToString, dr.Item("subject_description").ToString, dr.Item("subject_units").ToString, dr.Item("ds_code").ToString, dr.Item("time_start_schedule").ToString, dr.Item("time_end_schedule").ToString, dr.Item("room_code").ToString, dr.Item("Instructor").ToString, dr.Item("population").ToString, "👁", dr.Item("csperiod_id").ToString, dr.Item("is_active").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Public Sub ReportSectionClassSchedList(ByVal sectionId As Integer)
        frmReports.dgClassSchedules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.None
        frmReports.dgClassSchedules.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "SELECT t1.`class_schedule_id`, t1.`cb_code`, t1.`subject_code`, t1.`subject_description`, t1.`subject_units`, t1.`ds_code`, t1.`time_start_schedule`, t1.`time_end_schedule`, t1.`room_code`, t1.`Instructor`, t1.`population`, t1.`csperiod_id`, t1.`is_active` FROM `classschedulelist` t1 where t1.csperiod_id = " & CInt(frmReports.cbAcademicYear.SelectedValue) & " and t1.cb_id = " & sectionId & ""
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmReports.dgClassSchedules.Rows.Add(i, dr.Item("class_schedule_id").ToString, dr.Item("cb_code").ToString, dr.Item("subject_code").ToString, dr.Item("subject_description").ToString, dr.Item("subject_units").ToString, dr.Item("ds_code").ToString, dr.Item("time_start_schedule").ToString, dr.Item("time_end_schedule").ToString, dr.Item("room_code").ToString, dr.Item("Instructor").ToString, dr.Item("population").ToString, "👁", dr.Item("csperiod_id").ToString, dr.Item("is_active").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Public Sub ReportCourseList()
        frmReports.dgCourseList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select course_id, course_code, course_name, course_major, course_status from tbl_course where (course_code LIKE '%" & frmReports.txtSearch.Text & "%' or course_name LIKE '%" & frmReports.txtSearch.Text & "%') order by course_name asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmReports.dgCourseList.Rows.Add(i, dr.Item("course_id").ToString, dr.Item("course_code").ToString, dr.Item("course_name").ToString, dr.Item("course_major").ToString, dr.Item("course_status").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Public Sub ReportSchoolList()
        frmReports.dgSchoolList.Rows.Clear()
        Dim i As Integer
        Dim sql As String
        sql = "select (schl_id) as 'ID', (schl_code) as 'Code', (schl_name) as 'School Name', (schl_address) as 'School Address' from tbl_schools where (schl_code LIKE '%" & frmReports.txtSearch.Text & "%' or schl_name LIKE '%" & frmReports.txtSearch.Text & "%') order by schl_name asc limit 500"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            frmReports.dgSchoolList.Rows.Add(i, dr.Item("ID").ToString, dr.Item("Code").ToString, dr.Item("School Name").ToString, dr.Item("School Address").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub
End Module
