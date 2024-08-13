Imports MySql.Data.MySqlClient

Public Class frmSupplyPOSLocation

    Dim btnTable As New Button



    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub
    Sub loadTable()
        tablePanel.AutoScroll = True
        tablePanel.Controls.Clear()
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("Select * from vwlocationbill2 where location like '%" & ToolStripTextBox1.Text & "%'", cn)
        dr = cm.ExecuteReader
        While dr.Read
            btnTable = New Button
            btnTable.Width = 151
            btnTable.Height = 40
            If CDbl(dr.Item("bill").ToString) = 0 Then
                btnTable.Text = dr.Item("location").ToString
                btnTable.BackColor = Color.FromArgb(30, 39, 46)
                btnTable.ForeColor = Color.White
            Else
                btnTable.Text = dr.Item("location").ToString & " - ₱" & dr.Item("bill").ToString
                btnTable.BackColor = Color.FromArgb(252, 92, 101)
                btnTable.ForeColor = Color.Black
            End If

            btnTable.Tag = dr.Item("locationnumber")
            btnTable.FlatStyle = FlatStyle.Flat
            btnTable.FlatAppearance.BorderSize = 0

            btnTable.Cursor = Cursors.Hand
            btnTable.TextAlign = ContentAlignment.MiddleLeft
            tablePanel.Controls.Add(btnTable)

            AddHandler btnTable.Click, AddressOf GetTable_Click
        End While
        dr.Close()
        cn.Close()
    End Sub

    Sub GetTable_Click(sender As Object, e As EventArgs)
        Try
            Dim location As String = sender.tag.ToString
            Dim locationname As String
            cn.Open()
            cm = New MySqlCommand("Select locationname from tbl_supply_location where locationnumber = " & location & "", cn)
            dr = cm.ExecuteReader
            While dr.Read
                locationname = dr.Item("locationname").ToString
            End While
            cn.Close()

            If locationname = frmSupplyPOS.lblLocation.Text Then
                With frmSupplyPOS
                    .lblLocation.Text = locationname
                    .lblLocationNumber.Text = location
                End With
                Me.Dispose()
                If frmSupplyPOS.lblLocationNumber.Text = "0" Then
                    frmSupplyPOSStudID.ShowDialog()
                End If
            Else
                Dim found As Boolean
                cn.Open()
                cm = New MySqlCommand("select * from tbl_supply_location where locationnumber = @1 and status = 'False'", cn)
                With cm
                    .Parameters.AddWithValue("@1", location)
                End With
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    found = True
                Else
                    found = False
                End If
                cn.Close()

                If found = True Then
                    cn.Open()
                    cm = New MySqlCommand("Update tbl_supply_location set status = 'True' where locationnumber = " & location & "", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()

                    cn.Open()
                    cm = New MySqlCommand("Update tbl_supply_location set status = 'False' where locationnumber = '" & frmSupplyPOS.lblLocationNumber.Text & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()

                    With frmSupplyPOS
                        .lblLocation.Text = locationname
                        .lblLocationNumber.Text = location
                        .loadCart()
                    End With

                    'If frmDeployedPOS.lblLocation.Text = String.Empty Then
                    'Else
                    '    AuditTrail("Closed table " & frmDeployedPOS.lblLocation.Text & ".")
                    'End If

                    'AuditTrail("Opens table " & table & " for a new order.")

                    Me.Dispose()
                    If frmSupplyPOS.lblLocationNumber.Text = "0" Then
                        frmSupplyPOSStudID.ShowDialog()
                    End If
                ElseIf found = False Then
                    MsgBox("This office/table is already open for ordering/billing on another user's transaction window!", vbExclamation)
                    Return
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub frmSelectTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        cn.Close()
    End Sub

    Private Sub frmSelectTable_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Dispose()
        End If
    End Sub

    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox1.TextChanged
        loadTable()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        ResetControls(frmSupplyPOSLocationAdd)
        frmSupplyPOSLocationAdd.ShowDialog()
    End Sub

    Private Sub tablePanel_Paint(sender As Object, e As PaintEventArgs) Handles tablePanel.Paint

    End Sub
End Class