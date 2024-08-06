Imports MySql.Data.MySqlClient
Imports System.IO
Public Class frmUserList
    Private Sub dgUserList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgUserList.CellContentClick
        Dim colname As String = dgUserList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_user_account where ua_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgUserList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmUser
                    .AccountUserID = dr.Item("ua_id").ToString
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
                            .Parameters.AddWithValue("@1", dgUserList.Rows(e.RowIndex).Cells(1).Value.ToString)
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
            cn.Close()
        End If
    End Sub
End Class