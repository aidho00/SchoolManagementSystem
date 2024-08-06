Public Class frmSupplyCategoryAdd
    Private Sub frmSupplyCategoryAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(txtDesc) = True Then Return
        If CHECK_EXISTING("SELECT * FROM tbl_supply_category WHERE categoryname = '" & txtDesc.Text & "' and categorytype = '" & frmSupplyItemAdd.cbSupplyType.Text & "'") = True Then Return
        query("INSERT INTO `tbl_supply_item`(categoryname, categorytype) VALUES ('" & txtDesc.Text & "', '" & frmSupplyItemAdd.cbSupplyType.Text & "'")
        UserActivity("Added a(n) " & frmSupplyItemAdd.cbSupplyType.Text & " category " & txtDesc.Text.Trim & "", "SUPPLY ITEM ENTRY")
        frmWait.seconds = 1
        frmWait.ShowDialog()
        MsgBox("New " & frmSupplyItemAdd.cbSupplyType.Text & " category has been successfully added.", vbInformation, "")
        frmSupplyItemAdd.SupplyCategoryList()
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class