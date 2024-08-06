Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmEmployeeList
    Dim employeePhoto() As Byte
    Dim employeeSignature() As Byte

    Private Sub frmSubjectList_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'LibraryEmployeeList()
    End Sub

    Private Sub dgEmployeeList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgEmployeeList.CellMouseEnter
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = CType(sender, DataGridView).Columns(e.ColumnIndex).Name
            If columnName = "colUpdate" Then
                CType(sender, DataGridView).Cursor = Cursors.Hand
            Else
                CType(sender, DataGridView).Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub dgEmployeeList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgEmployeeList.CellContentClick

        Dim colname As String = dgEmployeeList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_employee where emp_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgEmployeeList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmEmployee

                    .EmployeeID = dr.Item("emp_id").ToString
                    .txtBio.Text = dr.Item("emp_code").ToString
                    .txtFirstName.Text = dr.Item("emp_first_name").ToString
                    .txtMiddleName.Text = dr.Item("emp_middle_name").ToString
                    .txtLastName.Text = dr.Item("emp_last_name").ToString
                    .cbSex.Text = dr.Item("emp_gender").ToString
                    .cbCivilStatus.Text = dr.Item("civil_status").ToString
                    .txtAddress.Text = dr.Item("emp_address").ToString
                    .txtContact.Text = dr.Item("contact_no").ToString
                    .txtEmail.Text = dr.Item("email").ToString
                    .dtBirthdate.Value = dr.Item("birthdate")
                    .txtBirthPlace.Text = dr.Item("place_of_birth").ToString

                    .txtSchool.Text = dr.Item("school_graduated").ToString
                    .txtYearGrad.Text = dr.Item("year_graduated").ToString
                    .cbTesda.Text = If(dr.Item("is_tesda").ToString = String.Empty, "No", dr.Item("is_tesda").ToString)
                    .txtTesdaCert.Text = dr.Item("tesda_cert").ToString

                    .txtGuadian.Text = dr.Item("emp_g_name").ToString
                    .txtGuadianAddress.Text = dr.Item("emp_g_address").ToString
                    .txtGuadianContact.Text = dr.Item("emp_g_contact").ToString
                    .txtGuadianOccupation.Text = dr.Item("emp_g_occu").ToString

                    .cbStatus.Text = dr.Item("is_active").ToString
                    .txtSSS.Text = dr.Item("emp_sss").ToString
                    .txtPH.Text = dr.Item("emp_philhealth").ToString
                    .txtPagibig.Text = dr.Item("emp_pagibig").ToString
                    .txtTin.Text = dr.Item("emp_tin").ToString
                    .txtDesignation.Text = dr.Item("emp_designation").ToString
                    .txtRequiredUnits.Text = dr.Item("required_subject_load").ToString
                    .cbEmploymentStatus.Text = dr.Item("emp_status").ToString
                    .dtEmployment.Value = dr.Item("employment_date").ToString
                    dr.Close()
                    cn.Close()
                    Try
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("select emp_photo from tbl_employee where emp_id = @1", cn)
                        With cm
                            .Parameters.AddWithValue("@1", dgEmployeeList.Rows(e.RowIndex).Cells(1).Value.ToString)
                        End With
                        dr = cm.ExecuteReader
                        While dr.Read
                            Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                            Dim array(CInt(len)) As Byte
                            dr.GetBytes(0, 0, array, 0, CInt(len))
                            Dim ms As New MemoryStream(array)
                            Dim bitmap As New System.Drawing.Bitmap(ms)
                            frmEmployee.empPhoto.Image = bitmap
                        End While
                        dr.Close()
                        cn.Close()
                    Catch ex As Exception
                        .empPhoto.Image = .Dummypicture.Image
                    End Try
                    Try
                        cn.Close()
                        cn.Open()
                        cm = New MySqlCommand("select emp_sign_photo from tbl_employee where emp_id = @1", cn)
                        With cm
                            .Parameters.AddWithValue("@1", dgEmployeeList.Rows(e.RowIndex).Cells(1).Value.ToString)
                        End With
                        dr = cm.ExecuteReader
                        While dr.Read
                            Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                            Dim array(CInt(len)) As Byte
                            dr.GetBytes(0, 0, array, 0, CInt(len))
                            Dim ms As New MemoryStream(array)
                            Dim bitmap As New System.Drawing.Bitmap(ms)
                            frmEmployee.empSignature.Image = bitmap
                        End While
                        dr.Close()
                        cn.Close()
                    Catch ex As Exception
                        .empSignature.Image = .Dummysign.Image
                    End Try

                    .btnSave.Visible = False
                    .btnUpdate.Visible = True
                    .ShowDialog()
                End With
            Else
            End If


        End If
    End Sub
End Class