Imports MySql.Data.MySqlClient
Imports System.IO
Module CRUD
    Public result As String
    Public da As New MySqlDataAdapter
    Public dr As MySqlDataReader
    Public dt As New DataTable
    Public ds As New DataSet

    Public da2 As New MySqlDataAdapter
    Public dr2 As MySqlDataReader
    Public dt2 As New DataTable
    Public ds2 As New DataSet


    Public Sub InsertStudent()
        Try
            AutoIDNumber()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("INSERT INTO tbl_employee (`s_id_no`, `s_lrn_no`, `s_so_no`, `s_fn`, `s_ln`, `s_mn`, `s_dob`, `s_pob`, `s_address`, `s_photo`, `s_tribe`, `s_religion_id`, `s_gender`, `s_civil_status`, `s_pwd`, `s_status`, `s_contact`, `s_email`, `s_nationality`, `s_yr_lvl`, `s_course_id`, `s_p_school_id`, `s_p_school_ya`, `s_p_school_remarks`, `s_s_school_id`, `s_s_school_ya`, `s_s_school_remarks`, `s_c_school_id`, `s_c_school_ya`, `s_c_school_remarks`, `s_m_school_id`, `s_m_school_remarks`, `s_m_school_ya`, `s_d_school_id`, `s_d_school_ya`, `s_d_school_remarks`, `s_active_status`, `s_begin_date`, `s_grad_date`, `s_bloodtype`, `s_lst_yr_attend`, `s_ent_cred`, `s_period_id`, `s_period_grad_id`, `s_scholarship`, `s_mother_name`, `s_mother_address`, `s_mother_occu`, `s_mother_contact`, `s_father_name`, `s_father_address`, `s_father_occu`, `s_father_contact`, `s_guardian_name`, `s_guardian_address`, `s_guardian_contact`, `s_guardian_occu`, `s_guardian_income`, `s_family_annual_income`, `s_sh_school_ya`, `s_sh_school_id`, `s_sh_school_remarks`, `s_self_support`, `s_lys_school_id`, `s_lys_school_ya`, `s_lys_school_remarks`, `s_4ps_ID`, `s_1ps_ID`, `s_notes`, `s_shirt_size`, `s_sign_photo`, `s_nstp_no`, `s_so_date`, `s_address_zipcode`, `s_cred_hscard`, `s_cred_f137`, `s_cred_birth`, `s_cred_marriage_cert`, `s_cred_gmc`, `s_cred_ncae`, `s_cred_hd`, `s_cred_tor`, `s_cred_ofc_tor`, `s_cred_als_cert`, `s_acad_awards`, `s_ext`, `s_is_tranfer`, `s_school_transfer`, `s_otr_released`, `s_otr_received`, `s_otr_mode`, `s_otr_remarks`,s_address_prov,s_address_citymun,s_address_brgy,s_mother_mname,s_mother_lname,s_father_mname,s_father_lname,s_course_status) values (@s_id_no, @s_lrn_no, @s_so_no, @s_fn, @s_ln, @s_mn, @s_dob, @s_pob, @s_address, @s_photo, @s_tribe, @s_religion_id, @s_gender, @s_civil_status, @s_pwd, @s_status, @s_contact, @s_email, @s_nationality, @s_yr_lvl, @s_course_id, @s_p_school_id, @s_p_school_ya, @s_p_school_remarks, @s_s_school_id, @s_s_school_ya, @s_s_school_remarks, @s_c_school_id, @s_c_school_ya, @s_c_school_remarks, @s_m_school_id, @s_m_school_remarks, @s_m_school_ya, @s_d_school_id, @s_d_school_ya, @s_d_school_remarks, @s_active_status, @s_begin_date, @s_grad_date, @s_bloodtype, @s_lst_yr_attend, @s_ent_cred, @s_period_id, @s_period_grad_id, @s_scholarship, @s_mother_name, @s_mother_address, @s_mother_occu, @s_mother_contact, @s_father_name, @s_father_address, @s_father_occu, @s_father_contact, @s_guardian_name, @s_guardian_address, @s_guardian_contact, @s_guardian_occu, @s_guardian_income, @s_family_annual_income, @s_sh_school_ya, @s_sh_school_id, @s_sh_school_remarks, @s_self_support, @s_lys_school_id, @s_lys_school_ya, @s_lys_school_remarks, @s_4ps_ID, @s_1ps_ID, @s_notes, @s_shirt_size, @s_sign_photo, @s_nstp_no, @s_so_date, @s_address_zipcode, @s_cred_hscard, @s_cred_f137, @s_cred_birth, @s_cred_marriage_cert, @s_cred_gmc, @s_cred_ncae, @s_cred_hd, @s_cred_tor, @s_cred_ofc_tor, @s_cred_als_cert, @s_acad_awards, @s_ext, @s_is_tranfer, @s_school_transfer, @s_otr_released, @s_otr_received, @s_otr_mode, @s_otr_remarks,@s_address_prov,@s_address_citymun,@s_address_brgy,@s_mother_mname,@s_mother_lname,@s_father_mname,@s_father_lname,@s_course_status)", cn)
            With frmStudentInfo
                cm.Parameters.AddWithValue("@s_id_no", .NewStudentID)
                cm.Parameters.AddWithValue("@s_lrn_no", .txtLRN.Text.Trim)
                cm.Parameters.AddWithValue("@s_so_no", .txtSO.Text.Trim)
                cm.Parameters.AddWithValue("@s_fn", .txtFname.Text.Trim)
                cm.Parameters.AddWithValue("@s_ln", .txtLname.Text.Trim)
                cm.Parameters.AddWithValue("@s_mn", .txtMname.Text.Trim)
                cm.Parameters.AddWithValue("@s_dob", .dtBirthdate.Value)
                cm.Parameters.AddWithValue("@s_pob", .txtBrithplace.Text.Trim)
                cm.Parameters.AddWithValue("@s_address", .txtAddress.Text.Trim)
                If frmStudentInfo.studentPhoto.Image Is Nothing Then
                    Dim mstream3 As New MemoryStream
                    frmStudentInfo.studentDummypicture.Image.Save(mstream3, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Dim arrImage3() As Byte = mstream3.GetBuffer

                    cm.Parameters.AddWithValue("@s_photo", arrImage3)
                Else
                    Try
                        Dim mstream As New MemoryStream
                        frmStudentInfo.studentPhoto.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage() As Byte = mstream.GetBuffer
                        cm.Parameters.AddWithValue("@s_photo", arrImage)
                    Catch ex As Exception
                        Dim mstream3 As New MemoryStream
                        frmStudentInfo.studentDummypicture.Image.Save(mstream3, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage3() As Byte = mstream3.GetBuffer

                        cm.Parameters.AddWithValue("@s_photo", arrImage3)
                    End Try
                End If
                cm.Parameters.AddWithValue("@s_tribe", .cbEthnicity.Text.Trim)
                cm.Parameters.AddWithValue("@s_religion_id", .RelegionID)
                cm.Parameters.AddWithValue("@s_gender", .cbSex.Text)
                cm.Parameters.AddWithValue("@s_civil_status", .cbCivilStatus.Text)
                cm.Parameters.AddWithValue("@s_pwd", .PWDID)
                cm.Parameters.AddWithValue("@s_status", .cbStudentStatus.Text)
                cm.Parameters.AddWithValue("@s_contact", .txtContact.Text.Trim)
                cm.Parameters.AddWithValue("@s_email", .txtEmail.Text.Trim)
                cm.Parameters.AddWithValue("@s_nationality", .cbNationality.Text)
                cm.Parameters.AddWithValue("@s_yr_lvl", .cbYearLevel.Text)
                cm.Parameters.AddWithValue("@s_course_id", .CourseID)
                cm.Parameters.AddWithValue("@s_p_school_id", .PrimaryID)
                cm.Parameters.AddWithValue("@s_p_school_ya", .txtPrimaryGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_p_school_remarks", .txtPrimaryRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_s_school_id", .JuniorHighID)
                cm.Parameters.AddWithValue("@s_s_school_ya", .txtJuniorHighGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_s_school_remarks", .txtJuniorHighRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_c_school_id", .CollegeID)
                cm.Parameters.AddWithValue("@s_c_school_ya", .txtCollegeGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_c_school_remarks", .txtCollegeRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_m_school_id", .MastersID)
                cm.Parameters.AddWithValue("@s_m_school_remarks", .txtMastersGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_m_school_ya", .txtMastersRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_d_school_id", .DoctorateID)
                cm.Parameters.AddWithValue("@s_d_school_ya", .txtDoctorateGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_d_school_remarks", .txtDoctorateRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_active_status", .cbStatus.Text)
                cm.Parameters.AddWithValue("@s_begin_date", .dtStarted.Value)
                cm.Parameters.AddWithValue("@s_grad_date", .txtDateGraduated.Text.Trim)
                cm.Parameters.AddWithValue("@s_bloodtype", .cbBloodType.Text)
                cm.Parameters.AddWithValue("@s_lst_yr_attend", "")
                cm.Parameters.AddWithValue("@s_ent_cred", .txtEntranceCred.Text.Trim)
                cm.Parameters.AddWithValue("@s_period_id", .PeriodID)
                cm.Parameters.AddWithValue("@s_period_grad_id", 0)
                cm.Parameters.AddWithValue("@s_scholarship", .ScholarshipID)
                cm.Parameters.AddWithValue("@s_mother_name", .txtMotherName.Text.Trim)
                cm.Parameters.AddWithValue("@s_mother_address", .txtMotherAddress.Text.Trim)
                cm.Parameters.AddWithValue("@s_mother_occu", .txtMotherOccu.Text.Trim)
                cm.Parameters.AddWithValue("@s_mother_contact", .txtMotherContact.Text.Trim)
                cm.Parameters.AddWithValue("@s_father_name", .txtFatherName.Text.Trim)
                cm.Parameters.AddWithValue("@s_father_address", .txtFatherAddress.Text.Trim)
                cm.Parameters.AddWithValue("@s_father_occu", .txtFatherOccu.Text.Trim)
                cm.Parameters.AddWithValue("@s_father_contact", .txtFatherContact.Text.Trim)
                cm.Parameters.AddWithValue("@s_guardian_name", .txtGName.Text.Trim)
                cm.Parameters.AddWithValue("@s_guardian_address", .txtGAddress.Text.Trim)
                cm.Parameters.AddWithValue("@s_guardian_contact", .txtGContact.Text.Trim)
                cm.Parameters.AddWithValue("@s_guardian_occu", .txtGOccu.Text.Trim)
                cm.Parameters.AddWithValue("@s_guardian_income", .txtGIncome.Text.Trim)
                cm.Parameters.AddWithValue("@s_family_annual_income", .txtIncome.Text.Trim)
                cm.Parameters.AddWithValue("@s_sh_school_ya", .SeniorHighID)
                cm.Parameters.AddWithValue("@s_sh_school_id", .txtSeniorHighGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_sh_school_remarks", .txtSeniorHighRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_self_support", 0)
                cm.Parameters.AddWithValue("@s_lys_school_id", .LastSchoolID)
                cm.Parameters.AddWithValue("@s_lys_school_ya", .txtLastSchoolGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_lys_school_remarks", .txtLastSchoolRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_4ps_ID", .txt4ps.Text.Trim)
                cm.Parameters.AddWithValue("@s_1ps_ID", .txt1ps.Text.Trim)
                cm.Parameters.AddWithValue("@s_notes", .txtNotes.Text.Trim)
                cm.Parameters.AddWithValue("@s_shirt_size", .txtShirtSize.Text.Trim)
                If frmStudentInfo.studentSignature.Image Is Nothing Then
                    Dim mstream4 As New MemoryStream
                    frmStudentInfo.studentDummysign.Image.Save(mstream4, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Dim arrImage4() As Byte = mstream4.GetBuffer
                    cm.Parameters.AddWithValue("@s_sign_photo", arrImage4)
                Else
                    Try
                        Dim mstream2 As New MemoryStream
                        frmStudentInfo.studentSignature.Image.Save(mstream2, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage2() As Byte = mstream2.GetBuffer
                        cm.Parameters.AddWithValue("@s_sign_photo", arrImage2)
                    Catch ex As Exception
                        Dim mstream4 As New MemoryStream
                        frmStudentInfo.studentDummysign.Image.Save(mstream4, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage4() As Byte = mstream4.GetBuffer
                        cm.Parameters.AddWithValue("@s_sign_photo", arrImage4)
                    End Try
                End If
                cm.Parameters.AddWithValue("@s_nstp_no", .txtNSTP.Text.Trim)
                cm.Parameters.AddWithValue("@s_so_date", .txtSODate.Text.Trim)
                cm.Parameters.AddWithValue("@s_address_zipcode", .txtZipCode.Text.Trim)

                If .cb_hscard.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_hscard", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_hscard", 0)
                End If
                If .cb_f137.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_f137", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_f137", 0)
                End If
                If .cb_birth.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_birth", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_birth", 0)
                End If
                If .cb_mcert.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_marriage_cert", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_marriage_cert", 0)
                End If
                If .cb_gmc.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_gmc", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_gmc", 0)
                End If
                If .cb_ncae.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_ncae", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_ncae", 0)
                End If
                If .cb_hd.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_hd", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_hd", 0)
                End If
                If .cb_tor_eval.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_tor", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_tor", 0)
                End If

                If .cb_ofc_tor.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_ofc_tor", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_ofc_tor", 0)
                End If

                If .cb_als.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_als_cert", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_als_cert", 0)
                End If

                If .cbTransferred.Checked = True Then
                    cm.Parameters.AddWithValue("@s_is_tranfer", 1)
                Else
                    cm.Parameters.AddWithValue("@s_is_tranfer", 0)
                End If

                cm.Parameters.AddWithValue("@s_acad_awards", .txtAwards.Text.Trim)
                cm.Parameters.AddWithValue("@s_ext", .txtSuffix.Text.Trim)
                cm.Parameters.AddWithValue("@s_school_transfer", .TransferredSchoolID)
                cm.Parameters.AddWithValue("@s_otr_released", .dtOtrRelease.Value)
                cm.Parameters.AddWithValue("@s_otr_received", .dtOtrReceived.Value)
                cm.Parameters.AddWithValue("@s_otr_mode", .cbTransferMode.Text.Trim)
                cm.Parameters.AddWithValue("@s_otr_remarks", .txtOTRremarks.Text.Trim)

                cm.Parameters.AddWithValue("@s_address_prov", .address_prov_code)
                cm.Parameters.AddWithValue("@s_address_citymun", .address_citymun_code)
                cm.Parameters.AddWithValue("@s_address_brgy", .address_brgy_code)

                cm.Parameters.AddWithValue("@s_mother_mname", .mother_mname.ToString.Trim)
                cm.Parameters.AddWithValue("@s_mother_lname", .mother_lname.ToString.Trim)
                cm.Parameters.AddWithValue("@s_father_mname", .father_mname.ToString.Trim)
                cm.Parameters.AddWithValue("@s_father_lname", .father_lname.ToString.Trim)

                cm.Parameters.AddWithValue("@s_course_status", .cbCourseStatus.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                UserActivity("Added a student information " & .txtFname.Text.Trim & " " & .txtMname.Text.Trim & " " & .txtLname.Text.Trim & " with ID Number:" & .NewStudentID & ".", "LIBRARY STUDENT PROFILING")
                frmWait.seconds = 1
                frmWait.ShowDialog()
                MsgBox("New student has been successfully saved/added.", vbInformation, "")
                LibraryStudentList()

                Dim dr2 As DialogResult
                dr2 = MessageBox.Show("Do you want to print student pre-enrollment form?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If dr2 = DialogResult.No Then
                    frmStudentInfo.Close()
                Else
                    fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmReportViewer.cbAcademicYear, "tbl_period", "PERIOD", "period_id")

                    frmReportViewer.frmTitle.Text = "Pre-Enrollment Form"
                    frmReportViewer.StudentID = frmStudentInfo.NewStudentID
                    frmReportViewer.recordStatus = "ADD"
                    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rptdoc = New Enrollment_Student_FORM
                    rptdoc.SetParameterValue("studentname", frmStudentInfo.txtFname.Text & " " & frmStudentInfo.txtMname.Text & " " & frmStudentInfo.txtLname.Text)
                    rptdoc.SetParameterValue("studentcourse", frmStudentInfo.txtCourse.Text)
                    rptdoc.SetParameterValue("schoolyear", frmReportViewer.cbAcademicYear.Text)
                    rptdoc.SetParameterValue("studentyearlevel", frmStudentInfo.cbYearLevel.Text)
                    rptdoc.SetParameterValue("studentidnumber", frmStudentInfo.NewStudentID)
                    frmReportViewer.ReportViewer.ReportSource = rptdoc
                    frmReportViewer.ShowDialog()
                End If
            End With
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub preEnrollmentForm()

    End Sub

    Public Sub UpdateStudent()
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("UPDATE `tbl_student` SET `s_lrn_no`=@s_lrn_no,`s_so_no`=@s_so_no,`s_fn`=@s_fn,`s_ln`=@s_ln,`s_mn`=@s_mn,`s_dob`=@s_dob,`s_pob`=@s_pob,`s_address`=@s_address,`s_photo`=@s_photo,`s_tribe`=@s_tribe,`s_religion_id`=@s_religion_id,`s_gender`=@s_gender,`s_civil_status`=@s_civil_status,`s_pwd`=@s_pwd,`s_status`=@s_status,`s_contact`=@s_contact,`s_email`=@s_email,`s_nationality`=@s_nationality,`s_yr_lvl`=@s_yr_lvl,`s_course_id`=@s_course_id,`s_p_school_id`=@s_p_school_id,`s_p_school_ya`=@s_p_school_ya,`s_p_school_remarks`=@s_p_school_remarks,`s_s_school_id`=@s_s_school_id,`s_s_school_ya`=@s_s_school_ya,`s_s_school_remarks`=@s_s_school_remarks,`s_c_school_id`=@s_c_school_id,`s_c_school_ya`=@s_c_school_ya,`s_c_school_remarks`=@s_c_school_remarks,`s_m_school_id`=@s_m_school_id,`s_m_school_remarks`=@s_m_school_remarks,`s_m_school_ya`=@s_m_school_ya,`s_d_school_id`=@s_d_school_id,`s_d_school_ya`=@s_d_school_ya,`s_d_school_remarks`=@s_d_school_remarks,`s_active_status`=@s_active_status,`s_begin_date`=@s_begin_date,`s_grad_date`=@s_grad_date,`s_bloodtype`=@s_bloodtype,`s_lst_yr_attend`=@s_lst_yr_attend,`s_ent_cred`=@s_ent_cred,`s_period_id`=@s_period_id,`s_period_grad_id`=@s_period_grad_id,`s_scholarship`=@s_scholarship,`s_mother_name`=@s_mother_name,`s_mother_address`=@s_mother_address,`s_mother_occu`=@s_mother_occu,`s_mother_contact`=@s_mother_contact,`s_father_name`=@s_father_name,`s_father_address`=@s_father_address,`s_father_occu`=@s_father_occu,`s_father_contact`=@s_father_contact,`s_guardian_name`=@s_guardian_name,`s_guardian_address`=@s_guardian_address,`s_guardian_contact`=@s_guardian_contact,`s_guardian_occu`=@s_guardian_occu,`s_guardian_income`=@s_guardian_income,`s_family_annual_income`=@s_family_annual_income,`s_sh_school_ya`=@s_sh_school_ya,`s_sh_school_id`=@s_sh_school_id,`s_sh_school_remarks`=@s_sh_school_remarks,`s_self_support`=@s_self_support,`s_lys_school_id`=@s_lys_school_id,`s_lys_school_ya`=@s_lys_school_ya,`s_lys_school_remarks`=@s_lys_school_remarks,`s_4ps_ID`=@s_4ps_ID,`s_1ps_ID`=@s_1ps_ID,`s_notes`=@s_notes,`s_shirt_size`=@s_shirt_size,`s_sign_photo`=@s_sign_photo,`s_nstp_no`=@s_nstp_no,`s_so_date`=@s_so_date,`s_address_zipcode`=@s_address_zipcode,`s_cred_hscard`=@s_cred_hscard,`s_cred_f137`=@s_cred_f137,`s_cred_birth`=@s_cred_birth,`s_cred_marriage_cert`=@s_cred_marriage_cert,`s_cred_gmc`=@s_cred_gmc,`s_cred_ncae`=@s_cred_ncae,`s_cred_hd`=@s_cred_hd,`s_cred_tor`=@s_cred_tor,`s_cred_ofc_tor`=@s_cred_ofc_tor,`s_cred_als_cert`=@s_cred_als_cert,`s_acad_awards`=@s_acad_awards,`s_ext`=@s_ext,`s_is_tranfer`=@s_is_tranfer,`s_school_transfer`=@s_school_transfer,`s_otr_released`=@s_otr_released,`s_otr_received`=@s_otr_received,`s_otr_mode`=@s_otr_mode,`s_otr_remarks`=@s_otr_remarks,s_address_prov=@s_address_prov,s_address_citymun=@s_address_citymun,s_address_brgy=@s_address_brgy,s_mother_mname=@s_mother_mname,s_mother_lname=@s_mother_lname,s_father_mname=@s_father_mname,s_father_lname=@s_father_lname,s_course_status=@s_course_status WHERE `s_id_no`=@s_id_no", cn)
            With frmStudentInfo
                cm.Parameters.AddWithValue("@s_id_no", .OldStudentID)
                cm.Parameters.AddWithValue("@s_lrn_no", .txtLRN.Text.Trim)
                cm.Parameters.AddWithValue("@s_so_no", .txtSO.Text.Trim)
                cm.Parameters.AddWithValue("@s_fn", .txtFname.Text.Trim)
                cm.Parameters.AddWithValue("@s_ln", .txtLname.Text.Trim)
                cm.Parameters.AddWithValue("@s_mn", .txtMname.Text.Trim)
                cm.Parameters.AddWithValue("@s_dob", .dtBirthdate.Value)
                cm.Parameters.AddWithValue("@s_pob", .txtBrithplace.Text.Trim)
                cm.Parameters.AddWithValue("@s_address", .address_street.ToString.Trim)
                If frmStudentInfo.studentPhoto.Image Is Nothing Then
                    Dim mstream3 As New MemoryStream
                    frmStudentInfo.studentDummypicture.Image.Save(mstream3, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Dim arrImage3() As Byte = mstream3.GetBuffer
                    cm.Parameters.AddWithValue("@s_photo", arrImage3)
                Else
                    Try
                        Dim mstream As New MemoryStream
                        frmStudentInfo.studentPhoto.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage() As Byte = mstream.GetBuffer
                        cm.Parameters.AddWithValue("@s_photo", arrImage)
                    Catch ex As Exception
                        Dim mstream3 As New MemoryStream
                        frmStudentInfo.studentDummypicture.Image.Save(mstream3, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage3() As Byte = mstream3.GetBuffer
                        cm.Parameters.AddWithValue("@s_photo", arrImage3)
                    End Try
                End If


                cm.Parameters.AddWithValue("@s_tribe", .cbEthnicity.Text.Trim)
                cm.Parameters.AddWithValue("@s_religion_id", .RelegionID)
                cm.Parameters.AddWithValue("@s_gender", .cbSex.Text)
                cm.Parameters.AddWithValue("@s_civil_status", .cbCivilStatus.Text)
                cm.Parameters.AddWithValue("@s_pwd", .PWDID)
                cm.Parameters.AddWithValue("@s_status", .cbStudentStatus.Text)
                cm.Parameters.AddWithValue("@s_contact", .txtContact.Text.Trim)
                cm.Parameters.AddWithValue("@s_email", .txtEmail.Text.Trim)
                cm.Parameters.AddWithValue("@s_nationality", .cbNationality.Text)
                cm.Parameters.AddWithValue("@s_yr_lvl", .cbYearLevel.Text)
                cm.Parameters.AddWithValue("@s_course_id", .CourseID)
                cm.Parameters.AddWithValue("@s_p_school_id", .PrimaryID)
                cm.Parameters.AddWithValue("@s_p_school_ya", .txtPrimaryGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_p_school_remarks", .txtPrimaryRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_s_school_id", .JuniorHighID)
                cm.Parameters.AddWithValue("@s_s_school_ya", .txtJuniorHighGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_s_school_remarks", .txtJuniorHighRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_c_school_id", .CollegeID)
                cm.Parameters.AddWithValue("@s_c_school_ya", .txtCollegeGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_c_school_remarks", .txtCollegeRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_m_school_id", .MastersID)
                cm.Parameters.AddWithValue("@s_m_school_remarks", .txtMastersGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_m_school_ya", .txtMastersRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_d_school_id", .DoctorateID)
                cm.Parameters.AddWithValue("@s_d_school_ya", .txtDoctorateGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_d_school_remarks", .txtDoctorateRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_active_status", .cbStatus.Text)
                cm.Parameters.AddWithValue("@s_begin_date", .dtStarted.Value)
                cm.Parameters.AddWithValue("@s_grad_date", .txtDateGraduated.Text.Trim)
                cm.Parameters.AddWithValue("@s_bloodtype", .cbBloodType.Text)
                cm.Parameters.AddWithValue("@s_lst_yr_attend", "")
                cm.Parameters.AddWithValue("@s_ent_cred", .txtEntranceCred.Text.Trim)
                cm.Parameters.AddWithValue("@s_period_id", .PeriodID)
                cm.Parameters.AddWithValue("@s_period_grad_id", 0)
                cm.Parameters.AddWithValue("@s_scholarship", .ScholarshipID)
                cm.Parameters.AddWithValue("@s_mother_name", .mother_fname.ToString.Trim)
                cm.Parameters.AddWithValue("@s_mother_address", .txtMotherAddress.Text.Trim)
                cm.Parameters.AddWithValue("@s_mother_occu", .txtMotherOccu.Text.Trim)
                cm.Parameters.AddWithValue("@s_mother_contact", .txtMotherContact.Text.Trim)
                cm.Parameters.AddWithValue("@s_father_name", .father_fname.ToString.Trim)
                cm.Parameters.AddWithValue("@s_father_address", .txtFatherAddress.Text.Trim)
                cm.Parameters.AddWithValue("@s_father_occu", .txtFatherOccu.Text.Trim)
                cm.Parameters.AddWithValue("@s_father_contact", .txtFatherContact.Text.Trim)
                cm.Parameters.AddWithValue("@s_guardian_name", .txtGName.Text.Trim)
                cm.Parameters.AddWithValue("@s_guardian_address", .txtGAddress.Text.Trim)
                cm.Parameters.AddWithValue("@s_guardian_contact", .txtGContact.Text.Trim)
                cm.Parameters.AddWithValue("@s_guardian_occu", .txtGOccu.Text.Trim)
                cm.Parameters.AddWithValue("@s_guardian_income", .txtGIncome.Text.Trim)
                cm.Parameters.AddWithValue("@s_family_annual_income", .txtIncome.Text.Trim)
                cm.Parameters.AddWithValue("@s_sh_school_ya", .SeniorHighID)
                cm.Parameters.AddWithValue("@s_sh_school_id", .txtSeniorHighGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_sh_school_remarks", .txtSeniorHighRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_self_support", 0)
                cm.Parameters.AddWithValue("@s_lys_school_id", .LastSchoolID)
                cm.Parameters.AddWithValue("@s_lys_school_ya", .txtLastSchoolGrad.Text.Trim)
                cm.Parameters.AddWithValue("@s_lys_school_remarks", .txtLastSchoolRem.Text.Trim)
                cm.Parameters.AddWithValue("@s_4ps_ID", .txt4ps.Text.Trim)
                cm.Parameters.AddWithValue("@s_1ps_ID", .txt1ps.Text.Trim)
                cm.Parameters.AddWithValue("@s_notes", .txtNotes.Text.Trim)
                cm.Parameters.AddWithValue("@s_shirt_size", .txtShirtSize.Text.Trim)
                If frmStudentInfo.studentSignature.Image Is Nothing Then
                    Dim mstream4 As New MemoryStream
                    frmStudentInfo.studentDummysign.Image.Save(mstream4, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Dim arrImage4() As Byte = mstream4.GetBuffer
                    cm.Parameters.AddWithValue("@s_sign_photo", arrImage4)
                Else
                    Try
                        Dim mstream2 As New MemoryStream
                        frmStudentInfo.studentSignature.Image.Save(mstream2, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage2() As Byte = mstream2.GetBuffer
                        cm.Parameters.AddWithValue("@s_sign_photo", arrImage2)
                    Catch ex As Exception
                        Dim mstream4 As New MemoryStream
                        frmStudentInfo.studentDummysign.Image.Save(mstream4, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage4() As Byte = mstream4.GetBuffer
                        cm.Parameters.AddWithValue("@s_sign_photo", arrImage4)
                    End Try
                End If


                cm.Parameters.AddWithValue("@s_nstp_no", .txtNSTP.Text.Trim)
                cm.Parameters.AddWithValue("@s_so_date", .txtSODate.Text.Trim)
                cm.Parameters.AddWithValue("@s_address_zipcode", .txtZipCode.Text.Trim)

                If .cb_hscard.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_hscard", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_hscard", 0)
                End If
                If .cb_f137.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_f137", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_f137", 0)
                End If
                If .cb_birth.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_birth", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_birth", 0)
                End If
                If .cb_mcert.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_marriage_cert", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_marriage_cert", 0)
                End If
                If .cb_gmc.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_gmc", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_gmc", 0)
                End If
                If .cb_ncae.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_ncae", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_ncae", 0)
                End If
                If .cb_hd.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_hd", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_hd", 0)
                End If
                If .cb_tor_eval.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_tor", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_tor", 0)
                End If

                If .cb_ofc_tor.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_ofc_tor", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_ofc_tor", 0)
                End If

                If .cb_als.Checked = True Then
                    cm.Parameters.AddWithValue("@s_cred_als_cert", 1)
                Else
                    cm.Parameters.AddWithValue("@s_cred_als_cert", 0)
                End If

                If .cbTransferred.Checked = True Then
                    cm.Parameters.AddWithValue("@s_is_tranfer", 1)
                Else
                    cm.Parameters.AddWithValue("@s_is_tranfer", 0)
                End If

                cm.Parameters.AddWithValue("@s_acad_awards", .txtAwards.Text.Trim)
                cm.Parameters.AddWithValue("@s_ext", .txtSuffix.Text.Trim)
                cm.Parameters.AddWithValue("@s_school_transfer", .TransferredSchoolID)
                cm.Parameters.AddWithValue("@s_otr_released", .dtOtrRelease.Value)
                cm.Parameters.AddWithValue("@s_otr_received", .dtOtrReceived.Value)
                cm.Parameters.AddWithValue("@s_otr_mode", .cbTransferMode.Text.Trim)
                cm.Parameters.AddWithValue("@s_otr_remarks", .txtOTRremarks.Text.Trim)

                cm.Parameters.AddWithValue("@s_address_prov", .address_prov_code)
                cm.Parameters.AddWithValue("@s_address_citymun", .address_citymun_code)
                cm.Parameters.AddWithValue("@s_address_brgy", .address_brgy_code)

                cm.Parameters.AddWithValue("@s_mother_mname", .mother_mname.ToString.Trim)
                cm.Parameters.AddWithValue("@s_mother_lname", .mother_lname.ToString.Trim)
                cm.Parameters.AddWithValue("@s_father_mname", .father_mname.ToString.Trim)
                cm.Parameters.AddWithValue("@s_father_lname", .father_lname.ToString.Trim)

                cm.Parameters.AddWithValue("@s_course_status", .cbCourseStatus.Text)

                cm.ExecuteNonQuery()
                cn.Close()
                UserActivity("Updated student " & .txtFname.Text.Trim & " " & .txtMname.Text.Trim & " " & .txtLname.Text.Trim & " with ID Number:" & .OldStudentID & " details.", "LIBRARY STUDENT PROFILING")
                frmWait.seconds = 1
                frmWait.ShowDialog()
                MsgBox("Record has been successfully updated.", vbInformation, "")
                LibraryStudentList()


                Dim dr2 As DialogResult
                dr2 = MessageBox.Show("Do you want to print student pre-enrollment form?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If dr2 = DialogResult.No Then
                    frmStudentInfo.Close()
                Else
                    fillCombo("SELECT CONCAT(period_name,'-',period_semester) as 'PERIOD', period_id FROM  tbl_period where period_enrollment_status = 'OPEN' order by  `period_name` desc, `period_semester` desc, `period_status` asc", frmReportViewer.cbAcademicYear, "tbl_period", "PERIOD", "period_id")

                    frmReportViewer.frmTitle.Text = "Pre-Enrollment Form"
                    frmReportViewer.StudentID = frmStudentInfo.OldStudentID
                    frmReportViewer.recordStatus = "UPDATE"
                    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rptdoc = New Enrollment_Student_FORM
                    rptdoc.SetParameterValue("studentname", frmStudentInfo.txtFname.Text & " " & frmStudentInfo.txtMname.Text & " " & frmStudentInfo.txtLname.Text)
                    rptdoc.SetParameterValue("studentcourse", frmStudentInfo.txtCourse.Text)
                    rptdoc.SetParameterValue("schoolyear", frmReportViewer.cbAcademicYear.Text)
                    rptdoc.SetParameterValue("studentyearlevel", frmStudentInfo.cbYearLevel.Text)
                    rptdoc.SetParameterValue("studentidnumber", frmStudentInfo.OldStudentID)
                    frmReportViewer.ReportViewer.ReportSource = rptdoc
                    frmReportViewer.ShowDialog()
                End If
            End With
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Public Sub InsertEmployee()
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("INSERT INTO tbl_employee (emp_photo, emp_code, emp_first_name, emp_middle_name, emp_last_name, emp_gender, birthdate, place_of_birth, emp_status, civil_status, emp_address, email, is_tesda, tesda_cert, school_graduated, year_graduated, contact_no, employment_date, emp_sign_photo, emp_sss, emp_philhealth, emp_pagibig, emp_tin, emp_g_name, emp_g_address, emp_g_occu, emp_g_contact, required_subject_load, is_active, emp_designation) values (@emp_photo, @emp_code, @emp_first_name, @emp_middle_name, @emp_last_name, @emp_gender, @birthdate, @place_of_birth, @emp_status, @civil_status, @emp_address, @email, @is_tesda, @tesda_cert, @school_graduated, @year_graduated, @contact_no, @employment_date, @emp_sign_photo, @emp_sss, @emp_philhealth, @emp_pagibig, @emp_tin, @emp_g_name, @emp_g_address, @emp_g_occu, @emp_g_contact, @required_subject_load, @is_active, @emp_designation)", cn)
            With frmEmployee
                If frmEmployee.empPhoto.Image Is Nothing Then
                    cm.Parameters.AddWithValue("@emp_photo", "")
                Else
                    Try
                        Dim mstream As New MemoryStream
                        frmEmployee.empPhoto.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage() As Byte = mstream.GetBuffer
                        cm.Parameters.AddWithValue("@emp_photo", arrImage)
                    Catch ex As Exception
                        Dim mstream As New MemoryStream
                        frmEmployee.Dummypicture.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage() As Byte = mstream.GetBuffer
                        cm.Parameters.AddWithValue("@emp_photo", arrImage)
                    End Try
                End If
                cm.Parameters.AddWithValue("@emp_code", .txtBio.Text.Trim)
                cm.Parameters.AddWithValue("@emp_first_name", .txtFirstName.Text.Trim)
                cm.Parameters.AddWithValue("@emp_middle_name", .txtMiddleName.Text.Trim)
                cm.Parameters.AddWithValue("@emp_last_name", .txtLastName.Text.Trim)
                cm.Parameters.AddWithValue("@emp_gender", .cbSex.Text.Trim)
                cm.Parameters.AddWithValue("@birthdate", .dtBirthdate.Value)
                cm.Parameters.AddWithValue("@place_of_birth", .txtBirthPlace.Text.Trim)
                cm.Parameters.AddWithValue("@emp_status", .cbEmploymentStatus.Text.Trim)
                cm.Parameters.AddWithValue("@civil_status", .cbCivilStatus.Text.Trim)
                cm.Parameters.AddWithValue("@emp_address", .txtAddress.Text.Trim)
                cm.Parameters.AddWithValue("@email", .txtEmail.Text.Trim)
                cm.Parameters.AddWithValue("@is_tesda", .cbTesda.Text.Trim)
                cm.Parameters.AddWithValue("@tesda_cert", .txtTesdaCert.Text.Trim)
                cm.Parameters.AddWithValue("@school_graduated", .txtSchool.Text.Trim)
                cm.Parameters.AddWithValue("@year_graduated", .txtYearGrad.Text.Trim)
                cm.Parameters.AddWithValue("@contact_no", .txtContact.Text.Trim)
                cm.Parameters.AddWithValue("@employment_date", .dtEmployment.Value)
                If frmEmployee.empSignature.Image Is Nothing Then
                    cm.Parameters.AddWithValue("@emp_sign_photo", "")
                Else
                    Try
                        Dim mstream2 As New MemoryStream
                        frmEmployee.empSignature.Image.Save(mstream2, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage2() As Byte = mstream2.GetBuffer
                        cm.Parameters.AddWithValue("@emp_sign_photo", arrImage2)
                    Catch ex As Exception
                        Dim mstream2 As New MemoryStream
                        frmEmployee.Dummysign.Image.Save(mstream2, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage2() As Byte = mstream2.GetBuffer
                        cm.Parameters.AddWithValue("@emp_sign_photo", arrImage2)
                    End Try
                End If

                cm.Parameters.AddWithValue("@emp_sss", .txtSSS.Text.Trim)
                cm.Parameters.AddWithValue("@emp_philhealth", .txtPH.Text.Trim)
                cm.Parameters.AddWithValue("@emp_pagibig", .txtPagibig.Text.Trim)
                cm.Parameters.AddWithValue("@emp_tin", .txtTin.Text.Trim)
                cm.Parameters.AddWithValue("@emp_g_name", .txtGuadian.Text.Trim)
                cm.Parameters.AddWithValue("@emp_g_address", .txtGuadianAddress.Text.Trim)
                cm.Parameters.AddWithValue("@emp_g_occu", .txtGuadianOccupation.Text.Trim)
                cm.Parameters.AddWithValue("@emp_g_contact", .txtGuadianContact.Text.Trim)
                cm.Parameters.AddWithValue("@required_subject_load", .txtRequiredUnits.Text.Trim)
                cm.Parameters.AddWithValue("@is_active", .cbStatus.Text.Trim)
                cm.Parameters.AddWithValue("@emp_designation", .txtDesignation.Text.Trim)
                cm.ExecuteNonQuery()
                cn.Close()
                UserActivity("Added a new employee '" & .txtLastName.Text.Trim & ", " & .txtFirstName.Text.Trim & " " & .txtMiddleName.Text.Trim & "'.", "LIBRARY EMPLOYEE")
                frmWait.seconds = 1
                frmWait.ShowDialog()
                MsgBox("New employee has been successfully saved.", vbInformation, "")
                LibraryEmployeeList()
                .Close()
            End With
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Public Sub UpdateEmployee()
        Try
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("UPDATE tbl_employee SET emp_photo=@emp_photo, emp_code=@emp_code, emp_first_name=@emp_first_name, emp_middle_name=@emp_middle_name, emp_last_name=@emp_last_name, emp_gender=@emp_gender, birthdate=@birthdate, place_of_birth=@place_of_birth, emp_status=@emp_status, civil_status=@civil_status, emp_address=@emp_address, email=@email, is_tesda=@is_tesda, tesda_cert=@tesda_cert, school_graduated=@school_graduated, year_graduated=@year_graduated, contact_no=@contact_no, employment_date=@employment_date,  emp_sign_photo=@emp_sign_photo, emp_sss=@emp_sss, emp_philhealth=@emp_philhealth, emp_pagibig=@emp_pagibig, emp_tin=@emp_tin, emp_g_name=@emp_g_name, emp_g_address=@emp_g_address, emp_g_occu=@emp_g_occu, emp_g_contact=@emp_g_contact, required_subject_load=@required_subject_load, is_active=@is_active, emp_designation=@emp_designation where emp_id = '" & frmEmployee.EmployeeID & "'", cn)
            With frmEmployee
                If frmEmployee.empPhoto.Image Is Nothing Then
                    cm.Parameters.AddWithValue("@emp_photo", "")
                Else
                    Try
                        Dim mstream As New MemoryStream
                        frmEmployee.empPhoto.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage() As Byte = mstream.GetBuffer
                        cm.Parameters.AddWithValue("@emp_photo", arrImage)
                    Catch ex As Exception
                        Dim mstream As New MemoryStream
                        frmEmployee.Dummypicture.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage() As Byte = mstream.GetBuffer
                        cm.Parameters.AddWithValue("@emp_photo", arrImage)
                    End Try
                End If

                cm.Parameters.AddWithValue("@emp_code", .txtBio.Text)
                cm.Parameters.AddWithValue("@emp_first_name", .txtFirstName.Text)
                cm.Parameters.AddWithValue("@emp_middle_name", .txtMiddleName.Text)
                cm.Parameters.AddWithValue("@emp_last_name", .txtLastName.Text)
                cm.Parameters.AddWithValue("@emp_gender", .cbSex.Text)
                cm.Parameters.AddWithValue("@birthdate", .dtBirthdate.Value)
                cm.Parameters.AddWithValue("@place_of_birth", .txtBirthPlace.Text)
                cm.Parameters.AddWithValue("@emp_status", .cbEmploymentStatus.Text)
                cm.Parameters.AddWithValue("@civil_status", .cbCivilStatus.Text)
                cm.Parameters.AddWithValue("@emp_address", .txtAddress.Text)
                cm.Parameters.AddWithValue("@email", .txtEmail.Text)
                cm.Parameters.AddWithValue("@is_tesda", .cbTesda.Text)
                cm.Parameters.AddWithValue("@tesda_cert", .txtTesdaCert.Text)
                cm.Parameters.AddWithValue("@school_graduated", .txtSchool.Text)
                cm.Parameters.AddWithValue("@year_graduated", .txtYearGrad.Text)
                cm.Parameters.AddWithValue("@contact_no", .txtContact.Text)
                cm.Parameters.AddWithValue("@employment_date", .dtEmployment.Value)
                If frmEmployee.empSignature.Image Is Nothing Then
                    cm.Parameters.AddWithValue("@emp_sign_photo", "")
                Else
                    Try
                        Dim mstream2 As New MemoryStream
                        frmEmployee.empSignature.Image.Save(mstream2, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage2() As Byte = mstream2.GetBuffer
                        cm.Parameters.AddWithValue("@emp_sign_photo", arrImage2)
                    Catch ex As Exception
                        Dim mstream2 As New MemoryStream
                        frmEmployee.Dummysign.Image.Save(mstream2, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim arrImage2() As Byte = mstream2.GetBuffer
                        cm.Parameters.AddWithValue("@emp_sign_photo", arrImage2)
                    End Try
                End If
                cm.Parameters.AddWithValue("@emp_sss", .txtSSS.Text)
                cm.Parameters.AddWithValue("@emp_philhealth", .txtPH.Text)
                cm.Parameters.AddWithValue("@emp_pagibig", .txtPagibig.Text)
                cm.Parameters.AddWithValue("@emp_tin", .txtTin.Text)
                cm.Parameters.AddWithValue("@emp_g_name", .txtGuadian.Text)
                cm.Parameters.AddWithValue("@emp_g_address", .txtGuadianAddress.Text)
                cm.Parameters.AddWithValue("@emp_g_occu", .txtGuadianOccupation.Text)
                cm.Parameters.AddWithValue("@emp_g_contact", .txtGuadianContact.Text)
                cm.Parameters.AddWithValue("@required_subject_load", .txtRequiredUnits.Text)
                cm.Parameters.AddWithValue("@is_active", .cbStatus.Text)
                cm.Parameters.AddWithValue("@emp_designation", .txtDesignation.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                UserActivity("Updated employee '" & .txtLastName.Text.Trim & ", " & .txtFirstName.Text.Trim & " " & .txtMiddleName.Text.Trim & "' details.", "LIBRARY EMPLOYEE")
                frmWait.seconds = 1
                frmWait.ShowDialog()
                MsgBox("Record has been successfully updated.", vbInformation, "")
                LibraryCourseList()
                .Close()
            End With
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub



    Public Sub query(ByVal sql As String)
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cm.Dispose()
        cn.Close()
    End Sub

    Public Sub query2(ByVal sql As String)
        cn2.Close()
        cn2.Open()
        cm2 = New MySqlCommand(sql, cn2)
        cm2.ExecuteNonQuery()
        cm2.Dispose()
        cn2.Close()
    End Sub

    Public Sub fillCombo(ByVal sql As String, ByVal combo_box As Object, ByVal table As String, ByVal dmember As String, ByVal vmember As String)
        Try
            cn.Close()
            cn.Open()
            Dim dtc As DataTableCollection
            ds = New DataSet
            dtc = ds.Tables
            da = New MySqlDataAdapter(sql, cn)
            da.Fill(ds, table)
            Dim view1 As New DataView(dtc(0))
            With combo_box
                .DataSource = ds.Tables(table)
                .DisplayMember = dmember
                .ValueMember = vmember
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            End With
            cn.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub load_datagrid(ByVal sql As String, ByVal DTG As Object)
        cn.Close()
        cn.Open()
        dt = New DataTable
        With cm
            .Connection = cn
            .CommandText = sql
        End With
        da.SelectCommand = cm
        da.Fill(dt)
        DTG.DataSource = dt
        da.Dispose()
        cn.Close()
    End Sub

    Public Sub load_data(ByVal sql As String, ByVal table As String)
        cn.Close()
        cn.Open()
        da = New MySqlDataAdapter(sql, cn)
        cm = New MySqlCommand(sql)
        da.Fill(ds, table)
        cn.Close()
    End Sub

    Public Function GetColumnSum(ByVal dgv As DataGridView, ByVal colIndex As Integer) As Double
        Dim sum As Double = 0.0
        For Each row As DataGridViewRow In dgv.Rows
            ' Check if cell value is numeric before adding
            If IsNumeric(row.Cells(colIndex).Value) Then
                sum += Convert.ToDouble(row.Cells(colIndex).Value)
            End If
        Next
        Return sum
    End Function

    Public Function GetRowCount(ByVal dgv As DataGridView, ByVal colIndex As Integer) As Double
        Dim sum As Double = 0.0
        For Each row As DataGridViewRow In dgv.Rows
            ' Check if cell value is numeric before adding
            If IsNumeric(row.Cells(colIndex).Value) Then
                sum += Convert.ToDouble(row.Cells(colIndex).Value)
            End If
        Next
        Return sum
    End Function

    Public Sub StockLedger(BarcodeID As String, StockIn As Integer, StockOut As Integer, Remarks As String, TransactionType As String, TransactionCode As String)
        Dim CurrentStockLevel As Integer = 0
        Dim NewStockLevel As Integer = 0
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT Spare from tbl_supply_inventory where itembarcode = '" & BarcodeID & "'", cn)
        CurrentStockLevel = CInt(cm.ExecuteScalar)
        cn.Close()

        NewStockLevel = CurrentStockLevel + StockIn
        NewStockLevel = NewStockLevel - StockOut

        query("INSERT INTO `tbl_supply_ledger`(`sl_itembarcode`, `sl_stockin_added`, `sl_stockout_deducted`, `sl_remark`, `sl_running_balance`, `sl_transaction_type`, `sl_reference_no`) VALUES ('" & BarcodeID & "'," & StockIn & "," & StockOut & ",'" & Remarks & "'," & NewStockLevel & ",'" & TransactionType & "','" & TransactionCode & "')")
        query("UPDATE `tbl_supply_inventory` SET `Spare`=" & NewStockLevel & " WHERE `itembarcode` = '" & BarcodeID & "'")
    End Sub

    Public Sub StockLedgerPhysicalRecount(BarcodeID As String, StockIn As Integer, StockOut As Integer, Remarks As String, TransactionType As String, TransactionCode As String, PhysicalCount As Integer)
        Dim CurrentStockLevel As Integer = 0
        Dim NewStockLevel As Integer = 0
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT Spare as PERIOD from tbl_supply_inventory where itembarcode = '" & BarcodeID & "'", cn)
        CurrentStockLevel = CInt(cm.ExecuteScalar)
        cn.Close()

        NewStockLevel = CurrentStockLevel + StockIn
        NewStockLevel = CurrentStockLevel - StockOut

        query("INSERT INTO `tbl_supply_ledger`(`sl_itembarcode`, `sl_stockin_added`, `sl_stockout_deducted`, `sl_remark`, `sl_running_balance`, `sl_transaction_type`, `sl_reference_no`) VALUES ('" & BarcodeID & "'," & StockIn & "," & StockOut & ",'" & Remarks & "'," & PhysicalCount & ",'" & TransactionType & "','" & TransactionCode & "')")
        query("UPDATE `tbl_supply_inventory` SET `Spare`=" & PhysicalCount & " WHERE `itembarcode` = '" & BarcodeID & "'")
    End Sub
End Module

