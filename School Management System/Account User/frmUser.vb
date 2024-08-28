Imports MySql.Data.MySqlClient
Imports System.IO


Public Class frmUser

    Public AccountUserID As Integer = 0

    Sub HideAllDGInPanelExcept(DGToShow As DataGridView)
        For Each control As Control In PanelDG.Controls
            If TypeOf control Is DataGridView Then
                Dim DGToHide As DataGridView = DirectCast(control, DataGridView)
                DGToHide.Visible = False
            End If
        Next
        DGToShow.Visible = True
    End Sub


    Private Sub dgUserModules_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgUserModules.CellClick
        Select Case dgUserModules.CurrentRow.Cells(1).Value.ToString
            Case "Enrollment"
                HideAllDGInPanelExcept(dgEnrollmentArea)
                SystemModule_Areas(dgEnrollmentArea, dgUserModules.CurrentRow.Cells(1).Value)
            Case "Cashiering"
                HideAllDGInPanelExcept(dgCashieringArea)
                SystemModule_Areas(dgCashieringArea, dgUserModules.CurrentRow.Cells(1).Value)
            Case "Reports"
                HideAllDGInPanelExcept(dgReportsArea)
                SystemModule_Areas(dgReportsArea, dgUserModules.CurrentRow.Cells(1).Value)
            Case "Registrar"
                HideAllDGInPanelExcept(dgRegistrarArea)
                SystemModule_Areas(dgRegistrarArea, dgUserModules.CurrentRow.Cells(1).Value)
            Case "Supply"
                HideAllDGInPanelExcept(dgSupplyArea)
                SystemModule_Areas(dgSupplyArea, dgUserModules.CurrentRow.Cells(1).Value)
            Case "Database"
                HideAllDGInPanelExcept(dgDatabaseArea)
                SystemModule_Areas(dgDatabaseArea, dgUserModules.CurrentRow.Cells(1).Value)
            Case "Dashboard"
                HideAllDGInPanelExcept(dgDashboardArea)
                SystemModule_Areas(dgDashboardArea, dgUserModules.CurrentRow.Cells(1).Value)
            Case "Information Registry"
                HideAllDGInPanelExcept(dgRegistryArea)
                SystemModule_Areas(dgRegistryArea, dgUserModules.CurrentRow.Cells(1).Value)
        End Select
    End Sub

    Private Sub cbGrantAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbGrantAllAreas.CheckedChanged
        Select Case dgUserModules.CurrentRow.Cells(1).Value.ToString
            Case "Information Registry"
                CheckUncheck(dgRegistryArea)
            Case "Enrollment"
                CheckUncheck(dgEnrollmentArea)
            Case "Cashiering"
                CheckUncheck(dgCashieringArea)
            Case "Reports"
                CheckUncheck(dgReportsArea)
            Case "Registrar"
                CheckUncheck(dgRegistrarArea)
            Case "Supply"
                CheckUncheck(dgSupplyArea)
            Case "Database"
                CheckUncheck(dgDatabaseArea)
            Case "Dashboard"
                CheckUncheck(dgDashboardArea)
        End Select
    End Sub

    Sub InsertGrantedAreas()
        For Each row As DataGridViewRow In dgUserModules.Rows
            If row.Cells(2).Value = True Then
                query("insert into tbl_user_account_areas2(uaa_area_user_id, uaa_area_id)values(" & AccountUserID & ", " & row.Cells(0).Value & ")")
            Else
            End If
        Next
        For Each row As DataGridViewRow In dgRegistryArea.Rows
            If row.Cells(2).Value = True Then
                query("insert into tbl_user_account_areas2(uaa_area_user_id, uaa_area_id)values(" & AccountUserID & ", " & row.Cells(0).Value & ")")
            Else
            End If
        Next
        For Each row As DataGridViewRow In dgEnrollmentArea.Rows
            If row.Cells(2).Value = True Then
                query("insert into tbl_user_account_areas2(uaa_area_user_id, uaa_area_id)values(" & AccountUserID & ", " & row.Cells(0).Value & ")")
            Else
            End If
        Next

        For Each row As DataGridViewRow In dgCashieringArea.Rows
            If row.Cells(2).Value = True Then
                query("insert into tbl_user_account_areas2(uaa_area_user_id, uaa_area_id)values(" & AccountUserID & ", " & row.Cells(0).Value & ")")
            Else
            End If
        Next
        For Each row As DataGridViewRow In dgReportsArea.Rows
            If row.Cells(2).Value = True Then
                query("insert into tbl_user_account_areas2(uaa_area_user_id, uaa_area_id)values(" & AccountUserID & ", " & row.Cells(0).Value & ")")
            Else
            End If
        Next
        For Each row As DataGridViewRow In dgRegistrarArea.Rows
            If row.Cells(2).Value = True Then
                query("insert into tbl_user_account_areas2(uaa_area_user_id, uaa_area_id)values(" & AccountUserID & ", " & row.Cells(0).Value & ")")
            Else
            End If
        Next
        For Each row As DataGridViewRow In dgSupplyArea.Rows
            If row.Cells(2).Value = True Then
                query("insert into tbl_user_account_areas2(uaa_area_user_id, uaa_area_id)values(" & AccountUserID & ", " & row.Cells(0).Value & ")")
            Else
            End If
        Next
        For Each row As DataGridViewRow In dgDatabaseArea.Rows
            If row.Cells(2).Value = True Then
                query("insert into tbl_user_account_areas2(uaa_area_user_id, uaa_area_id)values(" & AccountUserID & ", " & row.Cells(0).Value & ")")
            Else
            End If
        Next
        For Each row As DataGridViewRow In dgDashboardArea.Rows
            If row.Cells(2).Value = True Then
                query("insert into tbl_user_account_areas2(uaa_area_user_id, uaa_area_id)values(" & AccountUserID & ", " & row.Cells(0).Value & ")")
            Else
            End If
        Next
    End Sub

    Sub SystemModules()
        dgUserModules.Rows.Clear()
        Dim sql As String
        sql = "SELECT `area_id` as ID, `area_name` as Area FROM `tbl_system_areas2` where `area_name` NOT LIKE '%-%'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgUserModules.Rows.Add(dr.Item("ID").ToString, dr.Item("Area").ToString)
        End While
        dr.Close()
        cn.Close()
        For Each row As DataGridViewRow In dgUserModules.Rows
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_user_account_areas2 where uaa_area_id = " & row.Cells(0).Value & " and uaa_area_user_id = " & AccountUserID & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                row.Cells(2).Value = True
            Else
                row.Cells(2).Value = False
            End If
            dr.Close()
            cn.Close()

            Select Case row.Cells(1).Value.ToString
                Case "Enrollment"
                    HideAllDGInPanelExcept(dgEnrollmentArea)
                    SystemModule_Areas(dgEnrollmentArea, row.Cells(1).Value)
                Case "Cashiering"
                    HideAllDGInPanelExcept(dgCashieringArea)
                    SystemModule_Areas(dgCashieringArea, row.Cells(1).Value)
                Case "Reports"
                    HideAllDGInPanelExcept(dgReportsArea)
                    SystemModule_Areas(dgReportsArea, row.Cells(1).Value)
                Case "Registrar"
                    HideAllDGInPanelExcept(dgRegistrarArea)
                    SystemModule_Areas(dgRegistrarArea, row.Cells(1).Value)
                Case "Supply"
                    HideAllDGInPanelExcept(dgSupplyArea)
                    SystemModule_Areas(dgSupplyArea, row.Cells(1).Value)
                Case "Database"
                    HideAllDGInPanelExcept(dgDatabaseArea)
                    SystemModule_Areas(dgDatabaseArea, row.Cells(1).Value)
                Case "Dashboard"
                    HideAllDGInPanelExcept(dgDashboardArea)
                    SystemModule_Areas(dgDashboardArea, row.Cells(1).Value)
                Case "Information Registry"
                    HideAllDGInPanelExcept(dgRegistryArea)
                    SystemModule_Areas(dgRegistryArea, row.Cells(1).Value)
            End Select

        Next
        dgRegistryArea.BringToFront()
        dgRegistryArea.Visible = True
    End Sub

    Sub SystemModule_Areas(dg As DataGridView, areaName As String)
        If dg.Rows.Count = 0 Then
        Else
            dg.Rows.Clear()
            Dim sql As String
            sql = "SELECT `area_id` as ID, SUBSTR(`area_name`," & CInt(areaName.ToString.Length) + 3 & ") as Area FROM `tbl_system_areas2` where `area_name` LIKE '%" & areaName & " -%'"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                dg.Rows.Add(dr.Item("ID").ToString, dr.Item("Area").ToString)
            End While
            dr.Close()

            For Each row As DataGridViewRow In dg.Rows
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select * from tbl_user_account_areas2 where uaa_area_id = " & row.Cells(0).Value & " and uaa_area_user_id = " & AccountUserID & "", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    row.Cells(2).Value = True
                Else
                    row.Cells(2).Value = False
                End If
                dr.Close()
                cn.Close()
            Next
        End If
        Try
            Dim allChecked As Boolean = True
            For Each row As DataGridViewRow In dg.Rows
                If Convert.ToBoolean(row.Cells(2).Value) = True Then
                    allChecked = True
                Else
                    allChecked = False
                    Exit For
                End If
            Next
            If allChecked = False Then
                cbGrantAllAreas.Checked = False
            Else
                cbGrantAllAreas.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(txtFirstName) = True Then Return
        If IS_EMPTY(txtLastName) = True Then Return
        If IS_EMPTY(txtUsername) = True Then Return
        If IS_EMPTY(txtPassword) = True Then Return
        If CHECK_EXISTING("SELECT * FROM tbl_user_account WHERE ua_user_name = '" & txtUsername.Text.Trim & "'") = True Then Return

        If MsgBox("Are you sure you want to add this user account?", vbYesNo + vbQuestion) = vbYes Then
            Dim mstream As New MemoryStream
            userPhoto.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim arrImage() As Byte = mstream.GetBuffer
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("Insert into tbl_user_account(ua_first_name, ua_middle_name, ua_last_name, ua_address, ua_contact, ua_account_type, ua_registered_by_id, ua_user_name, ua_password, ua_photo)values('" & txtFirstName.Text & "'," & txtMiddleName.Text & "'," & txtLastName.Text & "','" & txtLastName.Text & "', '" & txtAddress.Text & "', '" & txtContact.Text & "', '" & cbAccountType.Text & "', " & str_userid & ", '" & txtUsername.Text & "', '" & txtPassword.Text & "', @image)", cn)
            cm.Parameters.AddWithValue("@image", arrImage)
            cm.ExecuteNonQuery()
            cn.Close()
            UserActivity("Added a user account " & txtFirstName.Text.Trim & " " & txtMiddleName.Text.Trim & " " & txtLastName.Text.Trim & ". Username: " & txtUsername.Text & "", "USER ACCOUNT")
            query("delete from tbl_user_account_areas2 WHERE uaa_area_user_id = " & AccountUserID & "")
            InsertGrantedAreas()
            frmWait.seconds = 1
            frmWait.ShowDialog()
            MsgBox("New user account has been successfully saved/added.", vbInformation, "")
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txtPassword.UseSystemPasswordChar = False
        Else
            txtPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Sub CheckUncheck(dg As DataGridView)
        If cbGrantAllAreas.Checked = True Then
            For Each row As DataGridViewRow In dg.Rows
                row.Cells(2).Value = True
            Next
        Else
            For Each row As DataGridViewRow In dg.Rows
                row.Cells(2).Value = False
            Next
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If IS_EMPTY(txtFirstName) = True Then Return
        If IS_EMPTY(txtLastName) = True Then Return
        If IS_EMPTY(txtUsername) = True Then Return
        If IS_EMPTY(txtPassword) = True Then Return

        If MsgBox("Are you sure you want to update this user account?", vbYesNo + vbQuestion) = vbYes Then
            Dim mstream As New MemoryStream
            userPhoto.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim arrImage() As Byte = mstream.GetBuffer
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("update tbl_user_account set ua_first_name = '" & txtFirstName.Text & "', ua_middle_name ='" & txtMiddleName.Text & "', ua_last_name = '" & txtLastName.Text & "', ua_address = '" & txtAddress.Text & "', ua_contact = '" & txtContact.Text & "', ua_account_type = '" & cbAccountType.Text & "', ua_password = '" & txtPassword.Text & "', ua_photo = @image where ua_id = " & AccountUserID & "", cn)
            cm.Parameters.AddWithValue("@image", arrImage)
            cm.ExecuteNonQuery()
            cn.Close()
            query("delete from tbl_user_account_areas2 WHERE uaa_area_user_id = " & AccountUserID & "")
            InsertGrantedAreas()
            frmWait.seconds = 1
            frmWait.ShowDialog()
            UserActivity("Updated a user account " & txtFirstName.Text.Trim & " " & txtMiddleName.Text.Trim & " " & txtLastName.Text.Trim & ". Username: " & txtUsername.Text & "", "USER ACCOUNT")
            MsgBox("User account has been successfully updated.", vbInformation, "")

            Try
                cn.Close()
                cn.Open()
                cm = New MySqlCommand("select * from tbl_user_account where ua_id = " & AccountUserID & "", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    str_name = dr.Item("ua_first_name").ToString & " " & dr.Item("ua_middle_name").ToString & " " & dr.Item("ua_last_name").ToString
                    str_role = dr.Item("ua_account_type").ToString
                Else
                End If
                dr.Close()
                cn.Close()
                frmMain.lblUser.Text = str_user
                frmMain.lblRole.Text = str_role
                frmMain.User_Name.Text = str_name
            Catch ex As Exception
                dr.Close()
                cn.Close()
            End Try
        End If
    End Sub

    Private Sub dgUserModules_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgUserModules.CellDoubleClick
        If dgUserModules.CurrentRow.Cells(2).Value = False Then
            dgUserModules.CurrentRow.Cells(2).Value = True
        Else
            dgUserModules.CurrentRow.Cells(2).Value = False
        End If
    End Sub

    Private Sub dgRegistryArea_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgRegistryArea.CellClick
        If dgRegistryArea.CurrentRow.Cells(2).Value = False Then
            dgRegistryArea.CurrentRow.Cells(2).Value = True
        Else
            dgRegistryArea.CurrentRow.Cells(2).Value = False
        End If
    End Sub

    Private Sub dgEnrollmentArea_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgEnrollmentArea.CellClick
        If dgEnrollmentArea.CurrentRow.Cells(2).Value = False Then
            dgEnrollmentArea.CurrentRow.Cells(2).Value = True
        Else
            dgEnrollmentArea.CurrentRow.Cells(2).Value = False
        End If
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        frmCamera.Show()
        frmCamera.lblCameraSubject.Text = "User"
    End Sub

    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        frmCamera.Show()
        frmCamera.lblCameraSubject.Text = "User"
    End Sub

    Private Sub dgUserModules_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgUserModules.CellContentClick

    End Sub

    Private Sub dgUserModules_SelectionChanged(sender As Object, e As EventArgs) Handles dgUserModules.SelectionChanged

    End Sub
End Class