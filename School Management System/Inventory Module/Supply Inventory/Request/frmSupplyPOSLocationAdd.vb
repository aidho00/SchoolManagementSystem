Public Class frmSupplyPOSLocationAdd
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmLocationAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(txtDesc) = True Then Return
        If CHECK_EXISTING("SELECT * FROM tbl_supply_location WHERE locationname = '" & txtDesc.Text & "'") = True Then Return
        query("INSERT INTO `tbl_supply_location`(locationname) VALUES ('" & txtDesc.Text & "')")
        UserActivity("Added a new supply location requester " & txtDesc.Text.Trim & "", "SUPPLY REQUEST LOCATION ENTRY")
        frmWait.seconds = 1
        frmWait.ShowDialog()
        MsgBox("New supply location requester has been successfully added.", vbInformation, "")
        frmSupplyPOSLocation.loadTable()
        Me.Close()
    End Sub
End Class