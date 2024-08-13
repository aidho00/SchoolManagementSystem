Public Class frmSupplyBrandAdd
    Private Sub frmSupplyBrandAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(txtDesc) = True Then Return
        If CHECK_EXISTING("SELECT * FROM tbl_supply_brand WHERE brandname = '" & txtDesc.Text & "' and catid = " & frmSupplyItemAdd.CategoryID & "") = True Then Return
        query("INSERT INTO `tbl_supply_brand` (brandname, catid) VALUES ('" & txtDesc.Text & "',  " & frmSupplyItemAdd.CategoryID & ")")
        UserActivity("Added a(n) " & frmSupplyItemAdd.cbSupplyType.Text & " category " & frmSupplyItemAdd.cbSupplyCategory.Text & " brand " & txtDesc.Text & "", "SUPPLY ITEM BRAND ENTRY")
        frmWait.seconds = 1
        frmWait.ShowDialog()
        MsgBox("New category " & frmSupplyItemAdd.cbSupplyCategory.Text & " brand has been successfully added.", vbInformation, "")
        frmSupplyItemAdd.SupplyBrandList()
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class