Public Class frmStudentAddress
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(txtStreet) = True Then Return
        If IS_EMPTY(cbProv) = True Then Return
        If IS_EMPTY(cbCity) = True Then Return
        If IS_EMPTY(cbBrgy) = True Then Return

        With frmStudentInfo
            .address_prov_code = cbProv.SelectedValue
            .address_citymun_code = cbCity.SelectedValue
            .address_brgy_code = cbBrgy.SelectedValue
            .address_street = txtStreet.Text.Trim

            .txtAddress.Text = txtStreet.Text.Trim & ", " & cbBrgy.Text & ", " & cbCity.Text & ", " & cbProv.Text
        End With
        Me.Dispose()
    End Sub

    Private Sub cbProv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProv.SelectedIndexChanged
        Try
            fillCombo("SELECT `citymunDesc`, `citymunCode` FROM `refcitymun` where `provCode` = '" & cbProv.SelectedValue.ToString & "' order by citymunDesc asc", cbCity, "refcitymun", "citymunDesc", "citymunCode")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cbCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCity.SelectedIndexChanged
        fillCombo("SELECT `brgyDesc`, `brgyCode` FROM `refbrgy` where `citymunCode` = '" & cbCity.SelectedValue.ToString & "' order by brgyDesc asc", cbBrgy, "refbrgy", "brgyDesc", "brgyCode")
    End Sub

    Private Sub cbProv_DataSourceChanged(sender As Object, e As EventArgs) Handles cbProv.DataSourceChanged
        fillCombo("SELECT `citymunDesc`, `citymunCode` FROM `refcitymun` where `provCode` = '" & cbProv.SelectedValue.ToString & "' order by citymunDesc asc", cbCity, "refcitymun", "citymunDesc", "citymunCode")
    End Sub

    Private Sub cbProv_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbProv.KeyPress, cbCity.KeyPress, cbBrgy.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
End Class