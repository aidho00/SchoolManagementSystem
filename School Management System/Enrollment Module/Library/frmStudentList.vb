Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmStudentList
    Private Sub frmStudentList_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'LibraryStudentList()
    End Sub

    Private Sub dgStudentList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgStudentList.CellMouseEnter
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = CType(sender, DataGridView).Columns(e.ColumnIndex).Name
            If columnName = "colUpdate" Or columnName = "colView" Then
                CType(sender, DataGridView).Cursor = Cursors.Hand
            Else
                CType(sender, DataGridView).Cursor = Cursors.Default
            End If
        End If
    End Sub


    Public Sub LoadComboBoxData()
        With frmStudentInfo
            fillComboWithBlank("select * from tbl_pwd", .cbDisability, "tbl_pwd", "pwd_name", "pwd_id")
            fillComboWithBlank("select * from tbl_religion", .cbReligion, "tbl_religion", "r_name", "r_id")
            fillComboWithBlank("select * from tbl_course", .cbCourse, "tbl_course", "course_code", "course_id")
            fillComboWithBlank("select * from tbl_scholarship_status order by case when scholar_name = 'PAYING' then 1 else 2 end", .cbScholarship, "tbl_scholarship_status", "scholar_name", "scholar_id")
        End With
    End Sub

    Private Sub LoadData()
        With frmStudentInfo
            If frmMain.systemModule.Text = "College Module" Then
                .collegePanel.Visible = True
            ElseIf frmMain.systemModule.Text = "Basic Education Module" Then
                .collegePanel.Visible = False
            End If

            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select pwd_name from tbl_pwd where pwd_id = " & .PWDID & "", cn)
            .cbDisability.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select r_name from tbl_religion where r_id = " & .RelegionID & "", cn)
            .cbReligion.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select course_code from tbl_course where course_id = " & .CourseID & "", cn)
            .cbCourse.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select course_name from tbl_course where course_id = " & .CourseID & "", cn)
            .txtCourse.Text = cm.ExecuteScalar
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select scholar_name from tbl_scholarship_status where scholar_id = '" & .ScholarshipID & "'", cn)
            .cbScholarship.Text = cm.ExecuteScalar
            cn.Close()

            If .PrimaryID = 0 Then
                cn.Close()
            Else
                cn.Open()
                cm = New MySqlCommand("select schl_name from tbl_schools where schl_id  = '" & .PrimaryID & "'", cn)
                .txtPrimary.Text = cm.ExecuteScalar
                cn.Close()
            End If
            If .JuniorHighID = 0 Then
                cn.Close()
            Else
                cn.Open()
                cm = New MySqlCommand("select schl_name from tbl_schools where schl_id  = '" & .JuniorHighID & "'", cn)
                .txtJuniorHigh.Text = cm.ExecuteScalar
                cn.Close()
            End If
            If .SeniorHighID = 0 Then
                cn.Close()
            Else
                cn.Open()
                cm = New MySqlCommand("select schl_name from tbl_schools where schl_id  = '" & .SeniorHighID & "'", cn)
                .txtSeniorHigh.Text = cm.ExecuteScalar
                cn.Close()
            End If
            If .CollegeID = 0 Then
                cn.Close()
            Else
                cn.Open()
                cm = New MySqlCommand("select schl_name from tbl_schools where schl_id  = '" & .CollegeID & "'", cn)
                .txtCollege.Text = cm.ExecuteScalar
                cn.Close()
            End If
            If .MastersID = 0 Then
                cn.Close()
            Else
                cn.Open()
                cm = New MySqlCommand("select schl_name from tbl_schools where schl_id  = '" & .MastersID & "'", cn)
                .txtMasters.Text = cm.ExecuteScalar
                cn.Close()
            End If
            If .DoctorateID = 0 Then
                cn.Close()
            Else
                cn.Open()
                cm = New MySqlCommand("select schl_name from tbl_schools where schl_id  = '" & .DoctorateID & "'", cn)
                .txtDoctorate.Text = cm.ExecuteScalar
                cn.Close()
            End If
            If .LastSchoolID = 0 Then
                cn.Close()
            Else
                cn.Open()
                cm = New MySqlCommand("select schl_name from tbl_schools where schl_id  = '" & .LastSchoolID & "'", cn)
                .txtLastSchool.Text = cm.ExecuteScalar
                cn.Close()
            End If
            If .TransferredSchoolID = 0 Then
                cn.Close()
            Else
                cn.Open()
                cm = New MySqlCommand("select schl_name from tbl_schools where schl_id  = '" & .TransferredSchoolID & "'", cn)
                .txtSchoolTransferred.Text = cm.ExecuteScalar
                cn.Close()
            End If

            If .TransferredSchoolID = 1 Then
                .transferredPanel.Visible = True
            Else
                .transferredPanel.Visible = False
            End If
        End With
    End Sub

    Private Sub dgStudentList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgStudentList.CellContentClick
        Dim TodayDate As DateTime = Convert.ToDateTime(DateToday)
        Dim colname As String = dgStudentList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            LoadComboBoxData()

            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_student where s_id_no = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgStudentList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmStudentInfo
                    comboStudentLevelWithIrreg(.cbYearLevel, .lbllabel, .lbllevel)

                    .OldStudentID = dr.Item("s_id_no").ToString
                    .txtStudentID.Text = "Student ID: " & dr.Item("s_id_no").ToString
                    .txtLRN.Text = dr.Item("s_lrn_no").ToString
                    .txtSO.Text = dr.Item("s_so_no").ToString
                    .txtFname.Text = dr.Item("s_fn").ToString
                    .txtMname.Text = dr.Item("s_mn").ToString
                    .txtLname.Text = dr.Item("s_ln").ToString
                    .dtBirthdate.Value = dr.Item("s_dob")
                    .txtBrithplace.Text = dr.Item("s_pob").ToString
                    .address_street = dr.Item("s_address").ToString

                    .cbEthnicity.Text = dr.Item("s_tribe").ToString
                    .RelegionID = CInt(dr.Item("s_religion_id").ToString)
                    .cbSex.Text = dr.Item("s_gender").ToString
                    .cbCivilStatus.Text = dr.Item("s_civil_status").ToString
                    .PWDID = CInt(dr.Item("s_pwd").ToString)
                    .cbStudentStatus.Text = dr.Item("s_status").ToString
                    .txtContact.Text = dr.Item("s_contact").ToString
                    .txtEmail.Text = dr.Item("s_email").ToString
                    .cbNationality.Text = dr.Item("s_nationality").ToString
                    .cbYearLevel.Text = dr.Item("s_yr_lvl").ToString
                    .CourseID = CInt(dr.Item("s_course_id").ToString)

                    Try
                        .PrimaryID = CInt(dr.Item("s_p_school_id").ToString)
                    Catch ex As Exception
                        .PrimaryID = 0
                    End Try
                    .txtPrimaryGrad.Text = dr.Item("s_p_school_ya").ToString
                    .txtPrimaryRem.Text = dr.Item("s_p_school_remarks").ToString

                    Try
                        .JuniorHighID = CInt(dr.Item("s_s_school_id").ToString)
                    Catch ex As Exception
                        .JuniorHighID = 0
                    End Try
                    .txtJuniorHighGrad.Text = dr.Item("s_s_school_ya").ToString
                    .txtJuniorHighRem.Text = dr.Item("s_s_school_remarks").ToString

                    Try
                        .SeniorHighID = CInt(dr.Item("s_sh_school_id").ToString)
                    Catch ex As Exception
                        .SeniorHighID = 0
                    End Try
                    .txtSeniorHighGrad.Text = dr.Item("s_sh_school_ya").ToString
                    .txtSeniorHighRem.Text = dr.Item("s_sh_school_remarks").ToString

                    Try
                        .CollegeID = CInt(dr.Item("s_c_school_id").ToString)
                    Catch ex As Exception
                        .CollegeID = 0
                    End Try
                    .txtCollegeGrad.Text = dr.Item("s_c_school_ya").ToString
                    .txtCollegeRem.Text = dr.Item("s_c_school_remarks").ToString

                    Try
                        .MastersID = CInt(dr.Item("s_m_school_id").ToString)
                    Catch ex As Exception
                        .MastersID = 0
                    End Try
                    .txtMastersGrad.Text = dr.Item("s_m_school_ya").ToString
                    .txtMastersRem.Text = dr.Item("s_m_school_remarks").ToString

                    Try
                        .DoctorateID = CInt(dr.Item("s_d_school_id").ToString)
                    Catch ex As Exception
                        .DoctorateID = 0
                    End Try
                    .txtDoctorateGrad.Text = dr.Item("s_d_school_ya").ToString
                    .txtDoctorateRem.Text = dr.Item("s_d_school_remarks").ToString

                    Try
                        .LastSchoolID = CInt(dr.Item("s_lys_school_id").ToString)
                    Catch ex As Exception
                        .LastSchoolID = 0
                    End Try
                    .txtLastSchoolGrad.Text = dr.Item("s_lys_school_ya").ToString
                    .txtLastSchoolRem.Text = dr.Item("s_lys_school_remarks").ToString

                    .cbStatus.Text = dr.Item("s_active_status").ToString
                    .dtStarted.Value = dr.Item("s_begin_date")
                    .txtDateGraduated.Text = dr.Item("s_grad_date").ToString
                    .cbBloodType.Text = dr.Item("s_bloodtype").ToString

                    '.txt.Text = dr.Item("s_lst_yr_attend").ToString
                    Try
                        .PeriodID = CInt(dr.Item("s_period_id").ToString)
                    Catch ex As Exception

                    End Try
                    .txtEntranceCred.Text = dr.Item("s_ent_cred").ToString

                    .ScholarshipID = CInt(dr.Item("s_scholarship").ToString)

                    .mother_fname = dr.Item("s_mother_name").ToString
                    .txtMotherAddress.Text = dr.Item("s_mother_address").ToString
                    .txtMotherOccu.Text = dr.Item("s_mother_occu").ToString
                    .txtMotherContact.Text = dr.Item("s_mother_contact").ToString

                    .father_fname = dr.Item("s_father_name").ToString
                    .txtFatherAddress.Text = dr.Item("s_father_address").ToString
                    .txtFatherOccu.Text = dr.Item("s_father_occu").ToString
                    .txtFatherContact.Text = dr.Item("s_father_contact").ToString

                    .txtGName.Text = dr.Item("s_guardian_name").ToString
                    .txtGAddress.Text = dr.Item("s_guardian_address").ToString
                    .txtGOccu.Text = dr.Item("s_guardian_occu").ToString
                    .txtGContact.Text = dr.Item("s_guardian_contact").ToString

                    .txtIncome.Text = dr.Item("s_family_annual_income").ToString
                    .txtGIncome.Text = dr.Item("s_guardian_income").ToString
                    .txt4ps.Text = dr.Item("s_4ps_ID").ToString
                    .txt1ps.Text = dr.Item("s_1ps_ID").ToString
                    .txtNotes.Text = dr.Item("s_notes").ToString
                    .txtShirtSize.Text = dr.Item("s_shirt_size").ToString

                    .txtNSTP.Text = dr.Item("s_nstp_no").ToString
                    .txtSODate.Text = dr.Item("s_so_date").ToString
                    .txtZipCode.Text = dr.Item("s_address_zipcode").ToString

                    .cbHScardValue = CInt(dr.Item("s_cred_hscard").ToString)
                    .cbF137Value = CInt(dr.Item("s_cred_f137").ToString)
                    .cbBirthCertValue = CInt(dr.Item("s_cred_birth").ToString)
                    .cbMarriageCertValue = CInt(dr.Item("s_cred_marriage_cert").ToString)
                    .cbGoodMoralValue = CInt(dr.Item("s_cred_gmc").ToString)
                    .cbNCAEValue = CInt(dr.Item("s_cred_ncae").ToString)
                    .cbHDValue = CInt(dr.Item("s_cred_hd").ToString)
                    .cbTORValue = CInt(dr.Item("s_cred_tor").ToString)
                    .cbOTRValue = CInt(dr.Item("s_cred_ofc_tor").ToString)
                    .cbAlsValue = CInt(dr.Item("s_cred_als_cert").ToString)

                    .txtAwards.Text = dr.Item("s_acad_awards").ToString
                    .txtSuffix.Text = dr.Item("s_ext").ToString
                    .cbTransferredValue = CInt(dr.Item("s_is_tranfer").ToString)

                    Try
                        .TransferredSchoolID = CInt(dr.Item("s_school_transfer").ToString)
                    Catch ex As Exception
                        .TransferredSchoolID = 0
                    End Try
                    Try
                        .dtOtrRelease.Value = dr.Item("s_otr_released")
                    Catch ex As Exception
                        .dtOtrRelease.Text = DateToday
                    End Try
                    Try
                        .dtOtrReceived.Value = dr.Item("s_otr_received")
                    Catch ex As Exception
                        .dtOtrReceived.Text = DateToday
                    End Try
                    .cbTransferMode.Text = dr.Item("s_otr_mode").ToString
                    .txtOTRremarks.Text = dr.Item("s_otr_remarks").ToString

                    If .cbTransferredValue = 1 Then
                        .cbTransferred.Checked = True
                    Else
                        .cbTransferred.Checked = False
                    End If

                    If .cbHScardValue = 1 Then
                        .cb_hscard.Checked = True
                    Else
                        .cb_hscard.Checked = False
                    End If
                    If .cbF137Value = 1 Then
                        .cb_f137.Checked = True
                    Else
                        .cb_f137.Checked = False
                    End If
                    If .cbBirthCertValue = 1 Then
                        .cb_birth.Checked = True
                    Else
                        .cb_birth.Checked = False
                    End If
                    If .cbMarriageCertValue = 1 Then
                        .cb_mcert.Checked = True
                    Else
                        .cb_mcert.Checked = False
                    End If
                    If .cbGoodMoralValue = 1 Then
                        .cb_gmc.Checked = True
                    Else
                        .cb_gmc.Checked = False
                    End If
                    If .cbNCAEValue = 1 Then
                        .cb_ncae.Checked = True
                    Else
                        .cb_ncae.Checked = False
                    End If
                    If .cbAlsValue = 1 Then
                        .cb_als.Checked = True
                    Else
                        .cb_als.Checked = False
                    End If
                    If .cbHDValue = 1 Then
                        .cb_hd.Checked = True
                    Else
                        .cb_hd.Checked = False
                    End If
                    If .cbTORValue = 1 Then
                        .cb_tor_eval.Checked = True
                    Else
                        .cb_tor_eval.Checked = False
                    End If
                    If .cbOTRValue = 1 Then
                        .cb_ofc_tor.Checked = True
                    Else
                        .cb_ofc_tor.Checked = False
                    End If

                    .address_prov_code = dr.Item("s_address_prov").ToString
                    .address_citymun_code = dr.Item("s_address_citymun").ToString
                    .address_brgy_code = dr.Item("s_address_brgy").ToString

                    .mother_mname = dr.Item("s_mother_mname").ToString
                    .mother_lname = dr.Item("s_mother_lname").ToString

                    .father_mname = dr.Item("s_father_mname").ToString
                    .father_lname = dr.Item("s_father_lname").ToString
                    .cbCourseStatus.Text = dr.Item("s_course_status").ToString


                    dr.Close()
                    cn.Close()

                    cn.Open()
                    cm = New MySqlCommand("SELECT `provDesc` FROM `refprovince` where provCode = '" & .address_prov_code & "'", cn)
                    .address_prov = cm.ExecuteScalar
                    cn.Close()

                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT `citymunDesc` FROM `refcitymun` where citymunCode = '" & .address_citymun_code & "'", cn)
                    .address_citymun = cm.ExecuteScalar
                    cn.Close()

                    cn.Close()
                    cn.Open()
                    cm = New MySqlCommand("SELECT `brgyDesc` FROM `refbrgy` where brgyCode = '" & .address_brgy_code & "'", cn)
                    .address_brgy = cm.ExecuteScalar
                    cn.Close()

                    .txtAddress.Text = .address_street & ", " & .address_brgy & ", " & .address_citymun & ", " & .address_prov
                    .txtMotherName.Text = .mother_fname & " " & .mother_mname & " " & .mother_lname
                    .txtFatherName.Text = .father_fname & " " & .father_mname & " " & .father_lname



                    cn.Open()
                    cm = New MySqlCommand("select * from cfcissmsdb_sphotos.tbl_student_photos where sp_student_id = '" & .OldStudentID & "'", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        Try
                            cn.Close()
                            cn.Open()
                            cm = New MySqlCommand("select sp_profile_photo from cfcissmsdb_sphotos.tbl_student_photos where sp_student_id = @1", cn)
                            With cm
                                .Parameters.AddWithValue("@1", dgStudentList.Rows(e.RowIndex).Cells(1).Value.ToString)
                            End With
                            dr = cm.ExecuteReader
                            While dr.Read
                                Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                                Dim array(CInt(len)) As Byte
                                dr.GetBytes(0, 0, array, 0, CInt(len))
                                Dim ms As New MemoryStream(array)
                                Dim bitmap As New System.Drawing.Bitmap(ms)
                                .studentPhoto.Image = bitmap
                            End While
                            dr.Close()
                            cn.Close()
                        Catch ex As Exception
                            .studentPhoto.Image = .studentDummypicture.Image
                        End Try
                        Try
                            cn.Close()
                            cn.Open()
                            cm = New MySqlCommand("select sp_sign_photo from cfcissmsdb_sphotos.tbl_student_photos where sp_student_id   = @1", cn)
                            With cm
                                .Parameters.AddWithValue("@1", dgStudentList.Rows(e.RowIndex).Cells(1).Value.ToString)
                            End With
                            dr = cm.ExecuteReader
                            While dr.Read
                                Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                                Dim array(CInt(len)) As Byte
                                dr.GetBytes(0, 0, array, 0, CInt(len))
                                Dim ms As New MemoryStream(array)
                                Dim bitmap As New System.Drawing.Bitmap(ms)
                                .studentSignature.Image = bitmap
                            End While
                            dr.Close()
                            cn.Close()
                        Catch ex As Exception
                            .studentSignature.Image = .studentDummysign.Image
                        End Try
                    Else
                        .studentPhoto.Image = .studentDummypicture.Image
                        .studentSignature.Image = .studentDummysign.Image
                    End If
                    dr.Close()
                    cn.Close()

                    LoadData()
                    .btnSave.Visible = False
                    .btnUpdate.Visible = True
                    .ShowDialog()
                End With
            Else
            End If

        End If
    End Sub
End Class