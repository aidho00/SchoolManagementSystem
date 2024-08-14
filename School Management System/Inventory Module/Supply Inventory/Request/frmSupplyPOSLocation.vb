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

            frmSupplyPOS.lblLocationNumber.Text = "0"
            frmSupplyPOS.txtItemID.Clear()
            frmSupplyPOS.lblTotal.Text = "0.00"
            frmSupplyPOS.lblLocation.Text = ""
            frmSupplyPOS.lblTransno.Text = ""
            frmSupplyPOS.dgCart.Rows.Clear()

            frmSupplyPOS.stud_gender.Text = ""
            frmSupplyPOS.stud_id.Text = ""
            frmSupplyPOS.stud_name.Text = ""
            frmSupplyPOS.stud_yrcourse.Text = ""
            frmSupplyPOS.cmb_period.DataSource = Nothing

            Dim location As String = sender.tag.ToString
            Dim locationname As String
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("Select locationname from tbl_supply_location where locationnumber = " & location & "", cn)
            dr = cm.ExecuteReader
            While dr.Read
                locationname = dr.Item("locationname").ToString
            End While
            cn.Close()

            If frmSupplyPOS.lblLocation.Text = "STUDENT" Then
                With frmSupplyPOS
                    .lblLocationNumber.Text = location
                    .lblLocation.Text = locationname
                End With
            Else
                With frmSupplyPOS
                    .lblLocationNumber.Text = location
                    .lblLocation.Text = locationname
                    .loadCart()
                End With
            End If
            Me.Close()
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
End Class