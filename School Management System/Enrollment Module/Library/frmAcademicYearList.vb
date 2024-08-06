Imports MySql.Data.MySqlClient
Public Class frmAcademicYearList
    Private Sub frmAcademicYear_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
        'LibraryAcadList()
    End Sub

    Private Sub dgAcadList_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgAcadList.CellMouseEnter
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = CType(sender, DataGridView).Columns(e.ColumnIndex).Name
            If columnName = "colUpdate" Then
                CType(sender, DataGridView).Cursor = Cursors.Hand
            Else
                CType(sender, DataGridView).Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub dgAcadList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgAcadList.CellContentClick
        Dim colname As String = dgAcadList.Columns(e.ColumnIndex).Name
        If colname = "colUpdate" Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_period where period_id = @1", cn)
            With cm
                .Parameters.AddWithValue("@1", dgAcadList.Rows(e.RowIndex).Cells(1).Value.ToString)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                With frmAcademicYear
                    .AcadID = dr.Item("period_id").ToString
                    .txtEnd.Text = dr.Item("period_end_year").ToString
                    .txtStart.Text = dr.Item("period_start_year").ToString
                    .cbSemester.Text = dr.Item("period_semester").ToString
                    .cbStatus.Text = dr.Item("period_status").ToString
                    Try
                        .dtGrad.Value = dr.Item("period_graduation")
                    Catch ex As Exception

                    End Try
                    Try
                        .dtStart.Value = dr.Item("period_enrollment_startdate")
                    Catch ex As Exception

                    End Try
                    Try
                        .dtEnd.Value = dr.Item("period_enrollment_enddate")
                    Catch ex As Exception

                    End Try
                    .cbEnroll.Text = dr.Item("period_enrollment_status").ToString
                    .cbAD.Text = dr.Item("period_enrollment_ad_status").ToString
                    .cbBalance.Text = dr.Item("period_balance_check").ToString
                    .btnSave.Visible = False
                    .btnUpdate.Visible = True
                    .ShowDialog()
                End With
            Else
            End If
            dr.Close()
            cn.Close()
        End If
    End Sub
End Class