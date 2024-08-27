Public Class frmSupplySizeAdd
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IS_EMPTY(txtDesc) = True Then Return
        If CHECK_EXISTING("SELECT * FROM cfcissmsdb_supply.tbl_supply_sizes WHERE sizes = '" & txtDesc.Text & "' and category_id = " & frmSupplyItemAdd.CategoryID & "") = True Then Return
        query("INSERT INTO cfcissmsdb_supply.`tbl_supply_sizes`(sizes, category_id) VALUES ('" & txtDesc.Text & "', '" & frmSupplyItemAdd.CategoryID & "')")
        UserActivity("Added a " & frmSupplyItemAdd.cbSupplyCategory.Text & " size " & txtDesc.Text.Trim & "", "SUPPLY ITEM CATEGORY SIZE ENTRY")
        frmWait.seconds = 1
        frmWait.ShowDialog()
        MsgBox("New " & frmSupplyItemAdd.cbSupplyCategory.Text & " size has been successfully added.", vbInformation, "")
        frmSupplyItemAdd.SupplyCategorySizeList()
        Me.Close()
    End Sub

    Private Sub frmSupplySizeAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class