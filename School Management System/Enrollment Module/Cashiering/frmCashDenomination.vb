Imports MySql.Data.MySqlClient

Public Class frmCashDenomination

    Dim CashDenominationCode As String = ""
    Sub LoadRecords()
        Try
            dgCashDenomination.Rows.Clear()
            Dim sql As String
            sql = "select (t1.cd_code) as Code, DATE_FORMAT(t1.cd_date, '%Y-%m-%d') AS 'Date', (t2.AccountName) as Cashier from tbl_cash_denomination t1 JOIN useraccounts t2 ON t1.cd_cashier_id = t2.useraccountID where (t1.cd_code LIKE '%" & txtSearch.Text & "%' or t2.AccountName LIKE '%" & txtSearch.Text & "%') order by cd_date desc limit 50"
            cn.Close()
            cn.Open()
            cm = New MySqlCommand(sql, cn)
            dr = cm.ExecuteReader
            While dr.Read
                dgCashDenomination.Rows.Add(dr.Item("Code").ToString, dr.Item("Date").ToString, dr.Item("Cashier").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            dr.Close()
            cn.Close()
            dgCashDenomination.Rows.Clear()
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadRecords()
    End Sub

    Private Sub btnSearchDate_Click(sender As Object, e As EventArgs) Handles btnSearchDate.Click
        dgCashDenomination.Rows.Clear()
        Dim sql As String
        sql = "select (t1.cd_code) as Code, DATE_FORMAT(t1.cd_date, '%Y-%m-%d') AS 'Date', (t2.AccountName) as Cashier from tbl_cash_denomination t1 JOIN useraccounts t2 ON t1.cd_cashier_id = t2.useraccountID where t1.cd_date = '" & dtDate.Text & "'"
        cn.Close()
        cn.Open()
        cm = New MySqlCommand(sql, cn)
        dr = cm.ExecuteReader
        While dr.Read
            dgCashDenomination.Rows.Add(dr.Item("Code").ToString, dr.Item("Date").ToString, dr.Item("Cashier").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub AutoCodeNumber()
        cn.Close()
        cn.Open()
        Dim currentDateCommand As New MySqlCommand("SELECT date_format(curdate(), '%M %d %Y')", cn)
        Dim s As String = currentDateCommand.ExecuteScalar().ToString()
        Dim t As String = s.Substring(s.Length - 2, 1)
        Dim v As String = s.Substring(s.Length - 3, 1)
        Dim u As String = s.Substring(s.Length - 4, 1)
        Dim yearid As String = u & v & t & s.Substring(s.Length - 1, 1)
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("SELECT cd_code FROM tbl_cash_denomination WHERE cd_code like 'CD" & yearid & "%'", cn)
        dr = cm.ExecuteReader()
        If dr.HasRows Then
            dr.Close()
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT MAX(cd_code) as Code from tbl_cash_denomination", cn)
            Dim lastCode As String = cm.ExecuteScalar
            cn.Close()
            lastCode = lastCode.Remove(0, 6)
            CashDenominationCode = "CD" & yearid & CInt(lastCode) + 1
        Else
            dr.Close()
            CashDenominationCode = "CD" + yearid + "0000001"
            cn.Close()
        End If
        cn.Close()
    End Sub

    Private Sub TextBox5_LostFocus(sender As Object, e As EventArgs) Handles TextBox5.LostFocus
        Try
            If TextBox5.Text = "" Or TextBox5.Text = " " Or TextBox5.Text = "  " Or TextBox5.Text = "   " Or TextBox5.Text = "    " Or TextBox5.Text = "     " Or TextBox5.Text = "      " Then
                TextBox5.Text = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox6_LostFocus(sender As Object, e As EventArgs) Handles TextBox6.LostFocus
        Try
            If TextBox6.Text = "" Or TextBox6.Text = " " Or TextBox6.Text = "  " Or TextBox6.Text = "   " Or TextBox6.Text = "    " Or TextBox6.Text = "     " Or TextBox6.Text = "      " Then
                TextBox6.Text = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox7_LostFocus(sender As Object, e As EventArgs) Handles TextBox7.LostFocus
        Try
            If TextBox7.Text = "" Or TextBox7.Text = " " Or TextBox7.Text = "  " Or TextBox7.Text = "   " Or TextBox7.Text = "    " Or TextBox7.Text = "     " Or TextBox7.Text = "      " Then
                TextBox7.Text = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox8_LostFocus(sender As Object, e As EventArgs) Handles TextBox8.LostFocus
        Try
            If TextBox8.Text = "" Or TextBox8.Text = " " Or TextBox8.Text = "  " Or TextBox8.Text = "   " Or TextBox8.Text = "    " Or TextBox8.Text = "     " Or TextBox8.Text = "      " Then
                TextBox8.Text = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox9_LostFocus(sender As Object, e As EventArgs) Handles TextBox9.LostFocus
        Try
            If TextBox9.Text = "" Or TextBox9.Text = " " Or TextBox9.Text = "  " Or TextBox9.Text = "   " Or TextBox9.Text = "    " Or TextBox9.Text = "     " Or TextBox9.Text = "      " Then
                TextBox9.Text = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox10_LostFocus(sender As Object, e As EventArgs) Handles TextBox10.LostFocus
        Try
            If TextBox10.Text = "" Or TextBox10.Text = " " Or TextBox10.Text = "  " Or TextBox10.Text = "   " Or TextBox10.Text = "    " Or TextBox10.Text = "     " Or TextBox10.Text = "      " Then
                TextBox10.Text = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox11_LostFocus(sender As Object, e As EventArgs) Handles TextBox11.LostFocus
        Try
            If TextBox11.Text = "" Or TextBox11.Text = " " Or TextBox11.Text = "  " Or TextBox11.Text = "   " Or TextBox11.Text = "    " Or TextBox11.Text = "     " Or TextBox11.Text = "      " Then
                TextBox11.Text = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox12_LostFocus(sender As Object, e As EventArgs) Handles TextBox12.LostFocus
        Try
            If TextBox12.Text = "" Or TextBox12.Text = " " Or TextBox12.Text = "  " Or TextBox12.Text = "   " Or TextBox12.Text = "    " Or TextBox12.Text = "     " Or TextBox12.Text = "      " Then
                TextBox12.Text = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox13_LostFocus(sender As Object, e As EventArgs) Handles TextBox13.LostFocus
        Try
            If TextBox13.Text = "" Or TextBox13.Text = " " Or TextBox13.Text = "  " Or TextBox13.Text = "   " Or TextBox13.Text = "    " Or TextBox13.Text = "     " Or TextBox13.Text = "      " Then
                TextBox13.Text = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox14_LostFocus(sender As Object, e As EventArgs) Handles TextBox14.LostFocus
        Try
            If TextBox14.Text = "" Or TextBox14.Text = " " Or TextBox14.Text = "  " Or TextBox14.Text = "   " Or TextBox14.Text = "    " Or TextBox14.Text = "     " Or TextBox14.Text = "      " Then
                TextBox14.Text = "0"
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        Try
            lbl1000.Text = Format(1000 * Val(TextBox5.Text), "n2")
            CalculateDenominationAmount()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        Try
            lbl500.Text = Format(500 * Val(TextBox6.Text), "n2")
            CalculateDenominationAmount()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        Try
            lbl200.Text = Format(200 * Val(TextBox7.Text), "n2")
            CalculateDenominationAmount()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        Try
            lbl100.Text = Format(100 * Val(TextBox8.Text), "n2")
            CalculateDenominationAmount()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        Try
            lbl50.Text = Format(50 * Val(TextBox9.Text), "n2")
            CalculateDenominationAmount()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        Try
            lbl20.Text = Format(20 * Val(TextBox10.Text), "n2")
            CalculateDenominationAmount()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        Try
            lbl10.Text = Format(10 * Val(TextBox11.Text), "n2")
            CalculateDenominationAmount()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        Try
            lbl5.Text = Format(5 * Val(TextBox12.Text), "n2")
            CalculateDenominationAmount()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged
        Try
            lbl1.Text = Format(1 * Val(TextBox13.Text), "n2")
            CalculateDenominationAmount()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged
        Try
            lbl_25.Text = Format(0.25 * Val(TextBox14.Text), "n2")
            CalculateDenominationAmount()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CalculateDenominationAmount()
        Try
            Dim c1 As Double
            Dim c2 As Double
            Dim c3 As Double
            Dim c4 As Double
            Dim c5 As Double
            Dim c6 As Double
            Dim c7 As Double
            Dim c8 As Double
            Dim c9 As Double
            Dim c10 As Double
            Dim t1 As Double
            Dim t2 As Double
            c1 = CDbl(lbl1000.Text)
            c2 = CDbl(lbl500.Text)
            c3 = CDbl(lbl200.Text)
            c4 = CDbl(lbl100.Text)
            c5 = CDbl(lbl50.Text)
            c6 = CDbl(lbl20.Text)
            c7 = CDbl(lbl10.Text)
            c8 = CDbl(lbl5.Text)
            c9 = CDbl(lbl1.Text)
            c10 = CDbl(lbl_25.Text)
            txtGrandTotal.Text = Format(c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + c9 + c10, "n2")
            t1 = txtChequeAmount.Text
            t2 = txtGrandTotal.Text
            txtTotalAmount.Text = Format(t1 + t2, "n2")
            txtBreakDownTotal.Text = Format(t1 + t2, "n2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnFetch_Click(sender As Object, e As EventArgs) Handles btnFetch.Click
        AutoCodeNumber()

        cn.Close()
        cn.Open()
        cm = New MySqlCommand("select csh_ornumber from tbl_cashiering where csh_cashier_id = " & str_userid & " and csh_date between '" & DateTimePickerFrom.Text & "' and '" & DateTimePickerTo.Text & "' and csh_ornumber REGEXP '^[0-9]' order by csh_ornumber asc", cn)
        txtORFrom.Text = Format(cm.ExecuteScalar, "n2")
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("select csh_ornumber from tbl_cashiering where csh_cashier_id = " & str_userid & " and csh_date between '" & DateTimePickerFrom.Text & "' and '" & DateTimePickerTo.Text & "' and csh_ornumber REGEXP '^[0-9]' order by csh_ornumber desc", cn)
        txtORTo.Text = Format(cm.ExecuteScalar, "n2")
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("Select sum(csh_total_amount) from tbl_cashiering where csh_cashier_id = " & str_userid & " and csh_date between '" & DateTimePickerFrom.Text & "' and '" & DateTimePickerTo.Text & "' and csh_ornumber REGEXP '^[0-9]'", cn)
        txtOOR.Text = Format(cm.ExecuteScalar, "n2")
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("Select sum(csh_total_amount) from tbl_cashiering where csh_cashier_id = " & str_userid & " and csh_date between '" & DateTimePickerFrom.Text & "' and '" & DateTimePickerTo.Text & "' and csh_ornumber NOT REGEXP '^[0-9]'", cn)
        txtUOR.Text = Format(cm.ExecuteScalar, "n2")
        cn.Close()
        cn.Close()
        cn.Open()
        cm = New MySqlCommand("Select sum(csh_total_amount) from tbl_cashiering where csh_cashier_id = " & str_userid & " and csh_date between '" & DateTimePickerFrom.Text & "' and '" & DateTimePickerTo.Text & "'", cn)
        txtAmount.Text = Format(cm.ExecuteScalar, "n2")
        cn.Close()
    End Sub

    Private Sub txtChequeAmount_TextChanged(sender As Object, e As EventArgs) Handles txtChequeAmount.TextChanged
        CalculateDenominationAmount()
    End Sub

    Private Sub txtGrandTotal_TextChanged(sender As Object, e As EventArgs) Handles txtGrandTotal.TextChanged
        CalculateDenominationAmount()
    End Sub

    Private Sub txtChequeAmount_LostFocus(sender As Object, e As EventArgs) Handles txtChequeAmount.LostFocus
        txtChequeAmount.Text = Format(Val(txtChequeAmount.Text), "n2")
    End Sub

    Private Sub Clear()
        txtCode.Text = ""
        txtORFrom.Text = ""
        txtORTo.Text = ""
        txtAmount.Text = "0.00"
        txtTotalAmount.Text = "0.00"
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox12.Text = ""
        TextBox13.Text = ""
        TextBox14.Text = ""
        txtChequeAmount.Text = "0.00"
        txtRemarks.Text = ""
        txtAmountRemarks.Text = ""
        'btn_save.Enabled = False
        'denomination_panel.Enabled = False
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Clear()
    End Sub

    Private Sub dgCashDenomination_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCashDenomination.CellContentClick
        Dim colname As String = dgCashDenomination.Columns(e.ColumnIndex).Name
        If colname = "colView" Then
            Try
                load_data("select * from tbl_cash_denomination where cd_code = '" & dgCashDenomination.CurrentRow.Cells(0).Value & "'", "tbl_cash_denomination")
                txtCode.Text = ds.Tables("tbl_cash_denomination").Rows(0)(1).ToString
                txtORFrom.Text = ds.Tables("tbl_cash_denomination").Rows(0)(2).ToString
                DateTimePickerFrom.Text = ds.Tables("tbl_cash_denomination").Rows(0)(3).ToString
                txtORTo.Text = ds.Tables("tbl_cash_denomination").Rows(0)(4).ToString
                DateTimePickerTo.Text = ds.Tables("tbl_cash_denomination").Rows(0)(5).ToString
                txtAmount.Text = ds.Tables("tbl_cash_denomination").Rows(0)(6).ToString
                txtOOR.Text = ds.Tables("tbl_cash_denomination").Rows(0)(7).ToString
                txtUOR.Text = ds.Tables("tbl_cash_denomination").Rows(0)(8).ToString
                txtTotalAmount.Text = ds.Tables("tbl_cash_denomination").Rows(0)(9).ToString
                TextBox5.Text = ds.Tables("tbl_cash_denomination").Rows(0)(10).ToString
                TextBox6.Text = ds.Tables("tbl_cash_denomination").Rows(0)(11).ToString
                TextBox7.Text = ds.Tables("tbl_cash_denomination").Rows(0)(12).ToString
                TextBox8.Text = ds.Tables("tbl_cash_denomination").Rows(0)(13).ToString
                TextBox9.Text = ds.Tables("tbl_cash_denomination").Rows(0)(14).ToString
                TextBox10.Text = ds.Tables("tbl_cash_denomination").Rows(0)(15).ToString
                TextBox11.Text = ds.Tables("tbl_cash_denomination").Rows(0)(16).ToString
                TextBox12.Text = ds.Tables("tbl_cash_denomination").Rows(0)(17).ToString
                TextBox13.Text = ds.Tables("tbl_cash_denomination").Rows(0)(18).ToString
                TextBox14.Text = ds.Tables("tbl_cash_denomination").Rows(0)(19).ToString
                txtChequeAmount.Text = ds.Tables("tbl_cash_denomination").Rows(0)(20).ToString
                txtGrandTotal.Text = ds.Tables("tbl_cash_denomination").Rows(0)(21).ToString
                txtRemarks.Text = ds.Tables("tbl_cash_denomination").Rows(0)(23).ToString
                txtAmountRemarks.Text = ds.Tables("tbl_cash_denomination").Rows(0)(24).ToString
                ds = New DataSet
            Catch ex As Exception
            End Try
        ElseIf colname = "colRemove" Then

        End If
    End Sub

    Private Sub frmCashDenomination_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Try
        Dim dr As DialogResult
        dr = MessageBox.Show("Are you sure you want to save this new cash denomination?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dr = DialogResult.No Then
        Else
            AutoCodeNumber()
            cn.Close()
            cn.Open()
            Dim str As String
            str = "INSERT INTO tbl_cash_denomination (cd_code, cd_total_amount, cd_1000_pcs, cd_500_pcs, cd_200_pcs, cd_100_pcs, cd_50_pcs, cd_20_pcs, cd_10_pcs, cd_5_pcs, cd_1_pcs, cd_25_pcs, cd_cheque_amount, cd_grand_total, cd_date, cd_notes, cd_notes_amount, cd_cashier_id) values (@cd_code, @cd_total_amount, @cd_1000_pcs, @cd_500_pcs, @cd_200_pcs, @cd_100_pcs, @cd_50_pcs, @cd_20_pcs, @cd_10_pcs, @cd_5_pcs, @cd_1_pcs, @cd_25_pcs, @cd_cheque_amount, @cd_grand_total, CURDATE(), @cd_notes, @cd_notes_amount, @cd_cashier_id)"
            cm = New MySqlCommand(str, cn)
            cm.Parameters.AddWithValue("@cd_code", txtCode.Text)
            cm.Parameters.AddWithValue("@cd_total_amount", CDec(txtBreakDownTotal.Text))
            cm.Parameters.AddWithValue("@cd_1000_pcs", TextBox5.Text)
            cm.Parameters.AddWithValue("@cd_500_pcs", TextBox6.Text)
            cm.Parameters.AddWithValue("@cd_200_pcs", TextBox7.Text)
            cm.Parameters.AddWithValue("@cd_100_pcs", TextBox8.Text)
            cm.Parameters.AddWithValue("@cd_50_pcs", TextBox9.Text)
            cm.Parameters.AddWithValue("@cd_20_pcs", TextBox10.Text)
            cm.Parameters.AddWithValue("@cd_10_pcs", TextBox11.Text)
            cm.Parameters.AddWithValue("@cd_5_pcs", TextBox12.Text)
            cm.Parameters.AddWithValue("@cd_1_pcs", TextBox13.Text)
            cm.Parameters.AddWithValue("@cd_25_pcs", TextBox14.Text)
            cm.Parameters.AddWithValue("@cd_cheque_amount", CDec(txtChequeAmount.Text))
            cm.Parameters.AddWithValue("@cd_grand_total", CDec(txtGrandTotal.Text))
            cm.Parameters.AddWithValue("@cd_notes", txtAmountRemarks.Text)
            cm.Parameters.AddWithValue("@cd_notes_amount", txtRemarks.Text)
            cm.Parameters.AddWithValue("@cd_cashier_id", str_userid)
            cm.ExecuteNonQuery()
            cm.Dispose()
            cn.Close()
            MsgBox("Cash Denomination successfully saved.", vbInformation)
            UserActivity("Generated cash denomination for the day with the total amount of " & txtGrandTotal.Text & ".", "CASH DENOMINATION")
            Clear()
            PrintDenomination()
        End If
        'Catch ex As Exception
        'End Try
    End Sub

    Sub PrintDenomination()
        Try
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New CashDenomination2
            rptdoc.SetParameterValue("cd_total_amount", Format(CDec(txtBreakDownTotal.Text), "n2"))
            rptdoc.SetParameterValue("cd_1000", TextBox5.Text)
            rptdoc.SetParameterValue("cd_500", TextBox6.Text)
            rptdoc.SetParameterValue("cd_200", TextBox7.Text)
            rptdoc.SetParameterValue("cd_100", TextBox8.Text)
            rptdoc.SetParameterValue("cd_50", TextBox9.Text)
            rptdoc.SetParameterValue("cd_20", TextBox10.Text)
            rptdoc.SetParameterValue("cd_10", TextBox11.Text)
            rptdoc.SetParameterValue("cd_5", TextBox12.Text)
            rptdoc.SetParameterValue("cd_1", TextBox13.Text)
            rptdoc.SetParameterValue("cd_25", TextBox14.Text)
            rptdoc.SetParameterValue("cd_cheque_amount", Format(CDec(txtChequeAmount.Text), "n2"))
            rptdoc.SetParameterValue("cd_grand_total", Format(CDec(txtGrandTotal.Text), "n2"))
            rptdoc.SetParameterValue("cd_notes", txtRemarks.Text)
            rptdoc.SetParameterValue("cd_notes_amount", txtAmountRemarks.Text)
            rptdoc.SetParameterValue("cd_cashier", str_name)
            rptdoc.SetParameterValue("1000_amount", lbl1000.Text)
            rptdoc.SetParameterValue("500_amount", lbl500.Text)
            rptdoc.SetParameterValue("200_amount", lbl200.Text)
            rptdoc.SetParameterValue("100_amount", lbl100.Text)
            rptdoc.SetParameterValue("50_amount", lbl50.Text)
            rptdoc.SetParameterValue("20_amount", lbl20.Text)
            rptdoc.SetParameterValue("10_amount", lbl10.Text)
            rptdoc.SetParameterValue("5_amount", lbl5.Text)
            rptdoc.SetParameterValue("1_amount", lbl1.Text)
            rptdoc.SetParameterValue("25_amount", lbl_25.Text)
            frmReportViewer.ReportViewer.ReportSource = rptdoc
            frmReportViewer.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class