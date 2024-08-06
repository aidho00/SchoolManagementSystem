Imports MySql.Data.MySqlClient

Public Class frmExamSched

    Private Sub frmExamSched_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyHoverEffectToControls(Me)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If MsgBox("Are you sure you want to add this examination schedule?", vbYesNo + vbQuestion) = vbYes Then
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("SELECT * from tbl_assessment_fee_breakdown where afb_period_id = " & CInt(frmAssessment.cbAcademicYear.SelectedValue) & "", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Close()
                cn.Close()
                query("update tbl_assessment_fee_breakdown set afb_breakdown_period_date = " & dtPrelim.Value & " where afb_period_id = " & CInt(frmAssessment.cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'PRELIM'")
                query("update tbl_assessment_fee_breakdown set afb_breakdown_period_date = " & dtMidterm.Value & " where afb_period_id = " & CInt(frmAssessment.cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'MID-TERM'")
                query("update tbl_assessment_fee_breakdown set afb_breakdown_period_date = " & dtSemi.Value & " where afb_period_id = " & CInt(frmAssessment.cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'SEMI-FINAL'")
                query("update tbl_assessment_fee_breakdown set afb_breakdown_period_date = " & dtFinal.Value & " where afb_period_id = " & CInt(frmAssessment.cbAcademicYear.SelectedValue) & " and afb_breakdown_period = 'FINAL'")
                MsgBox("Examination schedule successfully updated.", vbInformation)
            Else
                dr.Close()
                cn.Close()
                query("insert into tbl_assessment_fee_breakdown (afb_period_id, afb_breakdown_period, afb_breakdown_period_date) VALUES (" & CInt(frmAssessment.cbAcademicYear.SelectedValue) & "), 'PRELIM', " & dtPrelim.Value & "")
                query("insert into tbl_assessment_fee_breakdown (afb_period_id, afb_breakdown_period, afb_breakdown_period_date) VALUES (" & CInt(frmAssessment.cbAcademicYear.SelectedValue) & "), 'MID-TERM', " & dtMidterm.Value & "")
                query("insert into tbl_assessment_fee_breakdown (afb_period_id, afb_breakdown_period, afb_breakdown_period_date) VALUES (" & CInt(frmAssessment.cbAcademicYear.SelectedValue) & "), 'SEMI-FINAL', " & dtSemi.Value & "")
                query("insert into tbl_assessment_fee_breakdown (afb_period_id, afb_breakdown_period, afb_breakdown_period_date) VALUES (" & CInt(frmAssessment.cbAcademicYear.SelectedValue) & "), 'FINAL', " & dtFinal.Value & "")
                MsgBox("Examination schedule successfully saved.", vbInformation)
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)

    End Sub
End Class